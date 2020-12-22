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
	///         This class is used to define the shape of CatalogData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type CatalogData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CatalogData : DataSet
	{
		/// <value>The constant used for Catalog DataSet. </value>
		public const String DTS_CATALOG = "catalogs";

		private CatalogTable catalog;
		private CatalogGroupCatalogTable catalogGroupCatalog;
		private CatalogItemTable catalogItem;
		private CatalogItemCategoryTable catalogItemCategory;
        
		public CatalogData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected CatalogData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[CatalogTable.TBL_CATALOG] != null)) 
				{
					this.Tables.Add(new CatalogTable(ds.Tables[CatalogTable.TBL_CATALOG]));
				}				
				if ((ds.Tables[CatalogGroupCatalogTable.TBL_CATALOG_GROUP_CATALOG] != null)) 
				{
					this.Tables.Add(new CatalogGroupCatalogTable(ds.Tables[CatalogGroupCatalogTable.TBL_CATALOG_GROUP_CATALOG]));
				}
				if ((ds.Tables[CatalogItemTable.TBL_CATALOG_ITEM] != null)) 
				{
					this.Tables.Add(new CatalogItemTable(ds.Tables[CatalogItemTable.TBL_CATALOG_ITEM]));
				}
				if ((ds.Tables[CatalogItemCategoryTable.TBL_CATALOG_ITEM_CATEGORY] != null)) 
				{
					this.Tables.Add(new CatalogItemCategoryTable(ds.Tables[CatalogItemCategoryTable.TBL_CATALOG_ITEM_CATEGORY]));
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
        
		public CatalogTable Catalog 
		{
			get 
			{
				return this.catalog;
			}
		}
        
		public CatalogGroupCatalogTable CatalogGroup
		{
			get 
			{
				return this.catalogGroupCatalog;
			}
		}

		public CatalogItemTable CatalogItem
		{
			get 
			{
				return this.catalogItem;
			}
		}

		public CatalogItemCategoryTable CatalogItemCategory
		{
			get 
			{
				return this.catalogItemCategory;
			}
		}
        
		public override DataSet Clone() 
		{
			CatalogData cln = ((CatalogData)(base.Clone()));
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
			if ((ds.Tables[CatalogTable.TBL_CATALOG] != null)) 
			{
				this.Tables.Add(new CatalogTable(ds.Tables[CatalogTable.TBL_CATALOG]));
			}
			if ((ds.Tables[CatalogGroupCatalogTable.TBL_CATALOG_GROUP_CATALOG] != null)) 
			{
				this.Tables.Add(new CatalogGroupCatalogTable(ds.Tables[CatalogGroupCatalogTable.TBL_CATALOG_GROUP_CATALOG]));
			}
			if ((ds.Tables[CatalogItemTable.TBL_CATALOG_ITEM] != null)) 
			{
				this.Tables.Add(new CatalogItemTable(ds.Tables[CatalogItemTable.TBL_CATALOG_ITEM]));
			}
			if ((ds.Tables[CatalogItemCategoryTable.TBL_CATALOG_ITEM_CATEGORY] != null)) 
			{
				this.Tables.Add(new CatalogItemCategoryTable(ds.Tables[CatalogItemCategoryTable.TBL_CATALOG_ITEM_CATEGORY]));
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
			this.catalog = ((CatalogTable)(this.Tables[CatalogTable.TBL_CATALOG]));
			if ((this.catalog != null)) 
			{
				this.catalog.InitVars();
			}
			this.catalogGroupCatalog = ((CatalogGroupCatalogTable)(this.Tables[CatalogGroupCatalogTable.TBL_CATALOG_GROUP_CATALOG]));
			if ((this.catalogGroupCatalog != null)) 
			{
				this.catalogGroupCatalog.InitVars();
			}
			this.catalogItem = ((CatalogItemTable)(this.Tables[CatalogItemTable.TBL_CATALOG_ITEM]));
			if ((this.catalogItem != null)) 
			{
				this.catalogItem.InitVars();
			}
			this.catalogItemCategory = ((CatalogItemCategoryTable)(this.Tables[CatalogItemCategoryTable.TBL_CATALOG_ITEM_CATEGORY]));
			if ((this.catalogItemCategory != null)) 
			{
				this.catalogItemCategory.InitVars();
			}
			
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_CATALOG;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/CatalogData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Catalog
			this.catalog = new CatalogTable();
			this.Tables.Add(this.catalog);
			//Business rule
			this.catalogGroupCatalog = new CatalogGroupCatalogTable();
			this.Tables.Add(this.catalogGroupCatalog);
			//Business exception
			this.catalogItem = new CatalogItemTable();
			this.Tables.Add(this.catalogItem);
			//Business task
			this.catalogItemCategory = new CatalogItemCategoryTable();
			this.Tables.Add(this.catalogItemCategory);
			
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
