using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM 
{
	public enum PromotionalKitComparable
	{
		PromotionalKitId,
		LeadId,
		LeadVisitId,
		KitTypeId,
		CarrierId, 
        SampleID
	}

	public enum PromotionalKitStatus
	{
		Ok,
		Error
	}
	
	public class PromotionalKit: EFundraisingCRMDataObject
	{

		private PromotionalKitComparable sortBy = PromotionalKitComparable.PromotionalKitId;	
		
		private int promotionalKitId;
		private int leadId;
		private int leadVisitId;
		private int kitTypeId;
		private int carrierId;
		private int carrierTrackingId;
		private int postalAddressId;
		private int validated;
		private DateTime createDate;
		private DateTime sentDate;
		private IsValidated valid;
        private int sampleID;
        private string consultantName;

        protected const string DefaultSample = "39";
		
		public PromotionalKit() : this(int.MinValue) { }
		public PromotionalKit(int promotionalKitId) : this(promotionalKitId, int.MinValue) { }
		public PromotionalKit(int promotionalKitId, int leadId) : this(promotionalKitId, leadId, int.MinValue) { }
		public PromotionalKit(int promotionalKitId, int leadId, int leadVisitId) : this(promotionalKitId, leadId, leadVisitId, int.MinValue) { }
		public PromotionalKit(int promotionalKitId, int leadId, int leadVisitId, int kitTypeId) : this(promotionalKitId, leadId, leadVisitId, kitTypeId, int.MinValue) { }
		public PromotionalKit(int promotionalKitId, int leadId, int leadVisitId, int kitTypeId, int carrierId) : this(promotionalKitId, leadId, leadVisitId, kitTypeId, carrierId, int.MinValue) { }
		public PromotionalKit(int promotionalKitId, int leadId, int leadVisitId, int kitTypeId, int carrierId, int carrierTrackingId) : this(promotionalKitId, leadId, leadVisitId, kitTypeId, carrierId, carrierTrackingId, int.MinValue) { }
		public PromotionalKit(int promotionalKitId, int leadId, int leadVisitId, int kitTypeId, int carrierId, int carrierTrackingId, int postalAddressId) : this(promotionalKitId, leadId, leadVisitId, kitTypeId, carrierId, carrierTrackingId, postalAddressId, int.MinValue) { }
		public PromotionalKit(int promotionalKitId, int leadId, int leadVisitId, int kitTypeId, int carrierId, int carrierTrackingId, int postalAddressId, int validated) : this(promotionalKitId, leadId, leadVisitId, kitTypeId, carrierId, carrierTrackingId, postalAddressId, validated, DateTime.MinValue) { }
		public PromotionalKit(int promotionalKitId, int leadId, int leadVisitId, int kitTypeId, int carrierId, int carrierTrackingId, int postalAddressId, int validated, DateTime createDate) : this(promotionalKitId, leadId, leadVisitId, kitTypeId, carrierId, carrierTrackingId, postalAddressId, validated, createDate, DateTime.MinValue) { }
		public PromotionalKit(int promotionalKitId, int leadId, int leadVisitId, int kitTypeId, int carrierId, int carrierTrackingId, int postalAddressId, int validated, DateTime createDate, DateTime sentDate):  this(promotionalKitId, leadId, leadVisitId, kitTypeId, carrierId, carrierTrackingId, postalAddressId, validated, createDate, sentDate, int.MinValue) {}
        public PromotionalKit(int promotionalKitId, int leadId, int leadVisitId, int kitTypeId, int carrierId, int carrierTrackingId, int postalAddressId, int validated, DateTime createDate, DateTime sentDate, int sampleID)
        {
			this.promotionalKitId = promotionalKitId;
			this.leadId = leadId;
			this.leadVisitId = leadVisitId;
			this.kitTypeId = kitTypeId;
			this.carrierId = carrierId;
			this.carrierTrackingId = carrierTrackingId;
			this.postalAddressId = postalAddressId;
			this.validated = validated;
			this.createDate = createDate;
			this.sentDate = sentDate;
			this.valid = new IsValidated(validated);
            this.sampleID = sampleID;
		
			
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() 
		{
			return "<PromotionalKit>\r\n" +
				"	<PromotionalKitId>" + promotionalKitId + "</PromotionalKitId>\r\n" +
				"	<LeadId>" + leadId + "</LeadId>\r\n" +
				"	<LeadVisitId>" + leadVisitId + "</LeadVisitId>\r\n" +
				"	<KitTypeId>" + kitTypeId + "</KitTypeId>\r\n" +
				"	<CarrierId>" + carrierId + "</CarrierId>\r\n" +
				"	<CarrierTrackingId>" + carrierTrackingId + "</CarrierTrackingId>\r\n" +
				"	<PostalAddressId>" + postalAddressId + "</PostalAddressId>\r\n" +
				"	<Validated>" + validated + "</Validated>\r\n" +
				"	<Valid>" + valid.Valid + "</Validated>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"	<SentDate>" + sentDate + "</SentDate>\r\n" +
				"</PromotionalKit>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) 
		{
			foreach(XmlNode node in childNodes) 
			{
				if(node.Name.ToLower() == "promotionalKitId") 
				{
					SetXmlValue(ref promotionalKitId, node.InnerText);
				}
				if(node.Name.ToLower() == "leadId") 
				{
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(node.Name.ToLower() == "leadVisitId") 
				{
					SetXmlValue(ref leadVisitId, node.InnerText);
				}
				if(node.Name.ToLower() == "kitTypeId") 
				{
					SetXmlValue(ref kitTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "carrierId") 
				{
					SetXmlValue(ref carrierId, node.InnerText);
				}
				if(node.Name.ToLower() == "carrierTrackingId") 
				{
					SetXmlValue(ref carrierTrackingId, node.InnerText);
				}
				if(node.Name.ToLower() == "postalAddressId") 
				{
					SetXmlValue(ref postalAddressId, node.InnerText);
				}
				if(node.Name.ToLower() == "validated") 
				{
					SetXmlValue(ref validated, node.InnerText);
				}
				if(node.Name.ToLower() == "valid") 
				{
					SetXmlValue(ref validated, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") 
				{
					SetXmlValue(ref createDate, node.InnerText);
				}
				if(node.Name.ToLower() == "sentDate") 
				{
					SetXmlValue(ref sentDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PromotionalKit[] GetPromotionalKits() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionalKits();
		}

		public static PromotionalKitCollection GetPromotionalKitsByLeadID(int leadID)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionalKitByLeadID(leadID);
		}

		public static PromotionalKit GetPromotionalKitByID(int id) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionalKitByID(id);
		}
	
		public static PromotionalKitCollection GetPromotionalKitsByCarrierId(int carrierId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionalKitsByCarrierId(carrierId);
		}

		public static PromotionalKitCollection GetPromotionalKitsToProcess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionalKitsToProcess();
		}

		public static PromotionalKitCollection GetPromotionalKitsToProcessByKitTypeID(int kitTypeID) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionalKitsToProcessByKitTypeID(kitTypeID);
		}

		public static PromotionalKit[] GetPromotionalKitsReadyForFedex()
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPromotionalKitsReadyForFedex();
		}

		public PromotionalKitStatus Insert() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.InsertPromotionalKit(this);
			switch(returnValue) 
			{
				case 1:
					return PromotionalKitStatus.Ok;
			}
			return PromotionalKitStatus.Error;
		}

		public PromotionalKitStatus Update() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.UpdatePromotionalKit(this);
			switch(returnValue) 
			{
				case 1:
					return PromotionalKitStatus.Ok;
			}
			return PromotionalKitStatus.Error;
		}
		#endregion

		#region Properties
		public int PromotionalKitId 
		{
			set { promotionalKitId = value; }
			get { return promotionalKitId; }
		}

		public int LeadId 
		{
			set { leadId = value; }
			get { return leadId; }
		}

		public int LeadVisitId 
		{
			set { leadVisitId = value; }
			get { return leadVisitId; }
		}

		public int KitTypeId 
		{
			set { kitTypeId = value; }
			get { return kitTypeId; }
		}

		public int CarrierId 
		{
			set { carrierId = value; }
			get { return carrierId; }
		}

		public int CarrierTrackingId 
		{
			set { carrierTrackingId = value; }
			get { return carrierTrackingId; }
		}

		public int PostalAddressId 
		{
			set { postalAddressId = value; }
			get { return postalAddressId; }
		}

		public int Validated 
		{
			set { validated = value; }
			get { return validated; }
		}
		public IsValidated Valid
		{
			get { return valid;}
			set {valid = value;}
		}
		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		public DateTime SentDate {
			set { sentDate = value; }
			get { return sentDate; }
		}

        public string CreateDateShort
        {
            get { return createDate.ToShortDateString(); }

        }
		public PromotionalKitComparable SortBy 
		{
			set { sortBy = value; }
			get { return sortBy; }
		}
        public int SampleID
        {
            get { return sampleID; }
            set { sampleID = value; }
        }

        public string SampleIDString
        {
            get 
            {
                if (sampleID != int.MinValue)
                {
                    return sampleID.ToString();
                }
                else
                {
                    return DefaultSample;
                }
            }
        }

        public string ConsultantName
        {
            get { return consultantName;}
            set {consultantName = value ;}
        }
		#endregion

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			// check if the two objects are the same type (same as if(obj is Sale))
			if(!CheckObjectIntegrity(obj, typeof(PromotionalKit))) 
			{
				throw new EFundraisingCRMException("CompareTo(): Object is not PromotionalKit Object");
			}

			// get the object to compare with
			PromotionalKit o = (PromotionalKit)obj;
			
			// Compare the two object depending of their sort by argument
			switch(sortBy) 
			{
				case PromotionalKitComparable.PromotionalKitId:
					return promotionalKitId.CompareTo(o.PromotionalKitId);
				case PromotionalKitComparable.LeadId:
					return leadId.CompareTo(o.LeadId);
				case PromotionalKitComparable.LeadVisitId:
					return leadVisitId.CompareTo(o.LeadVisitId);
				case PromotionalKitComparable.KitTypeId:
					return kitTypeId.CompareTo(o.KitTypeId);
				case PromotionalKitComparable.CarrierId:
					return carrierId.CompareTo(o.CarrierId);
                case PromotionalKitComparable.SampleID:
                    return sampleID.CompareTo(o.SampleID);
				default:
					// compare argument not found, throw exception
					throw new EFundraisingCRMException("PromotionalKit.CompareTo invalid comparer option");
			}
		}

		#endregion
	}
}
