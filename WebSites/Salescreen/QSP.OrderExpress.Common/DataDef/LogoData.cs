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
	///     A custom serializable dataset containing user inLogoation.
	///     <remarks>
	///         This class is used to define the shape of LogoData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type LogoData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class LogoData : DataSet
	{
		/// <value>The constant used for Logo DataSet. </value>
		public const String DTS_LOGO = "Logos";

		private LogoTable logo;
		private LogoSubdivisionTable logoSubdivision;
		
		public LogoData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected LogoData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[LogoTable.TBL_LOGO] != null)) 
				{
					this.Tables.Add(new LogoTable(ds.Tables[LogoTable.TBL_LOGO]));
				}				
				if ((ds.Tables[LogoSubdivisionTable.TBL_LOGO_SUBDIVISION] != null)) 
				{
					this.Tables.Add(new LogoSubdivisionTable(ds.Tables[LogoSubdivisionTable.TBL_LOGO_SUBDIVISION]));
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
        
		public LogoTable Logo 
		{
			get 
			{
				return this.logo;
			}
		}
        
		public LogoSubdivisionTable LogoSubdivision
		{
			get 
			{
				return this.logoSubdivision;
			}
		}
        
		public override DataSet Clone() 
		{
			LogoData cln = ((LogoData)(base.Clone()));
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
			if ((ds.Tables[LogoTable.TBL_LOGO] != null)) 
			{
				this.Tables.Add(new LogoTable(ds.Tables[LogoTable.TBL_LOGO]));
			}
			if ((ds.Tables[LogoSubdivisionTable.TBL_LOGO_SUBDIVISION] != null)) 
			{
				this.Tables.Add(new LogoSubdivisionTable(ds.Tables[LogoSubdivisionTable.TBL_LOGO_SUBDIVISION]));
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
			this.logo = ((LogoTable)(this.Tables[LogoTable.TBL_LOGO]));
			if ((this.logo != null)) 
			{
				this.logo.InitVars();
			}
			this.logoSubdivision = ((LogoSubdivisionTable)(this.Tables[LogoSubdivisionTable.TBL_LOGO_SUBDIVISION]));
			if ((this.logoSubdivision != null)) 
			{
				this.logoSubdivision.InitVars();
			}
			
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_LOGO;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/LogoData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Logo
			this.logo = new LogoTable();
			this.Tables.Add(this.Logo);
			//Business rule
			this.logoSubdivision = new LogoSubdivisionTable();
			this.Tables.Add(this.logoSubdivision);			
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
