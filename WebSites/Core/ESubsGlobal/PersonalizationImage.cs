using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
    [Serializable]
    public class baseWebImage : EnvironmentBase
    {
        protected string _imageUrl;
        protected string _highImageUrl;

        protected byte _isCoverAlbum;

        public string ImageUrl
        {
            set { _imageUrl = value; }
            get { return _imageUrl; }
        }

        public string HighImageUrl
        {
            set { _highImageUrl = value; }
            get { return _highImageUrl; }
        }

        public byte IsCoverAlbum
        {
            set { _isCoverAlbum = value; }
            get { return _isCoverAlbum; }
        }

    }

    /// <summary>
    /// Class for PersonalizationImage.
    /// </summary>
    [Serializable]
    public class PersonalizationImage : baseWebImage
    {
        private int _imageID;
        private int _personalizationID;
        
        private byte _deleted;

        private int _image_approval_status_id;


        public PersonalizationImage() : this(int.MinValue) { }
        public PersonalizationImage(int imageID) : this(imageID, int.MinValue) { }
        public PersonalizationImage(int imageID, int personalizationID) : this(imageID, personalizationID, null) { }
        public PersonalizationImage(int imageID, int personalizationID, string imageUrl) : this(imageID, personalizationID, imageUrl, byte.MinValue) { }
        public PersonalizationImage(int imageID, int personalizationID, string imageUrl, byte deleted)
        {
            _imageID = imageID;
            _personalizationID = personalizationID;
            _imageUrl = imageUrl;
            _deleted = deleted;
            _isCoverAlbum = 0;
            _image_approval_status_id = 1;
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
            return "<PersonalizationImage>\r\n" +
            "	<ImageID>" + _imageID + "</ImageID>\r\n" +
            "	<PersonalizationID>" + _personalizationID + "</PersonalizationID>\r\n" +
            "	<ImageUrl>" + _imageUrl + "</ImageUrl>\r\n" +
            "	<Deleted>" + _deleted + "</Deleted>\r\n" +
            "</PersonalizationImage>\r\n";
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

        private void SetXmlValue(ref byte obj, string val)
        {
            if (val == "") { obj = (byte)0; return; }
            obj = val.Trim() == "0" ? (byte)0 : (byte)1;
        }

        #endregion

        #region Load Methods
        // loop through the xml and set the properties and child classes
        public virtual void Load(XmlNode childNodes)
        {
            foreach (XmlNode node in childNodes)
            {
                if (node.Name.ToLower() == "imageid")
                {
                    SetXmlValue(ref _imageID, node.InnerText);
                }
                if (node.Name.ToLower() == "personalizationid")
                {
                    SetXmlValue(ref _personalizationID, node.InnerText);
                }
                if (node.Name.ToLower() == "imageurl")
                {
                    SetXmlValue(ref _imageUrl, node.InnerText);
                }
                if (node.Name.ToLower() == "deleted")
                {
                    SetXmlValue(ref _deleted, node.InnerText);
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


        #region Public/Private Functions
        public static List<PersonalizationImage> GetPersonalizationImage(Int32 personalization_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetPersonalizationImages(personalization_id);
        }

        public static PersonalizationImage GetPersonalizationCoverAlbumImage(Int32 personalization_id, Int32 event_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetPersonalizationCoverAlbumImage(personalization_id, event_id);
        }

        public void UpdateIntoDatabase()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.UpdatePersonalizationImage(this);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }

        public void InsertIntoDatabase()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.InsertPersonalizationImage(_personalizationID, _imageUrl, _highImageUrl,_deleted, ref _imageID, _isCoverAlbum, _image_approval_status_id);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }

        public void Delete()
        {
            try
            {
                _deleted = (byte)1;

                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.UpdatePersonalizationImage(this);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }
        #endregion


        #region Properties
        public int ImageID
        {
            set { _imageID = value; }
            get { return _imageID; }
        }

        public int PersonalizationID
        {
            set { _personalizationID = value; }
            get { return _personalizationID; }
        }     

        public byte Deleted
        {
            set { _deleted = value; }
            get { return _deleted; }
        }

        public int ImageApprovalStatusId
        {
            set { this._image_approval_status_id = value; }
            get { return _image_approval_status_id; }
        }


        //ONLY IN USE FOR RENDER - NOT DB
        public string SupportText { get; set; }
        public string SupportUrl { get; set; }
        public string nameContext { get; set; }
        public string Target { get; set; }
        public bool isHighRes { get; set; }

        #endregion
    }
}
