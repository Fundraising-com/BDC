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
	///         This class is used to define the shape of FormData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type FormData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class FormData : DataSet
	{
		/// <value>The constant used for Form DataSet. </value>
		public const String DTS_FORM = "forms";

		private FormTable form;
		private BusinessRuleTable businessRule;
		private BusinessExceptionTable businessException;
		private BusinessTaskTable businessTask;
		private FormSectionTable formSection;
        private FormDeliveryMethodTable formDeliveryMethod;
        private FormOrderTypeTable formOrderType;
        private FormProfitRateTable formProfitRate;
        
		public FormData() 
		{
			this.InitClass();
			System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
			this.Tables.CollectionChanged += schemaChangedHandler;
			this.Relations.CollectionChanged += schemaChangedHandler;
		}
        
		protected FormData(SerializationInfo info, StreamingContext context) 
		{
			string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
			if ((strSchema != null)) 
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
				if ((ds.Tables[FormTable.TBL_FORM] != null)) 
				{
					this.Tables.Add(new FormTable(ds.Tables[FormTable.TBL_FORM]));
				}				
				if ((ds.Tables[BusinessRuleTable.TBL_BUSINESS_RULE] != null)) 
				{
					this.Tables.Add(new BusinessRuleTable(ds.Tables[BusinessRuleTable.TBL_BUSINESS_RULE]));
				}
				if ((ds.Tables[BusinessExceptionTable.TBL_BUSINESS_EXCEPTION] != null)) 
				{
					this.Tables.Add(new BusinessExceptionTable(ds.Tables[BusinessExceptionTable.TBL_BUSINESS_EXCEPTION]));
				}
				if ((ds.Tables[BusinessTaskTable.TBL_BUSINESS_TASK] != null)) 
				{
					this.Tables.Add(new BusinessTaskTable(ds.Tables[BusinessTaskTable.TBL_BUSINESS_TASK]));
				}
				if ((ds.Tables[FormSectionTable.TBL_FORM_SECTION] != null)) 
				{
					this.Tables.Add(new FormSectionTable(ds.Tables[FormSectionTable.TBL_FORM_SECTION]));
				}
                if ((ds.Tables[FormDeliveryMethodTable.TBL_FORM_DELIVERY_METHOD] != null))
                {
                    this.Tables.Add(new FormDeliveryMethodTable(ds.Tables[FormDeliveryMethodTable.TBL_FORM_DELIVERY_METHOD]));
                }
                if ((ds.Tables[FormOrderTypeTable.TBL_FORM_ORDER_TYPE] != null))
                {
                    this.Tables.Add(new FormOrderTypeTable(ds.Tables[FormOrderTypeTable.TBL_FORM_ORDER_TYPE]));
                }
                if ((ds.Tables[FormProfitRateTable.TBL_FORM_PROFIT_RATE] != null))
                {
                    this.Tables.Add(new FormProfitRateTable(ds.Tables[FormProfitRateTable.TBL_FORM_PROFIT_RATE]));
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
        
		public FormTable Form 
		{
			get 
			{
				return this.form;
			}
		}
        
		public BusinessRuleTable BusinessRule
		{
			get 
			{
				return this.businessRule;
			}
		}

		public BusinessExceptionTable BusinessException
		{
			get 
			{
				return this.businessException;
			}
		}

		public BusinessTaskTable BusinessTask
		{
			get 
			{
				return this.businessTask;
			}
		}

		public FormSectionTable FormSection
		{
			get 
			{
				return this.formSection;
			}
		}

        public FormDeliveryMethodTable FormDeliveryMethod
        {
            get
            {
                return this.formDeliveryMethod;
            }
        }
        public FormOrderTypeTable FormOrderType
        {
            get
            {
                return this.formOrderType;
            }
        }
        public FormProfitRateTable FormProfitRate
        {
            get
            {
                return this.formProfitRate;
            }
        }
        
		public override DataSet Clone() 
		{
			FormData cln = ((FormData)(base.Clone()));
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
			if ((ds.Tables[FormTable.TBL_FORM] != null)) 
			{
				this.Tables.Add(new FormTable(ds.Tables[FormTable.TBL_FORM]));
			}
			if ((ds.Tables[BusinessRuleTable.TBL_BUSINESS_RULE] != null)) 
			{
				this.Tables.Add(new BusinessRuleTable(ds.Tables[BusinessRuleTable.TBL_BUSINESS_RULE]));
			}
			if ((ds.Tables[BusinessExceptionTable.TBL_BUSINESS_EXCEPTION] != null)) 
			{
				this.Tables.Add(new BusinessExceptionTable(ds.Tables[BusinessExceptionTable.TBL_BUSINESS_EXCEPTION]));
			}
			if ((ds.Tables[BusinessTaskTable.TBL_BUSINESS_TASK] != null)) 
			{
				this.Tables.Add(new BusinessTaskTable(ds.Tables[BusinessTaskTable.TBL_BUSINESS_TASK]));
			}
			if ((ds.Tables[FormSectionTable.TBL_FORM_SECTION] != null)) 
			{
				this.Tables.Add(new FormSectionTable(ds.Tables[FormSectionTable.TBL_FORM_SECTION]));
			}
            if ((ds.Tables[FormDeliveryMethodTable.TBL_FORM_DELIVERY_METHOD] != null))
            {
                this.Tables.Add(new FormDeliveryMethodTable(ds.Tables[FormDeliveryMethodTable.TBL_FORM_DELIVERY_METHOD]));
            }
            if ((ds.Tables[FormOrderTypeTable.TBL_FORM_ORDER_TYPE] != null))
            {
                this.Tables.Add(new FormOrderTypeTable(ds.Tables[FormOrderTypeTable.TBL_FORM_ORDER_TYPE]));
            }
            if ((ds.Tables[FormProfitRateTable.TBL_FORM_PROFIT_RATE] != null))
            {
                this.Tables.Add(new FormProfitRateTable(ds.Tables[FormProfitRateTable.TBL_FORM_PROFIT_RATE]));
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
			this.form = ((FormTable)(this.Tables[FormTable.TBL_FORM]));
			if ((this.form != null)) 
			{
				this.form.InitVars();
			}
			this.businessRule = ((BusinessRuleTable)(this.Tables[BusinessRuleTable.TBL_BUSINESS_RULE]));
			if ((this.businessRule != null)) 
			{
				this.businessRule.InitVars();
			}
			this.businessException = ((BusinessExceptionTable)(this.Tables[BusinessExceptionTable.TBL_BUSINESS_EXCEPTION]));
			if ((this.businessException != null)) 
			{
				this.businessException.InitVars();
			}
			this.businessTask = ((BusinessTaskTable)(this.Tables[BusinessTaskTable.TBL_BUSINESS_TASK]));
			if ((this.businessTask != null)) 
			{
				this.businessTask.InitVars();
			}
			this.formSection = ((FormSectionTable)(this.Tables[FormSectionTable.TBL_FORM_SECTION]));
			if ((this.formSection != null)) 
			{
				this.formSection.InitVars();
			}
            this.formDeliveryMethod = ((FormDeliveryMethodTable)(this.Tables[FormDeliveryMethodTable.TBL_FORM_DELIVERY_METHOD]));
            if ((this.formDeliveryMethod != null))
            {
                this.formDeliveryMethod.InitVars();
            }
            this.formOrderType = ((FormOrderTypeTable)(this.Tables[FormOrderTypeTable.TBL_FORM_ORDER_TYPE]));
            if ((this.formOrderType != null))
            {
                this.formOrderType.InitVars();
            }
            this.formProfitRate = ((FormProfitRateTable)(this.Tables[FormProfitRateTable.TBL_FORM_PROFIT_RATE]));
            if ((this.formProfitRate != null))
            {
                this.formProfitRate.InitVars();
            }
		}
        
		private void InitClass() 
		{
			this.DataSetName = DTS_FORM;
			this.Prefix = "";
			this.Namespace = "http://tempuri.org/FormData.xsd";
			this.Locale = new System.Globalization.CultureInfo("en-US");
			this.CaseSensitive = false;
			this.EnforceConstraints = true;
			//Form
			this.form = new FormTable();
			this.Tables.Add(this.form);
			//Business rule
			this.businessRule = new BusinessRuleTable();
			this.Tables.Add(this.businessRule);
			//Business exception
			this.businessException = new BusinessExceptionTable();
			this.Tables.Add(this.businessException);
			//Business task
			this.businessTask = new BusinessTaskTable();
			this.Tables.Add(this.businessTask);
			//Form Catalog Group
			this.formSection = new FormSectionTable();
			this.Tables.Add(this.formSection);
            //Form Delivery Method
            this.formDeliveryMethod = new FormDeliveryMethodTable();
            this.Tables.Add(this.formDeliveryMethod);
            //Form Order Type
            this.formOrderType = new FormOrderTypeTable();
            this.Tables.Add(this.formOrderType);
            //Form Profit Rate
            this.formProfitRate = new FormProfitRateTable();
            this.Tables.Add(this.formProfitRate);
			
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
