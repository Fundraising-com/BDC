using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.QSPFulfillment
{
    public class CatalogItem : EnvironmentBase
    {
        private int _catalogItemId;
        private int _catalogId;
        private int _productId;
        private string _catalogItemCode;
        private string _catalogItemName;
        private string _description;
        private int _nbUnits;
        private decimal _price;
        private string _imageUrl;
        private bool _deleted;
        private DateTime _createDate;
        private int _createUserId;
        private DateTime _updateDate;
        private int _updateUserId;


        public CatalogItem() : this(int.MinValue) { }
        public CatalogItem(int catalogItemId) : this(catalogItemId, int.MinValue) { }
        public CatalogItem(int catalogItemId, int catalogId) : this(catalogItemId, catalogId, int.MinValue) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId) : this(catalogItemId, catalogId, productId, null) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode) : this(catalogItemId, catalogId, productId, catalogItemCode, null) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName) : this(catalogItemId, catalogId, productId, catalogItemCode, catalogItemName, null) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName, string description) : this(catalogItemId, catalogId, productId, catalogItemCode, catalogItemName, description, int.MinValue) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName, string description, int nbUnits) : this(catalogItemId, catalogId, productId, catalogItemCode, catalogItemName, description, nbUnits, decimal.MinValue) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName, string description, int nbUnits, decimal price) : this(catalogItemId, catalogId, productId, catalogItemCode, catalogItemName, description, nbUnits, price, null) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName, string description, int nbUnits, decimal price, string imageUrl) : this(catalogItemId, catalogId, productId, catalogItemCode, catalogItemName, description, nbUnits, price, imageUrl, false) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName, string description, int nbUnits, decimal price, string imageUrl, bool deleted) : this(catalogItemId, catalogId, productId, catalogItemCode, catalogItemName, description, nbUnits, price, imageUrl, deleted, DateTime.MinValue) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName, string description, int nbUnits, decimal price, string imageUrl, bool deleted, DateTime createDate) : this(catalogItemId, catalogId, productId, catalogItemCode, catalogItemName, description, nbUnits, price, imageUrl, deleted, createDate, int.MinValue) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName, string description, int nbUnits, decimal price, string imageUrl, bool deleted, DateTime createDate, int createUserId) : this(catalogItemId, catalogId, productId, catalogItemCode, catalogItemName, description, nbUnits, price, imageUrl, deleted, createDate, createUserId, DateTime.MinValue) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName, string description, int nbUnits, decimal price, string imageUrl, bool deleted, DateTime createDate, int createUserId, DateTime updateDate) : this(catalogItemId, catalogId, productId, catalogItemCode, catalogItemName, description, nbUnits, price, imageUrl, deleted, createDate, createUserId, updateDate, int.MinValue) { }
        public CatalogItem(int catalogItemId, int catalogId, int productId, string catalogItemCode, string catalogItemName, string description, int nbUnits, decimal price, string imageUrl, bool deleted, DateTime createDate, int createUserId, DateTime updateDate, int updateUserId)
        {
            _catalogItemId = catalogItemId;
            _catalogId = catalogId;
            _productId = productId;
            _catalogItemCode = catalogItemCode;
            _catalogItemName = catalogItemName;
            _description = description;
            _nbUnits = nbUnits;
            _price = price;
            _imageUrl = imageUrl;
            _deleted = deleted;
            _createDate = createDate;
            _createUserId = createUserId;
            _updateDate = updateDate;
            _updateUserId = updateUserId;
        }


        #region XML Methods

        #region Save XML
        private string IdentXML(string xml)
        {
            string newXML = "";
            string[] lines = xml.Split('\r');
            foreach (string strXml in lines)
            {
                if (strXml.Trim() == "")
                    break;
                newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
            }
            return newXML;
        }

        public virtual string GenerateXML()
        {
            return "<CatalogItem>\r\n" +
            "	<CatalogItemId>" + _catalogItemId + "</CatalogItemId>\r\n" +
            "	<CatalogId>" + _catalogId + "</CatalogId>\r\n" +
            "	<ProductId>" + _productId + "</ProductId>\r\n" +
            "	<CatalogItemCode>" + _catalogItemCode + "</CatalogItemCode>\r\n" +
            "	<CatalogItemName>" + _catalogItemName + "</CatalogItemName>\r\n" +
            "	<Description>" + _description + "</Description>\r\n" +
            "	<NbUnits>" + _nbUnits + "</NbUnits>\r\n" +
            "	<Price>" + _price + "</Price>\r\n" +
            "	<ImageUrl>" + _imageUrl + "</ImageUrl>\r\n" +
            "	<Deleted>" + _deleted + "</Deleted>\r\n" +
            "	<CreateDate>" + _createDate + "</CreateDate>\r\n" +
            "	<CreateUserId>" + _createUserId + "</CreateUserId>\r\n" +
            "	<UpdateDate>" + _updateDate + "</UpdateDate>\r\n" +
            "	<UpdateUserId>" + _updateUserId + "</UpdateUserId>\r\n" +
            "</CatalogItem>\r\n";
        }
        #endregion

        #region Set Xml Values
        private void SetXmlValue(ref int obj, string val)
        {
            if (val == "") { obj = int.MinValue; return; }
            obj = int.Parse(val);
        }

        private void SetXmlValue(ref string obj, string val)
        {
            if (val == "") { obj = null; return; }
            obj = val;
        }

        private void SetXmlValue(ref bool obj, string val)
        {
            if (val == "") { obj = false; return; }
            obj = (val.ToLower() == "true");
        }

        private void SetXmlValue(ref Decimal obj, string val)
        {
            if (val == "") { obj = Decimal.MinValue; return; }
            obj = Decimal.Parse(val);
        }

        private void SetXmlValue(ref DateTime obj, string val)
        {
            if (val == "") { obj = DateTime.MinValue; return; }
            obj = DateTime.Parse(val);
        }

        #endregion

        #region Load Methods
        // loop through the xml and set the properties and child classes
        public virtual void Load(XmlNode childNodes)
        {
            foreach (XmlNode node in childNodes)
            {
                if (node.Name.ToLower() == "catalogitemid")
                {
                    SetXmlValue(ref _catalogItemId, node.InnerText);
                }
                if (node.Name.ToLower() == "catalogid")
                {
                    SetXmlValue(ref _catalogId, node.InnerText);
                }
                if (node.Name.ToLower() == "productid")
                {
                    SetXmlValue(ref _productId, node.InnerText);
                }
                if (node.Name.ToLower() == "catalogitemcode")
                {
                    SetXmlValue(ref _catalogItemCode, node.InnerText);
                }
                if (node.Name.ToLower() == "catalogitemname")
                {
                    SetXmlValue(ref _catalogItemName, node.InnerText);
                }
                if (node.Name.ToLower() == "description")
                {
                    SetXmlValue(ref _description, node.InnerText);
                }
                if (node.Name.ToLower() == "nbunits")
                {
                    SetXmlValue(ref _nbUnits, node.InnerText);
                }
                if (node.Name.ToLower() == "price")
                {
                    SetXmlValue(ref _price, node.InnerText);
                }
                if (node.Name.ToLower() == "imageurl")
                {
                    SetXmlValue(ref _imageUrl, node.InnerText);
                }
                if (node.Name.ToLower() == "deleted")
                {
                    SetXmlValue(ref _deleted, node.InnerText);
                }
                if (node.Name.ToLower() == "createdate")
                {
                    SetXmlValue(ref _createDate, node.InnerText);
                }
                if (node.Name.ToLower() == "createuserid")
                {
                    SetXmlValue(ref _createUserId, node.InnerText);
                }
                if (node.Name.ToLower() == "updatedate")
                {
                    SetXmlValue(ref _updateDate, node.InnerText);
                }
                if (node.Name.ToLower() == "updateuserid")
                {
                    SetXmlValue(ref _updateUserId, node.InnerText);
                }
            }
        }
        // load from an xml string 
        public virtual void LoadXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from an xml document object
        public virtual void Load(System.Xml.XmlDocument doc)
        {
            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from a stream
        public virtual void Load(System.IO.Stream inStream)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(inStream);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from a text reader
        public virtual void Load(System.IO.TextReader txtReader)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(txtReader);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from an xml reader
        public virtual void Load(System.Xml.XmlReader reader)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from a xml filename
        public virtual void Load(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        #endregion

        #endregion

        #region Data Access Methods

        public static CatalogItem GetCatalogItemByID(int catalogItemID)
        {
            DataAccess.QSPReplication dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.QSPReplication();
            return dbo.GetCatalogItemByID(catalogItemID);
        }

        public static CatalogItem GetCatalogItemByCatalogIDandCode(int catalogID, string CatalogItemCode)
        {
            DataAccess.QSPReplication dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.QSPReplication();
            return dbo.GetCatalogItemByCatalogIDandCode(catalogID, CatalogItemCode);
        }

        public static CatalogItem GetCatalogItemByOrderDetailID(int orderDetailID)
        {
            DataAccess.QSPReplication dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.QSPReplication();
            return dbo.GetCatalogItemByOrderDetailID(orderDetailID);
        }

        public static CatalogItem GetLatestCatalogItemByCatalogItemID(int catalogItemID, int catalogID)
        {
            DataAccess.QSPReplication dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.QSPReplication();
            return dbo.GetLatestCatalogItemByCatalogItemID(catalogItemID, catalogID);
        }
        #endregion

        #region Properties
        public int CatalogItemId
        {
            set { _catalogItemId = value; }
            get { return _catalogItemId; }
        }

        public int CatalogId
        {
            set { _catalogId = value; }
            get { return _catalogId; }
        }

        public int ProductId
        {
            set { _productId = value; }
            get { return _productId; }
        }

        public string CatalogItemCode
        {
            set { _catalogItemCode = value; }
            get { return _catalogItemCode; }
        }

        public string CatalogItemName
        {
            set { _catalogItemName = value; }
            get { return _catalogItemName; }
        }

        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        public int NbUnits
        {
            set { _nbUnits = value; }
            get { return _nbUnits; }
        }

        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }

        public string ImageUrl
        {
            set { _imageUrl = value; }
            get { return _imageUrl; }
        }

        public bool Deleted
        {
            set { _deleted = value; }
            get { return _deleted; }
        }

        public DateTime CreateDate
        {
            set { _createDate = value; }
            get { return _createDate; }
        }

        public int CreateUserId
        {
            set { _createUserId = value; }
            get { return _createUserId; }
        }

        public DateTime UpdateDate
        {
            set { _updateDate = value; }
            get { return _updateDate; }
        }

        public int UpdateUserId
        {
            set { _updateUserId = value; }
            get { return _updateUserId; }
        }

        #endregion

    }
}
