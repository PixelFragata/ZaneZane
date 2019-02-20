using System;
using System.Threading;
using ZZ_ERP.API;
using ZZ_ERP.DataApplication;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;

namespace TestServers
{
    public class Program
    {
        private static Thread _dataThread;
        private static Thread _apiThread;

        static void Main(string[] args)
        {
            var timestamp = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
            ConsoleEx.WriteLine("====================================");
            ConsoleEx.WriteLine("====================================");
            ConsoleEx.WriteLine("|||||||||||Welcome to Adam||||||||||");
            ConsoleEx.WriteLine("====================================");
            ConsoleEx.WriteLine("====================================");
            ConsoleEx.WriteLine("Time :  " + timestamp);


            _dataThread = new Thread(ZZApkMain.Run);
            _dataThread.Start();
            Thread.Sleep(250);

            ZZApiMain.CreateWebHostBuilder(args).Build();

            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
