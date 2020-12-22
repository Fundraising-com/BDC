using System;
using System.Collections;
using System.Reflection;

namespace QSP.Collections
{
    [Serializable]
    public class ObjectComparer : IComparer
    {
        #region methods
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to or greater than the other.
        /// </summary>
        /// <param name="x">First object to compare.</param>
        /// <param name="y">Second object to compare.</param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            //Get types of the objects
            Type typex = x.GetType();
            Type typey = y.GetType();

            for (int i = 0; i < Fields.Length; i++)
            {
                //Get each property by name
                PropertyInfo pix = typex.GetProperty(Fields[i]);
                PropertyInfo piy = typey.GetProperty(Fields[i]);

                //Get the value of the property for each object
                IComparable pvalx = (IComparable)pix.GetValue(x, null);
                object pvaly = piy.GetValue(y, null);

                //Compare values, using IComparable interface of the property's type
                int iResult = pvalx.CompareTo(pvaly);
                if (iResult != 0)
                {
                    //Return if not equal
                    if (Descending[i])
                    {
                        //Invert order
                        return -iResult;
                    }
                    else
                    {
                        return iResult;
                    }
                }
            }
            //Objects have the same sort order
            return 0;
        }
        #endregion
        #region constructors
        /// <summary>
        /// Create a comparer for objects of arbitrary types having using the specified properties
        /// </summary>
        /// <param name="fields">Properties to sort objects by</param>
        public ObjectComparer(params string[] fields)
            : this(fields, new bool[fields.Length]) { }

        /// <summary>
        /// Create a comparer for objects of arbitrary types having using the specified properties and sort order
        /// </summary>
        /// <param name="fields">Properties to sort objects by</param>
        /// <param name="descending">Properties to sort in descending order</param>
        public ObjectComparer(string[] fields, bool[] descending)
        {
            Fields = fields;
            Descending = descending;
        }

        #endregion
        #region protected fields
        /// <summary>
        /// Properties to sort objects by
        /// </summary>
        protected string[] Fields;

        /// <summary>
        /// Properties to sort in descending order
        /// </summary>
        protected bool[] Descending;
        #endregion
    }
}
