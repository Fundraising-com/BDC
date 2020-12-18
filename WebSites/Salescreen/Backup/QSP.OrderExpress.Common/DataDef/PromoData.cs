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
	public class PromoData : DataSet
	{
		/// <value>The constant used for Promo DataSet. </value>
		public const String DTS_PROMO = "Promos";

		private PromoTable promo;
		private PromoSubdivisionTable promoSubdivision;
		
		public PromoData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected PromoData(SerializationInfo info, StreamingContext context) 
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
				if ((ds.Tables[PromoSubdivisionTable.TBL_PROMO_SUBDIVISION] != null)) 
				{
					this.Tables.Add(new PromoSubdivisionTable(ds.Tables[PromoSubdivisionTable.TBL_PROMO_SUBDIVISION]));
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
        
		public PromoSubdivisionTable PromoSubdivision
		{
			get 
			{
				return this.promoSubdivision;
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
			if ((ds.Tables[PromoSubdivisionTable.TBL_PROMO_SUBDIVISION] != null)) 
			{
				this.Tables.Add(new PromoSubdivisionTable(ds.Tables[PromoSubdivisionTable.TBL_PROMO_SUBDIVISION]));
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
			this.promoSubdivision = ((PromoSubdivisionTable)(this.Tables[PromoSubdivisionTable.TBL_PROMO_SUBDIVISION]));
			if ((this.promoSubdivision != null)) 
			{
				this.promoSubdivision.InitVars();
			}
			
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_PROMO;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/PromoData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Promo
			this.promo = new PromoTable();
			this.Tables.Add(this.Promo);
			//Business rule
			this.promoSubdivision = new PromoSubdivisionTable();
			this.Tables.Add(this.promoSubdivision);			
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
