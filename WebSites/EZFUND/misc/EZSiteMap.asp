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
	Dim OurASPPage:		OurASPPage = SiteMapASP
	Dim sPageHeader:	sPageHeader = "Site Map"

	' Our default page title
	HTMLTitle = "EZFund.com Site Map - find any Fundraising product from this page!"

	' NAV: define parent and main reports page
	Call InitParentNavBar("Home", "/")
	Call AddParentNavBar("Site map", OurASPPage)

	Call ExtractRequestParams()

	' database value overrides the page default
	If sPageCtlHTMLTitle <> "" Then HTMLTitle = sPageCtlHTMLTitle

	Call DisableMenuBoard()
%>
<!--#include virtual="common/EZHTMLTop.asp"-->
<!--#include virtual="common/EZHeadTop.asp"-->
<!--#include virtual="common/EZHeadBottom.asp"-->

<!--#include virtual="common/EZBodyTop.asp"-->
<!--#include virtual="common/EZMastHead.asp"-->
<!--#include virtual="common/EZMenuBoard.asp"-->
<%	
	Call EmitParentNavBar()	

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
	Call LoadControlParamsForPage(cSiteMapPageCde)

	' no params at this time

End Function


Function EmitMainPage()

	Call EmitMainPageHeader()
	Call EmitSiteMap()
	Call EmitMainPageFooter()
	
End Function


' ----- MAIN PAGE support functions

Function EmitMainPageHeader()
	' This section displays at the top of the MAIN page
%>
<!--#include virtual="includes/SiteMapMainPageHeaderInclude.asp"-->
<%
End Function

Function EmitMainPageFooter()
	' This section displays at the bottom of the MAIN page
%>
<!--#include virtual="includes/SiteMapMainPageFooterInclude.asp"-->
<%
End Function

Function EmitSiteMap()
	' Site Map:
	'
	'	* uses SITE_SITE_MAP_TBL to define the general map and sequencing
	'	* the SITE_MAP_URL_TXT and SITE_MAP_DESC_TXT fields in related tables 
	'	  are used to build sub-menu topics
	'	  (ie. SITE_REF_PCKL_LKUP_TBL, SITE_PDCT_TBL, SITE_PGM_TBL tables)
	'

	' NOTE: All pages that include MastHead must also include EZMainDBUtils.asp!
	'	    (If not, the Map will NOT be visible!)
	
	Dim RS, SQLStmt
	Dim sDispTxt, sURLTxt, nLevlNbr, sCSSClassNme, nPageColNbr
	
	Dim nPrevLevlNbr: nPrevLevlNbr = 0
	Dim nCurrColNbr: nCurrColNbr = 0
	
	Dim nColElementCnt: nColElementCnt = 0
	Dim nElementCnt: nElementCnt = 0
	Dim sElementTxt: sElementTxt = ""

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetSiteMap"
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	Do While CheckRS(RS)

		sDispTxt = nvs(RS.Fields("DISP_TXT"))
		sURLTxt = nvs(RS.Fields("URL_TXT"))
		nLevlNbr = nvn(RS.Fields("LEVL_NBR"))
		sCSSClassNme = nvs(RS.Fields("CSS_CLASS_NME"))
		nPageColNbr = nvn(RS.Fields("MAP_PAGE_COL_NBR"))

		If sURLTxt <> "" Then		
			If nElementCnt = 0 Then
				nCurrColNbr = nPageColNbr
				Response.Write "<br>"
				Response.Write "<center>"
				Response.Write "<table cellspacing=0 cellpadding=0 border=0 width=640>"
				Response.Write "<tr><td valign=top>"
				Response.Write HTFont(True, "")
			End If
		
			If nPageColNbr <> nCurrColNbr Then
				' switching columns
				nCurrColNbr = nPageColNbr
				Response.Write HTFontEnd()
				Response.Write "</td><td valign=top>"
				Response.Write HTFont(True, "")
				nColElementCnt = 0
			End If

			' format this entry
'			Select Case nLevlNbr
'				Case 3:		sElementTxt = "&nbsp;&nbsp;&nbsp;&raquo; "
'				Case 2:		sElementTxt = "&nbsp;&bull; "
'				Case Else:	sElementTxt = "<br>"
'			End Select
'			Response.Write sElementTxt & "<a href=" & QS(sURLTxt) & ">" & sDispTxt & "</a><BR>"

			Select Case nLevlNbr
				Case 3:
						sElementTxt = "&nbsp;&nbsp;&nbsp;&raquo; "
						Response.Write sElementTxt & "<a href=" & QS(sURLTxt) & " class=""SiteMapLevel3"">" & sDispTxt & "</a><BR>"
				Case 2:
						If nPrevLevlNbr = 3 Then Response.Write "<br>"
						sElementTxt = "&nbsp;&bull; "
						If sDispTxt = "Links" Or sDispTxt = "Resources" Or sDispTxt = "Order-Takers" Then
							' add nofollow attrib for these pages
							Response.Write sElementTxt & "<a href=" & QS(sURLTxt) & NoFollowLinkAttribute & " class=""SiteMapLevel2"">" & sDispTxt & "</a><BR>"
						Else	
							Response.Write sElementTxt & "<a href=" & QS(sURLTxt) & " class=""SiteMapLevel2"">" & sDispTxt & "</a><BR>"
						End If	
				Case Else:
						If nColElementCnt > 0 Then
							Response.Write "<br>"
							If nPrevLevlNbr > 1 Then Response.Write "<br>"
						End If	
						Response.Write "<div class=""SiteMapHeader""><a href=" & QS(sURLTxt) & " class=""SiteMapLevel1"">" & sDispTxt & "</a></div>"
			End Select
			nPrevLevlNbr = nLevlNbr
			
			nElementCnt = nElementCnt + 1
			nColElementCnt = nColElementCnt + 1
		End If
				
		RS.MoveNext
	Loop
	
	If nElementCnt > 0 Then
		Response.Write HTFontEnd()
		Response.Write "</td></tr></table>"
		Response.Write "</center>"
	End If

	RS.Close
	Set RS = Nothing
	Call CloseEZMainDB()
	
End Function

%>
