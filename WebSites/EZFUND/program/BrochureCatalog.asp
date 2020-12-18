<%@ LANGUAGE="VBScript" %>
<% Option Explicit %>
<% ' 11/2006 - make this page non-cacheable! %>
<!--#include virtual="common/EZExpirePageInclude.asp"-->
<!--#include virtual="common/EZPageTop.asp"-->
<!--#include virtual="common/EZCommonDBUtils.asp"-->
<!--#include virtual="common/EZMainDBUtils.asp"-->
<!--#include virtual="common/SitePageControlInclude.asp"-->
<!--#include virtual="common/GoGreenMenuInclude.asp"-->
<%

	' Our standard page attributes
	Dim OurASPPage:		OurASPPage = BrochureProgramASP
	Dim sPageHeader:	sPageHeader = "BROCHURE CATALOGS"

	' Our default page title
	HTMLTitle = "EZFund.com - Brochure Catalog Fundraising Ideas"

	Dim sPgmGrpCdeAddParamString
	Dim sPdctGrpCdeAddParamString
	Dim sProgramGroupName

	'	-- Main BROCHURE page
	Const cMainBrochuresPerRow = 3				' # brochure images per row
	Const cMainBrochureImageWidth = 150			' width for image cell (value must support mix of Portrait and Landscape thumbnails)
	'Const cMainBrochureImageHeight = 300
	Const cMainBrochureSpacerWidth = 50			' space between brochure images on main BROCHURE page
	'	-- Featured BROCHURE program
	Const cMainBrochureFeaturePortraitWidth = 232	' width for PORTRAIT orientation
	Const cMainBrochureFeatureLandscapeWidth = 300	' width for LANDSCAPE orientation
	Const cMainBrochureFeatureSpacerWidth = 15	' space between image and description
	'	-- Specific BROCHURE page
	Const cSpecificBrochurePortraitWidth = 232		' width for PORTRAIT orientation
	Const cSpecificBrochureLandscapeWidth = 300		' width for LANDSCAPE orientation
	Const cSpecificBrochureSpacerWidth = 15		' space between image and description
	'	-- BROCHURE INDEX page
	' Portrait params
	Const cBrochureIndexPortraitWidth = 111		' width for PORTRAIT orientation
	Const cBrochureIndexPortraitHeight = 150	' height for PORTRAIT orientation
	Const cBrochureIndexPortraitTNRows = 3		' # of thumbnail rows (portrait orientation)
	Const cBrochureIndexPortraitTNCols = 5		' # of thumbnail cols (portrait orientation)
	' Landscape params
	Const cBrochureIndexLandscapeWidth = 150	' width for LANDSCAPE orientation
	Const cBrochureIndexLandscapeHeight = 111	' height for LANDSCAPE orientation
	Const cBrochureIndexLandscapeTNRows = 5		' # of thumbnail rows (landscape orientation)
	Const cBrochureIndexLandscapeTNCols = 3		' # of thumbnail cols (landscape orientation)

	Const cBrochureIndexTNPerIndexPage = 15		' total thumbnails per index page
	Const cBrochureIndexSpacerWidth = 15		' space between brochure images

	' -- SITE variables for Brochure programs
	Dim brocPgmNme			' brochure program name
	Dim brocMenuTxt			' text to display on the menu
	Dim brocImagePrfxNme	' image (prefix) name (may include pathname; assumes current directory)
	Dim brocImageExtNme		' image (extension) name
	Dim brocImageDescTxt	' product name (to display under brochure image)
	Dim brocShrtFeatTxt		' short feature text (to display under product name)
	Dim brocDescTxt			' description text (to display next to brochure image)
	Dim brocFeatTxt			' feature text (to display at bottom of page - under brochure image)
	Dim brocXtrnPageQty		' # pages to display
	Dim brocPDFFileQty		' # PDF files (0=no PDF available)
	Dim brocXtrnStrtDte		' start date to display on the site
	Dim brocXtrnEndDte		' end date to display on the site
	Dim brocPortraitPageOrientFlg	' page orientation; 1=portrait, 0=landscape

	Const cMaxBrochures = 30	' pick a number; we should not reach this number
	Dim Brochures(30,8)
		Const cxBrocPgmCde = 1
		Const cxBrocMenuTxt = 2
		Const cxBrocImagePrfxNme = 3
		Const cxBrocImageExtNme = 4
		Const cxBrocImageDescTxt = 5
		Const cxBrocShrtFeatTxt = 6
		Const cxBrocPortraitPageOrientFlg = 7	' 1=Portrait, 0=Landscape
		Const cxBrocFeatPgmDescTxt = 8
	Dim nBrochures

	' NB: we must get params early to determine behavior for this page
	Call ExtractRequestParams()

	' NAV: define parent and main reports page
	Call InitParentNavBar(HomePageMenuSection, HomePageASP)
	Select Case sParamPgmGrpCde
		Case cGoGreenPgmGrpCde:
				Call AddParentNavBar(GoGreenMenuSection, GoGreenASP)
				sProgramGroupName = "Go-Green"
				HTMLTitle = "EZFund.com - Environment-friendly (Green) Fundraising Ideas"
		Case cHolidayPgmGrpCde:
				Call AddParentNavBar(HolidayStoreMenuSection, HolidayStoreASP)
				sProgramGroupName = "Holiday Store"
				HTMLTitle = "EZFund.com - In-School Holiday Store Fundraising Ideas"
		Case cMagazinePgmGrpCde:
				Call AddParentNavBar(MagazineProgramMenuSection, MagazineProgramASP)
				sProgramGroupName = "Magazine"
				HTMLTitle = "EZFund.com - " & brocPgmNme & " Magazine Fundraising Ideas"
		Case Else:
				Call AddParentNavBar(BrochureProgramMenuSection, OurASPPage)
				sProgramGroupName = "Brochure"
				If brocPgmNme <> "" Then
					HTMLTitle = "EZFund.com - " & brocPgmNme & " Brochure Catalog Fundraising Ideas"
				End If
	End Select
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
	
		Case (sParamPgmCde <> "" And nParamPage <> 0):		Call EmitSpecificBrochurePage()
		Case (sParamPgmCde <> "" And nParamIndex <> 0):		Call EmitSpecificBrochureIndex()
		Case sParamPgmCde <> "":							Call EmitSpecificBrochure()
		Case Else:											Call EmitMainBrochurePage()
				
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
	sParamPgmGrpCde = ""
	sParamPgmCde = ""
	nParamIndex = 0
	nParamPage = 0
End Function

Function ExtractRequestParams()		' Extract all params for this request

	Call ClearRequestParams()
	
	sParamPgmGrpCde = nvs(Request.QueryString(cPgmGrpCdeParam))		' program group
	If nvs(sParamPgmGrpCde) = "" Then sParamPgmGrpCde = cBrochurePgmGrpCde	' default to BROCHURE
	sParamPgmCde = nvs(Request.QueryString(cPgmCdeParam))	' program code
	sParamIndex = nvs(Request.QueryString(cIndexParam))		' index number
	nParamIndex = nvn(sParamIndex)
	sParamPage = nvs(Request.QueryString(cPageParam))		' individual page number
	nParamPage = nvn(sParamPage)

	' Load control params for the PARENT page 
	' (use these params as default; CHILD pages can override)
	' NB: this must be performed early in the initialize process (allow CHILD override downstream)
	Select Case sParamPgmGrpCde
		Case cGoGreenPgmGrpCde:		Call LoadControlParamsForPage(cGoGreenPageCde)
		Case cHolidayPgmGrpCde:		Call LoadControlParamsForPage(cHolidayStorePageCde)
		Case cMagazinePgmGrpCde:	Call LoadControlParamsForPage(cMagazineProgramPageCde)
		Case Else:					Call LoadControlParamsForPage(cBrochureProgramPageCde)
	End Select

	' these values will come from the database
	If sParamPgmCde <> "" Then
		If GetSiteProgramSpecifics(sParamPgmCde) = True Then
			If nParamPage < 0 Or nParamPage > brocXtrnPageQty Then nParamPage = 1
			If nParamIndex < 0 Or nParamIndex > (brocXtrnPageQty+cBrochureIndexTNPerIndexPage)/cBrochureIndexTNPerIndexPage Then nParamIndex = 1
		Else
			Call ClearRequestParams()
		End If	
		' validate params, starting with PgmCde	
		If sParamPgmCde <> "" And (nParamIndex <> 0 Or nParamPage <> 0) Then Call DisableMenuBoard()
	End If

	Call LoadSiteBrochureProgramList(sParamPgmGrpCde)
	Select Case sParamPgmGrpCde 
		Case cGoGreenPgmGrpCde:
			' NB: this string must be AFTER the first param (see "&" vs "?" indicator)
			sPgmGrpCdeAddParamString = AddParam("&", cPgmGrpCdeParam, cGoGreenPgmGrpCde)
			sPdctGrpCdeAddParamString = AddParam("&", cPdctGrpCdeParam, cGoGreenPgmGrpCde)
			Call LoadSiteGoGreenProgramList(sParamPgmGrpCde)	' load special program array for menu
			Call ConstructGoGreenMenu()
			Call ConstructMessageBoard(cGoGreenPageCde)
		Case cHolidayPgmGrpCde:
			' NB: this string must be AFTER the first param (see "&" vs "?" indicator)
			sPgmGrpCdeAddParamString = AddParam("&", cPgmGrpCdeParam, cHolidayPgmGrpCde)
			Call ConstructHolidayStoreMenu()
			Call ConstructMessageBoard(cHolidayStorePageCde)
		Case cMagazinePgmGrpCde:
			' NB: this string must be AFTER the first param (see "&" vs "?" indicator)
			sPgmGrpCdeAddParamString = AddParam("&", cPgmGrpCdeParam, cMagazinePgmGrpCde)
			Call ConstructMagazineMenu()
			Call ConstructMessageBoard(cMagazineProgramPageCde)
		Case Else:
			sPgmGrpCdeAddParamString = ""		' default is BROCHURE page
			Call ConstructBrochureMenu()
			Call ConstructMessageBoard(cBrochureProgramPageCde)
	End Select
	
End Function


' -------------------- View MAIN brochure program

Function EmitMainBrochurePage()
	Dim i, nCols
	Dim sSmallImageNme, sThumbImageNme
	Dim bShowSellingKitOption: bShowSellingKitOption = InclIf(sParamPgmGrpCde = cBrochurePgmGrpCde Or sParamPgmGrpCde = cMagazinePgmGrpCde Or sParamPgmGrpCde = cGoGreenPgmGrpCde, True, False)

	Dim nBrochureTableCols: nBrochureTableCols = (cMainBrochuresPerRow * 2) - 1	' account for spacer columns

	Call EmitParentNavBar()

	If nBrochures = 0 Then
		' no active brochures (or possible database issues!)
		Call EmitNoActiveProgramMessage(sParamPgmGrpCde)	' tell the user something
		Exit Function
	End If

	Call EmitMainPageHeader(sParamPgmGrpCde)	' MAIN PAGE header

	' ----- FEATURED BROCHURE program
	i = 1	' default to the FEATURED BROCHURE program (1st one in the list)
	sSmallImageNme = ProductImageFileName("SM", Brochures(i, cxBrocImagePrfxNme), Brochures(i, cxBrocImageExtNme), 1)
	Call EmitFeaturedBrochureProgram(Brochures(i, cxBrocPgmCde), sSmallImageNme, Brochures(i, cxBrocImageDescTxt), Brochures(i, cxBrocShrtFeatTxt), Brochures(i, cxBrocPortraitPageOrientFlg), Brochures(i, cxBrocFeatPgmDescTxt))

	' ----- other BROCHURE programs
	If (nBrochures > i) Then
		' display other BROCHURE programs beneath the FEATURED program
		Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"
		Response.Write "<tr><td colspan=" & nBrochureTableCols & " align=center class=ContentHeader><br><hr><br>Check out our other " & InclIf(UCase(sProgramGroupName) = "GO-GREEN", "Go-Green", LCase(sProgramGroupName)) & " programs...<br></td></tr>"
		Response.Write "<tr><td colspan=" & nBrochureTableCols & " align=center class=SmallContentData><br><br>Click on image to view " & InclIf(UCase(sProgramGroupName) = "GO-GREEN", "Go-Green", LCase(sProgramGroupName)) & " catalog<br><br></td></tr>"

		' display the available brochure programs (defined in the database)	
		Do While (i < nBrochures)
			' new Brochure row
			Response.Write "<tr>"
			For nCols = 1 To cMainBrochuresPerRow
				' display two Brochures per row
				i = i + 1
				If i <= nBrochures Then
					sThumbImageNme = ProductImageFileName("TN", Brochures(i, cxBrocImagePrfxNme), Brochures(i, cxBrocImageExtNme), 1)
					If sParamPgmGrpCde = cGoGreenPgmGrpCde Then
						Call EmitOneGoGreenTableEntry(GoGreenPrograms(i, cxGoGreenPgmCde), sThumbImageNme, GoGreenPrograms(i, cxGoGreenImageDescTxt), GoGreenPrograms(i, cxGoGreenShrtFeatTxt), GoGreenPrograms(i, cxGoGreenPdctGrpCde))
					Else
						Call EmitOneBrochureTableEntry(Brochures(i, cxBrocPgmCde), sThumbImageNme, Brochures(i, cxBrocImageDescTxt), Brochures(i, cxBrocShrtFeatTxt))
					End If
				Else
					Call EmitOneBrochureTableEntry("", "", "", "")
				End If
				If nCols < cMainBrochuresPerRow Then Response.Write "<td width=" & cMainBrochureSpacerWidth & ">&nbsp;</td>"	' column spacer
			Next
			Response.Write "</tr>"
			Response.Write "<tr><td colspan=" & nBrochureTableCols & ">&nbsp;</td></tr>"	' row spacer
		Loop

		Response.Write "</table>"
	End If	

	Response.Write "<br>"
	
	' display request for information links
	Call EmitSellingKitProductInfoLinks(cBrochurePdctGrpCde, cBrochurePdctGrpCde, "Brochure program", sParamPgmCde, bShowSellingKitOption, True, (sParamPgmGrpCde = cHolidayPgmGrpCde))

	Call EmitMainPageFooter(sParamPgmGrpCde)	' MAIN PAGE footer
	
End Function


' ----- MAIN PAGE support functions

Function EmitMainPageHeader(ByVal sPgmGrpCde)
	' This section displays at the top of the MAIN page
	Select Case sPgmGrpCde 
		Case cGoGreenPgmGrpCde:
%>
<!--#include virtual="includes/GoGreenMainPageHeaderInclude.asp"-->
<%
		Case cHolidayPgmGrpCde:
%>
<!--#include virtual="includes/HolidayStoreMainPageHeaderInclude.asp"-->
<%
		Case cMagazinePgmGrpCde:
%>
<!--#include virtual="includes/MagazineProgramMainPageHeaderInclude.asp"-->
<%
		Case Else:
%>
<!--#include virtual="includes/BrochureCatalogMainPageHeaderInclude.asp"-->
<%
	End Select
End Function

Function EmitMainPageFooter(ByVal sPgmGrpCde)
	' This section displays at the bottom of the MAIN page
	Select Case sPgmGrpCde
		Case cGoGreenPgmGrpCde:
%>
<!--#include virtual="includes/GoGreenMainPageFooterInclude.asp"-->
<%
		Case cHolidayPgmGrpCde:
%>
<!--#include virtual="includes/HolidayStoreMainPageFooterInclude.asp"-->
<%
		Case cMagazinePgmGrpCde:
%>
<!--#include virtual="includes/MagazineProgramMainPageFooterInclude.asp"-->
<%
		Case Else:
%>
<!--#include virtual="includes/BrochureCatalogMainPageFooterInclude.asp"-->
<%
	End Select
End Function

Function EmitNoActiveProgramMessage(ByVal sPgmGrpCde)
	' NB: This message displays when there are NO active programs to be displayed
	Select Case sPgmGrpCde
		Case cGoGreenPgmGrpCde:
%>
<!--#include virtual="includes/GoGreenNoActiveProgramInclude.asp"-->
<%
		Case cHolidayPgmGrpCde:
%>
<!--#include virtual="includes/HolidayStoreNoActiveProgramInclude.asp"-->
<%
		Case cMagazinePgmGrpCde:
%>
<!--#include virtual="includes/MagazineProgramNoActiveProgramInclude.asp"-->
<%
		Case Else:
%>
<!--#include virtual="includes/BrochureCatalogNoActiveProgramInclude.asp"-->
<%
	End Select
End Function

Function EmitOneBrochureTableEntry(ByVal sBrocCde, ByVal sBrocImage, ByVal sImageDescTxt, ByVal sBrocFeatTxt)
	Response.Write "<td width=" & cMainBrochureImageWidth & " align=center valign=top>"
	If sBrocCde <> "" Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sBrocCde) & sPgmGrpCdeAddParamString) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt & " brochure program")) & ">"
		Response.Write "<img src=" & sBrocImage & " alt=" & QS(StripHTMLTags(sImageDescTxt)) & "><br>"
		Response.Write "<div class=ContentHeader>" & sImageDescTxt & "</div>" 
		Response.Write "</a>" 
		Response.Write "<div class=ContentData>" & sBrocFeatTxt & "</div>"
	Else
		Response.Write "&nbsp;"	
	End If
	Response.Write "</td>"
End Function

Function EmitOneGoGreenTableEntry(ByVal sBrocCde, ByVal sBrocImage, ByVal sImageDescTxt, ByVal sBrocFeatTxt, ByVal sPdctGrpCde)
	Response.Write "<td width=" & cMainBrochureImageWidth & " align=center valign=top>"
	If sBrocCde <> "" Then
		If sPdctGrpCde = cDirectPdctGrpCde Then
			Response.Write "<a href=" & QS(DirectSellerGoGreenASP & AddParam("?", cPdctCdeParam, sBrocCde) & sPdctGrpCdeAddParamString) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt & " product")) & ">"
			Response.Write "<img src=" & sBrocImage & " alt=" & QS(StripHTMLTags(sImageDescTxt)) & "><br>"
			Response.Write "<div class=ContentHeader>" & sImageDescTxt & "</div>" 
			Response.Write "</a>" 
			Response.Write "<div class=ContentData>" & sBrocFeatTxt & "</div>"
		Else
			Response.Write "<a href=" & QS(BrochureGoGreenASP & AddParam("?", cPgmCdeParam, sBrocCde) & sPgmGrpCdeAddParamString) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt & " brochure program")) & ">"
			Response.Write "<img src=" & sBrocImage & " alt=" & QS(StripHTMLTags(sImageDescTxt)) & "><br>"
			Response.Write "<div class=ContentHeader>" & sImageDescTxt & "</div>" 
			Response.Write "</a>" 
			Response.Write "<div class=ContentData>" & sBrocFeatTxt & "</div>"
		End If
	Else
		Response.Write "&nbsp;"	
	End If
	Response.Write "</td>"
End Function

Function EmitFeaturedBrochureProgram(ByVal sBrocCde, ByVal sBrocImage, ByVal sImageDescTxt, ByVal sBrocFeatTxt, ByVal bPortraitPage, ByVal sFeatPgmDescTxt)
	Dim nFeaturedBrochureDescriptionWidth
	Dim nFeaturedBrochureImageWidth

	' compute image width based on Orientation flag	
	nFeaturedBrochureImageWidth = InclIf(bPortraitPage, cMainBrochureFeaturePortraitWidth, cMainBrochureFeatureLandscapeWidth)
	nFeaturedBrochureDescriptionWidth = (ContentWindowWidth - (nFeaturedBrochureImageWidth + cMainBrochureFeatureSpacerWidth))

	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"
	Response.Write "<tr>"
	' display FEATURED BROCHURE image
	Response.Write "<td width=" & nFeaturedBrochureImageWidth & " align=center valign=top>"
	Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sBrocCde) & sPgmGrpCdeAddParamString) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt & " brochure program")) & ">"
	Response.Write "<img src=" & sBrocImage & " alt=" & QS(StripHTMLTags(sImageDescTxt)) & "><br>"
	Response.Write "<div class=ContentHeader>" & sImageDescTxt & "</div>" 
	Response.Write "</a>" 
	Response.Write "<div class=ContentData>" & sBrocFeatTxt & "</div>"
	Response.Write "</td>"
	Response.Write "<td width=" & cMainBrochureFeatureSpacerWidth & ">&nbsp;</td>"
	' display text describing the FEATURED BROCHURE program
	Response.Write "<td width=" & nFeaturedBrochureDescriptionWidth & " valign=top class=ContentData>" & sFeatPgmDescTxt & "</td>"
	Response.Write "</tr>"
	Response.Write "</table>"

End Function


' ----- SELLING KIT, REQUEST FOR INFORMATION, ORDER FORM

Function EmitSellingKitProductInfoLinks(ByVal sPdctGrpCde, ByVal sPdctType, ByVal sPdctNme, ByVal sPgmCde, ByVal bShowSkitLink, ByVal bShowPdctInfoLink, ByVal bShowOrderLink)
	If bShowPdctInfoLink = True Then
		' request for PRODUCT INFORMATION link
		Response.Write "<br>"
		Response.Write "<div align=center class=ContentData>"
		Response.Write "<br><a href=" & QS(ProductInfoRequestASP & AddParam("?", cPdctTypeParam, sPdctType)) & " title=" & QS(StripHTMLTags("Request FREE information for " & sPdctNme)) & "><img src=" & QS(ProductInfoRequestImage) & " alt=" & QS(StripHTMLTags("Request FREE information for " & sPdctNme)) & " border=0></a>"
		Response.Write "</div>"
	End If	
	If bShowSkitLink = True Then
		' request for SELLING KIT link
		Response.Write "<br>"
		Response.Write "<div align=center class=ContentData>"
		Response.Write "<br><a href=" & QS(SellingKitRequestASP & AddParam("?", cPdctGrpCdeParam, sPdctGrpCde) & InclIf(sPgmCde<>"", AddParam("&", cPgmCdeParam, sPgmCde), "")) & " title=" & QS("Sign-up to run this fundraiser and receive FREE selling kits for your entire group.") & "><img src=" & QS(SellingKitRequestImage) & " alt=" & QS("FREE selling kits for group") & " border=0></a>"
		Response.Write "</div>"
	End If	
	If bShowOrderLink = True Then
		' Order instructions message
		Response.Write "<br>"
		Response.Write "<div align=center class=ContentData>"
		Response.Write "Please call <b>" & EZSalesPhone & "</b> or <b>" & EZSalesLocalPhone & "</b> to place your order TODAY."
		Response.Write "</div>"
	End If	
End Function


' -------------------- View program information about a SPECIFIC brochure

Function EmitSpecificBrochure()
	Dim sNextPageURL
	Dim bShowSellingKitOption: bShowSellingKitOption = InclIf(sParamPgmGrpCde = cBrochurePgmGrpCde Or sParamPgmGrpCde = cMagazinePgmGrpCde Or sParamPgmGrpCde = cGoGreenPgmGrpCde, True, False)

	' compute image width based on Orientation flag	
	Dim nBrochureImageWidth: nBrochureImageWidth = InclIf(brocPortraitPageOrientFlg, cSpecificBrochurePortraitWidth, cSpecificBrochureLandscapeWidth)
	Dim nBrochureDescriptionWidth: nBrochureDescriptionWidth = (ContentWindowWidth - (nBrochureImageWidth + cSpecificBrochureSpacerWidth))
	Dim nBrochureFeatureWidth: nBrochureFeatureWidth = ContentWindowWidth

	Const cSpecificBrochureCols = 3

	Call AddParentNavBar(brocPgmNme, OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString)
	Call EmitParentNavBar()

	' link to next page (Index)	
	sNextPageURL = OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cIndexParam, 1)

	Response.Write "<br>"
	
	Response.Write "<table border=0 cellpadding=0 cellspacing=0>"
	Response.Write "<tr>"
	' display brochure image
	Response.Write "<td width=" & nBrochureImageWidth & " align=center valign=top><a href=" & QS(sNextPageURL) & "><img src=" & ProductImageFileName("SM", brocImagePrfxNme, brocImageExtNme,1) & " alt=" & QS(StripHTMLTags(brocPgmNme)) & "></a><br>"
	Response.Write "<br>"
	Response.Write "<div class=ContentData><a href=" & QS(sNextPageURL) & ">Flip through this brochure</a></div>"
	Response.Write "</td>"
	Response.Write "<td width=" & cSpecificBrochureSpacerWidth & ">&nbsp;</td>"
	' display text describing the brochure program
	Response.Write "<td width=" & nBrochureDescriptionWidth & " valign=top class=ContentData>"
'''	Response.Write "<div class=SectionHeader>" & brocPgmNme & "</div>"
''' 2/8/08 - SEO: replaced above line with the following
	Response.Write "<H1>" & brocPgmNme & "</H1>"
	Response.Write brocDescTxt
	Response.Write "</td>"
	Response.Write "</tr>"
	Response.Write "<tr><td colspan=" & cSpecificBrochureCols & ">&nbsp;</td></tr>"
	' display feature text
	Response.Write "<tr><td width=" & nBrochureFeatureWidth & " colspan=" & cSpecificBrochureCols & " class=ContentData>" & brocFeatTxt & "</td></tr>"
	Response.Write "</table>"

	Response.Write "<br>"

	' display request for information links
	Call EmitSellingKitProductInfoLinks(cBrochurePdctGrpCde, cBrochurePdctGrpCde, brocPgmNme, sParamPgmCde, bShowSellingKitOption, True, (sParamPgmGrpCde = cHolidayPgmGrpCde))

End Function


' -------------------- View the brochure INDEX

Function EmitSpecificBrochureIndex()
	Dim nCols, nRows
	Dim nPage
	Dim sURL
	Dim sPDFFileNme
	Dim sPDFLinkTxt
	Dim sPDFSectionTxt
	Dim nFile

	Dim nBrochureImageWidth, nBrochureImageHeight
	Dim nBrochureIndexTNRows, nBrochureIndexTNCols
	If brocPortraitPageOrientFlg = True Then
		' orient the page for PORTRAIT images
		nBrochureImageWidth = cBrochureIndexPortraitWidth
		nBrochureImageHeight = cBrochureIndexPortraitHeight
		nBrochureIndexTNRows = cBrochureIndexPortraitTNRows		' # rows
		nBrochureIndexTNCols = cBrochureIndexPortraitTNCols		' # cols (per row)
	Else	
		' orient the page for LANDSCAPE images
		nBrochureImageWidth = cBrochureIndexLandscapeWidth
		nBrochureImageHeight = cBrochureIndexLandscapeHeight
		nBrochureIndexTNRows = cBrochureIndexLandscapeTNRows
		nBrochureIndexTNCols = cBrochureIndexLandscapeTNCols
	End If	
	Dim nBrochureIndexCols: nBrochureIndexCols = (nBrochureIndexTNCols * 2) - 1

	nPage = (nParamIndex * (cBrochureIndexTNPerIndexPage)) - (cBrochureIndexTNPerIndexPage)
	
	Call AddParentNavBar(brocPgmNme, OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString)
	Call AddParentNavBar("Index", OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cIndexParam, nParamIndex))
	Call EmitParentNavBar()
'''	Response.Write "<br><div class=SectionHeader>" & brocPgmNme & " - Index</div><br>"
''' 2/8/08 - SEO: replaced above line with the following
	Response.Write "<H1>" & brocPgmNme & " - Index</H1>"

	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"

	If brocXtrnPageQty > cBrochureIndexTNPerIndexPage Then 
		Call EmitBrochureIndexNavBar(nBrochureIndexCols)
	End If
	Response.Write "<tr><td colspan=" & nBrochureIndexCols & "><hr></td></tr>"
	Response.Write "<tr><td colspan=" & nBrochureIndexCols & " align=center class=SmallContentData>Click on image to view page in full screen</td></tr>"
	Response.Write "<tr><td colspan=" & nBrochureIndexCols & ">&nbsp;</td></tr>"
	For nRows=1 To nBrochureIndexTNRows
		' index row
		Response.Write "<tr>"
		For nCols=1 To nBrochureIndexTNCols
			' index col
			nPage = nPage + 1
			If nPage <= brocXtrnPageQty Then
				sURL = OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cPageParam, nPage)
				Response.Write "<td width=" & nBrochureImageWidth & " align=center valign=top class=SmallContentData>"
				Response.Write "<a href=" & QS(sURL) & ">"
				Response.Write "<img src=" & ProductImageFileName("TN", brocImagePrfxNme, brocImageExtNme, nPage) & " width=" & nBrochureImageWidth & " height=" & nBrochureImageHeight & " alt=" & QS(StripHTMLTags(brocPgmNme & " - Page " & nPage)) & "><br>"
				Response.Write "Page " & nPage
				Response.Write "</a>" 
				Response.Write "</td>"
			Else
				Response.Write "<td width=" & nBrochureImageWidth & " align=center class=SmallContentData>&nbsp;</td>"
			End If	
			If nCols < nBrochureIndexTNCols Then Response.Write "<td width=" & cBrochureIndexSpacerWidth & ">&nbsp;</td>"
		Next
		Response.Write "</tr>"
		Response.Write "<tr><td colspan=" & nBrochureIndexCols & ">&nbsp;</td></tr>"
		If nPage >= brocXtrnPageQty Then Exit For
	Next
	Response.Write "<tr><td colspan=" & nBrochureIndexCols & "><hr></td></tr>"
	If brocXtrnPageQty > cBrochureIndexTNPerIndexPage Then Call EmitBrochureIndexNavBar(nBrochureIndexCols)

	' -- PDF file for this brochure
	If brocPDFFileQty > 0 Then
		' we have PDF file(s) available for this brochure
		sPDFSectionTxt = "<br>"
		sPDFSectionTxt = sPDFSectionTxt & "<div class=ContentData><b>Note:</b> This brochure is also available as a PDF file for you to download and print."
		sPDFSectionTxt = sPDFSectionTxt & "<ul>"
		For nFile = 1 To brocPDFFileQty
			If brocPDFFileQty = 1 Then
				sPDFFileNme = brocImagePrfxNme & ".pdf"
				sPDFLinkTxt = "View entire brochure"
			Else
				sPDFFileNme = brocImagePrfxNme & "_Part" & nFile & ".pdf"
				sPDFLinkTxt = "View Part " & nFile & " of " & brocPgmNme & " brochure"
			End If
			sPDFSectionTxt = sPDFSectionTxt & "<li><a href=" & QS(sPDFFileNme) & " title=" & QS(StripHTMLTags(sPDFLinkTxt & " - in a new browser window")) & " target=" & QS("_new") & ">" & sPDFLinkTxt & "</a> &nbsp;<span class=SmallContentData>(PDF file - <i>requires Adobe Acrobat to view</i>)</span>"
		Next
		sPDFSectionTxt = sPDFSectionTxt & "</ul></div>"
		Response.Write "<tr><td colspan=" & nBrochureIndexCols & ">" & sPDFSectionTxt & "</td></tr>"
	End If	

	Response.Write "</table>"

End Function

Function EmitBrochureIndexNavBar(ByVal nCols)

	Response.Write "<tr><td colspan=" & nCols & " align=center class=ContentData>"
	If nParamIndex > 1 Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cIndexParam, nParamIndex-1)) & " title=" & QS("View index " & nParamIndex-1) & ">&lt;&nbsp;Prev</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	Else
		Response.Write "&lt;&nbsp;Prev&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	End If	
	If nvn(nParamIndex*cBrochureIndexTNPerIndexPage) < nvn(brocXtrnPageQty) Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cIndexParam, nParamIndex+1)) & " title=" & QS("View index " & nParamIndex+1) & ">Next&nbsp;&gt;</a>"
	Else
		Response.Write "Next&nbsp;&gt;"
	End If	
	Response.Write "</td></tr>"
End Function


' -------------------- View an individual brochure page from the catalog

Function EmitSpecificBrochurePage()

	Call AddParentNavBar(brocPgmNme, OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString)
	Call AddParentNavBar("Index", OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cIndexParam, 1))
	Call AddParentNavBar("Page " & nParamPage, OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cPageParam, nParamPage))
	Call EmitParentNavBar()

'''	Response.Write "<br><div class=SectionHeader>" & brocPgmNme & " - Page " & nParamPage & " of " & brocXtrnPageQty & "</div><br>"
''' 2/8/08 - SEO: replaced above line with the following
	Response.Write "<H1>" & brocPgmNme & " - Page " & nParamPage & " of " & brocXtrnPageQty & "</H1>"

	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"

	Call EmitBrochurePageNavBar()
	Response.Write "<tr><td><hr></td></tr>"
	Response.Write "<tr>"
	Response.Write "<td align=center valign=top class=ContentData>"
	Response.Write "<img src=" & ProductImageFileName("", brocImagePrfxNme, brocImageExtNme, nParamPage) & " alt=" & QS(StripHTMLTags(brocPgmNme & " - Page " & nParamPage)) & "></td>"
	Response.Write "</tr>"
	Response.Write "<tr><td><hr></td></tr>"
	Call EmitBrochurePageNavBar()

	Response.Write "</table>"

End Function

Function EmitBrochurePageNavBar()
	Response.Write "<tr><td align=center class=ContentData>"
	If nParamPage > 1 Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cPageParam, nParamPage-1)) & " title=" & QS("View page " & nParamPage-1) & ">&lt;&nbsp;Prev</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	Else
		Response.Write "&lt;&nbsp;Prev&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	End If	
	Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cIndexParam, 1)) & " title=" & QS("View the brochure index") & ">Index</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	If nParamPage < brocXtrnPageQty Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & sPgmGrpCdeAddParamString & AddParam("&", cPageParam, nParamPage+1)) & " title=" & QS("View page " & nParamPage+1) & ">Next&nbsp;&gt;</a>"
	Else
		Response.Write "Next&nbsp;&gt;"
	End If	
	Response.Write "</td></tr>"
End Function


' ---------- Support routines

Function ConstructBrochureMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' construct sub-menu
	For i = 1 To nBrochures
		Call AddSubMenuItem(Brochures(i, cxBrocMenuTxt), OurASPPage & AddParam("?", cPgmCdeParam, Brochures(i, cxBrocPgmCde)), "View " & Brochures(i, cxBrocMenuTxt) & " brochure", BrochureProgramMenuSection)
	Next
	If nBrochures = 0 Then 	Call AddSubMenuItem("", "", "", BrochureProgramMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)

End Function

Function ConstructMagazineMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' construct sub-menu
	For i = 1 To nBrochures
		Call AddSubMenuItem(Brochures(i, cxBrocMenuTxt), OurASPPage & AddParam("?", cPgmCdeParam, Brochures(i, cxBrocPgmCde)) & AddParam("&", cPgmGrpCdeParam, cMagazinePgmGrpCde), "View " & Brochures(i, cxBrocMenuTxt) & " brochure", MagazineProgramMenuSection)
	Next
	If nBrochures = 0 Then 	Call AddSubMenuItem("", "", "", MagazineProgramMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)

End Function

Function ConstructHolidayStoreMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' construct sub-menu
	For i = 1 To nBrochures
		Call AddSubMenuItem(Brochures(i, cxBrocMenuTxt), OurASPPage & AddParam("?", cPgmCdeParam, Brochures(i, cxBrocPgmCde)) & AddParam("&", cPgmGrpCdeParam, cHolidayPgmGrpCde), "View " & Brochures(i, cxBrocMenuTxt) & " brochure", HolidayStoreMenuSection)
	Next
	If nBrochures = 0 Then 	Call AddSubMenuItem("", "", "", HolidayStoreMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)

End Function

' REMOVE THIS! moved to GoGreenMenuInclude file
'
'Function ConstructGoGreenMenu()
'	Dim i
'
'	' main menu
'	Call ConstructMainMenu()
'	' construct sub-menu
'	For i = 1 To nBrochures
'		Call AddSubMenuItem(Brochures(i, cxBrocMenuTxt), OurASPPage & AddParam("?", cPgmCdeParam, Brochures(i, cxBrocPgmCde)) & AddParam("&", cPgmGrpCdeParam, cGoGreenPgmGrpCde), "View " & Brochures(i, cxBrocMenuTxt) & " brochure", GoGreenMenuSection)
'	Next
'	If nBrochures = 0 Then 	Call AddSubMenuItem("", "", "", GoGreenMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
'	' menu footer
'	' if defined (in database) use footer for this page, otherwise use the site default
'	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)
'
'End Function


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



' === Site Program Specifics - info gotten from SITE_GetProgramSpecifics sp()

Function GetSiteProgramSpecifics(ByVal sPgmCde)
	Dim RS, SQLStmt
	Dim sMetaKywdTxt, sMetaDescTxt, sHTMLTitlTxt
	
	On Error Resume Next
	Call OpenEZMainDB()
	
	SQLStmt = "SITE_GetProgramSpecifics @PgmCde=" & SQS(sPgmCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	If CheckRS(RS) Then
		brocPgmNme = nvs(RS.Fields("PGM_NME"))
		brocMenuTxt = nvs(RS.Fields("MENU_TXT"))
		brocImagePrfxNme = nvs(RS.Fields("IMAGE_PRFX_NME"))
		brocImageExtNme = nvs(RS.Fields("IMAGE_EXT_NME"))
		brocImageDescTxt = nvs(RS.Fields("IMAGE_DESC_TXT"))
		brocShrtFeatTxt = nvs(RS.Fields("SHRT_FEAT_TXT"))
		brocDescTxt = nvs(RS.Fields("DESC_TXT"))
		brocFeatTxt = nvs(RS.Fields("FEAT_TXT"))
		brocXtrnPageQty = nvn(RS.Fields("XTRN_PAGE_QTY"))
		brocPDFFileQty = nvn(RS.Fields("PDF_FILE_QTY"))
		brocXtrnStrtDte = nvd(RS.Fields("XTRN_STRT_DTE"))
		brocXtrnEndDte = nvd(RS.Fields("XTRN_END_DTE"))
		brocPortraitPageOrientFlg = nvn(RS.Fields("PAGE_ORIENT_PORT_FLG"))

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


' === Load Site Program List

Function LoadSiteBrochureProgramList(ByVal sPgmGrpCde)
	Dim RS, SQLStmt
	
	On Error Resume Next
	Call OpenEZMainDB()
	
	SQLStmt = "SITE_GetProgramList @PgmGrpCde=" & SQS(sPgmGrpCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	
	nBrochures = 0
	Do While CheckRS(RS)
		nBrochures = nBrochures + 1
		If nBrochures > cMaxBrochures Then nBrochures = cMaxBrochures: Exit Do

		Brochures(nBrochures, cxBrocPgmCde) = nvs(RS.Fields("PGM_CDE"))
		Brochures(nBrochures, cxBrocMenuTxt) = nvs(RS.Fields("MENU_TXT"))
		Brochures(nBrochures, cxBrocImagePrfxNme) = nvs(RS.Fields("IMAGE_PRFX_NME"))
		Brochures(nBrochures, cxBrocImageExtNme) = nvs(RS.Fields("IMAGE_EXT_NME"))
		Brochures(nBrochures, cxBrocImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT"))
		Brochures(nBrochures, cxBrocShrtFeatTxt) = nvs(RS.Fields("SHRT_FEAT_TXT"))
		Brochures(nBrochures, cxBrocPortraitPageOrientFlg) = nvn(RS.Fields("PAGE_ORIENT_PORT_FLG"))
		Brochures(nBrochures, cxBrocFeatPgmDescTxt) = nvs(RS.Fields("FEAT_PGM_DESC_TXT"))
		
		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing
	
	LoadSiteBrochureProgramList = (Err.number = 0)
End Function


%>