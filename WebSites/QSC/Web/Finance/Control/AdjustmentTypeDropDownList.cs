using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSP.WebControl;
using Business.Objects;
using Common.TableDef;

namespace QSPFulfillment.Finance.Control
{
	/// <summary>
	/// Summary description for AdjustmentTypeDropDownList.
	/// </summary>
	public class AdjustmentTypeDropDownList : DropDownListInteger
	{
		private const string CACHED_DATASOURCE_NAME = "AdjustmentTypeDataSet";

		private AdjustmentTypeDataSet AdjustmentTypeDataSet 
		{
			get 
			{
				return (AdjustmentTypeDataSet) DataSource;
			}
		}

		protected override bool EnableCache
		{
			get
			{
				return true;
			}
			set { }
		}

		protected override string CachedDataSourceName
		{
			get
			{
				return CACHED_DATASOURCE_NAME;
			}
			set { }
		}

		public override DateTime AbsoluteExpiration
		{
			get
			{
				return DateTime.Now.AddHours(12);
			}
			set { }
		}

		public override string DataMember
		{
			get
			{
				return AdjustmentTypeDataSet.ADJUSTMENT_TYPE.TableName;
			}
			set { }
		}

		public override string DataTextField
		{
			get
			{
				return AdjustmentTypeDataSet.ADJUSTMENT_TYPE.NAMEColumn.ColumnName;
			}
			set { }
		}

		public override string DataValueField
		{
			get
			{
				return AdjustmentTypeDataSet.ADJUSTMENT_TYPE.ADJUSTMENT_TYPE_IDColumn.ColumnName;
			}
			set { }
		}

		protected override object LoadCachedData()
		{
			AdjustmentType adjustmentType = new AdjustmentType();
			adjustmentType.GetAllForBatches();

			return adjustmentType.dataSet;
		}
	}
}
