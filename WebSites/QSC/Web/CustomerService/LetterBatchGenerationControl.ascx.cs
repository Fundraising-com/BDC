namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Common.TableDef;
	using Business.Objects;
	using QSPFulfillment.CommonWeb;

	/// <summary>
	///		Summary description for ControlerGenerateSwitchLetter.
	/// </summary>
	public class LetterBatchGenerationControl : CustomerServiceControl
	{
		protected System.Web.UI.WebControls.Button btnPreview;
		protected System.Web.UI.WebControls.Button btnGenerate;
		protected System.Web.UI.WebControls.PlaceHolder plhLetterTemplateGenerationControl;
		protected QSPFulfillment.CustomerService.LetterTemplateSelectionDropDownList ddlLetterTemplateSelection;
		
		public event System.EventHandler LetterBatchCreated;

		private QSPFulfillment.CustomerService.LetterTemplateGenerationControl ctrlLetterTemplateGenerationControl;

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
			this.ddlLetterTemplateSelection.SelectedIndexChanged += new EventHandler(ddlLetterTemplateSelection_SelectedIndexChanged);
			this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			this.Page.PreLoad += new System.EventHandler(this.LetterBatchGenerationControl_PreLoad);
		}
		#endregion

		private void LetterBatchGenerationControl_PreLoad(object sender, EventArgs e)
		{
			try 
			{
				LoadLetterTemplateGenerationControl();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void ddlLetterTemplateSelection_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try 
			{
				SelectedTemplate = ddlLetterTemplateSelection.SelectedLetterTemplateItem;

				SwitchLetterTemplate();
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
			try
			{
				ctrlLetterTemplateGenerationControl.PreviewBatch();
			}
			catch(Exception ex)
			{
				this.Page.ManageError(ex);
			}
		}

		private void btnGenerate_Click(object sender, System.EventArgs e)
		{
			try
			{
				ctrlLetterTemplateGenerationControl.GenerateBatch();

				if(LetterBatchCreated != null) 
				{
					LetterBatchCreated(this, EventArgs.Empty);
				}
			}
			catch(Exception ex)
			{
				this.Page.ManageError(ex);
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

		#region Controls

		public virtual RSGeneration LetterBatchReportControl 
		{
			get 
			{
				RSGeneration letterBatchReportControl = null;

				if(ctrlLetterTemplateGenerationControl != null) 
				{
					letterBatchReportControl = ctrlLetterTemplateGenerationControl.LetterBatchReportControl;
				}

				return letterBatchReportControl;
			}
			set 
			{
				if(ctrlLetterTemplateGenerationControl != null) 
				{
					ctrlLetterTemplateGenerationControl.LetterBatchReportControl = value;
				}
			}
		}

		#endregion

		public void DataBindStatelessData()
		{
			ddlLetterTemplateSelection.DataBind();
		}

		private void LoadLetterTemplateGenerationControl() 
		{
			string path;
			
			if(SelectedTemplate != null) 
			{
				path = LetterTemplateGenerationControlFactory.Instance.GetLetterTemplateGenerationControlPath(SelectedTemplate);

				ctrlLetterTemplateGenerationControl = (LetterTemplateGenerationControl) LoadControl(path);
				ctrlLetterTemplateGenerationControl.ID = "ctrlLetterTemplateGenerationControl";

				this.plhLetterTemplateGenerationControl.Controls.Add(ctrlLetterTemplateGenerationControl);

				ctrlLetterTemplateGenerationControl.SelectedTemplate = SelectedTemplate;
			}
		}

		private void SwitchLetterTemplate() 
		{
			LetterTemplateGenerationControl oldLetterTemplateGenerationControl = ctrlLetterTemplateGenerationControl;
			LoadLetterTemplateGenerationControl();

			if(oldLetterTemplateGenerationControl != null) 
			{
				oldLetterTemplateGenerationControl.CopyTo(ctrlLetterTemplateGenerationControl);

				this.plhLetterTemplateGenerationControl.Controls.Remove(oldLetterTemplateGenerationControl);
			}
		}
	}
}
