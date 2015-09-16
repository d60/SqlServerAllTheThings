using System;
using System.Text;
using Newtonsoft.Json;

namespace HybridTjek.Util
{
    //public class CustomSerializer : ISerializer
    //{
    //    static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        TypeNameHandling = TypeNameHandling.All
    //    };

    //    static readonly Encoding TextEncoding = Encoding.UTF8;

    //    public byte[] Serialize(object obj)
    //    {
    //        var jsonText = JsonConvert.SerializeObject(obj, Settings);

    //        return TextEncoding.GetBytes(jsonText);
    //    }

    //    public object Deserialize(byte[] data, Type type)
    //    {
    //        var jsonText = TextEncoding.GetString(data);

    //        try
    //        {
    //            return JsonConvert.DeserializeObject(jsonText, Settings);
    //        }
    //        catch (Exception exception)
    //        {
    //            throw new JsonSerializationException($"Could not deserialize '{jsonText}'!", exception);
    //        }
    //    }
    //}
}