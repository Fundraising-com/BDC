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
	///     A custom serializable dataset containing user inPromo_Textation.
	///     <remarks>
	///         This class is used to define the shape of Promo_TextData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type Promo_TextData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class Promo_TextData : DataSet
	{
		/// <value>The constant used for Promo_Text DataSet. </value>
		public const String DTS_Promo_Text = "Promo_Texts";

		private Promo_TextTable _Promo_Text;
		private Promo_TextSubdivisionTable _Promo_TextSubdivision;
		
		public Promo_TextData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected Promo_TextData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[Promo_TextTable.TBL_Promo_Text] != null)) 
				{
					this.Tables.Add(new Promo_TextTable(ds.Tables[Promo_TextTable.TBL_Promo_Text]));
				}				
				if ((ds.Tables[Promo_TextSubdivisionTable.TBL_Promo_Text_SUBDIVISION] != null)) 
				{
					this.Tables.Add(new Promo_TextSubdivisionTable(ds.Tables[Promo_TextSubdivisionTable.TBL_Promo_Text_SUBDIVISION]));
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
        
		public Promo_TextTable Promo_Text 
		{
			get 
			{
				return this._Promo_Text;
			}
		}
        
		public Promo_TextSubdivisionTable Promo_TextSubdivision
		{
			get 
			{
				return this._Promo_TextSubdivision;
			}
		}
        
		public override DataSet Clone() 
		{
			Promo_TextData cln = ((Promo_TextData)(base.Clone()));
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
			if ((ds.Tables[Promo_TextTable.TBL_Promo_Text] != null)) 
			{
				this.Tables.Add(new Promo_TextTable(ds.Tables[Promo_TextTable.TBL_Promo_Text]));
			}
			if ((ds.Tables[Promo_TextSubdivisionTable.TBL_Promo_Text_SUBDIVISION] != null)) 
			{
				this.Tables.Add(new Promo_TextSubdivisionTable(ds.Tables[Promo_TextSubdivisionTable.TBL_Promo_Text_SUBDIVISION]));
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
			this._Promo_Text = ((Promo_TextTable)(this.Tables[Promo_TextTable.TBL_Promo_Text]));
			if ((this.Promo_Text != null)) 
			{
				this.Promo_Text.InitVars();
			}
			this._Promo_TextSubdivision = ((Promo_TextSubdivisionTable)(this.Tables[Promo_TextSubdivisionTable.TBL_Promo_Text_SUBDIVISION]));
			if ((this._Promo_TextSubdivision != null)) 
			{
				this._Promo_TextSubdivision.InitVars();
			}
			
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_Promo_Text;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/Promo_TextData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Promo_Text
			this._Promo_Text = new Promo_TextTable();
			this.Tables.Add(this.Promo_Text);
			//Business rule
			this._Promo_TextSubdivision = new Promo_TextSubdivisionTable();
			this.Tables.Add(this._Promo_TextSubdivision);			
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
