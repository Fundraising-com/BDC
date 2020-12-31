namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Common;
	using Common.TableDef;
	using Business.Objects;

	/// <summary>
	///		Summary description for ProgramControl.
	/// </summary>
	public partial class ProgramMaintenanceControl : AcctMgtControl
	{

		private CampaignProgram cap;
		private CampaignProgramDataSet.CampaignProgramRow rowCampaignProgram;
		private CampaignProgramDataSet.ProgramRow rowProgram;

		public int CampaignID 
		{
			get 
			{
				if(this.ViewState["CampaignID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["CampaignID"]);
			}
			set 
			{
				this.ViewState["CampaignID"] = value;
			}
		}

		public int ProgramID 
		{
			get 
			{
				if(this.ViewState["ProgramID"] == null)
					return 0;

				return Convert.ToInt32(this.ViewState["ProgramID"]);
			}
			set 
			{
				this.ViewState["ProgramID"] = value;
			}
		}

		public CampaignProgram oCampaignProgram 
		{
			get 
			{
				return cap;
			}
			set 
			{
				cap = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			AddJavaScript();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Fields

		public bool IsLanded 
		{
			get 
			{
				return this.chkLanded.Checked;
			}
			set 
			{
				this.chkLanded.Checked = value;
			}
		}

		private double GroupProfit 
		{
			get 
			{
				double dGroupProfit = 0.0;

				try 
				{
               if (this.ddlGroupProfit.Visible)
					   dGroupProfit = Convert.ToDouble(this.ddlGroupProfit.SelectedValue);
               else
                  dGroupProfit = Convert.ToDouble(rowProgram.DefaultProfit);
				} 
				catch { }

				return dGroupProfit;
			}
			set 
			{
            this.ddlGroupProfit.SelectedIndex = this.ddlGroupProfit.Items.IndexOf(this.ddlGroupProfit.Items.FindByValue(value.ToString()));
         }
		}

        public bool FieldSupplyPacket
        {
           get
           {
              return this.chkFieldSupplyPacket.Checked;
           }
           set
           {
              this.chkFieldSupplyPacket.Checked = value;
           }
        }

        public bool AllowOnlineAccountDelivery
        {
           get
           {
              return this.chkAllowOnlineAccountDelivery.Checked;
           }
           set
           {
              this.chkAllowOnlineAccountDelivery.Checked = value;
           }
        }
		#endregion

		public bool IsStaffOrder 
		{
			get 
			{
				if(this.ViewState["IsStaffOrder"] == null)
					return false;

				return Convert.ToBoolean(this.ViewState["IsStaffOrder"]);
			}
			set 
			{
				this.ViewState["IsStaffOrder"] = value;
			}
		}

		#region JavaScript

		protected override void AddJavaScript()
		{
			base.AddJavaScript ();

			//this.chkLanded.Attributes["onclick"] = "SetProgramEnabled(event);";

		}

		#endregion

		public override void DataBind()
		{
			LoadData();
      }

		private void LoadData() 
		{
			DataRow[] rowCampaignPrograms;

			rowProgram = oCampaignProgram.dataSet.Program.FindByID(this.ProgramID);

			if(rowProgram != null) 
			{
				SetValueProgram();

				rowCampaignPrograms = rowProgram.GetChildRows(oCampaignProgram.dataSet.Relations[0]);

				if(rowCampaignPrograms.Length != 0) 
				{
					rowCampaignProgram = (CampaignProgramDataSet.CampaignProgramRow) rowCampaignPrograms[0];

					SetValueCampaignProgram();
				}
			}

         SetEnabledCampaignProgram();

		}

		private void SetValueProgram() 
		{
			this.lblProgramName.Text = rowProgram.Name;

         if (rowProgram.ID == 64)
         {
                if (this.ddlGroupProfit.Items.Count == 0)
                {
                    this.ddlGroupProfit.Items.Insert(0, new ListItem("50%", "50"));
                    this.ddlGroupProfit.Items.Insert(0, new ListItem("45%", "45"));
                    this.ddlGroupProfit.Items.Insert(0, new ListItem("40%", "40"));
                }
                this.ddlGroupProfit.Visible = true;
         }
         else if (rowProgram.ID == 61 || rowProgram.ID == 66)
         {
            if (this.ddlGroupProfit.Items.Count == 0)
            {
               this.ddlGroupProfit.Items.Insert(0, new ListItem("50%", "50"));
               this.ddlGroupProfit.Items.Insert(0, new ListItem("45%", "45"));
            }
            this.ddlGroupProfit.Visible = true;
         }
         else
         {
                this.ddlGroupProfit.Visible = false;
         }

         if (rowProgram.ID == 44)
         {
            this.chkFieldSupplyPacket.Visible = true;
         }
         else
         {
            this.chkFieldSupplyPacket.Visible = false;
         }

         if (rowProgram.ID == 44 || rowProgram.ID == 53 || rowProgram.ID == 54 || rowProgram.ID == 55 || rowProgram.ID == 56 || rowProgram.ID == 58 || rowProgram.ID == 59 || rowProgram.ID == 62 || rowProgram.ID == 64 || rowProgram.ID == 65 || rowProgram.ID == 67 || rowProgram.ID == 69 || rowProgram.ID == 70 || rowProgram.ID == 72 || rowProgram.ID == 73)
         {
            this.chkAllowOnlineAccountDelivery.Visible = true;
         }
         else
         {
            this.chkAllowOnlineAccountDelivery.Visible = false;
         }

		}

        private void SetValueCampaignProgram()
        {
            this.IsLanded = !rowCampaignProgram.OnlineOnly;
            this.FieldSupplyPacket = rowCampaignProgram.FieldSupplyPacket;
            this.AllowOnlineAccountDelivery = rowCampaignProgram.AllowOnlineAccountDelivery;
            this.GroupProfit = Convert.ToDouble(rowCampaignProgram.GroupProfit);
        }
        private void SetValueCampaignProgramEmpty() 
		{
         this.FieldSupplyPacket = false;
         this.AllowOnlineAccountDelivery = false;
         this.GroupProfit = 0.0;
		}

		private void SetEnabledCampaignProgram() 
		{
         /*if (rowProgram.ID == 44 || rowProgram.ID == 53)
         {
            if (this.chkLanded.Checked)
            {
               this.chkFieldSupplyPacket.Attributes.Remove("disabled");
            }
            else
            {
               this.chkFieldSupplyPacket.Attributes["disabled"] = "disabled";
            }
         }
         if (rowProgram.ID == 44)
         {
            this.chkAllowOnlineAccountDelivery.Attributes["disabled"] = "disabled";
         }

         if (rowProgram.ID == 53 || rowProgram.ID == 54 || rowProgram.ID == 55 || rowProgram.ID == 56 || rowProgram.ID == 58 || rowProgram.ID == 59 || rowProgram.ID == 62)
         {
            if (this.chkLanded.Checked)
            {
               this.chkAllowOnlineAccountDelivery.Attributes["disabled"] = "disabled";
            }
            else
            {
               this.chkAllowOnlineAccountDelivery.Attributes.Remove("disabled");
            }
         }*/
		}

		public void Save() 
		{
			DataRow[] rowCampaignPrograms;
			rowProgram = oCampaignProgram.dataSet.Program.FindByID(this.ProgramID);
				
			if(rowProgram != null) 
			{
				rowCampaignPrograms = rowProgram.GetChildRows(oCampaignProgram.dataSet.Relations[0]);

				if(rowCampaignPrograms.Length != 0) 
				{
					rowCampaignProgram = (CampaignProgramDataSet.CampaignProgramRow) rowCampaignPrograms[0];
				} 
				else 
				{
					rowCampaignProgram = null;
				}

            if (this.IsLanded || this.AllowOnlineAccountDelivery) 
				{
					if(rowCampaignProgram != null) 
					{
						FillCampaignProgramRow(rowCampaignProgram);
					}
					else 
					{
						rowCampaignProgram = oCampaignProgram.dataSet.CampaignProgram.NewCampaignProgramRow();

						rowCampaignProgram.CampaignID = this.CampaignID;
						rowCampaignProgram.ProgramRow = rowProgram;
						FillCampaignProgramRow(rowCampaignProgram);

						oCampaignProgram.dataSet.CampaignProgram.AddCampaignProgramRow(rowCampaignProgram);
					}
				} 
				else 
				{
					if(rowCampaignProgram != null) 
					{
						rowCampaignProgram.Delete();
					} 

					SetValueCampaignProgramEmpty();
				}
			}
		}

		private void FillCampaignProgramRow(CampaignProgramDataSet.CampaignProgramRow row) 
		{
         row.FieldSupplyPacket = this.FieldSupplyPacket;
         row.AllowOnlineAccountDelivery = this.AllowOnlineAccountDelivery;
         row.OnlineOnly = !this.IsLanded;
         row.GroupProfit = Convert.ToDecimal(this.GroupProfit);
        }
	}
}
