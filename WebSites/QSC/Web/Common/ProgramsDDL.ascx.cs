namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Business.Objects;

	/// <summary>Drop down list of QSP Programs</summary>
	public partial  class ProgramsDDL : System.Web.UI.UserControl
	{
		private const string PROGRAMID = "ID";
		private const string PROGRAMNAME = "Name";

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
		//protected RegularExpressionValidator	reg_Programs;
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
				ddlPrograms.Enabled	= _Enabled;
				//reg_ddlPrograms.Enabled	= _Enabled;
				if(_Enabled == true)
				{
					ddlPrograms.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					ddlPrograms.BackColor	= System.Drawing.Color.LightGray;
				}
				RequiredOrEnabled_ValueChanged();
			}
		}
		#endregion

		#region Required
		private bool _Required;
		private bool _Required_called = false;
		
		///<summary>Gets or Sets a value indicating whether the Web server control will be subject to required field validation.</summary>
		public bool Required
		{
			get 
			{
				return _Required;
			}
			set
			{
				_Required = value;
				RequiredOrEnabled_ValueChanged();
			}
		}

		private void RequiredOrEnabled_ValueChanged()
		{
			if((_Required == true)&&(_Enabled == true))
			{
				rq_Programs.Enabled = true;
			}
			else
			{
				rq_Programs.Enabled = false;
			}
		}
		#endregion

		#region SelectedValue
		///<summary>Gets the selected value of the ProgramsDDL control.</summary>
		private int _Selected_Value = -5;
		public int SelectedValue
		{
			get 
			{ 
				if(this.ddlPrograms.SelectedValue == "")
					return -5;
				
				else
					return Convert.ToInt32(this.ddlPrograms.SelectedValue);
			}
			set 
			{
				_Selected_Value = value;
				if(this.ddlPrograms.Items.Count > 0)
				{
					this.ddlPrograms.SelectedValue = Convert.ToString(_Selected_Value);
				}
			}
		}

		///<summary>Gets the Abreviated name of the selected program value of the ProgramsDDL control.</summary>
		public string SelectedAbr
		{
			get 
			{ 
				string retval = "";
				switch(this.ddlPrograms.SelectedValue)
				{
					#region Switch contents
					case "1":
						retval = "Mag Reg";
						break;
					case "2":						retval = "Mag Exp";						break;					case "3":						retval = "Magnet";						break;					case "4":						retval = "Gift";						break;					case "5":						retval = "Easy as Pie";						break;					case "6":						//retval = "Prize Zone";						retval = "Pick-A-Prize";
						break;					case "7":						retval = "Reach";						break;					case "8":						retval = "Hershey";						break;					case "9":						retval = "Draw Prizes";						break;					case "10":						retval = "Choc Symphony";						break;					case "11":						retval = "Planetary";						break;					case "12":						retval = "Kanata";						break;					case "13":						retval = "Mag Combo";						break;					case "14":						retval = "Mag Staff";						break;					case "15":						retval = "Cum Rewards";						break;					case "16":						retval = "Chart Rewards";						break;					case "17":						retval = "Gift Card";						break;
					case "":
					default:
						retval = "";
						break;
					#endregion Switch contents
				}
				return retval;
			}
		}

		///<summary>Gets the Name of the selected program value of the ProgramsDDL control.</summary>
		public string SelectedName
		{
			get 
			{ 
				string retval = "";
				switch(this.ddlPrograms.SelectedValue)
				{
					#region Switch contents
					case "1":						retval = "Magazine";						break;
					case "2":						retval = "Magazine Express";						break;
					case "3":						retval = "Magnet";						break;
					case "4":						retval = "Gift";						break;
					case "5":						retval = "Easy as Pie";						break;
					case "6":						//retval = "Prize Zone";						retval = "Pick-A-Prize";
						break;
					case "7":						retval = "Reach for the stars";						break;
					case "8":						retval = "Hershey chocolate";						break;
					case "9":						retval = "Draw prize";						break;
					case "10":						retval = "Chocolate symphony program";						break;
					case "11":						retval = "Planetary Rewards Program";						break;
					case "12":						retval = "Kanata Extreme Rewards Program";						break;
					case "13":						retval = "Magazine Combo";						break;
					case "14":						retval = "Magazine Staff";						break;
					case "15":						retval = "Cumulative Rewards";						break;
					case "16":						retval = "Chart Rewards";						break;
					case "17":						retval = "Gift Card Coupon";						break;
					case "":
					default:
						retval = "";
						break;
						#endregion Switch contents
				}
				return retval;
			}
		}

		#endregion SelectedValue

		#region Mode
		private int _Mode;
		///<summary>Gets or sets the display mode, 1:names, 2:abreviations</summary>
		public int Mode
		{

			get { return _Mode; }
			set 
			{ 
				int input = value;
				if((value == 1)||(value == 2))
				{
					_Mode = value;
				}
				else
				{
					//programmer error
					//it is OK to throw an exception here,
					//in order to alert the programmer to their error
					//this exception should never be seen by end-users
					throw new System.ArgumentOutOfRangeException("The only recognized ProgramsDDL modes are 1 and 2");
				}
			}
		}
		#endregion Mode

		#region AllProgramsOption
		private bool _AllPrograms = false;
		///<summary>Gets or sets whether or not a "All programs" listitem should be displayed</summary>
		public bool AllProgramsOption
		{
			get { return this._AllPrograms;  } 
			set { this._AllPrograms = value; }
		}
		#endregion AllPrograms

		#region CssClass
		///<summary>Get or sets the Cascading Style Sheet (CSS) class rendered by the ProgramsDDL control on the client.</summary>
		public string CssClass
		{
			get { return this.ddlPrograms.CssClass;  }
			set { this.ddlPrograms.CssClass = value; }					  
		}
		#endregion CssClass

		#endregion Control properties

		#region logic calls
		public void Bind()
		{
			if(_Mode == 1)
			{
				//program names
				this.ddlPrograms.Items.Clear();

				CampaignProgram cap = new CampaignProgram(true, true);
				
				this.ddlPrograms.DataSource = cap.dataSet.Program;
				this.ddlPrograms.DataTextField = PROGRAMNAME;
				this.ddlPrograms.DataValueField = PROGRAMID;
				this.ddlPrograms.DataBind();
				
				this.ddlPrograms.Items.Insert(0, new ListItem("Select a Program", String.Empty));
				if(_AllPrograms)
				{
					this.ddlPrograms.Items.Insert(1, new ListItem("All Programs", "0"));
				}
				/*this.ddlPrograms.Items.Add(new ListItem("Magazine", "1"));
				this.ddlPrograms.Items.Add(new ListItem("Magazine Express", "2"));
				this.ddlPrograms.Items.Add(new ListItem("Magnet", "3"));
				this.ddlPrograms.Items.Add(new ListItem("Gift", "4"));
				this.ddlPrograms.Items.Add(new ListItem("Easy as Pie", "5"));
				this.ddlPrograms.Items.Add(new ListItem("Prize Zone", "6"));
				this.ddlPrograms.Items.Add(new ListItem("Reach for the stars", "7"));
				//this.ddlPrograms.Items.Add(new ListItem("Hershey chocolate", "8"));
				this.ddlPrograms.Items.Add(new ListItem("Draw prize", "9"));
				this.ddlPrograms.Items.Add(new ListItem("Chocolate symphony program", "10"));
				this.ddlPrograms.Items.Add(new ListItem("Planetary Rewards Program", "11"));
				this.ddlPrograms.Items.Add(new ListItem("Kanata Extreme Rewards Program", "12"));
				this.ddlPrograms.Items.Add(new ListItem("Magazine Combo", "13"));
				this.ddlPrograms.Items.Add(new ListItem("Magazine Staff", "14"));
				this.ddlPrograms.Items.Add(new ListItem("Cumulative Rewards", "15"));
				this.ddlPrograms.Items.Add(new ListItem("Chart Rewards", "16"));
				this.ddlPrograms.Items.Add(new ListItem("Gift Card Coupon", "17"));
				this.ddlPrograms.Items.Add(new ListItem("Discover Canada Program", "18"));
				//this.ddlPrograms.Items.Add(new ListItem("Gift Program Only", "19"));
				this.ddlPrograms.Items.Add(new ListItem("Sweet Sensations", "20"));*/
			}
			else if (_Mode == 2)
			{
				//program abreviations
				this.ddlPrograms.Items.Clear();
				this.ddlPrograms.Items.Add(new ListItem("Select a Program", String.Empty));
				if(_AllPrograms)
				{
					this.ddlPrograms.Items.Add(new ListItem("All Programs", "0"));
				}
				this.ddlPrograms.Items.Add(new ListItem("Mag Reg", "1"));
				this.ddlPrograms.Items.Add(new ListItem("Mag Exp", "2"));
				this.ddlPrograms.Items.Add(new ListItem("Magnet", "3"));
				this.ddlPrograms.Items.Add(new ListItem("Gift", "4"));
				this.ddlPrograms.Items.Add(new ListItem("Easy as Pie", "5"));
				this.ddlPrograms.Items.Add(new ListItem("Pick-A-Prize", "6"));
				this.ddlPrograms.Items.Add(new ListItem("Reach", "7"));
				//this.ddlPrograms.Items.Add(new ListItem("Hershey", "8"));
				this.ddlPrograms.Items.Add(new ListItem("Draw Prizes", "9"));
				this.ddlPrograms.Items.Add(new ListItem("Choc Symphony", "10"));
				this.ddlPrograms.Items.Add(new ListItem("Planetary", "11"));
				this.ddlPrograms.Items.Add(new ListItem("Kanata", "12"));
				this.ddlPrograms.Items.Add(new ListItem("Mag Combo", "13"));
				this.ddlPrograms.Items.Add(new ListItem("Mag Staff", "14"));
				this.ddlPrograms.Items.Add(new ListItem("Cum Rewards", "15"));
				this.ddlPrograms.Items.Add(new ListItem("Chart Rewards", "16"));
				this.ddlPrograms.Items.Add(new ListItem("Gift Card", "17"));
				this.ddlPrograms.Items.Add(new ListItem("Discover Canada", "18"));
				//this.ddlPrograms.Items.Add(new ListItem("Gift Program Only", "19"));
				this.ddlPrograms.Items.Add(new ListItem("Sweet Sensations", "20"));
			}

			//selected value
			if(_Selected_Value == -5)
			{
				this.ddlPrograms.SelectedValue = "";
			}
			else
			{
				this.ddlPrograms.SelectedValue = Convert.ToString(_Selected_Value);
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
			if (this._Required_called == false)
			{
				this.Required = true;
			}
		}
		#endregion Page_Load, OnInIt_Handler
	}
}

