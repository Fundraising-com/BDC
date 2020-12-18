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
	///         This class is used to define the shape of AppMenuItemData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type AppMenuItemData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class AppItemData : DataSet
	{		
		//
		//AppItemData constants
		//
		public const String DTS_APP_ITEMS = "AppItems";
		
		//ENUMERATION OF DETAIL TYPE CONSTANT
		/// <value>The constant used for CM_AppItems table. </value>
		public const int DETAIL_TYPE_WEBFORM_SEARCH = 1;
		public const int DETAIL_TYPE_REPORT_PARAMETER = 2;
		public const int DETAIL_TYPE_REPORT_TYPE = 3;
		public const int DETAIL_TYPE_REPORT_SORT = 5;

		private AppItemTable appItem;
		private AppItemDetailTable appItemDetail;
		private AppItemFAQTable appItemFAQ;
		private RolePermissionTable rolePermission;
		
        
		public AppItemData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected AppItemData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[AppItemTable.TBL_APP_ITEM] != null)) 
				{
					this.Tables.Add(new AppItemTable(ds.Tables[AppItemTable.TBL_APP_ITEM]));
				}
				if ((ds.Tables[AppItemDetailTable.TBL_DETAIL_ITEMS] != null)) 
				{
					this.Tables.Add(new AppItemDetailTable(ds.Tables[AppItemDetailTable.TBL_DETAIL_ITEMS]));
				}
				if ((ds.Tables[AppItemFAQTable.TBL_FAQ_ITEMS] != null)) 
				{
					this.Tables.Add(new AppItemFAQTable(ds.Tables[AppItemFAQTable.TBL_FAQ_ITEMS]));
				}	
				if ((ds.Tables[RolePermissionTable.TBL_ROLE_PERMISSION] != null)) 
				{
					this.Tables.Add(new RolePermissionTable(ds.Tables[RolePermissionTable.TBL_ROLE_PERMISSION]));
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
        
		public AppItemTable AppItem 
		{
			get 
			{
				return this.appItem;
			}
		}

		public AppItemDetailTable AppItemDetail
		{
			get 
			{
				return this.appItemDetail;
			}
		}

		public AppItemFAQTable AppItemFAQ
		{
			get 
			{
				return this.appItemFAQ;
			}
		}
        
		public RolePermissionTable RolePermission
		{
			get 
			{
				return this.rolePermission;
			}
		}

		public override DataSet Clone() 
		{
			AppItemData cln = ((AppItemData)(base.Clone()));
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
			if ((ds.Tables[AppItemTable.TBL_APP_ITEM] != null)) 
			{
				this.Tables.Add(new AppItemTable(ds.Tables[AppItemTable.TBL_APP_ITEM]));
			}
			if ((ds.Tables[AppItemDetailTable.TBL_DETAIL_ITEMS] != null)) 
			{
				this.Tables.Add(new AppItemDetailTable(ds.Tables[AppItemDetailTable.TBL_DETAIL_ITEMS]));
			}
			if ((ds.Tables[AppItemFAQTable.TBL_FAQ_ITEMS] != null)) 
			{
				this.Tables.Add(new AppItemFAQTable(ds.Tables[AppItemFAQTable.TBL_FAQ_ITEMS]));
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
			this.appItem = ((AppItemTable)(this.Tables[AppItemTable.TBL_APP_ITEM]));
			if ((this.appItem != null)) 
			{
				this.appItem.InitVars();
			}
			this.appItemDetail = ((AppItemDetailTable)(this.Tables[AppItemDetailTable.TBL_DETAIL_ITEMS]));
			if ((this.appItemDetail != null)) 
			{
				this.appItemDetail.InitVars();
			}
			this.appItemFAQ = ((AppItemFAQTable)(this.Tables[AppItemFAQTable.TBL_FAQ_ITEMS]));
			if ((this.appItemFAQ != null)) 
			{
				this.appItemFAQ.InitVars();
			}
			this.rolePermission = ((RolePermissionTable)(this.Tables[RolePermissionTable.TBL_ROLE_PERMISSION]));
			if ((this.rolePermission != null)) 
			{
				this.rolePermission.InitVars();
			}
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_APP_ITEMS;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/AppItemData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Application Item
			this.appItem = new AppItemTable();
			this.Tables.Add(this.appItem);
			//Application Item Detail
			this.appItemDetail = new AppItemDetailTable();
			this.Tables.Add(this.appItemDetail);
			//FAQ
			this.appItemFAQ = new AppItemFAQTable();
			this.Tables.Add(this.appItemFAQ);	
			//Role Permission
			this.rolePermission = new RolePermissionTable();
			this.Tables.Add(this.rolePermission);	
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
