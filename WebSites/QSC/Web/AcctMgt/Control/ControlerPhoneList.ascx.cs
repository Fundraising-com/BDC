namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common;

	///<summary>ControlerPhoneList</summary>
	public class ControlerPhoneList : CustomerService.CustomerServiceControlDataGrid
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e,dtgMain,lblMessage);
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			//this.dtgMain.ItemDataBound += new DataGridItemEventHandler(this.dtgMain_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion  auto-generated code

		#region Item Declarations
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected PhoneBusiness busPhone = new PhoneBusiness();
		private DataTable TableType;
		private PhoneTable Table = new PhoneTable();
		#endregion Item Declarations

		private void Page_Load(object s, EventArgs e)
		{
		}

		protected override void LoadData()
		{
			DataSource = Table;
			busPhone.SelectAll(DataSource,this.PhoneListID);
		}
		protected override void Insert(DataGridCommandEventArgs e)
		{
			DataRow row = Table.NewRow();
			GetValueInsert(e,row);
			Table.Rows.Add(row);
			this.busPhone.Insert(Table);
			NewIDInserted = Convert.ToInt32(row[PhoneTable.FLD_ID]);
		}
		protected override void Update(DataGridCommandEventArgs e)
		{
			this.busPhone.SelectOne(Table,GetPhoneID(e));

			if(Table.Rows.Count != 0)
			{
				GetValueUpdate(e,Table.Rows[0]);
				this.busPhone.Update(Table);
			}


		}

		public int PhoneListID
		{
			get
			{
				if(ViewState["PhoneListID"]== null)
					return 0;

				return Convert.ToInt32(ViewState["PhoneListID"]);
			}
			set
			{
				ViewState["PhoneListID"] = value;
			}
		}

//		private void dtgMain_ItemDataBound(object s, DataGridItemEventArgs e)
//		{
//
//			if(e.Item.ItemType == ListItemType.EditItem)
//			{
//				//SetValueDropDownList((DropDownList)e.Item.FindControl("ddlType"));
//				e.Item.DataBind();
//			}
//			if(e.Item.ItemType == ListItemType.Footer)
//			{
//
//				//SetValueDropDownList((DropDownList)e.Item.FindControl("ddlTypeInsert"));
//				e.Item.DataBind();
//			}
//		}
//
//		private void SetValueDropDownList(DropDownList ddl)
//		{
//			if(ddl.Items.Count == 0)
//			{
//				if(TableType == null)
//				{
//					LoadType();
//				}
//			}
//
//			DataRow dtrow = TableType.NewRow();
//			dtrow[CodeDetailTable.FLD_DESCRIPTION]= "Select";
//			dtrow[CodeDetailTable.FLD_INSTANCE] = "0";
//			TableType.Rows.InsertAt(dtrow,0);
//			foreach(DataRow row in TableType.Rows)
//			{
//				ddl.Items.Add(new ListItem(row[CodeDetailTable.FLD_DESCRIPTION].ToString(),row[CodeDetailTable.FLD_INSTANCE].ToString()));
//			}
//			//ddl.DataBind();
//		}

		private void LoadType()

		{
			try
			{
				TableType = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(TableType,30500);
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}

		}
		protected int GetIndex(string Value)
		{
			if(TableType == null)
				LoadType();

			int index =0;
			foreach(DataRow row in TableType.Rows)
			{
				if(row[CodeDetailTable.FLD_INSTANCE].ToString() == Value)
					return index;

				index++;

			}
			return 0;
		}
		private void GetValueUpdate(DataGridCommandEventArgs e,DataRow row)
		{
			Insert(row,PhoneTable.FLD_PHONENUMBER,GetPhoneNumberUpdate(e.Item));
			Insert(row,PhoneTable.FLD_BESTTIMETOCALL,GetBestTimeToCallUpdate(e.Item));
			Insert(row,PhoneTable.FLD_TYPE,GetTypeUpdate(e.Item));

		}
		private void GetValueInsert(DataGridCommandEventArgs e,DataRow row)
		{
			Insert(row,PhoneTable.FLD_PHONENUMBER,GetPhoneNumberInsert(e.Item));
			Insert(row,PhoneTable.FLD_BESTTIMETOCALL,GetBestTimeToCallInsert(e.Item));
			Insert(row,PhoneTable.FLD_TYPE,GetTypeInsert(e.Item));
			row[PhoneTable.FLD_PHONELISTID] = PhoneListID;
		}
		private int GetPhoneID(DataGridCommandEventArgs e)
		{
			return Convert.ToInt32(e.CommandArgument);
		}
		private int GetTypeUpdate(DataGridItem e)
		{
			return Convert.ToInt32(((DropDownList)e.FindControl("ddlType")).SelectedItem.Value);
		}
		private int GetTypeInsert(DataGridItem e)
		{
			return Convert.ToInt32(((DropDownList)e.FindControl("ddlTypeInsert")).SelectedItem.Value);
		}
		private string GetBestTimeToCallInsert(DataGridItem e)
		{
			return ((TextBox)e.FindControl("ctrlBestTimeInsert")).Text;
		}
		private string GetBestTimeToCallUpdate(DataGridItem e)
		{
			return ((TextBox)e.FindControl("ctrlBestTime")).Text;
		}
		private string GetPhoneNumberUpdate(DataGridItem e)
		{
			return ((QSPFulfillment.CommonWeb.UC.Phone)e.FindControl("ctrlPhone")).Text;
		}
		private string GetPhoneNumberInsert(DataGridItem e)
		{
			return ((QSPFulfillment.CommonWeb.UC.Phone)e.FindControl("ctrlPhoneInsert")).Text;
		}



	}

}
