//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_MenuBar_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'MenuBar.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for MenuBar.
    /// </summary>
    public partial class MenuBar : BaseMenuBar {

        private QSPForm.Business.AppItem c_MenuItem = QSPForm.Business.AppItem.Welcome;
        private int TextSize = 8;
        private int MinWidthMenu = 80;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
                MenuBarItem.Value = Convert.ToString(Convert.ToInt32(c_MenuItem));
            }
            else {
                c_MenuItem = Page.AppItem;
            }
            //string strJavaSc = "<script type='text/javascript' src='script/drag_and_drop.js'></script>";
            //this.Page.RegisterClientScriptBlock("DragAndDropFct", strJavaSc);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Load += new EventHandler(Page_Load);
            this.PreRender += new EventHandler(Page_PreRender);
            this.MenuBarItem.ServerChange += new EventHandler(MenuBarItem_ServerChange);
        }
        #endregion

        protected void Page_PreRender(object sender, System.EventArgs e) {
            MenuBarItem.Value = Convert.ToString(Convert.ToInt32(c_MenuItem));

            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");
            strBuild.Append("	function InitMenu() {	\n");
            strBuild.Append("		document.forms(0)." + this.MenuBarItem.ClientID + ".value = " + MenuBarItem.Value + "; \n");
            //strBuild.Append("	    if (DragAndDropInit != null) \n");
            //strBuild.Append("	        DragAndDropInit(); \n");
            strBuild.Append("}\n");
            strBuild.Append("	document.onload = InitMenu; \n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");
            this.Page.RegisterClientScriptBlock("InitMenu", strBuild.ToString());

            setJavascriptFunctionMenuPostBack();
        }

        protected void MenuBarItem_ServerChange(object sender, System.EventArgs e) {
            c_MenuItem = (QSPForm.Business.AppItem)Convert.ToInt32(MenuBarItem.Value);
            OnMenuChange(new System.ComponentModel.CancelEventArgs());	//Raising the event
        }

        protected override void OnDataBinding(EventArgs e) {
            base.OnDataBinding(e);

            setMenuBar(c_MenuItem);
        }

        override public QSPForm.Business.AppItem MenuItem {
            get {
                return c_MenuItem;
            }
            set {
                c_MenuItem = value;
            }
        }

        private void setMenuBar(QSPForm.Business.AppItem MenuItem) {
            if (Page.DisplayMenu)
                SetJavascriptForBuildDropDownMenu();
        }

        private void setJavascriptFunctionMenuPostBack() {
            StringBuilder strBuild = new StringBuilder();

            strBuild.Append("<script type='text/javascript'>  \n");

            strBuild.Append("	function MenuPostBack(Menu) { \n");
            strBuild.Append("		var oCtl \n");
            strBuild.Append("		var find \n");
            strBuild.Append("		var Resp \n");
            strBuild.Append("		var IsChange \n");
            strBuild.Append("		var IsValid \n");
            strBuild.Append("		IsValid = false; \n");
            strBuild.Append("		IsChange =false; \n");

            strBuild.Append("		for (i=1; i < document.forms(0).elements.length; i++){	 \n");
            strBuild.Append("			if (document.forms(0).elements[i].id.match(/MenuBarItem/)) { \n");
            strBuild.Append("				oCtl = document.forms(0).elements[i]; \n");
            strBuild.Append("				break;														\n");
            strBuild.Append("			}																\n");
            strBuild.Append("		}																	\n");
            strBuild.Append("		//Assign MenuItem Value												\n");
            strBuild.Append("		if (oCtl != null) {													\n");

            strBuild.Append("				if (document.forms(0) != 'undefined') {						 \n");
            strBuild.Append("					if (document.forms[0].hidChange != null) {				 \n");
            strBuild.Append("						if (document.forms(0).hidChange.value == '1') {		 \n");
            strBuild.Append("							IsChange =true;									 \n");
            strBuild.Append("						}													 \n");
            strBuild.Append("					}														 \n");

            strBuild.Append("					if (IsChange) {											 \n");
            strBuild.Append("						IsChange = confirm('Modification have been made, do you want to save it ??');  \n");
            strBuild.Append("					}														 \n");
            strBuild.Append("					if (IsChange) {											 \n");

            strBuild.Append("						try {												 \n");
            strBuild.Append("							Page_ClientValidate();							 \n");
            strBuild.Append("							if (Page_IsValid) {								 \n");
            strBuild.Append("								IsValid = true;								 \n");
            strBuild.Append("							} else {										 \n");
            strBuild.Append("								IsValid = false;							 \n");
            strBuild.Append("							}												 \n");
            strBuild.Append("						} catch (e) {										 \n");
            strBuild.Append("							IsValid = true;									 \n");
            strBuild.Append("						}													 \n");
            strBuild.Append("					} else {												 \n");
            strBuild.Append("						if (document.forms(0).hidChange != null) {			 \n");
            strBuild.Append("							document.forms(0).hidChange.value = '0';		 \n");
            strBuild.Append("						}													 \n");
            strBuild.Append("						IsValid = true;										 \n");
            strBuild.Append("					}														 \n");
            strBuild.Append("					if (IsValid) {											 \n");
            strBuild.Append("						oCtl.value = Menu;									 \n");

            strBuild.Append("						" + this.Page.GetPostBackClientEvent(this.MenuBarItem, "MenuChange") + ";							 \n");
            //strBuild.Append("						document.forms(0).submit();							 \n");
            strBuild.Append("					}														 \n");

            strBuild.Append("				}															 \n");

            strBuild.Append("		}																	 \n");
            strBuild.Append("	}																		 \n");

            strBuild.Append("</script>  \n");

            this.Page.RegisterClientScriptBlock("MenuPostBack", strBuild.ToString());
        }

        private void SetJavascriptForBuildDropDownMenu() {

            QSPForm.Business.ContentManagerSystem CMSys = new QSPForm.Business.ContentManagerSystem();
            AppItemData appItem;

            //Build the Javascript
            StringBuilder strBuild = new StringBuilder();
            int MenuItemCount = 0;

            //Retreive Menu for the current app			
            int UserID = this.Page.UserID;

            //Get Menu Item for this user (the list of the application available for him)
            appItem = CMSys.SelectAllMenuItemByRole(this.Page.Role); //SelectAllMenuItem();

            DataColumn dc = new DataColumn("NameLEN", System.Type.GetType("System.Int32"), "LEN(" + AppItemTable.FLD_NAME + ")");
            appItem.Tables[AppItemTable.TBL_APP_ITEM].Columns.Add(dc);

            DataView dvRoot = new DataView(appItem.Tables[AppItemTable.TBL_APP_ITEM]);
            dvRoot.RowFilter = AppItemTable.FLD_PARENT_ID + " IS NULL";
            dvRoot.Sort = AppItemTable.FLD_ORDER;

            if (dvRoot.Count > 0) {
                MenuItemCount = dvRoot.Count;

                //COLOR="#336699"
                strBuild.Append("<script language=javascript>\n");
                strBuild.Append("<!--			\n");
                strBuild.Append("var NoOffFirstLineMenus=" + MenuItemCount + ";			// Number of first level items\n");
                strBuild.Append("var LowBgColor='';					// Background color when mouse is not over\n");
                strBuild.Append("var LowSubBgColor='F3CC60';			// Background color when mouse is not over on subs\n");
                strBuild.Append("var HighBgColor='';					// Background color when mouse is over\n");
                strBuild.Append("var HighSubBgColor='F3CC60';		// Background color when mouse is over on subs\n");
                strBuild.Append("var FontLowColor='005596';			// Font color when mouse is not over\n");
                strBuild.Append("var FontSubLowColor='003366';		// Font color subs when mouse is not over\n");
                strBuild.Append("var FontHighColor='005596';			// Font color when mouse is over\n");
                strBuild.Append("var FontSubHighColor='336699';		// Font color subs when mouse is over\n");
                strBuild.Append("var BorderColor='';					// Border color\n");
                strBuild.Append("var BorderSubColor='993300';		// Border color for subs\n");
                strBuild.Append("var BorderWidth=1;					// Border width\n");
                strBuild.Append("var BorderBtwnElmnts=1;				// Border between elements 1 or 0\n");
                strBuild.Append("var FontFamily='arial, tahoma'		// Font family menu items\n");
                strBuild.Append("var FontSize=8;						// Font size menu items\n");
                strBuild.Append("var FontBold=1;						// Bold menu items 1 or 0\n");
                strBuild.Append("var FontItalic=0;					// Italic menu items 1 or 0\n");
                strBuild.Append("var MenuTextCentered='left';		// Item text position 'left', 'center' or 'right'\n");
                strBuild.Append("var MenuCentered='left';			// Menu horizontal position 'left', 'center' or 'right'\n");
                strBuild.Append("var MenuVerticalCentered='top';		// Menu vertical position 'top', 'middle','bottom' or static\n");
                strBuild.Append("var ChildOverlap=0;					// horizontal overlap child/ parent\n");
                strBuild.Append("var ChildVerticalOverlap=0;			// vertical overlap child/ parent\n");
                strBuild.Append("var StartTop=45;					// Menu offset x coordinate\n");
                strBuild.Append("var StartLeft=110;					// Menu offset y coordinate\n");
                strBuild.Append("var VerCorrect=0;					// Multiple frames y correction\n");
                strBuild.Append("var HorCorrect=0;					// Multiple frames x correction\n");
                strBuild.Append("var LeftPaddng=3;					// Left padding\n");
                strBuild.Append("var TopPaddng=2;					// Top padding\n");
                strBuild.Append("var FirstLineHorizontal=1;			// SET TO 1 FOR HORIZONTAL MENU, 0 FOR VERTICAL\n");
                strBuild.Append("var MenuFramesVertical=1;			// Frames in cols or rows 1 or 0\n");
                strBuild.Append("var DissapearDelay=1000;			// delay before menu folds in\n");
                strBuild.Append("var TakeOverBgColor=1;				// Menu frame takes over background color subitem frame\n");
                strBuild.Append("var FirstLineFrame='navig';			// Frame where first level appears\n");
                strBuild.Append("var SecLineFrame='space';			// Frame where sub levels appear\n");
                strBuild.Append("var DocTargetFrame='space';			// Frame where target documents appear\n");
                strBuild.Append("var TargetLoc='';					// span id for relative positioning\n");
                strBuild.Append("var HideTop=0;						// Hide first level when loading new document 1 or 0\n");
                strBuild.Append("var MenuWrap=0;						// enables/ disables menu wrap 1 or 0\n");
                strBuild.Append("var RightToLeft=0;					// enables/ disables right to left unfold 1 or 0\n");
                strBuild.Append("var UnfoldsOnClick=0;				// Level 1 unfolds onclick/ onmouseover\n");
                strBuild.Append("var WebMasterCheck=0;				// menu tree checking on or off 1 or 0\n");
                strBuild.Append("var ShowArrow=0;					// Uses arrow gifs when 1\n");
                strBuild.Append("var KeepHilite=1;					// Keep selected path highligthed\n");
                strBuild.Append("var Arrws=['" + ResolveUrl("images/tri.gif") + "',5,10,'" +
                    ResolveUrl("images/tridown.gif") + "',10,5,'" +
                    ResolveUrl("images/trileft.gif") + "',5,10];	// Arrow source, width and height\n");

                strBuild.Append("function BeforeStart(){return}\n");
                strBuild.Append("function AfterBuild(){return}\n");
                strBuild.Append("function BeforeFirstOpen(){return}\n");
                strBuild.Append("function AfterCloseAll(){return}\n");
                // Menu tree
                //	MenuX=new Array(Text to show, Link, background image (optional), number of sub elements, height, width);
                //	For rollover images set "Text to show" to:  "rollover:Image1.jpg:Image2.jpg"

                for (int iIndex = 0; iIndex < dvRoot.Count; iIndex++) {
                    DataRowView drvw = dvRoot[iIndex];
                    int iMenuID = Convert.ToInt32(drvw[AppItemTable.FLD_PKID]);

                    string sName = drvw[AppItemTable.FLD_NAME].ToString();
                    string NoMenu = Convert.ToString((iIndex + 1));
                    int lenMenu = sName.Length * TextSize;
                    if (lenMenu <= MinWidthMenu) {
                        lenMenu = MinWidthMenu;
                    }
                    //Find the number of child
                    DataView dvChilds = new DataView(appItem.Tables[AppItemTable.TBL_APP_ITEM]);
                    dvChilds.RowFilter = AppItemTable.FLD_PARENT_ID + " = " + iMenuID;
                    string sNumberOfChilds = dvChilds.Count.ToString();
                    //Find the max size for the sub menu
                    int MaxLen = 0;
                    if (dvChilds.Count > 0) {
                        foreach (DataRowView drvwc in dvChilds) {
                            if (MaxLen < Convert.ToInt32(drvwc["NameLEN"])) {
                                MaxLen = Convert.ToInt32(drvwc["NameLEN"]);
                            }
                        }
                    }
                    if (drvw[AppItemTable.FLD_NO].ToString().Length > 0) {
                        int NoAppItem = Convert.ToInt32(drvw[AppItemTable.FLD_NO]);
                        strBuild.Append("Menu" + NoMenu + "=new Array('" + sName + "','javascript:MenuPostBack(" + NoAppItem.ToString() + ");',''," + sNumberOfChilds + ",20," + lenMenu + ");\n");
                    }
                    else {
                        string url = drvw[AppItemTable.FLD_PAGE_URL].ToString();
                        if (url.Length == 0) {
                            url = "javascript:void(0);";
                        }
                        strBuild.Append("Menu" + NoMenu + "=new Array('" + sName + "','" + url + "',''," + sNumberOfChilds + ",20," + lenMenu + ");\n");
                    }
                    string strPrefixIndex = NoMenu + "_";
                    AddChildDropDownMenu(strBuild, strPrefixIndex, iMenuID, appItem, MaxLen);
                }

                //End of the script
                strBuild.Append("//-->\n");
                strBuild.Append("</script>");

                if (!this.Page.IsClientScriptBlockRegistered("Menu_definition")) {
                    String strJavaSc = "";
                    if (Page.SmartNavigation)
                        strJavaSc = "<script type='text/javascript' src='" + this.ResolveUrl("script/menu_com_smart.js") + "'></script>";
                    else
                        strJavaSc = "<script type='text/javascript' src='" + this.ResolveUrl("script/menu_com.js") + "'></script>";
                    strBuild.Append(strJavaSc);
                    this.Page.RegisterClientScriptBlock("Menu_definition", strBuild.ToString());
                    //this.Page.RegisterClientScriptBlock("Menu_Command",strJavaSc);
                }
            }
        }

        private void AddChildDropDownMenu(StringBuilder strBuild, String strPrefixIndex, int ParentMenuID, DataSet dts, int SubMenuWidth) {
            DataView dvParent = new DataView(dts.Tables[AppItemTable.TBL_APP_ITEM]);

            dvParent.RowFilter = AppItemTable.FLD_PARENT_ID + " = " + ParentMenuID;
            dvParent.Sort = AppItemTable.FLD_ORDER;
            if (dvParent.Count > 0) {

                for (int iIndex = 0; iIndex < dvParent.Count; iIndex++) {

                    DataRowView drvw = dvParent[iIndex];
                    int iMenuID = Convert.ToInt32(drvw[AppItemTable.FLD_PKID]);
                    string sName = drvw[AppItemTable.FLD_NAME].ToString();
                    string NoMenu = Convert.ToString((iIndex + 1));
                    NoMenu = strPrefixIndex + NoMenu;
                    int lenMenu = SubMenuWidth * (TextSize - 1);
                    if (lenMenu <= MinWidthMenu) {
                        lenMenu = MinWidthMenu;
                    }
                    DataView dvChilds = new DataView(dts.Tables[AppItemTable.TBL_APP_ITEM]);
                    dvChilds.RowFilter = AppItemTable.FLD_PARENT_ID + " = " + iMenuID;
                    dvChilds.Sort = AppItemTable.FLD_ORDER;
                    string sNumberOfChilds = dvChilds.Count.ToString();
                    int MaxLen = 0;
                    if (dvChilds.Count > 0) {
                        foreach (DataRowView drvwc in dvChilds) {
                            if (MaxLen < Convert.ToInt32(drvwc["NameLEN"])) {
                                MaxLen = Convert.ToInt32(drvwc["NameLEN"]);
                            }
                        }
                    }

                    if ((drvw[AppItemTable.FLD_NO].ToString().Length > 0) && (!Convert.ToBoolean(drvw[AppItemTable.FLD_OPEN_IN_NEW_PAGE]))) {
                        int NoAppItem = Convert.ToInt32(drvw[AppItemTable.FLD_NO]);
                        strBuild.Append("Menu" + NoMenu + "=new Array('" + sName + "','javascript:MenuPostBack(" + NoAppItem.ToString() + ");',''," + sNumberOfChilds + ",20," + lenMenu + ");\n");
                    }
                    else {
                        string url = drvw[AppItemTable.FLD_PAGE_URL].ToString();
                        if (url.Length == 0) {
                            url = "javascript:void(0);";
                        }
                        strBuild.Append("Menu" + NoMenu + "=new Array('" + sName + "','" + url + "',''," + sNumberOfChilds + ",20," + lenMenu + ");\n");
                    }
                    string strNewPrefixIndex = NoMenu + "_";
                    AddChildDropDownMenu(strBuild, strNewPrefixIndex, iMenuID, dts, MaxLen);
                }
            }
        }
    }
}