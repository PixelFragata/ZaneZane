using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZZ_ERP.Infra.CrossCutting.Connections.Commons;

#pragma warning disable 1998



namespace ZZ_ERP.Infra.CrossCutting.Connections.Functions
{
    public class SerializerAsync
    {
        public static async Task<string> SerializeJson<T>(T entity)
        {
            string data;
            try
            {
                data = JsonConvert.SerializeObject(entity);
            }
            catch (Exception e)
            {

                ConsoleEx.WriteLine("Deu merda ao serializar o Json");
                Console.WriteLine(e);
                data = ServerCommands.EmptyStr;
            }
            return data;
        }

        public static async Task<T> DeserializeJson<T>(string data)
        {
            T entity;
            try
            {
                entity = JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine("Deu merda ao deserializar o Json");
                //Console.WriteLine(e);
                entity = default(T);
            }
            return entity;
        }

        public static async Task <string> SerializeJsonList<T>(List<T> entity)
        {
            string data;
            try
            {
                data = JsonConvert.SerializeObject(entity, Formatting.Indented);
                //data = String.Format(await SerializerAsync.SerializeJson<MercenaryPreset>(mercTmp));
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine("Deu merda ao serializar a lista de Json");
                Console.WriteLine(e);
                data = ServerCommands.EmptyStr;
            }
            return data;
        }

        public static async Task <List<T>> DeserializeJsonList<T>(string data)
        {
            List<T> entity;
            try
            {
                entity = JsonConvert.DeserializeObject<List<T>>(data);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteLine("Deu merda ao deserializar a lista do Json");
                Console.WriteLine(e);
                entity = null;
            }
            return entity;
        }

    }
}
