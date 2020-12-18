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
	Dim OurASPPage:		OurASPPage = HomePageASP
	Dim sPageHeader:	sPageHeader = "HOME PAGE"

	' Our default page title
	HTMLTitle = "Cookie dough fundraiser, lollipop and candy fundraiser - raise money with EZFund.com"

	' PAGE PRODUCT image display params
	'	-- HOME page
	Const cPageProductsPerRow = 4				' # product images per row
	Const cPageProductImageWidth = 125
	Const cPageProductImageHeight = 125
	Const cPageProductSpacerWidth = 30			' space between product images on HOME page

	Const cMaxPageProducts = 20		' pick a number; we should not reach this number
	Dim PageProducts(20,4)
		Const cxPdctImageNme = 1
		Const cxPdctImageDescTxt = 2
		Const cxPdctShrtFeatTxt = 3
		Const cxPdctURLTxt = 4
	Dim nPageProducts

	' NAV: define parent and main reports page
	Call InitParentNavBar(HomePageMenuSection, HomePageASP)

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
	Call EmitMainPage()
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
	' nothing to do at this time
End Function


Function ExtractRequestParams()		' Extract all params for this request

	Call ClearRequestParams()

	' Load control params for the PARENT page 
	' (use these params as default; CHILD pages can override)
	' NB: this must be performed early in the initialize process (allow CHILD override downstream)
	Call LoadControlParamsForPage(cHomePagePageCde)
	
	Call LoadSitePageProductList(cHomePagePageCde)

	' main menu
	Call ConstructMainMenu()
	' sub-menu
	Call AddSubMenuItem("", "", "", HomePageMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)

	Call ConstructMessageBoard(cHomePagePageCde)
	
End Function


' -------------------- View MAIN brochure program

Function EmitMainPage()
	Dim i, nCols

	Dim nProductCols: nProductCols = (cPageProductsPerRow * 2) - 1

	Call EmitMainPageHeader()	' MAIN PAGE header
	
	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"
	Response.Write "<tr><td colspan=" & nProductCols & " align=center class=ContentData>&nbsp;</td></tr>"

	' display the HOME page products (defined in the database)	
	i = 0
	Do While (i < nPageProducts)
		' new product row
		Response.Write "<tr>"
		For nCols = 1 To cPageProductsPerRow
			' display (x) Products per row
			i = i + 1
			If i <= nPageProducts Then
				Call EmitOneProductTableEntry(PageProducts(i, cxPdctImageNme), PageProducts(i, cxPdctImageDescTxt), PageProducts(i, cxPdctShrtFeatTxt), PageProducts(i, cxPdctURLTxt))
			Else
				Call EmitOneProductTableEntry("", "", "", "")
			End If
			If nCols < cPageProductsPerRow Then Response.Write "<td width=" & cPageProductSpacerWidth & ">&nbsp;</td>"	' column spacer
		Next
		Response.Write "</tr>"
		Response.Write "<tr><td colspan=" & nProductCols & " width=5>&nbsp;</td></tr>"	' row spacer
	Loop

	Response.Write "</table>"

	Call EmitMainPageFooter()	' MAIN PAGE footer
	
End Function


' ----- MAIN PAGE support functions

Function EmitMainPageHeader()
	' This section displays at the top of the MAIN page
%>
<!--#include virtual="includes/HomeMainPageHeaderInclude.asp"-->
<%
End Function

Function EmitMainPageFooter()
	' This section displays at the bottom of the MAIN page
%>
<!--#include virtual="includes/HomeMainPageFooterInclude.asp"-->
<%
End Function

Function EmitOneProductTableEntry(ByVal sPdctImage, ByVal sImageDescTxt, ByVal sPdctFeatTxt, ByVal sPdctURL)
	Response.Write "<td width=" & cPageProductImageWidth & " align=center valign=top class=SmallContentData>"
	If sPdctImage <> "" Then
		If sPdctURL <> "" Then
			Response.Write "<a href=" & QS(sPdctURL) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt)) & ">"
		End If	
		Response.Write "<img src=" & sPdctImage & " width=" & cPageProductImageWidth & " height=" & cPageProductImageHeight & " border=1 alt=" & QS(StripHTMLTags(sImageDescTxt)) & ">"
		Response.Write "<br><br>" & sImageDescTxt
		If sPdctURL <> "" Then
			Response.Write "</a>"
		End If	
		Response.Write "<div class=ContentData>" & sPdctFeatTxt & "</div>"
	Else
		Response.Write "&nbsp;"	
	End If
	Response.Write "</td>"
End Function


' ---------- Support routines


' === Load Site Product List for display on HOME page

Function LoadSitePageProductList(ByVal sPageCde)
	Dim RS, SQLStmt
	
	On Error Resume Next
	Call OpenEZMainDB()
	
	SQLStmt = "SITE_GetProductListForPage @PageCde=" & SQS(sPageCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	
	nPageProducts = 0
	Do While CheckRS(RS)
		nPageProducts = nPageProducts + 1
		If nPageProducts > cMaxPageProducts Then nPageProducts = cMaxPageProducts: Exit Do

		PageProducts(nPageProducts, cxPdctImageNme) = nvs(RS.Fields("IMAGE_NME"))
		If PageProducts(nPageProducts, cxPdctImageNme) = "" Then
			PageProducts(nPageProducts, cxPdctImageNme) = PhotoUnavailableImage
		End If
		PageProducts(nPageProducts, cxPdctImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT"))
		PageProducts(nPageProducts, cxPdctShrtFeatTxt) = nvs(RS.Fields("SHRT_FEAT_TXT"))
		PageProducts(nPageProducts, cxPdctURLTxt) = nvs(RS.Fields("URL_TXT"))
		
		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing
	
	LoadSitePageProductList = (Err.number = 0)
End Function

%>