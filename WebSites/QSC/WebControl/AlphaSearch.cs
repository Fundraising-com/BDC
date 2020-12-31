using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
[assembly: TagPrefix("AlphaSearch", "xxx")]	
namespace QSP.WebControl
{

	public delegate void AlphaSearchEventHandler(object sender,AlphaSearchClickedArgs e);
	

	/// <summary>
	/// Summary description for AlphaSearch.
	/// </summary>
	[Designer(typeof(QSP.WebControl.AlphaSearchDesigner)),DefaultProperty("Text"), 
		ToolboxData("<{0}:AlphaSearch runat=server></{0}:AlphaSearch>")]
	public class AlphaSearch : System.Web.UI.WebControls.WebControl,INamingContainer
	{
		public event AlphaSearchEventHandler OnClick;
        private string text;
		private char myCharSelected;
		public const string COMMANDNAME ="Select";
		string[] arrAlpha = {"#","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","ALL"};
		const char RETURNCARACALL = '%';
		const string ALL = "ALL";
		//private string sCssClassCurrent;
		private bool bIsEnabledOnClick;

		
		[Bindable(true),Category("Appearance"),DefaultValue("")] 
		public string Text 
		{
			get
			{
				return text;
			}

			set
			{
				text = value;
			}
		}
		override protected void OnInit( System.EventArgs e )
		{
			InitializeComponent();
			this.PreRender += new EventHandler(this.Page_PreRender);
			base.OnInit( e );
		}
		private void Page_PreRender(object sender,EventArgs e)
		{

			
				string SelectedChar = myCharSelected.ToString();

				if(SelectedChar == RETURNCARACALL.ToString())
					SelectedChar = ALL;

				foreach(Control cont in this.Controls)
				{
					LinkButton lbtn = (LinkButton)cont; 
					if(lbtn.Text != "&nbsp;")
                        lbtn.Enabled = Enabled;

					lbtn.Font.Underline = false;

					if(SelectedChar == lbtn.Text )
					{	
						if(bIsEnabledOnClick)
						{
							lbtn.Enabled = false;
						}
						else
						{
							lbtn.Font.Underline = true;
						}
					}
				}
			
		}
		private void InitializeComponent()
		{  
			LinkButton lbtn;
			LinkButton lbtnSpace;
			

		
			foreach(string ss in arrAlpha)
			{
				lbtn = new LinkButton();
				lbtn.CausesValidation = false;
				lbtn.CommandName= COMMANDNAME;
				lbtn.Text = ss;
				lbtn.Command += new CommandEventHandler(this.lbtn_Command);
				lbtn.CommandArgument = ss;
				lbtn.CssClass = this.CssClass;
				Controls.Add(lbtn);

				//Add space
				lbtnSpace = new LinkButton();
				lbtnSpace.Text = "&nbsp;";
				lbtnSpace.Enabled = false;
				Controls.Add(lbtnSpace);
			
			}
		}

		
		private void lbtn_Command(object sender,CommandEventArgs e)
		{
			if(e.CommandName == COMMANDNAME)
			{
			
				if(ALL == e.CommandArgument.ToString())
					myCharSelected = RETURNCARACALL;
				else
					myCharSelected = Convert.ToChar(e.CommandArgument);


				FireEvent();
			}
		}
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			output.Write(Text);
			base.Render(output);
			
		}
		private void FireEvent()
		{
			if(OnClick != null)
				OnClick(this,new AlphaSearchClickedArgs(myCharSelected));
		}
		[Bindable(true),Category("Appearance"),DefaultValue("true")] 
		public bool IsEnabledOnClick
		{
			get{return this.bIsEnabledOnClick;}
			set{bIsEnabledOnClick = value;}
		}

		
	}
}
