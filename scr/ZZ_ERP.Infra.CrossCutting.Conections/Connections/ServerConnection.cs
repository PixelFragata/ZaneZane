using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using Timer = System.Timers.Timer;

namespace ZZ_ERP.Infra.CrossCutting.Connections.Connections
{
    public class ServerConnection: IDisposable
    {
        private IPAddress _ip;
        private int _port;
        private TcpClient _clientSocket;
        private NetworkStream _stream;
        private StreamReader _sr;
        private StreamWriter _sw;
        private Timer _timerWrite;
        private Timer _timerRead;
        private Timer _timerHearthBit;
        private Timer _timerHeartBitCheck;
        private Timer _timerBufferRead;
        private Timer _timerDelegate;
        private int _hearthBitCount;
        private int _heartBitTimeOut = 20;
        public bool ClientConnected = true;
        private Queue<string> _bufferWrite = new Queue<string>();
        private Queue<string> _bufferRead = new Queue<string>();
        private Dictionary<string, DelegateAction> _srvsCmdsBuffer = new Dictionary<string, DelegateAction>();
        private object _owner;
        private bool _bWriteBuffer;
        
        public ServerConnection(TcpClient client)
        {
            try
            {
                _clientSocket = client;
                _stream = _clientSocket.GetStream();
                _sr = new StreamReader(_stream);
                _sw = new StreamWriter(_stream);
                StartCommons();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine(e.Message);
            }
        }

        public ServerConnection(string ip, int port)
        {
            try
            {
                _clientSocket = new TcpClient();

                _ip = IPAddress.Parse(ip);
                _port = port;
                _clientSocket.Connect(_ip, _port);

                _stream = _clientSocket.GetStream();
                _sr = new StreamReader(_stream);
                _sw = new StreamWriter(_stream);
                StartCommons();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine(port + e.Message);
            }
        }
        
        public ServerConnection(string ip, int port, object owner)
        {
            try
            {
                _clientSocket = new TcpClient();

                _ip = IPAddress.Parse(ip);
                _port = port;
                _clientSocket.Connect(_ip, _port);

                _stream = _clientSocket.GetStream();
                _sr = new StreamReader(_stream);
                _sw = new StreamWriter(_stream);
                _owner = owner;
                StartCommons();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine(port + e.Message);
            }
        }
        
        public ServerConnection(TcpClient client, object owner)
        {
            try
            {
                _clientSocket = client;
                _stream = _clientSocket.GetStream();
                _sr = new StreamReader(_stream);
                _sw = new StreamWriter(_stream);
                _owner = owner;
                StartCommons();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine(e.Message);
            }
        }

        public void StartCommons()
        {
            _timerRead = SetTimer(200.0f, ReadBuffer);
            _timerWrite = SetTimer(100.0f, WriteBuffer);

            _timerHearthBit = SetTimer(1000.0f, HearthBit);
            _timerBufferRead = SetTimer(50.0f,Read);
            _timerHeartBitCheck = SetTimer(1000.0f, HearthBitCheck);
            _timerDelegate = SetTimer(50.0f, DelegateExec);
        }

        public static Timer SetTimer(double interval, ElapsedEventHandler e)
        {
            Timer t = new Timer(interval);
            t.Elapsed += e;
            t.AutoReset = true;
            t.Enabled = true;

            return t;
        }

        private void DelegateExec(Object source, ElapsedEventArgs e)
        {
            if (_bufferRead.Count > 0)
            {
                var dataDescomp = _bufferRead.Dequeue();
                
                DelegateAction del;
                
                if (_srvsCmdsBuffer.TryGetValue(ServerCommands.BroadCastId, out del))
                {
                    del.server[0] = dataDescomp;
                    del.Exec();
                }
            }
        }

        private async void Read(Object source, ElapsedEventArgs e)
        {
            try
            {
//                _timerRead.Enabled = false;

                if (_clientSocket?.Client?.Available > 0)
                {
                    _hearthBitCount = 0;

                    var data = _sr?.ReadLine();

                    string dataDescomp = await StringCompressionAsync.DecompressString(data);

                    try
                    {
                        var bt = await SerializerAsync.DeserializeJson<Command>(dataDescomp);

                        if (bt != null)
                        {
                            if (!bt.Cmd.Equals(ServerCommands.HearthBit))
                            {
                                //_bufferRead.Enqueue(dataDescomp);
//                            ConsoleEx.WriteLine("Olha o que eu coloquei aqui: " +dataDescomp);


                                DelegateAction del;

                                if (!string.IsNullOrEmpty(bt.Id) &&
                                    _srvsCmdsBuffer.TryGetValue(bt.Id, out del))
                                {
                                    del.server[0] = dataDescomp;
                                    del.Exec();
                                }
                                else if (_srvsCmdsBuffer.TryGetValue(ServerCommands.BroadCastId, out del))
                                {
                                    del.server[0] = dataDescomp;
                                    del.Exec();
                                }

                            }
                        }
                        else
                        {
                            _bufferRead.Enqueue(dataDescomp);
                        }
                    }
                    catch (Exception ex)
                    {
                        ConsoleEx.WriteError("Read", ex);
                        //Dispose();

                    }
                }
            }
            catch (Exception exception)
            {
                ConsoleEx.WriteError("erro do Read:", exception);
                //Dispose();
            }
            finally
            {
//                _timerRead.Enabled = true;
            }

        }

        private async void HearthBit(Object source, ElapsedEventArgs e)
        {
            try
            {
                await WriteServer(ServerCommands.HearthBit, ServerCommands.HearthBit, "");
            }
            catch (Exception exception)
            {
                ConsoleEx.WriteError("Nao consegui bater", exception);
            }
        }
        
        private async void HearthBitCheck(Object source, ElapsedEventArgs e)
        {
            try
            {
                if (_hearthBitCount >= _heartBitTimeOut)
                {
                    _timerHeartBitCheck.Stop();
                    ConsoleEx.WriteLine("Dispose do HB");
                    Dispose();
                }
                else
                {
                    _hearthBitCount += 1;
                }
            }
            catch (Exception exception)
            {
                ConsoleEx.WriteError("Nao consegui bater", exception);
            }
        }

        /// <summary>
        /// Escreve o buffer no socket
        /// </summary>
        private async void WriteBuffer(Object source, ElapsedEventArgs e)
        {
            string command = "";
            try
            {
                if (_bWriteBuffer)
                {
                    return;
                }
                try
                {
                    _bWriteBuffer = true;
                    if (_bufferWrite.Count > 0)
                    {
                        command = _bufferWrite.Dequeue();
                        //ConsoleEx.WriteLine("Escrevi" + command);

                        if (command != null)
                        {
                            string data = await StringCompressionAsync.CompressString(command);
                            await _sw.WriteLineAsync(data).ConfigureAwait(false);
                            await _sw.FlushAsync().ConfigureAwait(false);
                        }
                    }
                }
                finally
                {
                    _bWriteBuffer = false;
                }
            }
            catch (Exception exception)
            {
                ConsoleEx.WriteLine("Tentei escrever | " + command);
                _timerWrite.Enabled = false;
                Dispose();
                //ConsoleEx.WriteError("Erro no WriteBuffer", exception);
            }
        }
        
        /// <summary>
        /// Escreve um comando para o servidor de forma assincrona.
        /// </summary>
        /// <param name="command">Command.</param>
        public async Task WriteServer(string command)
        {
            _bufferWrite.Enqueue(command);
        }


        /// <summary>
        /// Envia para o client um Command com id, comando e json
        /// </summary>
        /// <param name="cmd">Command que contem id, comando e json</param>
        /// <returns></returns>
        public async Task WriteServer(Command cmd)
        {
            if(string.IsNullOrEmpty(cmd.Id))
            {
                cmd.Id = ServerCommands.BroadCastId;
            }

            var cmdSend = await SerializerAsync.SerializeJson(cmd);
            _bufferWrite.Enqueue(cmdSend);
        }
        
        public async Task WriteServer(string id, string command, string json)
		{
            if(string.IsNullOrEmpty(id))
            {
                id = ServerCommands.BroadCastId;
            }

            var cmd = new Command {Id = id, Cmd = command, Json = json};

            var cmdSend = await SerializerAsync.SerializeJson(cmd);
		    _bufferWrite.Enqueue(cmdSend);

		}
        
        public async Task WriteServer(string id, long playerId, string command, string json)
        {
            if(string.IsNullOrEmpty(id))
            {
                id = ServerCommands.BroadCastId;
            }

            var cmd = new Command {Id = id, EntityId = playerId, Cmd = command, Json = json};

            var cmdSend = await SerializerAsync.SerializeJson(cmd);
		    _bufferWrite.Enqueue(cmdSend);
		}

        public async Task WriteServer(DelegateAction del, string id, string command, string json)
        {
            if(string.IsNullOrEmpty(id))
            {
                id = await GetCommandId();
            }

            var cmd = new Command {Id = id, Cmd = command, Json = json};

            var cmdSend = await SerializerAsync.SerializeJson(cmd);
            if (!_srvsCmdsBuffer.ContainsKey(id))
            {
                _srvsCmdsBuffer.Add(id, del);
            }
            
            _bufferWrite.Enqueue(cmdSend);
        }
        
        public async Task WriteServer(DelegateAction del, Command cmd)
        {
            if (string.IsNullOrEmpty(cmd.Id))
            {
                cmd.Id = await GetCommandId();
            }
            var cmdSend = await SerializerAsync.SerializeJson(cmd);
            
            if (!_srvsCmdsBuffer.ContainsKey(cmd.Id))
            {
                _srvsCmdsBuffer.Add(cmd.Id, del);
            }
            
            _bufferWrite.Enqueue(cmdSend);
        }
        
        public async Task PutDelegate(DelegateAction del)
        {
            if (!_srvsCmdsBuffer.ContainsKey(ServerCommands.BroadCastId))
            {
                _srvsCmdsBuffer.Add(ServerCommands.BroadCastId, del);
            }
        }

        /// <summary> 
        /// Le do socket e armazena no buffer
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void ReadBuffer(Object source, ElapsedEventArgs e)
        {
            
        }

        /// <summary>
        /// Lê uma string do servidor de forma assincrona.
        /// </summary>
        /// <returns>The server.</returns>
        public async Task <string> ReadServer()
        {
            ConsoleEx.WriteLine("Passei pelo Read");
            /*
            try
            {
                while (_bufferRead.Count == 0 )
                {
                    Thread.Sleep(10);

                    if (!ClientConnected)
                    {
                        return ServerCommands.Exit;
                    }
                }
                var strTmp = _bufferRead.Dequeue();
                
                ConsoleEx.WriteLine("Recebendo no ReadServer");

                ConsoleEx.WriteLine("Devolvi" + strTmp);
                DelegateAction del;
                if (_srvsCmdsBuffer.TryGetValue(ServerCommands.BroadCastId, out del))
                {
                    del.server[0] = strTmp;
                    del.Exec();
                }
                else
                {
                    var cmd = SerializerAsync.DeserializeJson<Command>(strTmp);
                    if (cmd != null)
                    {
                        if (_srvsCmdsBuffer.TryGetValue(cmd.Id.ToString(), out del))
                        {
                            del.server[0] = strTmp;
                            del.Exec();
                        }
                    }
                        
                }
                return strTmp;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError("Erro no ReadServer",e);
                return null;
            }*/
            return null;
        }
         
        public StreamReader Reader
        {
            get { return _sr; }
        }

        public StreamWriter Writer
        {
            get { return _sw; }
        }
          
        public TcpClient Client
        {
            get { return _clientSocket; }
        }

        public void Dispose()
        {
            try
            {   
                ClientConnected = false;
                _timerRead.Stop();
                _timerBufferRead.Stop();
                _timerWrite.Stop();
                _timerHeartBitCheck.Stop();
                _timerHearthBit.Stop();
                _timerDelegate.Stop();

                _timerRead.Dispose();
                _timerWrite.Dispose();
                _timerHeartBitCheck.Dispose();
                _timerHearthBit.Dispose();
                _timerBufferRead.Dispose();
                _timerDelegate.Dispose();


                ConsoleEx.WriteLine("Dispose da Conn");
                if (_owner != null)
                {
                    var disposable = _owner as IDisposable;
                    if (disposable != null) disposable.Dispose();
                }
                ConsoleEx.WriteLine("Server Conn | " + _owner + " | Destruido");
                GC.SuppressFinalize(this);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError("Erro no Dispose da Conn",e);
            }
        }
        
        /// <summary>
        /// Converte um array de object recebido como entrada de um DelegateAction em Command
        /// </summary>
        /// <param name="s"></param>
        /// <returns><typeparam name="dataJson"></typeparam></returns>
        public static async Task<Command> ObjectToCommand(Object[] s)
        {
            try
            {
                var data = s[0].ToString();
                Command dataJson = new Command(); 
                
                dataJson = await SerializerAsync.DeserializeJson<Command>(data);

                return dataJson;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return null;
            }
        }

        public static async Task<string> GetCommandId()
        {
            Thread.Sleep(2);
            return DateTime.Now.ToString("ddMMyyyyHHmmssfff");
        }
    }
}

