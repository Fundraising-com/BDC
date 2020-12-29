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
using System.Collections;

namespace GA.BDC.Core.Web.UI.InputControls
{
	/// <summary>
	/// Summary description for BaseImputControl.
	/// </summary>
	public abstract class BaseInputControl : System.Web.UI.WebControls.WebControl, INamingContainer {
		protected string commandArgument;	// field that contains extra serialized information
		protected string cssClass;			// css class of the control
		protected bool nullable;			// let nullable
		protected string text;				// text of the control
		protected string requiredMessage = "";
		protected string formatMessage = "";
		protected int columns = 0;
		protected int maxLength = 0;
		private bool readOnly = false;
		private bool disableControl = false;
		//protected System.Web.UI.AttributeCollection attributes;

		// inner textbox control
		protected System.Web.UI.WebControls.TextBox innerTextBox = new System.Web.UI.WebControls.TextBox();

		public BaseInputControl() {
			//if(this.Page.IsPostBack) {
			//	string s = "dsadas";
			//}
		}
	
		// base control load
		protected virtual void LoadControl(object sender, System.EventArgs e) {
			if (innerTextBox == null)
				innerTextBox = new System.Web.UI.WebControls.TextBox();
			innerTextBox.CssClass = this.CssClass;
			innerTextBox.ID = "InnertTextBox";
			innerTextBox.Text = text;
			innerTextBox.Columns = columns;
			innerTextBox.ReadOnly = readOnly;
			innerTextBox.Enabled = this.Enabled;
			innerTextBox.Height = this.Height;

			if(Attributes != null) {
				IEnumerator keys = Attributes.Keys.GetEnumerator();

				while(keys.MoveNext()) {
					String key = (String)keys.Current;
					innerTextBox.Attributes.Add(key, Attributes[key]);
				}				
			}

			Controls.Add(innerTextBox);

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

		#region Web Form Designer generated code
		protected override void OnInit(EventArgs e) {
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		#region Init
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() { 
			this.Load += new System.EventHandler(LoadControl);
		}

		#endregion
		#endregion

		#region Public Attributes

		[Browsable(true),
		Category("Settings"),
		Description("Nullable.")]
		public bool Nullable {
			set { nullable = value; }
			get { return nullable; }
		}

		[Browsable(true),
		Category("Settings"),
		Description("Required Message.")]
		public string RequiredMessage {
			set { requiredMessage = value; }
			get { return requiredMessage; }
		}

		[Browsable(true),
		Category("Settings"),
		Description("Nullable.")]
		public string FormatMessage {
			set { formatMessage = value; }
			get { return formatMessage; }
		}

		[Browsable(true),
		Category("Settings"),
		Description("Text.")]
		public virtual string Text {
            set { 
				if(innerTextBox != null) {
					innerTextBox.Text = value;
				}
				text = value; 
			}
			get { return innerTextBox.Text; }
		}

		[Browsable(true),
		Category("Layout"),
		Description("Store serialized data")]
		public string CommandArgument {
			set { commandArgument = value; }
			get { return commandArgument; }
		}

		[Browsable(true),
		Category("Layout"),
		Description("Number of columns")]
		public int Columns {
			set { columns = value; }
			get { return columns; }
		}

		[Browsable(true),
		Category("Layout"),
		Description("Max Length")]
		public int MaxLength {
			set { maxLength = value; }
			get { return maxLength; }
		}

		[Browsable(true),
		Category("Layout"),
		Description("Read only box")]
		public bool ReadOnly {
			set { readOnly = value; }
			get { return readOnly; }
		}

//		[Browsable(true),
//		Category("Layout"),
//		Description("Disable box")]
//		public bool DisableControl {
//			set { disableControl = value; }
//			get { return disableControl; }
//		}

		/*
		[Browsable(true),
		Category("Layout"),
		Description("Attributes")]
		public new System.Web.UI.AttributeCollection Attributes {
			set { attributes = value; }
			get { return attributes; }
		}*/

		#endregion
	}
}
