<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SavedCart.aspx.vb" Inherits="StoreFront.StoreFront.SavedCart"%>
<%@ Register TagPrefix="uc1" TagName="TopSubBanner" Src="CommonControls/TopSubBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="CommonControls/TopBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Instruction" Src="CommonControls/Instruction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LeftColumnNav" Src="CommonControls/LeftColumnNav.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="CommonControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RightColumnNav" Src="CommonControls/RightColumnNav.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Saved Cart</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.1.0

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
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
		
			function SetValidationUpdateQty(cnt)
			{
			//window.alert(window.document.Form2.item("AdvancedSearch1:AdvPriceStart").value);
			ResetForm(window.document.Form2);
			
			for (var i=1; i<cnt+1; i++) {
			if (window.document.Form2.elements["DynamicCartDisplay1:Qty" + i] !=null)
				{window.document.Form2.elements["DynamicCartDisplay1:Qty" + i].required=true;
				window.document.Form2.elements["DynamicCartDisplay1:Qty" + i].number=true;
				window.document.Form2.elements["DynamicCartDisplay1:Qty" + i].title="Qty " + i;
				}
				}
				return ValidateForm(window.document.Form2)
			}
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" class="GeneralPage" runat="server" id="BodyTag">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table id="PageSubTable" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1" runat="server">
							<tr>
								<td class="TopBanner" id="TopBannerCell" width="100%" colSpan="3">
									<!-- Top Banner Start -->
									<uc1:topbanner id="TopBanner1" runat="server"></uc1:topbanner>
									<!-- Top Banner End -->
								</td>
							</tr>
							<tr>
								<td class="TopSubBanner" id="TopSubBannerCell" width="100%" colSpan="3">
									<!-- Top Sub Banner Start -->
									<uc1:topsubbanner id="TopSubBanner1" runat="server"></uc1:topsubbanner>
									<!-- Top Sub Banner End -->
								</td>
							</tr>
							<tr>
								<td class="LeftColumn" id="LeftColumnCell">
									<!-- Left Column Start -->
									<uc1:leftcolumnnav id="LeftColumnNav1" runat="server"></uc1:leftcolumnnav>
									<!-- Left Column End -->
								</td>
								<td class="Content" vAlign="top" id="ContentCell">
									<!-- Content Start -->
									<table cellSpacing="3" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="Instructions">
												<!-- Instruction Start --><uc1:instruction id="Instruction1" runat="server"></uc1:instruction>
												<!-- Instruction End --></td>
										</tr>
										<tr>
											<td class="Content">
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
												<P id="MessageAlignment" runat="server" align="center">
													<asp:Label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:Label>
												</P>
												<TABLE id="Table3" runat="server" cellSpacing="0" cellPadding="0" border="0" width="100%">
													<TR>
														<TD class="Headings" noWrap><asp:Label id="lblHeading" runat="server"></asp:Label></TD>
													</TR>
													<TR>
														<TD class="Content">&nbsp;</TD>
													</TR>
													<TR>
														<TD class="Content">
															<cc1:DynamicCartDisplay id="DynamicCartDisplay1" runat="server" GiftWrapBtnDisplay="False" RemoveBtnDisplay="True" ReOrderBtnDisplay="False" WishListBtnDisplay="False" DesignCount="2" DisplayGiftWrapRow="False" Editable="True" NegativeMessage="Quantity Cannot be Negative" HeadingClass="ContentTableHeader" BorderClass="ContentTable" HorizontalClass="ContentTable" SavedCartBtnDisplay="False" OptionsLabel="Options" PriceLabel="Price" ProductLabel="Product" QuantityLabel="Qty" StatusLabel="Status" TotalLabel="Total"></cc1:DynamicCartDisplay></TD>
													</TR>
													<TR>
														<TD class="Content">&nbsp;</TD>
													</TR>
													<TR>
														<TD align="right" class="Content">
															<asp:LinkButton ID="btnUpdate" Runat="server">
																<asp:Image BorderWidth="0" ID="imgUpdate" Runat="server" AlternateText="Update"></asp:Image>
															</asp:LinkButton>
														</TD>
													</TR>
													<TR>
														<TD class="Content" align="right">&nbsp;</TD>
													</TR>
													<TR>
														<TD class="Content" align="right">
															<asp:LinkButton ID="btnEmailList" Runat="server" OnClick="EMailList">
																<asp:Image BorderWidth="0" ID="imgEmailList" Runat="server" AlternateText="Email List"></asp:Image>
															</asp:LinkButton>
														</TD>
													</TR>
												</TABLE>
											</td>
										</tr>
									</table>
									<!-- Content End -->
								</td>
								<td class="RightColumn" id="RightColumnCell">
									<!-- Right Column Start -->
									<uc1:rightcolumnnav id="RightColumnNav1" runat="server"></uc1:rightcolumnnav>
									<!-- Right Column End -->
								</td>
							</tr>
							<tr>
								<td colSpan="3" class="Footer" id="FooterCell">
									<!-- Footer Start -->
									<uc1:footer id="Footer1" runat="server"></uc1:footer>
									<!-- Footer End -->
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
