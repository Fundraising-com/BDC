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
	///     A custom serializable dataset containing user inPromoation.
	///     <remarks>
	///         This class is used to define the shape of PromoData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type PromoData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class PromoCouponData : DataSet
	{
		/// <value>The constant used for Promo DataSet. </value>
		public const String DTS_PROMO_COUPON = "Promo_coupon";

		private PromoTable promo;
		private PromoCouponTable promoCoupon;
		private PromoCouponSubdivisionTable promoCouponSubdivision;
		private DocumentEntityTable promoDocument;
		private EntityExceptionTable promoException;
		private ValidationTable promoValidation;
		
		public PromoCouponData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected PromoCouponData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[PromoTable.TBL_PROMO] != null)) 
				{
					this.Tables.Add(new PromoTable(ds.Tables[PromoTable.TBL_PROMO]));
				}				
				if ((ds.Tables[PromoCouponSubdivisionTable.TBL_PROMO_COUPON_SUBDIVISION] != null)) 
				{
					this.Tables.Add(new PromoCouponSubdivisionTable(ds.Tables[PromoCouponSubdivisionTable.TBL_PROMO_COUPON_SUBDIVISION]));
				}
				if ((ds.Tables[PromoCouponTable.TBL_PROMO_COUPON] != null)) 
				{
					this.Tables.Add(new PromoCouponTable(ds.Tables[PromoCouponTable.TBL_PROMO_COUPON]));
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
        
		public PromoTable Promo 
		{
			get 
			{
				return this.promo;
			}
		}
        
		public PromoCouponSubdivisionTable PromoCouponSubdivision
		{
			get 
			{
				return this.promoCouponSubdivision;
			}
		}

		public PromoCouponTable PromoCoupon
		{
			get 
			{
				return this.promoCoupon;
			}
		}

		public DocumentEntityTable PromoDocument
		{
			get 
			{
				return this.promoDocument;
			}
		}

		public EntityExceptionTable PromoException
		{
			get 
			{
				return this.promoException;
			}
		}

		public ValidationTable PromoValidation
		{
			get 
			{
				return this.promoValidation;
			}
		}
        
		public override DataSet Clone() 
		{
			PromoData cln = ((PromoData)(base.Clone()));
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
			if ((ds.Tables[PromoTable.TBL_PROMO] != null)) 
			{
				this.Tables.Add(new PromoTable(ds.Tables[PromoTable.TBL_PROMO]));
			}
			if ((ds.Tables[PromoCouponSubdivisionTable.TBL_PROMO_COUPON_SUBDIVISION] != null)) 
			{
				this.Tables.Add(new PromoCouponSubdivisionTable(ds.Tables[PromoCouponSubdivisionTable.TBL_PROMO_COUPON_SUBDIVISION]));
			}
			if ((ds.Tables[PromoCouponTable.TBL_PROMO_COUPON] != null)) 
			{
				this.Tables.Add(new PromoCouponTable(ds.Tables[PromoCouponTable.TBL_PROMO_COUPON]));
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
			this.promo = ((PromoTable)(this.Tables[PromoTable.TBL_PROMO]));
			if ((this.promo != null)) 
			{
				this.promo.InitVars();
			}
			this.promoCouponSubdivision = ((PromoCouponSubdivisionTable)(this.Tables[PromoCouponSubdivisionTable.TBL_PROMO_COUPON_SUBDIVISION]));
			if ((this.promoCouponSubdivision != null)) 
			{
				this.promoCouponSubdivision.InitVars();
			}
			this.promoCoupon = ((PromoCouponTable)(this.Tables[PromoCouponTable.TBL_PROMO_COUPON]));
			if ((this.promoCoupon != null)) 
			{
				this.promoCoupon.InitVars();
			}
			this.promoDocument = ((DocumentEntityTable)(this.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY]));
			if ((this.promoDocument != null)) 
			{
				this.promoDocument.InitVars();
			}
			this.promoException = ((EntityExceptionTable)(this.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION]));
			if ((this.promoException != null)) 
			{
				this.promoException.InitVars();
			}
			this.promoValidation = ((ValidationTable)(this.Tables[ValidationTable.TBL_VALIDATION]));
			if ((this.promoValidation != null)) 
			{
				this.promoValidation.InitVars();
			}
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_PROMO_COUPON;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/PromoCouponData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Promo
			this.promo = new PromoTable();
			this.Tables.Add(this.Promo);
			//Promo Subdivision
			this.promoCouponSubdivision = new PromoCouponSubdivisionTable();
			this.Tables.Add(this.promoCouponSubdivision);		
			//Promo Coupon
			this.promoCoupon = new PromoCouponTable();
			this.Tables.Add(this.promoCoupon);	
			//Promo Document
			this.promoDocument = new DocumentEntityTable();
			this.Tables.Add(this.promoDocument);
			//Promo Exception
			this.promoException = new EntityExceptionTable();
			this.Tables.Add(this.promoException);
			//Promo Validation
			this.promoValidation = new ValidationTable();
			this.Tables.Add(this.promoValidation);
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
