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
	///         This class is used to define the shape of CreditApplicationData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type CreditApplicationData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CreditApplicationData : DataSet
	{
		/// <value>The constant used for CreditApplication DataSet. </value>
		public const String DTS_CREDIT_APPLICATION = "credit_applications";

		private CreditApplicationTable creditApplication;
		private CreditCardTable creditCard;
		private AccountTable account;
		private PostalAddressEntityTable postalAddress;
		private PhoneNumberEntityTable phoneNumber;
		private EmailEntityTable emailAddress;
		private DocumentEntityTable creditDocument;
		private EntityExceptionTable creditException;
		private ValidationTable creditValidation;
		
		public CreditApplicationData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected CreditApplicationData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS] != null)) 
				{
					this.Tables.Add(new CreditApplicationTable(ds.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS]));
				}				
				if ((ds.Tables[CreditCardTable.TBL_CREDIT_CARDS] != null)) 
				{
					this.Tables.Add(new CreditCardTable(ds.Tables[CreditCardTable.TBL_CREDIT_CARDS]));
				}
				if ((ds.Tables[AccountTable.TBL_ACCOUNT] != null)) 
				{
					this.Tables.Add(new AccountTable(ds.Tables[AccountTable.TBL_ACCOUNT]));
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
				if ((ds.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY] != null)) 
				{
					this.Tables.Add(new DocumentEntityTable(ds.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY]));
				}
				if ((ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION] != null)) 
				{
					this.Tables.Add(new EntityExceptionTable(ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
				}
				if ((ds.Tables[ValidationTable.TBL_VALIDATION] != null)) 
				{
					this.Tables.Add(new ValidationTable(ds.Tables[ValidationTable.TBL_VALIDATION]));
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
        
		public CreditApplicationTable CreditApplication 
		{
			get 
			{
				return this.creditApplication;
			}
		}

		public CreditCardTable CreditCard 
		{
			get 
			{
				return this.creditCard;
			}
		}

		public AccountTable Account 
		{
			get 
			{
				return this.account;
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

		public DocumentEntityTable CreditDocument
		{
			get 
			{
				return this.creditDocument;
			}
		}

		public EntityExceptionTable CreditException
		{
			get 
			{
				return this.creditException;
			}
		}

		public ValidationTable CreditValidation
		{
			get 
			{
				return this.creditValidation;
			}
		}
        
		public override DataSet Clone() 
		{
			CreditApplicationData cln = ((CreditApplicationData)(base.Clone()));
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
			if ((ds.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS] != null)) 
			{
				this.Tables.Add(new CreditApplicationTable(ds.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS]));
			}
			if ((ds.Tables[CreditCardTable.TBL_CREDIT_CARDS] != null)) 
			{
				this.Tables.Add(new CreditCardTable(ds.Tables[CreditCardTable.TBL_CREDIT_CARDS]));
			}
			if ((ds.Tables[AccountTable.TBL_ACCOUNT] != null)) 
			{
				this.Tables.Add(new AccountTable(ds.Tables[AccountTable.TBL_ACCOUNT]));
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
			if ((ds.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY] != null)) 
			{
				this.Tables.Add(new DocumentEntityTable(ds.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY]));
			}
			if ((ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION] != null)) 
			{
				this.Tables.Add(new EntityExceptionTable(ds.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
			}
			if ((ds.Tables[ValidationTable.TBL_VALIDATION] != null)) 
			{
				this.Tables.Add(new ValidationTable(ds.Tables[ValidationTable.TBL_VALIDATION]));
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
			this.creditApplication = ((CreditApplicationTable)(this.Tables[CreditApplicationTable.TBL_CREDIT_APPLICATIONS]));
			if ((this.creditApplication != null)) 
			{
				this.creditApplication.InitVars();
			}
			this.creditCard = ((CreditCardTable)(this.Tables[CreditCardTable.TBL_CREDIT_CARDS]));
			if ((this.creditCard != null)) 
			{
				this.creditCard.InitVars();
			}
			this.account = ((AccountTable)(this.Tables[AccountTable.TBL_ACCOUNT]));
			if ((this.account != null)) 
			{
				this.account.InitVars();
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
			this.creditDocument = ((DocumentEntityTable)(this.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY]));
			if ((this.creditDocument != null)) 
			{
				this.creditDocument.InitVars();
			}
			this.creditException = ((EntityExceptionTable)(this.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
			if ((this.creditException != null)) 
			{
				this.creditException.InitVars();
			}
			this.creditValidation = ((ValidationTable)(this.Tables[ValidationTable.TBL_VALIDATION]));
			if ((this.creditValidation != null)) 
			{
				this.creditValidation.InitVars();
			}
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_CREDIT_APPLICATION;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/CreditApplicationData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Credit Application
			this.creditApplication = new CreditApplicationTable();
			this.Tables.Add(this.creditApplication);
			//Credit Card
			this.creditCard = new CreditCardTable();
			this.Tables.Add(this.creditCard);
			//Account
			this.account = new AccountTable();
			this.Tables.Add(this.account);
			//Postal Address
			this.postalAddress = new PostalAddressEntityTable();
			this.Tables.Add(this.postalAddress);
			//Phone Number
			this.phoneNumber = new PhoneNumberEntityTable();
			this.Tables.Add(this.phoneNumber);
			//Email Address
			this.emailAddress = new EmailEntityTable();
			this.Tables.Add(this.emailAddress);
			//Credit Document
			this.creditDocument = new DocumentEntityTable();
			this.Tables.Add(this.creditDocument);
			//Credit Exception
			this.creditException = new EntityExceptionTable();
			this.Tables.Add(this.creditException);
			//Credit Validation
			this.creditValidation = new ValidationTable();
			this.Tables.Add(this.creditValidation);
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
	}
}
