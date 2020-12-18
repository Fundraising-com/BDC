<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register TagPrefix="uc1" TagName="ShippingPackages" Src="controls/CSRShippingPackages.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Payment" Src="controls/CSRPayment.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Discounts" Src="controls/CSRDiscounts.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Top" Src="controls/CSRTop.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Customer" Src="controls/CSRCustomer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Products" Src="controls/CSRProducts.ascx" %>
<%@ Page validaterequest="false" Language="vb" AutoEventWireup="false" Codebehind="OrderForm.aspx.vb" Inherits="StoreFront.StoreFront.OrderForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Order Form</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		function CallPage(spage)
{
	var sFeatures, h, w, myThanks, i
	h = window.screen.availHeight 
	w = window.screen.availWidth 
	
	sFeatures = "resizable,scrollbars=yes"
	myThanks = window.open(spage,"",sFeatures)
}
function Search()
{
	var CallPageURL
	
	CallPageURL= "CSRSearch.aspx?Keyword=" + window.Form.item("products1:NewSKU").value;
	
	CallPage(CallPageURL);
}
function SetShip()
{
window.document.Form.elements["Customers1_IgnoreShipChange"].text="0"
}
function MoveRight()
{


if (window.document.Form.elements["Customers1_BillAddresslist"] != null)
						{
					window.document.Form.elements["Customers1_ShipAddressList"].selectedIndex = window.document.Form.elements["Customers1_BillAddresslist"].selectedIndex;
if (window.document.Form.elements["Customers1_ShipAddressList"].selectedIndex==0)
{//Move()
}
else
{
 //__doPostBack("Customers1:ShipAddressList", "SelectedIndexChanged");
}
											}	
											else
											{
//Move()
	}
}
function Move()
{
						window.document.Form.elements["Customers1_ShipNickName"].value = window.document.Form.elements["Customers1_BillNickName"].value;
						window.document.Form.elements["Customers1_ShipFirstName"].value = window.document.Form.elements["Customers1_BillFirstName"].value;
						window.document.Form.elements["Customers1_ShipMI"].value = window.document.Form.elements["Customers1_BillMI"].value;
						window.document.Form.elements["Customers1_ShipLastName"].value = window.document.Form.elements["Customers1_BillLastName"].value;
						window.document.Form.elements["Customers1_ShipCompany"].value = window.document.Form.elements["Customers1_BillCompany"].value;
						window.document.Form.elements["Customers1_ShipAddress1"].value = window.document.Form.elements["Customers1_BillAddress1"].value;
						window.document.Form.elements["Customers1_ShipAddress2"].value = window.document.Form.elements["Customers1_BillAddress2"].value;
						window.document.Form.elements["Customers1_ShipCity"].value = window.document.Form.elements["Customers1_BillCity"].value;
						window.document.Form.elements["Customers1_ShipZip"].value = window.document.Form.elements["Customers1_BillZip"].value;
						window.document.Form.elements["Customers1_ShipPhone"].value = window.document.Form.elements["Customers1_BillPhone"].value;
						window.document.Form.elements["Customers1_ShipFax"].value = window.document.Form.elements["Customers1_BillFax"].value;
window.document.Form.elements["Customers1_ShipState"].selectedIndex = window.document.Form.elements["Customers1_BillState"].selectedIndex;
window.document.Form.elements["Customers1_ShipCountry"].selectedIndex = window.document.Form.elements["Customers1_BillCountry"].selectedIndex;
}
function isKey(e, k)
{
        var key;
        if(window.event) //IE
        {
                e = window.event;
                key = e.keyCode;
        }
        else //Mozilla
        {
                key = e.which;
        }
        if (key == k)
        {
                return true;
        }
        return false;
}
                
function isEnterKey(e)
{
   return isKey(e, 13);
}
 
 
function postBack(e, eventTarget, eventArgument)
{
   e.returnValue=false;
   e.cancel = true;
   __doPostBack(eventTarget, eventArgument);
}
		</script>
	</HEAD>
	<body class="GeneralPage" id="BodyTag" runat="server" MS_POSITIONING="GridLayout">
		<form id="Form" runat="server">
			<table id="PageTable" cellSpacing="0" cellpadding="0" width="100%" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" cellSpacing="0" cellpadding="0" width="100%" runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start -->
									<div class="TopBanner"><img src="images/sflogo.jpg" style="FLOAT:left;" width="203" height="39"> <font style="LINE-HEIGHT:38px">&nbsp; 
											Order Form</font>
									</div> <!-- Top Banner End --></td>
							</tr>
							<tr align="center">
								<td class="TopSubBanner" id="TopSubBannerCell" align="center" width="100%" colSpan="3">
									<!-- Top Sub Banner Start -->
									<ajax:ajaxpanel id="Ajaxpanel3" runat="server" width="100%">
										<uc1:Top id="Top1" runat="server" width="100%"></uc1:Top>
									</ajax:ajaxpanel>
									<!-- Top Sub Banner End --></td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell" vAlign="top" align="left">
									<!-- Left Column Start -->
									<!-- Left Column End --></td>
								<td class="Content" id="ContentCell" vAlign="top" align="center">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
								            <td class="Content" align="right">
									            <!-- Help Button -->
									            <a href="javascript: doHelp(' http://support.storefront.net/mtdocs70/csr_orderform.asp  ')">
										            <img src="images/help.jpg" border="0"></a> 
									            <!-- End Help Button --></td>
							            </tr>
										<tr>
											<td>
												<!-- Instruction Start -->
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content" width="100%">
												<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label></font></P>
												<ajax:ajaxpanel id="AjaxPanel1" runat="server" width="100%">
													<TABLE id="HoldsProducts" cellSpacing="0" cellPadding="0" width="100%">
														<TR>
															<TD class="MainTableHeader" style="PADDING-LEFT: 3px" colSpan="3">Order Items</TD>
														<TR>
															<TD class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1" height="1"></TD>
															<TD class="Content" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px">
																<uc1:products id="Products1" runat="server" width="100%"></uc1:products></TD>
															<TD class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1" height="1"></TD>
														</TR>
														<TR>
															<TD class="MainTableHeader" width="100%" colSpan="3" height="1"><IMG src="images/clear.gif" width="1" height="1"></TD>
														</TR>
													</TABLE>
												</ajax:ajaxpanel>
												<br>
												<br>
		
													<TABLE id="HoldsCustomers" cellSpacing="0" cellPadding="0" width="100%">
														<TR>
															<TD class="MainTableHeader" style="PADDING-LEFT: 3px" colSpan="3">Customer and Shipping 
																Information</TD>
														<TR>
															<TD class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1" height="1"></TD>
															<TD class="Content" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px">
													<ajax:ajaxpanel id="Ajaxpanel2" runat="server" width="100%">
															<uc1:Customer id="Customers1" runat="server" width="100%"></uc1:Customer>
													</ajax:ajaxpanel>
													<ajax:ajaxpanel id="Ajaxpanel4" runat="server" width="100%"><BR>
													
													<uc1:ShippingPackages id="ShippingPackages1" runat="server" width="100%"></uc1:ShippingPackages>
												<BR></ajax:ajaxpanel>
													
															</TD>
															<TD class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1" height="1"></TD>
														</TR>
														<TR>
															<TD class="MainTableHeader" width="100%" colSpan="3" height="1"><IMG src="images/clear.gif" width="1" height="1"></TD>
														</TR>
													</TABLE>
													
													
													
												<ajax:ajaxpanel id="Ajaxpanel5" runat="server" width="100%">
													<uc1:Discounts id="Discounts1" runat="server" width="100%"></uc1:Discounts>
												</ajax:ajaxpanel>
												<br><br>
													<TABLE id="HoldsPaymentInfo" cellSpacing="0" cellPadding="0" width="100%">
														<TR>
															<TD class="MainTableHeader" style="PADDING-LEFT: 3px" colSpan="3">Payment 
																Information</TD>
														<TR>
															<TD class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1" height="1"></TD>
															<TD class="Content" style="PADDING-RIGHT: 3px; PADDING-LEFT: 3px; PADDING-BOTTOM: 3px; PADDING-TOP: 3px">
												<br/>																											
												<ajax:ajaxpanel id="Ajaxpanel6" runat="server" width="100%">
													<uc1:Payment id="Payment1" runat="server" width="100%"></uc1:Payment>
												</ajax:ajaxpanel>
															</TD>
															<TD class="MainTableHeader" width="1"><IMG src="images/clear.gif" width="1" height="1"></TD>
														</TR>
														<TR>
															<TD class="MainTableHeader" width="100%" colSpan="3" height="1"><IMG src="images/clear.gif" width="1" height="1"></TD>
														</TR>
													</TABLE>
																

																								
												<input id="FillShipping" type="hidden" value="0" runat="server">
												<table width="100%">
													<tr>
														<td align="right"><ajax:ajaxpanel id="Ajaxpanel7" runat="server" width="100%">
																<asp:LinkButton id="CompleteOrder" Runat="server">
																	<asp:Image ImageUrl="images/submit.jpg" Runat="server" ID="Image1"></asp:Image>
																</asp:LinkButton>
															</ajax:ajaxpanel></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<!-- Content End --></td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start -->
									<!-- Right Column End --></td>
							</tr>
							<tr>
								<td class="Footer" id="FooterCell" colSpan="3">
									<!-- Footer Start -->
									<!-- Footer End --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
