using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class KitType: EFundraisingCRMDataObject {

        private int kitTypeID;
        private string description;
        private DateTime deliveryTime;
        private string comments;
        private int isDefault;


        public KitType() : this(int.MinValue) { }
        public KitType(int kitTypeID) : this(kitTypeID, null) { }
        public KitType(int kitTypeID, string description) : this(kitTypeID, description, DateTime.MinValue) { }
        public KitType(int kitTypeID, string description, DateTime deliveryTime) : this(kitTypeID, description, deliveryTime, null) { }
        public KitType(int kitTypeID, string description, DateTime deliveryTime, string comments) : this(kitTypeID, description, deliveryTime, comments, int.MinValue) { }
        public KitType(int kitTypeID, string description, DateTime deliveryTime, string comments, int isDefault)
        {
            this.kitTypeID = kitTypeID;
            this.description = description;
            this.deliveryTime = deliveryTime;
            this.comments = comments;
            this.isDefault = isDefault;
        }

        #region Method

        #endregion

        #region Static Data

        public static KitType General
        {
            get { return new KitType(0, "General", DateTime.MinValue, null, 0); }
        }
        public static KitType FirstQualityScratchcard
        {
            get { return new KitType(1, "1st Quality Scratchcard", DateTime.MinValue, "A", 0); }
        }
        public static KitType NSG
        {
            get { return new KitType(2, "NSG", DateTime.MinValue, "B", 0); }
        }
        public static KitType FirstQualitySuperstore
        {
            get { return new KitType(3, "1st Quality Superstore", DateTime.MinValue, "C", 0); }
        }
        public static KitType SecondQualitySuperstore
        {
            get { return new KitType(4, "2nd Quality Superstore", DateTime.MinValue, "D", 1); }
        }
        public static KitType Online
        {
            get { return new KitType(5, "OnLine", DateTime.MinValue, "E", 0); }
        }
        public static KitType PersonalizedSuperstore
        {
            get { return new KitType(6, "Personalized Superstore", DateTime.MinValue, "F", 0); }
        }
        public static KitType Agent
        {
            get { return new KitType(7, "Agent", DateTime.MinValue, "G", 0); }
        }
        public static KitType CheapScratchcard
        {
            get { return new KitType(8, "Cheap Scratchcard", DateTime.MinValue, "H", 0); }
        }
        public static KitType CheapSuperstore
        {
            get { return new KitType(9, "Cheap Superstore", DateTime.MinValue, "I", 0); }
        }
        public static KitType NoKitToSend
        {
            get { return new KitType(10, "No Kit to Send", DateTime.MinValue, "J", 0); }
        }
        public static KitType QuebecKit
        {
            get { return new KitType(11, "Quebec Kit", DateTime.MinValue, "K", 0); }
        }
        public static KitType MyTeamRegular
        {
            get { return new KitType(12, "myTeam Regular", DateTime.MinValue, "M", 0); }
        }
        public static KitType InsufficientAddress
        {
            get { return new KitType(13, "Insufficient Address", DateTime.MinValue, "L", 0); }
        }
        public static KitType MyTeamAAU
        {
            get { return new KitType(14, "myTeam A.A.U", DateTime.MinValue, "N", 0); }
        }
        public static KitType MyTeamASA
        {
            get { return new KitType(15, "myTeam A.S.A", DateTime.MinValue, "O", 0); }
        }
        public static KitType Active
        {
            get { return new KitType(16, "Active", DateTime.MinValue, "P", 0); }
        }
        public static KitType FedExPriority
        {
            get { return new KitType(17, "Fed-Ex Priority", DateTime.MinValue, "q", 0); }
        }
        public static KitType FedExActive
        {
            get { return new KitType(18, "Fed-Ex Active", DateTime.MinValue, "r", 0); }
        }
        public static KitType NSGFedex
        {
            get { return new KitType(19, "NSG Fedex", DateTime.MinValue, "s", 0); }
        }
        public static KitType ASAFedex
        {
            get { return new KitType(20, "ASA Fedex", DateTime.MinValue, "T", 0); }
        }
        public static KitType ASARegularMail
        {
            get { return new KitType(21, "ASA Regular Mail", DateTime.MinValue, "u", 0); }
        }
        public static KitType GoFundraisingKit
        {
            get { return new KitType(22, "Go!Fundraising kit", DateTime.MinValue, "v", 0); }
        }
        public static KitType TeamCheerKit
        {
            get { return new KitType(23, "Team Cheer kit", DateTime.MinValue, "w", 0); }
        }
        public static KitType StepbyStepKit
        {
            get { return new KitType(24, "Step by Step kit", DateTime.MinValue, "x", 0); }
        }
        public static KitType FRBrochure
        {
            get { return new KitType(25, "FR Brochure", DateTime.MinValue, "y", 0); }
        }
        public static KitType Catalogue
        {
            get { return new KitType(26, "Catalogue", DateTime.MinValue, "z", 0); }
        }
        public static KitType FRHealthyKit
        {
            get { return new KitType(27, "FR Healthy Kit", DateTime.MinValue, null, 0); }
        }
        public static KitType FrUsa
        {
            get { return new KitType(28, "FR USA", DateTime.MinValue, null, 0); }
        }
        #endregion

        #region XML Methods

        #region Save XML
        public override string GenerateXML()
        {
            return "<KitType>\r\n" +
            "	<KitTypeID>" + kitTypeID + "</KitTypeID>\r\n" +
            "	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
            "	<DeliveryTime>" + deliveryTime + "</DeliveryTime>\r\n" +
            "	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
            "	<IsDefault>" + isDefault + "</IsDefault>\r\n" +
            "</KitType>\r\n";
        }
        #endregion

        #region Load Methods
        // loop through the xml and set the properties and child classes
        public override void Load(XmlNode childNodes)
        {
            foreach (XmlNode node in childNodes)
            {
                if (ToLowerCase(node.Name) == ToLowerCase("kitTypeId"))
                {
                    SetXmlValue(ref kitTypeID, node.InnerText);
                }
                if (ToLowerCase(node.Name) == ToLowerCase("description"))
                {
                    SetXmlValue(ref description, node.InnerText);
                }
                if (ToLowerCase(node.Name) == ToLowerCase("deliveryTime"))
                {
                    SetXmlValue(ref deliveryTime, node.InnerText);
                }
                if (ToLowerCase(node.Name) == ToLowerCase("comments"))
                {
                    SetXmlValue(ref comments, node.InnerText);
                }
                if (ToLowerCase(node.Name) == ToLowerCase("isDefault"))
                {
                    SetXmlValue(ref isDefault, node.InnerText);
                }
            }
        }
        #endregion

        #endregion

        #region Data Source Methods
        public static KitType[] GetKitTypes()
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetKitTypes();
        }
        public static KitType[] GetKitTypesActive()
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetKitTypesActive();
        }
        public static KitType GetKitTypeByID(int id)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetKitTypeByID(id);
        }

        //public static KitType GetProperKitTypeFromLeadInformation(int consultantId, string channelCode, int promotionId, int partnerId, string stateCode, string countryCode) {
        //    DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
        //    return dbo.GetProperKitTypeFromLeadInformation(consultantId, channelCode, promotionId, partnerId, stateCode, countryCode);
        //}

        public static KitType GetProperKitTypeFromLeadInformation(int consultantId, string channelCode, int promotionId, int partnerId, string stateCode, string countryCode)
        {
            return GetProperKitTypeFromLeadInformation(-1, consultantId, channelCode, promotionId, partnerId, stateCode, countryCode);
        }

        public static KitType GetProperKitTypeFromLeadInformation(int kittype, int consultantId, string channelCode, int promotionId, int partnerId, string stateCode, string countryCode)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetProperKitTypeFromLeadInformation(kittype, consultantId, channelCode, promotionId, partnerId, stateCode, countryCode);
        }

        public int Insert()
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.InsertKitType(this);
        }

        public int Update()
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.UpdateKitType(this);
        }
        #endregion

        #region Properties
        public int KitTypeID
        {
            set { kitTypeID = value; }
            get { return kitTypeID; }
        }

        public string Description
        {
            set { description = value; }
            get { return description; }
        }

        public DateTime DeliveryTime
        {
            set { deliveryTime = value; }
            get { return deliveryTime; }
        }

        public string Comments
        {
            set { comments = value; }
            get { return comments; }
        }

        public int IsDefault
        {
            set { isDefault = value; }
            get { return isDefault; }
        }

        #endregion

	}
}
