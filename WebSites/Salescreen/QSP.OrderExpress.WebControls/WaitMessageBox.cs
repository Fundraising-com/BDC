using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for WaitMessageHandler.
	/// </summary>
	[ToolboxData("<{0}:WaitMessageBox runat=server></{0}:WaitMessageBox>")]
	public class WaitMessageBox : System.Web.UI.WebControls.WebControl
	{
		protected HtmlGenericControl WaitMessage = new HtmlGenericControl("IFRAME");
		//protected HtmlTable tblWaitMessage = new HtmlTable();
		//protected HtmlTableRow trWaitMessage = new HtmlTableRow();
		//protected HtmlTableCell tdWaitMessage = new HtmlTableCell();
		protected Label lblWaitMessage = new Label();

	

		protected Image imgWaitMessage = new Image();
        private const string FUNCTION_NAME = "ShowWatitingFrame";

        private const string SUFFIX_IFRAME_NAME = "_ifrWaitMessage";

		private const string ARRAY_NAME = "arrWaitMsgBox";
		private const string SUFFIX_BOX_NAME = "_divWaitMessage";
		private const string SUFFIX_LABEL_NAME = "_lblWaitMessage";
		private const string SUFFIX_IMG_NAME = "_imgWaitMessage";
		private int iTimeOut = 150;
		private string sText = "";//Please wait! The page is refreshing";
		private string sImageURL ="";
		private string sStyle="";
		private string sLayoutStyle = "";
		private string sTop = "0";
		
		public WaitMessageBox()
		{
			
		}

		private string GetArrayScript() 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script language=\"JavaScript\">			\n");
			sb.Append("		var " + ARRAY_NAME + " = new Array() \n");
			sb.Append("</script>");
			
			return sb.ToString();
		}

		private string GetAddToArrayScript() 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script language=\"JavaScript\">			\n");
			sb.Append("		var " + ARRAY_NAME + " = new Array() \n");
			sb.Append("		function AddElementToArray(contolID) {					\n");
			sb.Append("			var iIndex = 0; \n");
			sb.Append("			iIndex = " + ARRAY_NAME + ".length; \n");
			sb.Append("			" + ARRAY_NAME + "[iIndex] = contolID; \n");
			sb.Append("		}										\n");
			sb.Append("</script>");
			
			return sb.ToString();
		}

		private string GetAddControlToArrayScript() 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script language=\"JavaScript\">			\n");
			sb.Append("		AddElementToArray('" + WaitMessage.ClientID + "');	\n");
			sb.Append("</script>");
			
			return sb.ToString();
		}

		private string GetFunctionScript() 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script language=\"JavaScript\">								\n");
			sb.Append("		var WaitMsgTimeOutID = 0;								\n");
			sb.Append("		function " + FUNCTION_NAME + "() {						\n");
			sb.Append("			try	 {												\n");
			sb.Append("				for (i=0;i<" + ARRAY_NAME + ".length;i++) {		\n");
//			sb.Append("					alert('test');		\n");
			sb.Append("					var sID = " + ARRAY_NAME + "[i];		\n");
//			sb.Append("					alert(sID);		\n");
			sb.Append("					SetLayer(sID);		\n");
			sb.Append("					sID= sID.replace(/div/,'lbl');		\n");
//			sb.Append("					alert('WaitMsg (' + sID + ')');		\n");
			sb.Append("					WaitMsg(sID); \n");
			sb.Append("				}													\n");
//			sb.Append("				SetLayer('" + WaitMessage.ClientID + "');			\n");
			sb.Append("				WaitMsg('" + lblWaitMessage.ClientID  + "');		\n");
			sb.Append("			} catch(e) {										\n");
			sb.Append("				alert(e.message);				\n");
//			sb.Append("				alert('test catch');					\n");
			sb.Append("			}									\n");
			sb.Append("		}										\n");
			sb.Append("</script>");
			
			return sb.ToString();
		}

		private string GetLayerScript() 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script language=\"JavaScript\">			\n");
			sb.Append("		var WaitMsgTimeOutID = 0;			\n");
			sb.Append("		function SetLayer(whichLayer) {			\n");
//			sb.Append("			alert('SetLayer Begin Script');				\n");
			sb.Append("			var obj;							\n");
			sb.Append("			if (document.getElementById) {		\n");
			sb.Append("				obj = document.getElementById(whichLayer);	\n");
			sb.Append("			} else if (document.all) {				\n");
			sb.Append("				obj = document.all[whichLayer];		\n");
			sb.Append("			} else if (document.layers) {			\n");
			sb.Append("				obj = document.layers[whichLayer];	\n");
			sb.Append("			}										\n");
			sb.Append("			var style2 = obj.style;					\n");
			sb.Append("			if ((style2 != null) || (style2 != undefined)) \n");
			sb.Append("         {	\n");
			sb.Append("				if(style2.display == 'none')\n");
			sb.Append("				{\n");
			sb.Append("					style2.display = 'block';\n");
			sb.Append("					if (document.documentElement && document.documentElement.scrollTop)\n");
			sb.Append("					{\n");
			sb.Append("						style2.pixelTop = document.documentElement.scrollTop + " + sTop + ";\n");
            sb.Append("					}\n");
			sb.Append("					else \n");
			sb.Append("					{\n");
			sb.Append("						style2.pixelTop = document.body.scrollTop + " + sTop + ";\n");
            sb.Append("					}");
			sb.Append("				}\n");
			sb.Append("				else\n");
			sb.Append("				{\n");
			sb.Append("					style2.display = 'none'\n");
			sb.Append("				}\n");	
			sb.Append("			}										\n");
			//end of IF

//			sb.Append("				style2.display = style2.display? '':'block';	\n");
//			sb.Append("				if (style2.visibility == 'hidden')	\n");
//			sb.Append("					style2.visibility ='visible';		\n");
//			sb.Append("				else									\n");
//			sb.Append("					style2.visibility ='hidden';		\n");

			sb.Append("		}											\n");
			//end of SetLayer

			sb.Append("		var countOf = 0;							\n");
			sb.Append("		var cpt = 1;							\n");
			sb.Append("		function WaitMsg(controlID)   			\n");
			sb.Append("		{\n");
			sb.Append("			try	 								\n");
			sb.Append("			{\n");
//			sb.Append("			alert('WaitMsg Begin Script :' + controlID);				\n");
			sb.Append("				var objSpan = window.document.getElementById(controlID);	\n");
			sb.Append("				if ( (document.readyState == 'loading')  || (countOf < 5))  			\n");
			sb.Append("				{\n");
			
			sb.Append("					if(cpt == 5)   \n");
			sb.Append("					{\n");
			sb.Append("						if( objSpan.style.visibility == 'hidden'){ \n");
            sb.Append("							objSpan.style.visibility = 'visible';  \n");
            sb.Append("							objSpan.style.pixelTop = document.body.scrollTop + " + sTop + ";\n");
            sb.Append("						}\n");
			sb.Append("                     else{objSpan.style.visibility = 'hidden'\n");
			sb.Append("						}cpt = 1; \n");
			sb.Append("					} else 	{ \n");
			sb.Append("						cpt += 1;	\n");
            sb.Append("					}objSpan.style.pixelTop = document.body.scrollTop + " + sTop + ";\n");

			sb.Append("					var fctToRun = 'WaitMsg(\\\"' + controlID + '\\\")'; \n");
			sb.Append("					WaitMsgTimeOutID = window.setTimeout(fctToRun, " + iTimeOut + ");	\n");
			sb.Append("				} else { 			\n");
			sb.Append("					window.clearTimeout();	\n");
//			sb.Append("					window.clearTimeout(WaitMsgTimeOutID);	\n");
			//sb.Append("					alert('Test clearTimeout');				\n");
			sb.Append("					var sID = controlID.replace(/lbl/,'div');		\n");
			sb.Append("					objSpan.style.visibility = 'hidden'; ");
			sb.Append("					obj = document.getElementById(sID);\n");
			sb.Append("					obj.style.display = 'none';\n");
			sb.Append("					countOf = 0; cpt = 1;\n");
			//sb.Append("					//SetLayer(sID); \n");

			sb.Append("				}								\n");
			sb.Append("				countOf = countOf + 1;	\n");
			sb.Append("			} catch(e) {						\n");
			sb.Append("				alert(e.message);				\n");
			sb.Append("			}									\n");
			sb.Append("		}										\n");	
			sb.Append("</script>");
			
			return sb.ToString();
		}

		private string GetEventAssignmentScript() 
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("<script language=\"javascript\">\n");
			
			sb.Append("	function initPrePostBack (eventTarget, eventArgument) \n");
			sb.Append("	{\n");
			sb.Append("		try	 {								\n");
			sb.Append("			netPostBack = __doPostBack;\n");
			sb.Append("			if ((__doPostBack != null) || (__doPostBack != undefined)) {\n");
			sb.Append("			// replace __doPostBack with your own function\n");
			sb.Append("			__doPostBack = __doPrePostBack;\n");
			sb.Append("			}\n");
			sb.Append("		} catch(e) {						\n");
			sb.Append("				//alert(e.message);				\n");
			sb.Append("		}									\n");
			sb.Append("	}\n");	
			sb.Append("	\n");
			sb.Append("	function __doPrePostBack (eventTarget, eventArgument) \n");
			sb.Append("	{\n");
			sb.Append("		try	 {								\n");
			sb.Append("			// execute your own code before the page is submitted\n");
			sb.Append(			FUNCTION_NAME + "();\n");
			sb.Append("			\n");
			sb.Append("			// call base functionality\n");
			sb.Append("			\n");
			sb.Append("			if ((__doPostBack != null) || (__doPostBack != undefined)) \n");
			sb.Append("				return netPostBack (eventTarget, eventArgument);\n");
			sb.Append("			else		\n");
			sb.Append("				return; 	\n");
			sb.Append("		} catch(e) {						\n");
			sb.Append("				//alert(e.message);				\n");
			sb.Append("		}									\n");
			sb.Append("	}\n");		
			sb.Append("						\n");	
			sb.Append("	initPrePostBack();\n");
			//sb.Append("	//Call at StartUp to init and put invisble the Div\n");
			//sb.Append("	" + FUNCTION_NAME + "();\n");	
			//sb.Append("		InitLayer('" + WaitMessage.ClientID + "'); \n");
            sb.Append(" document.body.onbeforeunload=" + FUNCTION_NAME + ";		\n");
            sb.Append(" document.body.onscroll=Scroll();		\n");
			sb.Append("</script>\n");
			
			return sb.ToString();
		}

		protected override void OnLoad(EventArgs e) 
		{
			base.OnLoad(e);			
			CreateControl();			
	
		}

		private void CreateControl() 
		{
			string ss = "_" + this.ClientID;
			WaitMessage.TagName = "IFRAME";			
			WaitMessage.ID =  this.ID + SUFFIX_IFRAME_NAME;
			
			if (this.Visible)
			{
                WaitMessage.Attributes["height"] = this.Height.Value.ToString();
                WaitMessage.Attributes["height"] = this.Height.Value.ToString();
                WaitMessage.Attributes["frameborder"] = "no";
                WaitMessage.Attributes["src"] = "loading.aspx";
                WaitMessage.Attributes["scrolling"] = "no";
                /*
                height=156 frameborder=no src=loading.aspx scrolling=no 
                 */
                //Set Image Control
				if(this.ImageURL != String.Empty)
				{

					lblWaitMessage.Style.Add("display","none");
					imgWaitMessage.ImageUrl = this.ImageURL;
					//tdWaitMessage.Controls.Add(imgWaitMessage);
					WaitMessage.Controls.Add(imgWaitMessage);
				}

				//Set Label Control
				lblWaitMessage.Text = this.Text;
				lblWaitMessage.Attributes.Add("style",this.TextStyle);
				lblWaitMessage.CssClass = this.CssClass;
				//tdWaitMessage.Controls.Add(lblWaitMessage);
				WaitMessage.Controls.Add(lblWaitMessage);

				//Set DIV				
				//WaitMessage.Attributes.Add("class", this.CssClass);
				//WaitMessage.Attributes.Add("width", this.Width.ToString());

				WaitMessage.Attributes.Add("style",this.LayoutStyle);
				WaitMessage.Style.Add("display","none");

				this.Controls.Add(WaitMessage);
//				if (!Page.IsStartupScriptRegistered(ARRAY_NAME))
//					Page.RegisterClientScriptBlock(ARRAY_NAME, GetArrayScript());
				if (!Page.IsStartupScriptRegistered("GetAddToArrayScript"))
					Page.RegisterClientScriptBlock("GetAddToArrayScript", GetAddToArrayScript());
				if (!Page.IsStartupScriptRegistered(this.ID + "_GetAddControlToArrayScript"))
					Page.RegisterClientScriptBlock(this.ID + "_GetAddControlToArrayScript", GetAddControlToArrayScript());
				
                //if (!Page.IsStartupScriptRegistered(FUNCTION_NAME))
                //    Page.RegisterClientScriptBlock(FUNCTION_NAME, GetNetPostBackScript());
				if (!Page.IsStartupScriptRegistered("GetLayerScript"))
					Page.RegisterClientScriptBlock("GetLayerScript", GetLayerScript());
				if (!Page.IsStartupScriptRegistered("GetFunctionScript"))
					Page.RegisterClientScriptBlock("GetFunctionScript", GetFunctionScript());
				
				
				WaitMessage.Page.RegisterOnSubmitStatement(FUNCTION_NAME,FUNCTION_NAME + "();");
			}
			//this.Page.FindControl();
		}
		
		protected override void OnPreRender(EventArgs e) 
		{
			EnsureChildControls();

			base.OnPreRender(e);
		}

		public int TimeOut
		{
			get 
			{
				return iTimeOut;	
			}
			set
			{
				iTimeOut = value;	
			}
		}

		public string Text
		{
			get 
			{
				return sText;	
			}
			set
			{
				sText = value;	
			}
		}

		public string ImageURL
		{
			get{return sImageURL;}
			set{sImageURL = value;}
		}
		public string TextStyle
		{
			get{return sStyle;}
			set{sStyle = value;}
		}
		public string LayoutStyle
		{
			get{return sLayoutStyle;}
			set{sLayoutStyle = value;}
		}
		public string Top
		{
			get{return sTop;}
			set{sTop = value;}
		}
		

//		protected override void Render(HtmlTextWriter writer) 
//		{
//		}
	}
}
