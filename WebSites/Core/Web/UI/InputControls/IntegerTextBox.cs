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
	[DesignerAttribute(typeof(IntegerTextBoxDesigner), typeof(IDesigner))]
	[DefaultEvent("Click")]
	public class IntegerTextBox : BaseInputControl 
	{
		protected int maxValue;	 //set a maximum value 

		public IntegerTextBox() 
		{
			
		}

		protected override void LoadControl(object sender, EventArgs e) 
		{
			base.LoadControl (sender, e);

			RangeValidator rangeValidator = new RangeValidator();
			rangeValidator.Display = ValidatorDisplay.Dynamic;
			rangeValidator.MinimumValue = int.MinValue.ToString();

			if (MaxValue > 0)
			{
               rangeValidator.MaximumValue = MaxValue.ToString();
			}
			else
			{
			   rangeValidator.MaximumValue = int.MaxValue.ToString();
			}
			
			
			rangeValidator.Type = ValidationDataType.Integer;
			rangeValidator.ControlToValidate = "InnertTextBox";
			rangeValidator.ErrorMessage = formatMessage;
			rangeValidator.ForeColor = Color.Red;
			rangeValidator.Text = "*";

			Controls.Add(rangeValidator);
		}

		[Browsable(true),
		Category("Settings"),
		Description("Max Value")]
		public int MaxValue 
		{
			set { maxValue = value; }
			get { return maxValue; }
		}
	}

	/// <summary>
	/// This class is used by the Visual Studio at Design Time.
	/// You can set control properties behavior in the ControlDesigner class.
	/// </summary>
	public class IntegerTextBoxDesigner : ControlDesigner 
	{
		public IntegerTextBoxDesigner() 
		{

		}

		// return the html display we want the UI control to see at design time
		public override string GetDesignTimeHtml() 
		{
			return "<input type=\"text\" value=\"" + ID + "\" />";
		}
	}


}
