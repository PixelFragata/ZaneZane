using System;
using System.Collections.Generic;
using System.Linq;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.Data.Contexts;

namespace ZZ_ERP.DataApplication
{
    public class ZZApkMain
    {
        public static ZZServer ZZServer;
        private const string DebugIp = "127.0.0.1";
        private const string ServerIp = "";
        private const int Port = 7000;

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Run();
            //using (var context = new ZZContext())
            //{
            //    var tipos = context.TipoServicos.ToList();
            //    Console.WriteLine(tipos.Count);

            //    foreach (var tipo in tipos)
            //    {
            //        Console.WriteLine("Tipo serviço :  " + tipo.DescricaoServico);
            //    }
            //}

            //Console.ReadKey();
        }

        public static void Run()
        {
            ConsoleEx.WriteLine("============================================");
            ConsoleEx.WriteLine("============================================");
            ConsoleEx.WriteLine("||||||||||Bem vindo a Zane & Zane|||||||||||");
            ConsoleEx.WriteLine("============================================");
            ConsoleEx.WriteLine("============================================");

            try
            {
                ZZServer = new ZZServer(DebugIp, Port);
                ZZServer.Start();
            }
            catch (InvalidCastException e)
            {
                ConsoleEx.WriteLine("Error :( :" + e.Source);
            }

        }
    }
}
