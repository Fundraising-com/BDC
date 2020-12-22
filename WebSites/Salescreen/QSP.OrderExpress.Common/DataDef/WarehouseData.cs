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
	public class WarehouseData : DataSet
	{
		/// <value>The constant used for Order DataSet. </value>
		public const String DTS_WAREHOUSE = "warehouses";

		private WarehouseTable warehouse;
		private PostalAddressEntityTable postalAddress;
		private PhoneNumberEntityTable phoneNumber;
		private EmailEntityTable emailAddress;	
		private WarehouseBusinessCalendarTable businessCalendar;
        
		public WarehouseData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected WarehouseData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[WarehouseTable.TBL_WAREHOUSE] != null)) 
				{
					this.Tables.Add(new WarehouseTable(ds.Tables[WarehouseTable.TBL_WAREHOUSE]));
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
				if ((ds.Tables[WarehouseBusinessCalendarTable.TBL_WAREHOUSE_BUSINESS_CALENDAR] != null)) 
				{
					this.Tables.Add(new WarehouseBusinessCalendarTable(ds.Tables[WarehouseBusinessCalendarTable.TBL_WAREHOUSE_BUSINESS_CALENDAR]));
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
        
		public WarehouseTable Warehouse 
		{
			get 
			{
				return this.warehouse;
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

		public WarehouseBusinessCalendarTable BusinessCalendar
		{
			get 
			{
				return this.businessCalendar;
			}
		}
        
		public override DataSet Clone() 
		{
			WarehouseData cln = ((WarehouseData)(base.Clone()));
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
			if ((ds.Tables[WarehouseTable.TBL_WAREHOUSE] != null)) 
			{
				this.Tables.Add(new WarehouseTable(ds.Tables[WarehouseTable.TBL_WAREHOUSE]));
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
			if ((ds.Tables[WarehouseBusinessCalendarTable.TBL_WAREHOUSE_BUSINESS_CALENDAR] != null)) 
			{
				this.Tables.Add(new WarehouseBusinessCalendarTable(ds.Tables[WarehouseBusinessCalendarTable.TBL_WAREHOUSE_BUSINESS_CALENDAR]));
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
			this.warehouse = ((WarehouseTable)(this.Tables[WarehouseTable.TBL_WAREHOUSE]));
			if ((this.warehouse != null)) 
			{
				this.warehouse.InitVars();
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
			this.businessCalendar = ((WarehouseBusinessCalendarTable)(this.Tables[WarehouseBusinessCalendarTable.TBL_WAREHOUSE_BUSINESS_CALENDAR]));
			if ((this.businessCalendar != null)) 
			{
				this.businessCalendar.InitVars();
			}
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_WAREHOUSE;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/WarehouseData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Warehouse
			this.warehouse = new WarehouseTable();
			this.Tables.Add(this.warehouse);
			//Postal Address
			this.postalAddress = new PostalAddressEntityTable();
			this.Tables.Add(this.postalAddress);
			//Phone Number
			this.phoneNumber = new PhoneNumberEntityTable();
			this.Tables.Add(this.phoneNumber);
			//Email Address
			this.emailAddress = new EmailEntityTable();
			this.Tables.Add(this.emailAddress);
			//Business Calendar
			this.businessCalendar = new WarehouseBusinessCalendarTable();
			this.Tables.Add(this.businessCalendar);
		}
        

		private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) 
		{
			if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) 
			{
				this.InitVars();
			}
		}
	}
}
