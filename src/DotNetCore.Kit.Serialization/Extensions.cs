using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

/// <summary>
/// Serialization.Extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    /// SerializeBinary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static string SerializeBinary<T>(this T @this)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            binaryFormatter.Serialize(memoryStream, @this);
            return Encoding.Default.GetString(memoryStream.ToArray());
        }
    }

    /// <summary>
    /// SerializeBinary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string SerializeBinary<T>(this T @this, Encoding encoding)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            binaryFormatter.Serialize(memoryStream, @this);
            return encoding.GetString(memoryStream.ToArray());
        }
    }

    /// <summary>
    /// SerializeToJson
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string SerializeToJson(this object input)
    {
        return JsonConvert.SerializeObject(input);
    }
}