using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Client.Common
{
    public static class ObjectSerialize
    {
        public static string SerializeToJson(this Object obj)
        {
            if (obj == null)
            {
                return null;
            }        

            return JsonConvert.SerializeObject(obj);
        }
        public static byte[] SerializeToBytes(this Object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var jsonString = JsonConvert.SerializeObject(obj);

            return Encoding.ASCII.GetBytes(jsonString);
        }


        public static string DeSerializeToString(this byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }

        public static object DeSerializeToObject(this byte[] bytes)
        {
            var decodedString = Encoding.ASCII.GetString(bytes);
            var obj = JsonConvert.DeserializeObject(decodedString);

            return obj;
        }
        public static object DeSerializeToObject(this byte[] bytes,Type type)
        {
            var decodedString = Encoding.ASCII.GetString(bytes);

            return JsonConvert.DeserializeObject(decodedString, type); ;
        }
    }
}
