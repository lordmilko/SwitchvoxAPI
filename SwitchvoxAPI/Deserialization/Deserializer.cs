using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SwitchvoxAPI.Deserialization
{
    static class Deserializer
    {
        internal static T Deserialize<T>(XDocument xml) where T : new()
        {
            var root = xml.Descendants(typeof(T).GetXmlRoot()).Single();

            try
            {
                var deserializer = new XmlSerializer(typeof (T));
                var obj = deserializer.Deserialize(root);

                return (T) obj;
            }
            catch (InvalidOperationException ex)
            {
                Exception ex1 = null;

                try
                {
                    ex1 = GetInvalidXml(root, ex, typeof(T));
                }
                catch
                {
                }

                if (ex1 != null)
                    throw ex1;
                else
                    throw;
            }
        }

        private static Stream GetStream(XElement root)
        {
            var stream = new MemoryStream();
            root.Save(stream);
            stream.Position = 0;

            return stream;
        }

        private static Exception GetInvalidXml(XElement root, InvalidOperationException ex, Type type)
        {
            var stream = GetStream(root);

            var xmlReader = (XmlReader)new XmlTextReader(stream)
            {
                WhitespaceHandling = WhitespaceHandling.Significant,
                Normalization = true,
                XmlResolver = null
            };

            var regex = new Regex("(.+\\()(.+)(, )(.+)(\\).+)");
            var line = Convert.ToInt32(regex.Replace(ex.Message, "$2"));
            var position = Convert.ToInt32(regex.Replace(ex.Message, "$4"));

            while (xmlReader.Read())
            {
                IXmlLineInfo xmlLineInfo = (IXmlLineInfo)xmlReader;

                if (xmlLineInfo.LineNumber == line)
                {
                    var xml = xmlReader.ReadOuterXml();

                    var prevSpace = xml.LastIndexOf(' ', position) + 1;
                    var nextSpace = xml.IndexOf(' ', position);

                    var length = nextSpace - prevSpace;

                    var str = xml.Substring(prevSpace, length);

                    return new XmlDeserializationException(type, str, ex);
                }
            }

            return null;
        }
    }
}
