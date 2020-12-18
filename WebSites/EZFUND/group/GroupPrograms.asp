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
	Dim OurASPPage:		OurASPPage = GroupProgramASP
	Dim sPageHeader:	sPageHeader = "GROUPS"
	
	' Our default page title
	HTMLTitle = "EZFund.com - Fundraising Ideas to maximize profits for your group!"

	' flag - decide whether to reposition user after clicking a tabindex link
	Const bTabIndexRepositionEnabled = True	' True=enable, False=disable

	Const cMostPopularOrgTypeID = 999	' defines a pseudo-group
	
	Const cMaxOrgTypes = 20		' pick a number; we should not reach this number
	Dim OrgTypes(20,6)
		Const cxGrpOrgTypeID = 1
		Const cxGrpOrgTypeTxt = 2
		Const cxGrpMenuTxt = 3
		Const cxGrpImageNme = 4
		Const cxGrpImageDescTxt = 5
		Const cxGrpShrtFeatTxt = 6
	Dim nOrgTypes

	Dim FRFundraisingIdeas(3,20,9)		' Tab-Index, #ideas, attributes
		Const cxIdeaGrpCde = 1			' FOOD-PGM, BROCHURE, DIRECT, CUSTOM, OTHER
			Const cFoodIdeaGrpCde = "FOOD-PGM"
			Const cBrochureIdeaGrpCde = "BROCHURE"
			Const cDirectIdeaGrpCde = "DIRECT"
			Const cCustomIdeaGrpCde = "CUSTOM"
			Const cOtherIdeaGrpCde = "OTHER"
			Const cPrizeIdeaGrpCde = "PRIZE"
		Const cxIdeaPgmCde = 2			' program code (FOOD-PGM, BROCHURE)
		Const cxIdeaPdctCde = 3			' product code (DIRECT, CUSTOM, OTHER)
		Const cxIdeaPdctTxt = 4			' product text
		Const cxIdeaPrftTxt = 5			' profit text
		Const cxIdeaDescTxt = 6			' description text
		Const cxIdeaImagePrfxNme = 7	' image (prefix) name
		Const cxIdeaImageExtNme = 8		' image (extension) name
		Const cxIdeaImageDescTxt = 9	' image description text
	' NB: Primary + Other must NOT exceed 20
	Const cMaxFRPrimaryProductIdeas = 3
	Const cMaxFROtherProductIdeas = 17
	Const cMaxFRPrizeIdeas = 20			' pick a number; we should not reach this number
	
	Dim nFRPrimaryProductIdeas: nFRPrimaryProductIdeas = 0
	Dim nFROtherProductIdeas: nFROtherProductIdeas = 0
	Dim nFRPrizeIdeas: nFRPrizeIdeas = 0

	' Tab-Index
	Const cFRPrimaryProductTabIndex = 1
	Const cFROtherProductTabIndex = 2
	Const cFRPrizeTabIndex = 3

	' GROUP TYPE image display params
	'	-- Main GROUP page
	Const cMainGroupsPerRow = 2					' # group images per row
	Const cMainGroupImageWidth = 225
	Const cMainGroupImageHeight = 150
	Const cMainGroupSpacerWidth = 50			' space between group images on main GROUP page
	'	-- Specific GROUP page
	Const cSpecificGroupImageColWidth = 200		' defined space for image to float
	Const cSpecificGroupImageWidth = 150
	Const cSpecificGroupImageHeight = 111
	Const cSpecificGroupSpacerWidth = 15		' space between image and description

	' -- SITE variables for ORG TYPE
	Dim otOrgTypeTxt		' organization type text
	Dim otMenuTxt			' text to display on the menu
	Dim otImageNme			' image name (may include pathname; assumes current directory)
	Dim otImageDescTxt		' image description text (to display under image)
	Dim otShrtFeatTxt		' short feature text (to display under name)
	Dim otDescTxt			' description text (to display next to image)
	Dim otFeatTxt			' feature text (to display at bottom of page - under image)
	Dim otXtrnStrtDte		' start date to display on the site
	Dim otXtrnEndDte		' end date to display on the site

	' Note: This implementation is slightly different than the DIRECT SALES page
	Const cMaxTabIndexes = 4	' this many will fit on a single tab index row
	Dim TabIndexes(4,2)
		Const cxTabLabelTxt = 1
		Const cxTabDescTxt = 2
	Dim nTabIndexes

	' anchor text for TAB index - position user back on the index control when clicking tabs
	Const cAllProductTabAnchorText = "ALLProducts"
	Const cProductDataTabAnchorText = "ProductData"

	' NAV: define parent and main reports page
	Call InitParentNavBar(HomePageMenuSection, HomePageASP)
	Call AddParentNavBar(GroupProgramMenuSection, OurASPPage)

	Call ExtractRequestParams()
	
	If otOrgTypeTxt <> "" Then 
		HTMLTitle = "EZFund.com - " & otOrgTypeTxt & " fundraising ideas that will maximize your group profits!"
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

		Case (sParamOrgTypeID <> "")
				Call EmitProgramsForOrgType()

		Case Else
				Call EmitMainOrgTypePage()

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
	sParamOrgTypeID = "":	nParamOrgTypeID = 0
	sParamPgmCde = ""
	sParamPdctGrpCde = ""
	sParamPdctType = ""
	sParamTabIndex = "": nParamTabIndex = 0
End Function

Function ExtractRequestParams()		' Extract all params for this request

	Call ClearRequestParams()

	' Load control params for the PARENT page 
	' (use these params as default; CHILD pages can override)
	' NB: this must be performed early in the initialize process (allow CHILD override downstream)
	Call LoadControlParamsForPage(cGroupProgramPageCde)

	sParamOrgTypeID = nvs(Request.QueryString(cOrgTypeIDParam))	' OrgType ID
	nParamOrgTypeID = nvn(sParamOrgTypeID)
	sParamTabIndex = nvs(Request.QueryString(cTabIndexParam))	' tab index
	nParamTabIndex = nvn(sParamTabIndex)

	' these values will come from the database
	If nParamOrgTypeID > 0 Then
		Call GetSiteOrgTypeSpecifics(nParamOrgTypeID)
	End If	

	Call LoadOrgTypeList()
	Call ConstructOrgTypeMenu()
	Call ConstructMessageBoard(cGroupProgramPageCde)

End Function


' -------------------- View MAIN Group page

Function EmitMainOrgTypePage()
	Dim i, nCols

	Dim nOrgTypeTableCols: nOrgTypeTableCols = (cMainGroupsPerRow * 2) - 1	' account for spacer columns

	Call EmitParentNavBar()

	Call EmitMainPageHeader()	' MAIN PAGE header
	
	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"
	' display the available organization types (defined in the database)	
	i = 0
	Do While (i < nOrgTypes)
		' new Product row
		Response.Write "<tr>"
		For nCols = 1 To cMainGroupsPerRow
			' display (x) Groups per row
			i = i + 1
			If i <= nOrgTypes Then
				Call EmitOneOrgTypeTableEntry(OrgTypes(i, cxGrpOrgTypeID), OrgTypes(i, cxGrpOrgTypeTxt), OrgTypes(i, cxGrpImageNme), OrgTypes(i, cxGrpImageDescTxt), OrgTypes(i, cxGrpShrtFeatTxt))
			Else
				Call EmitOneOrgTypeTableEntry("", "", "", "", "")
			End If
			If nCols < cMainGroupsPerRow Then Response.Write "<td width=" & cMainGroupSpacerWidth & ">&nbsp;</td>"	' column spacer
		Next
		Response.Write "</tr>"
		Call EmitBlankTableRow(nOrgTypeTableCols)
	Loop
	Response.Write "</table>"
	
	Response.Write "<br>"
	Response.Write "<div align=center class=ContentData><font color=blue><i>Can't find your group in our list?</i></font>"
	Response.Write "&nbsp; Check out our most <a href=" & QS(OurASPPage & AddParam("?", cOrgTypeIDParam, cMostPopularOrgTypeID)) & " title=" & QS("View our most popular fundraising ideas.") & ">popular fundraising products</a>.</div>"

	' display request for information links
	Response.Write "<br><br>"
	Call EmitSellingKitProductInfoLinks(cFoodPdctGrpCde, "Groups", "Group", "", True, True)

	Call EmitMainPageFooter()	' MAIN PAGE footer

End Function


' ----- MAIN PAGE support functions

Function EmitMainPageHeader()
	' This section displays at the top of the MAIN page
%>
<!--#include virtual="includes/GroupProgramsMainPageHeaderInclude.asp"-->
<%
End Function

Function EmitMainPageFooter()
	' This section displays at the bottom of the MAIN page
%>
<!--#include virtual="includes/GroupProgramsMainPageFooterInclude.asp"-->
<%
End Function

Function EmitNoActiveProgramMessage()
	' NB: This message displays when there are NO active programs to be displayed
%>
<!--#include virtual="includes/GroupProgramsNoActiveProgramInclude.asp"-->
<%
End Function

Function EmitOneOrgTypeTableEntry(ByVal nOrgTypeID, ByVal sOrgTypeTxt, ByVal sOrgTypeImage, ByVal sImageDescTxt, ByVal sPdctFeatTxt)
	Response.Write "<td width=" & cMainGroupImageWidth & " align=center valign=top>"
	If nvn(nOrgTypeID) <> 0 Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cOrgTypeIDParam, nOrgTypeID)) & " title=" & QS(StripHTMLTags("View " & sOrgTypeTxt & " fundraising ideas.")) & ">"
		Response.Write "<div class=ContentData>" & sOrgTypeTxt & " fundraiser" & "</div>"
		Response.Write "</a>"
	Else
		Response.Write "&nbsp;"	
	End If
	Response.Write "</td>"
End Function


' ----- SELLING KIT, REQUEST FOR INFORMATION, ORDER FORM

Function EmitSellingKitProductInfoLinks(ByVal sPdctGrpCde, ByVal sPdctType, ByVal sPdctNme, ByVal sPgmCde, ByVal bShowSkitLink, ByVal bShowPdctInfoLink)
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
		Response.Write "<br><a href=" & QS(SellingKitRequestASP & AddParam("?", cPdctGrpCdeParam, sPdctGrpCde) & InclIf(sPgmCde<>"", AddParam("&", cPgmCdeParam, sPgmCde), "")) & " title=" & QS("Sign-up to run a fundraiser and receive FREE selling kits for your entire group.") & "><img src=" & QS(SellingKitRequestImage) & " alt=" & QS("FREE selling kits for group") & " border=0></a>"
		Response.Write "</div>"
	End If
End Function

Function EmitBlankTableRow(ByVal nCols)
	Response.Write "<tr><td colspan=" & nCols & ">&nbsp;</td></tr>"
End Function


' -------------------- View an individual ORG TYPE

Function EmitProgramsForOrgType()

	Call AddParentNavBar(otOrgTypeTxt, OurASPPage & AddParam("?", cOrgTypeIDParam, nParamOrgTypeID))
	Call EmitParentNavBar()

	Response.Write "<div>"
	If otImageNme <> "" Then
		' not all pages will have an image
		Response.Write "<img src=" & QS(otImageNme) & " alt=" & QS(StripHTMLTags(otOrgTypeTxt & " fundraising group")) & " class=GroupPageImage>"	' float image
	End If	
'''	Response.Write "<span align=left class=SectionHeader><p>" & otOrgTypeTxt & " fundraiser" & "</span>"
''' 1/31/08 - SEO: replaced above line with the following
	Response.Write "<H1>" & otOrgTypeTxt & " fundraiser" & "</H1>"
	Response.Write "<span align=left class=ContentData><p>" & otDescTxt & "<p></span>"
	Response.Write "</div>"

	Response.Write "<p style=""clear: both"">"		' clear image float for text downstream

	' the main event - display all fundraising ideas
	Call EmitFundraisingIdeasData(nParamTabIndex)

	' display request for information links
	Response.Write "<br>"
	Call EmitSellingKitProductInfoLinks(cFoodPdctGrpCde, "Groups", otOrgTypeTxt, sParamPgmCde, True, True)

End Function

Function EmitFundraisingIdeasData(ByVal nIndex)
	If nTabIndexes = 0 Then Exit Function	' no tab data loaded
	
	If nIndex < 0 Then
		Call EmitAllFundraisingIdeasTabData()
	Else
		Call EmitFundraisingIdeasTabIndex(nIndex)
	End If
End Function

Function EmitFundraisingIdeasTabIndex(ByVal nIndex)
	Dim i, j, sURL
	Dim nSelectedIndex: nSelectedIndex = 0
	Dim sSpacerRow: sSpacerRow = ""

	If nTabIndexes = 0 Then Exit Function

	If bTabIndexRepositionEnabled = True Then Response.Write "<a name=" & QS(cProductDataTabAnchorText) & "></a>"	
	sURL = OurASPPage & AddParam("?", cOrgTypeIDParam, nParamOrgTypeID) & AddParam("&", cTabIndexParam, -1) & InclIf(bTabIndexRepositionEnabled = True, "#" & cAllProductTabAnchorText, "")
	Response.Write "<div align=right class=SmallContentData>"
	Response.Write "<a href=" & QS(sURL) & " title=" & QS("View all fundraising ideas on a single page.") & NoFollowLinkAttribute & ">" & "View all fundraising ideas" & "</a>"
	Response.Write "</div>"

	Response.Write "<table cellpadding=0 cellspacing=0 border=0 width='100%'>"
	Response.Write "<tr>"
	For i=1 To nTabIndexes

		If ((nIndex <= 0 Or nIndex > nTabIndexes) And i=1) Or (i = nIndex) Then
			Response.Write "<td class=TabIndexSelect width='25%' align=center>" & TabIndexes(i, cxTabLabelTxt) & "</td>"
			nSelectedIndex = i
		Else
			sURL = OurASPPage & AddParam("?", cOrgTypeIDParam, nParamOrgTypeID) & AddParam("&", cTabIndexParam, i) & InclIf(bTabIndexRepositionEnabled = True, "#" & cProductDataTabAnchorText, "")
			Response.Write "<td class=TabIndex width='25%' align=center>" & "<a href=" & QS(sURL) & " title=" & QS(StripHTMLTags(otOrgTypeTxt & " fundraiser - " & TabIndexes(i, cxTabLabelTxt))) & InclIf(i<>1, NoFollowLinkAttribute, "") & ">" & TabIndexes(i, cxTabLabelTxt) & "</a>" & "</td>"
		End If

	Next
	For j=i To cMaxTabIndexes
		' fill remaining empty tab positions
		Response.Write "<td class=TabIndexFill width='25%'>&nbsp;</td>"
	Next
	Response.Write "</tr>"

	If nSelectedIndex > 0 Then
		Response.Write "<tr><td class=TabBox colspan=4>"
		Response.Write TabIndexes(nSelectedIndex, cxTabDescTxt)
		Response.Write EmitFundraisingIdeasCategory(nSelectedIndex, 20)
		Response.Write "</td></tr>"
	End If	

	Response.Write "</table>"
End Function

Function EmitAllFundraisingIdeasTabData()
	Dim i, sURL

	If nTabIndexes = 0 Then Exit Function
	
	If bTabIndexRepositionEnabled = True Then Response.Write "<a name=" & QS(cAllProductTabAnchorText) & "></a>"	
	sURL = OurASPPage & AddParam("?", cOrgTypeIDParam, nParamOrgTypeID) & AddParam("&", cTabIndexParam, 1) & InclIf(bTabIndexRepositionEnabled = True, "#" & cProductDataTabAnchorText, "")
	Response.Write "<div align=right class=SmallContentData>"
	Response.Write "<a href=" & QS(sURL) & " title=" & QS("View fundraising ideas by sections.") & ">" & "View individual fundraising ideas" & "</a>"
	Response.Write "</div>"

	For i=1 To nTabIndexes
		Response.Write "<hr>"
		Response.Write "<div class=SectionHeader>" & TabIndexes(i, cxTabLabelTxt) & "</div>"
		Response.Write "<br>"
		Response.Write TabIndexes(i, cxTabDescTxt)
		Response.Write EmitFundraisingIdeasCategory(i, 20)
		Response.Write "<p>"
	Next
End Function

Function EmitFundraisingIdeasCategory(ByVal nIdeaCategoryID, ByVal nMaxIdeas)
	Dim i
	Dim sThumbImageNme

	Const cIdeasCategoryCols = 3

	Response.Write "<table width=""100%"" border=0 cellpadding=0 cellspacing=0 align=center>"
	For i = 1 To nMaxIdeas
		' display (max) fundraising ideas for this group

		If FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaGrpCde) = "" Then Exit For

		' separator line between ideas
		If i > 1 Then Response.Write "<tr><td colspan=" & cIdeasCategoryCols & "><p><hr class=TabHR><p></td></tr>"

		Call EmitBlankTableRow(cIdeasCategoryCols)
		Call EmitBlankTableRow(cIdeasCategoryCols)
		' display image
		Response.Write "<tr>"
		Response.Write "<td align=center valign=top class=ContentData width=" & cSpecificGroupImageColWidth & ">"
		Select Case FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaGrpCde)
			Case cBrochureIdeaGrpCde, cFoodIdeaGrpCde, cPrizeIdeaGrpCde:
					sThumbImageNme = ProductImageFileName("TN", FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaImagePrfxNme), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaImageExtNme), 1)
			Case cDirectIdeaGrpCde, cCustomIdeaGrpCde:
					sThumbImageNme = ProductImageFileName("TN", FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaImagePrfxNme), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaImageExtNme), 0)
			Case Else:
					sThumbImageNme = ProductImageFileName("", FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaImagePrfxNme), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaImageExtNme), 0)
		End Select
		Response.Write SpecificProgramURL(FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaGrpCde), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPgmCde), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPdctCde), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPdctTxt), "<img src=" & sThumbImageNme & " alt=" & QS(StripHTMLTags(FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPdctTxt))) & " border=0>")
		Response.Write "<br>"
		Response.Write SpecificProgramURL(FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaGrpCde), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPgmCde), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPdctCde), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPdctTxt), "View details")
		Response.Write "</td>"
		Response.Write "<td width=" & cSpecificGroupSpacerWidth & ">&nbsp;</td>"
		' display text describing this program
		Response.Write "<td align=left valign=top class=ContentData>" 
		Response.Write "<div align=left class=ContentHeader>" & HTBold(SpecificProgramURL(FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaGrpCde), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPgmCde), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPdctCde), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPdctTxt), FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaImageDescTxt))) & "</div>"
		If nvs(FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPrftTxt)) <> "" Then
			Response.Write "<div align=right class=ContentWarning>" & HTBold(FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaPrftTxt) & " Profit") & "</div>"
		Else	
			Response.Write "<div align=right class=ContentWarning>&nbsp;</div>"
		End If
		Response.Write "<br>" & FRFundraisingIdeas(nIdeaCategoryID, i, cxIdeaDescTxt)
		Response.Write "</td>"
		Response.Write "</tr>"
		Call EmitBlankTableRow(cIdeasCategoryCols)
		Call EmitBlankTableRow(cIdeasCategoryCols)
	Next	
	Response.Write "</table>"

End Function


Function SpecificProgramURL(ByVal sTypeCde, ByVal sPgmCde, ByVal sPdctCde, ByVal sPdctOrPgmTxt, ByVal sDisplayTxt)
	Dim sURL
	Select Case sTypeCde
		Case cFoodIdeaGrpCde
				sURL = "<a href=" & QS(FoodProgramASP & AddParam("?", cPdctCdeParam, "DOUGH") & AddParam("&", cPgmCdeParam, sPgmCde)) & " title=" & QS(StripHTMLTags("View complete details on this " & sPdctOrPgmTxt & " fundraiser.")) & ">" & sDisplayTxt & "</a>"
		Case cBrochureIdeaGrpCde
				sURL = "<a href=" & QS(BrochureProgramASP & AddParam("?", cPgmCdeParam, sPgmCde)) & " title=" & QS(StripHTMLTags("View complete details on this " & sPdctOrPgmTxt & " brochure program.")) & ">" & sDisplayTxt & "</a>"
		Case cPrizeIdeaGrpCde
				sURL = "<a href=" & QS(PrizeIncentiveASP & AddParam("?", cPgmCdeParam, sPgmCde)) & " title=" & QS(StripHTMLTags("View complete details on this " & sPdctOrPgmTxt & " prize program.")) & ">" & sDisplayTxt & "</a>"
		Case cCustomIdeaGrpCde
				sURL = "<a href=" & QS(DirectSellerASP & AddParam("?", "PdctGrpCde", "CUSTOM") & AddParam("&", "PdctCtgyCde", "CUSTOM") & AddParam("&", cPdctCdeParam, sPdctCde)) & " title=" & QS(StripHTMLTags("View complete details on this " & sPdctOrPgmTxt & " fundraiser.")) & ">" & sDisplayTxt & "</a>"
		Case Else
				sURL = "<a href=" & QS(DirectSellerASP & AddParam("?", cPdctCdeParam, sPdctCde)) & " title=" & QS(StripHTMLTags("View complete details on this " & sPdctOrPgmTxt & " fundraiser.")) & ">" & sDisplayTxt & "</a>"
	End Select
	SpecificProgramURL = sURL
End Function


' ---------- Support routines

Function ConstructOrgTypeMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' construct sub-menu
	For i = 1 To nOrgTypes
		Call AddSubMenuItem(OrgTypes(i, cxGrpMenuTxt), OurASPPage & AddParam("?", cOrgTypeIDParam, OrgTypes(i, cxGrpOrgTypeID)), "View " & OrgTypes(i, cxGrpMenuTxt) & " fundraising ideas.", GroupProgramMenuSection)
	Next
	If nOrgTypes = 0 Then 	Call AddSubMenuItem("", "", "", GroupProgramMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
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

' ------------------------------ ORG TYPE routines

' === Load Site Organization Type List

Function LoadOrgTypeList()
	Dim RS, SQLStmt

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetOrgTypeList"
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	Do While CheckRS(RS)
		nOrgTypes = nOrgTypes + 1
		OrgTypes(nOrgTypes, cxGrpOrgTypeID) = nvn(RS.Fields("ORG_TYPE_ID"))
		OrgTypes(nOrgTypes, cxGrpOrgTypeTxt) = nvs(RS.Fields("ORG_TYPE_TXT"))
		OrgTypes(nOrgTypes, cxGrpMenuTxt) = nvs(RS.Fields("MENU_TXT"))

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	LoadOrgTypeList = (Err.number = 0)
End Function

' === Site Org Type Specifics - info gotten from SITE_OrgTypeSpecifics sp()

Function GetSiteOrgTypeSpecifics(ByVal nOrgTypeID)
	Dim RS, SQLStmt
	Dim sMetaKywdTxt, sMetaDescTxt, sHTMLTitlTxt

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetOrgTypeSpecifics @OrgTypeID=" & nOrgTypeID
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	If CheckRS(RS) Then
		otOrgTypeTxt = nvs(RS.Fields("ORG_TYPE_TXT"))
		otMenuTxt = nvs(RS.Fields("MENU_TXT"))
		otImageNme = nvs(RS.Fields("IMAGE_NME"))	' NB: some pages will not have an image
		' Note: don't use the image placeholder (if not exists!)
		otImageDescTxt = nvs(RS.Fields("IMAGE_DESC_TXT"))
		otShrtFeatTxt = nvs(RS.Fields("SHRT_FEAT_TXT"))
		otDescTxt = nvs(RS.Fields("DESC_TXT"))
		otFeatTxt = nvs(RS.Fields("FEAT_TXT"))
		otXtrnStrtDte = nvd(RS.Fields("XTRN_STRT_DTE"))
		otXtrnEndDte = nvd(RS.Fields("XTRN_END_DTE"))

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

	' get recommended products and prizes for given OrgType
	Call LoadPrimaryProductListForOrgType(nOrgTypeID)	' new
	Call LoadOtherProductListForOrgType(nOrgTypeID)		' new
	Call LoadPrizeListForOrgType(nOrgTypeID)

	GetSiteOrgTypeSpecifics = (Err.number = 0)
End Function


' === Load Site Lists

Function LoadPrimaryProductListForOrgType(ByVal nOrgTypeID)
	Dim RS, SQLStmt

	On Error Resume Next
	Call OpenEZMainDB()

	' RETRIEVE (PRIMARY) product ideas for given OrgType

	SQLStmt = "SITE_GetFundraisingIdeasForOrgType @OrgTypeID=" & nOrgTypeID & ", @SrcGrp='PDCT'"
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	nFRPrimaryProductIdeas = 0
	Do While CheckRS(RS)

		nFRPrimaryProductIdeas = nFRPrimaryProductIdeas + 1
		If nFRPrimaryProductIdeas > cMaxFRPrimaryProductIdeas Then nFRPrimaryProductIdeas = cMaxFRPrimaryProductIdeas: Exit Do

		FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaGrpCde) = nvs(RS.Fields("GRP_CDE").Value)
		FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaPgmCde) = nvs(RS.Fields("PGM_CDE").Value)
		FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaPdctCde) = nvs(RS.Fields("PDCT_CDE").Value)
		FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaPdctTxt) = nvs(RS.Fields("FR_PDCT_TXT").Value)
		FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaPrftTxt) = nvs(RS.Fields("FR_PRFT_TXT").Value)
		FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaDescTxt) = nvs(RS.Fields("DESC_TXT").Value)
		FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT").Value)
		FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaImagePrfxNme) = nvs(RS.Fields("IMAGE_PRFX_NME").Value)
		FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaImageExtNme) = nvs(RS.Fields("IMAGE_EXT_NME").Value)
		If FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaImagePrfxNme) = "" Then
			FRFundraisingIdeas(cFRPrimaryProductTabIndex, nFRPrimaryProductIdeas, cxIdeaImagePrfxNme) = PhotoUnavailableImage
		End If

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	' Primary Product TAB-index	
	nTabIndexes = nTabIndexes + 1
	TabIndexes(cFRPrimaryProductTabIndex, cxTabLabelTxt) = "Top 3 Ideas"
	If nFRPrimaryProductIdeas > 0 Then
		TabIndexes(cFRPrimaryProductTabIndex, cxTabDescTxt) = ""
	Else
		TabIndexes(cFRPrimaryProductTabIndex, cxTabDescTxt) = "<p>&nbsp;<p>There are currently NO <b>Top 3</b> product recommendations available.<p>&nbsp;<p>"
	End If

	LoadPrimaryProductListForOrgType = (Err.number = 0)
End Function

Function LoadOtherProductListForOrgType(ByVal nOrgTypeID)
	Dim RS, SQLStmt
	Dim i: i = 0

	On Error Resume Next
	Call OpenEZMainDB()

	' RETRIEVE (OTHER) product ideas for given OrgType

	SQLStmt = "SITE_GetFundraisingIdeasForOrgType @OrgTypeID=" & nOrgTypeID & ", @SrcGrp='PDCT'"
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	nFROtherProductIdeas = 0
	Do While CheckRS(RS)

		' NB: the PDCT recordset contains ALL product ideas; 
		' the first (x) are PRIMARY ideas and the remainder are OTHER ideas
		
		i = i + 1
		
		If i > cMaxFRPrimaryProductIdeas Then
			' OTHER product ideas
			nFROtherProductIdeas = nFROtherProductIdeas + 1
			If nFROtherProductIdeas > cMaxFROtherProductIdeas Then nFROtherProductIdeas = cMaxFROtherProductIdeas: Exit Do

			FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaGrpCde) = nvs(RS.Fields("GRP_CDE").Value)
			FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaPgmCde) = nvs(RS.Fields("PGM_CDE").Value)
			FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaPdctCde) = nvs(RS.Fields("PDCT_CDE").Value)
			FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaPdctTxt) = nvs(RS.Fields("FR_PDCT_TXT").Value)
			FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaPrftTxt) = nvs(RS.Fields("FR_PRFT_TXT").Value)
			FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaDescTxt) = nvs(RS.Fields("DESC_TXT").Value)
			FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT").Value)
			FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaImagePrfxNme) = nvs(RS.Fields("IMAGE_PRFX_NME").Value)
			FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaImageExtNme) = nvs(RS.Fields("IMAGE_EXT_NME").Value)
			If FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaImagePrfxNme) = "" Then
				FRFundraisingIdeas(cFROtherProductTabIndex, nFROtherProductIdeas, cxIdeaImagePrfxNme) = PhotoUnavailableImage
			End If
		' else we're still walking the PRIMARY ideas
		End If
		
		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	' Other Product TAB-index	
	nTabIndexes = nTabIndexes + 1
	TabIndexes(cFROtherProductTabIndex, cxTabLabelTxt) = "Other Ideas"
	If nFROtherProductIdeas > 0 Then
		TabIndexes(cFROtherProductTabIndex, cxTabDescTxt) = "<br><div align=left class=ContentData>Check out these other " & HTBold(otOrgTypeTxt) & " fundraising ideas that are tailored to your size group.</div><p>"
	Else
		TabIndexes(cFROtherProductTabIndex, cxTabDescTxt) = "<p>&nbsp;<p>There are currently NO <b>Other</b> product recommendations available.<p>&nbsp;<p>"
	End If

	LoadOtherProductListForOrgType = (Err.number = 0)
End Function


Function LoadPrizeListForOrgType(ByVal nOrgTypeID)
	Dim RS, SQLStmt

	On Error Resume Next
	Call OpenEZMainDB()

	' RETRIEVE prize ideas for given OrgType

	SQLStmt = "SITE_GetFundraisingIdeasForOrgType @OrgTypeID=" & nOrgTypeID & ", @SrcGrp='PRZP'"
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)

	nFRPrizeIdeas = 0
	Do While CheckRS(RS)

		nFRPrizeIdeas = nFRPrizeIdeas + 1
		If nFRPrizeIdeas > cMaxFRPrizeIdeas Then nFRPrizeIdeas = cMaxFRPrizeIdeas: Exit Do

		FRFundraisingIdeas(cFRPrizeTabIndex, nFRPrizeIdeas, cxIdeaGrpCde) = nvs(RS.Fields("GRP_CDE").Value)
		FRFundraisingIdeas(cFRPrizeTabIndex, nFRPrizeIdeas, cxIdeaPgmCde) = nvs(RS.Fields("PGM_CDE").Value)
		FRFundraisingIdeas(cFRPrizeTabIndex, nFRPrizeIdeas, cxIdeaPdctCde) = nvs(RS.Fields("PDCT_CDE").Value)
		FRFundraisingIdeas(cFRPrizeTabIndex, nFRPrizeIdeas, cxIdeaPdctTxt) = nvs(RS.Fields("FR_PDCT_TXT").Value)
		FRFundraisingIdeas(cFRPrizeTabIndex, nFRPrizeIdeas, cxIdeaPrftTxt) = nvs(RS.Fields("FR_PRFT_TXT").Value)
		FRFundraisingIdeas(cFRPrizeTabIndex, nFRPrizeIdeas, cxIdeaDescTxt) = nvs(RS.Fields("DESC_TXT").Value)
		FRFundraisingIdeas(cFRPrizeTabIndex, nFRPrizeIdeas, cxIdeaImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT").Value)
		FRFundraisingIdeas(cFRPrizeTabIndex, nFRPrizeIdeas, cxIdeaImagePrfxNme) = nvs(RS.Fields("IMAGE_PRFX_NME").Value)
		FRFundraisingIdeas(cFRPrizeTabIndex, nFRPrizeIdeas, cxIdeaImageExtNme) = nvs(RS.Fields("IMAGE_EXT_NME").Value)

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing

	' Prize TAB-index
	nTabIndexes = nTabIndexes + 1
	TabIndexes(cFRPrizeTabIndex, cxTabLabelTxt) = "Prizes"
	If nFRPrizeIdeas > 0 Then
		TabIndexes(cFRPrizeTabIndex, cxTabDescTxt) = _
				"<br><div align=left class=ContentData>Consider one of these prize incentive programs to help boost sales for your " & HTBold(otOrgTypeTxt) & " fundraiser.&nbsp; " & _
				"Prize incentives are proven to increase group sales and they also provide a great way to reward your sellers for a super job!</div><p>"
	Else
		TabIndexes(cFRPrizeTabIndex, cxTabDescTxt) = "<p>&nbsp;<p>There are currently NO <b>prize</b> recommendations available.<p>&nbsp;<p>"
	End If

	LoadPrizeListForOrgType = (Err.number = 0)
End Function


%>