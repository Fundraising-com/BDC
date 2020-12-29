using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
    [Serializable]
    public class Tag
    {
        private int _tagID;
        private string _startTagName;
        private string _endTagName;
        private string _description;

        public Tag() : this(int.MinValue) { }
        public Tag(int tagID) : this(tagID, null) { }
        public Tag(int tagID, string startTagName) : this(tagID, startTagName, null) { }
        public Tag(int tagID, string startTagName, string endTagName) : this(tagID, startTagName, endTagName, null) { }
        public Tag(int tagID, string startTagName, string endTagName, string description)
        {
            _tagID = tagID;
            _startTagName = startTagName;
            _endTagName = endTagName;
            _description = description;
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
            return "<Tag>\r\n" +
            "	<TagID>" + _tagID + "</TagID>\r\n" +
            "	<StartTagName>" + _startTagName + "</StartTagName>\r\n" +
            "	<EndTagName>" + _endTagName + "</EndTagName>\r\n" +
            "	<Description>" + _description + "</Description>\r\n" +
            "</Tag>\r\n";
        }
        #endregion

        #endregion

        #region Methods
        public static List<Tag> GetTags()
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetTags();
        }

        public static Tag GetTagByID(Int32 tag_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetTagByID(tag_id);
        }
        #endregion

        #region Properties
        public int TagID
        {
            set { _tagID = value; }
            get { return _tagID; }
        }

        public string StartTagName
        {
            set { _startTagName = value; }
            get { return _startTagName; }
        }

        public string EndTagName
        {
            set { _endTagName = value; }
            get { return _endTagName; }
        }

        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        #endregion
    }
}
