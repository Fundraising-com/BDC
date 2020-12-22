<%
' EZPageTop - stuff before <HTML>
' ----------------------------------------------------------------------

' Define our environment - Development or Production
Dim ProjectMode
' IMPORTANT:
'   THE FOLLOWING CODE MUST BE EXECUTED BEFORE USE BY ANY CALLING PAGE!
If Request.ServerVariables("SERVER_NAME") = "rdezf.atchou.com" Then
	ProjectMode = "DEV"		' Development
Else
	ProjectMode = "PROD"	' Production
End If	

' --- To allow for dynamically changing these references (based on the
'     mood we're in, er I mean mode).
'     These can't be constants!
Dim HOME_URL
' The ATCWebFiles directory contain include files and downloadable files
' for the ATC site.  The reason for locating these types of files outside 
' the project is to give Operations personnel access to modify the content
' without requiring the use of VI and VSS.
If ProjectMode = "PROD" Then
	' --- Production ---
	HOME_URL =				"http://www.ezfund.com/"
Else	
	' --- Development ---
	HOME_URL =				"http://rdezf.atchou.com/"
End If	

' The log is in this directory
Const LogsPhysPathname =		"C:\EZWebFiles\Logs\"
Const IncludesPhysPathname =	"C:\EZWebFiles\Includes\"

' ----------------------------------------------------------------------

' ********************
'   GLOBAL CONSTANTS
' ********************

' EZFund.com Color scheme
Const EZBGColor = "#6699FF"		' LightBlue:#99CCFF, MediumBlue:#6699FF
Const EZFGColor = "#0000FF"		' #0000FF

'Const ATCBGColor = "#0000FF"		' #0000FF (CTP colors)
'Const ATCFGColor = "#FFFF00"		' #FFFF00 (CTP colors)
Const EZColor_Table1 = "#CCCC99"	' the bronze and gold of the tables
Const EZColor_Table2 = "#FFFFCC"   ' #CCCC99, #FFFFCC
				' following is logically but not automatically linked to colors!
'Const ATCButton_Style = "font-weight:bold; background-color:#0000FF; color:#FFFF00"
Const EZButton_Style = "font-weight:bold; background-color:#6699FF; color:#FFFF00"

' Font defaults - these are being phased out in favor of CSS
Const dfltFontTag = "<font face=""Verdana,Arial,Helvetica,Sans"" size=2>"
Const dfltFontFace =     " face=""Verdana,Arial,Helvetica,Sans"""
Const dfltFontSize = " size=2"

' Commonly used anchor names
Const cTopAnchorName =		"TOP"		' Top of Page	
Const cHintsAnchorName =	"HINTS"		' Hints section (or help)
Const cResultsAnchorName =	"RESULTS"	' Results section (generally associated with a search)

Const ContentWindowWidthNoMenu = "740"
Const ContentWindowWidthWithMenu = "580"
' default content window width to assume menu/message board
Dim ContentWindowWidth: ContentWindowWidth = ContentWindowWidthWithMenu		' default w/menu; altered by EZMenuBoard.asp


' ----- EZFund contact information

' WRITE THIS!  eventually load this info into application variables???

' EZFund contact information
Const EZMainLocalPhone =	"1-713-973-1616"
Const EZMainLocalFax =		"1-713-973-8321"
Const EZMainEmail =			"Info@ezfund.com"

Const EZHelpDeskContact =	"EZFund.com Help Desk"
Const EZHelpDeskEmail =		"HelpDesk@ezfund.com"
Const EZHelpDeskPhone =		"1-800-991-8779"
Const EZHelpDeskLocalPhone ="1-713-973-1616"
Const EZSalesContact =		"Sales"
Const EZSalesEmail =		"Order@ezfund.com"		' per Winnie, they don't have a Sales email address
Const EZSalesPhone =		"1-800-991-8779"
Const EZSalesLocalPhone =	"1-713-973-1616"
Const EZSalesFax =			"1-800-299-4884"
Const EZInfoContact =		"Fundraising Consultants"
Const EZInfoEmail =			"Info@ezfund.com"
Const EZInfoPhone =			"1-800-991-8779"
Const EZInfoLocalPhone =	"1-713-973-1616"
Const EZInfoFax =			"1-800-299-4884"
ReDim EZInfoMailingAddress(3)
EZInfoMailingAddress(1) = "EZFund.com"
EZInfoMailingAddress(2) = "1980 Afton"
EZInfoMailingAddress(3) = "Houston, TX 77055"

' -- SELLING KIT FORM (email notifications)
Const cSKITEmailNotificationToEmail = "Order@ezfund.com"	' To for email notification
Const cSKITEmailNotificationFromEmail = "Sales@ezfund.com"	' From for email notification
Const cSKITEmailNotificationBccEmail = "mjanak@ezfund.com"	' Bcc for email notification message


' ----- Common images

' our standard "Photo Not Available" image
Const PhotoUnavailableImage = "/images/PhotoNotAvailable.jpg"
Const PhotoUnavailableImagePrefix = "/images/PhotoNotAvailable"		' for pages expecting to construct the remainder of the filename

' Image banners for FREE information
Const RequestFreeInfoImage = "/images/InfoRequest_Banner_ClickHere.jpg"
Const RequestSellingKitImage = "/images/SellingKit_Banner_ClickHere.jpg"

Const ProductInfoRequestImage = "/images/InfoRequest_Banner_ClickHere.jpg"
Const SellingKitRequestImage = "/images/SellingKit_Banner_ClickHere.jpg"

' SEO - nofollow to preserve PageRank on some pages (add to anchor tag)
Const NoFollowLinkAttribute = " rel=""nofollow"""


' ----- ASP pages

' -- Common ASP pages
Const HomePageASP =			"/"
Const BrochureProgramASP =	"/program/BrochureCatalog.asp"			' Brochure product pages
Const FoodProgramASP =		"/program/FoodProducts.asp"				' Food product pages (pre-sell)
Const DirectSellerASP =		"/direct/DirectSaleProducts.asp"		' Direct Seller product pages
Const CustomProductASP =	"/direct/DirectSaleProducts.asp?PdctGrpCde=CUSTOM&PdctCtgyCde=CUSTOM"		' Customized product pages
Const GroupProgramASP =		"/group/GroupPrograms.asp"				' Group type program pages
Const FundraisingServicesASP = "/svc/FullServiceFundraising.asp"	' Fundraising Services page
Const PrizeIncentiveASP =	"/prize/PrizeIncentives.asp"			' Prize incentive page
Const MagazineProgramASP =	"/program/BrochureCatalog.asp?PgmGrpCde=MAGAZINE"		' Magazines page
Const HolidayStoreASP =		"/program/BrochureCatalog.asp?PgmGrpCde=HOLIDAY"		' Holiday Store page
Const GoGreenASP =			"/program/BrochureCatalog.asp?PgmGrpCde=GO-GREEN"		' Go-Green page
Const OrderOnlineASP =		"/svc/OrderOnline.asp"					' Order Online page (explains Direct vs. Pre-Sell ordering)
Const LinksASP =			"/misc/Links.asp"						' Links page
Const ResourcesASP =		"/misc/Resources.asp"					' Resources page
Const WhatsNewASP =			"/misc/WhatsNew.asp"					' What's New? page
Const AboutUsASP =			"/misc/AboutUs.asp"						' About Us page
Const ContactUsASP =		"/misc/ContactUs.asp"					' Contact Us page
Const PrivacyPolicyASP =	"/misc/EZPrivacyPolicy.asp"				' Privacy Policy page
Const SiteMapASP =			"/misc/EZSiteMap.asp"					' Site Map page

' -- Resource pages
Const ProductCalculatorASP = "/ezforms/OrderQuantityForm.asp"		' Order quantity form page
Const SellingKitRequestASP = "/ezforms/SellingKitRequest.asp"		' FREE selling kit
Const ProductInfoRequestASP = "/ezforms/ProductInfoRequest.asp"		' FREE product information

' -- External pages (reference is outside our main EZFund theme)
Const OnlineStoreASP =		"http://shop.ezfund.com/Default.asp"	' MAIN Shopping Cart page
Const OnlineStoreCatalogASP = "http://shop.ezfund.com/Default.asp"	' Shopping Cart Catalog
Const OnlineStoreOrderASP = "http://shop.ezfund.com/ShoppingCart.asp?ProductCode="	' EZFund Shopping Cart PRODUCT page
Const EZShoppingCartASP =	"http://shop.ezfund.com/Default.asp"		' EZFund Shopping Cart page
Const MyEZFundASP =			"http://www.ezfund.com/myezfund"		' MyEZFund site


' ----- MENU & MESSAGE BOARD consts

' Menu - main menu section text (used to define a section and also the display text)
Const HomePageMenuSection = "Home"
Const GroupProgramMenuSection = "Group Offerings"
Const FoodProgramMenuSection = "Food Products"
Const BrochureProgramMenuSection = "Brochures"
Const DirectSellerMenuSection = "In-Hand Sellers"
Const MagazineProgramMenuSection = "Magazines"
Const CustomProductMenuSection = "Customized Products"
Const PrizeIncentiveMenuSection = "Prize Incentives"
Const HolidayStoreMenuSection = "Holiday Store"
Const GoGreenMenuSection = "Go-Green"
Const FundraisingServicesMenuSection = "Fundraising Services"
Const OrderOnlineMenuSection = "Order Online!"
Const LinksMenuSection = "Links"
Const ResourcesMenuSection = "Resources"
Const WhatsNewMenuSection = "What's New?"

'' Message Board - page indicators
'' NB: This allows for special announcements to appear on certain pages.
'Const cHomePageMessageBoard = "HOME"
'Const cFoodProgramMessageBoard = "FOOD-PGM"
'Const cBrochureProgramMessageBoard = "BROCHURE"		' was BROC-PGM
'Const cGroupProgramMessageBoard = "GROUP"			' was GRP-PGM
'Const cDirectSellerMessageBoard = "DIRECT"			' was DIRSLS
'Const cFundraisingServicesMessageBoard = "FRSVC"
'Const cPrizeIncentiveMessageBoard = "PRIZE"			' was PRZP
'Const cHolidayStoreMessageBoard = "HOLIDAY"
'Const cOrderOnlineMessageBoard = "ORDER"

' Page Code
' NB: These identify most of the MAIN pages. The PageCde is referenced in a couple of DB tables.
Const cHomePagePageCde = "HOME"
Const cGroupProgramPageCde = "GROUP"
Const cFoodProgramPageCde = "FOOD-PGM"
Const cBrochureProgramPageCde = "BROCHURE"
Const cDirectSellerPageCde = "DIRECT"
Const cPrizeIncentivePageCde = "PRIZE"
Const cHolidayStorePageCde = "HOLIDAY"
Const cGoGreenPageCde = "GO-GREEN"
Const cMagazineProgramPageCde = "MAGAZINE"
Const cCustomProductPageCde = "CUSTOM"
Const cOrderOnlinePageCde = "ORDER"
Const cFundraisingServicesPageCde = "FRSVC"
Const cLinksPageCde = "LINKS"
Const cResourcesPageCde = "RESOURCES"
Const cAboutUsPageCde = "ABOUT"
Const cContactUsPageCde = "CONTACT"
Const cPrivacyPolicyPageCde = "PRIVACY"
Const cSiteMapPageCde = "SITEMAP"
Const cInfoRequestPageCde = "INFOREQUEST"
Const cSellingKitBrochurePageCde = "SKITBROC"
Const cSellingKitFoodPageCde = "SKITFOOD"


' ----- Common params

' Program Group - used on pages that need to distinguish program groups
Const cPgmGrpCdeParam = "PgmGrpCde"		' program group
	Const cFoodPgmGrpCde = "FOOD-PGM"
	Const cBrochurePgmGrpCde = "BROCHURE"
	Const cPrizePgmGrpCde = "PRIZE"
	Const cHolidayPgmGrpCde = "HOLIDAY"
	Const cGoGreenPgmGrpCde = "GO-GREEN"
	Const cMagazinePgmGrpCde = "MAGAZINE"
Dim sParamPgmGrpCde

' Product Group - used on pages that need to distinguish product groups
Const cPdctGrpCdeParam = "PdctGrpCde"	' product group
'	Const cFrozenPdctGrpCde = "FROZEN"
	Const cFoodPdctGrpCde = "FOOD-PGM"
	Const cBrochurePdctGrpCde = "BROCHURE"
	Const cDirectPdctGrpCde = "DIRECT"
	Const cGoGreenPdctGrpCde = "GO-GREEN"
	Const cCustomPdctGrpCde = "CUSTOM"
Dim sParamPdctGrpCde

' Product Category - we needed another layer between product groups and product codes
Const cPdctCtgyCdeParam = "PdctCtgyCde"	' product category
Dim sParamPdctCtgyCde

' Product code - used on pages that display EZ products
Const cPdctCdeParam = "PdctCde"			' product code
Dim sParamPdctCde

' Product type - used on pages that display general product information
Const cPdctTypeParam = "PdctType"		' product type
Dim sParamPdctType

' Item code - used on pages that display EZ products (mainly direct sales)
Const cItemCdeParam = "ItemCde"			' item code
Dim sParamItemCde

' Program code - used on pages that display EZ programs
Const cPgmCdeParam = "PgmCde"			' program code
Dim sParamPgmCde

' Tab Index - used on pages like direct sales and group programs
Const cTabIndexParam = "TabIndex"		' TAB-index
Dim sParamTabIndex, nParamTabIndex

' REMOVE THIS!
'' Brochure ID - used on brochure pages (need to change usage to PgmCde for consistency)
'Const cBrocIDParam = "BrocID"			' BROCHURE page - change usage to PgmCde (consistent with OP)
'Dim sParamBrocID

' Index - used on pages that display thumbnail images of brochure pages
Const cIndexParam = "Index"
Dim sParamIndex, nParamIndex

' Page - used on pages that display brochure pages
Const cPageParam = "Page"
Dim sParamPage, nParamPage

Const cFlavorParam = "Flavor"
Dim sParamFlavor

Const cOptionParam = "Option"
	Const cFlavorOptionParam = "FLAVOR"	' display flavors page
Dim sParamOption

' Org Type - used for group pages
Const cOrgTypeIDParam = "OrgTypeID"		' group type
Dim sParamOrgTypeID, nParamOrgTypeID

' ----------------------------------------------------------------------


' ********************
'   GLOBAL VARIABLES
' ********************

' Global variables for identifying Browser type
Dim IE3, IE4, IE5, IE6, IE7				' Internet Explorer
Dim NS3, NS4, NS5, NS6, NS7, NS8		' Netscape
Dim FX0, FX1, FX2						' Firefox
Dim OtherBrowser						' Opera, Safari, etc.
Dim BrowserName, HTTPUserAgent

HTTPUserAgent = Request.ServerVariables("HTTP_USER_AGENT")
IE3 = (Instr(HTTPUserAgent,"MSIE 3.") > 0)
IE4 = (Instr(HTTPUserAgent,"MSIE 4.") > 0)
IE5 = (Instr(HTTPUserAgent,"MSIE 5.") > 0)
IE6 = (Instr(HTTPUserAgent,"MSIE 6.") > 0)
IE7 = (Instr(HTTPUserAgent,"MSIE 7.") > 0)
If (IE3 Or IE4 Or IE5 Or IE6 Or IE7) Then
	NS3 = False
	NS4 = False
	NS5 = False
	NS6 = False
	NS7 = False
	NS8 = False
	FX0 = False
	FX1 = False
	FX2 = False
	OtherBrowser = False
	Select Case True
		Case IE7:	BrowserName = "MSIE 7"
		Case IE6:	BrowserName = "MSIE 6"
		Case IE5:	BrowserName = "MSIE 5"
		Case IE4:	BrowserName = "MSIE 4"
		Case IE3:	BrowserName = "MSIE 3"
		Case Else:	BrowserName = "Other"
	End Select
Else
	NS3 = (Instr(HTTPUserAgent,"Mozilla/3") > 0)
	NS4 = (Instr(HTTPUserAgent,"Mozilla/4") > 0)
	NS5 = (Instr(HTTPUserAgent,"Mozilla/5") > 0)	' oddball Linux flavors of Mozilla
	NS6 = (Instr(HTTPUserAgent,"Netscape6") > 0)
	NS7 = (Instr(HTTPUserAgent,"Netscape/7") > 0)
	NS8 = (Instr(HTTPUserAgent,"Netscape/8") > 0)
	FX0 = (Instr(HTTPUserAgent,"FireFox/0") > 0)
	FX1 = (Instr(HTTPUserAgent,"FireFox/1") > 0)
	FX2 = (Instr(HTTPUserAgent,"FireFox/2") > 0)
	OtherBrowser = Not (NS3 Or NS4 Or NS5 Or NS6 Or NS7 Or NS8 Or FX0 Or FX1 Or FX2)
	Select Case True
		Case FX2:	BrowserName = "Firefox 2"
		Case FX1:	BrowserName = "Firefox 1"
		Case FX0:	BrowserName = "Firefox 0"
		Case NS8:	BrowserName = "Netscape 8"
		Case NS7:	BrowserName = "Netscape 7"
		Case NS6:	BrowserName = "Netscape 6"
		Case NS5:	BrowserName = "Netscape 5"
		Case NS4:	BrowserName = "Netscape 4"
		Case NS3:	BrowserName = "Netscape 3"
		Case Else:	BrowserName = "Other"
	End Select
End If
'OtherBrowser = Not (NS3 Or NS4 Or NS5 Or NS6 Or IE3 Or IE4 Or IE5)

' Global variable for controlling the display of Main Menu Board
Dim bPageHasMenuBoard
' The default behavior is to show the Main Menu Board.
' See DisableMenuBoard() and PageHasMenuBoard() functions in this source.
bPageHasMenuBoard = True		' Normal behavior: Assume we will have a Main Menu.

' Global variable for whether to emit an onLoad event in the BODY tag.
' Default is not to emit it... see EmitBodyOnLoad() below.
Dim bBodyHasOnLoad
bBodyHasOnLoad = False

' Global variable for displaying the HTML Title (displayed in EZHeadTop.asp)
Dim HTMLTitle
HTMLTitle = "EZFund.com - Your source for fundraising!"	' default - individual pages can overwrite this value!

' Global PAGE CONTROL variables
' (the sPageCtl prefix refers to page control params that are defined in the PAGE_CTL table)
' -- META DATA
' allow dynamic creation of the META DATA (see SitePageControlInclude.asp)
Const cMaxMETADATATags = 20
Dim METADATATags(20, 2)
	Const cxMETAName = 1
	Const cxMETAContent = 2
' -- MENU FOOTER
Dim sPageCtlMenuFooter: sPageCtlMenuFooter = ""
' -- HTML TITLE
Dim sPageCtlHTMLTitle: sPageCtlHTMLTitle = ""


' ----- PARENT Navigation params

' allow navigation to traverse 10 pages deep (I certainly hope not!)
' see ParentNavBar() routines below...
Const cMaxParentNavPages = 10
Dim ParentNavText(10)
Dim ParentNavLink(10)


' ----- MENU params

' allow dynamic creation of the menu (see EZMenuBoard.asp)
Const cMaxMenuItems = 50
Dim MenuItems(50,5)
	Const cxMenuSection = 1
	Const cxMenuText = 2
	Const cxMenuURL = 3
	Const cxMenuTitle = 4
	Const cxMenuImage = 5
Dim MenuFooter: MenuFooter = ""

' allow dynamic creation of a sub-menu (see EZMenuBoard.asp)
Const cMaxSubMenuItems = 50
Dim SubMenuItems(50,4)
	Const cxSubMenuSection = 1
	Const cxSubMenuText = 2
	Const cxSubMenuURL = 3
	Const cxSubMenuTitle = 4

' allow dynamic creation of a sub-item-menu (see EZMenuBoard.asp)
Const cMaxSubItemMenuItems = 50
Dim SubItemMenuItems(50,4)
	Const cxSubItemMenuSection = 1
	Const cxSubItemMenuText = 2
	Const cxSubItemMenuURL = 3
	Const cxSubItemMenuTitle = 4

' allow dynamic creation of the message board (see EZMenuBoard.asp)
Const cMaxMessageBoardEntries = 10
Dim MessageBoardEntries(10, 2)
	Const cxMBHeader = 1
	Const cxMBText = 2

' -- Testimonial params (for display on Message Board)
'Dim nTestimonials
'Const cMaxTestimonials = 5		' max 5 per page?
'Dim Testimonials(5,3)
'	Const cxTestmlTestmlTxt = 1
'	Const cxTestmlOrgNme = 2
'	Const cxTestmlCtctNme = 3

%>
<SCRIPT LANGUAGE=VBScript RunAt=Server>

Sub DisableMenuBoard()
	' Disable the Main Menu Board

	' NOTE: This routine must be called before any HTML is issued!
	'       The EZMenuBoard.asp, as well as other EZxxxTop/Bottom.asp check this flag!
	bPageHasMenuBoard = False	' Default case is True
End Sub

Function PageHasMenuBoard()
	' Get current state of flag
	PageHasMenuBoard = bPageHasMenuBoard
End Function

Function ModernBrowser()
    ModernBrowser = (IE4 Or IE5 Or IE6 Or IE7 Or NS4 Or NS6 Or NS7 Or NS8 Or FX1 Or FX2)
End Function

Function BrowserSupportsHover()
    BrowserSupportsHover = (IE4 Or IE5 Or IE6 Or IE7 Or NS4 Or NS6 Or NS7 Or NS8 Or FX1 Or FX2)
End Function

Function BrowserSupportsStyle()
    BrowserSupportsStyle = (IE3 Or IE4 Or IE5 Or IE6 Or IE7 Or NS4 Or NS6 Or NS7 Or NS8 Or FX1 Or FX2)
End Function

Function BrowserSupportsActiveX()
    BrowserSupportsActiveX = (IE3 Or IE4 Or IE5 Or IE6 Or IE7)
End Function

Function InclIf(IfCondition, ThenPart, ElsePart)
	If IfCondition Then
		InclIf = ThenPart
	Else
		InclIf = ElsePart
	End If
End Function

Function nbsp(n)
	Dim i,t
	t=""
	For i=1 To n
		t = t & "&nbsp;"
	Next
	nbsp = t
End Function

Function AddParam(ByVal theParamDelim, ByVal theName, ByVal theValue)
	AddParam = theParamDelim & theName & "=" & theValue
End Function

Function FormatDateParam(ByVal sDate)
	' format Date for passing as command line param
	FormatDateParam = Right("0" & Month(sDate),2) & Right("0" & Day(sDate),2) & Right(Year(CDate(sDate)),2)
End Function

Function EmitUserFeedbackMessage()
	If sUserFeedbackMessage <> "" Then
		Response.Write _
			HTTable("align=center border=2 bordercolor=red cellpadding=5") & _
				HTTableRow("") & _
					HTTableCell("class=ContentWarning", sUserFeedbackMessage) & _
				HTTableRowEnd() & _
			HTTableEnd()
		sUserFeedbackMessage = ""	' clear feedback message
	End If	
End Function

' emit an entire HTML page after a serious error. CAN ONLY BE CALLED BEFORE HTML IS EMITTED!
function EmitShortError(txtMsg, txtURL, txtJumpText)
	Response.Write "<html><head></head><body><p><h2>" & _
		"An internal error has occurred. " & LogEventErrorStandardText & _
		"</h2><p>"
	Response.Write txtMsg
	if txtURL <> "" then
		Response.Write "<p><a href=""" & txtURL & """>" & txtJumpText & "</a>"
	end if	
	Response.Write "</body></html>"
	Response.End
end function


' ---------- PARENT Navigation Bar ----------

Function InitParentNavBar(ByVal sText, ByVal sURL)
	' initialize the array
	Dim i
	For i = 0 To cMaxParentNavPages
		ParentNavText(i) = ""			' initialize array
		ParentNavLink(i) = ""
	Next
	Call AddParentNavBar(sText, sURL)	' add our first entry
End Function

Function AddParentNavBar(ByVal sText, ByVal sURL)
	' add an entry to the array
	Dim i
	For i = 0 To cMaxParentNavPages
		If ParentNavText(i) = "" Then
			ParentNavText(i) = sText	' add to first available slot
			ParentNavLink(i) = sURL
			Exit For
		End If
	Next
End Function

Function EmitParentNavBar()
	' construct and emit the parent navigational bar
	Dim i
	Dim sDelim
	Dim ParentNavBar: ParentNavBar = ""
	
	For i = 0 To cMaxParentNavPages
		If ParentNavText(i) = "" Then Exit For
		sDelim = InclIf(ParentNavBar = "", "&nbsp;", "&nbsp;&gt;&nbsp;")
		If i = cMaxParentNavPages Then
			ParentNavBar = ParentNavBar & sDelim & ParentNavText(i)		' max nav link
		ElseIf ParentNavText(i+1) = "" Then
			ParentNavBar = ParentNavBar & sDelim & ParentNavText(i)		' last nav link
		Else
			' make all but the last link HOT!
			ParentNavBar = ParentNavBar & sDelim & HTHREF(ParentNavLink(i), "", ParentNavText(i))	' not last
		End If
	Next
	Response.Write "<div class=""ParentNavBar"">" & ParentNavBar & "</div>"
End Function


' ---------- PAGE Navigation Links ----------

Function TopLink()
	TopLink = nbsp(3) & "<span class=SmallContentData>" & "[<a href=""#" & cTopAnchorName & """>Top of page</a>]" & "</span>"
End Function

Function PreviousPageLink(display)
	PreviousPageLink = "<span class=SmallContentData>" & "[<a href='JavaScript:history.back()'>"
	If display <> "" Then
		PreviousPageLink = PreviousPageLink & display
	Else	
		PreviousPageLink = PreviousPageLink & "Return to previous page"
	End If
	PreviousPageLink = PreviousPageLink & "</a>]" & "</span>"
End Function

Function HintsLink()
	HintsLink = nbsp(3) & "<span class=SmallContentData>" & "[<a href=""#" & cHintsAnchorName & """>Hints</a>]" & "</span>"
End Function



Function EmitBodyOnLoad(ByVal sJavaScriptFunctionBody)
	' Emit a JavaScript function called BodyOnLoad(), to be referenced in the BODY tag's onLoad event.
	' Pass the body of the JavaScript function, e.g., focus on a control, or pass "" for a do-nothing function
	' NOTE: we split the writing of some tags to avoid confusing the InterDev and IIS.
	Response.Write "<" & "script language=javascript" & ">" & vbCrLf
	Response.Write "<" & "!--" & vbCrLf
	Response.Write "function BodyOnLoad() {" & vbCrLf
	If sJavaScriptFunctionBody = "" Then sJavaScriptFunctionBody = vbTab & "//" & vbCrLf
	If Left(sJavaScriptFunctionBody, 1) <> vbTab Then sJavaScriptFunctionBody = vbTab & sJavaScriptFunctionBody
	If Right(sJavaScriptFunctionBody, 2) <> vbCrLf Then sJavaScriptFunctionBody = sJavaScriptFunctionBody & vbCrLf
	Response.Write sJavaScriptFunctionBody
	Response.Write "}" & vbCrLf
	Response.Write "//--" & ">" & vbCrLf
	Response.Write "<" & "/script" & ">" & vbCrLf
	bBodyHasOnLoad = True	' there is an onload event, therefore we want to invoke it from the BODY tag generated later.
End Function

</SCRIPT>
<!--#include virtual="common/HTUtilPrimitives.asp"-->
<!--#include virtual="common/StringPrimitives.asp"-->
<!--#include virtual="common/EZMenuInclude.asp" -->