namespace QSPFulfillment.CommonWeb.UC
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>Language Drop Down Control class</summary>
	public partial  class Language : System.Web.UI.UserControl
	{
		#region Page Initialization Stuff
		///<summary>AUTO GENERATED - modify at your own risk</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///<summary>AUTO GENERATED - modify at your own risk</summary>
		private void InitializeComponent()
		{
		}

		protected void Page_Load(object s, EventArgs e)
		{
		}

		private void OnInit_Handler(object s, EventArgs e)
		{
			if (this._Enabled_called == false)
			{
				this.Enabled = true;
			}
		}
		#endregion

		#region Item Declarations
		#endregion
		/// <summary>
		/// Lang is the culture code of the user that entered the info, as defined by RFC 1766. 
		/// See http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfSystemGlobalizationCultureInfoClassTopic.asp for more info.
		/// </summary>

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
				_Enabled		= value;
				_Enabled_called = true;
				DDL.Enabled		= _Enabled;
				if(_Enabled == true)
				{
					DDL.BackColor	= System.Drawing.Color.Transparent;
				}
				else
				{
					DDL.BackColor	= System.Drawing.Color.LightGray;
				}
				RequiredOrEnabled_ValueChanged();
			}
		}
		#endregion

			#region Required
		private bool _Required;
		
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
				rq_Language.Enabled = true;
			}
			else
			{
				rq_Language.Enabled = false;
			}
		}
		#endregion

			#region SelectedValue
		public string SelectedValue
		{
			get { return this.DDL.SelectedValue; }
			set 
			{ 
				string choice;
				string input = "";
				try   {input = value.ToLower().Trim(); }
				catch (NullReferenceException){}
				switch (input)
				{
					case "en":
					case "en-us":
					case "en-ca":
						choice = "en";
						break;
					case "fr":
					case "fr-ca":
						choice = "fr";
						break;
					default:
						//will trigger a System.ArgumentOutOfRangeException
						//when it is assigned to the ddl
						choice = value; 
						break;
				}
				this.DDL.SelectedValue = choice; 
			}
		}

		#endregion

		#endregion
	}
}
