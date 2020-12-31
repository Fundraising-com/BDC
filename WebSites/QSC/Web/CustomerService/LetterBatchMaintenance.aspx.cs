using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business.Objects;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for GenerateSwitchLetter.
	/// </summary>
	public class LetterBatchMaintenance : CustomerServicePage
	{
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationLetterBatch;
		protected QSPFulfillment.CustomerService.LetterBatchGenerationControl ctrlLetterBatchGenerationControl;
		protected System.Web.UI.WebControls.PlaceHolder plhLetterBatchMaintenanceControl;
		protected System.Web.UI.WebControls.Label lblTitle;

		private QSPFulfillment.CustomerService.LetterBatchMaintenanceControl ctrlLetterBatchMaintenanceControl;

        private void Page_Init(object sender, System.EventArgs e)
        {
            try
            {
                ctrlLetterBatchGenerationControl.DataBindStatelessData();
            }
            catch (Exception ex)
            {
                ManageError(ex);
            }
        }
        
        private void Page_PreLoad(object sender, System.EventArgs e)
		{
			try 
			{
				LoadLetterBatchMaintenanceControl();

                ctrlLetterBatchGenerationControl.LetterBatchReportControl = rsGenerationLetterBatch;
            } 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private void Page_Load(object sender, EventArgs e)
		{
			try 
			{
				if(!IsPostBack) 
				{
					ctrlLetterBatchMaintenanceControl.DataBindInitialData();
					ctrlLetterBatchMaintenanceControl.DataBind();
				}
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlLetterBatchGenerationControl.LetterBatchCreated += new EventHandler(ctrlLetterBatchGenerationControl_LetterBatchCreated);
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Init += new System.EventHandler(this.Page_Init);
            this.Load += new System.EventHandler(this.Page_Load);
			this.PreLoad += new System.EventHandler(this.Page_PreLoad);
		}
		#endregion

		private void ctrlLetterBatchGenerationControl_LetterBatchCreated(object sender, EventArgs e)
		{
			try 
			{
				ctrlLetterBatchMaintenanceControl.DataBind();
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private void ctrlLetterBatchMaintenanceControl_SelectedTemplateChanged(object sender, SelectedTemplateChangedArgs e)
		{
			try 
			{
				SelectedTemplate = e.SelectedTemplate;
				SwitchLetterBatchMaintenanceControl();
			} 
			catch(Exception ex) 
			{
				ManageError(ex);
			}
		}

		private LetterTemplateItem SelectedTemplate 
		{
			get 
			{
				LetterTemplateItem selectedTemplate = null;

				if(ViewState["SelectedTemplate"] != null) 
				{
					selectedTemplate = (LetterTemplateItem) ViewState["SelectedTemplate"];
				}

				return selectedTemplate;
			}
			set 
			{
				ViewState["SelectedTemplate"] = value;
			}
		}

		private void LoadLetterBatchMaintenanceControl() 
		{
			string path = LetterBatchMaintenanceControlFactory.Instance.GetLetterBatchMaintenanceControlPath(SelectedTemplate);

			ctrlLetterBatchMaintenanceControl = (LetterBatchMaintenanceControl) LoadControl(path);
			ctrlLetterBatchMaintenanceControl.ID = "ctrlLetterBatchMaintenanceControl";
			this.plhLetterBatchMaintenanceControl.Controls.Add(ctrlLetterBatchMaintenanceControl);

			this.ctrlLetterBatchMaintenanceControl.SelectedTemplateChanged += new SelectedTemplateChangedEventHandler(ctrlLetterBatchMaintenanceControl_SelectedTemplateChanged);

			ctrlLetterBatchMaintenanceControl.LetterBatchReportControl = rsGenerationLetterBatch;
			ctrlLetterBatchMaintenanceControl.DataBindStatelessData();
		}

		private void SwitchLetterBatchMaintenanceControl() 
		{
			LetterBatchMaintenanceControl oldLetterBatchMaintenanceControl = ctrlLetterBatchMaintenanceControl;
			LoadLetterBatchMaintenanceControl();

			ctrlLetterBatchMaintenanceControl.DataBindInitialData();

			if(oldLetterBatchMaintenanceControl != null) 
			{
				oldLetterBatchMaintenanceControl.CopyTo(ctrlLetterBatchMaintenanceControl);

				this.plhLetterBatchMaintenanceControl.Controls.Remove(oldLetterBatchMaintenanceControl);
			}

			ctrlLetterBatchMaintenanceControl.DataBind();
		}
	}
}
