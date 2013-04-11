//@ivan A0086401M
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ToDo
{
    /// <summary>
    /// 
    /// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Serializes an object into an XML string.
        /// </summary>
        /// <param name="obj">The object to serialize</param>
        /// <returns>The XML string representation of the object.</returns>
        public static string Serialize<T>(this T obj)
        {
            var serializer = new DataContractSerializer(obj.GetType());
            using (var writer = new StringWriter())
            using (var stm = new XmlTextWriter(writer))
            {
                serializer.WriteObject(stm, obj);         
                string check = writer.ToString();
                return writer.ToString();
            }
        }

        /// <summary>
        /// Deserializes an object into an XML string.
        /// </summary>
        /// <param name="serialized">The object to deserialize.</param>
        /// <returns>The original object that is deserialized from it's string representation.</returns>
        public static T Deserialize<T>(this string serialized)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var reader = new StringReader(serialized))
            using (var stm = new XmlTextReader(reader))
            {
                return (T)serializer.ReadObject(stm);
            }
        }

        /// <summary>
        /// Serializes any object to an XElement.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized XElement.</returns>
        public static XElement ToXElement<T>(this object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, obj, namespaces);
                    return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));                    
                }
            }
        }

        /// <summary>
        /// Deserializes an XElement into it's original object.
        /// </summary>
        /// <param name="xElement">The XElement to deserialize.</param>
        /// <returns>The original object that was serialized into an XElement.</returns>
        public static T FromXElement<T>(this XElement xElement)
        {            
            using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(xElement.ToString())))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(memoryStream);
            }
        }
    }
}
