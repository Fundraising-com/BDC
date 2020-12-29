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
	[DesignerAttribute(typeof(DecimalTextBoxDesigner), typeof(IDesigner))]
	[DefaultEvent("Click")]
	public class DecimalTextBox : BaseInputControl {
		public DecimalTextBox() {
			
		}

		protected override void LoadControl(object sender, EventArgs e) {
			base.LoadControl (sender, e);

			RangeValidator rangeValidator = new RangeValidator();
			rangeValidator.Display = ValidatorDisplay.Dynamic;
			rangeValidator.MinimumValue = Decimal.MinValue.ToString();
			rangeValidator.MaximumValue = Decimal.MaxValue.ToString();
			rangeValidator.Type = ValidationDataType.Currency;
			rangeValidator.ControlToValidate = "InnertTextBox";
			rangeValidator.ErrorMessage = formatMessage;
			rangeValidator.ForeColor = Color.Red;
			rangeValidator.Text = "*";

			Controls.Add(rangeValidator);
		}

		public override string Text {
			get {
				return base.Text;
			}
			set {
				Decimal dec = Decimal.Parse(value.ToString());
				base.Text = dec.ToString("###,###,##0.00");
			}
		}

	}

	/// <summary>
	/// This class is used by the Visual Studio at Design Time.
	/// You can set control properties behavior in the ControlDesigner class.
	/// </summary>
	public class DecimalTextBoxDesigner : ControlDesigner {
		public DecimalTextBoxDesigner() {

		}

		/*
		// catch the event when a component change from the properties
		public override void OnComponentChanged(object obj, ComponentChangedEventArgs ce) {
			IsDirty = true;
			base.OnComponentChanged(obj, ce);
			base.UpdateDesignTimeHtml();
		}*/

		// return the html display we want the UI control to see at design time
		public override string GetDesignTimeHtml() {
			return "<input type=\"text\" value=\"" + ID + "\" />";
		}
	}
}
