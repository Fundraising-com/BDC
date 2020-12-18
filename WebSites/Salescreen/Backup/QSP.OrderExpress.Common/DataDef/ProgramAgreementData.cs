using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of ProgramAgreementCampaignData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type ProgramAgreementCampaignData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class ProgramAgreementData : DataSet
	{
		/// <value>The constant used for Order DataSet. </value>
		public const String DTS_PROGRAM_AGREEMENT = "program_agreement";
        public const String TBL_SUPPLY = "orderSupply";

		private ProgramAgreementTable programAgreement;
		private ProgramAgreementCampaignTable programAgreementCampaign;
        private CampaignTable campaign;
        private PostalAddressEntityTable postalAddress;
		private PhoneNumberEntityTable phoneNumber;
		private EmailEntityTable emailAddress;
        private OrderGroupTable orderGroup;
        private OrderHeaderTable orderHeader;
        private ShipmentGroupTable shipmentGroup;
        private OrderDetailTable orderSupply;
		private ValidationTable programAgreementValidation;
		private EntityExceptionTable programAgreementException;
        private ProgramAgreementCatalogTable programAgreementCatalog;
        
		public ProgramAgreementData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected ProgramAgreementData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[CampaignTable.TBL_CAMPAIGNS] != null)) 
				{
					this.Tables.Add(new CampaignTable(ds.Tables[CampaignTable.TBL_CAMPAIGNS]));
				}
				if ((ds.Tables[ProgramAgreementTable.TBL_PROGRAM_AGREEMENT] != null)) 
				{
					this.Tables.Add(new ProgramAgreementTable(ds.Tables[ProgramAgreementTable.TBL_PROGRAM_AGREEMENT]));
				}
				if ((ds.Tables[ProgramAgreementCampaignTable.TBL_PROGRAM_AGREEMENT_CAMPAIGN] != null)) 
				{
					this.Tables.Add(new ProgramAgreementCampaignTable(ds.Tables[ProgramAgreementCampaignTable.TBL_PROGRAM_AGREEMENT_CAMPAIGN]));
				}			
				if ((ds.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY] != null)) 
				{
					this.Tables.Add(new PostalAddressEntityTable(ds.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY]));
				}
				if ((ds.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY] != null)) 
				{
					this.Tables.Add(new PhoneNumberEntityTable(ds.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY]));
				}
				if ((ds.Tables[EmailEntityTable.TBL_EMAIL_ENTITY] != null)) 
				{
					this.Tables.Add(new EmailEntityTable(ds.Tables[EmailEntityTable.TBL_EMAIL_ENTITY]));
				}
                if ((ds.Tables[OrderGroupTable.TBL_ORDER_GROUP] != null))
                {
                    this.Tables.Add(new OrderGroupTable(ds.Tables[OrderGroupTable.TBL_ORDER_GROUP]));
                }
                if ((ds.Tables[OrderHeaderTable.TBL_ORDERS] != null))
                {
                    this.Tables.Add(new OrderHeaderTable(ds.Tables[OrderHeaderTable.TBL_ORDERS]));
                }
                if ((ds.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP] != null))
                {
                    this.Tables.Add(new ShipmentGroupTable(ds.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP]));
                }
                if ((ds.Tables[TBL_SUPPLY] != null))
                {
                    this.Tables.Add(new OrderDetailTable(ds.Tables[TBL_SUPPLY]));
                }
                if ((ds.Tables[ValidationTable.TBL_VALIDATION] != null)) 
				{
					this.Tables.Add(new ValidationTable(ds.Tables[ValidationTable.TBL_VALIDATION]));
				}
				if ((ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION] != null)) 
				{
					this.Tables.Add(new EntityExceptionTable(ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
				}
                if ((ds.Tables[ProgramAgreementCatalogTable.TBL_PROGRAM_AGREEMENT_CATALOG] != null))
                {
                    this.Tables.Add(new ProgramAgreementCatalogTable(ds.Tables[ProgramAgreementCatalogTable.TBL_PROGRAM_AGREEMENT_CATALOG]));
                }

				this.DataSetName = ds.DataSetName;
				this.Prefix = ds.Prefix;
				this.Namespace = ds.Namespace;
				this.Locale = ds.Locale;
				this.CaseSensitive = ds.CaseSensitive;
				this.EnforceConstraints = ds.EnforceConstraints;
				this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
				this.InitVars();
			}
			else 
			{
				this.InitClass();
			}
			this.GetSerializationData(info, context);
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		public CampaignTable Campaign 
		{
			get 
			{
				return this.campaign;
			}
		}

		public ProgramAgreementTable ProgramAgreement 
		{
			get 
			{
				return this.programAgreement;
			}
		}

		public ProgramAgreementCampaignTable ProgramAgreementCampaign 
		{
			get 
			{
				return this.programAgreementCampaign;
			}
		}
        
		public PostalAddressEntityTable PostalAddress 
		{
			get 
			{
				return this.postalAddress;
			}
		}

		public PhoneNumberEntityTable PhoneNumber
		{
			get 
			{
				return this.phoneNumber;
			}
		}

		public EmailEntityTable EmailAddress
		{
			get 
			{
				return this.emailAddress;
			}
		}

        public OrderGroupTable OrderGroup
        {
            get
            {
                return this.orderGroup;
            }
        }

        public OrderHeaderTable OrderHeader
        {
            get
            {
                return this.orderHeader;
            }
        }

        public ShipmentGroupTable ShipmentGroup
        {
            get
            {
                return this.shipmentGroup;
            }
        }

        public OrderDetailTable OrderSupply
        {
            get
            {
                return this.orderSupply;
            }
        }

		public ValidationTable ProgramAgreementValidation
		{
			get 
			{
				return this.programAgreementValidation;
			}
		}

		public EntityExceptionTable ProgramAgreementException
		{
			get 
			{
				return this.programAgreementException;
			}
		}
        public ProgramAgreementCatalogTable ProgramAgreementCatalog
        {
            get
            {
                return this.programAgreementCatalog;
            }
        }
        
		public override DataSet Clone() 
		{
			ProgramAgreementData cln = ((ProgramAgreementData)(base.Clone()));
			cln.InitVars();
			return cln;
		}
        
		protected override bool ShouldSerializeTables() 
		{
			return false;
		}
        
		protected override bool ShouldSerializeRelations() 
		{
			return false;
		}
        
		protected override void ReadXmlSerializable(XmlReader reader) 
		{
			this.Reset();
			DataSet ds = new DataSet();
			ds.ReadXml(reader);
			if ((ds.Tables[CampaignTable.TBL_CAMPAIGNS] != null)) 
			{
				this.Tables.Add(new CampaignTable(ds.Tables[CampaignTable.TBL_CAMPAIGNS]));
			}
			if ((ds.Tables[ProgramAgreementTable.TBL_PROGRAM_AGREEMENT] != null)) 
			{
				this.Tables.Add(new ProgramAgreementTable(ds.Tables[ProgramAgreementTable.TBL_PROGRAM_AGREEMENT]));
			}
			if ((ds.Tables[ProgramAgreementCampaignTable.TBL_PROGRAM_AGREEMENT_CAMPAIGN] != null)) 
			{
				this.Tables.Add(new ProgramAgreementCampaignTable(ds.Tables[ProgramAgreementCampaignTable.TBL_PROGRAM_AGREEMENT_CAMPAIGN]));
			}
			if ((ds.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY] != null)) 
			{
				this.Tables.Add(new PostalAddressEntityTable(ds.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY]));
			}
			if ((ds.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY] != null)) 
			{
				this.Tables.Add(new PhoneNumberEntityTable(ds.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY]));
			}
			if ((ds.Tables[EmailEntityTable.TBL_EMAIL_ENTITY] != null)) 
			{
				this.Tables.Add(new EmailEntityTable(ds.Tables[EmailEntityTable.TBL_EMAIL_ENTITY]));
			}
            if ((ds.Tables[OrderGroupTable.TBL_ORDER_GROUP] != null))
            {
                this.Tables.Add(new OrderGroupTable(ds.Tables[OrderGroupTable.TBL_ORDER_GROUP]));
            }
            if ((ds.Tables[OrderHeaderTable.TBL_ORDERS] != null))
            {
                this.Tables.Add(new OrderHeaderTable(ds.Tables[OrderHeaderTable.TBL_ORDERS]));
            }
            if ((ds.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP] != null))
            {
                this.Tables.Add(new ShipmentGroupTable(ds.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP]));
            }
            if ((ds.Tables[TBL_SUPPLY] != null))
            {
                this.Tables.Add(new OrderDetailTable(ds.Tables[TBL_SUPPLY]));
            }
			if ((ds.Tables[ValidationTable.TBL_VALIDATION] != null)) 
			{
				this.Tables.Add(new ValidationTable(ds.Tables[ValidationTable.TBL_VALIDATION]));
			}
			if ((ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION] != null)) 
			{
				this.Tables.Add(new EntityExceptionTable(ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
			}
            if ((ds.Tables[ProgramAgreementCatalogTable.TBL_PROGRAM_AGREEMENT_CATALOG] != null))
            {
                this.Tables.Add(new ProgramAgreementCatalogTable(ds.Tables[ProgramAgreementCatalogTable.TBL_PROGRAM_AGREEMENT_CATALOG]));
            }
			
			this.DataSetName = ds.DataSetName;
			this.Prefix = ds.Prefix;
			this.Namespace = ds.Namespace;
			this.Locale = ds.Locale;
			this.CaseSensitive = ds.CaseSensitive;
			this.EnforceConstraints = ds.EnforceConstraints;
			this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
			this.InitVars();
		}
        
		protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() 
		{
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			this.WriteXmlSchema(new XmlTextWriter(stream, null));
			stream.Position = 0;
			return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
		}
        
		internal void InitVars() 
		{
			this.campaign = ((CampaignTable)(this.Tables[CampaignTable.TBL_CAMPAIGNS]));
			if ((this.campaign != null)) 
			{
				this.campaign.InitVars();
			}
			this.programAgreement = ((ProgramAgreementTable)(this.Tables[ProgramAgreementTable.TBL_PROGRAM_AGREEMENT]));
			if ((this.programAgreement != null)) 
			{
				this.programAgreement.InitVars();
			}
			this.programAgreementCampaign = ((ProgramAgreementCampaignTable)(this.Tables[ProgramAgreementCampaignTable.TBL_PROGRAM_AGREEMENT_CAMPAIGN]));
			if ((this.programAgreementCampaign != null)) 
			{
				this.programAgreementCampaign.InitVars();
			}
			this.postalAddress = ((PostalAddressEntityTable)(this.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY]));
			if ((this.postalAddress != null)) 
			{
				this.postalAddress.InitVars();
			}
			this.phoneNumber = ((PhoneNumberEntityTable)(this.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY]));
			if ((this.phoneNumber != null)) 
			{
				this.phoneNumber.InitVars();
			}
			this.emailAddress = ((EmailEntityTable)(this.Tables[EmailEntityTable.TBL_EMAIL_ENTITY]));
			if ((this.emailAddress != null)) 
			{
				this.emailAddress.InitVars();
			}
            this.orderGroup = ((OrderGroupTable)(this.Tables[OrderGroupTable.TBL_ORDER_GROUP]));
            if ((this.orderGroup != null))
            {
                this.orderGroup.InitVars();
            }
            this.orderHeader = ((OrderHeaderTable)(this.Tables[OrderHeaderTable.TBL_ORDERS]));
            if ((this.orderHeader != null))
            {
                this.orderHeader.InitVars();
            }
            this.shipmentGroup = ((ShipmentGroupTable)(this.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP]));
            if ((this.shipmentGroup != null))
            {
                this.shipmentGroup.InitVars();
            }
            this.orderSupply = ((OrderDetailTable)(this.Tables[TBL_SUPPLY]));
            if ((this.orderSupply != null))
            {
                this.orderSupply.InitVars();
            }
			this.programAgreementValidation = ((ValidationTable)(this.Tables[ValidationTable.TBL_VALIDATION]));
			if ((this.programAgreementValidation != null)) 
			{
				this.programAgreementValidation.InitVars();
			}
			this.programAgreementException = ((EntityExceptionTable)(this.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
			if ((this.programAgreementException != null)) 
			{
				this.programAgreementException.InitVars();
			}
            this.programAgreementCatalog = ((ProgramAgreementCatalogTable)(this.Tables[ProgramAgreementCatalogTable.TBL_PROGRAM_AGREEMENT_CATALOG]));
            if (this.programAgreementCatalog != null)
            {
                this.programAgreementCatalog.InitVars();
            }
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_PROGRAM_AGREEMENT;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/ProgramAgreementData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Campaign
			this.campaign = new CampaignTable();
			this.Tables.Add(this.campaign);
			//ProgramAgreement
			this.programAgreement = new ProgramAgreementTable();
			this.Tables.Add(this.programAgreement);
			//ProgramAgreementCampaign
			this.programAgreementCampaign = new ProgramAgreementCampaignTable();
			this.Tables.Add(this.programAgreementCampaign);
			//Postal Address
			this.postalAddress = new PostalAddressEntityTable();
			this.Tables.Add(this.postalAddress);
			//Phone Number
			this.phoneNumber = new PhoneNumberEntityTable();
			this.Tables.Add(this.phoneNumber);
			//Email Address
			this.emailAddress = new EmailEntityTable();
			this.Tables.Add(this.emailAddress);
            //OrderGroup
            this.orderGroup = new OrderGroupTable();
            this.Tables.Add(this.orderGroup);
            //OrderHeader
            this.orderHeader = new OrderHeaderTable();
            this.Tables.Add(this.orderHeader);
            //Shipment Group
            this.shipmentGroup = new ShipmentGroupTable();
            this.Tables.Add(this.shipmentGroup);
            //Order Detail Supply
            OrderDetailTable dtSupply = new OrderDetailTable();
            dtSupply.TableName = TBL_SUPPLY;
            this.orderSupply = dtSupply;
            this.Tables.Add(this.orderSupply);
            //ProgramAgreement Validation
			this.programAgreementValidation = new ValidationTable();
			this.Tables.Add(this.programAgreementValidation);
			//ProgramAgreement Exception
			this.programAgreementException = new EntityExceptionTable();
			this.Tables.Add(this.programAgreementException);
            this.programAgreementCatalog = new ProgramAgreementCatalogTable();
            this.Tables.Add(this.programAgreementCatalog);
		}

        
//		private bool ShouldSerializecampaign() 
//		{
//			return false;
//		}
//        
//		private bool ShouldSerializepostal_address_campaign() 
//		{
//			return false;
//		}
//        
		private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) 
		{
			if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) 
			{
				this.InitVars();
			}
		}
		
		public int FormID
		{
			get
			{
				int toReturn = 0;
				if (this.ProgramAgreement.Rows.Count > 0)
				{
					DataRow paRow = this.ProgramAgreement.Rows[0];
                    if (!paRow.IsNull(ProgramAgreementTable.FLD_FORM_ID))
                        toReturn = Convert.ToInt32(paRow[ProgramAgreementTable.FLD_FORM_ID]);
						
				}
				return toReturn;
			
			}
		}

		public bool IsFormIDNull
		{
			get
			{
				bool toReturn = false;
				if (this.ProgramAgreement.Rows.Count > 0)
				{
					DataRow paRow = this.ProgramAgreement.Rows[0];
                    toReturn = (paRow.IsNull(ProgramAgreementTable.FLD_FORM_ID));
				}
				return toReturn;
			
			}
		}

	}
}
