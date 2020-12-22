using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using QSP.Xml.Serialization;

namespace QSP.Collections
{
    /// <summary>
    /// Collection base class for Business objects.
    /// </summary>
    [Serializable]
    public class BusinessCollectionBase : CollectionBase, ICloneable
    {

        #region Constructors
        public BusinessCollectionBase()
        {

        }
        #endregion

        #region Methods

        #region BusinessBase Methods

        /// <summary>
        /// Return object representation as XML string.
        /// </summary>
        /// <returns></returns>
        public virtual string ToXmlString()
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(this.GetType());
                using (StringWriter sw = new StringWriter())
                {
                    xs.SerializeFields(sw, this);
                    sw.Flush();
                    return sw.ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Sorts the elements in the CollectionBase.
        /// </summary>
        public void Sort()
        {
            InnerList.Sort();
        }

        /// <summary>
        /// Sorts the elements in the CollectionBase.
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer comparer)
        {
            InnerList.Sort(comparer);
        }

        /// <summary>
        /// Sorts the elements in the CollectionBase.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public void Sort(int index, int count, IComparer comparer)
        {
            InnerList.Sort(index, count, comparer);

        }
        #endregion

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Creates a clone of the object.
        /// </summary>
        /// <returns>A new object containing the exact data of the original object.</returns>
        public object Clone()
        {
            MemoryStream buffer = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(buffer, this);
            buffer.Position = 0;
            return formatter.Deserialize(buffer);
        }
        #endregion
    }
}
