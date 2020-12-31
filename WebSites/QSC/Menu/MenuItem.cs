using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace skmMenu
{
	/// <summary>
	/// A MenuItem represents a single item in a menu.</summary>
	/// <remarks>A MenuItem is a single "item" in a menu.  Typically a MenuItem will have some <see cref="Text"/>
	/// associated with it, and often a <see cref="Url"/> or <see cref="CommandName"/>.  MenuItems can also optionally
	/// have a set of <see cref="SubItems"/>, which represents a nested submenu.</remarks>
	[ToolboxItem(false)]
	public class MenuItem : WebControl, IStateManager
	{
		#region Private Member Variables
		// private member variables
		private MenuItemCollection subItems = new MenuItemCollection();
		private RoleCollection roles = new RoleCollection();
		#endregion

		#region Contructors
		/// <summary>
		/// Creates a new MenuItem instance.
		/// </summary>
		public MenuItem() {}		// empty, default constructor
		/// <summary>
		/// Creates a new MenuItem instance.
		/// </summary>
		public MenuItem(string itemText) : this(itemText, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty) {}
		/// <summary>
		/// Creates a new MenuItem instance.
		/// </summary>
		public MenuItem(string itemText, string itemUrl) : this(itemText, itemUrl, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty) {}
		/// <summary>
		/// Creates a new MenuItem instance.
		/// </summary>
		public MenuItem(string itemText, string itemUrl, string itemToolTip) : this(itemText, itemUrl, itemToolTip, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty) {}
		/// <summary>
		/// Creates a new MenuItem instance.
		/// </summary>
		public MenuItem(string itemText, string itemUrl, string itemToolTip, string itemCssClass)  : this(itemText, itemUrl, itemToolTip, itemCssClass, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty) {}
		/// <summary>
		/// Creates a new MenuItem instance.
		/// </summary>
		public MenuItem(string itemText, string itemUrl, string itemToolTip, string itemCssClass, string itemMouseOverCssClass) : this(itemText, itemUrl, itemToolTip, itemCssClass, itemMouseOverCssClass, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty) {}
		/// <summary>
		/// Creates a new MenuItem instance.
		/// </summary>
		public MenuItem(string itemText, string itemUrl, string itemToolTip, string itemCssClass, string itemMouseOverCssClass, string itemMouseDownCssClass, string itemMouseUpCssClass) : this(itemText, itemUrl, itemToolTip, itemCssClass, itemMouseOverCssClass, itemMouseUpCssClass, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty) {}
		/// <summary>
		/// Creates a new MenuItem instance.
		/// </summary>
		public MenuItem(string itemText, string itemUrl, string itemToolTip, string itemCssClass, string itemMouseOverCssClass, string itemMouseDownCssClass, string itemMouseUpCssClass, string itemImage, string itemMouseOverImage) : this(itemText, itemUrl, itemToolTip, itemCssClass, itemMouseOverCssClass, itemMouseUpCssClass, itemMouseOverImage, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty) {}
		/// <summary>
		/// Creates a new MenuItem instance.
		/// </summary>
		public MenuItem(string itemText, string itemUrl, string itemToolTip, string itemCssClass, string itemMouseOverCssClass, string itemMouseDownCssClass, string itemMouseUpCssClass, string itemImage, string itemMouseOverImage, string itemMouseDownImage, string itemMouseUpImage, string itemName)
		{
			if ((itemText == string.Empty || itemText == null) && (itemImage == string.Empty || itemImage == null))
				throw new ArgumentException("Either itemText or itemImage must be specified.");
			Text = itemText;
			Url = itemUrl;
			ToolTip = itemToolTip;
			CssClass = itemCssClass;
			MouseOverCssClass = itemMouseOverCssClass;
			MouseDownCssClass = itemMouseDownCssClass;
			MouseUpCssClass = itemMouseUpCssClass;
			Image = itemImage;
			MouseOverImage = itemMouseOverImage;
			MouseDownImage = itemMouseDownImage;
			MouseUpImage = itemMouseUpImage;
			Name = itemName;
			MenuType = MenuItemType.MenuItem;
		}
		#endregion

		#region IStateManager Implementation
		/// <summary>
		/// This method saves the state for a particular menuitem.</summary>
		/// <remarks>This method returns a Triplet, where the first item is the result from the MenuItem's ViewState's
		/// SaveViewState() method call.  The second is the ViewState saved from the
		/// subItems MenuItemCollection instance.  The third item is the ViewState saved from the
		/// RolesCollection.
		/// </remarks>
		/// <returns>A Triplet containing the ViewState and state of the subitems.</returns>
		object IStateManager.SaveViewState()
		{
			object baseState = ((IStateManager) this.ViewState).SaveViewState();
			object subItemsState = ((IStateManager) this.subItems).SaveViewState();
			object rolesState = ((IStateManager) this.roles).SaveViewState();

			if (baseState == null && subItemsState == null && rolesState == null)
				return null;
			else
				return new Triplet(baseState, subItemsState, rolesState);
		}

		/// <summary>
		/// Loads the state upon postback back into the MenuItem.
		/// </summary>
		/// <param name="savedState">The state preserved from the SaveViewState() method in the
		/// previous page invocation.</param>
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Triplet t = (Triplet) savedState;
				if (t.First != null)
					((IStateManager) this.ViewState).LoadViewState(t.First);
				if (t.Second != null)
					((IStateManager) this.subItems).LoadViewState(t.Second);
				if (t.Third != null)
					((IStateManager) this.roles).LoadViewState(t.Third);
			}
		}

		/// <summary>
		/// Starts tracking view state for the ViewState and subItems properties.
		/// </summary>
		void IStateManager.TrackViewState()
		{
			base.TrackViewState();

			if (subItems != null)
				((IStateManager) subItems).TrackViewState();

			if (roles != null)
				((IStateManager) roles).TrackViewState();
		}
		#endregion

		#region MenuItem Properties
		#region "Web Control Properties"
		/// <summary>
		/// Gets or sets a value indicating whether the Web server control is enabled.
		/// </summary>
		/// <value><b>true</b> if the control is enabled, <b>false</b> otherwise; the default is <b>true</b>.</value>
		public new bool Enabled
		{
			get
			{
				object o = ViewState["MenuItemEnabled"];
				
				if (o != null)
					return (bool) o;
				else
					return true;
			}
			set
			{
				ViewState["MenuItemEnabled"] = value;
				ViewState.SetItemDirty("MenuItemEnabled", true);
			}
		}

		/// <summary>
		/// Gets or sets the background color of the Web server control.
		/// </summary>
		/// <remarks>Use the BackColor property to specify the background color of the Web server control.</remarks>
		public new System.Drawing.Color BackColor
		{
			get
			{
				object o = ViewState["MenuItemBackColor"];
				
				if (o != null)
					return (Color) o;
				else
					return Color.Empty;
			}
			set
			{
				ViewState["MenuItemBackColor"] = value;
				ViewState.SetItemDirty("MenuItemBackColor", true);
			}
		}

		/// <summary>
		/// Gets or sets the border color of the Web control.
		/// </summary>
		/// <value>A System.Drawing.Color that represents the border color of the control. The default is Color.Empty, which indicates that this property is not set.</value>
		/// <remarks>Use the BorderColor property to specify the border color of the Web Server control.</remarks>
		public new System.Drawing.Color BorderColor
		{
			get
			{
				object o = ViewState["MenuItemBorderColor"];
				
				if (o != null)
					return (Color) o;
				else
					return Color.Empty;
			}
			set
			{
				ViewState["MenuItemBorderColor"] = value;
				ViewState.SetItemDirty("MenuItemBorderColor", true);
			}
		}

		/// <summary>
		/// Gets or sets the border style of the Web server control.
		/// </summary>
		/// <value>One of the BorderStyle enumeration values. The default is <b>NotSet</b>.</value>
		/// <remarks>Use the BorderStyle property to specify the border style for the Web server control. This property is set using one of the BorderStyle enumeration values.</remarks>
		public new BorderStyle BorderStyle
		{
			get
			{
				object o = ViewState["MenuItemBorderStyle"];
				
				if (o != null)
					return (BorderStyle) o;
				else
					return BorderStyle.NotSet;
			}
			set
			{
				ViewState["MenuItemBorderStyle"] = value;
				ViewState.SetItemDirty("MenuItemBorderStyle", true);
			}
		}

		/// <summary>
		/// Gets or sets the border width of the Web server control.
		/// </summary>
		/// <value>A Unit that represents the border width of a Web server control. The default value is Unit.Empty, which indicates that this property is not set.</value>
		/// <remarks>Use the BorderWidth property to specify a border width for a control.</remarks>
		public new Unit BorderWidth
		{
			get
			{
				object o = ViewState["MenuItemBorderWidth"];
				
				if (o != null)
					return (Unit) o;
				else
					return Unit.Empty;
			}
			set
			{
				ViewState["MenuItemBorderWidth"] = value;
				ViewState.SetItemDirty("MenuItemBorderWidth", true);
			}
		}

		/// <summary>
		/// Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.
		/// </summary>
		public new string CssClass
		{
			get
			{
				object o = ViewState["MenuItemCssClass"];
				
				if (o != null)
					return (string) o;
				else
					return string.Empty;
			}
			set
			{
				ViewState["MenuItemCssClass"] = value;
				ViewState.SetItemDirty("MenuItemCssClass", true);
			}
		}

		/// <summary>
		/// Gets the font properties associated with the Web server control.
		/// </summary>
		/// <value>A FontInfo that represents the font properties of the Web server control.</value>
		public new FontInfo Font
		{
			get
			{
				object o = ViewState["MenuItemFont"];
				
				if (o != null)
					return (FontInfo) o;
				else
					return null;
			}
			set
			{
				ViewState["MenuItemFont"] = value;
				ViewState.SetItemDirty("MenuItemFont", true);
			}
		}

		
		/// <summary>
		/// Gets or sets the foreground color (typically the color of the text) of the Web server control.
		/// </summary>
		public new Color ForeColor
		{
			get
			{
				object o = ViewState["MenuItemForeColor"];
				
				if (o != null)
					return (Color) o;
				else
					return Color.Empty;
			}
			set
			{
				ViewState["MenuItemForeColor"] = value;
				ViewState.SetItemDirty("MenuItemForeColor", true);
			}
		}

		
		/// <summary>
		/// Gets or sets the height of the Web server control.
		/// </summary>
		public new Unit Height
		{
			get
			{
				object o = ViewState["MenuItemHeight"];
				
				if (o != null)
					return (Unit) o;
				else
					return Unit.Empty;
			}
			set
			{
				ViewState["MenuItemHeight"] = value;
				ViewState.SetItemDirty("MenuItemHeight", true);
			}
		}

		
		/// <summary>
		/// Gets or sets the text displayed when the mouse pointer hovers over the Web server control.
		/// </summary>
		public new string ToolTip
		{
			get
			{
				object o = ViewState["MenuItemToolTip"];
				
				if (o != null)
					return (string) o;
				else
					return string.Empty;
			}
			set
			{
				ViewState["MenuItemToolTip"] = value;
				ViewState.SetItemDirty("MenuItemToolTip", true);
			}
		}
		
		
		/// <summary>
		/// Gets or sets the width of the Web server control.
		/// </summary>
		public new Unit Width
		{
			get
			{
				object o = ViewState["MenuItemWidth"];
				
				if (o != null)
					return (Unit) o;
				else
					return Unit.Empty;
			}
			set
			{
				ViewState["MenuItemWidth"] = value;
				ViewState.SetItemDirty("MenuItemWidth", true);
			}
		}
		#endregion

		/// <summary>
		/// Specifies if the MenuItem is tracking ViewState.  Required, since MenuItem
		/// implements the IStateManager interface.
		/// </summary>
		bool IStateManager.IsTrackingViewState
		{
			get 
			{
				return base.IsTrackingViewState;
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's image.
		/// </summary>
		/// <remarks>If both of the MenuItem's <see cref="Text"/> and <see cref="Image"/> properties are specified, 
		/// <see cref="Text"/> will be used.</remarks>
		[ 
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("The MenuItem's image.")
		]
		public virtual string Image
		{
			get
			{
				object o = ViewState["ItemImage"];
				
				if (o != null)
					return (string) o;
				else
					return string.Empty;
			}
			set
			{
				ViewState["ItemImage"] = value;
				ViewState.SetItemDirty("ItemImage", true);
			}
		}


		/// <summary>
		/// Gets or sets the alternate text for the MenuItem's image.
		/// </summary>
		/// <remarks>This value, if specified, is rendered as the <b>alt</b> attribute in the generated
		/// <b>&lt;img&gt;</b> element.</remarks>
		[ 
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("The alternate text for the MenuItem's image.  Output as the Alt tag.")
		]
		public virtual string ImageAltText
		{
			get
			{
				object o = ViewState["ItemImageAltText"];
				
				if (o != null)
					return (string) o;
				else
					return string.Empty;
			}
			set
			{
				ViewState["ItemImageAltText"] = value;
				ViewState.SetItemDirty("ItemImageAltText", true);
			}
		}

		
		/// <summary>
		/// Gets or sets an image to be shown to the right of the menuitem's text or main image.</summary>
		/// <remarks>You can only have either a <see cref="LeftImage"/> or a <see cref="RightImage"/>.  If both 
		/// are specified, only the <see cref="LeftImage"/> will be used.
		/// </remarks>
		[ 
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("An image to show on the right of the MenuItem's text or main image.")
		]
		public virtual string RightImage
		{
			get
			{
				object o = ViewState["ItemRightImage"];
				
				if (o != null)
					return (string) o;
				else
					return string.Empty;
			}
			set
			{
				ViewState["ItemRightImage"] = value;
				ViewState.SetItemDirty("ItemRightImage", true);
			}
		}

		
		/// <summary>
		/// Gets or sets the width of the space to show an image in on the right of the MenuItem's <see cref="Text"/> or 
		/// main image.
		/// </summary>
		/// <value>The default value is <b>Unit.Empty</b>.</value>
		[ 
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("The width of the space to show an image in on the right of the MenuItem's text or main image.")
		]
		public virtual Unit RightImageLeftPadding
		{
			get
			{
				object o = ViewState["ItemRightImageLeftPadding"];
				
				if (o != null)
					return (Unit) o;
				else
					return Unit.Empty;
			}
			set
			{
				ViewState["ItemRightImageLeftPadding"] = value;
				ViewState.SetItemDirty("ItemRightImageLeftPadding", true);
			}
		}


		/// <summary>
		/// Specifies alignment for an image to be shown to the right of the MenuItem's <see cref="Text"/> or main image.
		/// </summary>
		/// <value>Set to one of the values of the <b>System.Web.UI.WebControls.ImageAlign</b> enumeration.
		/// The default is <b>ImageAlign.NotSet</b></value>
		[ 
		Category("Behavior"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies alignment for an image to be shown to the right of the MenuItem's text or main image.")
		]
		public virtual System.Web.UI.WebControls.ImageAlign RightImageAlign
		{
			get
			{
				object o = ViewState["ItemRightImageAlign"];
				if (o == null)
					return System.Web.UI.WebControls.ImageAlign.NotSet;
				else
					return (System.Web.UI.WebControls.ImageAlign) o;
			}
			set
			{
				ViewState["ItemRightImageAlign"] = value;
				ViewState.SetItemDirty("ItemRightImageAlign", true);
			}
		}
			
		
		/// <summary>
		/// Gets or sets an image to be shown to the left of the MenuItem's <see cref="Text"/> or main image.		
		/// </summary>
		/// <remarks>You can only have either a <see cref="LeftImage"/> or a <see cref="RightImage"/>.  If both are 
		/// specified, only the <see cref="LeftImage"/> will be used.
		/// </remarks>
		[ 
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("An image to show on the left of the MenuItem's text or main image.")
		]
		public virtual string LeftImage
		{
			get
			{
				object o = ViewState["ItemLeftImage"];
				
				if (o != null)
					return (string) o;
				else
					return string.Empty;
			}
			set
			{
				ViewState["ItemLeftImage"] = value;
				ViewState.SetItemDirty("ItemLeftImage", true);
			}
		}

		
		/// <summary>
		/// Gets or sets the width of the space to show an image in on the left of the MenuItem's <see cref="Text"/> or main image.
		/// </summary>
		/// <value>The default value is <b>Unit.Empty</b>.</value>
		[ 
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("The width of the space to show an image in on the left of the MenuItem's text or main image.")
		]
		public virtual Unit LeftImageRightPadding
		{
			get
			{
				object o = ViewState["ItemLeftImageRightPadding"];
				
				if (o != null)
					return (Unit) o;
				else
					return Unit.Empty;
			}
			set
			{
				ViewState["ItemLeftImageRightPadding"] = value;
				ViewState.SetItemDirty("ItemLeftImageRightPadding", true);
			}
		}


		/// <summary>
		/// Specifies alignment for an image to be shown to the left of the MenuItem's <see cref="Text"/> or main image.
		/// </summary>
		/// <value>Set to one of the values of the <b>System.Web.UI.WebControls.ImageAlign</b> enumeration.
		/// The default is <b>ImageAlign.NotSet</b></value>
		[ 
		Category("Behavior"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies alignment for an image to be shown to the left of the MenuItem's text or main image.")
		]
		public virtual System.Web.UI.WebControls.ImageAlign LeftImageAlign
		{
			get
			{
				object o = ViewState["ItemLeftImageAlign"];
				if (o == null)
					return System.Web.UI.WebControls.ImageAlign.NotSet;
				else
					return (System.Web.UI.WebControls.ImageAlign) o;
			}
			set
			{
				ViewState["ItemLeftImageAlign"] = value;
				ViewState.SetItemDirty("ItemLeftImageAlign", true);
			}
		}
			

		/// <summary>
		/// Gets or sets the MenuItem's mouseover image.
		/// </summary>
		/// <remarks>The client-side <b>mouseover</b> event fires when the user's mouse moves over the MenuItem.</remarks>
		[ 
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("The image displayed when the mouse is moved over the MenuItem.")
		]
		public virtual string MouseOverImage
		{
			get
			{
				object o = ViewState["ItemMouseOverImage"];
				if (o != null)
					return (string) o;
				else
					return string.Empty;
			}
			set
			{
				ViewState["ItemMouseOverImage"] = value;
				ViewState.SetItemDirty("ItemMouseOverImage", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's mouseup image.
		/// </summary>
		[ 
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("The image displayed on mouse up over the MenuItem.")
		]
		public virtual string MouseUpImage
		{
			get
			{
				object o = ViewState["ItemMouseUpImage"];
				if (o != null)
					return (string) o;
				else
					return string.Empty;
			}
			set
			{
				ViewState["ItemMouseUpImage"] = value;
				ViewState.SetItemDirty("ItemMouseUpImage", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's mousedown image.
		/// </summary>
		/// <remarks>The client-side <b>mousedown</b> event fires when the mouse is over the MenuItem
		/// and the uses clicks the mouse button.</remarks>
		[ 
		Category("Appearance"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("The image displayed on mousedown over the menuitem.")
		]
		public virtual string MouseDownImage
		{
			get
			{
				object o = ViewState["ItemMouseDownImage"];
				if (o != null)
					return (string) o;
				else
					return string.Empty;
			}
			set
			{
				ViewState["ItemMouseDownImage"] = value;
				ViewState.SetItemDirty("ItemMouseDownImage", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's name.
		/// </summary>
		/// <value>A string value denoting the MenuItem's <b>Name</b>.  The default is an empty string.</value>
		/// <remarks>Use the <b>Name</b> property to give a unique, identifying name to a MenuItem instance.
		/// The <see cref="MenuItemCollection"/> class, which contains a collection of MenuItem instances, can
		/// be searched for a MenuItem with a specified <b>Name</b>.</remarks>
		[
		Description("The menuitem's name."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual string Name
		{
			get
			{
				object o = ViewState["ItemName"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemName"] = value;
				ViewState.SetItemDirty("ItemName", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's text content.
		/// </summary>
		/// <remarks>For total customization of the appearance of the MenuItem, the <b>Text</b> property can have 
		/// HTML content.</remarks>
		/// <example>
		/// The following example illustrates using HTML content in the <b>Text</b> property:<p />[C#]
		/// <code>
		/// MenuItem mi = new MenuItem();
		/// mi.Text = "&lt;b&gt;This will be bold!&lt;/b&gt;";
		/// </code>
		/// </example>
		[
		Description("The MenuItem's text."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual string Text
		{
			get
			{
				object o = ViewState["ItemText"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemText"] = value;
				ViewState.SetItemDirty("ItemText", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's Url.
		/// </summary>
		/// <remarks>If a MenuItem has a <b>Url</b> value, the MenuItem is "clickable."  That is, the end user
		/// will be able to click the MenuItem and be whisked to the specified URL.<p />The <b>Url</b> value can use
		/// the ~ notation.  For this to work, though, the Menu class's <b>DefaultResolveUrl</b> property must be
		/// set to True.</remarks>
		/// <value>Specifies the URL the user will be whisked to when the MenuItem is clicked.  The default value
		/// is <b>String.Empty</b>.  The <b>Url</b> property is optional.</value>
		[
		Description("The optional URL for the MenuItem."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual string Url
		{
			get
			{
				object o = ViewState["ItemURL"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemURL"] = value;
				ViewState.SetItemDirty("ItemURL", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's Javascript command.
		/// </summary>
		[
		Description("The optional javascript command for the menuitem."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual string JavascriptCommand
		{
			get
			{
				object o = ViewState["ItemJavascriptCommand"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemJavascriptCommand"] = value;
				ViewState.SetItemDirty("ItemJavascriptCommand", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's target used when the <see cref="Url"/> is navigated to.
		/// </summary>
		/// <remarks>The <see cref="Url"/> property must be set to a non-empty string for <b>Target</b> to have any affect.
		/// <p />To have a MenuItem opened in a new window, set <b>Target</b> to _blank</remarks>
		[
		Description("The optional target for the MenuItem URL."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual string Target
		{
			get
			{
				object o = ViewState["ItemTarget"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemTarget"] = value;
				ViewState.SetItemDirty("ItemTarget", true);
			}
		}


		/// <summary>
		/// Retrieves the MenuItem's set of SubItems.
		/// </summary>
		/// <remarks>The <b>SubItems</b> collection is useful when programmatically creating or modifying
		/// a menu's content.</remarks>
		[
		Category("Behavior"),
		Description("The collection of submenu items."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		PersistenceMode(PersistenceMode.InnerProperty)
		]		
		public virtual MenuItemCollection SubItems
		{
			get
			{
				if (base.IsTrackingViewState)
					((IStateManager) subItems).TrackViewState();

				return this.subItems;
			}
		}


		/// <summary>
		/// Specifies the MenuItem's roles.
		/// </summary>
		[
		Category("Behavior"),
		Description("Indicates the menu item's roles.")
		]
		public virtual RoleCollection Roles
		{
			get
			{
				if (base.IsTrackingViewState)
					((IStateManager) roles).TrackViewState();

				return this.roles;
			}
		}



		/// <summary>
		/// Gets or sets the MenuItem's ID.  It is not recommended that this be set directly.
		/// </summary>
		/// <remarks>The <b>ID</b> for each MenuItem is programmatically set in the <see cref="Menu"/> class's
		/// <see cref="Menu.BuildMenuItem"/> method.</remarks>
		[ Browsable(false) ]
		public virtual string MenuID
		{
			get
			{
				object o = ViewState["ItemID"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemID"] = value;
				ViewState.SetItemDirty("ItemID", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's type.  It is not recommended that this be set directly.
		/// </summary>
		/// <value>The MenuItem type can be one of the available <see cref="MenuItemType"/> enumeration values.
		/// The default is <b>MenuItemType.MenuItem</b>.</value>
		[
		Description("The type of menuitem."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual MenuItemType MenuType
		{
			get
			{
				object o = ViewState["ItemType"];
				if (o != null)
					return (MenuItemType) o;
				else
					return MenuItemType.MenuItem;
			}
			set
			{
				ViewState["ItemType"] = value;
				ViewState.SetItemDirty("ItemType", true);
			}
		}


		/// <summary>
		/// Gets or Sets the CommandName property.
		/// </summary>
		/// <remarks>If a MenuItem's <b>CommandName</b> property is set, and the <see cref="Url"/> property is
		/// <i>not</i> set, then the MenuItem, when clicked, will cause the Web Form to postback, and a
		/// MenuItemClicked event will be raised.</remarks>
		[
		Category("Behavior"),
		Description("The optional command name for the menuitem."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual string CommandName
		{
			get
			{
				object o = ViewState["ItemCommandName"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemCommandName"] = value;
				ViewState.SetItemDirty("ItemCommandName", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's mouseover CSS class.
		/// </summary>
		[
		Category("Appearance"),
		Description("The menuitem's mouse over stylesheet class."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual string MouseOverCssClass
		{
			get
			{
				object o = ViewState["ItemMouseOverCssClass"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemMouseOverCssClass"] = value;
				ViewState.SetItemDirty("ItemMouseOverCssClass", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's mouseup CSS class.
		/// </summary>
		[
		Category("Appearance"),
		Description("The MenuItem's mouse up stylesheet class."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual string MouseUpCssClass
		{
			get
			{
				object o = ViewState["ItemMouseUpCssClass"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemMouseUpCssClass"] = value;
				ViewState.SetItemDirty("ItemMouseUpCssClass", true);
			}
		}


		/// <summary>
		/// Gets or sets the MenuItem's mousedown CSS class.
		/// </summary>
		[
		Category("Appearance"),
		Description("The MenuItem's mouse down stylesheet class."),
		NotifyParentProperty(true),
		DefaultValue("")
		]
		public virtual string MouseDownCssClass
		{
			get
			{
				object o = ViewState["ItemMouseDownCssClass"];
				if (o != null)
					return (string) o;
				else
					return String.Empty;
			}
			set
			{
				ViewState["ItemMouseDownCssClass"] = value;
				ViewState.SetItemDirty("ItemMouseDownCssClass", true);
			}
		}
		

		/// <summary>
		/// Specifies whether URL should be resolved before being output.  </summary>
		/// <remarks>If either <see cref="Menu.DefaultResolveURL"/> in the <see cref="Menu"/> class is true or this value is true, 
		/// then the URL for the menuitem will be resolved.
		/// </remarks>
		[ 
		Category("Behavior"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies whether URL should be resolved before being output.  If either DefaultResolveURL in the MenuClass is true or this value is true, then the URL for the menuitem will be resolved.")
		]
		public virtual bool ResolveURL
		{
			get
			{
				object o = ViewState["ItemResolveURL"];
				if (o == null)
					return false;
				else
					return (bool) o;
			}
			set
			{
				ViewState["ItemResolveURL"] = value;
				ViewState.SetItemDirty("ItemResolveURL", true);
			}
		}
		

		/// <summary>
		/// Specifies horizontal alignment for the MenuItem.
		/// </summary>
		/// <value>One of the horizontal alignment options from the <b>System.Web.UI.WebControls.HorizontalAlign</b> enumeration.
		/// The default is <b>HorizontalAlign.NotSet</b>.</value>
		/// <remarks>The horizontal alignment indicates how the text and/or images of the MenuItem are aligned.</remarks>
		[ 
		Category("Behavior"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies horizontal alignment for the MenuItem.")
		]
		public virtual System.Web.UI.WebControls.HorizontalAlign HorizontalAlign
		{
			get
			{
				object o = ViewState["ItemHorizontalAlign"];
				if (o == null)
					return System.Web.UI.WebControls.HorizontalAlign.NotSet;
				else
					return (System.Web.UI.WebControls.HorizontalAlign) o;
			}
			set
			{
				ViewState["ItemHorizontalAlign"] = value;
				ViewState.SetItemDirty("ItemHorizontalAlign", true);
			}
		}
			

		/// <summary>
		/// Specifies vertical alignment for the MenuItem.
		/// </summary>
		/// <value>One of the vertical alignment options from the <b>System.Web.UI.WebControls.VerticalAlign</b> enumeration.
		/// The default is <b>VerticalAlign.NotSet</b>.</value>
		[ 
		Category("Behavior"),
		PersistenceMode(PersistenceMode.Attribute),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		NotifyParentProperty(true),
		Description("Specifies vertical alignment for the MenuItem.")
		]
		public virtual System.Web.UI.WebControls.VerticalAlign VerticalAlign
		{
			get
			{
				object o = ViewState["ItemVerticalAlign"];
				if (o == null)
					return System.Web.UI.WebControls.VerticalAlign.NotSet;
				else
					return (System.Web.UI.WebControls.VerticalAlign) o;
			}
			set
			{
				ViewState["ItemVerticalAlign"] = value;
				ViewState.SetItemDirty("ItemVerticalAlign", true);
			}
		}
		#endregion
	}
}
