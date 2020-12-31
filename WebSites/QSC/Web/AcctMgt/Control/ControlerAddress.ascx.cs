namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.TableDef;

	///<summary>ControlerAddress</summary>
	public partial class ControlerAddress : CustomerService.CustomerServiceControl
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{

		}
		#endregion auto-generated code

		#region Item Declarations

		private AddressBusiness BusAddress = new AddressBusiness();
		private AddressTable Table = new AddressTable();
		private DataTable TableType ;
		#endregion Item Declarations

		protected void Page_Load(object s, EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadData();

				if(Table.Rows.Count!=0)
				{
					SetValue();
				}

			}
		}


		private void LoadData()
		{

			DataSource = new AddressTable();
			this.BusAddress.SelectOne(Table,AddressID);
		}
		private void SetValue()
		{
			DataRow row= Table.Rows[0];
			SetValueType(row[AddressTable.FLD_ADDRESS_TYPE].ToString());
			this.tbxCity.Text = row[AddressTable.FLD_CITY].ToString();
			this.tbxPostalCode.Text = row[AddressTable.FLD_POSTAL_CODE].ToString();
			this.tbxStreet1.Text = row[AddressTable.FLD_STREET1].ToString();
			this.tbxStreet2.Text= row[AddressTable.FLD_STREET2].ToString();
			SetValueProvince(row[AddressTable.FLD_STATEPROVINCE].ToString());
			SetValueCountry(row[AddressTable.FLD_COUNTRY].ToString());
		}

		private void SetValueDropDownList()
		{
			if(ddlAddressType.Items.Count == 0)
			{
				TableType = new DataTable();
				this.Page.BusCodeDetail.SelectByCodeHeaderInstance(TableType,54000);

				DataRow dtrow = TableType.NewRow();
				dtrow[CodeDetailTable.FLD_DESCRIPTION]= "Select";
				dtrow[CodeDetailTable.FLD_INSTANCE] = "0";
				TableType.Rows.InsertAt(dtrow,0);
				foreach(DataRow row in TableType.Rows)
				{
					ddlAddressType.Items.Add(new ListItem(row[CodeDetailTable.FLD_DESCRIPTION].ToString(),row[CodeDetailTable.FLD_INSTANCE].ToString()));
				}
				ddlAddressType.DataBind();
			}
		}

		private void SetValueType(string Value)
		{
			//SetValueDropDownList();


			if((Value != "54005")&&(Value != "54006"))
			{
				this.ddlAddressType.SelectedValue = Value;
			}
			else if(Value == "54005")
			{
				//add supply address to the list
				//and lock it in so there can't be a choice
				this.ddlAddressType.Items.Clear();
				this.ddlAddressType.Items.Add(new ListItem("Supply Address", "54005"));
				//this.ddlAddressType.Enabled = false;
				this.ddlAddressType.BackColor = System.Drawing.Color.Transparent;
				this.ddlAddressType.ForeColor = System.Drawing.Color.Black;
				this.ddlAddressType.SelectedValue = "54005";
			}
			else if(Value == "54006")
			{
				//add contact address to the list
				//and lock it in so there can't be a choice
				this.ddlAddressType.Items.Clear();
				this.ddlAddressType.Items.Add(new ListItem("Contact Address", "54006"));
				//this.ddlAddressType.Enabled = false;
				this.ddlAddressType.BackColor = System.Drawing.Color.Transparent;
				this.ddlAddressType.ForeColor = System.Drawing.Color.Black;
				this.ddlAddressType.SelectedValue = "54006";
			}


		}

		private void SetValueProvince(string Value)
		{
			this.ddlProvince.SetSelectedValue(Value);
		}

		private void SetValueCountry(string Value)
		{
			this.Country = Value;
			//this.ddlCountry.Enabled = false;
			this.ddlCountry.BackColor = System.Drawing.Color.Transparent;
			this.ddlCountry.ForeColor = System.Drawing.Color.Black;
		}
		protected void btnSave_Click(object s, EventArgs e)
		{
			try
			{
				Update();
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private void Update()
		{
			this.BusAddress.SelectOne(Table,AddressID);
			DataRow row= Table.Rows[0];

			Insert(row,AddressTable.FLD_CITY,this.tbxCity.Text);
			Insert(row,AddressTable.FLD_POSTAL_CODE,this.tbxPostalCode.Text);
			Insert(row,AddressTable.FLD_COUNTRY,this.ddlCountry.SelectedItem.Value);
			Insert(row,AddressTable.FLD_ADDRESS_TYPE,AddressType);
			Insert(row,AddressTable.FLD_STREET1,this.tbxStreet1.Text);
			Insert(row,AddressTable.FLD_STREET2,this.tbxStreet2.Text);
			this.BusAddress.Update(Table);
		}
		public int AddressID
		{
			get
			{
				if(ViewState["AddressID"]== null)
					return 0;

				return Convert.ToInt32(ViewState["AddressID"]);
			}
			set
			{
				ViewState["AddressID"] = value;
			}
		}

		private int AddressType
		{
			get
			{
				return Convert.ToInt32(ddlAddressType.SelectedItem.Value);
			}
		}


		private string Country
		{
			get
			{
				return this.ddlCountry.SelectedItem.Value.ToString();
			}
			set
			{
				string country;
				string input = "";
				try   {input = value.ToLower().Trim(); }
				catch (NullReferenceException){}
				switch (input)
				{
					case "us":
					case "usa":
					case "united states":
					case "united states of america":
						country = "USA";
						break;
					case "ca":
					case "canada":
					default:
						country = "CA";
						break;
				}
				for(int i=0; i < ddlCountry.Items.Count; i++)
				{
					if(ddlCountry.Items[i].Value == country)
					{
						ddlCountry.SelectedIndex = i;
						break;
					}
				}
				//ddlCountry_SelectedIndexChanged(ddlCountry.SelectedValue);
			}
		}


	}
}