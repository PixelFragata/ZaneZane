using System;
using System.Collections.Generic;
using System.Text;

namespace ZZ_ERP.Infra.CrossCutting.Connections.Commons
{
    public static class AdressPool
    {
        private static ServerIP SrvIp = ServerIP.ServerDebug;

        public enum ServerIP
        {
            ServerUniversal,
            ServerDebug,
            ServerClient
        }

        public static string GetIp(ServerIP srv)
        {
            switch (srv)
            {
                case ServerIP.ServerUniversal:
                    return "192.168.1.104";
                case ServerIP.ServerDebug:
                    return "127.0.0.1";
                case ServerIP.ServerClient:
                    return "35.160.15.132";
                default:
                    return "";
            }
        }

        public static class ZZ_EF_APK
        {
            public static string Ip = GetIp(SrvIp);
            public static int Port = 6666;
        }
    }
}
