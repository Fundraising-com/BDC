using System;
using System.Data;
using System.Runtime.Serialization;

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
	public class OrderDetailTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Order Header constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_ORDER_DETAILS = "order_detail";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_PKID = "order_detail_id";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_ORDER_ID = "order_id";
		/// <value>The constant used for the order "Product ID" field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_DETAIL_ID = "catalog_item_detail_id";
		/// <value>The constant used for the order "Product Name" field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_CODE = "catalog_item_code";
		/// <value>The constant used for the order "Product Name" field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_NAME = "catalog_item_name";
		/// <value>The constant used for the order "Product Name" field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_DESC = "catalog_item_desc";
		/// <value>The constant used for the order "Product Name" field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_DETAIL_PRICE = "catalog_item_detail_price";
		/// <value>The constant used for the order "Product Name" field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_NB_UNITS = "nb_units";
        /// <value>The constant used for product Unit field in the Order table. </value>
        public const String FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE = "catalog_item_detail_profit_rate";
        /// <value>The constant used for the order "Source ID" field in the Order table. </value>
		public const String FLD_SOURCE_ID = "source_id";
		/// <value>The constant used for the order "Order Status ID" field in the Order table. </value>
		public const String FLD_ORDER_STATUS_ID = "order_status_id";
		/// <value>The constant used for the order "Order Status ID" field in the Order table. </value>
		public const String FLD_STATUS_REASON_ID = "status_reason_id";
		/// <value>The constant used for the order "Shipment Group ID" field in the Order table. </value>
		public const String FLD_SHIPMENT_GROUP_ID = "shipment_group_id";
		/// <value>The constant used for the order "Price" field in the Order table. </value>
		public const String FLD_PRICE = "price";
        /// <value>The constant used for product Unit field in the Order table. </value>
        public const String FLD_PROFIT_RATE = "profit_rate";
		/// <value>The constant used for Quantity field in the Order table. </value>
		public const String FLD_QUANTITY = "quantity";
		/// <value>The constant used for Quantity field in the Order table. </value>
		public const String FLD_ADJUSTMENT_QUANTITY = "adjustment_quantity";
		/// <value>The constant used for Quantity field in the Order table. </value>
		public const String FLD_AMOUNT = "amount";
		/// <value>The constant used for Quantity field in the Order table. </value>
		public const String FLD_GROSS_AMOUNT = "gross_amount";		
		/// <value>The constant used for Quantity field in the Order table. </value>
		public const String FLD_ADJUSTMENT_AMOUNT = "adjustment_amount";
		/// <value>The constant used for the order "Price" field in the Order table. </value>
		public const String FLD_TAX_RATE = "tax_rate";
		/// <value>The constant used for the order "Price" field in the Order table. </value>
		public const String FLD_TAX_AMOUNT = "tax_amount";
		/// <value>The constant used for the order "Price" field in the Order table. </value>
		public const String FLD_DISPLAY_ORDER = "display_order";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_NB_DAY_LEAD_TIME = "nb_day_lead_time";	
		public const String FLD_PERSONALIZATION_ID = "personalization_id";
        /// <value>The constant used for product Unit field in the Order table. </value>
        public const String FLD_CATALOG_ITEM_DETAIL_SCHOOL_PRICE = "catalog_item_detail_school_price";
        public const String FLD_SCHOOL_PRICE = "school_price";
        public const String FLD_CALCULATED_PRICE = "calculated_price";
        public const String FLD_PROFIT_RATE_COUNT = "profit_rate_count";
        public const String FLD_FORM_SECTION_TYPE_ID = "form_section_type_id";
        public const String FLD_FORM_SECTION_NUMBER = "form_section_number";
        
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";

        public event DataColumnChangeEventHandler PKColumnChanged;
        
        
		
		public OrderDetailTable() 
		{
			this.InitClass();
		}
		
//		public OrderDetailTable() : base(TBL_ORDER_DETAILS) {
//            this.InitClass();
//        }
		    
        public OrderDetailTable(DataTable table) : base(table.TableName) {
            if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                this.CaseSensitive = table.CaseSensitive;
            }
            if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                this.Locale = table.Locale;
            }
            if ((table.Namespace != table.DataSet.Namespace)) {
                this.Namespace = table.Namespace;
            }
            this.Prefix = table.Prefix;
            this.MinimumCapacity = table.MinimumCapacity;
            this.DisplayExpression = table.DisplayExpression;
        }
        
        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            OrderDetailTable cln = ((OrderDetailTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new OrderDetailTable();
        }
        
        internal void InitVars() {
            
        }

		public int MaxNbDayLeadTime
		{
			get
			{
				int nbDay =0;
				object obj = this.Compute("MAX(" + FLD_NB_DAY_LEAD_TIME + ")","");
				
				if (obj != System.DBNull.Value)
					nbDay = Convert.ToInt32(obj.ToString());

				return nbDay;
		   }
		}

		public int TotalQuantity
		{
			get
			{
				int quantity =0;
				foreach (DataRow row in this.Rows)
				{
					if (row.RowState != DataRowState.Deleted)
					{
						if (!row.IsNull(FLD_QUANTITY))
							quantity = quantity + Convert.ToInt32(row[FLD_QUANTITY]);
					}
				}
				return quantity;
			}
		}

		public decimal TotalAmount
		{
			get
			{
				decimal amount =0;
				foreach (DataRow row in this.Rows)
				{
					if (row.RowState != DataRowState.Deleted)
					{
						if (!row.IsNull(FLD_AMOUNT))
							amount += Convert.ToDecimal(row[FLD_AMOUNT]);
					}
				}
				return amount;
			}
		}

        public int GetTotalQuantity(int FormSectionType, int FormSectionNumber)
        {
            int quantity = 0;
            DataView dv = new DataView(this);
            string sFilter = "";
            sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionType.ToString();
            if (FormSectionNumber > 0)
                sFilter = sFilter + " AND ISNULL(" + FLD_FORM_SECTION_NUMBER + ",1) = " + FormSectionNumber.ToString();
            dv.RowFilter = sFilter;  
            foreach (DataRowView drvwRow in dv)
            {
                if (drvwRow.Row.RowState != DataRowState.Deleted)
                {
                    if (!drvwRow.Row.IsNull(FLD_QUANTITY))
                        quantity = quantity + Convert.ToInt32(drvwRow[FLD_QUANTITY]);
                }
            }
            return quantity;
            
        }

        public int GetTotalCDQuantity(int FormSectionType, int FormSectionNumber, ProductTable cdProducts)
        {
            int quantity = 0;
            DataView dv = new DataView(this);
            string sFilter = "";
            sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionType.ToString();
            if (FormSectionNumber > 0)
                sFilter = sFilter + " AND ISNULL(" + FLD_FORM_SECTION_NUMBER + ",1) = " + FormSectionNumber.ToString();
            dv.RowFilter = sFilter;
            foreach (DataRowView drvwRow in dv)
            {
                if (drvwRow.Row.RowState != DataRowState.Deleted)
                {
                    if (!drvwRow.Row.IsNull(FLD_QUANTITY))
                    {
                        if (cdProducts.Select(ProductTable.FLD_CODE + "=" + drvwRow[FLD_CATALOG_ITEM_CODE]).Length > 0)
                        {
                            quantity = quantity + Convert.ToInt32(drvwRow[FLD_QUANTITY]);
                        }
                    }
                }
            }
            return quantity;

        }

        public decimal GetTotalAmount(int FormSectionType, int FormSectionNumber)
        {
            decimal amount = 0;
            DataView dv = new DataView(this);
            string sFilter = "";
            sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionType.ToString();
            if (FormSectionNumber > 0)
                sFilter = sFilter + " AND ISNULL(" + FLD_FORM_SECTION_NUMBER + ",1) = " + FormSectionNumber.ToString();
            dv.RowFilter = sFilter;
            foreach (DataRowView drvwRow in dv)
            {
                if (drvwRow.Row.RowState != DataRowState.Deleted)
                {
                    if (!drvwRow.Row.IsNull(FLD_AMOUNT))
                        amount = amount + Convert.ToDecimal(drvwRow[FLD_AMOUNT]);
                }
            }
            return amount;

        }

        public int GetMaxNbDayLeadTime(int FormSectionTypeID)
        {
            int nbDayLeadTime = 0;
            string sFilter = "";
            if (FormSectionTypeID == FormSectionType.STANDARD_PRODUCT)
                sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionTypeID.ToString();
            else
                sFilter = FLD_FORM_SECTION_TYPE_ID + " = " + FormSectionTypeID.ToString();
            sFilter = sFilter + " AND " + FLD_QUANTITY + " > 0";
            object oMax = this.Compute("MAX(" + FLD_NB_DAY_LEAD_TIME + ")", sFilter);
            if ((oMax != null) && (oMax.ToString().Length >0))
                nbDayLeadTime = Convert.ToInt32(oMax);
            return nbDayLeadTime;

        }

        public int GetFormSectionNumber_ForMinNbDayLeadTime(int FormSectionTypeID, int MinNbDayLeadTime)
        {
            int SectionNumber = 0;
            string sFilter = "";
            if (FormSectionTypeID == FormSectionType.STANDARD_PRODUCT)
                sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionTypeID.ToString();
            else
                sFilter = FLD_FORM_SECTION_TYPE_ID + " = " + FormSectionTypeID.ToString();
            sFilter = sFilter + " AND " + FLD_QUANTITY + " > 0";
            sFilter = sFilter + " AND " + FLD_NB_DAY_LEAD_TIME + " = " + MinNbDayLeadTime.ToString();
            DataView dv = new DataView(this);
            dv.RowFilter = sFilter;
            //dv.Sort = FLD_NB_DAY_LEAD_TIME;
            if (dv.Count > 0)
            {
                if (!dv[0].Row.IsNull(FLD_FORM_SECTION_NUMBER))
                    SectionNumber = Convert.ToInt32(dv[0][FLD_FORM_SECTION_NUMBER]);
            }
               
            return SectionNumber;

        }

        public bool IsPersonalizeComplete
        {
            get
            {
                string sFilter = "";
                sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",1) <> " + FormSectionType.SUPPLY_PRODUCT.ToString();

                sFilter = sFilter + " AND " + FLD_QUANTITY + " > 0";
                sFilter = sFilter + " AND " + FLD_CATALOG_ITEM_CODE + " LIKE '*PE*'";
                sFilter = sFilter + " AND ISNULL(" + FLD_PERSONALIZATION_ID + ",0) = 0";

                DataView dv = new DataView(this);
                dv.RowFilter = sFilter;
                return !(dv.Count > 0);
            }
        }

        public bool IsContainFormSectionType(int FormSectionTypeID)
        {
            DataView dv = new DataView(this);
            dv.RowFilter = "ISNULL(" + OrderDetailTable.FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionTypeID.ToString();

            return (dv.Count > 0);
               
        }


        public float CommonProfitRate
        {
            get
            {
                
                DataTable dTbl = new DataTable("CommonProfitRate");
                DataColumnCollection columns = dTbl.Columns;
                columns.Add(FLD_PROFIT_RATE, typeof(System.Single)).DefaultValue = 0;
                columns.Add("profit_rate_count", typeof(System.Int32)).DefaultValue = 0;
                DataView dvRate = new DataView(dTbl);
                dvRate.Sort = FLD_PROFIT_RATE;
                float common_profit_rate = 0;
                foreach (DataRow row in this.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (!row.IsNull(FLD_PROFIT_RATE))
                        {
                            float profit_rate = 0;
                            profit_rate = Convert.ToSingle(row[FLD_PROFIT_RATE]);

                            if (profit_rate > 0)
                            {
                                int iIndex = dvRate.Find(profit_rate);
                                if (iIndex == -1)
                                {
                                    DataRow newRow = dTbl.NewRow();
                                    newRow[FLD_PROFIT_RATE] = profit_rate;
                                    newRow[FLD_PROFIT_RATE_COUNT] = 1;
                                    dTbl.Rows.Add(newRow);
                                }
                                else
                                {
                                    DataRow existRow = dvRate[iIndex].Row;
                                    existRow[FLD_PROFIT_RATE] = profit_rate;
                                    existRow[FLD_PROFIT_RATE_COUNT] = Convert.ToInt32(existRow[FLD_PROFIT_RATE_COUNT]) + 1;
                                    
                                }
                                
                            }
                        }
                    }
                }
                //Last operation, by changing the filter we will see the most common
                if (dvRate.Count > 0)
                {
                    dvRate.Sort = FLD_PROFIT_RATE_COUNT + " ," + FLD_PROFIT_RATE;
                    common_profit_rate = Convert.ToSingle(dvRate[0][FLD_PROFIT_RATE]);
                }
                
                return common_profit_rate;
            }
        }

		public String CatalogItemCodeList
		{
			get
			{
				String sList = "";
				foreach (DataRow row in this.Rows)
				{
					if (row.RowState != DataRowState.Deleted)
					{
						if (!row.IsNull(OrderDetailTable.FLD_CATALOG_ITEM_CODE))
						{
							int qty = 0;
							if (!row.IsNull(OrderDetailTable.FLD_QUANTITY))
								qty = Convert.ToInt32(row[OrderDetailTable.FLD_QUANTITY]);							
							if (qty > 0)
								sList = sList + row[OrderDetailTable.FLD_CATALOG_ITEM_CODE].ToString() + "||";
						}
					}
				}
				return sList;
			}
		}

        public void CleanData()
        {
            //Remove detail with quantity 0
            bool IsFound = true;
            int iIndexFound = -1;
            DataView dv = new DataView(this);
            dv.Sort = FLD_QUANTITY;
            while (IsFound)
            {
                iIndexFound = dv.Find(0);
                if (iIndexFound == -1)
                {
                    IsFound = false;
                    break;
                }
                else
                {
                    dv[iIndexFound].Row.Delete();
                }
            }
        }
        
        

        private void InitClass() {	
			//
			// Create the Campaign table
			//
			this.TableName = TBL_ORDER_DETAILS;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
			//For the system, when PKID => 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....			

            columns.Add(FLD_ORDER_ID, typeof(System.Int32));
			columns.Add(FLD_CATALOG_ITEM_DETAIL_ID, typeof(System.Int32));
			columns.Add(FLD_CATALOG_ITEM_CODE, typeof(System.String));
			columns.Add(FLD_CATALOG_ITEM_NAME, typeof(System.String));
			columns.Add(FLD_CATALOG_ITEM_DESC, typeof(System.String));
			columns.Add(FLD_CATALOG_ITEM_NB_UNITS, typeof(System.Int32)).DefaultValue = 0;
			columns.Add(FLD_CATALOG_ITEM_DETAIL_PRICE, typeof(System.Decimal)).DefaultValue = 0;
            columns.Add(FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_SOURCE_ID, typeof(System.Int32));
			columns.Add(FLD_ORDER_STATUS_ID, typeof(System.Int32));
			columns.Add(FLD_STATUS_REASON_ID, typeof(System.Int32));	
			columns.Add(FLD_SHIPMENT_GROUP_ID, typeof(System.Int32));	
			columns.Add(FLD_QUANTITY, typeof(System.Int32)).DefaultValue = 0;
			columns.Add(FLD_ADJUSTMENT_QUANTITY, typeof(System.Int32)).DefaultValue = 0;
			columns.Add(FLD_PRICE, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_TAX_RATE, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_DISPLAY_ORDER, typeof(System.Int32));
            columns.Add(FLD_PROFIT_RATE, typeof(System.Single)).DefaultValue = 0;
            columns.Add(FLD_FORM_SECTION_TYPE_ID, typeof(System.Int32));
            columns.Add(FLD_FORM_SECTION_NUMBER, typeof(System.Int32));
            
			DataColumn colGrossAmount = columns.Add(FLD_GROSS_AMOUNT, typeof(System.Decimal));
			colGrossAmount.Expression = "ISNULL (" + FLD_PRICE + ",0) * ISNULL (" + FLD_QUANTITY + ",0)";

			DataColumn colAmount = columns.Add(FLD_AMOUNT, typeof(System.Decimal));
			colAmount.Expression = "ISNULL (" + FLD_PRICE + ",0) * (ISNULL (" + FLD_QUANTITY + ",0) + ISNULL (" + FLD_ADJUSTMENT_QUANTITY + ",0))";
			
			DataColumn colAdjAmount = columns.Add(FLD_ADJUSTMENT_AMOUNT, typeof(System.Decimal));
			colAdjAmount.Expression = "ISNULL (" + FLD_PRICE + ",0) * ISNULL (" + FLD_ADJUSTMENT_QUANTITY + ",0)";
			
			DataColumn colTaxAmount = columns.Add(FLD_TAX_AMOUNT, typeof(System.Decimal));
			colTaxAmount.Expression = "ISNULL (" + FLD_TAX_RATE + ",0) * ISNULL (" + FLD_AMOUNT + ",0)";			
			
			columns.Add(FLD_NB_DAY_LEAD_TIME, typeof(System.Int32));

            DataColumn colProfitRateCount = columns.Add(FLD_PROFIT_RATE_COUNT, typeof(System.Int32));
            colProfitRateCount.Expression = "Count(" + FLD_PROFIT_RATE + ")";

            //Profit Rate Calculation
            //School Price based on the catalog
            
            DataColumn colCatDetailSchoolPrice = columns.Add(FLD_CATALOG_ITEM_DETAIL_SCHOOL_PRICE, typeof(System.Decimal));
            colCatDetailSchoolPrice.Expression = "ISNULL (" + FLD_CATALOG_ITEM_DETAIL_PRICE + ",0) / (1 - ISNULL (" + FLD_CATALOG_ITEM_DETAIL_PROFIT_RATE + ",0))";
            //School Price based on the order_detail
            DataColumn colSchoolPrice = columns.Add(FLD_SCHOOL_PRICE, typeof(System.Decimal));
            colSchoolPrice.Expression = "ISNULL (" + FLD_PRICE + ",0) / (1 - ISNULL (" + FLD_PROFIT_RATE + ",0))";
            //Calculated Price base on profit
            DataColumn colCalcPrice = columns.Add(FLD_CALCULATED_PRICE, typeof(System.Decimal));
            colCalcPrice.Expression = "ISNULL (" + FLD_CATALOG_ITEM_DETAIL_SCHOOL_PRICE + ",0) * (1 - ISNULL (" + FLD_PROFIT_RATE + ",0))";			
			

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			columns.Add(FLD_PERSONALIZATION_ID,typeof(System.String));

			//Primary Key
			this.PrimaryKey = new DataColumn[] {this.Columns[FLD_PKID]};

            this.ColumnChanging += new DataColumnChangeEventHandler(OrderDetailTable_ColumnChanging);
            this.RowDeleting += new DataRowChangeEventHandler(OrderDetailTable_RowDeleting);
		}

        private void OrderDetailTable_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            //if (this.DataSet != null)
            //{
            //    OrderData dts = (OrderData)this.DataSet;
            //    DataRow row = e.Row;
            //    if (!row.IsNull(FLD_PKID))
            //    {
            //        int pkID = Convert.ToInt32(row[FLD_PKID]);
            //        if (dts.OrderDetailTax.Rows.Count > 0)
            //        {
            //            bool IsFound = true;
            //            int iIndexFound = -1;
            //            DataView dv = new DataView(dts.OrderDetailTax);
            //            dv.Sort = OrderDetailTaxTable.FLD_ORDER_DETAIL_ID;
            //            while (IsFound)
            //            {
            //                iIndexFound = dv.Find(pkID);
            //                if (iIndexFound == -1)
            //                {
            //                    IsFound = false;
            //                    break;
            //                }
            //                else
            //                {
            //                    dv[iIndexFound].Row.Delete();
            //                }
            //            }
            //            //foreach (DataRow ordTaxRow in dts.OrderDetailTax)
            //            //{
            //            //    if (ordTaxRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID].ToString() == pkID.ToString())
            //            //    {
            //            //        ordTaxRow.Delete();
            //            //    }
            //            //}

            //        }
            //    }
            //}
        }

        private void OrderDetailTable_ColumnChanging(object sender, DataColumnChangeEventArgs args)
        {
            if (args.Column == this.Columns[FLD_PKID])
            {
                if (PKColumnChanged != null)
                {
                    PKColumnChanged(sender, args);
                }
                //if (this.DataSet != null)
                //{
                //    OrderData dts = (OrderData)this.DataSet;

                //    if (args.ProposedValue.ToString().Length > 0)
                //    {
                //        int NewID = Convert.ToInt32(args.ProposedValue);
                //        int OldID = Convert.ToInt32(args.Row[args.Column]);
                //        if (dts.OrderDetailTax.Rows.Count > 0)
                //        {
                //            DataView dv = new DataView(dts.OrderDetailTax);
                //            dv.RowStateFilter = DataViewRowState.Added;
                //            foreach (DataRowView drvRow in dv)
                //            {
                //                if (drvRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID].ToString() == OldID.ToString())
                //                {
                //                    drvRow[OrderDetailTaxTable.FLD_ORDER_DETAIL_ID] = NewID;
                //                }
                //            }

                //        }
                //    }
                //}
                
            }
        }


	}
}
