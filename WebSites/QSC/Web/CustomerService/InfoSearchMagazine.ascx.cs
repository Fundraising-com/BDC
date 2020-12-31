using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPFulfillment.DataAccess;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for InfoSearchMagazine.
	/// </summary>
	public partial class InfoSearchMagazine : CustomerServiceControl,ISearch
	{
		protected System.Web.UI.WebControls.CompareValidator CompareValidator2;
		protected System.Web.UI.WebControls.Label Label12;
		protected string sJavaSrciptFunction = "SetTitleCode";

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			hypFindMagazine.DataBind();
		}
		protected void Page_Render(object sender,System.EventArgs e)
		{
			hypFindMagazine.Attributes.Add("onclick","javasrcipt:Open('Magazine.aspx?IsNewWindow=true&ID=true&Fct="+sJavaSrciptFunction+"');");

			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new System.EventHandler(this.Page_Render);

		}
		#endregion
		public ParameterValueList GetParameterValue(string StartParameterName)
		{

		
			ParameterValueList List = new ParameterValueList();
		
			AddParameterValue(this.Controls,List,StartParameterName);

			return List;

		}
		public string JavaSrciptFunction
		{
			get
			{
				return sJavaSrciptFunction;
			}
			set
			{
				sJavaSrciptFunction = value;
			}
		}
		protected override void AddJavaScript()
		{
			//AddEnabledInfoSearchMagazine();
			AddSetTitleCode();
		}
		private void AddSetTitleCode()
		{
			if(!this.Page.IsClientScriptBlockRegistered(sJavaSrciptFunction))
			{
				System.Text.StringBuilder SB = new System.Text.StringBuilder();
				SB.Append("<script language=\"javascript\">");
				SB.Append("function "+sJavaSrciptFunction+"(PCID,Description)");
				SB.Append("{");
				SB.Append("	var tbxTitleCode = document.getElementById('"+this.UniqueID.Replace("?","_")+"_tbxTitleCode');");
				SB.Append("	tbxTitleCode.value = PCID;");
				SB.Append("}");
				SB.Append("</script>");
				this.Page.RegisterClientScriptBlock(sJavaSrciptFunction,SB.ToString());
			}
		}
		/*private void AddEnabledInfoSearchMagazine()
		{
			if(!this.Page.IsClientScriptBlockRegistered("EnabledInfoSearchMagazine"))
			{
				System.Text.StringBuilder SB = new System.Text.StringBuilder();
				SB.Append("<script language=\"javascript\">");
				SB.Append("function EnabledInfoSearchMagazine(value)");
				SB.Append("{");
				SB.Append("alert('EnabledInfoSearchMagazine debut'+value);");
				SB.Append("	var tbxTitleCode = document.getElementById('"+this.tbxTitleCode.UniqueID.Replace("?","_")+"');");
				SB.Append("	tbxTitleCode.disabled = value;");
				SB.Append("	alert(tbxTitleCode.disabled);");
				SB.Append("	var hypFind = document.getElementById('"+this.hypFindMagazine.UniqueID.Replace("?","_")+"');");
				SB.Append("	hypFind.disabled = value;");
				SB.Append("alert('EnabledInfoSearchMagazine fin');");
				SB.Append("}");
				SB.Append("</script>");
				this.Page.RegisterClientScriptBlock("EnabledInfoSearchMagazine",SB.ToString());
			}
		}*/
		public bool Enabled
		{
			get
			{
				return this.tbxTitle.Enabled;
			}
			set
			{
				this.tbxTitle.Enabled = value;
				this.tbxTitleCode.Enabled = value;
				this.hypFindMagazine.Enabled = value;
				this.hypFindMagazine.DataBind();

				
			}
		}
		
	}
}
