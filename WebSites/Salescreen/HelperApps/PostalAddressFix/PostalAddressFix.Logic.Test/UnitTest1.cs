using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PostalAddressFix.Logic.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        #region USA zip code tests

        [TestMethod]
        public void Test_USA_()
        {
            string sourceText = "";
            string expectedResult = "";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_1()
        {
            string sourceText = "1";
            string expectedResult = "";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_12()
        {
            string sourceText = "12";
            string expectedResult = "";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_123()
        {
            string sourceText = "123";
            string expectedResult = "";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_1234()
        {
            string sourceText = "1234";
            string expectedResult = "";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_12345()
        {
            string sourceText = "12345";
            string expectedResult = "12345";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_1234_dash_5()
        {
            string sourceText = "1234-5";
            string expectedResult = "";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_123451_dash()
        {
            string sourceText = "123451-";
            string expectedResult = "12345";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_12345_dash()
        {
            string sourceText = "12345-";
            string expectedResult = "12345";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_12345_dash_1()
        {
            string sourceText = "12345-1";
            string expectedResult = "12345";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_12345_dash_12()
        {
            string sourceText = "12345-12";
            string expectedResult = "12345";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_12345_dash_123()
        {
            string sourceText = "12345-123";
            string expectedResult = "12345";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_12345_dash_1234()
        {
            string sourceText = "12345-1234";
            string expectedResult = "12345-1234";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_12345_dash_12345()
        {
            string sourceText = "12345-12345";
            string expectedResult = "12345-1234";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_12345_dash_123456()
        {
            string sourceText = "12345-123456";
            string expectedResult = "12345-1234";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_USA_Exclamation23Oo_dash_L234()
        {
            string sourceText = "I23Oo-L234";
            string expectedResult = "12300-1234";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixUSAZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        #endregion

        #region Canada zip code tests

        [TestMethod]
        public void Test_Canada_H1H1H()
        {
            string sourceText = "H1H1H";
            string expectedResult = "";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixCanadaZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Canada_H1H1H1()
        {
            string sourceText = "H1H1H1";
            string expectedResult = "H1H 1H1";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixCanadaZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Canada_H1H1H1H()
        {
            string sourceText = "H1H1H1H";
            string expectedResult = "H1H 1H1";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixCanadaZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Canada_H1H_1H1()
        {
            string sourceText = "H1H 1H1";
            string expectedResult = "H1H 1H1";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixCanadaZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Canada_H1H_dash_1H1()
        {
            string sourceText = "H1H-1H1";
            string expectedResult = "H1H 1H1";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixCanadaZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Canada_0O1LHI()
        {
            string sourceText = "0O1LHI";
            string expectedResult = "O0I 1H1";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixCanadaZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Canada_1H1H1H()
        {
            string sourceText = "1H1H1H";
            string expectedResult = "";
            string actualResult = String.Empty;

            actualResult = ZipCodeFixer.FixCanadaZip(sourceText);

            Assert.AreEqual(expectedResult, actualResult);
        }

        #endregion

    }
}
