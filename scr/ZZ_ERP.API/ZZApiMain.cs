using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZZ_ERP.DataApplication;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;
using ZZ_ERP.Infra.CrossCutting.Connections.Connections;

namespace ZZ_ERP.API
{
    public class ZZApiMain
    {
        private static Thread _dataThread;
        public static ServerConnection Zz;
        public static long Id;

        public static void Main(string[] args)
        {

            var timestamp = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
            Console.WriteLine("====================================");
            Console.WriteLine("====================================");
            Console.WriteLine("||||||||||Welcome to ZZ API|||||||||");
            Console.WriteLine("====================================");
            Console.WriteLine("====================================");
            Console.WriteLine("Time :  " + timestamp);

            _dataThread = new Thread(ZZApkMain.Run);
            _dataThread.Start();
            Thread.Sleep(250);
            CreateWebHostBuilder(args).Build().Run();
            Zz = new ServerConnection("127.0.0.1", 7000);
            Zz.WriteServer(new Command{Cmd = ServerCommands.IsController }).RunSynchronously();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }
}
