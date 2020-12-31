using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.Design;
using System.Drawing.Design;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web;
using System.ComponentModel;
using System.Xml;
using System.Resources;
using System.Configuration;
using System.Globalization;


namespace skmMenu
{
	/// <summary>
	/// Represents the method that will handle the <see cref="Menu"/> class's <see cref="MenuItemClicked"/>
	/// event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="MenuItemClickEventArgs"/> that contains the event data.</param>
	public delegate void MenuItemClickedEventHandler(object sender, MenuItemClickEventArgs e);


	/// <summary>
	/// The menu class is an ASP.NET server control that displays a client-side menu utilizing
	/// CSS and DHTML.  Its contents are bound through an XML file or by programmatically constructing the
	/// Menu's menu items.  For full documentation, FAQs, examples, and a messageboard, be sure to check out the
	/// official skmMenu site: <a href="http://skmMenu.com/">skmMenu.com</a>.
	/// </summary>
	[
		DefaultProperty("ID"),
		ToolboxData("<{0}:Menu runat=server></{0}:Menu>"),
		Designer("skmMenu.Design.MenuDesigner"),
		ParseChildren(true),
		PersistChildren(false),
		DefaultEvent("MenuItemClick")
	 ]
	public class Menu : WebControl, INamingContainer, IPostBackEventHandler
	{

		#region Private Member Variables
		private ArrayList subItemsIds = new ArrayList();		// the list of submenu ids
		private object dataSource = null;						// the menu's datasource - used for databinding
		private MenuItemCollection items = new MenuItemCollection(); // the top-level menu
		private RoleCollection roles = new RoleCollection();	// the user roles
		private StringBuilder imagePreload;
		private int curzindex;
		
		// styles for the Menu, and unselected & selected menuitems...
		private TableItemStyle unselectedMenuItemStyle = new TableItemStyle();
		private TableItemStyle selectedMenuItemStyle = new TableItemStyle();
		#endregion

		#region MenuItemClick Event
		/// <summary>
		/// Occurs when a <see cref="MenuItem"/> associated with a command is clicked.
		/// </summary>
		public event MenuItemClickedEventHandler MenuItemClick;


		/// <summary>
		/// Raises the <see cref="MenuItemClick"/> event.  This allows you to provide a custom handler for the event.
		/// </summary>
		/// <param name="e">Instance of <see cref="MenuItemClickEventArgs"/> that contains the event data.</param>
		protected virtual void OnMenuItemClick(MenuItemClickEventArgs e)
		{
			if (MenuItemClick != null)
				MenuItemClick(this, e);
		}
		#endregion

		#region IStateManager Implementation
		/// <summary>
		/// SaveViewState saves the state of the menu into an object (specifically, an object array
		/// with five indices).  This is required to have the state persisted across postbacks.
		/// </summary>
		/// <returns>A five-element object array representing the menu's state.</returns>
		protected override object SaveViewState()
		{
			Object [] state = new object[5];
			state[0] = base.SaveViewState();
			state[1] = ((IStateManager) this.selectedMenuItemStyle).SaveViewState();
			state[2] = ((IStateManager) this.unselectedMenuItemStyle).SaveViewState();
			state[3] = ((IStateManager) this.items).SaveViewState();
			state[4] = ((IStateManager) this.roles).SaveViewState();

			return state;
		}


		/// <summary>
		/// Loads the state from the passed in saveState object.  This method runs during the
		/// page life-cycle, and is required for the menu to work across postbacks.
		/// </summary>
		/// <param name="savedState">The state persisted by SaveViewState() in the previous life-cycle.</param>
		protected override void LoadViewState(object savedState)
		{
			object [] state = null;

			if (savedState != null)
			{
				state = (object[]) savedState;

				base.LoadViewState(state[0]);
				((IStateManager) this.selectedMenuItemStyle).LoadViewState(state[1]);
				((IStateManager) this.unselectedMenuItemStyle).LoadViewState(state[2]);
				((IStateManager) this.items).LoadViewState(state[3]);
				((IStateManager) this.roles).LoadViewState(state[4]);
			}			
		}


		/// <summary>
		/// TrackViewState informs all of the menus complex properties that they, too, need to
		/// track their viewstate changes.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState ();
			
			if (this.items != null)
				((IStateManager) items).TrackViewState();
			
			if (this.selectedMenuItemStyle != null)
				((IStateManager) this.selectedMenuItemStyle).TrackViewState();

			if (this.unselectedMenuItemStyle != null)
				((IStateManager) this.unselectedMenuItemStyle).TrackViewState();

			if (this.roles != null)
				((IStateManager) this.roles).TrackViewState();
		}
		#endregion

		#region Overriden Control Methods
		/// <summary>
		/// The Render method is responsible for generating the HTML markup.
		/// </summary>
		/// <param name="writer">HTMLTextWriter instance to write to.</param>
		/// <remarks><b>Render</b> ensures that the Menu is created in a server-side Web Form.  This check is required
		/// because the Menu may contain <see cref="MenuItem"/> instances that, when clicked, cause a postback.</remarks>
		protected override void Render(HtmlTextWriter writer)
		{
			// Make sure that the menu is inside a Web Form...
			if (Page != null)
				Page.VerifyRenderingInServerForm(this);

			base.RenderChildren(writer);
		}


		/// <summary>
		/// This method is called from base.Render(), and starts the build menu process.
		/// </summary>
		protected override void CreateChildControls()
		{
			if (this.dataSource == null) 
			{
				// If not databound (i.e dynamic), traverse the entire menu
				// and set all of the MenuIDs
				for (int i = 0; i < this.Items.Count; i++)
				{
					BuildMenuID(this.Items[i], this.ClientID, i);
				}
			}
			BuildMenu();
		}


		/// <summary>
		/// Event handler for the DataBinding event.
		/// </summary>
		/// <remarks>This method runs when the DataBind() method is called.  Essentially, it clears out the
		/// current state and builds up the menu from the specified <see cref="DataSource"/>.
		/// </remarks>
		protected override void OnDataBinding(EventArgs e)
		{
			// Start by resetting the Control's state
			this.Controls.Clear();
			if (HasChildViewState)
				ClearChildViewState();

			// load the datasource either as a string or XmlDocuemnt
			XmlDocument xmlDoc = new XmlDocument();

			if (this.DataSource is String)
				// Load the XML document specified by DataSource as a filepath
				xmlDoc.Load((string) this.DataSource);
			else if (this.DataSource is XmlDocument)
				xmlDoc = (XmlDocument) DataSource;
			else
				return; // exit - nothing to databind

			// Clear out the MenuItems and build them according to the XmlDocument
			this.items.Clear();
			this.items = GatherMenuItems(xmlDoc.SelectSingleNode("/menu"), this.ClientID);
			BuildMenu();

			this.ChildControlsCreated = true;

			if (!IsTrackingViewState)
				TrackViewState();
		}


		/// <summary>
		/// Generates the client-side JavaScript.
		/// </summary><remarks>
		/// For non-databound (dynamic) menus, <b>OnPreRender</b> also sets all of the MenuIDs.
		/// <p />
		/// For more information on adding client-side script via an ASP.NET server control, refer to:
		///	<a href="http://msdn.microsoft.com/asp.net/default.aspx?pull=/library/en-us/dnaspp/html/aspnet-injectclientsidesc.asp">Injecting Client-Side Script from an ASP.NET Server Control</a>
		/// </remarks>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			this.RegisterClientScriptBlock();		// adds methods and global vars
			this.RegisterClientStartupScript();		// adds initialization code for each menu
			this.RegisterSubmenuArray();			// adds the skm_SubMenus array

			// Build Preload command to preload images (if any)
			this.imagePreload = new StringBuilder();
			for (int i = 0; i < this.Items.Count; i++)
			{
				BuildImagePreload(this.Items[i]);
			}
			this.RegisterPreloadCommand();			// adds the code to preload the images
		}

		/// <summary>
		/// Determines what images are used by a specified <see cref="MenuItem"/> instance.
		/// </summary>
		/// <param name="mi">The <b>MenuItem</b> to examine.</param>
		/// <remarks>The <see cref="MenuItem"/> class has a number of image properties, like <see cref="MouseOverImage"/>,
		/// <see cref="MouseDownImage"/> and others.  This method checks these properties to determine if any images
		/// are used.  If it locates any, it marks the image to be preloaded using client-side JavaScript.</remarks>
		protected virtual void BuildImagePreload(MenuItem mi)
		{
			if (mi.Image != String.Empty ) 
			{
				if (this.imagePreload.ToString() != String.Empty) 
				{
					this.imagePreload.Append(",");
				}
				this.imagePreload.Append("'" + mi.Image + "'");
			}
			if (mi.MouseOverImage != String.Empty ) 
			{
				if (this.imagePreload.ToString() != String.Empty) 
				{
					this.imagePreload.Append(",");
				}
				this.imagePreload.Append("'" + mi.MouseOverImage + "'");
			}
			if (mi.MouseDownImage != String.Empty ) 
			{
				if (this.imagePreload.ToString() != String.Empty) 
				{
					this.imagePreload.Append(",");
				}
				this.imagePreload.Append("'" + mi.MouseDownImage + "'");
			}
			if (mi.MouseUpImage != String.Empty ) 
			{
				if (this.imagePreload.ToString() != String.Empty) 
				{
					this.imagePreload.Append(",");
				}
				this.imagePreload.Append("'" + mi.MouseUpImage + "'");
			}
			if (mi.SubItems.Count > 0) 
			{
				for (int i = 0; i < mi.SubItems.Count; i++)
				{
					BuildImagePreload(mi.SubItems[i]);
				}
			}
		}


		/// <summary>
		/// Creates a <see cref="MenuID"/> for a <see cref="MenuItem"/>.
		/// </summary>
		/// <param name="mi">The <see cref="MenuItem"/> that will have its <b>MenuID</b> set.</param>
		/// <param name="parentID">The <b>MenuID</b> of the <b>MenuItem</b> <i>mi</i>'s parent.</param>
		/// <param name="indexValue">If <b>MenuItem</b> <i>mi</i> has a parent, the <b>indexValue</b> indicates
		/// what index <i>mi</i> is in its parent's set of <b>MenuItem</b> children.</param>
		/// <remarks><b>BuildMenuID()</b> formats the index as a three-digit number.  This puts an upperbound on the
		/// number of <b>MenuItems</b> any menu can contain.  Precisely, no single menu may contain more than 1,000
		/// <b>MenuItem</b>s.</remarks>
		protected virtual void BuildMenuID(MenuItem mi, string parentID, int indexValue)
		{
			mi.MenuID = parentID + "-menuItem" + indexValue.ToString("d3");	// to allow for more than 1,000 menu items per menu, change "d3" to "d4" ("d4" will allow for 10,000 MenuItems...)
			if (mi.SubItems.Count > 0) 
				for (int i = 0; i < mi.SubItems.Count; i++)
					BuildMenuID(mi.SubItems[i], mi.MenuID + "-subMenu", i);	// recurse through the mi's children...
		}
		#endregion


		#region Menu Creation Methods
		#region Class and URL Helper Functions
		/// <summary>
		/// Allows for two strings to be passed in - a default CSS class and a specific Menu class.  If no
		/// Menu class is specifies, the default class is returned.
		/// </summary>
		private string GetClass(string defaultclass, string menuclass)
		{
			if (menuclass != String.Empty)
				return menuclass;
			else
				return defaultclass;
		}

		/// <summary>
		/// Allows for three strings to be passed in - a default CSS class, a specific Menu class, and a style class.
		/// The proper class string is returned.
		/// </summary>
		private string GetClass(string defaultclass, string menuclass, string styleclass)
		{
			if (menuclass != String.Empty)
				return menuclass;
			else if (defaultclass != String.Empty)
				return defaultclass;
			else
				return styleclass;
		}


		/// <summary>
		/// This method utilizes <b>ResolveUrl()</b> if needed.  <b>ResolveUrl()</b> resolves any tildes that may
		/// appear in the URL into the proper path.  For more information see <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemwebuicontrolclassresolveurltopic.asp">http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemwebuicontrolclassresolveurltopic.asp</a>
		/// </summary>
		private string GetURL(bool resolveurl, string url)
		{
			if (this.DefaultResolveURL || resolveurl)
				return this.ResolveUrl(url);
			else
				return url;
		}
		#endregion

		/// <summary>
		/// BuildMenu builds the top-level menu.  It is called from the OnDataBinding method as well
		/// as from <see cref="CreateChildControls"/>.  It has code to check if the top-level menu should be
		/// laid out horizontally or vertically.
		/// </summary>
		protected virtual void BuildMenu()
		{
			string image = string.Empty;
			string mouseoverimage = string.Empty;
			string mousedownimage = string.Empty;
			string mouseupimage = string.Empty;
			CultureInfo enUSCulture = new CultureInfo("en-us", false);

			// iterate through the Items
			Table menu = new Table();
			menu.Attributes.Add("id", this.ClientID);
			menu.MergeStyle(this.ControlStyle);

			menu.CellPadding = ItemPadding;
			menu.CellSpacing = ItemSpacing;
			menu.GridLines = GridLines;

			// Add the Menu control's STYLE properties to the TABLE
			IEnumerator key = this.Style.Keys.GetEnumerator();
			while(key.MoveNext())
			{
				string k = key.Current.ToString();
				menu.Style.Add(k, this.Style[k]);
			}

			menu.Style.Remove("Z-INDEX");		// remove z-index added automatically by grid positioning

			// set the Z-INDEX
			menu.Style.Add("z-index", this.zIndex.ToString());
			curzindex = this.zIndex + 2;

			BuildOpacity(menu);
			
			TableRow tr = null;
			if (Layout == MenuLayout.Horizontal)
				tr = new TableRow();

			// Iterate through the top-level menu's menuitems, and add a <td> tag for each menuItem
			for (int i = 0; i < this.items.Count; i++)
			{
				MenuItem mi = this.items[i];

				// only render this MenuItem if it is visible and the user has permissions
				if (mi.Visible && UserHasPermission(mi)) 
				{ 
					if (Layout == MenuLayout.Vertical)
						tr = new TableRow();
					TableCell td = new TableCell();
					td.ApplyStyle(this.unselectedMenuItemStyle);
					// The style is overwritten by anything specifically set in menuitem
					if (mi.BackColor != Color.Empty) 
						td.BackColor = mi.BackColor;
					if (mi.Font != null)
						td.Font.CopyFrom(mi.Font);
					if (mi.ForeColor != Color.Empty)
						td.ForeColor = mi.ForeColor;
					if (mi.Height != Unit.Empty)
						td.Height = mi.Height;
					if (mi.Width != Unit.Empty)
						td.Width = mi.Width;
					if (mi.CssClass != String.Empty)
						td.CssClass = mi.CssClass;
					else if	(this.DefaultCssClass != String.Empty)
						td.CssClass = this.DefaultCssClass;
					if (mi.BorderColor != Color.Empty)
						td.BorderColor = mi.BorderColor;
					if (mi.BorderStyle != BorderStyle.NotSet)
						td.BorderStyle = mi.BorderStyle;
					if (mi.BorderWidth != Unit.Empty)
						td.BorderWidth = mi.BorderWidth;
					if (mi.HorizontalAlign != System.Web.UI.WebControls.HorizontalAlign.NotSet)
						td.HorizontalAlign = mi.HorizontalAlign;
					if (mi.VerticalAlign != System.Web.UI.WebControls.VerticalAlign.NotSet)
						td.VerticalAlign = mi.VerticalAlign;
					BuildOpacity(td);
					if (mi.Text != string.Empty)
						td.Text = mi.Text;  // Text
					else if (mi.Image != string.Empty) // Show Image
					{
						System.Web.UI.WebControls.Image cellimage = new System.Web.UI.WebControls.Image();

						cellimage.ImageUrl = mi.Image;
						cellimage.AlternateText = mi.ImageAltText;
						td.Controls.Add(cellimage);
						
						image = mi.Image;
						mouseoverimage = mi.MouseOverImage;
						mousedownimage = mi.MouseDownImage;
						mouseupimage = mi.MouseUpImage;
					}
					td.Attributes.Add("id", mi.MenuID);

					// Add in the left or right image as needed
					if (mi.LeftImage != String.Empty) 
					{
						System.Web.UI.WebControls.Image leftimage = new System.Web.UI.WebControls.Image();
						System.Web.UI.WebControls.Literal leftliteral = new System.Web.UI.WebControls.Literal();

						leftimage.ImageAlign = mi.LeftImageAlign;
						if (mi.LeftImageRightPadding != Unit.Empty)
							leftimage.Style.Add("margin-right",mi.LeftImageRightPadding.Value.ToString());
						leftimage.ImageUrl = mi.LeftImage;
						td.Controls.Add(leftimage);

						leftliteral.Text = td.Text;
						td.Controls.Add(leftliteral);
					} 
					else if (mi.RightImage != String.Empty) 
					{
						System.Web.UI.WebControls.Image rightimage = new System.Web.UI.WebControls.Image();
						System.Web.UI.WebControls.Literal rightliteral = new System.Web.UI.WebControls.Literal();
						
						rightliteral.Text = td.Text;
						td.Controls.Add(rightliteral);

						rightimage.ImageAlign = mi.RightImageAlign;
						if (mi.RightImageLeftPadding != Unit.Empty)
							rightimage.Style.Add("margin-left",mi.RightImageLeftPadding.Value.ToString());
						rightimage.ImageUrl = mi.RightImage;
						td.Controls.Add(rightimage);
					}

					// Prepare MouseOverCssClass
					string mouseover = String.Empty;

					if (this.DefaultMouseOverCssClass != string.Empty || mi.MouseOverCssClass != string.Empty || this.selectedMenuItemStyle.CssClass != String.Empty)
						mouseover = "this.className='" + GetClass(this.DefaultMouseOverCssClass, mi.MouseOverCssClass, this.selectedMenuItemStyle.CssClass) + "';";

					if (mi.Enabled) // if enabled...
					{
						// Generate OnClick handler
						if (mi.JavascriptCommand != String.Empty) // javascript command
							td.Attributes.Add("onclick", mi.JavascriptCommand + "skm_closeSubMenus(document.getElementById('" + this.ClientID + "'));");
						else if (mi.Url != String.Empty) 
						{
							if (mi.Target != String.Empty)
								td.Attributes.Add("onclick", "javascript:skm_closeSubMenus(document.getElementById('" + this.ClientID + "'));window.open('" + GetURL(mi.ResolveURL, mi.Url) + "','" + mi.Target + "');");
							else if (this.DefaultTarget != String.Empty)
								td.Attributes.Add("onclick", "javascript:skm_closeSubMenus(document.getElementById('" + this.ClientID + "'));window.open('" + GetURL(mi.ResolveURL, mi.Url) + "','" + this.DefaultTarget + "');");
							else
								td.Attributes.Add("onclick", "javascript:skm_closeSubMenus(document.getElementById('" + this.ClientID + "'));location.href='" + GetURL(mi.ResolveURL, mi.Url) + "';");
						}
						else if (mi.CommandName != String.Empty) // Must be postback action
							td.Attributes.Add("onclick", Page.GetPostBackClientHyperlink(this, mi.CommandName));
						else if (this.ClickToOpen) // Open submenu on click
							if (Layout == MenuLayout.Vertical)
								td.Attributes.Add("onclick", "javascript:skm_mousedOverMenu('" + this.ClientID + "',this, document.getElementById('" + this.ClientID + "'), true, '" + mouseoverimage + "');" + GenerateShimCall("true", mi.MenuID + "-subMenu") + mouseover);
							else
								td.Attributes.Add("onclick", "javascript:skm_mousedOverMenu('" + this.ClientID + "',this, document.getElementById('" + this.ClientID + "'), false, '" + mouseoverimage + "');" + GenerateShimCall("true", mi.MenuID + "-subMenu") + mouseover);
					}

					if (mi.Enabled)
					{
						// Output Tooltip
						if (mi.ToolTip != String.Empty) 
							td.ToolTip = mi.ToolTip;

						// Output MouseDownCssClass or MouseDownImage
						string mousedown = String.Empty;

						if (this.DefaultMouseDownCssClass != string.Empty || mi.MouseDownCssClass != string.Empty)
							mousedown = "this.className='" + GetClass(this.DefaultMouseDownCssClass, mi.MouseDownCssClass) + "';";

						if (mousedownimage != String.Empty)
							mousedown += "setimage(this, '" + mousedownimage + "');";

						if (mousedown != string.Empty)
							td.Attributes.Add("onmousedown", mousedown);

						// Output MouseUpCssClass or MouseUpImage
						string mouseup = String.Empty;

						if (this.DefaultMouseUpCssClass != string.Empty || mi.MouseUpCssClass != string.Empty)
							mouseup = "this.className='" + GetClass(this.DefaultMouseUpCssClass, mi.MouseUpCssClass) + "';";

						if (mouseupimage != String.Empty)
							mouseup += "setimage(this, '" + mouseupimage + "');";

						if (mouseup != string.Empty)
							td.Attributes.Add("onmouseup", mouseup);

						if (this.ClickToOpen == false) 
						{
							if (Layout == MenuLayout.Vertical)
								td.Attributes.Add("onmouseover", "javascript:skm_mousedOverMenu('" + this.ClientID + "',this, document.getElementById('" + this.ClientID + "'), true, '" + mouseoverimage + "');" + GenerateShimCall("true", mi.MenuID + "-subMenu") + mouseover);
							else
								td.Attributes.Add("onmouseover", "javascript:skm_mousedOverMenu('" + this.ClientID + "',this, document.getElementById('" + this.ClientID + "'), false, '" + mouseoverimage + "');" + GenerateShimCall("true", mi.MenuID + "-subMenu") + mouseover);
						} 
						else
							td.Attributes.Add("onmouseover", "javascript:skm_mousedOverClickToOpen('" + this.ClientID + "', this, document.getElementById('" + this.ClientID + "'), '" + mouseoverimage + "');"+ mouseover);
						
						td.Attributes.Add("onmouseout", "javascript:skm_mousedOutMenu('" + this.ClientID +"', this, '" + image + "');" + "this.className='" + GetClass(this.DefaultCssClass, mi.CssClass, this.unselectedMenuItemStyle.CssClass) + "';");
					} 
					else // disabled...
					{
						td.Attributes.Add("onmouseover", "javascript:skm_mousedOverSpacer('" + this.ClientID + "', this, document.getElementById('" + this.ClientID + "'));");
						td.Attributes.Add("onmouseout", "javascript:skm_mousedOutSpacer('" + this.ClientID + "', this);");
					}

					if (mi.Url != String.Empty || mi.CommandName != String.Empty)
						if (this.Cursor != MouseCursor.Default) 
						{
							if (this.Page.Request.Browser.Browser == "IE")
								td.Style.Add("cursor","hand"); 
							else
								td.Style.Add("cursor","pointer"); 
						}

					tr.Cells.Add(td);

					if (Layout == MenuLayout.Vertical)
						menu.Rows.Add(tr);

					// Add the subitems for this menu, if needed
					if (mi.SubItems.Count > 0)
					{
						// Create an IFrame (IE5.5 or better) to windowed form elements that might
						// interfere with display of the menu
						if (this.Page.Request.Browser.Browser == "IE" && Convert.ToDouble(this.Page.Request.Browser.Version, enUSCulture) >= 5.5) 
						{
							System.Web.UI.HtmlControls.HtmlGenericControl iframe = new System.Web.UI.HtmlControls.HtmlGenericControl();
							iframe.TagName = "iframe";
							iframe.Attributes.Add("id", "shim" + mi.MenuID + "-subMenu");
							iframe.Attributes.Add("src", IFrameSrc);
							iframe.Attributes.Add("scrolling", "no");
							iframe.Attributes.Add("frameborder", "no");
							iframe.Style.Add("position", "absolute");
							iframe.Style.Add("top", "0px");
							iframe.Style.Add("left", "0px");
							iframe.Style.Add("display", "none");
							BuildOpacity(iframe);
							Controls.Add(iframe);			
						}

						this.subItemsIds.Add(mi.MenuID + "-subMenu");
						AddMenu(mi.MenuID + "-subMenu", mi.SubItems);
					}
				}
			}

			if (Layout == MenuLayout.Horizontal)
				menu.Rows.Add(tr);

			Controls.Add(menu);
		}


		#region Opacity Adding Methods
		private void BuildOpacity(WebControl ctrl) 
		{
			CultureInfo enUSCulture = new CultureInfo("en-us", false);
			if (this.Opacity != null && this.Opacity != string.Empty) 
				if (Convert.ToInt32(this.Opacity) != 100) // If 100 don't output, 100=solid
					if (this.Page.Request.Browser.Browser == "IE" && Convert.ToDouble(this.Page.Request.Browser.Version, enUSCulture) >= 5.0)
						ctrl.Style.Add("filter", "alpha (opacity=" + this.Opacity + ")");
					else if (this.Page.Request.Browser.Browser == "Mozilla" || (this.Page.Request.Browser.Browser == "Netscape" && Convert.ToDouble(this.Page.Request.Browser.Version, enUSCulture) >= 6.0))
						ctrl.Style.Add("-moz-opacity", "." + this.Opacity);
		}


		private void BuildOpacity(System.Web.UI.HtmlControls.HtmlGenericControl ctrl) 
		{
			CultureInfo enUSCulture = new CultureInfo("en-us", false);
			if (this.Opacity != null && this.Opacity != string.Empty) 
				if (Convert.ToInt32(this.Opacity) != 100) // If 100 don't output, 100=solid
					if (this.Page.Request.Browser.Browser == "IE" && Convert.ToDouble(this.Page.Request.Browser.Version, enUSCulture) >= 5.0)
						ctrl.Style.Add("filter", "alpha (opacity=" + this.Opacity + ")");
					else if (this.Page.Request.Browser.Browser == "Mozilla" || (this.Page.Request.Browser.Browser == "Netscape" && Convert.ToDouble(this.Page.Request.Browser.Version, enUSCulture) >= 6.0))
						ctrl.Style.Add("-moz-opacity", "." + this.Opacity);
		}
		#endregion


		/// <summary>
		/// AddMenu is called recusively, doing a depth-first traversal of the menu hierarchy and building
		/// up the HTML elements from the object model.
		/// </summary>
		/// <param name="menuID">The ID of the parent menu.</param>
		/// <param name="myItems">The collection of menuitems.</param>
		protected virtual void AddMenu(string menuID, MenuItemCollection myItems)
		{
			string image = string.Empty;
			string mouseoverimage = string.Empty;
			string mousedownimage = string.Empty;
			string mouseupimage = string.Empty;
			CultureInfo enUSCulture = new CultureInfo("en-us", false);

			// iterate through the Items
			Table menu = new Table();

			menu.Attributes.Add("id", menuID);
			menu.Attributes.Add("style", "display: none;");
			// The style is overwritten by anthing specifically set in menuitem
			if (this.BackColor != Color.Empty)
				menu.BackColor = this.BackColor;
			if (this.Font != null)
				menu.Font.CopyFrom(this.Font);
			if (this.ForeColor != Color.Empty)
				menu.ForeColor = this.ForeColor;
			if (this.SubMenuCssClass != String.Empty) 
				menu.CssClass = this.SubMenuCssClass;
			else if (this.CssClass != String.Empty) // Use if SubMenuCssClass was blank
				menu.CssClass = this.CssClass;
			if (this.BorderColor != Color.Empty)
				menu.BorderColor = this.BorderColor;
			if (this.BorderStyle != BorderStyle.NotSet)
				menu.BorderStyle = this.BorderStyle;
			if (this.BorderWidth != Unit.Empty)
				menu.BorderWidth = this.BorderWidth;
			menu.CellPadding = ItemPadding;
			menu.CellSpacing = ItemSpacing;
			menu.GridLines = GridLines;
			menu.Style.Add("z-index", curzindex.ToString());
			curzindex += 2;

			BuildOpacity(menu);

			// Iterate through the menuItem's subMenu...
			for (int i = 0; i < myItems.Count; i++)
			{
				MenuItem mi = myItems[i];

				// only render this MenuItem if it is visible and the user has permissions
				if (mi.Visible && UserHasPermission(mi)) 
				{
					TableRow tr = new TableRow();
					TableCell td = new TableCell();
					td.ApplyStyle(this.unselectedMenuItemStyle);
					// The style is overwritten by anything specifically set in menuitem
					if (mi.BackColor != Color.Empty) 
						td.BackColor = mi.BackColor;
					if (mi.Font != null)
						td.Font.CopyFrom(mi.Font);
					if (mi.ForeColor != Color.Empty)
						td.ForeColor = mi.ForeColor;
					if (mi.Height != Unit.Empty)
						td.Height = mi.Height;
					if (mi.Width != Unit.Empty)
						td.Width = mi.Width;
					if (mi.CssClass != String.Empty)
						td.CssClass = mi.CssClass;
					else if	(this.DefaultCssClass != String.Empty)
						td.CssClass = this.DefaultCssClass;
					if (mi.BorderColor != Color.Empty)
						td.BorderColor = mi.BorderColor;
					if (mi.BorderStyle != BorderStyle.NotSet)
						td.BorderStyle = mi.BorderStyle;
					if (mi.BorderWidth != Unit.Empty)
						td.BorderWidth = mi.BorderWidth;
					if (mi.HorizontalAlign != System.Web.UI.WebControls.HorizontalAlign.NotSet)
						td.HorizontalAlign = mi.HorizontalAlign;
					if (mi.VerticalAlign != System.Web.UI.WebControls.VerticalAlign.NotSet)
						td.VerticalAlign = mi.VerticalAlign;
					BuildOpacity(td);
					if (mi.Text != string.Empty)
						td.Text = mi.Text;  // Text
					else if (mi.Image != string.Empty) // Show Image
					{
						System.Web.UI.WebControls.Image cellimage = new System.Web.UI.WebControls.Image();

						cellimage.ImageUrl = mi.Image;
						cellimage.AlternateText = mi.ImageAltText;
						td.Controls.Add(cellimage);

						image = mi.Image;
						mouseoverimage = mi.MouseOverImage;
						mousedownimage = mi.MouseDownImage;
						mouseupimage = mi.MouseUpImage;
					}
					td.Attributes.Add("id", mi.MenuID);

					// Add in the left or right image as needed
					if (mi.LeftImage != String.Empty) 
					{
						System.Web.UI.WebControls.Image leftimage = new System.Web.UI.WebControls.Image();
						System.Web.UI.WebControls.Literal leftliteral = new System.Web.UI.WebControls.Literal();

						leftimage.ImageAlign = mi.LeftImageAlign;
						if (mi.LeftImageRightPadding != Unit.Empty)
							leftimage.Style.Add("margin-right",mi.LeftImageRightPadding.Value.ToString());
						leftimage.ImageUrl = mi.LeftImage;
						td.Controls.Add(leftimage);

						leftliteral.Text = td.Text;
						td.Controls.Add(leftliteral);
					} 
					else if (mi.RightImage != String.Empty) 
					{
						System.Web.UI.WebControls.Image rightimage = new System.Web.UI.WebControls.Image();
						System.Web.UI.WebControls.Literal rightliteral = new System.Web.UI.WebControls.Literal();
						
						rightliteral.Text = td.Text;
						td.Controls.Add(rightliteral);

						rightimage.ImageAlign = mi.RightImageAlign;
						if (mi.RightImageLeftPadding != Unit.Empty)
							rightimage.Style.Add("margin-left",mi.RightImageLeftPadding.Value.ToString());
						rightimage.ImageUrl = mi.RightImage;
						td.Controls.Add(rightimage);
					}

					// Prepare MouseOverCssClass
					string mouseover = String.Empty;

					if (this.DefaultMouseOverCssClass != string.Empty || mi.MouseOverCssClass != string.Empty || this.selectedMenuItemStyle.CssClass != String.Empty)
						mouseover = "this.className='" + GetClass(this.DefaultMouseOverCssClass, mi.MouseOverCssClass, this.selectedMenuItemStyle.CssClass) + "';";

					if (mi.Enabled) // If enabled...
					{
						// Generate OnClick handler
						if (mi.JavascriptCommand != String.Empty) // javascript command
							td.Attributes.Add("onclick", mi.JavascriptCommand + "skm_closeSubMenus(document.getElementById('" + this.ClientID + "'));");
						else if (mi.Url != String.Empty) 
						{
							if (mi.Target != String.Empty)
								td.Attributes.Add("onclick", "javascript:skm_closeSubMenus(document.getElementById('" + this.ClientID + "'));window.open('" + GetURL(mi.ResolveURL, mi.Url) + "','" + mi.Target + "');");
							else if (this.DefaultTarget != String.Empty)
								td.Attributes.Add("onclick", "javascript:skm_closeSubMenus(document.getElementById('" + this.ClientID + "'));window.open('" + GetURL(mi.ResolveURL, mi.Url) + "','" + this.DefaultTarget + "');");
							else
								td.Attributes.Add("onclick", "javascript:skm_closeSubMenus(document.getElementById('" + this.ClientID + "'));location.href='" + GetURL(mi.ResolveURL, mi.Url) + "';");	
						}
						else if (mi.CommandName != String.Empty)  // Must be postback action
							td.Attributes.Add("onclick", Page.GetPostBackClientHyperlink(this, mi.CommandName));
						else if (this.ClickToOpen) // Open submenu on click
							td.Attributes.Add("onclick", "javascript:skm_mousedOverMenu('" + this.ClientID + "',this, document.getElementById('" + menuID + "'), true, '" + mouseoverimage + "');" + GenerateShimCall("true", mi.MenuID + "-subMenu"));
					}

					if (mi.Enabled)
					{
						// Output Tooltip
						if (mi.ToolTip != String.Empty) 
							td.ToolTip = mi.ToolTip;
					}

					// Is this a enabled menuitem?  (as opposed to a Separator or Header)
					if (mi.MenuType == MenuItemType.MenuItem && mi.Enabled) 
					{
						// Output MouseDownCssClass or MouseDownImage
						string mousedown = String.Empty;

						if (this.DefaultMouseDownCssClass != string.Empty || mi.MouseDownCssClass != string.Empty)
							mousedown = "this.className='" + GetClass(this.DefaultMouseDownCssClass, mi.MouseDownCssClass) + "';";

						if (mousedownimage != String.Empty)
							mousedown += "setimage(this, '" + mousedownimage + "');";

						if (mousedown != string.Empty)
							td.Attributes.Add("onmousedown", mousedown);

						// Output MouseUpCssClass or MouseUpImage
						string mouseup = String.Empty;

						if (this.DefaultMouseUpCssClass != string.Empty || mi.MouseUpCssClass != string.Empty)
							mouseup = "this.className='" + GetClass(this.DefaultMouseUpCssClass, mi.MouseUpCssClass) + "';";

						if (mouseupimage != String.Empty)
							mouseup += "setimage(this, '" + mouseupimage + "');";

						if (mouseup != string.Empty)
							td.Attributes.Add("onmouseup", mouseup);

						if (this.ClickToOpen == false) 
						{ 
							td.Attributes.Add("onmouseover", "javascript:skm_mousedOverMenu('" + this.ClientID + "',this, document.getElementById('" + menuID + "'), true, '" + mouseoverimage + "');" + GenerateShimCall("true", mi.MenuID + "-subMenu") + mouseover);
						} 
						else
							td.Attributes.Add("onmouseover", "javascript:skm_mousedOverClickToOpen('" + this.ClientID + "', this, document.getElementById('" + menuID + "'), '" + mouseoverimage + "');"+ mouseover);

						td.Attributes.Add("onmouseout", "javascript:skm_mousedOutMenu('" + this.ClientID + "', this,'" + image + "');" + "this.className='" + GetClass(this.DefaultCssClass, mi.CssClass, this.unselectedMenuItemStyle.CssClass) + "';");
					} 
					else 
					{  // If only a spacer, header or disabled, don't make any style change on mouseover
						td.Attributes.Add("onmouseover", "javascript:skm_mousedOverSpacer('" + this.ClientID + "', this, document.getElementById('" + menuID + "'));");
						td.Attributes.Add("onmouseout", "javascript:skm_mousedOutSpacer('" + this.ClientID + "', this);");
					}

					if (mi.Url != String.Empty || mi.CommandName != String.Empty)
						if (this.Cursor != MouseCursor.Default) 
						{
							if (this.Page.Request.Browser.Browser == "IE")
								td.Style.Add("cursor","hand"); 
							else
								td.Style.Add("cursor","pointer"); 
						}

					tr.Cells.Add(td);
					menu.Rows.Add(tr);

					// (Recursively) Add the subitems for this menu, if needed
					if (mi.SubItems.Count > 0)
					{
						// Create an IFrame (IE5.5 or better) to windowed form elements that might
						// interfere with display of the menu
						if (this.Page.Request.Browser.Browser == "IE" && Convert.ToDouble(this.Page.Request.Browser.Version, enUSCulture) >= 5.5) 
						{
							System.Web.UI.HtmlControls.HtmlGenericControl iframe = new System.Web.UI.HtmlControls.HtmlGenericControl();
							iframe.TagName = "iframe";
							iframe.Attributes.Add("id", "shim" + mi.MenuID + "-subMenu");
							iframe.Attributes.Add("src", IFrameSrc);
							iframe.Attributes.Add("scrolling", "no");
							iframe.Attributes.Add("frameborder", "no");
							iframe.Style.Add("position", "absolute");
							iframe.Style.Add("top", "0px");
							iframe.Style.Add("left", "0px");
							iframe.Style.Add("display", "none");
							BuildOpacity(iframe);
							Controls.Add(iframe);			
						}

						this.subItemsIds.Add(mi.MenuID + "-subMenu");
						AddMenu(mi.MenuID + "-subMenu", mi.SubItems); 
					}
				}
			}

			Controls.Add(menu);
		}

		/// <summary>
		/// Determines if a user belongs to a role for a particular <see cref="MenuItem"/>.
		/// </summary>
		/// <param name="mi">The MenuItem to check.</param>
		/// <returns><b>true</b> if the user has the right permissions to view this MenuItem; <b>false</b> otherwise.</returns>
		/// <remarks>UserHasPermission() works by returning <b>true</b> if there are no roles defined for <b>mi</b>
		/// or if there is at least one role defined for the user and the <b>mi</b> role collection and user role collection
		/// are <i>not</i> disjoint.</remarks>
		protected virtual bool UserHasPermission(MenuItem mi)
		{
			if (mi.Roles.Count == 0) return true;		// no roles needed to access mi, return true;
			if (this.roles.Count == 0) return false;	// there are roles needed for mi, but user has no roles

			return !mi.Roles.Disjoint(roles);			// allow access if mi's roles and user's roles are non-disjoint
		}

		/// <summary>
		/// For Internet Explorer 5.5 and up, IFRAMEs are used.  This method returns the client-side JavaScript to show/hide
		/// the IFRAME.
		/// </summary>
		/// <param name="state">A string vaue containing either the string <b>true</b> or <b>false</b>.  A value of
		/// <b>true</b> makes the IFRAME visible; <b>false</b> hides it.</param>
		/// <param name="id">The client-side <b>id</b> of the IFRAME.</param>
		/// <returns>A client-side JavaScript function call that will show/hide the IFRAME.</returns>
		protected virtual string GenerateShimCall(string state, string id)
		{
			CultureInfo enUSCulture = new CultureInfo("en-us", false);
			if (this.Page.Request.Browser.Browser == "IE" && Convert.ToDouble(this.Page.Request.Browser.Version, enUSCulture) >= 5.5) 
			{
				return "skm_shimSetVisibility(" + state + ",'" + id + "');";
			} else
				return String.Empty;
		}


		/// <summary>
		/// This method is used from the OnDataBinding method; it traverses the XML document,
		/// building up the object model.
		/// </summary>
		/// <param name="itemsNode">The current menuItem XmlNode</param>
		/// <param name="parentID">The ID of the parent menuItem XmlNode</param>
		/// <returns>A set of MenuItems for this menu.</returns>
		protected virtual MenuItemCollection GatherMenuItems(XmlNode itemsNode, string parentID)
		{
			// Make sure we have an XmlNode instance - it should never be null, else the
			// XML document does not have the expected structure
			if (itemsNode == null)
				throw new ArgumentException("The XML data for the Menu control is in an invalid format.");

			MenuItemCollection myItems = new MenuItemCollection();
			if (IsTrackingViewState)
				((IStateManager) myItems).TrackViewState();

			// iterate through each MenuItem
			XmlNodeList menuItems = itemsNode.ChildNodes;
			for (int i = 0; i < menuItems.Count; i++)
			{
				XmlNode menuItem = menuItems[i];

				// Create the menuitem
				if (menuItem.Name == "menuItem" || menuItem.Name == "menuSpacer")
					myItems.Add(BuildMenuItem(menuItem, parentID, i));
			}

			return myItems;
		}


        /// <summary>
		/// This method creates a single <see cref="MenuItem"/> and is called repeatedly from <see cref="GatherMenuItems"/>.
		/// </summary>
		/// <param name="menuItem">The MenuItem XmlNode.</param>
		/// <param name="parentID">The parent MenuItem's <see cref="MenuID"/>.</param>
		/// <param name="indexValue">The ordinal index of the MenuItem in the set of MenuItems.</param>
		/// <returns>A new <see cref="MenuItem"/> instance.</returns>
		protected virtual MenuItem BuildMenuItem(XmlNode menuItem, string parentID, int indexValue)
		{
			MenuItem mi = new MenuItem();
			if (IsTrackingViewState)
				((IStateManager) mi).TrackViewState();

			// Format the indexValue so its three-digits (allows for 1,000 menuitems per (sub)menu
			mi.MenuID = parentID + "-menuItem" + indexValue.ToString("d3");

			// Build a spacer
			if (menuItem.Name == "menuSpacer")
				return BuildMenuSpacer(menuItem, mi);


			XmlNode textTextNode = menuItem.SelectSingleNode("text/text()");
			XmlNode urlTextNode = menuItem.SelectSingleNode("url/text()");
			XmlNode targetTextNode = menuItem.SelectSingleNode("target/text()");
			XmlNode commandNameTextNode = menuItem.SelectSingleNode("commandname/text()");
			XmlNode javascriptCommandTextNode = menuItem.SelectSingleNode("javascriptcommand/text()");
			XmlNode tooltipTextNode = menuItem.SelectSingleNode("tooltip/text()");
			XmlNode cssclassTextNode = menuItem.SelectSingleNode("cssclass/text()");
			XmlNode mouseovercssclassTextNode = menuItem.SelectSingleNode("mouseovercssclass/text()");
			XmlNode mouseupcssclassTextNode = menuItem.SelectSingleNode("mouseupcssclass/text()");
			XmlNode mousedowncssclassTextNode = menuItem.SelectSingleNode("mousedowncssclass/text()");
			XmlNode imageTextNode = menuItem.SelectSingleNode("image/text()");
			XmlNode imagealttextTextNode = menuItem.SelectSingleNode("imagealttext/text()");
			XmlNode mouseoverimageTextNode = menuItem.SelectSingleNode("mouseoverimage/text()");
			XmlNode mouseupimageTextNode = menuItem.SelectSingleNode("mouseupimage/text()");
			XmlNode mousedownimageTextNode = menuItem.SelectSingleNode("mousedownimage/text()");
			XmlNode resolveurlTextNode = menuItem.SelectSingleNode("resolveurl/text()");
			XmlNode	enabledTextNode = menuItem.SelectSingleNode("enabled/text()");
			XmlNode	visibleTextNode = menuItem.SelectSingleNode("visible/text()");
			XmlNode	horizontalalignTextNode = menuItem.SelectSingleNode("horizontalalign/text()");
			XmlNode	verticalalignTextNode = menuItem.SelectSingleNode("verticalalign/text()");
			XmlNode	widthTextNode = menuItem.SelectSingleNode("width/text()");
			XmlNode	heightTextNode = menuItem.SelectSingleNode("height/text()");
			XmlNode leftImageTextNode = menuItem.SelectSingleNode("leftimage/text()");
			XmlNode rightImageTextNode = menuItem.SelectSingleNode("rightimage/text()");
			XmlNode rightImageLeftPaddingTextNode = menuItem.SelectSingleNode("rightimageleftpadding/text()");
			XmlNode leftImageRightPaddingTextNode = menuItem.SelectSingleNode("leftimagerightpadding/text()");
			XmlNode rightImageAlignTextNode = menuItem.SelectSingleNode("rightimagealign/text()");
			XmlNode leftImageAlignTextNode = menuItem.SelectSingleNode("leftimagealign/text()");
			XmlNode backColorTextNode = menuItem.SelectSingleNode("backcolor/text()");
			XmlNode borderColorTextNode = menuItem.SelectSingleNode("bordercolor/text()");
			XmlNode borderWidthTextNode = menuItem.SelectSingleNode("borderwidth/text()");
			XmlNode rolesTextNode = menuItem.SelectSingleNode("roles/text()");

			if (textTextNode == null && imageTextNode == null)
				// whoops, the <text> element is required
				throw new ArgumentException("The XML data for the Menu control is in an invalid format: missing both the <text> and <image> elements for a <menuItem>.  One of these must be specified.");

			if (textTextNode != null)
				mi.Text = textTextNode.Value;

			if (urlTextNode != null)
				mi.Url = urlTextNode.Value;

			if (targetTextNode != null)
				mi.Target = targetTextNode.Value;

			if (commandNameTextNode != null)
				mi.CommandName = commandNameTextNode.Value;

			if (javascriptCommandTextNode != null)
				mi.JavascriptCommand = javascriptCommandTextNode.Value;

			if (tooltipTextNode != null)
				mi.ToolTip = tooltipTextNode.Value;

			if (backColorTextNode != null)
				mi.BackColor = ColorTranslator.FromHtml( backColorTextNode.Value );

			if (borderColorTextNode != null)
				mi.BorderColor = ColorTranslator.FromHtml( borderColorTextNode.Value );

			if (borderWidthTextNode != null)
				mi.BorderWidth = new Unit( borderWidthTextNode.Value );

			if (cssclassTextNode != null)
				mi.CssClass = cssclassTextNode.Value;

			if (mouseovercssclassTextNode != null)
				mi.MouseOverCssClass = mouseovercssclassTextNode.Value;

			if (mouseupcssclassTextNode != null)
				mi.MouseUpCssClass = mouseupcssclassTextNode.Value;

			if (mousedowncssclassTextNode != null)
				mi.MouseDownCssClass = mousedowncssclassTextNode.Value;

			if (imageTextNode != null)
				mi.Image = imageTextNode.Value;

			if (imagealttextTextNode != null)
				mi.ImageAltText = imagealttextTextNode.Value;

			if (mouseoverimageTextNode != null)
				mi.MouseOverImage = mouseoverimageTextNode.Value;

			if (mouseupimageTextNode != null)
				mi.MouseUpImage = mouseupimageTextNode.Value;

			if (mousedownimageTextNode != null)
				mi.MouseDownImage = mousedownimageTextNode.Value;

			if (resolveurlTextNode != null)
				if (resolveurlTextNode.Value.ToLower() == "true")
					mi.ResolveURL = true;
				else if (resolveurlTextNode.Value.ToLower() == "false")
					mi.ResolveURL = false;

			if (enabledTextNode != null)
				if (enabledTextNode.Value.ToLower() == "true")
					mi.Enabled = true;
				else if (enabledTextNode.Value.ToLower() == "false")
					mi.Enabled = false;

			if (visibleTextNode != null)
				if (visibleTextNode.Value.ToLower() == "true")
					mi.Visible = true;
				else if (visibleTextNode.Value.ToLower() == "false")
					mi.Visible = false;

			if (horizontalalignTextNode != null)
				if (horizontalalignTextNode.Value.ToLower() == "center")
					mi.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
				else if (horizontalalignTextNode.Value.ToLower() == "justify")
					mi.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Justify;
				else if (horizontalalignTextNode.Value.ToLower() == "left")
					mi.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
				else if (horizontalalignTextNode.Value.ToLower() == "right")
					mi.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
			
			if (verticalalignTextNode != null)
				if (verticalalignTextNode.Value.ToLower() == "bottom")
					mi.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Bottom;
				else if (verticalalignTextNode.Value.ToLower() == "middle")
					mi.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
				else if (verticalalignTextNode.Value.ToLower() == "top")
					mi.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top;

			if (widthTextNode != null)
				mi.Width = Unit.Pixel(Convert.ToInt32(widthTextNode.Value));

			if (heightTextNode != null)
				mi.Height = Unit.Pixel(Convert.ToInt32(heightTextNode.Value));

			if (leftImageTextNode != null)
			{
				mi.LeftImage = leftImageTextNode.Value;
				if (leftImageRightPaddingTextNode != null)
					mi.LeftImageRightPadding = Unit.Pixel(Convert.ToInt32(leftImageRightPaddingTextNode.Value));
				if (leftImageAlignTextNode != null)
				{
					switch (leftImageAlignTextNode.Value.ToLower())
					{
						case "absbottom":
							mi.LeftImageAlign = ImageAlign.AbsBottom;
							break;
						case "absmiddle":
							mi.LeftImageAlign = ImageAlign.AbsMiddle;
							break;
						case "baseline":
							mi.LeftImageAlign = ImageAlign.Baseline;
							break;
						case "bottom":
							mi.LeftImageAlign = ImageAlign.Bottom;
							break;
						case "left":
							mi.LeftImageAlign = ImageAlign.Left;
							break;
						case "middle":
							mi.LeftImageAlign = ImageAlign.Middle;
							break;
						case "right":
							mi.LeftImageAlign = ImageAlign.Right;
							break;
						case "texttop":
							mi.LeftImageAlign = ImageAlign.TextTop;
							break;
						case "top":
							mi.LeftImageAlign = ImageAlign.Top;
							break;
						default:
							mi.LeftImageAlign = ImageAlign.NotSet;
							break;
					}
				}
			}
			else if (rightImageTextNode != null)
			{
				mi.RightImage = rightImageTextNode.Value;
				if (rightImageLeftPaddingTextNode != null)
					mi.RightImageLeftPadding = Unit.Pixel(Convert.ToInt32(rightImageLeftPaddingTextNode.Value));
				if (rightImageAlignTextNode != null)
				{
					switch (rightImageAlignTextNode.Value.ToLower())
					{
						case "absbottom":
							mi.RightImageAlign = ImageAlign.AbsBottom;
							break;
						case "absmiddle":
							mi.RightImageAlign = ImageAlign.AbsMiddle;
							break;
						case "baseline":
							mi.RightImageAlign = ImageAlign.Baseline;
							break;
						case "bottom":
							mi.RightImageAlign = ImageAlign.Bottom;
							break;
						case "left":
							mi.RightImageAlign = ImageAlign.Left;
							break;
						case "middle":
							mi.RightImageAlign = ImageAlign.Middle;
							break;
						case "right":
							mi.RightImageAlign = ImageAlign.Right;
							break;
						case "texttop":
							mi.RightImageAlign = ImageAlign.TextTop;
							break;
						case "top":
							mi.RightImageAlign = ImageAlign.Top;
							break;
						default:
							mi.RightImageAlign = ImageAlign.NotSet;
							break;
					}
				}
			}

			if (rolesTextNode != null)	// add the roles
				mi.Roles.AddRange(rolesTextNode.Value.Split(new char[] {','}));				
            
			// see if there is a submenu
			XmlNode subMenu = menuItem.SelectSingleNode("subMenu");
			if (subMenu != null)
			{
				// Recursively processes the <menuItem>'s <subMenu> node, if present
				mi.SubItems.AddRange(GatherMenuItems(subMenu, mi.MenuID + "-subMenu"));
			}

			return mi;
		}
        
		/// <summary>
		/// Creates a MenuItem spacer.
		/// </summary>
		protected virtual MenuItem BuildMenuSpacer(XmlNode menuItem, MenuItem mi)
		{
			XmlNodeList children = menuItem.ChildNodes;
			
			mi.Text = String.Empty;
			mi.MenuType = MenuItemType.MenuSeparator;

			for (int i = 0; i < children.Count; i++)
			{
                XmlNode node = children[i];
				switch(node.Name)
				{
					case "cssclass":
						mi.CssClass = node.InnerText;
						break;
					case "height":
						mi.Height = Unit.Parse(node.InnerText);
						break;
					case "spacermarkup":
						mi.Text = node.InnerText;
						break;
				}
			}

			return mi;
		}
		#endregion

		# region Client Script Methods
		/// <summary>
		/// A helper method that determines if an external script is being used.
		/// </summary>
		/// <remarks><b>UseExternalScript</b> simply returns the result of the check <b>ExternalScriptUrl != null</b>.</remarks>
		protected virtual bool UseExternalScript {
			get
			{
				return (ExternalScriptUrl != null);
			}
		}


		///<summary>
		///Return the configured URL for the external script.
		///</summary>
		///<remarks>
		/// The external JavaScript file maybe specified either in the <b>Web.config</b> file or through the
		/// Menu control's <see cref="ScriptPath"/> property.  <b>ExternalScriptUrl checks first the <b>Web.config</b>
		/// file and then the <see cref="ScriptPath"/> property to determine if an external JavaScript file should be used.</b>
		///</remarks>
		protected virtual string ExternalScriptUrl {
			get
			{
				NameValueCollection config = ConfigurationSettings.GetConfig("skmMenu") as NameValueCollection;
				if (config != null)
				{
					return config["ExternalScriptUrl"];
				}
				else if (this.ScriptPath != string.Empty) 
				{
					return this.ScriptPath;
				} 
				else
				{
					return null;
				}
			}
		}


		/// <summary>
		/// Registers the main client script.
		/// </summary>
		/// <remarks>RegisterClientScriptBlock() adds the global variables and JavaScript methods.  Depending on
		/// how skmMenu is configured, this client-side script is either emitted directly to the browser in the
		/// HTML stream, or references an external JavaScript file.  It is recommended that the external JavaScript
		/// file approach be used, since it yields better performance on both the client and server.<p />
		/// For more information on using an external JavaScript file, see the <see cref="ScriptPath"/> property.</remarks>
		protected virtual void RegisterClientScriptBlock() {
			if ( !this.Page.IsClientScriptBlockRegistered("skmMenu") )
			{
				String script;
				if(UseExternalScript)		// we have an external JavaScript file reference.
				{
					StringBuilder scriptBuilder = new StringBuilder();
					scriptBuilder.AppendFormat("<script language=\"javascript\" src=\"{0}\"></script>",this.ResolveUrl(ExternalScriptUrl));
					script = scriptBuilder.ToString();
				}
				else
				{
					// Need to load the script from the assembly's resources.
					// If you are working on the skmMenu code base, this is in Menu.resx...
					ResourceManager manager = new ResourceManager( this.GetType() );
					script = manager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true).GetString("ClientScript");
				}
				this.Page.RegisterClientScriptBlock("skmMenu", script );
			}
		}


		///<summary>
		///Registers the startup client script.
		///</summary>
		///<remarks>The startup script involves calls to the client-side <b>skm_registerMenu()</b> function.
		///</remarks>
		protected virtual void RegisterClientStartupScript() {
			StringBuilder script = new StringBuilder();
			script.Append("<script language=\"javascript\">");
			script.Append("skm_registerMenu('");
			script.Append(this.ClientID);
			script.Append("',");
			script.Append(InstantiateStyleInfoJavascript(this.SelectedMenuItemStyle));
			script.Append(',');
			script.Append(InstantiateStyleInfoJavascript(this.UnselectedMenuItemStyle));
			script.Append(',');
			script.Append(this.MenuFadeDelay);
			script.Append(',');
			script.Append(this.HighlightTopMenu.ToString().ToLower());
			script.Append(");");
			script.Append("</script>");

			// It is vital that Page.RegisterStartupScript be used so that we can be guaranteed
			// that this initialization code appears *AFTER* the methods registerd in the
			// RegisterClientScriptBlock() method...
			Page.RegisterStartupScript(this.ClientID, script.ToString());
		}


		///<summary>
		///Registers the SubMenus for the menu with a JavaScript array.
		///</summary>
		///<remarks>A client-side array <b>skm_subMenuIDs</b> maintains an array of all of the <b>id</b> values
		///of the client-side submenus.  This is useful in dynamically determining the client position of a submenu.</remarks>
		protected virtual void RegisterSubmenuArray() {
			for(int i=0; i<this.subItemsIds.Count;i++)
			{
				Page.RegisterArrayDeclaration("skm_subMenuIDs", "'" + (string) subItemsIds[i] + "'");
			}

			//This is a hacky fix for the case where no submenus are specified
			//must do this properly later

			Page.RegisterArrayDeclaration("skm_subMenuIDs", "'" + this.ClientID + "'");
		}


		///<summary>
		///Registers the Preload command to preload any images.
		///</summary>
		///<remarks>Preloading images used in a page improves the user experience by loading an image before it is needed.
		///If a menu has rollover images and does <b>not</b> preload its images, there will be a short delay when the
		///user mouses over the menu item for the first time, as the image must then be downloaded.  Preloading the images
		///removes this delay since the image will have likely been downloaded by the time the user mouses over the menu
		///image.<p />For more information on preloading JavaScript images refer to <a href="http://javascript.internet.com/miscellaneous/preload-images.html">this article</a>.</remarks>
		protected virtual void RegisterPreloadCommand() 
		{
			if (this.imagePreload.ToString() != String.Empty) 
			{
				// Again, this must use RegisterStartupScript since it relies on the preloadimages() JavaScript
				// method - that is, it must appear *after* preloadimages() is defined...
				Page.RegisterStartupScript(this.ClientID+"_preload","<script language=\"javascript\">"+"preloadimages("+this.imagePreload.ToString()+");</script>");
			}
		}

        
		///<summary>
		///Returns the javascript that instantiates a style info object
		///that corresponds to the style specified.
		///</summary>
		protected virtual string InstantiateStyleInfoJavascript(Style style)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("new skm_styleInfo('");
			sb.Append(ColorTranslator.ToHtml(style.BackColor));
			sb.Append("','");
			sb.Append(ColorTranslator.ToHtml(style.BorderColor));
			sb.Append("','");
			sb.Append(style.BorderStyle == BorderStyle.NotSet ? string.Empty : style.BorderStyle.ToString());
			sb.Append("','");
			sb.Append(style.BorderWidth.ToString());
			sb.Append("','");
			sb.Append(ColorTranslator.ToHtml(style.ForeColor));
			sb.Append("','");
			sb.Append(string.Join(",",style.Font.Names));
			sb.Append("','");
			sb.Append(style.Font.Size.ToString());
			sb.Append("','");
			sb.Append(style.Font.Italic ? "italic" : string.Empty);
			sb.Append("','");
			sb.Append(style.Font.Bold ? "bold" : string.Empty);
			sb.Append("','");
			sb.Append(style.CssClass);
			sb.Append("')");

			return sb.ToString();
		}
		#endregion


		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			OnMenuItemClick(new MenuItemClickEventArgs(eventArgument));
		}


		#region Menu Properties
		/// <summary>
		/// Sets or gets the name of the XML file or XmlDocument object that is the datasource for the menu.
		/// </summary>
		/// <remarks>The <b>DataSource</b> can be assigned either a string filename to an XML file, or an
		/// XmlDocument object.  Attempting to assign the <b>DataSource</b> to something other than a string or
		/// XmlDocument will result in an <b>ArgumentException</b> being thrown.</remarks>
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value is string || value is XmlDocument)
					this.dataSource = value;
				else
					throw new ArgumentException("DataSource must be a string or XmlDocument instance.");
			}
		}


		/// <summary>
		/// Returns the Menu's top-level <see cref="MenuItem"/>s.
		/// </summary>
		/// <remarks>This returns a <see cref="MenuItemCollection"/> instance holding the top-level menu items only.
		/// If you want to drill down into submenu items, you must programmatically recurse down.  For example,
		/// imagine we had a menu with two top-level menu items, File and Help, and File had three menu items,
		/// New, Open, and Save.  To programmatically access the New menu item from the Menu you'd use:
		/// <code>
		///  MenuItem FileNewMenuItem = MenuControlID.Items[0].Items[0];
		/// </code></remarks>
		[
			Browsable(false),
			EditorAttribute(typeof(System.ComponentModel.Design.CollectionEditor), typeof(UITypeEditor))
		]
		public MenuItemCollection Items
		{
			get
			{				
				if (this.IsTrackingViewState)
					((IStateManager) items).TrackViewState();

				return this.items;
			}
		}

		/// <summary>
		/// Specifies the roles the current user belongs to.  When assigning roles to the MenuItems, the user's roles
		/// affect what menu items are displayed.
		/// </summary>
		/// <remarks>
		/// Each MenuItem in a skmMenu can be assigned a set of <i>roles</i>.  A <i>role</i> is a name that implies some
		/// level of access.  Roles are denoted as strings.  Example roles might be: developer, tester, and admin.<p />
		/// To denote a role for a <see cref="MenuItem"/>, the <see cref="MenuItem.Roles"/> property can be used programmatically,
		/// <i>or</i> the &lt;roles&gt; XML element can be used if the menu data is supplied in an XML file.  (For more
		/// information on the &lt;roles&gt; XML element and binding menu data via an XML file, consult
		/// <a href="http://skmmenu.com/menu/Download/XMLStructure.html">http://skmmenu.com/menu/Download/XMLStructure.html</a>.<p />
		/// The Menu class's <b>UserRoles</b> property specifies the set of roles the user viewing the page has.  This
		/// needs to be set programmatically in the first page load (the role information is persisted across postbacks).
		/// Typically this role assignment will need to be done by looking up the current user's set of roles in a database
		/// or some other ACL store.<p />
		/// After the <b>UserRoles</b> property has been set, you can bind the XML data to skmMenu through a call to
		/// <see cref="DataBind"/>.  Only those <see cref="MenuItem"/>s that either (a) have no roles defined or (b) has a role
		/// collection that intersects the <b>UserRoles</b> role collection, will be rendered.  For a live demo of
		/// roles with skmMenu, see <a href="http://skmmenu.com/menu/Examples/Roles.aspx">http://skmmenu.com/menu/Examples/Roles.aspx</a>.
		/// </remarks>
		[
		Browsable(false)
		]
		public RoleCollection UserRoles
		{
			get
			{				
				if (this.IsTrackingViewState)
					((IStateManager) items).TrackViewState();

				return this.roles;
			}
		}

	
		/// <summary>
		/// Specifies the style for selected <see cref="MenuItem"/>s.
		/// </summary>
		/// <remarks>A MenuItem is <i>selected</i> when the user moves the mouse over the menu item.</remarks>
		[ 
			Category("Appearance"),
			PersistenceMode(PersistenceMode.InnerProperty),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
			NotifyParentProperty(true),
			Description("Specifies the style for selected menuitems.")
		]
		public TableItemStyle SelectedMenuItemStyle
		{
			get
			{
				if (this.IsTrackingViewState)
					((IStateManager) this.selectedMenuItemStyle).TrackViewState();

				return this.selectedMenuItemStyle;
			}
		}


		/// <summary>
		/// Specifies whether URL's should be resolved before being output.
		/// </summary>
		/// <remarks>If this property is marked <b>True</b> then MenuItem URLs can use ~, as in ~/SomeDir/SomePage.htm.  Internally, if this
		/// property is set to <b>True</b> the <b>ResolveUrl()</b> method is used.</remarks>
		[ 
		Category("Behavior"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies whether URL's should be resolved before being output.")
		]
		public bool DefaultResolveURL
		{
			get
			{
				object o = ViewState["MenuDefaultResolveURL"];
				if (o == null)
					return false;
				else
					return (bool) o;
			}
			set
			{
				ViewState["MenuDefaultResolveURL"] = value;
			}
		}


		/// <summary>
		/// Specifies whether the top level menu should remain highlighted when the submenu beneath is displayed.
		/// </summary>
		[ 
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies whether the top level menu should remain highlighted when the submenu beneath is displayed.")
		]
		public bool HighlightTopMenu
		{
			get
			{
				object o = ViewState["MenuHighlightTopMenu"];
				if (o == null)
					return false;
				else
					return (bool) o;
			}
			set
			{
				ViewState["MenuHighlightTopMenu"] = value;
			}
		}
		

		/// <summary>
		/// Specifies whether a submenu is displayed on mouse over or when clicked.
		/// </summary>
		/// <value>A value of <b>True</b> indicates that submenus are displayed with a menu item is clicked;
		/// a value of <b>False</b> indicates that the submenu is displayed when the menu item is moused over.</value>
		[ 
		Category("Behavior"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies whether a submenu is displayed on mouse over or when clicked."),
		DefaultValue("false")
		]
		public bool ClickToOpen
		{
			get
			{
				object o = ViewState["MenuClickToOpen"];
				if (o == null)
					return false;
				else
					return (bool) o;
			}
			set
			{
				ViewState["MenuClickToOpen"] = value;
			}
		}

		
		/// <summary>
		/// Gets or sets the z-index style value for the menu.
		/// </summary>
		/// <remarks>For Internet Explorer, submenus are displayed using IFRAMEs.  Using IFRAMEs keeps the menu
		/// above other form elements, such as drop-down lists.  The <b>zIndex</b> specifies the "height" of the IFRAME
		/// relative to othjer elements on the page.  The default value of 1000 should ensure that the menus are
		/// always the top-most elements on the page.</remarks>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("The z-index property for the menu."),
		NotifyParentProperty(true),
		DefaultValue("1000")
		]
		public int zIndex
		{
			get
			{
				object o = ViewState["ItemzIndex"];
				if (o != null)
					return (int) o;
				else
					return 1000;
			}
			set
			{
				ViewState["MenuzIndex"] = value;
			}
		}

		
		/// <summary>
		/// Gets or sets the opacity style value for the menu.
		/// </summary>
		/// <remarks>Opacity values must be between 0 and 100.<p />
		/// The opacity property only works for select Web browsers: IE 5.0+, Netscape 6+ and Mozilla.</remarks>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("The opacity property only works IE 5.0+, Netscape 6+ and Mozilla."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public string Opacity
		{
			get
			{
				object o = ViewState["MenuOpacity"];
				if (o != null)
					return (string) o;
				else
					return null;
			}
			set
			{
				// Conversion in order to cause error if non-numeric
				int opacityvalue = Convert.ToInt32(value);

				if (opacityvalue>100 || opacityvalue<0) 
					throw new ArgumentOutOfRangeException("Opacity can not be greater than 100 or less than 0.");
				
				ViewState["MenuOpacity"] = value;
			}
		}


		/// <summary>
		/// Gets or sets the value for css class used for SubMenus.
		/// </summary>
		/// <remarks>If <b>SubMenuCssClass</b> is not specified and <see cref="CssClass"/> is specified, CssClass is 
		/// used instead.</remarks>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("The value used for the stylesheet class for SubMenus.  If not specified and CssClass is specified, CssClass is used instead."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public string SubMenuCssClass
		{
			get
			{
				object o = ViewState["MenuSubMenuCssClass"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["MenuSubMenuCssClass"] = value;
			}
		}

		
		/// <summary>
		/// Gets or sets the default CSS class name for the <see cref="MenuItem"/>'s CSS class.
		/// </summary>
		/// <remarks>If a <see cref="MenuItem"/> has no specified CSS class, the <b>DefaultCssClass</b> is used instead.</remarks>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("The default for a menuitem's optional stylesheet class if the menuitem doesn't specify a value."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public string DefaultCssClass
		{
			get
			{
				object o = ViewState["MenuDefaultCssClass"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["MenuDefaultCssClass"] = value;
			}
		}

		
		/// <summary>
		/// Gets or sets the default value for the menuitem's mouse over css class.  Used when a menuitem has no value specified.
		/// </summary>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("The default for a menuitem's mouse over stylesheet class if the menuitem doesn't specify a value."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public string DefaultMouseOverCssClass
		{
			get
			{
				object o = ViewState["MenuDefaultMouseOverCssClass"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["MenuDefaultMouseOverCssClass"] = value;
			}
		}

	
		/// <summary>
		/// Gets or sets the default value for the menuitem's mouse down css class.  Used when a menuitem has no value specified.
		/// </summary>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("The default for a menuitem's mouse down stylesheet class if the menuitem doesn't specify a value."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public string DefaultMouseDownCssClass
		{
			get
			{
				object o = ViewState["MenuDefaultMouseDownCssClass"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["MenuDefaultMouseDownCssClass"] = value;
			}
		}
		
		
		/// <summary>
		/// Gets or sets the default value for the menuitem's mouse up css class.  Used when a menuitem has no value specified.
		/// </summary>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("The default for a menuitem's mouse up stylesheet class if the menuitem doesn't specify a value."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public string DefaultMouseUpCssClass
		{
			get
			{
				object o = ViewState["MenuDefaultMouseUpCssClass"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["MenuDefaultMouseUpCssClass"] = value;
			}
		}
	
	
		/// <summary>
		/// Gets or sets the path to an external JavaScript file.  If no external path is specified, the JavaScript is
		/// rendered by the control directly in the page.
		/// </summary>
		/// <remarks>It is highly recommended that you utilize an external JavaScript file, as it promises performance increases
		/// both for the client and server.  The client can cache an external JavaScript file, thereby reducing the bandwidth
		/// needed to be downloaded.  On the server side, using an external JavaScript file results in several dozen lines
		/// of JavaScript code not needing to be rendered.<p />To use an external JavaScript file, place the <b>skmMenu.js</b> file
		/// that was included in the download in a directory in your Web site.  You can then set this property to the
		/// path of the <b>skmMenu.js</b> file.</remarks>
		/// <example>
		/// [C#] This example assumes that the <b>skmMenu.js</b> file was placed in the /scripts/ directory of your Web site.
		/// <code>
		/// protected Menu myMenuControl;
		/// 
		/// private void Page_Load(object sender, EventArgs e)
		/// {
		///   myMenuControl.ScriptPath = "/scripts/skmMenu.js";
		///   ...
		/// }
		/// </code>
		/// </example>
		[ 
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies external path and filename for javascript file."),
		DefaultValue(""),
		EditorAttribute(typeof(UrlEditor), typeof(UITypeEditor))
		]
		public string ScriptPath
		{
			get
			{
				object o = ViewState["MenuScriptPath"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["MenuScriptPath"] = value;
			}
		}

	
		/// <summary>
		/// Gets or sets the default target for links in menuitems.  Used if no target property is specified for a menuitem.
		/// </summary>
		[ 
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies default target for links in menuitems - if no target property specified for a menuitem."),
		DefaultValue("")
		]
		public string DefaultTarget
		{
			get
			{
				object o = ViewState["MenuDefaultTarget"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["MenuDefaultTarget"] = value;
			}
		}

	
		/// <summary>
		/// Gets or sets padding for each menuitem (pixels).
		/// </summary>
		/// <remarks>Menus and submenus are rendered as HTML tables.  This property, then, specifies the rendered
		/// table's <b>CellPadding</b>.</remarks>
		[ 
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		DefaultValue(2),
		Description("Specifies padding for each menuitem.")
		]
		public int ItemPadding
		{
			get
			{
				object o = ViewState["MenuItemPadding"];
				if (o != null)
					return (int) o;
				else
					return 2;
			}
			set
			{
				ViewState["MenuItemPadding"] = value;
			}
		}	

	
		/// <summary>
		/// Gets or sets the spacing for each menuitem (pixels).
		/// </summary>
		/// <remarks>Menus and submenus are rendered as HTML tables.  This property, then, specifies the rendered
		/// table's <b>CellSpacing</b>.</remarks>
		[ 
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		DefaultValue(2),
		Description("Specifies spacing for each menuitem.")
		]
		public int ItemSpacing
		{
			get
			{
				object o = ViewState["MenuItemSpacing"];
				if (o != null)
					return (int) o;
				else
					return 2;
			}
			set
			{
				ViewState["MenuItemSpacing"] = value;
			}
		}			


		/// <summary>
		/// Specifies the style for unselected <see cref="MenuItem"/>s.
		/// </summary>
		/// <remarks>An <i>unselected</i> MenuItem is one that does <b>not</b> have the mouse cursor hovering over it.</remarks>
		[ 
			Category("Appearance"),
			PersistenceMode(PersistenceMode.InnerProperty),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
			NotifyParentProperty(true),
			Description("Specifies the style for unselected menuitems.")
		]
		public TableItemStyle UnselectedMenuItemStyle
		{
			get
			{
				if (this.IsTrackingViewState)
					((IStateManager) this.unselectedMenuItemStyle).TrackViewState();

				return this.unselectedMenuItemStyle;
			}
		}


		/// <summary>
		/// Sets or Gets the menu's layout direction.
		/// </summary>
		/// <value>A <see cref="MenuLayout"/> enumeration value.  The default is <b>Vertical</b></value>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("Specifies the menu layout direction."),
		DefaultValue(MenuLayout.Vertical)
		]
		public MenuLayout Layout
		{
			get
			{
				object o = ViewState["MenuLayout"];
				if (o == null)
					return MenuLayout.Vertical;
				else
					return (MenuLayout) o;
			}
			set
			{
				ViewState["MenuLayout"] = value;
			}
		}


		/// <summary>
		/// Sets or Gets the menu's mouse cursor.
		/// </summary>
		/// <value>A <see cref="MouseCursor"/> enumeration value.  The default is <b>MouseCursor.Default</b>.</value>
		/// <remarks>Setting the <b>Cursor</b> property to <b>MouseCursor.Pointer</b> will have the browser display
		/// a mouse pointer when the mouse is above a clickable menu item.</remarks>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("Specifies the menu mouse cursor for items with an associated URL or command.")
		]
		public MouseCursor Cursor
		{
			get
			{
				object o = ViewState["MenuMouseCursor"];
				if (o == null)
					return MouseCursor.Default;
				else
					return (MouseCursor) o;
			}
			set
			{
				ViewState["MenuMouseCursor"] = value;
			}
		}


		/// <summary>
		/// Sets or Gets the menu's gridline property.
		/// </summary>
		/// <value>This property is set with one of the <b>GridLines</b> enumeration values.  The default is <b>None</b>.</value>
		/// <remarks>
		/// Menus and submenus are rendered as HTML tables.  This property, then, specifies the rendered
		/// table's <b>GridLines</b> property value.
		/// The following table lists the possible values:
		/// <list type="table">
		///		<listheader><term>Value</term><term>Description</term></listheader>
		///		<item><term>None</term><description>No cell border is displayed.</description></item>
		///		<item><term>Horizontal</term><description>Only the upper and lower borders of the cells in a data listing control are displayed.</description></item>
		///		<item><term>Vertical</term><description>Only the left and right borders of the cells in the data list control are displayed.</description></item>
		///		<item><term>Both</term><description>All borders of the cells in a data listing control are displayed.</description></item>
		/// </list>
		/// </remarks>
		[
		Category("Appearance"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("Specifies the menu gridline setting.")
		]
		public GridLines GridLines
		{
			get
			{
				object o = ViewState["MenuGridLines"];
				if (o == null)
					return GridLines.None;
				else
					return (GridLines) o;
			}
			set
			{
				ViewState["MenuGridLines"] = value;
			}
		}


		/// <summary>
		/// Gets or sets the number of half seconds to display the menu after the user's mouse
		/// has left the menu, before hiding the menu.
		/// </summary>
		/// <value>The number of half-seconds to delay after the user's mouse has left the menu before hiding the menu.  Must have an integer value greater than
		/// or equal to 0.  The default is 2, causing the submenus to delay for one second before vanishing.</value>
		/// <remarks>It is recommended that this property have at least a value of 1.  Setting it to 0 will cause the
		/// submenu to disappear <b>immediately</b> after the user's mouse cursor leaves the submenu area.  This can
		/// result in a frustrating user experience.</remarks>
		[
		Category("Behavior"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Description("Specifies the number of half seconds it takes an inactive menu to fade."),
		DefaultValue(2)
		]
		public int MenuFadeDelay
		{
			get
			{
				object o = ViewState["MenuFadeDelay"];
				if (o == null)
					return 2;
				else
					return (int) ViewState["MenuFadeDelay"];
			}
			set
			{
				if (value < 0)
					throw new ArgumentException("MenuFadeDelay must have a value greater than or equal to 0.");

				ViewState["MenuFadeDelay"] = value;
			}
		}
		

		/// <summary>
		/// Specifies what SRC property should be loaded into an IFRAME.
		/// </summary>
		/// <remarks>When the user is using Internet Explorer to render a page with skmMenu on it, the skmMenu submenus
		/// are rendered as IFRAMEs.  By default, the SRC is a blank string.  If you are using skmMenu on SSL, however, this
		/// blank SCR will cause a dialog box to appear warning the user that both secure and non-secure items are
		/// being displayed.  To circumvent this, you need to create a blank page, like blank.html.  Configure this
		/// property so that the SCR of the IFRAME is then blank.html.  This is only needed when using skmMenu over
		/// SSL.</remarks>
		[ 
		Category("Behavior"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies what SRC property should be loaded into an IFRAME."),
		DefaultValue("")
		]
		public string IFrameSrc
		{
			get
			{
				object o = ViewState["MenuIFrameSrc"];
				if (o == null)
					return String.Empty;
				else
					return (string) o;
			}
			set
			{
				ViewState["MenuIFrameSrc"] = value;
			}
		}
		#endregion
	}
}


