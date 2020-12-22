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
	///     A custom serializable dataset containing user inPromo_logoation.
	///     <remarks>
	///         This class is used to define the shape of Promo_logoData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type Promo_logoData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class Promo_LogoData : DataSet
	{
		/// <value>The constant used for Promo_logo DataSet. </value>
		public const String DTS_Promo_logo = "Promo_logos";

		private Promo_LogoTable _Promo_logo;
		private Promo_LogoSubdivisionTable _Promo_logoSubdivision;
		
		public Promo_LogoData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected Promo_LogoData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[Promo_LogoTable.TBL_Promo_logo] != null)) 
				{
					this.Tables.Add(new Promo_LogoTable(ds.Tables[Promo_LogoTable.TBL_Promo_logo]));
				}				
				if ((ds.Tables[Promo_LogoSubdivisionTable.TBL_Promo_logo_SUBDIVISION] != null)) 
				{
					this.Tables.Add(new Promo_LogoSubdivisionTable(ds.Tables[Promo_LogoSubdivisionTable.TBL_Promo_logo_SUBDIVISION]));
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
        
		public Promo_LogoTable Promo_logo 
		{
			get 
			{
				return this._Promo_logo;
			}
		}
        
		public Promo_LogoSubdivisionTable Promo_LogoSubdivision
		{
			get 
			{
				return this._Promo_logoSubdivision;
			}
		}
        
		public override DataSet Clone() 
		{
			Promo_LogoData cln = ((Promo_LogoData)(base.Clone()));
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
			if ((ds.Tables[Promo_LogoTable.TBL_Promo_logo] != null)) 
			{
				this.Tables.Add(new Promo_LogoTable(ds.Tables[Promo_LogoTable.TBL_Promo_logo]));
			}
			if ((ds.Tables[Promo_LogoSubdivisionTable.TBL_Promo_logo_SUBDIVISION] != null)) 
			{
				this.Tables.Add(new Promo_LogoSubdivisionTable(ds.Tables[Promo_LogoSubdivisionTable.TBL_Promo_logo_SUBDIVISION]));
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
			this._Promo_logo = ((Promo_LogoTable)(this.Tables[Promo_LogoTable.TBL_Promo_logo]));
			if ((this.Promo_logo != null)) 
			{
				this.Promo_logo.InitVars();
			}
			this._Promo_logoSubdivision = ((Promo_LogoSubdivisionTable)(this.Tables[Promo_LogoSubdivisionTable.TBL_Promo_logo_SUBDIVISION]));
			if ((this._Promo_logoSubdivision != null)) 
			{
				this._Promo_logoSubdivision.InitVars();
			}
			
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_Promo_logo;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/Promo_logoData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Promo_logo
			this._Promo_logo = new Promo_LogoTable();
			this.Tables.Add(this.Promo_logo);
			//Business rule
			this._Promo_logoSubdivision = new Promo_LogoSubdivisionTable();
			this.Tables.Add(this._Promo_logoSubdivision);			
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
