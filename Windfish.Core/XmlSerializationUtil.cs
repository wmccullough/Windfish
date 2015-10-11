using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Windfish.Core
{
    public static class XmlSerializationUtil
    {
        public static bool LoadXML<T>(out T value, string path)
        {
            value = default(T);

            try {
                using (var stream = TitleContainer.OpenStream(path)) {
                    var serializer = new XmlSerializer(typeof(T));
                    value = (T)serializer.Deserialize(stream);
                }

                return true;
            }
            catch (Exception) {
                throw;
            }

            return false;
        }
    }
}
