<%@ LANGUAGE="VBScript" %>
<% Option Explicit %>
<% ' 11/2006 - make this page non-cacheable! %>
<!--#include virtual="common/EZExpirePageInclude.asp"-->
<!--#include virtual="common/EZPageTop.asp"-->
<!--#include virtual="common/EZCommonDBUtils.asp"-->
<!--#include virtual="common/EZMainDBUtils.asp"-->
<!--#include virtual="common/SitePageControlInclude.asp"-->
<%

	' Our standard page attributes
	Dim OurASPPage:		OurASPPage = FundraisingServicesASP
	Dim sPageHeader:	sPageHeader = "Full Service Fundraising"

	' Our default page title
	HTMLTitle = "EZFund.com - Full Service Fundraising, we provide everything to make your fundraising successful!"

	' NAV: define parent and main reports page
	Call InitParentNavBar("Home", "/")
	Call AddParentNavBar(FundraisingServicesMenuSection, OurASPPage)

	Call ExtractRequestParams()

	' database value overrides the page default
	If sPageCtlHTMLTitle <> "" Then HTMLTitle = sPageCtlHTMLTitle
%>
<!--#include virtual="common/EZHTMLTop.asp"-->
<!--#include virtual="common/EZHeadTop.asp"-->
<!--#include virtual="common/EZHeadBottom.asp"-->

<!--#include virtual="common/EZBodyTop.asp"-->
<!--#include virtual="common/EZMastHead.asp"-->
<!--#include virtual="common/EZMenuBoard.asp"-->
<%	
	Call EmitParentNavBar()	

	' NB: main content of this page is externalized in two include files

	Call EmitMainPageHeader()	' MAIN PAGE header
	Call EmitMainPageFooter()	' MAIN PAGE footer
%>
<!--#include virtual="common/EZBodyBottom.asp"-->
<!--#include virtual="common/EZHTMLBottom.asp"-->
<%
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
'                                 END OF MAIN ASP SCRIPT
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
'                       Start of support functions for this script
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

Function ClearRequestParams()
	' no params at this time
End Function

Function ExtractRequestParams()		' Extract all params for this request

	Call ClearRequestParams()

	' Load control params for the PARENT page 
	' (use these params as default; CHILD pages can override)
	' NB: this must be performed early in the initialize process (allow CHILD override downstream)
	Call LoadControlParamsForPage(cFundraisingServicesPageCde)

	' no params at this time

	Call ConstructServicesMenu()
	Call ConstructMessageBoard(cFundraisingServicesPageCde)

End Function


' ----- MAIN PAGE support functions

Function EmitMainPageHeader()
	' This section displays at the top of the MAIN page
%>
<!--#include virtual="includes/FundraisingServicesMainPageHeaderInclude.asp"-->
<%
End Function

Function EmitMainPageFooter()
	' This section displays at the bottom of the MAIN page
%>
<!--#include virtual="includes/FundraisingServicesMainPageFooterInclude.asp"-->
<%
End Function


' ---------- Support routines

Function ConstructServicesMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' sub-menu
	Call AddSubMenuItem("Request FREE Info",	ProductInfoRequestASP,	"Request FREE information on any of our fundraising products.", FundraisingServicesMenuSection)
	Call AddSubMenuItem("Request Selling Kits",	SellingKitRequestASP,	"Request FREE selling kits for each member in your group.  Available for all pre-sale fundraising programs.", FundraisingServicesMenuSection)
' REMOVE THIS! RB wants this moved to MyEZFund site (hidden behind the login page)
'	Call AddSubMenuItem("Product Calculator",	ProductCalculatorASP,	"Use our online product calculator to estimate your sales.", FundraisingServicesMenuSection)
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)
	
End Function
%>