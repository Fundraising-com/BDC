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
	public partial class NewSub : NewItemControl
	{
	
		protected const string MSG_HEADER = "New subscription";
		protected NewSubStep2 ctrlNewSubStep2;
		protected NewSubStep1 ctrlNewSubStep1;

		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		protected void Page_Render(object sender, System.EventArgs e)
		{
			if(this.Step1Completed)
			{
				ctrlNewSubStep2.Visible = true;
				this.Page.CancelButton.Visible = true;
				ctrlNewSubStep1.Visible= false;
				ctrlNewSubStep2.DataBind();
				this.Page.CommentTextBox.Visible = true;
				this.Page.CommentsLabel.Visible = true;
				ctrlNewSubStep2.SetConfirmButtonVisibility();
			}
			else
			{
				this.Page.CancelButton.Visible = false;
				this.Page.ConfirmButton.Visible = false;
				ctrlNewSubStep1.Visible= true;
				ctrlNewSubStep2.Visible = false;
				ctrlNewSubStep1.DataBind();
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
			this.ctrlNewSubStep1.SelectMagazineClicked += new SelectMagazineEventHandler(ctrlNewSubStep1_SelectMagazineClicked);
			this.ctrlNewSubStep2.BackClicked += new EventHandler(ctrlNewSubStep2_BackClicked);
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

		private void ctrlNewSubStep1_SelectMagazineClicked(object sender, SelectMagazineClickedArgs e)
		{
			this.ctrlNewSubStep2.MagazineInfo = e.MagazineInfo;
			this.Step1Completed = true;
		}

		private void ctrlNewSubStep2_BackClicked(object sender, EventArgs e)
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
