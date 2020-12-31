using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;



namespace QSPFulfillment.CustomerService.action
{


	/// <summary>
	///		Summary description for newsub.
	/// </summary>
	public partial class NewItem : NewItemControl
	{
	
		protected const string MSG_HEADER = "New item";
		protected NewItemStep2 ctrlNewItemStep2;
		protected NewItemStep1 ctrlNewItemStep1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		protected void Page_Render(object sender, System.EventArgs e)
		{
			if(this.Step1Completed)
			{
				ctrlNewItemStep2.Visible = true;
				this.Page.CancelButton.Visible = true;
				ctrlNewItemStep1.Visible= false;
				ctrlNewItemStep2.DataBind();
				this.Page.CommentTextBox.Visible = true;
				this.Page.CommentsLabel.Visible = true;
				ctrlNewItemStep2.SetConfirmButtonVisibility();
			}
			else
			{
				this.Page.CancelButton.Visible = false;
				this.Page.ConfirmButton.Visible = false;
				ctrlNewItemStep1.Visible= true;
				ctrlNewItemStep2.Visible = false;
				ctrlNewItemStep1.DataBind();
				this.Page.CommentTextBox.Visible = false;
				this.Page.CommentsLabel.Visible = false;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlNewItemStep1.SelectMagazineClicked += new SelectMagazineEventHandler(ctrlNewItemStep1_SelectMagazineClicked);
			this.ctrlNewItemStep2.BackClicked += new EventHandler(ctrlNewItemStep2_BackClicked);
			InitializeComponent();
			
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += 	new System.EventHandler(this.Page_Render);

		}
		#endregion
		
		private void ctrlNewItemStep1_SelectMagazineClicked(object sender, SelectMagazineClickedArgs e)
		{
			this.ctrlNewItemStep2.ProductInfo = e.MagazineInfo;
			this.Step1Completed = true;
		}

		private void ctrlNewItemStep2_BackClicked(object sender, EventArgs e)
		{
			this.Step1Completed = false;
		}

		protected override bool RaisesDoAction
		{
			get
			{
				return false;
			}
		}

		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}
	}
}
