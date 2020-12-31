namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>Drop down list of FieldManagers</summary>
	public partial  class FieldManagerDDL : System.Web.UI.UserControl
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			this.Init += new System.EventHandler(this.OnInit_Handler);
		}
		#endregion auto-generated code

		#region Item Declarations
		//protected RequiredFieldValidator		rq_email;
		//protected RegularExpressionValidator	reg_email;
		#endregion Item Declarations

		#region Control properties
		
		#region Enabled
		private bool _Enabled;
		private bool _Enabled_called;

		///<summary>Gets or Sets a value indicating whether the Web server control is enabled.</summary>
		public bool Enabled
		{
			get
			{
				return _Enabled;
			}
			set
			{
				_Enabled = value;
				_Enabled_called = true;
				ddlFieldManager.Enabled	= _Enabled;
				//reg_email.Enabled	= _Enabled;
				if(_Enabled == true)
				{
					ddlFieldManager.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					ddlFieldManager.BackColor	= System.Drawing.Color.LightGray;
				}
				//RequiredOrEnabled_ValueChanged();
			}
		}
		#endregion

		#region Required
//		private bool _Required;
//		
//		///<summary>Gets or Sets a value indicating whether the Web server control will be subject to required field validation.</summary>
//		public bool Required
//		{
//			get 
//			{
//				return _Required;
//			}
//			set
//			{
//				_Required = value;
//				RequiredOrEnabled_ValueChanged();
//			}
//		}
//
//		private void RequiredOrEnabled_ValueChanged()
//		{
//			if((_Required == true)&&(_Enabled == true))
//			{
//				rq_email.Enabled = true;
//			}
//			else
//			{
//				rq_email.Enabled = false;
//			}
//		}
		#endregion

		#region Width 
		///<summary>Gets the width of the FieldManagerDDL control.</summary>
		///<remarks>Not sure what this was used for, commented it out and compilation still worked.</remarks>
//		public double Width
//		{
//			get { return this.ddlFieldManager.Width.Value;  }
//		}
		#endregion Width

		#region SelectedValue
		///<summary>Gets the selected value of the FieldManagerDDL control.</summary>
		public string SelectedValue
		{
			get { return this.ddlFieldManager.SelectedValue;  }
            set { this.ddlFieldManager.SelectedIndex = this.ddlFieldManager.Items.IndexOf(this.ddlFieldManager.Items.FindByValue(value)); }
		}
		#endregion SelectedValue

		#region CssClass
		///<summary>Get or sets the Cascading Style Sheet (CSS) class rendered by the ProgramsDDL control on the client.</summary>
		public string CssClass
		{
			get { return this.ddlFieldManager.CssClass;  }
			set { this.ddlFieldManager.CssClass = value; }					  
		}
		#endregion CssClass

		#endregion Control properties

		#region logic calls
		public void Bind(int mode)
		{
			this.Bind(mode, null);
		}

		public void Bind(int mode, string FMIDselected)
		{
			Business.FieldManagerList aFMlist = new Business.FieldManagerList();
			System.Collections.ArrayList AL = aFMlist.GetFMDropDownData(mode, FMIDselected);
			Common.QSPListItem QLI;
			ListItem LI;
			for(int i = 0; i < AL.Count; i++)
			{
				//convert to a true ListItem, add to DDL
				QLI = (Common.QSPListItem) AL[i];
				LI = new ListItem();
				LI.Selected = QLI.Selected;
				LI.Value    = QLI.Value;
				LI.Text     = QLI.Text;

				ddlFieldManager.Items.Add(LI);
			}
		}
		#endregion logic calls

		#region Page_Load, OnInIt_Handler
		protected void Page_Load(object s, EventArgs e)
		{
		}

		protected void OnInit_Handler(object s, EventArgs e)
		{
			if (this._Enabled_called == false)
			{
				this.Enabled = true;
			}
		}
		#endregion Page_Load, OnInIt_Handler
	}
}

