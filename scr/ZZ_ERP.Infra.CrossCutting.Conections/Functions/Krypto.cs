using System;
using System.Linq;

namespace ZZ_ERP.Infra.CrossCutting.Connections.Functions
{
    public class Krypto
    {
        public static string Encode(string aStr)
        {
            return aStr;
        }

        public static string Decode(string aStr)
        {

            return aStr;
        }
        
        public static string RandonAlphanumeric(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return result;
        }
    }
}
