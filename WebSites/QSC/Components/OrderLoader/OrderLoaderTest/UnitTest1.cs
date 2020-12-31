using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Data;

namespace OrderLoaderTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            System.DateTime d = new System.DateTime(2012,10, 24);
            
            var _c = new QSPCanadaOrderManagementEntities();
            var b = from c in _c.Batches where c.Date == d && c.ID==3671  select c;

            foreach (var x in b)
            {
                Debug.WriteLine("xxx");
            }
        }
    }
}
