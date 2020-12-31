namespace QSPFulfillment.AcctMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using Common;
	using Common.TableDef;
	using Business.Objects;

	/// <summary>
	///		Summary description for CampaignProgramControl.
	/// </summary>
	public partial class CampaignProgramMaintenanceControl : AcctMgtControl
	{
		protected System.Web.UI.WebControls.Label lblBrochureTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;

		private CampaignProgram cap;

		protected void CampaignProgramControl_Init(object sender, EventArgs e)
		{
			ProgramMaintenanceControl ctrlProgramMaintenanceControl;

			foreach(string ID in ProgramControlIDCollection) 
			{
				ctrlProgramMaintenanceControl = (ProgramMaintenanceControl) LoadControl("ProgramMaintenanceControl.ascx");
				ctrlProgramMaintenanceControl.ID = ID;
				this.plhProgramList.Controls.Add(ctrlProgramMaintenanceControl);
			}
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

      protected void CampaignProgramMaintenanceControl_PreRender(object sender, System.EventArgs e)
      {
         if (((QSPFulfillment.AcctMgt.Control.CampaignGeneralInformationControl)this.Parent.FindControl("ctrlCampaignGeneralInformationControl")).OnlineOnly)
            this.lblLandedTitle.Text = "Active";
         else
            this.lblLandedTitle.Text = "Landed";
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
			this.Init += new System.EventHandler(this.CampaignProgramControl_Init);
         this.PreRender += new System.EventHandler(this.CampaignProgramMaintenanceControl_PreRender);
		}
		#endregion

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

		public bool MagProgramRunning()
		{
			bool IsMagProgramRunning = false;
			ProgramMaintenanceControl ctrlProgMaintControl;

			LoadData();

			foreach(System.Web.UI.Control ctrl1 in this.plhProgramList.Controls) 
			{
				if(ctrl1 is ProgramMaintenanceControl) 
				{
				ctrlProgMaintControl = (ProgramMaintenanceControl) ctrl1;

				ctrlProgMaintControl.CampaignID = this.CampaignID;
				ctrlProgMaintControl.oCampaignProgram = oCampaignProgram;
				//ctrlProgMaintControl.Save();

                if ((ctrlProgMaintControl.ProgramID == 1 || ctrlProgMaintControl.ProgramID == 2 || ctrlProgMaintControl.ProgramID == 47) && ctrlProgMaintControl.IsLanded == true)
				{
					IsMagProgramRunning =true;
				}
				}
			}
			return IsMagProgramRunning;
		}

		private ArrayList ProgramControlIDCollection 
		{
			get 
			{
				if(Session[this.ClientID + "ProgramControlIDCollection"] == null)
					Session[this.ClientID + "ProgramControlIDCollection"] = new ArrayList();

				return (ArrayList) Session[this.ClientID + "ProgramControlIDCollection"];
			}
		}

		public override void DataBind()
		{
			LoadData();
			CreateControls();
		}

		private void LoadData() 
		{
			oCampaignProgram = new CampaignProgram(this.CampaignID, this.Page.CurrentMessageManager, this.Page.CurrentTransaction);
		}

		public void CreateControls() 
		{
			ProgramMaintenanceControl ctrlProgramMaintenanceControl;

			this.ProgramControlIDCollection.Clear();
			this.plhProgramList.Controls.Clear();

			foreach(CampaignProgramDataSet.ProgramRow row in oCampaignProgram.dataSet.Program.Rows) 
			{
				ctrlProgramMaintenanceControl = (ProgramMaintenanceControl) LoadControl("ProgramMaintenanceControl.ascx");
				this.plhProgramList.Controls.Add(ctrlProgramMaintenanceControl);
            ctrlProgramMaintenanceControl.ID = "ctrlProgramMaintenanceControl" + row.ID; // (this.plhProgramList.Controls.Count + 1);
				ctrlProgramMaintenanceControl.CampaignID = this.CampaignID;
				ctrlProgramMaintenanceControl.ProgramID = row.ID;
				ctrlProgramMaintenanceControl.oCampaignProgram = oCampaignProgram;
				ctrlProgramMaintenanceControl.IsStaffOrder = this.IsStaffOrder;
				ctrlProgramMaintenanceControl.DataBind();

				this.ProgramControlIDCollection.Add(ctrlProgramMaintenanceControl.ID);
			}
		}

		public void Save() 
		{
			ProgramMaintenanceControl ctrlProgramMaintenanceControl;

			LoadData();

			foreach(System.Web.UI.Control ctrl in this.plhProgramList.Controls) 
			{
				if(ctrl is ProgramMaintenanceControl) 
				{
					ctrlProgramMaintenanceControl = (ProgramMaintenanceControl) ctrl;

					ctrlProgramMaintenanceControl.CampaignID = this.CampaignID;
					ctrlProgramMaintenanceControl.oCampaignProgram = oCampaignProgram;
					ctrlProgramMaintenanceControl.Save();
				}
			}

			try 
			{
				oCampaignProgram.Validate();
			}
			// 03/26/2006 - Ben :	Tweak to save CA but not show errors before
			//						all is saved.
			catch(MessageException) { }

			oCampaignProgram.SaveWithoutValidation(this.CampaignID);
		}
	}
}
