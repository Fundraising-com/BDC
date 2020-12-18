using System;
using System.Data;
using System.Runtime.Serialization;
using System.Xml;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of OrderData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type OrderData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class OrderData : DataSet
	{
		/// <value>The constant used for Order DataSet. </value>
		public const String DTS_ORDER = "orders";
		public const String TBL_SUPPLY = "orderSupply";
//		public const String REL_HEADER_DETAIL = "orderorder_detail";
		
		//Tables
		private OrderHeaderTable orderHeader;
		private PostalAddressEntityTable orderPostalAddress;
		private PhoneNumberEntityTable orderPhoneNumber;
		private EmailEntityTable orderEmailAddress;
		private OrderDetailTable orderDetail;
		private OrderDetailTaxTable orderDetailTax;
		private ShipmentGroupTable shipmentGroup;
		private OrderGroupTable orderGroup;
		private DocumentEntityTable orderDocument;
		private ValidationTable orderValidation;
		private EntityExceptionTable orderException;
		private OrderDetailTable orderSupply;
		//Relations
//		private DataRelation relOrderHeader_OrderDetail;
        
		public OrderData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected OrderData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[OrderGroupTable.TBL_ORDER_GROUP] != null)) 
				{
					this.Tables.Add(new OrderGroupTable(ds.Tables[OrderGroupTable.TBL_ORDER_GROUP]));
				}
				if ((ds.Tables[OrderHeaderTable.TBL_ORDERS] != null)) 
				{
					this.Tables.Add(new OrderHeaderTable(ds.Tables[OrderHeaderTable.TBL_ORDERS]));
				}				
				if ((ds.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY] != null)) 
				{
					this.Tables.Add(new PostalAddressEntityTable(ds.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY]));
				}
				if ((ds.Tables[OrderDetailTable.TBL_ORDER_DETAILS] != null)) 
				{
					this.Tables.Add(new OrderDetailTable(ds.Tables[OrderDetailTable.TBL_ORDER_DETAILS]));
				}
				if ((ds.Tables[OrderDetailTaxTable.TBL_ORDER_DETAILS_TAX] != null)) 
				{
					this.Tables.Add(new OrderDetailTaxTable(ds.Tables[OrderDetailTaxTable.TBL_ORDER_DETAILS_TAX]));
				}
				if ((ds.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP] != null)) 
				{
					this.Tables.Add(new ShipmentGroupTable(ds.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP]));
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
				if ((ds.Tables[TBL_SUPPLY] != null)) 
				{
					this.Tables.Add(new OrderDetailTable(ds.Tables[TBL_SUPPLY]));
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
            this.orderDetail.PKColumnChanged += new DataColumnChangeEventHandler(OrderDetail_PKColumnChanged);

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
        
		public PostalAddressEntityTable OrderPostalAddress 
		{
			get 
			{
				return this.orderPostalAddress;
			}
		}
         
		public OrderDetailTable OrderDetail
		{
			get 
			{
				return this.orderDetail;
			}
		}

		public OrderDetailTaxTable OrderDetailTax
		{
			get 
			{
				return this.orderDetailTax;
			}
		}

		public ShipmentGroupTable ShipmentGroup
		{
			get 
			{
				return this.shipmentGroup;
			}
		}

		public PhoneNumberEntityTable OrderPhoneNumber
		{
			get 
			{
				return this.orderPhoneNumber;
			}
		}

		public EmailEntityTable OrderEmailAddress
		{
			get 
			{
				return this.orderEmailAddress;
			}
		}

		public DocumentEntityTable OrderDocument
		{
			get 
			{
				return this.orderDocument;
			}
		}

		public ValidationTable OrderValidation
		{
			get 
			{
				return this.orderValidation;
			}
		}
		public EntityExceptionTable OrderException
		{
			get 
			{
				return this.orderException;
			}
		}
		public OrderDetailTable OrderSupply
		{
			get 
			{
				return this.orderSupply;
			}
		}

		        
		public override DataSet Clone() 
		{
			OrderData cln = ((OrderData)(base.Clone()));
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
			if ((ds.Tables[OrderGroupTable.TBL_ORDER_GROUP] != null)) 
			{
				this.Tables.Add(new OrderGroupTable(ds.Tables[OrderGroupTable.TBL_ORDER_GROUP]));
			}
			if ((ds.Tables[OrderHeaderTable.TBL_ORDERS] != null)) 
			{
				this.Tables.Add(new OrderHeaderTable(ds.Tables[OrderHeaderTable.TBL_ORDERS]));
			}
			if ((ds.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY] != null)) 
			{
				this.Tables.Add(new PostalAddressEntityTable(ds.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY]));
			}
			if ((ds.Tables[OrderDetailTable.TBL_ORDER_DETAILS] != null)) 
			{
				this.Tables.Add(new OrderDetailTable(ds.Tables[OrderDetailTable.TBL_ORDER_DETAILS]));
			}
			if ((ds.Tables[OrderDetailTaxTable.TBL_ORDER_DETAILS_TAX] != null)) 
			{
				this.Tables.Add(new OrderDetailTaxTable(ds.Tables[OrderDetailTaxTable.TBL_ORDER_DETAILS_TAX]));
			}
			if ((ds.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP] != null)) 
			{
				this.Tables.Add(new ShipmentGroupTable(ds.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP]));
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
			if ((ds.Tables[TBL_SUPPLY] != null)) 
			{
				this.Tables.Add(new OrderDetailTable(ds.Tables[TBL_SUPPLY]));
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
			this.orderPostalAddress = ((PostalAddressEntityTable)(this.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY]));
			if ((this.orderPostalAddress != null)) 
			{
				this.orderPostalAddress.InitVars();
			}
			this.orderDetail = ((OrderDetailTable)(this.Tables[OrderDetailTable.TBL_ORDER_DETAILS]));
			if ((this.orderDetail != null)) 
			{
				this.orderDetail.InitVars();
			}
			this.orderDetailTax = ((OrderDetailTaxTable)(this.Tables[OrderDetailTaxTable.TBL_ORDER_DETAILS_TAX]));
			if ((this.orderDetailTax != null)) 
			{
				this.orderDetailTax.InitVars();
			}
			this.shipmentGroup = ((ShipmentGroupTable)(this.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP]));
			if ((this.shipmentGroup != null)) 
			{
				this.shipmentGroup.InitVars();
			}
			this.orderPhoneNumber = ((PhoneNumberEntityTable)(this.Tables[PhoneNumberEntityTable.TBL_PHONE_NUMBER_ENTITY]));
			if ((this.orderPhoneNumber != null)) 
			{
				this.orderPhoneNumber.InitVars();
			}
			this.orderEmailAddress = ((EmailEntityTable)(this.Tables[EmailEntityTable.TBL_EMAIL_ENTITY]));
			if ((this.orderEmailAddress != null)) 
			{
				this.orderEmailAddress.InitVars();
			}
			this.orderDocument = ((DocumentEntityTable)(this.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY]));
			if ((this.orderDocument != null)) 
			{
				this.orderDocument.InitVars();
			}
			this.orderException = ((EntityExceptionTable)(this.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
			if ((this.orderException != null)) 
			{
				this.orderException.InitVars();
			}
			this.orderValidation = ((ValidationTable)(this.Tables[ValidationTable.TBL_VALIDATION]));
			if ((this.orderValidation != null)) 
			{
				this.orderValidation.InitVars();
			}
			this.orderSupply = ((OrderDetailTable)(this.Tables[TBL_SUPPLY]));
			if ((this.orderSupply != null)) 
			{
				this.orderSupply.InitVars();
			}
//			//Relations
//			this.relOrderHeader_OrderDetail = this.Relations[REL_HEADER_DETAIL];

		}
        
		private void InitClass() 
		{
			this.DataSetName = "OrderData";
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/OrderData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//OrderGroup
			this.orderGroup = new OrderGroupTable();
			this.Tables.Add(this.orderGroup);
			//OrderHeader
			this.orderHeader = new OrderHeaderTable();
			this.Tables.Add(this.orderHeader);
			//Order Postal Address
			this.orderPostalAddress = new PostalAddressEntityTable();
			this.orderPostalAddress.TableName = PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY;
			this.Tables.Add(this.orderPostalAddress);
			//OrderDetail
			this.orderDetail = new OrderDetailTable();
			this.Tables.Add(this.orderDetail);
            
			//Order Detail Tax
			this.orderDetailTax = new OrderDetailTaxTable();
			this.Tables.Add(this.orderDetailTax);
			//Shipment Group
			this.shipmentGroup = new ShipmentGroupTable();
			this.Tables.Add(this.shipmentGroup);
			//Phone Number
			this.orderPhoneNumber = new PhoneNumberEntityTable();
			this.Tables.Add(this.orderPhoneNumber);
			//Shipment Group
			this.orderEmailAddress = new EmailEntityTable();
			this.Tables.Add(this.orderEmailAddress);
			//Account Document
			this.orderDocument = new DocumentEntityTable();
			this.Tables.Add(this.orderDocument);
			//Order Exception
			this.orderException = new EntityExceptionTable();
			this.Tables.Add(this.orderException);
			//Order Validation
			this.orderValidation = new ValidationTable();
			this.Tables.Add(this.orderValidation);
			//Order Detail Supply
			OrderDetailTable dtSupply = new OrderDetailTable();
			dtSupply.TableName = TBL_SUPPLY;
			this.orderSupply = dtSupply;
			this.Tables.Add(this.orderSupply);

            

			//Create Relation
//			DataColumn parentCol;
//			DataColumn childCol;
//			parentCol = this.orderHeader.Columns[OrderHeaderTable.FLD_PKID];
//			childCol = this.orderDetail.Columns[OrderDetailTable.FLD_ORDER_ID];
//			this.relOrderHeader_OrderDetail = new DataRelation(REL_HEADER_DETAIL, parentCol, childCol);
//			this.Relations.Add(this.relOrderHeader_OrderDetail);
//			
//			//Expression To Add and doing calculation between tables
//			this.orderHeader.Columns[OrderHeaderTable.FLD_TOTAL_QTY].Expression = "SUM(Child(" + REL_HEADER_DETAIL + ")." + OrderDetailTable.FLD_QUANTITY + ")";
//			//(" + REL_HEADER_DETAIL + ")
		}


		private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) 
		{
			if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) 
			{
				this.InitVars();
			}
		}

        private void OrderDetail_PKColumnChanged(object sender, DataColumnChangeEventArgs args)
        {
            if (args.Column == this.orderDetail.Columns[OrderDetailTable.FLD_PKID])
            {
                if (args.ProposedValue.ToString().Length > 0)
                {
                    int NewID = Convert.ToInt32(args.ProposedValue);
                    int OldID = Convert.ToInt32(args.Row[args.Column]);
                    if (this.orderDetailTax.Rows.Count > 0)
                    {
                        DataView dv = new DataView(this.orderDetailTax);
                        dv.RowStateFilter = DataViewRowState.Added;
                        foreach (DataRowView drvRow in dv)
                        {
                            if (drvRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID].ToString() == OldID.ToString())
                            {
                                drvRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID] = NewID;
                            }                        
                        }
                        
                    }
                }
            }
            //Console.Write(" ColumnChanged: ");
            //Console.Write(args.Column.ColumnName + " changed to '" + args.ProposedValue + "'\n");
        }
		

	}
}
