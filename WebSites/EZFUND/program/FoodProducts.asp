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
	Dim OurASPPage:		OurASPPage = FoodProgramASP
	Dim sPageHeader:	sPageHeader = "FOOD FUNDRAISERS"

	' Our default page title
	HTMLTitle = "EZFund.com - Food Fundraising Ideas to make BIG PROFITS for your group!"

	' FOOD PRODUCT image display params
	'	-- Main PRODUCT page
	Const cMainPageProductsPerRow = 3				' # product images per row
	Const cMainPageProductImageWidth = 150
	Const cMainPageProductImageHeight = 150
	Const cMainPageProductSpacerWidth = 50			' space between product images on main FOOD PRODUCTS page
	'	-- Specific PRODUCT page
	Const cSpecificProductImageWidth = 150
	Const cSpecificProductImageHeight = 150
	Const cSpecificProductSpacerWidth = 15		' space between image and description
	'	-- Specific PROGRAM page
	Const cSpecificProgramPortraitWidth = 232	' width for PORTRAIT orientation
	Const cSpecificProgramLandscapeWidth = 300	' width for LANDSCAPE orientation
	Const cSpecificProgramSpacerWidth = 15		' space between image and description

	'	-- PRIZE INDEX page
	' Portrait params
	Const cProgramIndexPortraitWidth = 111		' width for PORTRAIT orientation
	Const cProgramIndexPortraitHeight = 150		' height for PORTRAIT orientation
	Const cProgramIndexPortraitTNRows = 3		' # of thumbnail rows (portrait orientation)
	Const cProgramIndexPortraitTNCols = 5		' # of thumbnail cols (portrait orientation)
	' Landscape params
	Const cProgramIndexLandscapeWidth = 150		' width for LANDSCAPE orientation
	Const cProgramIndexLandscapeHeight = 111	' height for LANDSCAPE orientation
	Const cProgramIndexLandscapeTNRows = 5		' # of thumbnail rows (landscape orientation)
	Const cProgramIndexLandscapeTNCols = 3		' # of thumbnail cols (landscape orientation)

	Const cProgramIndexSpacerWidth = 15			' space between prize images
	Const cProgramIndexTNPerIndexPage = 15		' total thumbnails per index page

	' -- SITE variables for FOOD PROGRAMS
	
	' Program
	Dim pgmPgmNme			' food program name
	Dim pgmMenuTxt			' text to display on the menu
	Dim pgmImagePrfxNme		' image (prefix) name (may include pathname; assumes current directory)
	Dim pgmImageExtNme		' image (extension) name
	Dim pgmImageDescTxt			' product name (to display under brochure image)
	Dim pgmShrtFeatTxt		' short feature text (to display under product name)
	Dim pgmDescTxt			' description text (to display next to brochure image)
	Dim pgmFeatTxt			' feature text (to display at bottom of page - under brochure image)
	Dim pgmXtrnPageQty		' # pages to display
	Dim pgmPDFFileQty		' # PDF files (0=no PDF available)
	Dim pgmXtrnStrtDte		' start date to display on the site
	Dim pgmXtrnEndDte		' end date to display on the site
	Dim pgmPortraitPageOrientFlg	' page orientation; 1=portrait, 0=landscape
	' -- Product Category
	Dim ctgyPdctNme			' food product category name
	Dim ctgyMenuTxt			' text to display on the menu
	Dim ctgyImageNme		' image name (may include pathname; assumes current directory)
	Dim ctgyImageDescTxt	' image description text (to display under product image)
	Dim ctgyShrtFeatTxt		' short feature text (to display under product name)
	Dim ctgyDescTxt			' description text (to display next to product image)
	Dim ctgyFeatTxt			' feature text (to display at bottom of page - under product image)
	Dim ctgyXtrnStrtDte		' start date to display on the site
	Dim ctgyXtrnEndDte		' end date to display on the site

	Const cMaxPageProducts = 20		' pick a number; we should not reach this number
	Dim PageProducts(20,5)
		Const cxPdctPdctCtgyCde = 1		' product category
		Const cxPdctMenuTxt = 2
		Const cxPdctImageNme = 3
		Const cxPdctImageDescTxt = 4
		Const cxPdctShrtFeatTxt = 5
	Dim nPageProducts

	Const cMaxPrimaryPrograms = 20		' pick a number; we should not reach this number
	Dim PrimaryPrograms(20,6)
		Const cxPrimPgmPgmCde = 1
		Const cxPrimPgmDescTxt = 2
		Const cxPrimPgmExtDescTxt = 3	' 12/10/07 - added extended description
		Const cxPrimPgmPrftTxt = 4
		Const cxPrimPgmImagePrfxNme = 5
		Const cxPrimPgmImageExtNme = 6
	Dim nPrimaryPrograms

	Const cMaxTagPrograms = 20			' pick a number; we should not reach this number
	Dim TagPrograms(20,6)
		Const cxTagPgmPgmCde = 1
		Const cxTagPgmDescTxt = 2
		Const cxTagPgmExtDescTxt = 3	' 12/10/07 - added extended description
		Const cxTagPgmPrftTxt = 4
		Const cxTagPgmImagePrfxNme = 5
		Const cxTagPgmImageExtNme = 6
	Dim nTagPrograms

	' NAV: define parent and main reports page
	Call InitParentNavBar(HomePageMenuSection, HomePageASP)
	Call AddParentNavBar(FoodProgramMenuSection, OurASPPage)

	Call ExtractRequestParams()
	
	If ctgyPdctNme <> "" Then
		HTMLTitle = "EZFund.com - " & ctgyPdctNme & " Fundraising Ideas to make BIG PROFITS for your group!"
	End If
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
	Select Case True
	
		Case (sParamPgmCde <> "" And nParamPage <> 0):		Call EmitSpecificProgramPage()
		Case (sParamPgmCde <> "" And nParamIndex <> 0):		Call EmitSpecificProgramIndex()
		Case (sParamPgmCde <> ""):							Call EmitSpecificProgram()
		Case (sParamPdctCtgyCde <> ""):						Call EmitSpecificProductCategory()
		Case Else:											Call EmitMainProductPage()
				
	End Select

	Call CloseEZMainDB()
	
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
	sParamPdctCtgyCde = ""
	sParamPdctCde = ""
	sParamPgmCde = ""
	nParamIndex = 0
	nParamPage = 0
End Function

Function ExtractRequestParams()		' Extract all params for this request

	Call ClearRequestParams()

	' Load control params for the PARENT page 
	' (use these params as default; CHILD pages can override)
	' NB: this must be performed early in the initialize process (allow CHILD override downstream)
	Call LoadControlParamsForPage(cFoodProgramPageCde)

	sParamPdctCtgyCde = nvs(Request.QueryString(cPdctCtgyCdeParam))		' product category
	sParamPdctCde = nvs(Request.QueryString(cPdctCdeParam))				' product code
	sParamPgmCde = nvs(Request.QueryString(cPgmCdeParam))				' program code
	sParamIndex = nvs(Request.QueryString(cIndexParam))					' index number
	nParamIndex = nvn(sParamIndex)
	sParamPage = nvs(Request.QueryString(cPageParam))					' individual page number
	nParamPage = nvn(sParamPage)

	' these values will come from the database
	If sParamPdctCtgyCde <> "" Then
		Call GetSiteProductCategorySpecifics(sParamPdctCtgyCde)
	End If	
	If sParamPgmCde <> "" Then
		If GetSiteProgramSpecifics(sParamPgmCde) = True Then
			If nParamPage < 0 Or nParamPage > pgmXtrnPageQty Then nParamPage = 1
			If nParamIndex < 0 Or nParamIndex > (pgmXtrnPageQty+cProgramIndexTNPerIndexPage)/cProgramIndexTNPerIndexPage Then nParamIndex = 1
		Else
			Call ClearRequestParams()
		End If	
		' validate params, starting with PgmCde	
		If sParamPgmCde <> "" And (nParamIndex <> 0 Or nParamPage <> 0) Then Call DisableMenuBoard()
	End If

	Call LoadSitePageProductList(cFoodProgramPageCde)
	Call ConstructProductMenu()
	Call ConstructMessageBoard(cFoodProgramPageCde)

End Function


' -------------------- View MAIN Food products

Function EmitMainProductPage()
	Dim i, nCols

	Dim nProductTableCols: nProductTableCols = (cMainPageProductsPerRow * 2) - 1	' account for spacer columns

	Call EmitParentNavBar()
	
	If nPageProducts = 0 Then
		' no active programs (or possible database issues!)
		Call EmitNoActiveProgramMessage()	' tell the user something
		Exit Function
	End If
	
	Call EmitMainPageHeader()	' MAIN PAGE header

	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"

	' display the available food products (defined in the database)	
	i = 0
	Do While (i < nPageProducts)
		' new Product row
		Response.Write "<tr>"
		For nCols = 1 To cMainPageProductsPerRow
			' display three Products per row
			i = i + 1
			If i <= nPageProducts Then
				Call EmitOneProductTableEntry(PageProducts(i, cxPdctPdctCtgyCde), PageProducts(i, cxPdctImageNme), PageProducts(i, cxPdctImageDescTxt), PageProducts(i, cxPdctShrtFeatTxt))
			Else
				Call EmitOneProductTableEntry("", "", "", "")
			End If
			If nCols < cMainPageProductsPerRow Then Response.Write "<td width=" & cMainPageProductSpacerWidth & ">&nbsp;</td>"	' column spacer
		Next
		Response.Write "</tr>"
		Response.Write "<tr><td colspan=" & nProductTableCols & " width=5>&nbsp;</td></tr>"	' row spacer
	Loop

	' display request for information links
	Call EmitSellingKitProductInfoLinks(cFoodPdctGrpCde, cFoodPdctGrpCde, "Food products", sParamPgmCde, nProductTableCols, True, True)
	
	Response.Write "<tr><td colspan=" & nProductTableCols & " width=5>&nbsp;</td></tr>"	' row spacer
	Response.Write "</table>"

	Call EmitMainPageFooter()	' MAIN PAGE footer

End Function


' ----- MAIN PAGE support functions

Function EmitMainPageHeader()
	' This section displays at the top of the MAIN page
%>
<!--#include virtual="includes/FoodProductsMainPageHeaderInclude.asp"-->
<%
End Function

Function EmitMainPageFooter()
	' This section displays at the bottom of the MAIN page
%>
<!--#include virtual="includes/FoodProductsMainPageFooterInclude.asp"-->
<%
End Function

Function EmitNoActiveProgramMessage()
	' NB: This message displays when there are NO active programs to be displayed
%>
<!--#include virtual="includes/FoodProductsNoActiveProgramInclude.asp"-->
<%
End Function

Function EmitOneProductTableEntry(ByVal sPdctCtgyCde, ByVal sPdctImage, ByVal sImageDescTxt, ByVal sPdctFeatTxt)
	Response.Write "<td width=" & cMainPageProductImageWidth & " align=center valign=top>"
	If sPdctCtgyCde <> "" Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sPdctCtgyCde)) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt & " programs")) & ">"
		Response.Write "<img src=" & sPdctImage & " width=" & cMainPageProductImageWidth & " height=" & cMainPageProductImageHeight & " alt=" & QS(StripHTMLTags(sImageDescTxt & " fundraising programs")) & "><br>"
		Response.Write "<div class=ContentHeader>" & sImageDescTxt & "</div></a>"
		Response.Write "<div class=ContentData>" & sPdctFeatTxt & "</div>"
	Else
		Response.Write "&nbsp;"	
	End If
	Response.Write "</td>"
End Function


' ----- SELLING KIT, REQUEST FOR INFORMATION, ORDER FORM

Function EmitSellingKitProductInfoLinks(sPdctGrpCde, sPdctType, sPdctNme, sPgmCde, nTableCols, bShowSkitLink, bShowPdctInfoLink)
	If bShowPdctInfoLink = True Then
		' request for PRODUCT INFORMATION link
		Response.Write "<tr><td colspan=" & nTableCols & ">&nbsp;</td></tr>"
		Response.Write "<tr><td colspan=" & nTableCols & " align=center class=ContentData>"
		Response.Write "<br><a href=" & QS(ProductInfoRequestASP & AddParam("?", cPdctTypeParam, sPdctType)) & " title=" & QS(StripHTMLTags("Request FREE information for " & sPdctNme)) & "><img src=" & QS(ProductInfoRequestImage) & " alt=" & QS(StripHTMLTags("Request FREE information for " & sPdctNme)) & " border=0></a>"
		Response.Write "</td></tr>"
	End If	
	If bShowSkitLink = True Then
		' request for SELLING KIT link
		Response.Write "<tr><td colspan=" & nTableCols & ">&nbsp;</td></tr>"
		Response.Write "<tr><td colspan=" & nTableCols & " align=center class=ContentData>"
		Response.Write "<br><a href=" & QS(SellingKitRequestASP & AddParam("?", cPdctGrpCdeParam, sPdctGrpCde) & InclIf(sPgmCde<>"", AddParam("&", cPgmCdeParam, sPgmCde), "")) & " title=" & QS("Sign-up to run this fundraiser and receive FREE selling kits for your entire group.") & "><img src=" & QS(SellingKitRequestImage) & " alt=" & QS("FREE selling kits for group") & " border=0></a>"
		Response.Write "</td></tr>"
	End If	
End Function


' -------------------- View program information about a SPECIFIC product category

Function EmitSpecificProductCategory()
	Dim sNextPageURL

	Dim nProductDescriptionWidth: nProductDescriptionWidth = (ContentWindowWidth - (cSpecificProductImageWidth + cSpecificProductSpacerWidth))
	Dim nProductFeatureWidth: nProductFeatureWidth = ContentWindowWidth

	Const cSpecificProductCols = 3

	Call AddParentNavBar(ctgyPdctNme, OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde))
	Call EmitParentNavBar()

	' link to next page (Index)
	sNextPageURL = OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde)

	Response.Write "<br>"

	Response.Write "<table border=0 cellpadding=0 cellspacing=0>"
	Response.Write "<tr>"
	' display product image
	Response.Write "<td width=" & cSpecificProductImageWidth & " align=center valign=top><img src=" & ctgyImageNme & " width=" & cSpecificProductImageWidth & " height=" & cSpecificProductImageHeight & " alt=" & QS(StripHTMLTags(ctgyPdctNme)) & " border=1><br>"
	Response.Write "</td>"
	Response.Write "<td width=" & cSpecificProductSpacerWidth & ">&nbsp;</td>"
	' display text describing this product
	Response.Write "<td width=" & nProductDescriptionWidth & " valign=top class=ContentData>"
'''	Response.Write "<div class=SectionHeader>" & ctgyPdctNme & "</div>"
''' 2/7/08 - SEO: replaced above line with the following
	Response.Write "<H1>" & ctgyPdctNme & "</H1>"
	Response.Write ctgyDescTxt
	Response.Write "</td>"
	Response.Write "</tr>"
	Response.Write "<tr><td colspan=" & cSpecificProductCols & ">&nbsp;</td></tr>"
	' display feature text
	Response.Write "<tr><td width=" & nProductFeatureWidth & " colspan=" & cSpecificProductCols & " class=ContentData>"
	Response.Write ctgyFeatTxt
	Call EmitPrimaryTagProgramLists()	' show the available programs for this product
	Response.Write "</td></tr>"

	' display request for information links
	Call EmitSellingKitProductInfoLinks(cFoodPdctGrpCde, sParamPdctCtgyCde, ctgyPdctNme, sParamPgmCde, cSpecificProductCols, True, True)

	Response.Write "<tr><td colspan=" & cSpecificProductCols & ">&nbsp;</td></tr>"
	Response.Write "</table>"

End Function

Function EmitPrimaryTagProgramLists()
	Dim nIndex
	Dim sTitleLink, sImageLink

	' emit our PROGRAM lists
	If nPrimaryPrograms = 0 And nTagPrograms = 0 Then
		' this is highly unusual case, but we still need to code for it
		Response.Write "<br><div class=HiliteHeader><b>This product is currently not available!</b></div>"
		Exit Function
	End If

	' emit our PRIMARY program list
	If nPrimaryPrograms = 0 Then
		Response.Write "<br>"
		Response.Write "<div class=HiliteHeader><b>This product is not available in a primary program!</b></div>"
	Else
		Response.Write "<br>"
		Response.Write "<div class=HiliteHeader><b>Just select a tasty " & UCase(ctgyPdctNme) & " program...</b></div>"
		Response.Write "<table cellspacing=5 cellpadding=5 border=0>"
		For nIndex = 1 To nPrimaryPrograms
			sTitleLink = "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, PrimaryPrograms(nIndex, cxPrimPgmPgmCde))) & " title=" & QS("View complete information on this fundraising program.") & ">" & "<i>" & PrimaryPrograms(nIndex, cxPrimPgmDescTxt) & "</i></a>"
			sImageLink = "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, PrimaryPrograms(nIndex, cxPrimPgmPgmCde))) & " title=" & QS("View complete information on this fundraising program.") & "><img src=" & QS(ProductImageFileName("ST", PrimaryPrograms(nIndex, cxPrimPgmImagePrfxNme), PrimaryPrograms(nIndex, cxPrimPgmImageExtNme), 1)) & " alt=" & QS(StripHTMLTags(PrimaryPrograms(nIndex, cxPrimPgmDescTxt))) & "></a>"
			Response.Write "<tr>" & _
						"<td>" & sImageLink & "</td>" & _
						"<td valign=middle>" & sTitleLink & InclIf(nvs(PrimaryPrograms(nIndex, cxPrimPgmPrftTxt)) <> "", " <span class=Profit>(" & PrimaryPrograms(nIndex, cxPrimPgmPrftTxt) & " profit)</span>", "") & _
						"<div style=""font-size:4px; height=3px""></div>" & _
						"<font size=1>" & PrimaryPrograms(nIndex, cxPrimPgmExtDescTxt) & "</font>" & _
						"</td>" & _
					"</tr>"
		Next
		Response.Write "</table>"
'		Response.Write "<ul>"
'		For nIndex = 1 To nPrimaryPrograms
'			sURL = "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, PrimaryPrograms(nIndex, cxPrimPgmPgmCde))) & " title=" & QS("View complete information on this fundraising program.") & "><i>" & PrimaryPrograms(nIndex, cxPrimPgmDescTxt) & "</i></a>"
'			Response.Write "<li>" & sURL & InclIf(nvs(PrimaryPrograms(nIndex, cxPrimPgmPrftTxt)) <> "", " <span class=Profit>(" & PrimaryPrograms(nIndex, cxPrimPgmPrftTxt) & " profit)</span>", "") & "<br><br>"
'		Next
'		Response.Write "</ul>"
	End If

	' emit our PRIMARY program list
	If nTagPrograms > 0 Then
		Response.Write "<br>"
		If nPrimaryPrograms = 0 Then
			Response.Write "<div class=HiliteHeader><b>You can tag one of these with one of our other programs.</b></div>"
		Else
			Response.Write "<div class=HiliteHeader><b>...and tag one of these onto your " & UCase(ctgyPdctNme) & " program above.</b></div>"
		End If
		Response.Write "<table cellspacing=5 cellpadding=5 border=0>"
		For nIndex = 1 To nTagPrograms
			sTitleLink = "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, TagPrograms(nIndex, cxTagPgmPgmCde))) & " title=" & QS("View complete information on this fundraising program.") & "><i>" & TagPrograms(nIndex, cxTagPgmDescTxt) & "</i></a>"
			sImageLink = "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, TagPrograms(nIndex, cxTagPgmPgmCde))) & " title=" & QS("View complete information on this fundraising program.") & "><img src=" & QS(ProductImageFileName("ST", TagPrograms(nIndex, cxTagPgmImagePrfxNme), TagPrograms(nIndex, cxTagPgmImageExtNme), 1)) & " alt=" & QS(StripHTMLTags(TagPrograms(nIndex, cxTagPgmDescTxt))) & "></a>"
			Response.Write "<tr>" & _
						"<td>" & sImageLink & "</td>" & _
						"<td valign=middle>" & sTitleLink & InclIf(nvs(TagPrograms(nIndex, cxTagPgmPrftTxt)) <> "", " <span class=Profit>(" & TagPrograms(nIndex, cxTagPgmPrftTxt) & " profit)</span>", "") & _
						"<div style=""font-size:4px; height=3px""></div>" & _
						"<font size=1>" & TagPrograms(nIndex, cxTagPgmExtDescTxt) & "</font>" & _
						"</td>" & _
					"</tr>"
		Next
		Response.Write "</table>"
'		Response.Write "<ul>"
'		For nIndex = 1 To nTagPrograms
'			sURL = "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, TagPrograms(nIndex, cxTagPgmPgmCde))) & " title=" & QS("View complete information on this fundraising program.") & "><i>" & TagPrograms(nIndex, cxTagPgmDescTxt) & "</i></a>"
'			Response.Write "<li>" & sURL & InclIf(nvs(TagPrograms(nIndex, cxTagPgmPrftTxt)) <> "", " <span class=Profit>(" & TagPrograms(nIndex, cxTagPgmPrftTxt) & " profit)</span>", "") & "<br><br>"
'		Next
'		Response.Write "</ul>"
	End If
End Function


' -------------------- View an individual FOOD PROGRAM

Function EmitSpecificProgram()
	Dim sNextPageURL, sNextPageTitle

	' compute image width based on Orientation flag	
	Dim nProgramImageWidth: nProgramImageWidth = InclIf(pgmPortraitPageOrientFlg, cSpecificProgramPortraitWidth, cSpecificProgramLandscapeWidth)
	Dim nProgramDescriptionWidth: nProgramDescriptionWidth = (ContentWindowWidth - (nProgramImageWidth + cSpecificProgramSpacerWidth))
	Dim nProgramFeatureWidth: nProgramFeatureWidth = ContentWindowWidth

	Const cSpecificProgramCols = 3

	Call AddParentNavBar(ctgyPdctNme,	OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde))
	Call AddParentNavBar(pgmPgmNme,		OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde))
	Call EmitParentNavBar()

	' link to next page (Index)	
	sNextPageURL = OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, 1)
	sNextPageTitle = "View all pages of " & pgmImageDescTxt & " brochure."

	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"

	Response.Write "<tr><td colspan=" & cSpecificProgramCols & ">&nbsp;</td></tr>"
	Response.Write "<tr>"
	' display program image (w/ link to index)
	Response.Write "<td width=" & nProgramImageWidth & " align=center valign=top>"
	Response.Write "<a href=" & QS(sNextPageURL) & " title=" & QS(StripHTMLTags(sNextPageTitle)) & "><img src=" & ProductImageFileName("SM", pgmImagePrfxNme, pgmImageExtNme, 1) & " alt=" & QS(StripHTMLTags(sNextPageTitle)) & " border=0></a>"
	Response.Write "<br><div class=ContentData><a href=" & QS(sNextPageURL) & " title=" & QS(StripHTMLTags(sNextPageTitle)) & ">View brochure</a></div>"
	Response.Write "<br>" & pgmShrtFeatTxt
	Response.Write "</td>"
	Response.Write "<td width=" & cSpecificProgramSpacerWidth & ">&nbsp;</td>"
	' display text describing this program
	Response.Write "<td width=" & nProgramDescriptionWidth & " align=left valign=top class=ContentData>" 
'''	Response.Write "<div class=SectionHeader>" & pgmPgmNme & "</div>"
''' 2/7/08 - SEO: replaced above line with the following
	Response.Write "<H1>" & pgmPgmNme & "</H1>"
	Response.Write pgmDescTxt & "</td>"
	Response.Write "</tr>"
	Response.Write "<tr><td colspan=" & cSpecificProgramCols & ">&nbsp;</td></tr>"
	' display feature text
	Response.Write "<tr><td width=" & nProgramFeatureWidth & " colspan=" & cSpecificProgramCols & " class=ContentData>" & pgmFeatTxt & "</td></tr>"

	' display request for information links
	Call EmitSellingKitProductInfoLinks(cFoodPdctGrpCde, sParamPdctCtgyCde, ctgyPdctNme, sParamPgmCde, cSpecificProgramCols, True, True)

	Response.Write "</table>"

End Function


' -------------------- View the Program Page INDEX

Function EmitSpecificProgramIndex()
	Dim nCols, nRows
	Dim nPage
	Dim sURL
	Dim sPDFFileNme
	Dim sPDFLinkTxt
	Dim sPDFSectionTxt
	Dim nFile

	Dim nProgramImageWidth, nProgramImageHeight
	Dim nProgramIndexTNRows, nProgramIndexTNCols
	If pgmPortraitPageOrientFlg = True Then
		' orient the page for PORTRAIT images
		nProgramImageWidth = cProgramIndexPortraitWidth
		nProgramImageHeight = cProgramIndexPortraitHeight
		nProgramIndexTNRows = cProgramIndexPortraitTNRows		' # rows
		nProgramIndexTNCols = cProgramIndexPortraitTNCols		' # cols (per row)
	Else	
		' orient the page for LANDSCAPE images
		nProgramImageWidth = cProgramIndexLandscapeWidth
		nProgramImageHeight = cProgramIndexLandscapeHeight
		nProgramIndexTNRows = cProgramIndexLandscapeTNRows
		nProgramIndexTNCols = cProgramIndexLandscapeTNCols
	End If	
	Dim nProgramIndexCols: nProgramIndexCols = (nProgramIndexTNCols * 2) - 1

	nPage = (nParamIndex * (cProgramIndexTNPerIndexPage)) - (cProgramIndexTNPerIndexPage)
	
	Call AddParentNavBar(ctgyPdctNme,	OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde))
	Call AddParentNavBar(pgmPgmNme,		OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde))
	Call AddParentNavBar("Index",		OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, nParamIndex))
	Call EmitParentNavBar()
'''	Response.Write "<br><div class=SectionHeader>" & pgmPgmNme & " - Index</div><br>"
''' 2/7/08 - SEO: replaced above line with the following
	Response.Write "<br><H1>&nbsp;" & pgmPgmNme & " - Index</H1>"

	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"

	If pgmXtrnPageQty > cProgramIndexTNPerIndexPage Then 
		Call EmitProgramIndexNavBar(nProgramIndexCols)
	End If
	Response.Write "<tr><td colspan=" & nProgramIndexCols & "><hr></td></tr>"
	Response.Write "<tr><td colspan=" & nProgramIndexCols & " align=center class=SmallContentData>Click on image to view page in full screen</td></tr>"
	Response.Write "<tr><td colspan=" & nProgramIndexCols & ">&nbsp;</td></tr>"
	For nRows=1 To nProgramIndexTNRows
		' index row
		Response.Write "<tr>"
		For nCols=1 To nProgramIndexTNCols
			' index col
			nPage = nPage + 1
			If nPage <= pgmXtrnPageQty Then
				sURL = OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cPageParam, nPage)
				Response.Write "<td width=" & nProgramImageWidth & " align=center valign=top class=SmallContentData>"
				Response.Write "<a href=" & QS(sURL) & ">"
				Response.Write "<img src=" & ProductImageFileName("TN", pgmImagePrfxNme, pgmImageExtNme, nPage) & " width=" & nProgramImageWidth & " height=" & nProgramImageHeight & " alt=" & QS(StripHTMLTags(pgmPgmNme & " - Page " & nPage)) & " border=0><br>"
				Response.Write "Page " & nPage
				Response.Write "</a>" 
				Response.Write "</td>"
			Else
				Response.Write "<td width=" & nProgramImageWidth & " align=center class=SmallContentData>&nbsp;</td>"
			End If	
			If nCols < nProgramIndexTNCols Then Response.Write "<td width=" & cProgramIndexSpacerWidth & ">&nbsp;</td>"
		Next
		Response.Write "</tr>"
		Response.Write "<tr><td colspan=" & nProgramIndexCols & ">&nbsp;</td></tr>"
		If nPage >= pgmXtrnPageQty Then Exit For
	Next
	Response.Write "<tr><td colspan=" & nProgramIndexCols & "><hr></td></tr>"
	If pgmXtrnPageQty > cProgramIndexTNPerIndexPage Then Call EmitProgramIndexNavBar(nProgramIndexCols)

	' -- PDF file for this brochure
	If pgmPDFFileQty > 0 Then
		' we have PDF file(s) available for this brochure
		sPDFSectionTxt = "<br>"
		sPDFSectionTxt = sPDFSectionTxt & "<div class=ContentData><b>Note:</b> This brochure is also available as a PDF file for you to download and print."
		sPDFSectionTxt = sPDFSectionTxt & "<ul>"
		For nFile = 1 To pgmPDFFileQty
			If pgmPDFFileQty = 1 Then
				sPDFFileNme = pgmImagePrfxNme & ".pdf"
				sPDFLinkTxt = "View entire brochure"
			Else
				sPDFFileNme = pgmImagePrfxNme & "_Part" & nFile & ".pdf"
				sPDFLinkTxt = "View Part " & nFile & " of " & pgmPgmNme & " brochure"
			End If
			sPDFSectionTxt = sPDFSectionTxt & "<li><a href=" & QS(sPDFFileNme) & " title=" & QS(StripHTMLTags(sPDFLinkTxt & " - in a new browser window")) & " target=" & QS("_new") & ">" & sPDFLinkTxt & "</a> &nbsp;<span class=SmallContentData>(PDF file - <i>requires Adobe Acrobat to view</i>)</span>"
		Next
		sPDFSectionTxt = sPDFSectionTxt & "</ul></div>"
		Response.Write "<tr><td colspan=" & nProgramIndexCols & ">" & sPDFSectionTxt & "</td></tr>"
	End If	

	Response.Write "</table>"

End Function

Function EmitProgramIndexNavBar(ByVal nCols)

	Response.Write "<tr><td colspan=" & nCols & " align=center class=ContentData>"
	If nParamIndex > 1 Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, nParamIndex-1)) & " title=" & QS("View index " & nParamIndex-1) & ">&lt;&nbsp;Prev</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	Else
		Response.Write "&lt;&nbsp;Prev&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	End If	
	If nvn(nParamIndex*cProgramIndexTNPerIndexPage) < nvn(pgmXtrnPageQty) Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, nParamIndex+1)) & " title=" & QS("View index " & nParamIndex+1) & ">Next&nbsp;&gt;</a>"
	Else
		Response.Write "Next&nbsp;&gt;"
	End If	
	Response.Write "</td></tr>"
End Function


' -------------------- View an individual program page from the catalog

Function EmitSpecificProgramPage()
	
	Call AddParentNavBar(ctgyPdctNme,	OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde))
	Call AddParentNavBar(pgmPgmNme,		OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde))
	Call AddParentNavBar("Index",		OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, 1))
	Call AddParentNavBar("Page " & nParamPage, OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cPageParam, nParamPage))
	Call EmitParentNavBar()

'''	Response.Write "<br><div class=SectionHeader>" & pgmPgmNme & " - Page " & nParamPage & " of " & pgmXtrnPageQty & "</div><br>"
''' 2/7/08 - SEO: replaced above line with the following
	Response.Write "<br><H1>&nbsp;" & pgmPgmNme & " - Page " & nParamPage & " of " & pgmXtrnPageQty & "</H1>"

	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"

	Call EmitProgramPageNavBar()
	Response.Write "<tr><td><hr></td></tr>"
	Response.Write "<tr>"
	Response.Write "<td align=center valign=top class=ContentData>"
	Response.Write "<img src=" & ProductImageFileName("", pgmImagePrfxNme, pgmImageExtNme, nParamPage) & " alt=" & QS(StripHTMLTags(pgmPgmNme & " - Page " & nParamPage)) & "></td>"
	Response.Write "</tr>"
	Response.Write "<tr><td><hr></td></tr>"
	Call EmitProgramPageNavBar()

	Response.Write "</table>"

End Function

Function EmitProgramPageNavBar()
	Response.Write "<tr><td align=center class=ContentData>"
	If nParamPage > 1 Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cPageParam, nParamPage-1)) & " title=" & QS("View page " & nParamPage-1) & ">&lt;&nbsp;Prev</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	Else
		Response.Write "&lt;&nbsp;Prev&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	End If	
	Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, 1)) & " title=" & QS("View the brochure index") & ">Index</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	If nParamPage < pgmXtrnPageQty Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPgmCdeParam, sParamPgmCde) & AddParam("&", cPageParam, nParamPage+1)) & " title=" & QS("View page " & nParamPage+1) & ">Next&nbsp;&gt;</a>"
	Else
		Response.Write "Next&nbsp;&gt;"
	End If	
	Response.Write "</td></tr>"
End Function


' ---------- Support routines

Function ConstructProductMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' construct sub-menu
	For i = 1 To nPageProducts
		Call AddSubMenuItem(PageProducts(i, cxPdctMenuTxt), OurASPPage & AddParam("?", cPdctCtgyCdeParam, PageProducts(i, cxPdctPdctCtgyCde)), "View " & PageProducts(i, cxPdctMenuTxt) & " programs", FoodProgramMenuSection)
	Next	
	If nPageProducts = 0 Then 	Call AddSubMenuItem("", "", "", FoodProgramMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)

End Function


Function ProductImageFileName(ByVal sFileType, ByVal sFilePrfx, ByVal sFileExt, ByVal nPageNbr)
	Dim sImageNme

	sImageNme = sFilePrfx		' default with prefix name
	If Instr(sFilePrfx,".") > 0 Then 
		' assume prefix contains full filename
	Else
		' construct our image filename based on params
		If nPageNbr > 0 Then sImageNme = sImageNme & "_Pg" & nPageNbr			' page number
		sImageNme = sImageNme & InclIf(sFileType <> "", "_" & sFileType, "")	' file type
		sImageNme = sImageNme & "." & InclIf(sFileExt <> "", sFileExt, "jpg")	' file extension
	End If
	ProductImageFileName = sImageNme
End Function


' === Site Program Specifics - info gotten from SITE_PgmSpecifics sp()

Function GetSiteProgramSpecifics(ByVal sPgmCde)
	Dim RS, SQLStmt
	Dim sMetaKywdTxt, sMetaDescTxt, sHTMLTitlTxt

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetProgramSpecifics @PgmCde=" & SQS(sPgmCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	If CheckRS(RS) Then
		pgmPgmNme = nvs(RS.Fields("PGM_NME"))
		pgmMenuTxt = nvs(RS.Fields("MENU_TXT"))
		pgmImagePrfxNme = nvs(RS.Fields("IMAGE_PRFX_NME"))
		pgmImageExtNme = nvs(RS.Fields("IMAGE_EXT_NME"))
		pgmImageDescTxt = nvs(RS.Fields("IMAGE_DESC_TXT"))
		pgmShrtFeatTxt = nvs(RS.Fields("SHRT_FEAT_TXT"))
		pgmDescTxt = nvs(RS.Fields("DESC_TXT"))
		pgmFeatTxt = nvs(RS.Fields("FEAT_TXT"))
		pgmXtrnPageQty = nvn(RS.Fields("XTRN_PAGE_QTY"))
		pgmPDFFileQty = nvn(RS.Fields("PDF_FILE_QTY"))
		pgmXtrnStrtDte = nvd(RS.Fields("XTRN_STRT_DTE"))
		pgmXtrnEndDte = nvd(RS.Fields("XTRN_END_DTE"))
		pgmPortraitPageOrientFlg = nvn(RS.Fields("PAGE_ORIENT_PORT_FLG"))

		' NB: only use these params if defined, otherwise we use parent page params
		sMetaKywdTxt = nvs(RS.Fields("META_KYWD_TXT"))
		If sMetaKywdTxt <> "" Then Call AddMETADATATags("KEYWORDS", sMetaKywdTxt)
		sMetaDescTxt = nvs(RS.Fields("META_DESC_TXT"))
		If sMetaDescTxt <> "" Then Call AddMETADATATags("DESCRIPTION", sMetaDescTxt)
		sHTMLTitlTxt = nvs(RS.Fields("HTML_TITL_TXT"))
		If sHTMLTitlTxt <> "" Then sPageCtlHTMLTitle = sHTMLTitlTxt

		RS.Close
	End If
	Set RS = Nothing

	GetSiteProgramSpecifics = (Err.number = 0)
End Function


' === Site Product Category Specifics - info gotten from SITE_PdctCtgySpecifics sp()

Function GetSiteProductCategorySpecifics(ByVal sPdctCtgyCde)
	Dim RS, SQLStmt
	Dim sMetaKywdTxt, sMetaDescTxt, sHTMLTitlTxt

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetProductCategorySpecifics @PdctCtgyCde=" & SQS(sPdctCtgyCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	If CheckRS(RS) Then
		ctgyPdctNme = nvs(RS.Fields("PDCT_CTGY_TXT"))
		ctgyMenuTxt = nvs(RS.Fields("MENU_TXT"))
		ctgyImageNme = nvs(RS.Fields("IMAGE_NME"))
		If ctgyImageNme = "" Then
			ctgyImageNme = PhotoUnavailableImage
		End If
		ctgyImageDescTxt = nvs(RS.Fields("IMAGE_DESC_TXT"))
		ctgyShrtFeatTxt = nvs(RS.Fields("SHRT_FEAT_TXT"))
		ctgyDescTxt = nvs(RS.Fields("DESC_TXT"))
		ctgyFeatTxt = nvs(RS.Fields("FEAT_TXT"))
		ctgyXtrnStrtDte = nvd(RS.Fields("XTRN_STRT_DTE"))
		ctgyXtrnEndDte = nvd(RS.Fields("XTRN_END_DTE"))

		' NB: only use these params if defined, otherwise we use parent page params
		sMetaKywdTxt = nvs(RS.Fields("META_KYWD_TXT"))
		If sMetaKywdTxt <> "" Then Call AddMETADATATags("KEYWORDS", sMetaKywdTxt)
		sMetaDescTxt = nvs(RS.Fields("META_DESC_TXT"))
		If sMetaDescTxt <> "" Then Call AddMETADATATags("DESCRIPTION", sMetaDescTxt)
		sHTMLTitlTxt = nvs(RS.Fields("HTML_TITL_TXT"))
		If sHTMLTitlTxt <> "" Then sPageCtlHTMLTitle = sHTMLTitlTxt

		RS.Close
	End If
	Set RS = Nothing

	' RETRIEVE PRIMARY/TAG programs for given PdctCtgyCde
	Call LoadProgramListForProduct(sPdctCtgyCde)

	GetSiteProductCategorySpecifics = (Err.number = 0)
End Function


' === Load Site Program List

Function LoadProgramListForProduct(ByVal sPdctCtgyCde)
	Dim RS, SQLStmt

	On Error Resume Next
	Call OpenEZMainDB()

	' RETRIEVE PRIMARY/TAG programs for given PdctCtgyCde

	SQLStmt = "SITE_GetProgramListForPdctCtgy @PdctCtgyCde=" & SQS(sPdctCtgyCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	nPrimaryPrograms = 0
	nTagPrograms = 0

	Do While CheckRS(RS)
		If nvs(RS.Fields("SRC_CDE")) = "PRIM" Then
			nPrimaryPrograms = nPrimaryPrograms + 1
			PrimaryPrograms(nPrimaryPrograms, cxPrimPgmPgmCde) = nvs(RS.Fields("PGM_CDE"))
			PrimaryPrograms(nPrimaryPrograms, cxPrimPgmDescTxt) = nvs(RS.Fields("PGM_DESC_TXT"))
			PrimaryPrograms(nPrimaryPrograms, cxPrimPgmExtDescTxt) = nvs(RS.Fields("EXT_PGM_DESC_TXT"))
			PrimaryPrograms(nPrimaryPrograms, cxPrimPgmPrftTxt) = nvs(RS.Fields("PGM_PRFT_TXT"))
			PrimaryPrograms(nPrimaryPrograms, cxPrimPgmImagePrfxNme) = nvs(RS.Fields("IMAGE_PRFX_NME"))
			PrimaryPrograms(nPrimaryPrograms, cxPrimPgmImageExtNme) = nvs(RS.Fields("IMAGE_EXT_NME"))
		Else
			nTagPrograms = nTagPrograms + 1
			TagPrograms(nTagPrograms, cxTagPgmPgmCde) = nvs(RS.Fields("PGM_CDE"))
			TagPrograms(nTagPrograms, cxTagPgmDescTxt) = nvs(RS.Fields("PGM_DESC_TXT"))
			TagPrograms(nTagPrograms, cxTagPgmExtDescTxt) = nvs(RS.Fields("EXT_PGM_DESC_TXT"))
			TagPrograms(nTagPrograms, cxTagPgmPrftTxt) = nvs(RS.Fields("PGM_PRFT_TXT"))
			TagPrograms(nTagPrograms, cxTagPgmImagePrfxNme) = nvs(RS.Fields("IMAGE_PRFX_NME"))
			TagPrograms(nTagPrograms, cxTagPgmImageExtNme) = nvs(RS.Fields("IMAGE_EXT_NME"))
		End If

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	LoadProgramListForProduct = (Err.number = 0)
End Function


' === Load Site Product List for display on FOOD PRODUCT page

Function LoadSitePageProductList(ByVal sPdctGrpCde)
	Dim RS, SQLStmt

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetProductCategoryList @PdctGrpCde=" & SQS(sPdctGrpCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	nPageProducts = 0
	Do While CheckRS(RS)
		nPageProducts = nPageProducts + 1
		If nPageProducts > cMaxPageProducts Then nPageProducts = cMaxPageProducts: Exit Do

		PageProducts(nPageProducts, cxPdctPdctCtgyCde) = nvs(RS.Fields("PDCT_CTGY_CDE"))
		PageProducts(nPageProducts, cxPdctMenuTxt) = nvs(RS.Fields("MENU_TXT"))
		PageProducts(nPageProducts, cxPdctImageNme) = nvs(RS.Fields("IMAGE_NME"))
		If PageProducts(nPageProducts, cxPdctImageNme) = "" Then
			PageProducts(nPageProducts, cxPdctImageNme) = PhotoUnavailableImage
		End If
		PageProducts(nPageProducts, cxPdctImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT"))
		PageProducts(nPageProducts, cxPdctShrtFeatTxt) = nvs(RS.Fields("SHRT_FEAT_TXT"))

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	LoadSitePageProductList = (Err.number = 0)
End Function


%>