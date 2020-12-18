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
	///         This class is used to define the shape of CampaignData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type CampaignData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class AccountData : DataSet
	{
		/// <value>The constant used for Order DataSet. </value>
		public const String DTS_ACCOUNT = "accounts";

		private OrganizationTable organization;
		private AccountTable account;
		private AccountXTable accountX;
		private CampaignTable campaign;
		private PostalAddressEntityTable postalAddress;
		private PhoneNumberEntityTable phoneNumber;
		private EmailEntityTable emailAddress;
		private CreditApplicationTable creditApplication;
		private DocumentEntityTable accountDocument;
		private ValidationTable accountValidation;
		private EntityExceptionTable accountException;		
        
		public AccountData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected AccountData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[OrganizationTable.TBL_ORGANIZATION] != null)) 
				{
					this.Tables.Add(new OrganizationTable(ds.Tables[OrganizationTable.TBL_ORGANIZATION]));
				}
				if ((ds.Tables[AccountTable.TBL_ACCOUNT] != null)) 
				{
					this.Tables.Add(new AccountTable(ds.Tables[AccountTable.TBL_ACCOUNT]));
				}
				if ((ds.Tables[AccountXTable.TBL_ACCOUNTX] != null)) 
				{
					this.Tables.Add(new AccountXTable(ds.Tables[AccountXTable.TBL_ACCOUNTX]));
				}
				if ((ds.Tables[CampaignTable.TBL_CAMPAIGNS] != null)) 
				{
					this.Tables.Add(new CampaignTable(ds.Tables[CampaignTable.TBL_CAMPAIGNS]));
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
				if ((ds.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS] != null)) 
				{
					this.Tables.Add(new CreditApplicationTable(ds.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS]));
				}
				if ((ds.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY] != null)) 
				{
					this.Tables.Add(new DocumentEntityTable(ds.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY]));
				}
				if ((ds.Tables[ValidationTable.TBL_VALIDATION] != null)) 
				{
					this.Tables.Add(new ValidationTable(ds.Tables[ValidationTable.TBL_VALIDATION]));
				}
				if ((ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION] != null)) 
				{
					this.Tables.Add(new EntityExceptionTable(ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
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
        
		public OrganizationTable Organization 
		{
			get 
			{
				return this.organization;
			}
		}

		public AccountTable Account 
		{
			get 
			{
				return this.account;
			}
		}

		public AccountXTable AccountX 
		{
			get 
			{
				return this.accountX;
			}
		}

		public CampaignTable Campaign 
		{
			get 
			{
				return this.campaign;
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

		public CreditApplicationTable CreditApplication
		{
			get 
			{
				return this.creditApplication;
			}
		}

		public DocumentEntityTable AccountDocument
		{
			get 
			{
				return this.accountDocument;
			}
		}

		public ValidationTable AccountValidation
		{
			get 
			{
				return this.accountValidation;
			}
		}

		public EntityExceptionTable AccountException
		{
			get 
			{
				return this.accountException;
			}
		}
        
		public override DataSet Clone() 
		{
			AccountData cln = ((AccountData)(base.Clone()));
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
			if ((ds.Tables[OrganizationTable.TBL_ORGANIZATION] != null)) 
			{
				this.Tables.Add(new OrganizationTable(ds.Tables[OrganizationTable.TBL_ORGANIZATION]));
			}
			if ((ds.Tables[AccountTable.TBL_ACCOUNT] != null)) 
			{
				this.Tables.Add(new AccountTable(ds.Tables[AccountTable.TBL_ACCOUNT]));
			}
			if ((ds.Tables[AccountXTable.TBL_ACCOUNTX] != null)) 
			{
				this.Tables.Add(new AccountXTable(ds.Tables[AccountXTable.TBL_ACCOUNTX]));
			}
			if ((ds.Tables[CampaignTable.TBL_CAMPAIGNS] != null)) 
			{
				this.Tables.Add(new CampaignTable(ds.Tables[CampaignTable.TBL_CAMPAIGNS]));
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
			if ((ds.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS] != null)) 
			{
				this.Tables.Add(new CreditApplicationTable(ds.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS]));
			}
			if ((ds.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY] != null)) 
			{
				this.Tables.Add(new DocumentEntityTable(ds.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY]));
			}
			if ((ds.Tables[ValidationTable.TBL_VALIDATION] != null)) 
			{
				this.Tables.Add(new ValidationTable(ds.Tables[ValidationTable.TBL_VALIDATION]));
			}
			if ((ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION] != null)) 
			{
				this.Tables.Add(new EntityExceptionTable(ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
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
			this.organization = ((OrganizationTable)(this.Tables[OrganizationTable.TBL_ORGANIZATION]));
			if ((this.organization != null)) 
			{
				this.organization.InitVars();
			}
			this.account = ((AccountTable)(this.Tables[AccountTable.TBL_ACCOUNT]));
			if ((this.account != null)) 
			{
				this.account.InitVars();
			}
			this.accountX = ((AccountXTable)(this.Tables[AccountXTable.TBL_ACCOUNTX]));
			if ((this.accountX != null)) 
			{
				this.accountX.InitVars();
			}
			this.campaign = ((CampaignTable)(this.Tables[CampaignTable.TBL_CAMPAIGNS]));
			if ((this.campaign != null)) 
			{
				this.campaign.InitVars();
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
			this.creditApplication = ((CreditApplicationTable)(this.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS]));
			if ((this.creditApplication != null)) 
			{
				this.creditApplication.InitVars();
			}
			this.accountDocument = ((DocumentEntityTable)(this.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY]));
			if ((this.accountDocument != null)) 
			{
				this.accountDocument.InitVars();
			}
			this.accountValidation = ((ValidationTable)(this.Tables[ValidationTable.TBL_VALIDATION]));
			if ((this.accountValidation != null)) 
			{
				this.accountValidation.InitVars();
			}
			this.accountException = ((EntityExceptionTable)(this.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
			if ((this.accountException != null)) 
			{
				this.accountException.InitVars();
			}
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_ACCOUNT;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/AccountData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Organization
			this.organization = new OrganizationTable();
			this.Tables.Add(this.organization);
			//Account
			this.account = new AccountTable();
			this.Tables.Add(this.account);
			//AccountX
			this.accountX = new AccountXTable();
			this.Tables.Add(this.accountX);
			//Campaign
			this.campaign = new CampaignTable();
			this.Tables.Add(this.campaign);
			//Postal Address
			this.postalAddress = new PostalAddressEntityTable();
			this.Tables.Add(this.postalAddress);
			//Phone Number
			this.phoneNumber = new PhoneNumberEntityTable();
			this.Tables.Add(this.phoneNumber);
			//Email Address
			this.emailAddress = new EmailEntityTable();
			this.Tables.Add(this.emailAddress);
			//Credit Application 
			this.creditApplication = new CreditApplicationTable();
			this.Tables.Add(this.creditApplication);
			//Account Document
			this.accountDocument = new DocumentEntityTable();
			this.Tables.Add(this.accountDocument);
			//Account Validation
			this.accountValidation = new ValidationTable();
			this.Tables.Add(this.accountValidation);
			//Account Exception
			this.accountException = new EntityExceptionTable();
			this.Tables.Add(this.accountException);
			
		}

        
//		private bool ShouldSerializeorganization() 
//		{
//			return false;
//		}
//        
//		private bool ShouldSerializepostal_address_organization() 
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
				if (this.Campaign.Rows.Count > 0)
				{
					DataRow campRow = this.Campaign.Rows[0];
					if (!campRow.IsNull(CampaignTable.FLD_FORM_ID))
						toReturn = Convert.ToInt32(campRow[CampaignTable.FLD_FORM_ID]);
						
				}
				return toReturn;
			
			}
		}

		public bool IsFormIDNull
		{
			get
			{
				bool toReturn = false;
				if (this.Campaign.Rows.Count > 0)
				{
					DataRow campRow = this.Campaign.Rows[0];
					toReturn = (campRow.IsNull(CampaignTable.FLD_FORM_ID));
				}
				return toReturn;
			
			}
		}

	}
}
