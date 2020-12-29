using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using GA.BDC.Core.EnterpriseComponents;

namespace GA.BDC.Core.Web.UI.InputControls
{
	/*
	protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
	protected System.Web.UI.WebControls.RangeValidator RangeValidator1;
	protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
	*/

	/// <summary>
	/// Summary description for DecimalTextBox.
	/// </summary>
	[DesignerAttribute(typeof(EmailAddressTextBoxDesigner), typeof(IDesigner))]
	[DefaultEvent("Click")]
	public class EmailAddressTextBox : BaseInputControl {
		public EmailAddressTextBox() {
			
		}

		protected override void LoadControl(object sender, EventArgs e) {
			base.LoadControl (sender, e);

			RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
			regularExpressionValidator.ValidationExpression = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
			regularExpressionValidator.ControlToValidate = "InnertTextBox";
			regularExpressionValidator.Text = "*";
			regularExpressionValidator.ErrorMessage = formatMessage;
			regularExpressionValidator.Display = ValidatorDisplay.Dynamic;

			Controls.Add(regularExpressionValidator);
		}
	}

	/// <summary>
	/// This class is used by the Visual Studio at Design Time.
	/// You can set control properties behavior in the ControlDesigner class.
	/// </summary>
	public class EmailAddressTextBoxDesigner : ControlDesigner {
		public EmailAddressTextBoxDesigner() {

		}

		// return the html display we want the UI control to see at design time
		public override string GetDesignTimeHtml() {
			return "<input type=\"text\" value=\"" + ID + "\" />";
		}
	}
}
