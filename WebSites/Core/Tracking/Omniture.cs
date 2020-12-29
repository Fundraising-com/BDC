using System;

namespace GA.BDC.Core.Tracking
{
	/// <summary>
	/// Summary description for Omniture.
	/// </summary>
	

	using System.Collections;

	/// <summary>
	/// Omniture SiteCatalyst Class.<para/>
	/// Used to populate and build the client script block to be included on pages to track.
	/// </summary>
	public abstract class Omniture
	{
		#region Protected
		
		protected ArrayList Props = new ArrayList();
		protected ArrayList EVars = new ArrayList();
		protected ArrayList Products = new ArrayList();
		protected ArrayList Events = new ArrayList();
		protected string prefixVar
		{
			get
			{
				if (_prefixVar ==null || _prefixVar == string.Empty)
					return "s.";
				else
					return _prefixVar;
			}
			set
			{
				_prefixVar = value;
			}
		}
		protected abstract string PageFormat
		{
			get ;
		}
		#endregion

		#region Declarations
		private string sPageName;
		private string sServer;
		private string sChannel;
		private string sPageType;
		private string sCampaign;
		private string sState;
		private string sZip;
		private string sPurchaseId;
		private double dTax;
		private double dShipping;
		private string jsFileName;
		#endregion item declarations

		#region Public Properties


		public string PageType
		{
			get {return sPageType;}
		}

		public string JSFileName
		{
			get
			{
				return jsFileName;
			}

			set
			{
				jsFileName = value;
			}
		}

		/// <summary>
		/// Controls the SiteCatalyst s.pageName traffic property.<para/>
		/// SiteCatalyst will use the value of this property when referencing this page on reports.  
		/// Any pages that have the same s.pageName value will be considered the same page by SiteCatalyst.<para/>
		/// If this is not populated, SiteCatalyst will use the URL of the page by default.
		/// </summary>
		/// <example>
		/// /Index.aspx
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// objOmniture.PageName = "QSP US: Home";
		/// </code>
		/// /Store/Search.aspx
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// objOmniture.PageName = "QSP US: Store: Search";
		/// </code>
		/// /Email/ImportAddressBook.aspx
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// objOmniture.PageName = "QSP US: Email Collection: Import your Address Book";
		/// </code>
		/// </example>
		protected string PageName
		{
			get	{ return sPageName; }
			set	{ sPageName = value; this.AddEVar_Custom( 1, value );}  
		}

		public string GetPageName()
		{
			return PageName;
		}
	
		/// <summary>
		/// Controls the SiteCatalyst s.server traffic property.<para/>
		/// This should be populated with the IP address of the server hosting this page.  
		/// This will allow us to break down traffic reporting in load-balanced scenarios.
		/// </summary>
		public string Server
		{
			get	{ return sServer; }
			set	{ sServer = value; }  
		}

		/// <summary>
		/// Controls the SiteCatalyst s.channel traffic property.<para/>
		/// This should be populated with the logical site section that the page is in.  
		/// </summary>
		/// <example>
		/// QSP US: Store
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// objOmniture.Channel = "QSP US: Store";
		/// </code>
		/// QSP US: MyQSP
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// objOmniture.Channel = "QSP US: MyQSP";
		/// </code>
		/// QSP US: Email Collection
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// objOmniture.Channel = "QSP US: Email Collection";
		/// </code>
		/// </example>
		public string Channel
		{
			get	{ return sChannel; }
			set	{ sChannel = value; }  
		}

		/// <summary>
		/// Controls the SiteCatalyst s.campaign commerce property.<para/>
		/// This should be populated with external tracking codes (ie: banners, referrers, etc).
		/// </summary>
		public string Campaign
		{
			get	{ return sCampaign; }
			set	
			{ 
				sCampaign = value; 
				//this.AddProp_Custom( 11, value ); 
			}  
		}

		/// <summary>
		/// Controls the SiteCatalyst s.state commerce property.<para/>
		/// This should only be populated at checkout time with the billing address state.
		/// </summary>
		public string State
		{
			get	{ return sState; }
			set	{ sState = value; }  
		}

		/// <summary>
		/// Controls the SiteCatalyst s.zip commerce property.<para/>
		/// This should only be populated at checkout time with the billing address zip code.
		/// </summary>
		public string Zip
		{
			get	{ return sZip; }
			set	{ sZip = value; }  
		}

		/// <summary>
		/// Controls the SiteCatalyst s.purchaseId commerce property.<para/>
		/// This should only be populated at checkout time with the EDS order number.
		/// </summary>
		public string PurchaseId
		{
			get	{ return sPurchaseId; }
			set	{ sPurchaseId = value; }  
		}

		/// <summary>
		/// Sets the amount of tax associated with an order.<para/>
		/// If this is set to a non-zero value when the <see cref="Omniture.AddEvent_CheckoutComplete"/> 
		/// method is invoked, the tax amount will be transmitted to Omniture via a custom event.
		/// </summary>
		/// <seealso cref="Omniture.AddEvent_CheckoutComplete"/>
		public double Tax
		{
			get	{ return dTax; }
			set	{ dTax = value; }  
		}

		/// <summary>
		/// Sets the shipping amount associated with an order.<para/>
		/// If this is set to a non-zero value when the <see cref="Omniture.AddEvent_CheckoutComplete"/> 
		/// method is invoked, the shipping amount will be transmitted to Omniture via a custom event.
		/// </summary>
		/// <seealso cref="Omniture.AddEvent_CheckoutComplete"/> 
		public double Shipping
		{
			get	{ return dShipping; }
			set	{ dShipping = value; }  
		}

		#endregion

		#region Public Methods


		public abstract void SetPageNameAndCategory(string PageCategory, string PageName);

		/// <summary>
		/// Constructor.  Creates an empty SiteCatalyst script block to populate.<para/>
		/// Automatically populates custom event 1 (the page view event), which should happen on every page.
		/// </summary>
		public Omniture()
		{
			this.Clear();
		}


		/// <summary>
		/// Constructor.  Creates an empty SiteCatalyst script block to populate.<para/>
		/// Automatically populates custom event 1 (the page view event), which should happen on every page.
		/// </summary>
		/// <param name="JavaSscriptFileName">The full path of javascript file containing most of the tracking code that Omniture will reference.</param>
		public Omniture(string JavascriptFileName)
		{
			this.Clear();
			this.JSFileName = JavascriptFileName;
		}

		/// <summary>
		/// Removes all populated information from the script block, completely resetting it.<para/> 
		/// Custom event 1 (the page view event) remains, since it should always happen.
		/// </summary>
		public void Clear()
		{
			sPageName = "";
			sServer = "";
			sChannel = "";
			sPageType = "";
			sCampaign = "";
			sState = "";
			sZip = "";
			sPurchaseId = "";
			Props.Clear();
			EVars.Clear();
			Products.Clear();
			Events.Clear();
			dTax = 0;
			dShipping = 0;
			JSFileName = string.Empty;

			AddEvent_Custom( 1 );
		}

		/// <summary>
		/// Sets the Omniture s.pageType variable to flag this page as a HTTP 404 error page.<para/>
		/// This should currently only be set on the custom 404 error page.
		/// </summary>
		/// <example>
		/// To tell SiteCatalyst that this is a custom HTTP 404 error page:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Flag this as an error page
		/// objOmniture.SetErrorPage();
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void SetErrorPage() 
		{
			sPageType = "errorPage";
		}

		/// <summary>
		/// Adds a custom Omniture sProp traffic variable to the client script block.
		/// </summary>
		/// <param name="iIndex">The number of the sProp.</param>
		/// <param name="sValue">The value of the sProp.</param>
		/// <example>
		/// To add custom prop 24 to the page (ie: make s.prop24="some value" appear), use:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Add the custom prop to the script block
		/// objOmniture.AddProp_Custom( 24, "some value" );
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddProp_Custom( int iIndex, string sValue ) 
		{
			Props.Add ( new Prop( iIndex, sValue ) );
		}

		/// <summary>
		/// Adds an Omniture sProp traffic variable to the client script block 
		/// which will capture an internal tracking id (internal banners, email ids, etc).  
		/// This method also populates a custom eVar to duplicate the capture on the 
		/// commerce side of SiteCatalyst.
		/// </summary>
		/// <param name="sValue">The internal tracking id.</param>
		/// <example>
		/// To tell SiteCatalyst about an internal tracking id that was captured on this page:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // add the tracking id to the script block
		/// objOmniture.InternalTrackingId( "trk123" );
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddProp_InternalTrackingId ( string sValue )
		{
			this.AddProp_Custom( 12, sValue );
			this.AddEVar_Custom( 2, sValue );
		}

		/// <summary>
		/// Adds several Omniture sProp traffic variables to the client script block 
		/// which will capture search parameters.  The term itself is also captured as a custom 
		/// eVar on the commerce side of SiteCatalyst.<para/>
		/// This should always be populated on search result pages.
		/// </summary>
		/// <param name="sSearchTerm">The search phrase that the user entered.</param>
		/// <param name="iResults">The number of results that the search returned.</param>
		/// <param name="sPage">Description of the page where the search was performed.</param>
		/// <param name="sLocation">Description of the location of the page of the search box.</param>
		/// <example>
		/// Below is an example on how to capture search results with SiteCatalyst:
		/// <code>
		/// // The user searched for "foo"
		/// string sSearch = "foo";
		/// 
		/// // ... and found 12 results
		/// int iItems = 12;
		/// 
		/// // The search occured on the store homepage
		/// string sSearchPage = "Store Homepage";
		/// 
		/// // ... in the search box in the header
		/// string sSeachLocation = "Header";
		/// 
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst about the search
		/// objOmniture.AddProp_SearchTerms ( sSearch, iItems, sSearchPage, sSearchLocation );
		/// </code>
		/// </example>
		public void AddProp_SearchTerms ( string sSearchTerm, int iResults, string sPage, string sLocation) 
		{
			this.AddProp_Custom( 13, sSearchTerm.ToLower() );
			this.AddProp_Custom( 14, Convert.ToString(iResults) );
			this.AddProp_Custom( 15, sPage );
			this.AddProp_Custom( 16, sLocation );

			this.AddEVar_Custom( 3, sSearchTerm.ToLower() );

			this.AddEvent_Custom( 7 );
		}

		/*
		/// <summary>
		/// Adds an Omniture sProp traffic variable to the client script block 
		/// which will capture logged-in status.
		/// </summary>
		/// <param name="iLoginStatus">
		/// The login status of the current user.  
		/// 0 = Logged out.  1 = Recognized.  2 = Fully authenticated.
		/// </param>
		/// <example>
		/// Use in the following manner to indicate login status:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that this user has not logged in
		/// objOmniture.AddProp_LoggedIn( 0 );
		/// 
		/// // Tell SiteCatalyst that we recognize this user (via cookie, etc), but they haven't fully authenticated
		/// objOmniture.AddProp_LoggedIn( 1 );
		/// 
		/// // Tell SiteCatalyst that this user has fully authenticated by entering their password
		/// objOmniture.AddProp_LoggedIn( 2 );
		/// </code>
		/// </example>
		public void AddProp_LoggedIn ( int iLoginStatus ) 
		{
			string sLoginText = "";

			switch ( iLoginStatus )
			{
				case 0: sLoginText = "Logged out"; break;
				case 1: sLoginText = "Recognized"; break;
				case 2: sLoginText = "Fully authenticated"; break;
			}

			this.AddProp_Custom( 17, sLoginText );
		}

		/// <summary>
		/// Adds an Omniture sProp traffic variable to the client script block 
		/// which will capture users opting-out of promotional emails.  Note that this can be 
		/// invoked with false to send an opt-in event to SiteCatalyst.
		/// </summary>
		/// <param name="bOptOut">Is the user opting-out of future emails?</param>
		/// <example>
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that this user has opted-out of future emails
		/// objOmniture.AddProp_OptOutStatus( true );
		/// 
		/// // Tell SiteCatalyst that this user has opted-in of future emails
		/// objOmniture.AddProp_OptOutStatus( false );
		/// </code>
		/// </example>
		public void AddProp_OptOutStatus ( bool bOptOut ) 
		{
			if ( bOptOut ) 
			{
				this.AddProp_Custom( 10, "Email" );
			} 
			else
			{
				this.AddProp_Custom( 9, "Email" );
			}
		}

		/// <summary>
		/// Adds an Omniture sProp traffic variable to the client script block 
		/// which will capture locale information associated with the current user.
		/// </summary>
		/// <param name="sLocale">The locale associated with the current user.  Example: en-US</param>
		/// <example>
		/// To tell SiteCatalyst that the en-CA locale applies to this user:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Populate the locale value
		/// objOmniture.AddProp_Locale( "en-CA" );
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddProp_Locale ( string sLocale )
		{
			this.AddProp_Custom( 21, sLocale );
		}

		/// <summary>
		/// Tells SiteCatalyst what organization that this user is currently attached to.<para/>
		/// For the US site, use the 9-digit EDS account number.
		/// </summary>
		/// <param name="sOrganizationId">The organization number.</param>
		public void AddProp_Organization ( string sOrganizationId )
		{
			this.AddProp_Custom( 25, sOrganizationId );
			this.AddEVar_Custom( 17, sOrganizationId );
		}

		*/

		/// <summary>
		/// Adds a custom Omniture eVar commerce variable to the client script block.
		/// </summary>
		/// <param name="iIndex">The number of the eVar.</param>
		/// <param name="sValue">The value of the eVar.</param>
		/// <example>
		/// To add a value for eVar8 (ie: make s.eVar8="some value" appear on the page), use:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Assign a value for eVar8
		/// objOmniture.AddEVar_Custom( 8, "some value" );
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddEVar_Custom( int iIndex, string sValue ) 
		{
			EVars.Add ( new EVar( iIndex, sValue ) );
		}

		/*
		/// <summary>
		/// Tells SiteCatalyst that a user's search generated a successful click-through.<para/>
		/// This should be populated only if a user clicks a search result.
		/// </summary>
		public void AddEVar_SearchSuccess() 
		{
			this.AddEVar_Custom( 4, "success" );
		}*/

		/// <overloads>
		/// Adds a product to the Omniture s.products commerce variable.<para/>
		/// Does nothing by itself; must be used in combination with event methods such as: 
		/// <see cref="Omniture.AddEvent_ProductView"/>, <see cref="AddEvent_ShoppingCartAdd"/>, 
		/// <see cref="Omniture.AddEvent_ShoppingCartRemove"/>, <see cref="Omniture.AddEvent_CheckoutComplete"/>, etc.<para/>
		/// <note>This does NOT persist on Omniture's servers and must be re-set on every page.</note>
		/// </overloads>
		/// <summary>
		/// Adds a product to the Omniture s.products commerce variable with quantity and price info.<para/> 
		/// This should only be used with <see cref="Omniture.AddEvent_CheckoutComplete"/>.  
		/// Quantity and price information are ignored with all other events.
		/// </summary>
		/// <param name="sProductName">The name of the product.</param>
		/// <param name="iQuantity">The quantity purchased.</param>
		/// <param name="dPrice">The TOTAL price of the added item(s).</param>
		public void AddProduct( string sProductName, int iQuantity, double dPrice ) 
		{
			Products.Add ( new Product( sProductName, iQuantity, dPrice ) );
		}

		/// <summary>
		/// Adds a product to the Omniture s.products commerce variable.
		/// </summary>
		/// <param name="sProductName">The name of the product.</param>
		/// <seealso cref="Omniture.AddEvent_ProductView"/> 
		/// <seealso cref="Omniture.AddEvent_ShoppingCartAdd"/> 
		/// <seealso cref="Omniture.AddEvent_ShoppingCartRemove"/> 
		/// <seealso cref="Omniture.AddEvent_CheckoutBegin"/> 
		/// <seealso cref="Omniture.AddEvent_CheckoutComplete"/> 
		public void AddProduct( string sProductName ) 
		{
			Products.Add ( new Product( sProductName ) );
		}

		/// <summary>
		/// Adds a custom event to the Omniture s.events commerce variable.  
		/// </summary>
		/// <param name="iCustomEvent">The custom event number to add.</param>
		/// <example>
		/// To fire custom event 7 (ie: make s.events="event7" appear on the page), use:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that custom event 7 has occured
		/// objOmniture.AddEvent_Custom(7);
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddEvent_Custom( int iCustomEvent ) 
		{
			string sCustomEvent = "event" + iCustomEvent;
			Events.Add ( sCustomEvent );
		}
		
		
		// added 2006-10-12 Maxime Normand
		public void AddEvent_Serialized( int iCustomEvent, int serialNumber ) 
		{
			string sCustomEvent = "event" + iCustomEvent + ":" + serialNumber;
			Events.Add ( sCustomEvent );
		}
		
		/*

		/// <summary>
		/// Tells SiteCatalyst that this user viewed a form page.<para/>
		/// Ensure that the same name is always passed when referring to the same form.  
		/// This name must also be used when calling <see cref="Omniture.AddEvent_FormSubmitted"/>.  
		/// For example, the Registration form might always be called "registration".
		/// </summary>
		/// <param name="sWhichForm">Which form the user viewed.</param>
		/// <seealso cref="Omniture.AddEvent_FormSubmitted"/> 
		/// <example>
		/// The following example shows how this might be used:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that a customer viewed the customer service form
		/// objOmniture.AddEvent_FormViewed("customer service");
		/// </code>
		/// </example>
		public void AddEvent_FormViewed( string sWhichForm )
		{
			this.AddEvent_Custom( 4 );
			this.AddEVar_Custom( 5, sWhichForm.ToLower() );
		}

		/// <summary>
		/// Tells SiteCatalyst that this user successfully submitted a form.<para/>
		/// Ensure that the same name is always passed when referring to the same form.  
		/// This name must also be used when calling <see cref="Omniture.AddEvent_FormViewed"/>.  
		/// For example, the Registration form might always be called "registration".
		/// </summary>
		/// <param name="sWhichForm">Which form the user submitted.</param>
		/// <seealso cref="Omniture.AddEvent_FormViewed"/> 
		/// <example>
		/// The following example shows how this might be used:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that a customer submitted an issue via the customer service form
		/// objOmniture.AddEvent_FormSubmitted("customer service");
		/// </code>
		/// </example>
		public void AddEvent_FormSubmitted( string sWhichForm )
		{
			this.AddEvent_Custom( 5 );
			this.AddEVar_Custom( 5, sWhichForm.ToLower() );
		}
		*/

		/// <summary>
		/// Tells SiteCatalyst that a product was viewed on this page.<para/>
		/// Use with <see cref="Omniture.AddProduct"/> to specify the product(s).
		/// </summary>
		/// <seealso cref="Omniture.AddProduct"/> 
		/// <example>
		/// The following example shows how to set the commerce variables for product views:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that a product has been viewed
		/// objOmniture.AddEvent_ProductView();
		/// 
		/// // Tell SiteCatalyst what product was viewed
		/// objOmniture.AddProduct("Some Product");
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddEvent_ProductView()
		{
			Events.Add ( "prodView" );
		}

		/// <summary>
		/// Tells SiteCatalyst that the shopping cart was viewed on this page.<para/>
		/// Should only be set when a user navigates to the cart page.
		/// </summary>
		public void AddEvent_ShoppingCartView()
		{
			Events.Add ( "scView" );
		}

		/// <summary>
		/// Tells SiteCatalyst that a product was added to the shopping cart.<para/>
		/// Use with <see cref="Omniture.AddProduct"/> to specify the product(s).
		/// </summary>
		/// <seealso cref="Omniture.AddProduct"/>
		/// <example>
		/// The following example shows how to set the commerce variables for adding a product to the cart:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that a product has been added to the cart
		/// objOmniture.AddEvent_ShoppingCartAdd();
		/// 
		/// // Tell SiteCatalyst what product we're talking about
		/// objOmniture.AddProduct("Some Product");
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddEvent_ShoppingCartAdd()
		{
			Events.Add ( "scAdd" );
		}

		/// <summary>
		/// Tells SiteCatalyst that a product was removed from the shopping cart.<para/>
		/// Use with <see cref="Omniture.AddProduct"/> to specify the product(s).
		/// </summary>
		/// <seealso cref="Omniture.AddProduct"/>
		/// <example>
		/// The following example shows how to set the commerce variables for removing a product from the cart:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that a product has been removed from the cart
		/// objOmniture.AddEvent_ShoppingCartRemove();
		/// 
		/// // Tell SiteCatalyst what product we're talking about
		/// objOmniture.AddProduct("Some Product");
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddEvent_ShoppingCartRemove()
		{
			Events.Add ( "scRemove" );
		}

		/// <summary>
		/// Tells SiteCatalyst that the user has started the checkout process.<para/>
		/// Use with <see cref="Omniture.AddProduct"/> to specify the product(s) 
		/// that the user has started to purchase.
		/// </summary>
		/// <seealso cref="Omniture.AddProduct"/>
		/// <example>
		/// The following example shows how to set the commerce variables for beginning checkout:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that the user has started the purchase process
		/// objOmniture.AddEvent_CheckoutBegin();
		/// 
		/// // Tell SiteCatalyst what product(s) they have started to purchase
		/// objOmniture.AddProduct("Some Product");
		/// objOmniture.AddProduct("Another Product");
		/// objOmniture.AddProduct("Yet another Product");
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddEvent_CheckoutBegin()
		{
			Events.Add ( "scCheckout" );
		}

		/// <summary>
		/// Tells SiteCatalyst that the user has finished checkout and completed a purchase.<para/>
		/// This should always be used in tandem with <see cref="Omniture.AddProduct"/>, 
		/// <see cref="Omniture.State"/>, <see cref="Omniture.Zip"/>, and <see cref="Omniture.PurchaseId"/> 
		/// to populate the products purchased, the billing address info, and the EDS order ID.
		/// </summary>
		/// <seealso cref="Omniture.AddProduct"/> 
		/// <seealso cref="Omniture.State"/> 
		/// <seealso cref="Omniture.Zip"/> 
		/// <seealso cref="Omniture.PurchaseId"/> 
		/// <example>
		/// The following example shows how to set the commerce variables for a typical purchase:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 		
		/// // Tell SiteCatalyst that a purchase has occured
		/// objOmniture.AddEvent_CheckoutComplete();
		/// 
		/// // Tell SiteCatalyst what product(s) are in the order - use full quantity and price detail
		/// objOmniture.AddProduct("Some Product", 1, 14.95);
		/// objOmniture.AddProduct("Another Product", 3, 3.95);
		/// objOmniture.AddProduct("Yet another Product", 1, 8.95);
		/// 
		/// // Populate the billing address information
		/// objOmniture.State = "NY";
		/// objOmniture.Zip = "10570";
		/// 
		/// // Populate the EDS order ID
		/// objOmniture.PurchaseId = "50622781";
		/// 
		/// // sScript now contains the Javascript block that should be placed on the page
		/// string sScript = objOmniture.FetchScriptBlock();
		/// </code>
		/// </example>
		public void AddEvent_CheckoutComplete()
		{
			Events.Add ( "purchase" );
		}

		/*
		/// <overloads>
		/// Tells SiteCatalyst that a user has logged in.
		/// </overloads>
		/// <summary>
		/// Should be used for normal user logins.  The login type is set to "user" and sent to SiteCatalyst.
		/// </summary>
		public void AddEvent_Login()
		{
			this.AddEvent_Custom( 3 );
			this.AddEVar_Custom( 6, "user" );
		}

		/// <summary>
		/// Should be used for special logins (ie: automatic).  You may specify the type of login that occurred.
		/// </summary>
		/// <param name="sLoginType">The type of login that occurred.</param>
		public void AddEvent_Login( string sLoginType )
		{
			string sOmnitureType = "user";

			if ( sLoginType + "" != "" ) 
			{
				sOmnitureType = sLoginType.ToLower();
			}

			this.AddEvent_Custom( 3 );
			this.AddEVar_Custom( 6, sOmnitureType );
		}

		/// <summary>
		/// Tells SiteCatalyst that the customer reached the shipping information page.
		/// </summary>
		public void AddEvent_ReachedShippingPage()
		{
			this.AddEvent_Custom( 10 );
		}

		/// <summary>
		/// Tells SiteCatalyst that the customer reached the billing information page.
		/// </summary>
		public void AddEvent_ReachedBillingPage()
		{
			this.AddEvent_Custom( 11 );
		}

		/// <summary>
		/// Tells SiteCatalyst that the customer reached the order summary page.  
		/// Note that this is the page just BEFORE they submit their order.
		/// </summary>
		public void AddEvent_ReachedOrderSummaryPage()
		{
			this.AddEvent_Custom( 12 );
		}*/

		/// <summary>
		/// Builds and returns the SiteCatalyst client-side script block.<para/>
		/// This is the Javascript code that must be included on every page for SiteCatalyst to function.
		/// </summary>
		/// <returns>A string containing the populated SiteCatalyst client-side Javascript.</returns>
		/// <example>
		/// The following example shows how the checkout confirmation page might be populated:
		/// <code>
		/// Omniture objOmniture = new Omniture();
		/// 
		/// // Populate common page properties
		/// objOmniture.PageName = "QSP US: Store: Checkout: Order Confirmation";
		/// objOmniture.Channel = "QSP US: Store";
		/// objOmniture.Server = "161.230.137.168";
		/// 
		/// // Populate common page props
		/// objOmniture.AddProp_LoggedIn( 2 );
		/// objOmniture.AddProp_Locale( "en-US" );
		/// 
		/// // Tell SiteCatalyst that a purchase has occured
		/// objOmniture.AddEvent_CheckoutComplete();
		/// 
		/// // Tell SiteCatalyst what product(s) are in the order - use full quantity and price detail
		/// objOmniture.AddProduct("Product A", 1, 14.95);
		/// objOmniture.AddProduct("Product B", 3, 3.95);
		/// objOmniture.AddProduct("Product C", 1, 8.95);
		/// 
		/// // Populate the billing address information
		/// objOmniture.State = "NY";
		/// objOmniture.Zip = "10570";
		/// 
		/// // Populate the shipping charge, if any
		/// objOmniture.Shipping = 3.95;
		/// 
		/// // Populate external tracking info if applicable
		/// objOmniture.Campaign = "trk123";
		/// 
		/// // Populate the EDS order ID
		/// objOmniture.PurchaseId = "50622781";
		/// 
		/// Page.RegisterStartupScript("OmnitureScript", objOmniture.FetchScriptBlock() );
		/// </code>
		/// Here is how the resulting Javascript block would appear on the page:
		/// <code>
		///	&lt;!-- SiteCatalyst code version: H.2.
		///	Copyright 1997-2005 Omniture, Inc. More info available at
		/// http://www.omniture.com --&gt;
		///	&lt;script language="JavaScript" src="/js/s_code.js"&gt;&lt;/script&gt;
		/// &lt;script language="JavaScript"&gt;&lt;!--
		/// /* You may give each page an identifying name, server, and channel on
		/// the next lines. */
		/// s.pageName="QSP US: Store: Checkout: Order Confirmation"
		/// s.server="161.230.137.168"
		/// s.channel="QSP US: Store"
		/// s.pageType=""
		/// s.prop17="Fully authenticated"
		/// s.prop21="en-US"
		/// s.prop11="trk123"
		/// /* E-commerce Variables */
		/// s.campaign="trk123"
		/// s.state="NY"
		/// s.zip="10570"
		/// s.events="event1,purchase"
		/// s.products=";Product A;1;14.95,;Product B;3;3.95,;Product C;1;8.95,;Shipping Amount;;;event8=3.95"
		/// s.purchaseID="50622781"
		/// s.eVar1="QSP US: Store: Checkout: Order Confirmation"
		/// /************* DO NOT ALTER ANYTHING BELOW THIS LINE ! **************/
		/// var s_code=s.t();if(s_code)document.write(s_code)//--&gt;&lt;/script&gt;
		/// &lt;script language="JavaScript"&gt;&lt;!--
		/// if(navigator.appVersion.indexOf('MSIE')&gt;=0)document.write(unescape('%3C')+'\!-'+'-')
		/// //--&gt;&lt;/script&gt;&lt;!--/DO NOT REMOVE/--&gt;
		/// &lt;!-- End SiteCatalyst code version: H.2. --&gt;
		/// </code>
		/// </example>
		virtual public string FetchScriptBlock() 
		{
			string sOmnitureScript = "";

			sOmnitureScript = "<!-- SiteCatalyst code version: H.2.\n" +
				"Copyright 1997-2005 Omniture, Inc. More info available at\n" +
				"http://www.omniture.com -->\n" +
				"<script language=\"JavaScript\" src=\"{0}\"></script>\n" +
				"<script language=\"JavaScript\"><!--\n" +
				"/* You may give each page an identifying name, server, and channel on\n" +
				"the next lines. */\n" +
				"{1}pageName=\"" + sPageName + "\";\n" +
				"{1}server=\"" + sServer + "\";\n" +
				"{1}channel=\"" + sChannel + "\";\n" +
				"{1}pageType=\"" + sPageType + "\";\n" +
				this.FetchProps() + 
				"/* E-commerce Variables */\n" +
				"{1}campaign=\"" + sCampaign + "\";\n" +
				"{1}state=\"" + sState + "\";\n" +
				"{1}zip=\"" + sZip + "\";\n" +
				this.FetchEvents() + 
				this.FetchProducts() + 
				"{1}purchaseID=\"" + sPurchaseId + "\";\n" +
				this.FetchEVars() + 
				"/************* DO NOT ALTER ANYTHING BELOW THIS LINE ! **************/\n" +
				"var s_code={1}t();if(s_code)document.write(s_code)//--></script>\n" +
				"<script language=\"JavaScript\"><!--\n" +
				"if(navigator.appVersion.indexOf('MSIE')>=0)document.write(unescape('%3C')+'\\!-'+'-')\n" +
				"//--></script><!--/DO NOT REMOVE/-->\n" +
				"<!-- End SiteCatalyst code version: H.2. -->\n";

			return  string.Format (sOmnitureScript, JSFileName, prefixVar );
		}


		#endregion

		#region Private Methods

		private string _prefixVar = "s.";

		/// <summary>
		/// Builds and returns the current sProps.
		/// </summary>
		/// <returns>A string containing the sProps in a properly-formatted Javascript block.</returns>
		protected string FetchProps()
		{
			string sPropString = "";

			for ( int i = 0; i < Props.Count; i++ ) 
			{
				Prop objProp = (Prop)Props[i];
				sPropString = sPropString +  prefixVar + "prop" + objProp.Index + "=\"" + objProp.Value + "\"\n";
			}

			return sPropString;
		}

		/// <summary>
		/// Builds and returns the current eVars.
		/// </summary>
		/// <returns>A string containing the eVars in a properly-formatted Javascript block.</returns>
		protected string FetchEVars()
		{
			string sEVarString = "";

			for ( int i = 0; i < EVars.Count; i++ ) 
			{
				EVar objEVar = (EVar)EVars[i];
				sEVarString = sEVarString + prefixVar + "eVar" + objEVar.Index + "=\"" + objEVar.Value + "\";\n";
			}

			return sEVarString;
		}

		/// <summary>
		/// Builds and returns the current products.
		/// </summary>
		/// <returns>A string containing the current products in a properly-formatted Javascript block.</returns>
		protected string FetchProducts()
		{
			string sProductString = prefixVar + "products=\"";

			for ( int i = 0; i < Products.Count; i++ ) 
			{
				if (i > 0) 
				{
					sProductString = sProductString + ",";
				}
				Product objProd = (Product)Products[i];
				if (objProd.Quantity > 0) 
				{
					sProductString = sProductString + ";" + objProd.ProductName + ";" + objProd.Quantity + ";" + objProd.Price;
				} 
				else 
				{
					sProductString = sProductString + ";" + objProd.ProductName + ";;";
				}
			}

			if ( Events.Contains("purchase") ) 
			{

				if ( dShipping > 0 )
				{
					this.AddEvent_Custom(8);
					sProductString = sProductString + ",;Shipping Amount;;;event8=" + dShipping;
				}

				if ( dTax > 0 )
				{
					this.AddEvent_Custom(9);
					sProductString = sProductString + ",;Tax Amount;;;event9=" + dTax;
				}

			}

			sProductString = sProductString + "\";\n";
			return sProductString;
		}

		/// <summary>
		/// Builds and returns the current events.
		/// </summary>
		/// <returns>A string containing the current events in a properly-formatted Javascript block.</returns>
		protected string FetchEvents()
		{
			string sEventString = prefixVar + "events=\"";

			for ( int i = 0; i < Events.Count; i++ ) 
			{
				if (i > 0) 
				{
					sEventString = sEventString + ",";
				}
				string sEvent = (string)Events[i];
				sEventString = sEventString + sEvent;
			}

			sEventString = sEventString + "\";\n";
			return sEventString;
		}

		#endregion

		#region Private Classes

		/// <summary>
		/// sProp class.
		/// </summary>
		private class Prop
		{
			private int iIndex;
			private string sValue;

			/// <summary>
			/// Constructor.  Takes and stores the index and value of this sProp.
			/// </summary>
			/// <param name="i">The number of this sProp.</param>
			/// <param name="s">The value of this sProp.</param>
			public Prop( int i, string s )
			{
				iIndex = i;
				sValue = s;
			}

			/// <summary>
			/// Gets and sets the prop number.
			/// </summary>
			public int Index
			{
				get	{ return iIndex; }
				set	{ iIndex = value; }  
			}

			/// <summary>
			/// Gets and sets the prop value.
			/// </summary>
			public string Value
			{
				get	{ return sValue; }
				set	{ sValue = value; }  
			}
		}

		/// <summary>
		/// eVar class.
		/// </summary>
		private class EVar
		{
			private int iIndex;
			private string sValue;

			/// <summary>
			/// Constructor.  Takes and stores the index and value of this eVar.
			/// </summary>
			/// <param name="i">The number of this eVar.</param>
			/// <param name="s">The value of this eVar.</param>
			public EVar( int i, string s )
			{
				iIndex = i;
				sValue = s;
			}

			/// <summary>
			/// Gets and sets the eVar number.
			/// </summary>
			public int Index
			{
				get	{ return iIndex; }
				set	{ iIndex = value; }  
			}

			/// <summary>
			/// Gets and sets the eVar value.
			/// </summary>
			public string Value
			{
				get	{ return sValue; }
				set	{ sValue = value; }  
			}
		}

		/// <summary>
		/// Product class.
		/// </summary>
		private class Product
		{
			private string sProductName;
			private int iQuantity;
			private double dPrice;

			/// <summary>
			/// Constructor.  Takes and stores all the attributes of this product.
			/// </summary>
			/// <param name="s">The name of this product.</param>
			/// <param name="i">The quantity ordered.</param>
			/// <param name="d">The price per item.</param>
			public Product( string s, int i, double d ) 
			{
				sProductName = s;
				iQuantity = i;
				dPrice = d;
			}

			/// <summary>
			/// Constructor.  Takes and stores the name of this product.
			/// </summary>
			/// <param name="s">The name of this product.</param>
			public Product( string s ) 
			{
				sProductName = s;
				iQuantity = -1;
				dPrice = -1;
			}

			/// <summary>
			/// Gets and sets the product name.
			/// </summary>
			public string ProductName
			{
				get	{ return sProductName; }
				set	{ sProductName = value; }  
			}

			/// <summary>
			/// Gets and sets the quantity purchased.
			/// </summary>
			public int Quantity
			{
				get	{ return iQuantity; }
				set	{ iQuantity = value; }  
			}

			/// <summary>
			/// Gets and sets the price of each item.
			/// </summary>
			public double Price
			{
				get	{ return dPrice; }
				set	{ dPrice = value; }  
			}
		}

		#endregion

	}

}
