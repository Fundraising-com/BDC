using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{

    /// <summary>
    /// Class for PersonalizationImage.
    /// </summary>
    [Serializable]
    public class DmPersonalizationImage : baseWebImage
    {
        private int _imageID;
        private int _directMailInfoId;
        private string _imageUrl;
        private byte _deleted;

        private int _image_approval_status_id;
        //add by jason
        private int _personalizationID;
        private int _eventID;
        private DateTime _approvedDate;
        private DateTime _creatingDate;
        private string _approverName;
        private string _message;


        public DmPersonalizationImage() : this(int.MinValue) { }
        public DmPersonalizationImage(int imageID) : this(imageID, int.MinValue) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId) : this(imageID, directMailInfoId, null) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId, string imageUrl) : this(imageID, directMailInfoId, imageUrl, byte.MinValue) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId, string imageUrl, byte deleted) : this(imageID, directMailInfoId, imageUrl, deleted, int.MinValue) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId, string imageUrl, byte deleted, int image_approval_status_id) : this(imageID, directMailInfoId, imageUrl, deleted, image_approval_status_id, int.MinValue) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId, string imageUrl, byte deleted, int image_approval_status_id, int personalizationID) : this(imageID, directMailInfoId, imageUrl, deleted, image_approval_status_id, personalizationID, int.MinValue) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId, string imageUrl, byte deleted, int image_approval_status_id, int personalizationID, int eventID) : this(imageID, directMailInfoId, imageUrl, deleted, image_approval_status_id, personalizationID, eventID, DateTime.MinValue) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId, string imageUrl, byte deleted, int image_approval_status_id, int personalizationID, int eventID, DateTime approvedDate) : this(imageID, directMailInfoId, imageUrl, deleted, image_approval_status_id, personalizationID, eventID, approvedDate, DateTime.MinValue) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId, string imageUrl, byte deleted, int image_approval_status_id, int personalizationID, int eventID, DateTime approvedDate, DateTime creatingDate) : this(imageID, directMailInfoId, imageUrl, deleted, image_approval_status_id, personalizationID, eventID, approvedDate, creatingDate, null) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId, string imageUrl, byte deleted, int image_approval_status_id, int personalizationID, int eventID, DateTime approvedDate, DateTime creatingDate, string approverName) : this(imageID, directMailInfoId, imageUrl, deleted, image_approval_status_id, personalizationID, eventID, approvedDate, creatingDate, approverName, null) { }
        public DmPersonalizationImage(int imageID, int directMailInfoId, string imageUrl, byte deleted, int image_approval_status_id, int personalizationID, int eventID, DateTime approvedDate, DateTime creatingDate, string approverName, string message) 
        
        {
            _imageID = imageID;
            _directMailInfoId = directMailInfoId;
            _imageUrl = imageUrl;
            _deleted = deleted;
            _isCoverAlbum = 0;
            _image_approval_status_id = 1;
            _personalizationID = personalizationID;
            _approverName = approverName;
            _approvedDate = approvedDate;
            _creatingDate = creatingDate;
            _eventID = eventID;
            _message = message;

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
            "     <ImageID>" + _imageID + "</ImageID>\r\n" +
            "     <_directMailInfoId>" + _directMailInfoId + "</_directMailInfoId>\r\n" +
            "     <ImageUrl>" + _imageUrl + "</ImageUrl>\r\n" +
            "     <Deleted>" + _deleted + "</Deleted>\r\n" +
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
                if (node.Name.ToLower() == "_directMailInfoId")
                {
                    SetXmlValue(ref _directMailInfoId, node.InnerText);
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
        public static List<DmPersonalizationImage> GetPersonalizationImage(Int32 direct_mail_info_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetDmPersonalizationImages(direct_mail_info_id);
        }

        public static List<DmPersonalizationImage> GetDmPersonalizationUserImage(int image_status, string start_date, string end_date, int event_id)
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                return dbo.GetDmPersonalizationUserImage(image_status, start_date, end_date, event_id);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex);
            }
        }

        public void UpdateIntoDatabase()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.UpdateDmPersonalizationImage(this);
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
                this.ImageID = dbo.InsertDmPersonalizationImage(_directMailInfoId, _imageUrl, _deleted, ref _imageID, _isCoverAlbum, _image_approval_status_id);
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
                dbo.UpdateDmPersonalizationImage(this);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }        

        public static void UpdateDMValidatedImage(List<DmPersonalizationImage> imageIDTextHolder, int image_approval_status_id, string approver_name)
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                foreach (DmPersonalizationImage IDTextHolder in imageIDTextHolder)
                {
                    dbo.UpdateDmValidatedImage(IDTextHolder.ImageID, IDTextHolder.Message, image_approval_status_id, approver_name); 
                }
                
                
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex);
            }
        }

        #endregion


        #region Properties
        public int ImageID
        {
            set { _imageID = value; }
            get { return _imageID; }
        }

        public int DirectMailInfoId
        {
            set { _directMailInfoId = value; }
            get { return _directMailInfoId; }
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

        public DateTime CreatingDate
        {
            set { _creatingDate = value; }
            get { return _creatingDate; }
        }

        public string ImageUrl
        {
            set { _imageUrl = value; }
            get { return _imageUrl; }
        }

        public DateTime ApprovedDate
        {
            set { _approvedDate = value; }
            get { return _approvedDate; }
        }

        public int PersonalizationID
        {
            set { _personalizationID = value; }
            get { return _personalizationID; }
        }

        public int EventId
        {
            set { _eventID = value; }
            get { return _eventID; }
        }

        public string ApproverName
        {
            set { _approverName = value; }
            get { return _approverName; }
        }

        public string Message
        {
            set { this._message = value; }
            get { return _message; }
        }



        //ONLY IN USE FOR RENDER - NOT DB
        public string SupportText { get; set; }
        public string SupportUrl { get; set; }
        public string nameContext { get; set; }
        public string Target { get; set; }     

        #endregion
    }
}
