using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Appinion.Helper
{
    public class FormatHelper
    {
        public static String Serializer(Type type, Object obj)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer;
            serializer = new DataContractJsonSerializer(type);
            serializer.WriteObject(stream, obj);
            return Encoding.Default.GetString(stream.ToArray());
        }
        public static string Serializer(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static bool TryDeSerializer<T>(string json)
        {
            try
            {
                DeSerializer<T>(json);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static T DeSerializer<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        }
        public static DateTime SetTime(DateTime dateTimeSource, string time)
        {
            return Convert.ToDateTime(dateTimeSource.ToShortDateString() + " " + time);
        }

        public static string RemovePrep(string keyWord)
        {
            return keyWord
                .Replace("de", string.Empty)
                .Replace("da", string.Empty)
                .Replace("das", string.Empty)
                .Replace("do", string.Empty)
                .Replace("dos", string.Empty)
                .Replace("di", string.Empty);

        }

        public static string RemoveAccents(string word)
        {
            return System.Text.Encoding.UTF8.GetString(System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(word));
        }
    }
}
