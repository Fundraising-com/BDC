using System;
using System.Web.UI.WebControls;
using QSP.WebControl.DataAccess.Business;
using QSP.WebControl.DataAccess.Common.TableDef;
using QSP.WebControl.DataAccess.Common;
using System.ComponentModel;
using System.Data;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for DDLProvince.
	/// </summary>
	public class DropDownListProvince:InternalDropDownListSearch
	{
		private ProvinceBusiness busProvince;
		private ProvinceTable dtbProvince;

		[Bindable(true),Category("Data"),DefaultValue(CountryCode.All)]
		public CountryCode Code
		{
			get
			{
				CountryCode code = CountryCode.All;

				if(ViewState["CountryCode"] != null) 
				{
					code = (CountryCode) ViewState["CountryCode"];
				}

				return code;
			}
			set
			{
				ViewState["CountryCode"] = value;
			}
		}

		[Bindable(true),Category("Data"),DefaultValue(TaxRegion.All)]
		public TaxRegion TaxRegion 
		{
			get 
			{
				TaxRegion taxRegion = TaxRegion.All;

				if(ViewState["TaxRegion"] != null) 
				{
					taxRegion = (TaxRegion) ViewState["TaxRegion"];
				}

				return taxRegion;
			}
			set 
			{
				ViewState["TaxRegion"] = value;
			}
		}

		[Bindable(true),Category("Behavior"),DefaultValue("")]
		public string TextFirstRow
		{
			get
			{
				string textFirstRow = String.Empty;

				if(ViewState["TextFirstRow"] != null) 
				{
					textFirstRow = ViewState["TextFirstRow"].ToString();
				}

				return textFirstRow;
			}
			set
			{
				ViewState["TextFirstRow"] = value;
			}
		}

		[Bindable(true),Category("Behavior"),DefaultValue(true)]
		public bool AsTextFirstRow
		{
			get
			{
				bool hasTextFirstRow = true;

				if(ViewState["HasTextFirstRow"] != null) 
				{
					hasTextFirstRow = Convert.ToBoolean(ViewState["HasTextFirstRow"]);
				}

				return hasTextFirstRow;
			}
			set
			{
				ViewState["HasTextFirstRow"] = value;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			if(this.Items.Count == 0)
			{
				CreateControl();
				base.OnLoad (e);
			}
		}

		public override void DataBind()
		{
			CreateControl();
			base.DataBind ();
		}
		
		private void LoadData()
		{
			if(Code == CountryCode.All && TaxRegion == TaxRegion.All) 
			{
				busProvince.SelectAll(dtbProvince);
			}
			else if(TaxRegion == TaxRegion.All) 
			{
				busProvince.SelectByCountryCode(dtbProvince, Code);
			} 
			else 
			{
				busProvince.SelectByTaxRegion(dtbProvince, TaxRegion);
			}
		}
		
		private void SetDataField()
		{
			this.DataTextField = ProvinceTable.FLD_PROVINCE_NAME;
			this.DataValueField = ProvinceTable.FLD_PROVINCE_CODE;
		}

		private void SetDataSource()
		{
			this.DataSource = dtbProvince;
		}

		private void AddTextFirstRow()
		{
			DataRow row = this.dtbProvince.NewRow();
			row[ProvinceTable.FLD_PROVINCE_NAME] = TextFirstRow;
			row[ProvinceTable.FLD_PROVINCE_CODE] = "";
			dtbProvince.Rows.InsertAt(row,0);
		}
		private void CreateControl()
		{
			busProvince = new ProvinceBusiness();
			dtbProvince = new ProvinceTable();
			LoadData();
			SetDataField();
			SetDataSource();
			if(AsTextFirstRow)
				AddTextFirstRow();

			base.DataBind();
		}
		public void SetSelectedValue(string ProvinceCode)
		{
			
			for(int i=0; i < this.Items.Count; i++)
			{
				if(this.Items[i].Value ==ProvinceCode )
				{
					this.SelectedIndex = i;
					break;
				}
			}
		}
	}
}
