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
	Dim OurASPPage:		OurASPPage = ResourcesASP
	Dim sPageHeader:	sPageHeader = "Resources"

	' Our default page title
	HTMLTitle = "EZFund.com - Full Service Fundraising, we provide everything to make your fundraising successful!"

	' NAV: define parent and main reports page
	Call InitParentNavBar("Home", "/")
	Call AddParentNavBar(ResourcesMenuSection, OurASPPage)

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

	' NB: main content of this page is externalized in one include file

	Call EmitMainPage()		' MAIN PAGE
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
	Call LoadControlParamsForPage(cResourcesPageCde)

	' no params at this time

	Call ConstructServicesMenu()
	Call ConstructMessageBoard(cResourcesPageCde)

End Function


' ----- MAIN PAGE support functions

Function EmitMainPage()
%>
<!--#include virtual="includes/ResourcesMainPageInclude.asp"-->
<%
End Function


' ---------- Support routines

Function ConstructServicesMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' sub-menu
	Call AddSubMenuItem("", "", "", ResourcesMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)
	
End Function
%>