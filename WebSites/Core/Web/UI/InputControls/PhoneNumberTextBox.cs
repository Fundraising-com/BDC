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
	[DesignerAttribute(typeof(PhoneNumberTextBoxDesigner), typeof(IDesigner))]
	[DefaultEvent("Click")]
	public class PhoneNumberTextBox : BaseInputControl {
		public PhoneNumberTextBox() {
			
		}

		private IntegerTextBox CreateTextBox(string myID) {
			IntegerTextBox integerTextBox = new IntegerTextBox();
			//integerTextBox.CssClass = cssClass;
			integerTextBox.ID = myID;
			return integerTextBox;
		}

		private Label CreateLabel(string message, string css) {
			Label lbl = new Label();
			lbl.CssClass = css;
			lbl.Text = message;
			return lbl;
		}

		protected override void LoadControl(object sender, EventArgs e) {
			//base.LoadControl(sender, e);
			innerTextBox = new System.Web.UI.WebControls.TextBox();
			innerTextBox.CssClass = this.CssClass;
			innerTextBox.ID = "InnertTextBox";
			innerTextBox.Text = text;

            string myID = ClientID;

			string javascript = 
				"<script language=\"javascript\">\r\n" + 
				"function switchphone" + myID + "() {\r\n" +
                "	dest = \"" + myID + "_InnertTextBox\";\r\n" +
                "	src1 = document.getElementById(\"" + myID + "_phone1_InnertTextBox\").value;\r\n" +
                "	src2 = document.getElementById(\"" + myID + "_phone2_InnertTextBox\").value;\r\n" +
                "	src3 = document.getElementById(\"" + myID + "_phone3_InnertTextBox\").value;\r\n" +
                "	srcext = document.getElementById(\"" + myID + "_phoneExt_InnertTextBox\").value;\r\n" + 
				//"	document.getElementById(dest).value = \"(\" + src1 + \")\" + src2 + \"-\" + src3;\r\n" + 
				"	document.getElementById(dest).value = \"\" + src1 + \"-\" + src2 + \"-\" + src3;\r\n" + 
                "   if(document.getElementById(dest).value == '()-') {\r\n" +
                "       document.getElementById(dest).value = '';\r\n" + 
                "   }\r\n" +
				"}\r\n" + 
				"</script>\r\n";

			Page.RegisterClientScriptBlock(myID + "phone_number_text_box_script", javascript);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), myID + "phone_number_text_box_script", javascript);

			// make the inner textbox invisible
			innerTextBox.Style.Add("position", "absolute");
			innerTextBox.Style.Add("left", "-1000");
            innerTextBox.Style.Add("top", "-1000");
            innerTextBox.Style.Add("DISPLAY", "none");

			// adding the innertextbox
			Controls.Add(innerTextBox);

			// create the phone number
			IntegerTextBox phoneArea = CreateTextBox("phone1");
			IntegerTextBox phone1 = CreateTextBox("phone2");
			IntegerTextBox phone2 = CreateTextBox("phone3");
			IntegerTextBox phoneExt = CreateTextBox("phoneExt");

            phoneArea.Attributes.Add("onchange", "switchphone" + myID + "()");
            phone1.Attributes.Add("onchange", "switchphone" + myID + "()");
            phone2.Attributes.Add("onchange", "switchphone" + myID + "()");
            phoneExt.Attributes.Add("onchange", "switchphone" + myID + "()");

			// set the size of the boxes
			phoneArea.Columns = 1;
			phone1.Columns = 1;
			phone2.Columns = 2;
			phoneExt.Columns = 1;

			phoneArea.MaxLength = 3;
			phone1.MaxLength = 3;
			phone2.MaxLength = 4;
			phoneExt.MaxLength = 5;

            phoneArea.Nullable = Nullable;
            phone1.Nullable = Nullable;
            phone2.Nullable = Nullable;

			// make the extention field nullable
			phoneExt.Nullable = true;

			if(cssClass == null) {
				cssClass = "";
			}

			// add the phone number textboxes
			Controls.Add(phoneArea);
			Controls.Add(CreateLabel("-", cssClass));
			Controls.Add(phone1);
			Controls.Add(CreateLabel("-", cssClass));
			Controls.Add(phone2);
			Controls.Add(CreateLabel(" Ext:", cssClass));
			Controls.Add(phoneExt);

			if(Text != null) {
                if (Text != "") {
                    string sphone = "";
                   // sphone = "(" + PlacePhone(phoneArea, Text, 0, 3) + ")";
					sphone = PlacePhone(phoneArea, Text, 0, 3) + "-";
                    sphone += PlacePhone(phone1, Text, 3, 3) + "-";
                    sphone += PlacePhone(phone2, Text, 6, 4);
                    PlacePhone(phoneExt, Text, 10, -1);
                    innerTextBox.Text = sphone;
                    //innerTextBox.Text = "(" + phoneArea.Text + ")" + phone1.Text + "-" + phone2.Text;
                    // text = "(" + phoneArea.Text + ")" + phone1.Text + "-" + phone2.Text;
                }
			}


			RegularExpressionValidator regularExpressionValidator = new RegularExpressionValidator();
			regularExpressionValidator.ValidationExpression = @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}";
			regularExpressionValidator.ControlToValidate = "InnertTextBox";
			regularExpressionValidator.Text = "*";
			regularExpressionValidator.ErrorMessage = formatMessage;
			regularExpressionValidator.Display = ValidatorDisplay.Dynamic;

			Controls.Add(regularExpressionValidator);

			if(!nullable) {
				RequiredFieldValidator requiredField =
					new RequiredFieldValidator();
				requiredField.Display = ValidatorDisplay.Dynamic;
				requiredField.ControlToValidate = "InnertTextBox";
				requiredField.ErrorMessage = requiredMessage;
				requiredField.ForeColor = Color.Red;
				requiredField.Text = "*";

				Controls.Add(requiredField);
			}
		}

		private string PlacePhone(IntegerTextBox textBox, string phoneNumber, int start, int len) {
            string rstr = "";
			if(phoneNumber.Length > start) {
				if(len > 0) {
					if(phoneNumber.Length > start + len) {
						textBox.Text = phoneNumber.Substring(start, len);
                        rstr = phoneNumber.Substring(start, len);
					} else {
						textBox.Text = phoneNumber.Substring(start);
                        rstr = phoneNumber.Substring(start);
					}
				} else {
					textBox.Text = phoneNumber.Substring(start);
                    rstr = phoneNumber.Substring(start);
				}
			}
            return rstr;
		}

		private string ToNumeric(string val) {
			if(val == null) return "";

			string newVal = "";
			foreach(char c in val) {
				if(c >= '0' && c <= '9') {
					newVal += c;
				}
			}
			return newVal;
		}

		public override string Text {
			get {
				return base.Text;
			}
			set {
                if (value != null) {
                    base.Text = ToNumeric(value.ToString());
                } else {
                    base.text = "";
                }
			}
		}


		public int Extention {
			get { 
				IntegerTextBox phoneExt = (IntegerTextBox)FindControl("phoneExt");
				if(phoneExt.Text.Trim() != "") {
					return int.Parse(phoneExt.Text.Trim());
				}
				return int.MinValue;
			}
		}
	}

	/// <summary>
	/// This class is used by the Visual Studio at Design Time.
	/// You can set control properties behavior in the ControlDesigner class.
	/// </summary>
	public class PhoneNumberTextBoxDesigner : ControlDesigner {
		public PhoneNumberTextBoxDesigner() {

		}

		// return the html display we want the UI control to see at design time
		public override string GetDesignTimeHtml() {
			return "<input columns=3 type=\"text\" value=\"555\" size=\"1\"/>-" +
				"<input rows=3 type=\"text\" value=\"555\" size=\"1\" />-" +
				"<input type=\"text\" value=\"5555\" size=\"2\" /> Ext:" +
				"<input type=\"text\" value=\"555\" size=\"1\" />";
		}
	}
}
