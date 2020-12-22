using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace QSP.Site.MyFeedback
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            SummaryLabel.Text = "";
		}

        protected void SubmitFeedbackButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    Feedback f = new Feedback();
                    f.Active = true;
                    f.Category = SubjectDropDownList.SelectedValue;
                    f.Message = MessageTextBox.Text.Trim();
                    f.CreateDate = DateTime.Now;
                    f.Name = NameTextBox.Text.Trim();
                    f.Email = EmailTextBox.Text.Trim();
                    f.Location = LocationTextBox.Text.Trim();
                    f.Phone = PhoneTextBox.Text.Trim();
                    f.Published = PublishCheckBox.Checked;
                    f.Save();

                    TopFeedbackGridView.DataBind();
                    TopFeedbackUpdatePanel.Update();
                    SummaryLabel.Text = "Thank you for your feedback!";

                    ResetInput();
                }
                catch (Exception ex) 
                { 
                    SummaryLabel.Text = ex.Message; 
                }
            }
        }

        private void ResetInput()
        {
            SubjectDropDownList.SelectedIndex = -1;
            MessageTextBox.Text = "";
            NameTextBox.Text = "";
            EmailTextBox.Text = "";
            LocationTextBox.Text = "";
            PhoneTextBox.Text = "";
            PublishCheckBox.Checked = true;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ResetInput();
        }

        protected void TopFeedbackTimer_Tick(object sender, EventArgs e)
        {
            TopFeedbackGridView.DataBind();
        }

        protected void RefreshTopFeedbackLinkButton_Click(object sender, EventArgs e)
        {
            TopFeedbackGridView.DataBind();
        }
	}
}
