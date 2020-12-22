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
	Dim OurASPPage:		OurASPPage = DirectSellerASP
	Dim sPageHeader:	sPageHeader = "DIRECT SELLERS"
	
	Dim sPdctGrpCdeAddParamString
	
	' Our default page title
	HTMLTitle = "EZFund.com - Direct Sale Fundraising products offer BIG profits!"

	' flag - decide whether to reposition user after clicking a tabindex link
	Const bTabIndexRepositionEnabled = True	' True=enable, False=disable

	' PAGE PRODUCT image display params
	'	-- Main PRODUCT page
	Const cMainPageProductsPerRow = 3				' # product images per row
	Const cMainPageProductImageWidth = 150
	Const cMainPageProductImageHeight = 150
	Const cMainPageProductSpacerWidth = 50			' space between product images on main FROZEN PRODUCTS page
	'	-- Specific PRODUCT page
	Const cSpecificProductImageWidth = 150
	Const cSpecificProductImageHeight = 150
	Const cSpecificProductSpacerWidth = 15			' space between image and description
	'	-- PRODUCT list
	Const cProductListProductsPerRow = 5			' # product images per row
	Const cProductListProductImageWidth = 75
	Const cProductListProductImageHeight = 75
	Const cProductListProductSpacerWidth = 35		' space between images

	' -- SITE variables for DIRECT SELLERS
	' -- Product
	Dim pdctPdctNme			' product name
	Dim pdctMenuTxt			' text to display on the menu
	Dim pdctImagePrfxNme	' image (prefix) name (may include pathname; assumes current directory)
	Dim pdctImageExtNme		' image (extension) name
	Dim pdctImageDescTxt	' image description text (to display under product image)
	Dim pdctShrtFeatTxt		' short feature text (to display under product name)
	Dim pdctDescTxt			' description text (to display next to product image)
	Dim pdctFeatTxt			' feature text (to display at bottom of page - under product image)
	Dim pdctOrdrItemNbr		' order item number (ie. shopping cart ID)
	Dim pdctXtrnStrtDte		' start date to display on the site
	Dim pdctXtrnEndDte		' end date to display on the site

	' -- Product Category 
	Dim ctgyPdctNme			' product category text
	Dim ctgyMenuTxt			' text to display on the menu
	Dim ctgyImageNme		' image name (may include pathname; assumes current directory)
	Dim ctgyImageDescTxt	' image description text (to display under product image)
	Dim ctgyShrtFeatTxt		' short feature text (to display under product name)
	Dim ctgyDescTxt			' description text (to display next to product image)
	Dim ctgyFeatTxt			' feature text (to display at bottom of page - under product image)
	Dim ctgyXtrnStrtDte		' start date to display on the site
	Dim ctgyXtrnEndDte		' end date to display on the site

	Const cMaxProductCategories = 20	' pick a number; we should not reach this number
	Dim ProductCategories(20,5)
		Const cxCtgyPdctCtgyCde = 1		' product category
		Const cxCtgyMenuTxt = 2
		Const cxCtgyImageNme = 3
		Const cxCtgyImageDescTxt = 4
		Const cxCtgyShrtFeatTxt = 5
	Dim nProductCategories

	Const cMaxProducts = 40				' pick a number; we should not reach this number
	Dim Products(40,10)
		Const cxPdctPdctCde = 1			' product code
		Const cxPdctPdctNme = 2			' product name
		Const cxPdctMenuTxt = 3
		Const cxPdctPrftTxt = 4			' profit text
		Const cxPdctDescTxt = 5			' description text
		Const cxPdctImagePrfxNme = 6	' image prefix
		Const cxPdctImageExtNme = 7		' image extension (ie. jpg, gif, bmp, etc.)
		Const cxPdctImageDescTxt = 8
		Const cxPdctShrtFeatTxt = 9
		Const cxPdctOrdrItemNbr = 10	' shopping cart ID
	Dim nProducts

	Const cMaxTabIndexes = 4	' this many will fit on a single tab index row
	Dim TabIndexes(4,2)
		Const cxTabLabelTxt = 1
		Const cxTabDescTxt = 2
	Dim nTabIndexes

	' anchor text for TAB index - position user back on the index control when clicking tabs
	Const cAllProductTabAnchorText = "ALLProducts"
	Const cProductDataTabAnchorText = "ProductData"


	Call ExtractRequestParams()

	' NAV: define parent and main reports page
	Call InitParentNavBar(HomePageMenuSection, HomePageASP)
	Select Case sParamPdctGrpCde
		Case cCustomPdctGrpCde:
				' The Custom PdctCtgy will add our NavBar entry
				'Call AddParentNavBar(CustomProductMenuSection, CustomProductASP)
				HTMLTitle = "EZFund.com - Customized Fundraising products offer BIG profits!"
		Case cGoGreenPdctGrpCde:
				' The Go-Green PdctCtgy will add our NavBar entry
				Call AddParentNavBar(GoGreenMenuSection, GoGreenASP)
				HTMLTitle = "EZFund.com - Environment-friendly (Green) Fundraising Ideas"
		Case Else:		
				Call AddParentNavBar(DirectSellerMenuSection, OurASPPage)
				If ctgyPdctNme <> "" Then
					' we tailor an HTML title based on product
					If pdctPdctNme <> "" Then
						HTMLTitle = "EZFund.com - " & pdctPdctNme & " - " & ctgyPdctNme & " Fundraising products offer BIG profits!"
					Else
						HTMLTitle = "EZFund.com - " & ctgyPdctNme & " Fundraising products offer BIG profits!"
					End If	
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
	
		Case sParamPdctCde <> "":				Call EmitSpecificProduct()
		Case sParamPdctCtgyCde <> "":			Call EmitSpecificProductCategory()
		Case Else:								Call EmitMainProductPage()
				
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
	sParamTabIndex = "": nParamTabIndex = 0
End Function

Function ExtractRequestParams()		' Extract all params for this request

	Call ClearRequestParams()

	sParamPdctGrpCde = nvs(Request.QueryString(cPdctGrpCdeParam))	' product group
	If nvs(sParamPdctGrpCde) = "" Then sParamPdctGrpCde = cDirectPdctGrpCde	' default to DIRECT
	sParamPdctCtgyCde = nvs(Request.QueryString(cPdctCtgyCdeParam))	' product category
	sParamPdctCde = nvs(Request.QueryString(cPdctCdeParam))			' product code
	sParamPgmCde = nvs(Request.QueryString(cPgmCdeParam))			' program code
	sParamTabIndex = nvs(Request.QueryString(cTabIndexParam))		' tab index
	nParamTabIndex = nvn(sParamTabIndex)

	' Load control params for the PARENT page 
	' (use these params as default; CHILD pages can override)
	' NB: this must be performed early in the initialize process (allow CHILD override downstream)
	Select Case sParamPdctGrpCde
		Case cGoGreenPdctGrpCde:	Call LoadControlParamsForPage(cGoGreenPageCde)
		Case cCustomPdctGrpCde:		Call LoadControlParamsForPage(cCustomProductPageCde)
		Case Else:					Call LoadControlParamsForPage(cDirectSellerPageCde)
	End Select

	' these values will come from the database
	If sParamPdctCtgyCde <> "" Then
		Call GetSiteProductCategorySpecifics(sParamPdctCtgyCde)
	End If	
	
	If sParamPdctCde <> "" Then
		If GetSiteProductSpecifics(sParamPdctCde) = False Then
			Call ClearRequestParams()	' bad product, force user to main direct seller page
		End If	
	End If	

	Call LoadSitePageProductList(sParamPdctGrpCde)	' cDirectSellerPageCde
	Select Case sParamPdctGrpCde 
		Case cGoGreenPdctGrpCde:
			' NB: this string must be AFTER the first param (see "&" vs "?" indicator)
			sPdctGrpCdeAddParamString = AddParam("&", cPdctGrpCdeParam, cGoGreenPdctGrpCde)
			Call LoadSiteGoGreenProgramList(sParamPdctGrpCde)	' load special program array for menu
			Call ConstructGoGreenMenu()
			Call ConstructMessageBoard(cGoGreenPageCde)
		Case cCustomPdctGrpCde:
			' NB: this string must be AFTER the first param (see "&" vs "?" indicator)
			sPdctGrpCdeAddParamString = AddParam("&", cPdctGrpCdeParam, cCustomPdctGrpCde)
			Call ConstructCustomProductMenu()
			Call ConstructMessageBoard(cCustomProductPageCde)
		Case Else:	
			sPdctGrpCdeAddParamString = ""		' default is DIRECT page
			Call ConstructProductMenu()
			Call ConstructMessageBoard(cDirectSellerPageCde)
	End Select		

End Function


' -------------------- View MAIN Direct Sale products

Function EmitMainProductPage()
	Dim i, nCols

	Dim nProductTableCols: nProductTableCols = (cMainPageProductsPerRow * 2) - 1	' account for spacer columns

	Call EmitParentNavBar()

	If nProductCategories = 0 Then
		' no active products (or possible database issues!)
		Call EmitNoActiveProgramMessage(sParamPdctGrpCde)	' tell the user something
		Exit Function
	End If

	Call EmitMainPageHeader(sParamPdctGrpCde)	' MAIN PAGE header (NB: content controlled externally)
	
	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"

	' display the available direct seller products (defined in the database)	
	i = 0
	Do While (i < nProductCategories)
		' new Product row
		Response.Write "<tr>"
		For nCols = 1 To cMainPageProductsPerRow
			' display three Products per row
			i = i + 1
			If i <= nProductCategories Then
				Call EmitOneProductTableEntry(ProductCategories(i, cxCtgyPdctCtgyCde), ProductCategories(i, cxCtgyImageNme), ProductCategories(i, cxCtgyImageDescTxt), ProductCategories(i, cxCtgyShrtFeatTxt))
			Else
				Call EmitOneProductTableEntry("", "", "", "")
			End If
			If nCols < cMainPageProductsPerRow Then Response.Write "<td width=" & cMainPageProductSpacerWidth & ">&nbsp;</td>"	' column spacer
		Next
		Response.Write "</tr>"
		Response.Write "<tr><td colspan=" & nProductTableCols & " width=5>&nbsp;</td></tr>"	' row spacer
		Response.Write "<tr><td colspan=" & nProductTableCols & " width=5>&nbsp;</td></tr>"	' row spacer
	Loop

	' display request for information links
	Call EmitSellingKitProductInfoLinks(cDirectPdctGrpCde, cDirectPdctGrpCde, "Direct Seller", sParamPgmCde, 0, nProductTableCols, False, True, False)
	
	Response.Write "<tr><td colspan=" & nProductTableCols & " width=5>&nbsp;</td></tr>"	' row spacer
	Response.Write "</table>"

	Call EmitMainPageFooter(sParamPdctGrpCde)	' MAIN PAGE footer (NB: content controlled externally)

End Function


' ----- MAIN PAGE support functions

Function EmitMainPageHeader(ByVal sPdctGrpCde)
	' This section displays at the top of the MAIN page
	Select Case sPdctGrpCde
		Case cCustomPdctGrpCde:
%>
<!--#include virtual="includes/CustomProductMainPageHeaderInclude.asp"-->
<%
		Case Else:
%>
<!--#include virtual="includes/DirectSellerMainPageHeaderInclude.asp"-->
<%
	End Select
End Function

Function EmitMainPageFooter(ByVal sPdctGrpCde)
	' This section displays at the bottom of the MAIN page
	Select Case sPdctGrpCde
		Case cCustomPdctGrpCde:
%>
<!--#include virtual="includes/CustomProductMainPageFooterInclude.asp"-->
<%
		Case Else:
%>
<!--#include virtual="includes/DirectSellerMainPageFooterInclude.asp"-->
<%
	End Select
End Function

Function EmitNoActiveProgramMessage(ByVal sPdctGrpCde)
	' NB: This message displays when there are NO active programs to be displayed
	Select Case sPdctGrpCde
		Case cCustomPdctGrpCde:
%>
<!--#include virtual="includes/CustomProductNoActiveProgramInclude.asp"-->
<%
		Case Else:
%>
<!--#include virtual="includes/DirectSellerNoActiveProgramInclude.asp"-->
<%
	End Select
End Function

Function EmitOneProductTableEntry(ByVal sPdctCtgyCde, ByVal sPdctImage, ByVal sImageDescTxt, ByVal sPdctFeatTxt)
	Response.Write "<td width=" & cMainPageProductImageWidth & " align=center valign=top>"
	If sPdctCtgyCde <> "" Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sPdctCtgyCde) & sPdctGrpCdeAddParamString) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt & " programs")) & ">"
		Response.Write "<img border=0 src=" & sPdctImage & " width=" & cMainPageProductImageWidth & " height=" & cMainPageProductImageHeight & " alt=" & QS(StripHTMLTags(sImageDescTxt)) & "><br>"
		Response.Write "<div class=ContentHeader>" & sImageDescTxt & "</div></a>"
		Response.Write "<div class=ContentData>" & sPdctFeatTxt & "</div>"
	Else
		Response.Write "&nbsp;"	
	End If
	Response.Write "</td>"
End Function


' ----- SELLING KIT, REQUEST FOR INFORMATION, ORDER FORM

Function EmitSellingKitProductInfoLinks(sPdctGrpCde, sPdctType, sPdctNme, sPgmCde, nOrderItemNbr, nTableCols, bShowSkitLink, bShowPdctInfoLink, bShowOrderLink)
	If bShowPdctInfoLink = True Then
		' request for PRODUCT INFORMATION link
		Response.Write "<tr><td colspan=" & nTableCols & ">&nbsp;</td></tr>"
		Response.Write "<tr><td colspan=" & nTableCols & " align=center class=ContentData>"
		Response.Write "<br><a href=" & QS(ProductInfoRequestASP & AddParam("?", cPdctTypeParam, sPdctType)) & " title=" & QS(StripHTMLTags("Request FREE information for " & sPdctNme)) & "><img src=" & QS(ProductInfoRequestImage) & " alt=" & QS(StripHTMLTags("FREE information for " & sPdctNme)) & " border=0></a>"
		Response.Write "</td></tr>"
	End If	
	If bShowSkitLink = True Then
		' request for SELLING KIT link
		Response.Write "<tr><td colspan=" & nTableCols & ">&nbsp;</td></tr>"
		Response.Write "<tr><td colspan=" & nTableCols & " align=center class=ContentData>"
		Response.Write "<br><a href=" & QS(SellingKitRequestASP & AddParam("?", cPdctGrpCdeParam, sPdctGrpCde) & InclIf(sPgmCde<>"", AddParam("&", cPgmCdeParam, sPgmCde), "")) & " title=" & QS("Sign-up to run this fundraising program and receive FREE selling kits for your entire group.") & "><img src=" & QS(SellingKitRequestImage) & " alt=" & QS("FREE selling kit for groups") & " border=0></a>"
		Response.Write "</td></tr>"
	End If
	If bShowOrderLink = True And nvn(nOrderItemNbr) = 0 Then
		' Shopping Cart link - only display message for products we can't order online
		Response.Write "<tr><td colspan=" & nTableCols & ">&nbsp;</td></tr>"
		Response.Write "<tr><td colspan=" & nTableCols & " align=center class=ContentData>"
		If nvn(nOrderItemNbr) <> 0 Then
			Response.Write "<font size=+1 color=blue>Ready to <b>ORDER</b> your " & sPdctNme & "?</font>"
			Response.Write "<br><span style="" font-size: 16px;""><a href=" & QS(OnlineStoreOrderASP & nOrderItemNbr) & " title=" & QS("Order this direct sale item using our secure online shopping cart.") & "><span class=Red>Click here to <b>ORDER</b> online!</span></a></span>"
		Else
			Response.Write "<i>Online ordering is not available for this product.</i><br>"
			Response.Write "Please call <b>" & EZSalesPhone & "</b> or <b>" & EZSalesLocalPhone & "</b> to place your order TODAY."
		End If
		Response.Write "</td></tr>"
	End If	
End Function


' -------------------- View product information about a SPECIFIC product category

Function EmitSpecificProductCategory()

	Dim nProductDescriptionWidth: nProductDescriptionWidth = (ContentWindowWidth - (cSpecificProductImageWidth + cSpecificProductSpacerWidth))
	Dim nProductFeatureWidth: nProductFeatureWidth = ContentWindowWidth

	Const cSpecificProductCols = 3

	Call AddParentNavBar(ctgyPdctNme, OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & sPdctGrpCdeAddParamString)
	Call EmitParentNavBar()

	Response.Write "<br>"

	Response.Write "<table border=0 cellpadding=0 cellspacing=0>"
	Response.Write "<tr>"
	' display product image
	Response.Write "<td width=" & cSpecificProductImageWidth & " align=center valign=top><img src=" & ctgyImageNme & " width=" & cSpecificProductImageWidth & " height=" & cSpecificProductImageHeight & " alt=" & QS(StripHTMLTags(ctgyPdctNme)) & " border=0><br>"
	Response.Write "<br><div class=ContentData>" & ctgyShrtFeatTxt & "</div>"
	Response.Write "</td>"
	Response.Write "<td width=" & cSpecificProductSpacerWidth & ">&nbsp;</td>"
	' display text describing this product
	Response.Write "<td width=" & nProductDescriptionWidth & " valign=top class=ContentData>"
'''	Response.Write "<div class=SectionHeader>" & ctgyPdctNme & "</div>"
''' 2/8/08 - SEO: replaced above line with the following
	Response.Write "<H1>" & ctgyPdctNme & "</H1>"
	Response.Write ctgyDescTxt
	Response.Write "</td>"
	Response.Write "</tr>"
	Response.Write "<tr><td colspan=" & cSpecificProductCols & ">&nbsp;</td></tr>"

	' display feature text
	Response.Write "<tr><td width=" & nProductFeatureWidth & " colspan=" & cSpecificProductCols & " class=ContentData>"
	Response.Write ctgyFeatTxt
	Call EmitDirectSellerProductList()	' show the available products for this category
	Response.Write "</td></tr>"

	' display request for information links
	Call EmitSellingKitProductInfoLinks(cDirectPdctGrpCde, sParamPdctCtgyCde, ctgyPdctNme, sParamPgmCde, 0, cSpecificProductCols, False, True, False)

	Response.Write "<tr><td colspan=" & cSpecificProductCols & ">&nbsp;</td></tr>"
	Response.Write "</table>"

End Function

Function EmitDirectSellerProductList()
	Dim nIndex, sURL
	Dim i, nCols, sSmallThumbImageNme

	Dim nProductListTableCols: nProductListTableCols = (cProductListProductsPerRow * 2) - 1	' account for spacer columns

	If nProducts = 0 Then
		' this is highly unusual case, but we still need to code for it
		Response.Write "<br><div class=HiliteHeader><b>This product is currently not available!</b></div>"
		Exit Function
	End If

	Response.Write "<br>"
	Response.Write "<div class=HiliteHeader><b>Available " & UCase(ctgyPdctNme) & " products...</b></div>"

' REMOVE THIS! original display method
'	' emit our PRODUCT list
'	Response.Write "<ul>"
'	For nIndex = 1 To nProducts
'		sURL = "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPdctCdeParam, Products(nIndex, cxPdctPdctCde))) & "><i>" & Products(nIndex, cxPdctPdctNme) & "</i></a>"
'		Response.Write "<li>" & sURL & InclIf(nvs(Products(nIndex, cxPdctPrftTxt)) <> "", " <font color=red>(" & Products(nIndex, cxPdctPrftTxt) & " profit)</font>", "") & "<br><br>"
'	Next
'	Response.Write "</ul>"
	
	
	' emit our PRODUCT images
	Response.Write "<br><br>"
	Response.Write "<table border=0 cellpadding=0 cellspacing=0 align=center>"

	' display the available direct seller products for this category
	i = 0
	Do While (i < nProducts)
		' new Product row
		Response.Write "<tr>"
		For nCols = 1 To cProductListProductsPerRow
			' display three Products per row
			i = i + 1
			If i <= nProducts Then
				sSmallThumbImageNme = ProductImageFileName("ST", Products(i, cxPdctImagePrfxNme), Products(i, cxPdctImageExtNme), 0)
				Call EmitOneProductListTableEntry(sParamPdctCtgyCde, Products(i, cxPdctPdctCde), sSmallThumbImageNme, Products(i, cxPdctImageDescTxt), Products(i, cxPdctPrftTxt))
			Else
				Call EmitOneProductListTableEntry("", "", "", "", "")
			End If
			If nCols < cProductListProductsPerRow Then Response.Write "<td width=" & cProductListProductSpacerWidth & ">&nbsp;</td>"	' column spacer
		Next
		Response.Write "</tr>"
		Response.Write "<tr><td colspan=" & nProductListTableCols & " width=5>&nbsp;<p></td></tr>"	' row spacer
	Loop
	
	Response.Write "<tr><td colspan=" & nProductListTableCols & " width=5>&nbsp;</td></tr>"	' row spacer
	Response.Write "</table>"
	
End Function

Function EmitOneProductListTableEntry(ByVal sPdctCtgyCde, ByVal sPdctCde, ByVal sPdctImage, ByVal sImageDescTxt, ByVal sPdctFeatTxt)
	Response.Write "<td width=" & cProductListProductImageWidth & " align=center valign=top>"
	If sPdctCtgyCde <> "" Then
		Response.Write "<a href=" & QS(OurASPPage & AddParam("?", cPdctCtgyCdeParam, sPdctCtgyCde) & AddParam("&", cPdctCdeParam, sPdctCde) & sPdctGrpCdeAddParamString) & " title=" & QS(StripHTMLTags("View " & sImageDescTxt & " products")) & ">"
		Response.Write "<img border=0 src=" & sPdctImage & " width=" & cProductListProductImageWidth & " height=" & cProductListProductImageHeight & " alt=" & QS(StripHTMLTags(sImageDescTxt)) & "><br>"
		Response.Write "<div class=SmallContentData>" & sImageDescTxt & "</div></a>"
		Response.Write "<div class=SmallContentData><font color=red>" & sPdctFeatTxt & "</font></div>"
	Else
		Response.Write "&nbsp;"	
	End If
	Response.Write "</td>"
End Function


' -------------------- View product information about a SPECIFIC product

Function EmitSpecificProduct()

'' REMOVE THIS! remove the image width restriction to allow greater freedom on the individual products
'' (see other '' commented code in this section)
''	Dim nProductDescriptionWidth: nProductDescriptionWidth = (ContentWindowWidth - (cSpecificProductImageWidth + cSpecificProductSpacerWidth))
	Dim nProductFeatureWidth: nProductFeatureWidth = ContentWindowWidth
	Dim sImageNme
	Dim sURL

	Const cSpecificProductCols = 3

	If sParamPdctGrpCde <> cGoGreenPdctGrpCde Then
'		Call AddParentNavBar(ctgyPdctNme, BrochureGoGreenASP & AddParam("?", cPgmGrpCdeParam, sParamPdctGrpCde))
'	Else
		Call AddParentNavBar(ctgyPdctNme, OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & sPdctGrpCdeAddParamString)
	End If	
	Call AddParentNavBar(pdctPdctNme, OurASPPage & AddParam("?", cPdctCdeParam, sParamPdctCde) & sPdctGrpCdeAddParamString)
	Call EmitParentNavBar()

	Response.Write "<br>"

	Response.Write "<table border=0 cellpadding=0 cellspacing=0>"
	Response.Write "<tr>"
	' display product image
	Response.Write "<td class=ContentData align=center valign=top><img src=" & ProductImageFileName("SM", pdctImagePrfxNme, pdctImageExtNme, 0) & " alt=" & QS(StripHTMLTags(pdctImageDescTxt)) & " border=0><br>"
	Response.Write pdctImageDescTxt & "<br>" & pdctShrtFeatTxt
	If nvn(pdctOrdrItemNbr) > 0 Then	' NB: -1 means multiple product IDs exist
		Response.Write "<br><br>"
		Response.Write "<a href=" & QS(OnlineStoreOrderASP & pdctOrdrItemNbr) & " title=" & QS("Order this item using our secure online shopping cart.") & ">"
		Response.Write "<img src=" & QS("/images/order.gif") & " alt=" & QS("Order using secure shopping cart") & " border=0></a>"
	End If
	
	' OrdrItemNbr = -1 means multiple shopping cart item#'s exist for this product
	If nvn(pdctOrdrItemNbr) < 0 Then
		' force expansion of all tabs so user can view individual shopping cart items
		sURL = OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPdctCdeParam, sParamPdctCde) & sPdctGrpCdeAddParamString & AddParam("&", cTabIndexParam, -1) & "#ALLProducts"
		Response.Write "<br><br>"
		Response.Write "<a href=" & QS(sURL) & " title=" & QS("View all product data on a single page.") & ">"
		Response.Write "<div class=ContentWarning><b>Order Online!</b></div>"
		Response.Write "</a>"
		Response.Write "<div class=SmallContentData>(see varieties)</div>"
	End If
	Response.Write "</td>"
	Response.Write "<td width=" & cSpecificProductSpacerWidth & ">&nbsp;</td>"
	' display text describing this product
''	Response.Write "<td width=" & nProductDescriptionWidth & " valign=top class=ContentData>"
	Response.Write "<td valign=top class=ContentData>"
'''	Response.Write "<div class=SectionHeader>" & pdctPdctNme & "</div>"
''' 2/8/08 - SEO: replaced above line with the following
	Response.Write "<H1>" & pdctPdctNme & "</H1>"
	Response.Write pdctDescTxt
	Response.Write "</td>"
	Response.Write "</tr>"
	Response.Write "<tr><td colspan=" & cSpecificProductCols & ">&nbsp;</td></tr>"
	' display feature text
	Response.Write "<tr><td width=" & nProductFeatureWidth & " colspan=" & cSpecificProductCols & " class=ContentData>"
	' -- feature text (from PDCT table)
	Response.Write pdctFeatTxt
	' -- feature data (from TAB_INDEX table)
	Call EmitProductFeatureData(nParamTabIndex)
	Response.Write "</td></tr>"

	' display request for information links
	Call EmitSellingKitProductInfoLinks(cDirectPdctGrpCde, sParamPdctCde, pdctPdctNme, sParamPgmCde, pdctOrdrItemNbr, cSpecificProductCols, False, True, True)

	Response.Write "<tr><td colspan=" & cSpecificProductCols & ">&nbsp;</td></tr>"
	Response.Write "</table>"

End Function

Function EmitProductFeatureData(nIndex)
	If nTabIndexes = 0 Then Exit Function	' no tab data loaded
	
	If nIndex < 0 Then
		Call EmitAllProductFeatureTabData()
	Else
		Call EmitProductFeatureTabIndex(nIndex)
	End If
End Function

Function EmitProductFeatureTabIndex(nIndex)
	Dim i, j, sURL
	Dim nSelectedIndex: nSelectedIndex = 0
	Dim sSpacerRow: sSpacerRow = ""

	If nTabIndexes = 0 Then Exit Function
	
	If bTabIndexRepositionEnabled = True Then Response.Write "<a name=" & QS(cProductDataTabAnchorText) & "></a>"
	Response.Write "<table cellpadding=0 cellspacing=0 border=0 width='100%'>"

	sURL = OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPdctCdeParam, sParamPdctCde) & sPdctGrpCdeAddParamString & AddParam("&", cTabIndexParam, -1) & InclIf(bTabIndexRepositionEnabled = True, "#" & cAllProductTabAnchorText, "")
	Response.Write "<tr><td colspan=4 align=right class=SmallContentData>"
	Response.Write "<a href=" & QS(sURL) & " title=" & QS("View all product data on a single page.") & NoFollowLinkAttribute & ">" & "View all product data" & "</a>"
	Response.Write "</td></tr>"
	
	Response.Write "<tr>"
	For i=1 To nTabIndexes
	
		If ((nIndex <= 0 Or nIndex > nTabIndexes) And i=1) Or (i = nIndex) Then
			Response.Write "<td class=TabIndexSelect width='25%' align=center>" & TabIndexes(i, cxTabLabelTxt) & "</td>"
			nSelectedIndex = i
		Else
			sURL = OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPdctCdeParam, sParamPdctCde) & sPdctGrpCdeAddParamString & AddParam("&", cTabIndexParam, i) & InclIf(bTabIndexRepositionEnabled = True, "#" & cProductDataTabAnchorText, "")
			Response.Write "<td class=TabIndex width='25%' align=center>" & "<a href=" & QS(sURL) & " title=" & QS(StripHTMLTags(pdctPdctNme & " fundraiser - " & TabIndexes(i, cxTabLabelTxt))) & InclIf(i<>1, NoFollowLinkAttribute, "") & ">" & TabIndexes(i, cxTabLabelTxt) & "</a>" & "</td>"
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
		Response.Write "</td></tr>"
	End If	

	Response.Write "</table>"
End Function

Function EmitAllProductFeatureTabData()
	Dim i, sURL

	If nTabIndexes = 0 Then Exit Function

	If bTabIndexRepositionEnabled = True Then Response.Write "<a name=" & QS(cAllProductTabAnchorText) & "></a>"
	Response.Write "<table cellpadding=0 cellspacing=0 border=0 width='100%'>"
	sURL = OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPdctCdeParam, sParamPdctCde) & sPdctGrpCdeAddParamString & AddParam("&", cTabIndexParam, 1) & InclIf(bTabIndexRepositionEnabled = True, "#" & cProductDataTabAnchorText, "")
	Response.Write "<tr><td align=right class=SmallContentData>"
	Response.Write "<a href=" & QS(sURL) & " title=" & QS("View product data by sections.") & ">" & "View individual product data" & "</a>"
	Response.Write "</td></tr>"

	Response.Write "<tr><td class=ContentData>"
	For i=1 To nTabIndexes
		Response.Write "<hr>"
		Response.Write "<div class=SectionHeader>" & TabIndexes(i, cxTabLabelTxt) & "</div>"
		Response.Write "<br>"
		Response.Write TabIndexes(i, cxTabDescTxt)
		Response.Write "<p>"
	Next
	Response.Write "</td></tr>"

	Response.Write "</table>"
End Function


' ---------- Support routines

Function ConstructProductMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' construct sub-menu
	For i = 1 To nProductCategories
		Call AddSubMenuItem(ProductCategories(i, cxCtgyMenuTxt), OurASPPage & AddParam("?", cPdctCtgyCdeParam, ProductCategories(i, cxCtgyPdctCtgyCde)), "View " & ProductCategories(i, cxCtgyMenuTxt) & " programs", DirectSellerMenuSection)
	Next
	If nProductCategories = 0 Then 	Call AddSubMenuItem("", "", "", DirectSellerMenuSection)	' NB: create empty sub-menu for this section (in case of DB error!)
	' construct sub-item-menu
	For i = 1 To nProducts
		Call AddSubItemMenuItem(Products(i, cxPdctMenuTxt), OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPdctCdeParam, Products(i, cxPdctPdctCde)), "View " & Products(i, cxPdctMenuTxt) & " products", ctgyMenuTxt)
	Next
	' menu footer
	' if defined (in database) use footer for this page, otherwise use the site default
	If sPageCtlMenuFooter <> "" Then Call AddMenuFooter(sPageCtlMenuFooter)
	
End Function

Function ConstructCustomProductMenu()
	Dim i

	' main menu
	Call ConstructMainMenu()
	' construct sub-menu (of products)
	For i = 1 To nProducts
		Call AddSubMenuItem(Products(i, cxPdctMenuTxt), OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPdctCdeParam, Products(i, cxPdctPdctCde)) & sPdctGrpCdeAddParamString, "View " & Products(i, cxPdctMenuTxt) & " products", CustomProductMenuSection)
	Next
' if we have multiple categories	
'	For i = 1 To nProductCategories
'		Call AddSubMenuItem(ProductCategories(i, cxCtgyMenuTxt), OurASPPage & AddParam("?", cPdctCtgyCdeParam, ProductCategories(i, cxCtgyPdctCtgyCde)) & sPdctGrpCdeAddParamString, "View " & ProductCategories(i, cxCtgyMenuTxt) & " products", CustomProductMenuSection)
'	Next
'	' construct sub-item-menu
'	For i = 1 To nProducts
'		Call AddSubItemMenuItem(Products(i, cxPdctMenuTxt), OurASPPage & AddParam("?", cPdctCtgyCdeParam, sParamPdctCtgyCde) & AddParam("&", cPdctCdeParam, Products(i, cxPdctPdctCde)) & sPdctGrpCdeAddParamString, "View " & Products(i, cxPdctMenuTxt) & " products", ctgyMenuTxt)
'	Next
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


' === Site Product Category Specifics - info gotten from SITE_GetProductCategorySpecifics sp()

Function GetSiteProductCategorySpecifics(ByVal sPdctCtgyCde)
	Dim RS, SQLStmt
	Dim sURL
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

	' RETRIEVE PRIMARY/TAG programs for given PdctCde
	Call LoadDirectSellerListForProductCategory(sPdctCtgyCde)
	
	GetSiteProductCategorySpecifics = (Err.number = 0)
End Function


' === Site Product Specifics - info gotten from SITE_GetProductSpecifics sp()

Function GetSiteProductSpecifics(ByVal sPdctCde)
	Dim RS, SQLStmt
	Dim sURL
	Dim sMetaKywdTxt, sMetaDescTxt, sHTMLTitlTxt

	On Error Resume Next
	Call OpenEZMainDB()

	SQLStmt = "SITE_GetProductSpecifics @PdctCde=" & SQS(sPdctCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	If CheckRS(RS) Then
		pdctPdctNme = nvs(RS.Fields("PDCT_NME"))
		pdctMenuTxt = nvs(RS.Fields("MENU_TXT"))
		pdctImagePrfxNme = nvs(RS.Fields("IMAGE_PRFX_NME"))
		pdctImageExtNme = nvs(RS.Fields("IMAGE_EXT_NME"))
		If pdctImagePrfxNme = "" Then
			pdctImagePrfxNme = PhotoUnavailableImage
		End If
		pdctImageDescTxt = nvs(RS.Fields("IMAGE_DESC_TXT"))
		pdctShrtFeatTxt = nvs(RS.Fields("SHRT_FEAT_TXT"))
		pdctDescTxt = nvs(RS.Fields("DESC_TXT"))
		pdctFeatTxt = nvs(RS.Fields("FEAT_TXT"))
		pdctOrdrItemNbr = nvn(RS.Fields("ORDR_ITEM_NBR"))
		pdctXtrnStrtDte = nvd(RS.Fields("XTRN_STRT_DTE"))
		pdctXtrnEndDte = nvd(RS.Fields("XTRN_END_DTE"))

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

	' RETRIEVE TAB-index information for given PdctCde
	Call LoadProductTabIndex(sParamPdctCde)

	GetSiteProductSpecifics = (Err.number = 0)
End Function


' === Load Site Product List for category

Function LoadDirectSellerListForProductCategory(ByVal sPdctCtgyCde)
	Dim RS, SQLStmt

	On Error Resume Next
	Call OpenEZMainDB()
	
	' RETRIEVE products for given PdctCtgyCde
	
	SQLStmt = "SITE_GetDirectSellerListForPdctCtgy @PdctCtgyCde=" & SQS(sPdctCtgyCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	
	nProducts = 0
	
	Do While CheckRS(RS)

		nProducts = nProducts + 1
		If nProducts > cMaxProducts Then nProducts = cMaxProducts: Exit Do
		
		Products(nProducts, cxPdctPdctCde) = nvs(RS.Fields("PDCT_CDE"))
		Products(nProducts, cxPdctPdctNme) = nvs(RS.Fields("PDCT_NME"))
		Products(nProducts, cxPdctMenuTxt) = nvs(RS.Fields("MENU_TXT"))
		Products(nProducts, cxPdctPrftTxt) = nvs(RS.Fields("PDCT_PRFT_TXT"))
		Products(nProducts, cxPdctDescTxt) = nvs(RS.Fields("DESC_TXT"))
		Products(nProducts, cxPdctImagePrfxNme) = nvs(RS.Fields("IMAGE_PRFX_NME"))
		Products(nProducts, cxPdctImageExtNme) = nvs(RS.Fields("IMAGE_EXT_NME"))
'		If Products(nProducts, cxPdctImagePrfxNme) = "" Then
'			Products(nProducts, cxPdctImagePrfxNme) = PhotoUnavailableImage
'		End If
		Products(nProducts, cxPdctImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT"))
		Products(nProducts, cxPdctShrtFeatTxt) = nvs(RS.Fields("SHRT_FEAT_TXT"))
		Products(nProducts, cxPdctOrdrItemNbr) = nvn(RS.Fields("ORDR_ITEM_NBR"))

		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing
	
	LoadDirectSellerListForProductCategory = (Err.number = 0)
End Function


' === Load Site Product Category List for display on DIRECT SELLER page

Function LoadSitePageProductList(ByVal sPdctGrpCde)
	Dim RS, SQLStmt

	On Error Resume Next
	Call OpenEZMainDB()
	
	SQLStmt = "SITE_GetProductCategoryList @PdctGrpCde=" & SQS(sPdctGrpCde)
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	
	nProductCategories = 0
	Do While CheckRS(RS)
		nProductCategories = nProductCategories + 1
		If nProductCategories > cMaxProductCategories Then nProductCategories = cMaxProductCategories: Exit Do

		ProductCategories(nProductCategories, cxCtgyPdctCtgyCde) = nvs(RS.Fields("PDCT_CTGY_CDE"))
		ProductCategories(nProductCategories, cxCtgyMenuTxt) = nvs(RS.Fields("MENU_TXT"))
		ProductCategories(nProductCategories, cxCtgyImageNme) = nvs(RS.Fields("IMAGE_NME"))
		If ProductCategories(nProductCategories, cxCtgyImageNme) = "" Then
			ProductCategories(nProductCategories, cxCtgyImageNme) = PhotoUnavailableImage
		End If
		ProductCategories(nProductCategories, cxCtgyImageDescTxt) = nvs(RS.Fields("IMAGE_DESC_TXT"))
		ProductCategories(nProductCategories, cxCtgyShrtFeatTxt) = nvs(RS.Fields("SHRT_FEAT_TXT"))
		
		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing
	
	LoadSitePageProductList = (Err.number = 0)
End Function


' === Load Site Product Tab Index data

Function LoadProductTabIndex(sPdctCde)
	Dim RS, SQLStmt

	On Error Resume Next
	Call OpenEZMainDB()
	
	SQLStmt = "SITE_GetProductTabIndex @PdctCde='" & sPdctCde & "'"
	Set RS = Nothing
	Set RS = objEZMainDB.Execute(SQLStmt)
	
	nTabIndexes = 0
	Do While CheckRS(RS)
		nTabIndexes = nTabIndexes + 1
		If nTabIndexes > cMaxTabIndexes Then nTabIndexes = cMaxTabIndexes: Exit Do

		TabIndexes(nTabIndexes, cxTabLabelTxt) = nvs(RS.Fields("LABEL_TXT"))
		TabIndexes(nTabIndexes, cxTabDescTxt) = nvs(RS.Fields("DESC_TXT"))
		
		RS.MoveNext
	Loop
	RS.Close
	Set RS = Nothing
	
	LoadProductTabIndex = (Err.number = 0)
End Function

%>