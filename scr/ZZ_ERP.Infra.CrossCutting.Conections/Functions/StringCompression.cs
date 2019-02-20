using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;

#pragma warning disable 1998

namespace ZZ_ERP.Infra.CrossCutting.Connections.Functions
{
    public class StringCompressionAsync
    {
        public static async Task<string> CompressString(string text)
        {
            try
            {
                if (text == null || text.Equals(""))
                    text = ServerCommands.EmptyStr;
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                var memoryStream = new MemoryStream();
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gZipStream.Write(buffer, 0, buffer.Length);
                }

                memoryStream.Position = 0;

                var compressedData = new byte[memoryStream.Length];
                memoryStream.Read(compressedData, 0, compressedData.Length);

                var gZipBuffer = new byte[compressedData.Length + 4];
                Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
                return Convert.ToBase64String(gZipBuffer);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine("Deu merda ao comprimir a string");
                Console.WriteLine(e);
                return ServerCommands.EmptyStr;
            }
        }

        public static async Task<string> DecompressString(string compressedText)
        {
            try
            {
                if(compressedText == null || compressedText.Equals(ServerCommands.EmptyStr) )
                {
                    ConsoleEx.WriteLine("possivel merda ao comprimir, chegou comando vazio aqui...");
                    return ServerCommands.Exit;
                }
                byte[] gZipBuffer = Convert.FromBase64String(compressedText);
                using (var memoryStream = new MemoryStream())
                {
                    int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                    memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                    var buffer = new byte[dataLength];

                    memoryStream.Position = 0;
                    using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        gZipStream.Read(buffer, 0, buffer.Length);
                    }

                    string dataTmp = Encoding.UTF8.GetString(buffer);

                    if (dataTmp.Equals(ServerCommands.EmptyStr))
                        dataTmp = "";
                    
                    return dataTmp;
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine("Deu merda ao descomprimir a string");
                Console.WriteLine(e);
                return "";
            }
        }
    }
}

