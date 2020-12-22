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
	Dim OurASPPage:		OurASPPage = OrderOnlineASP
	Dim sPageHeader:	sPageHeader = "Order Online"

	' Our default page title
	HTMLTitle = "EZFund.com - Order your fundraising products online using our secure shopping cart!"

	' NAV: define parent and main reports page
	Call InitParentNavBar("Home", "/")
	Call AddParentNavBar(OrderOnlineMenuSection, OurASPPage)

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
	Call LoadControlParamsForPage(cOrderOnlinePageCde)

	' no params at this time

	Call ConstructOrderMenu()
	Call ConstructMessageBoard(cOrderOnlinePageCde)

End Function


' ----- MAIN PAGE support functions

Function EmitMainPageHeader()
	' This section displays at the top of the MAIN page
%>
<!--#include virtual="includes/OrderOnlineMainPageHeaderInclude.asp"-->
<%
End Function

Function EmitMainPageFooter()
	' This section displays at the bottom of the MAIN page
%>
<!--#include virtual="includes/OrderOnlineMainPageFooterInclude.asp"-->
<%
End Function


' ---------- Support routines

Function ConstructOrderMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' sub-menu
	Call AddSubMenuItem("Order-Takers",		MyEZFundASP,		"Use our online tabulation program to assist in ordering your order-taker products.", OrderOnlineMenuSection)
	Call AddSubMenuItem("In-Hand Sellers",	EZShoppingCartASP,	"Order your in-hand sale items online using our secure shopping cart.", OrderOnlineMenuSection)
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)
	
End Function
%>