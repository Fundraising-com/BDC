using System;
using System.Collections;
using System.Globalization;

namespace QSP.Collections
{
    /// <summary>
    /// Compare string according to Unicode ordinal number.
    /// </summary>
    public class OrdinalComparer : IComparer
    {
        #region IComparer Members
        public int Compare(object x, object y)
        {
            CompareInfo cmpInfo = CompareInfo.GetCompareInfo("en-US");
            return cmpInfo.Compare((string)x, (string)y, CompareOptions.Ordinal);
        }
        #endregion

    }
}
