<!-- START OF MASTHEAD -->
<td width="100%" valign="top" align="left">
  <table class="MastHead">
	<tr>
		<td rowspan="3" valign="middle" align="left" class="MastLogo">
			<a href="<%=HOME_URL%>" title="EZFund.com home page">
				<img border="0" src="../images/EZFundLogo_Web.gif" alt="EZFund.com - Makes fundraising EZ!" align="left" valign="top" hspace="8" vspace="0" width="220" height="55">
			</a>
		</td>
		<td colspan="3" valign="middle" align="right" class="MastSiteMap"><a href="http://shop.ezfund.com/shoppingcart.asp" title="View EZFund.com Shopping Cart."><img src="../images/ShoppingCart.gif" width="25"height="24 alt="View EZFund.com Shopping Cart">
</a><a href="http://shop.ezfund.com/shoppingcart.asp" title="EZFund.com">Shopping Cart</a>&nbsp;</td>
	</tr>
	<tr><td colspan="3" valign="top" align="right" class="MastPhoneInfo"><img src="../images/1800-number.gif" alt="Call EZFund.com for your Fundraising Ideas!"></td></tr>
	<tr><td colspan="3" valign="top" align="right" class="MastMenuLinks"><%=EmitEZMastMenu()%></td></tr>
  </table>
  <div class="MastHR"></div>
</td>
<!-- END OF MASTHEAD -->
</tr>
<tr>
<!-- START OF PAGE CONTENT -->
<td valign="top" align="left" class="PageContent">

<script LANGUAGE="VBScript" RunAt="Server">

Function EmitEZSiteMapLink()
	Response.Write "<a href=" & QS(SiteMapASP) & " title=" & QS("View map of EZFund.com site") & ">Site map</a>"
End Function

Function EmitEZSiteMapDropdown()

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
	Dim sDispTxt, sURLTxt, nLevlNbr, sCSSClassNme
	
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
		
		If nElementCnt = 0 Then
			' START dropdown
			' Note: EZRedirect.asp is executed for browsers w/ Javascript disabled!
			Response.Write "<form method='POST' action='/svc/EZRedirect.asp' name=SiteMapForm class=SiteMapFormBox>"
' REMOVE THIS! wrap logic in function to handle empty URL and other special handling
'			Response.Write "Map: <select name=URL onChange='window.location=document.SiteMapForm.URL.options[document.SiteMapForm.URL.selectedIndex].value' class=FormField>"
			Response.Write "Map: <select name=URL onChange='SiteMapOnChange(document.SiteMapForm.URL)' class=FormField>"
		End If

		' format this entry
		sElementTxt = "<option value='" & sURLTxt & "'" & InclIf(sCSSClassNme <> "", " class=" & sCSSClassNme, "") & ">"
		Select Case nLevlNbr
			Case 3:		sElementTxt = sElementTxt & "&nbsp;&nbsp;&nbsp;&raquo; "
			Case 2:		sElementTxt = sElementTxt & "&nbsp;&bull; "
			Case Else:	' nothing
		End Select
		sElementTxt = sElementTxt & sDispTxt & "</option>"
		Response.Write sElementTxt
		
		nElementCnt = nElementCnt + 1
		RS.MoveNext
	Loop
	
	If nElementCnt > 0 Then
		' END dropdown
		Response.Write "</select>"
		Response.Write "<noscript><INPUT type='submit' value='Go' name=submit1></noscript>"
		Response.Write "</form>"
	End If

	RS.Close
	Set RS = Nothing
	Call CloseEZMainDB()
	
End Function

</script>