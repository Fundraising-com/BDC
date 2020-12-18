using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace QSP
{
    public class Prototype
    {
        /// <summary>
        /// Creates a clone of the object.
        /// </summary>
        /// <returns>A new object containing the exact data of the original object.</returns>
        public static object Clone(object master)
        {
            MemoryStream buffer = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(buffer, master);
            buffer.Position = 0;
            return formatter.Deserialize(buffer);
        }

        public static T Clone<T>(T master)
        {
            MemoryStream buffer = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(buffer, master);
            buffer.Position = 0;
            return (T)formatter.Deserialize(buffer);
        }
    }
}
