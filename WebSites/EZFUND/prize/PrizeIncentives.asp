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
	Dim OurASPPage:		OurASPPage = PrizeIncentiveASP
	Dim sPageHeader:	sPageHeader = "Prize Incentives"

	' Our default page title
	HTMLTitle = "EZFund.com - Prize incentives will INCREASE your fundraiser PROFITS!"

	' PRIZE PROGRAM image display params
	'	-- Main PRIZE page
	Const cMainPrizesPerRow = 3					' # prize images per row
	Const cMainPrizeImageWidth = 150			' width for image cell (value must support mix of Portrait and Landscape thumbnails)
	'Const cMainPrizeImageHeight = 300
	Const cMainPrizeSpacerWidth = 50			' space between prize images on main PRIZE page
	'	-- Featured PRIZE program
	Const cMainPrizeFeaturePortraitWidth = 232	' width for PORTRAIT orientation
	Const cMainPrizeFeatureLandscapeWidth = 300	' width for LANDSCAPE orientation
	Const cMainPrizeFeatureSpacerWidth = 15		' space between image and description

	'	-- Specific PRIZE page
	Const cSpecificBrochurePortraitWidth = 232	' width for PORTRAIT orientation
	Const cSpecificBrochureLandscapeWidth = 300	' width for LANDSCAPE orientation
	Const cSpecificBrochureSpacerWidth = 15		' space between image and description

	'	-- PRIZE INDEX page
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

	Const cBrochureIndexSpacerWidth = 15		' space between prize images
	Const cBrochureIndexTNPerIndexPage = 15		' total thumbnails per index page

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

	Const cMaxPrizes = 20		' pick a number; we should not reach this number
	Dim Prizes(20,8)
		Const cxPrzpPgmCde = 1
		Const cxPrzpMenuTxt = 2
		Const cxPrzpImagePrfxNme = 3
		Const cxPrzpImageExtNme = 4
		Const cxPrzpImageDescTxt = 5
		Const cxPrzpShrtFeatTxt = 6
		Const cxPrzpPortraitPageOrientFlg = 7	' 1=Portrait, 0=Landscape
		Const cxPrzpFeatPgmDescTxt = 8
	Dim nPrizes

	' NAV: define parent and main reports page
	Call InitParentNavBar(HomePageMenuSection, HomePageASP)
	Call AddParentNavBar(PrizeIncentiveMenuSection, OurASPPage)

	Call ExtractRequestParams()
	
	If brocPgmNme <> "" Then
		HTMLTitle = "EZFund.com - " & brocPgmNme & " Prize Program will INCREASE your fundraiser PROFITS!"
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
	
		Case (sParamPgmCde <> "" And nParamPage <> 0):		Call EmitSpecificBrochurePage()
		Case (sParamPgmCde <> "" And nParamIndex <> 0):		Call EmitSpecificBrochureIndex()
		Case sParamPgmCde <> "":							Call EmitSpecificBrochure()
		Case Else:											Call EmitMainPrizePage()
				
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
	sParamPgmCde = ""
	nParamIndex = 0
	nParamPage = 0
End Function

Function ExtractRequestParams()		' Extract all params for this request

	Call ClearRequestParams()

	' Load control params for the PARENT page 
	' (use these params as default; CHILD pages can override)
	' NB: this must be performed early in the initialize process (allow CHILD override downstream)
	Call LoadControlParamsForPage(cPrizeIncentivePageCde)

	sParamPgmCde = nvs(Request.QueryString(cPgmCdeParam))	' program code
	sParamIndex = nvs(Request.QueryString(cIndexParam))		' index number
	nParamIndex = nvn(sParamIndex)
	sParamPage = nvs(Request.QueryString(cPageParam))		' individual page number
	nParamPage = nvn(sParamPage)

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

	Call LoadSitePrizeList()
	Call ConstructPrizeMenu()
	Call ConstructMessageBoard(cPrizeIncentivePageCde)

End Function


' -------------------- View MAIN prize program page

Function EmitMainPrizePage()
	Dim i, nCols

	Dim nPrizeTableCols: nPrizeTableCols = (cMainPrizesPerRow * 2) - 1	' account for spacer columns

	Call EmitParentNavBar()

	If nPrizes = 0 Then
		' no active prize programs (or possible database issues!)
		Call EmitNoActiveProgramMessage()	' tell the user something
		Exit Function
	End If

	Call EmitMainPageHeader()	' MAIN PAGE header

	' ----- FEATURED PRIZE program
	i = 1	' default to the FEATURED PRIZE program (1st one in the list)
	Call EmitFeaturedPrizeProgram(Prizes(i, cxPrzpPgmCde), ProductImageFileName("SM", Prizes(i, cxPrzpImagePrfxNme), Prizes(i, cxPrzpImageExtNme), 1), Prizes(i, cxPrzpImageDescTxt), Prizes(i, cxPrzpShrtFeatTxt), Prizes(i, cxPrzpPortraitPageOrientFlg), Prizes(i, cxPrzpFeatPgmDescTxt))

	' ----- other PRIZE programs
	If (i+1 < nPrizes) Then
		' display other PRIZE programs beneath the FEATURED program
		Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"
		Response.Write "<tr><td colspan=" & nPrizeTableCols & " align=center class=ContentHeader><br><hr><br>Check out our other prize incentive programs...<br></td></tr>"
		Response.Write "<tr><td colspan=" & nPrizeTableCols & " align=center class=SmallContentData><br><br>Click on image to view prize program<br><br></td></tr>"

		' display the available prize programs (defined in the database)	
		Do While (i < nPrizes)
			' new Brochure row
			Response.Write "<tr>"
			For nCols = 1 To cMainPrizesPerRow
				' display (x) Prize Programs per row
				i = i + 1
				If i <= nPrizes Then
					Call EmitOnePrizeTableEntry(Prizes(i, cxPrzpPgmCde), ProductImageFileName("TN", Prizes(i, cxPrzpImagePrfxNme), Prizes(i, cxPrzpImageExtNme), 1), Prizes(i, cxPrzpImageDescTxt), Prizes(i, cxPrzpShrtFeatTxt))
				Else
					Call EmitOnePrizeTableEntry("", "", "", "")
				End If
				If nCols < cMainPrizesPerRow Then Response.Write "<td width=" & cMainPrizeSpacerWidth & ">&nbsp;</td>"	' column spacer
			Next
			Response.Write "</tr>"
			Response.Write "<tr><td colspan=" & nPrizeTableCols & ">&nbsp;</td></tr>"	' row spacer
		Loop

		Response.Write "</table>"
	End If	

	Call EmitMainPageFooter()	' MAIN PAGE footer
	
End Function


' ----- MAIN PAGE support functions

Function EmitMainPageHeader()
	' This section displays at the top of the MAIN page
%>
<!--#include virtual="includes/PrizeIncentivesMainPageHeaderInclude.asp"-->
<%
End Function

Function EmitMainPageFooter()
	' This section displays at the bottom of the MAIN page
%>
<!--#include virtual="includes/PrizeIncentivesMainPageFooterInclude.asp"-->
<%
End Function

Function EmitNoActiveProgramMessage()
	' NB: This message displays when there are NO active programs to be displayed
%>
<!--#include virtual="includes/PrizeIncentivesNoActiveProgramInclude.asp"-->
<%
End Function

Function EmitOnePrizeTableEntry(ByVal sPrzpCde, ByVal sPrzpImage, ByVal sImageDescTxt, ByVal sPrzpFeatTxt)
	Response.Write "<td width=" & cMainPrizeImageWidth & " align=center valign=top>"
	If sPrzpCde <> "" Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sPrzpCde)) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt & " prize program")) & ">"
		Response.Write "<img src=" & sPrzpImage & " alt=" & QS(StripHTMLTags(sImageDescTxt)) & "><br>"
		Response.Write "<div class=ContentHeader>" & sImageDescTxt & "</div>" 
		Response.Write "</a>" 
		Response.Write "<div class=ContentData>" & sPrzpFeatTxt & "</div>"
	Else
		Response.Write "&nbsp;"	
	End If
	Response.Write "</td>"
End Function

Function EmitFeaturedPrizeProgram(ByVal sPrzpCde, ByVal sPrzpImage, ByVal sImageDescTxt, ByVal sPrzpFeatTxt, ByVal bPortraitPage, ByVal sFeatPgmDescTxt)
	Dim nFeaturedPrizeDescriptionWidth
	Dim nFeaturedPrizeImageWidth

	' compute image width based on Orientation flag	
	nFeaturedPrizeImageWidth = InclIf(bPortraitPage, cMainPrizeFeaturePortraitWidth, cMainPrizeFeatureLandscapeWidth)
	nFeaturedPrizeDescriptionWidth = (ContentWindowWidth - (nFeaturedPrizeImageWidth + cMainPrizeFeatureSpacerWidth))

	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"
	Response.Write "<tr>"
	' display FEATURED PRIZE image
	Response.Write "<td width=" & nFeaturedPrizeImageWidth & " align=center valign=top>"
	Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sPrzpCde)) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt & " prize program")) & ">"
	Response.Write "<img src=" & sPrzpImage & " alt=" & QS(StripHTMLTags(sImageDescTxt)) & "><br>"
	Response.Write "<div class=ContentHeader>" & sImageDescTxt & "</div>" 
	Response.Write "</a>" 
	Response.Write "<div class=ContentData>" & sPrzpFeatTxt & "</div>"
	Response.Write "</td>"
	Response.Write "<td width=" & cMainPrizeFeatureSpacerWidth & ">&nbsp;</td>"
	' display text describing the FEATURED PRIZE program
	Response.Write "<td width=" & nFeaturedPrizeDescriptionWidth & " valign=top class=ContentData>" & sFeatPgmDescTxt & "</td>"
	Response.Write "</tr>"
	Response.Write "</table>"

End Function


' -------------------- View program information about a SPECIFIC brochure

Function EmitSpecificBrochure()
	Dim sNextPageURL, sNextPageTitle

	' compute image width based on Orientation flag	
	Dim nBrochureImageWidth: nBrochureImageWidth = InclIf(brocPortraitPageOrientFlg, cSpecificBrochurePortraitWidth, cSpecificBrochureLandscapeWidth)
	Dim nBrochureDescriptionWidth: nBrochureDescriptionWidth = (ContentWindowWidth - (nBrochureImageWidth + cSpecificBrochureSpacerWidth))
	Dim nBrochureFeatureWidth: nBrochureFeatureWidth = ContentWindowWidth

	Const cSpecificBrochureCols = 3

	Call AddParentNavBar(brocPgmNme, OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde))
	Call EmitParentNavBar()

	' link to next page (Index)	
	sNextPageURL = OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, 1)
	sNextPageTitle = "View all pages of " & brocPgmNme & " prize brochure."

	Response.Write "<br>"
	
	Response.Write "<table border=0 cellpadding=0 cellspacing=0>"
	Response.Write "<tr>"
	' display brochure image
	Response.Write "<td width=" & nBrochureImageWidth & " align=center valign=top>"
	Response.Write "<a href=" & QS(sNextPageURL) & " title=" & QS(StripHTMLTags(sNextPageTitle)) & "><img src=" & ProductImageFileName("SM", brocImagePrfxNme, brocImageExtNme, 1) & " alt=" & QS(StripHTMLTags(brocPgmNme)) & " border=0></a><br>"
	Response.Write "<br><div class=ContentData><a href=" & QS(sNextPageURL) & " title=" & QS(StripHTMLTags(sNextPageTitle)) & ">View prize brochure</a></div>"
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
	
	Call AddParentNavBar(brocPgmNme, OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde))
	Call AddParentNavBar("Index", OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, nParamIndex))
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
				sURL = OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cPageParam, nPage)
				Response.Write "<td width=" & nBrochureImageWidth & " align=center valign=top class=SmallContentData>"
				Response.Write "<a href=" & QS(sURL) & ">"
				Response.Write "<img src=" & ProductImageFileName("TN", brocImagePrfxNme, brocImageExtNme, nPage) & " width=" & nBrochureImageWidth & " height=" & nBrochureImageHeight & " alt=" & QS(StripHTMLTags(brocPgmNme & " - Page " & nPage)) & " border=0><br>"
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
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, nParamIndex-1)) & " title=" & QS("View index " & nParamIndex-1) & ">&lt;&nbsp;Prev</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	Else
		Response.Write "&lt;&nbsp;Prev&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	End If	
	If nvn(nParamIndex*cBrochureIndexTNPerIndexPage) < nvn(brocXtrnPageQty) Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, nParamIndex+1)) & " title=" & QS("View index " & nParamIndex+1) & ">Next&nbsp;&gt;</a>"
	Else
		Response.Write "Next&nbsp;&gt;"
	End If	
	Response.Write "</td></tr>"
End Function


' -------------------- View an individual brochure page from the catalog

Function EmitSpecificBrochurePage()
	
	Call AddParentNavBar(brocPgmNme, OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde))
	Call AddParentNavBar("Index", OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, 1))
	Call AddParentNavBar("Page " & nParamPage, OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cPageParam, nParamPage))
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
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cPageParam, nParamPage-1)) & " title=" & QS("View page " & nParamPage-1) & ">&lt;&nbsp;Prev</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	Else
		Response.Write "&lt;&nbsp;Prev&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	End If	
	Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cIndexParam, 1)) & " title=" & QS("View the brochure index") & ">Index</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
	If nParamPage < brocXtrnPageQty Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPgmCdeParam, sParamPgmCde) & AddParam("&", cPageParam, nParamPage+1)) & " title=" & QS("View page " & nParamPage+1) & ">Next&nbsp;&gt;</a>"
	Else
		Response.Write "Next&nbsp;&gt;"
	End If	
	Response.Write "</td></tr>"
End Function


' ---------- Support routines

Function ConstructPrizeMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' sub-menu
	For i = 1 To nPrizes
		Call AddSubMenuItem(Prizes(i, cxPrzpMenuTxt), OurASPPage & AddParam("?", cPgmCdeParam, Prizes(i, cxPrzpPgmCde)), "View " & Prizes(i, cxPrzpMenuTxt) & " programs", PrizeIncentiveMenuSection)
	Next	
	If nPrizes = 0 Then Call AddSubMenuItem("", "", "", PrizeIncentiveMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
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


' === Load Site Prize List

Function LoadSitePrizeList()
	Dim RS, SQLStmt

	On Error Resume Next
	Call OpenEZMainDB()
	
	SQLStmt = "SITE_GetProgramList @PgmGrpCde='PRIZE'"
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	
	nPrizes = 0
	Do While CheckRS(RS)
		nPrizes = nPrizes + 1
		If nPrizes > cMaxPrizes Then nPrizes = cMaxPrizes: Exit Do

		Prizes(nPrizes, cxPrzpPgmCde) = nvs(RS.Fields("PGM_CDE"))
		Prizes(nPrizes, cxPrzpMenuTxt) = nvs(RS.Fields("MENU_TXT"))
		Prizes(nPrizes, cxPrzpImagePrfxNme) = nvs(RS.Fields("IMAGE_PRFX_NME"))
		Prizes(nPrizes, cxPrzpImageExtNme) = nvs(RS.Fields("IMAGE_EXT_NME"))
		If Prizes(nPrizes, cxPrzpImagePrfxNme) = "" Then
			Prizes(nPrizes, cxPrzpImagePrfxNme) = PhotoUnavailableImage
		End If
		Prizes(nPrizes, cxPrzpImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT"))
		Prizes(nPrizes, cxPrzpShrtFeatTxt) = nvs(RS.Fields("SHRT_FEAT_TXT"))
		Prizes(nPrizes, cxPrzpPortraitPageOrientFlg) = nvn(RS.Fields("PAGE_ORIENT_PORT_FLG"))
		Prizes(nPrizes, cxPrzpFeatPgmDescTxt) = nvs(RS.Fields("FEAT_PGM_DESC_TXT"))
		
		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing
	
	LoadSitePrizeList = (Err.number = 0)
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

%>