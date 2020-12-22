<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ShippingCarriersControl.ascx.vb" Inherits="StoreFront.StoreFront.ShippingCarriersControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.2.0

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
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;General&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" nowrap colspan="2">City Products Will Be Shipped 
				From:</td>
			<td class="content" align="left" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox id="OriginCity" runat="server" MaxLength="50"></asp:TextBox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" nowrap colspan="2">State Products Will Be Shipped 
				From:</td>
			<td class="content" align="left" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;<asp:dropdownlist id="OriginState" runat="server"></asp:dropdownlist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" nowrap colspan="2">Postal Code Products Will Be 
				Shipped From:</td>
			<td class="content" align="left" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox id="OriginZip" runat="server" MaxLength="50"></asp:TextBox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colspan="2">Backup Shipping Method:</td>
			<td class="content" align="left" colspan="2" width="75%">&nbsp;&nbsp;&nbsp;&nbsp;<asp:dropdownlist id="BackupShipping" runat="server"></asp:dropdownlist></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<asp:Panel Runat="server" ID="CPPanel">
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Canada 
					Post&nbsp;
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="middle" colSpan="1"><IMG src="images/canadapost.jpg"></TD>
				<TD class="content" align="left" colSpan="3">Canada Post Shipping and Delivery 
					Services are the choice for Canadian businesses shipping internationally, to 
					the USA or within Canada. <A href="http://www.canadapost.ca/business/obc/apply/cust_number-e.asp" target="_blank">
						Click Here</A> to sign up for an account.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="left" colSpan="4">
					<asp:CheckBox id="CPActive" runat="server" Text="Activate CanadaPost"></asp:CheckBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="25%">User Name:</TD>
				<TD class="content" align="left" colSpan="3">
					<asp:TextBox id="CPUserName" runat="server" MaxLength="255"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="left" colSpan="4"><A href="shippingMethods.aspx?CarrierCode=CP">Select 
						Supported Canada Post Delivery Services</A></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
			</TR>
		</asp:Panel>
		<asp:Panel Runat="server" ID="UPSPanel">
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;United 
					Parcel Service (UPS®)&nbsp;
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="middle" colSpan="1"><IMG src="images/ups.jpg"></TD>
				<TD class="content" align="left" colSpan="3">United Parcel Service (NYSE: UPS), the 
					world's largest express carrier and package delivery company, operates in more 
					than 200 countries and territories and employs more than 370,000 people 
					worldwide.<BR>
					<asp:LinkButton id="UPSLink" Runat="server">Click Here</asp:LinkButton>&nbsp;to 
					register to use UPS® Rates &amp; Service Selection Tool and UPS® Tracking Tool.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="left" colSpan="4">
					<asp:CheckBox id="UPSActive" runat="server" Text="Activate UPS"></asp:CheckBox>®</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="middle" colSpan="4">
					<asp:Label id="UPSRegistered" Runat="server">You are currently registered for UPS® Rates &amp; Service Selection and UPS® Tracking 
      Tools.</asp:Label>
					<asp:Label id="UPSNotRegistered" Runat="server">You are not currently registered for UPS® Rates &amp; Service Selection and UPS® Tracking 
      Tools.</asp:Label></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="left" colSpan="4"><A href="shippingMethods.aspx?CarrierCode=UPS">Select 
						Supported UPS® Delivery Services</A></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="middle" colSpan="4">UPS®, UPS &amp; Shield Design® and 
					UNITED PARCEL SERVICE® are registered trademarks of United Parcel Service of 
					America, Inc.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
			</TR>
		</asp:Panel>
		<asp:Panel Runat="server" ID="FEPanel">
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Federal 
					Express (FedEx)&nbsp;
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="middle" colSpan="1"><IMG src="images/fedex.jpg"></TD>
				<TD class="content" align="left" colSpan="3">FedEx Corporation provides integrated 
					transportation, information, and logistics solutions through a powerful family 
					of companies that operate independently yet compete collectively. <A href="https://www.fedex.com/us/ebusiness/internetintegration/" target="_blank">
						Click Here</A> to sign up for an account.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="left" colSpan="4">
					<asp:CheckBox id="FEActive" runat="server" Text="Activate FedEx"></asp:CheckBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right" width="25%">Account Number:</TD>
				<TD class="content" align="left" colSpan="3">
					<asp:TextBox id="FedExUserName" runat="server" MaxLength="255"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="left" colSpan="4"><A href="shippingMethods.aspx?CarrierCode=FEDEX">Select 
						Supported FedEx Delivery Services</A></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
			</TR>
		</asp:Panel>
		<asp:Panel Runat="server" ID="USPSPanel">
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;US 
					Postal Service (USPS)&nbsp;
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="middle" colSpan="1"><IMG src="images/usps.jpg"></TD>
				<TD class="content" align="left" colSpan="3">Use the Postal Service as a shipping 
					solution for online purchases to take advantage of additional benefits, 
					including lower shipping costs, Saturday delivery and delivery to post office 
					boxes. <A href="http://www.usps.com" target="_blank">Click Here</A> to sign up 
					for an account.
				</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="left" colSpan="4">
					<asp:CheckBox id="USPSActive" runat="server" Text="Activate USPS"></asp:CheckBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right">User Name:</TD>
				<TD class="content" align="left">
					<asp:TextBox id="USPSUserName" runat="server" MaxLength="255"></asp:TextBox></TD>
				<TD class="content" align="right">Password:</TD>
				<TD class="content" align="left">
					<asp:TextBox id="USPSPassword" runat="server" MaxLength="255"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="left" colSpan="4"><A href="shippingMethods.aspx?CarrierCode=USPS">Select 
						Supported US Postal Service Delivery Services</A></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
			</TR>
		</asp:Panel>
		<asp:Panel Runat="server" ID="LTLPanel">
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;FreightQuote.com 
					(LTL)&nbsp;
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="middle" colSpan="1"><IMG src="images/freightquote.jpg"></TD>
				<TD class="content" align="left" colSpan="3">Compare freight quotes and dispatch 
					shipments in an instant. Self schedule your freight at freightquote.com. <A href="http://www.freightquote.com" target="_blank">
						Click Here</A> to sign up for an account.</TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="left" colSpan="4">
					<asp:CheckBox id="LTLActive" runat="server" Text="Activate FreightQuote"></asp:CheckBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" align="right">User Name:</TD>
				<TD class="content" align="left">
					<asp:TextBox id="LTLUserName" runat="server" MaxLength="255"></asp:TextBox></TD>
				<TD class="content" align="right">Password:</TD>
				<TD class="content" align="left">
					<asp:TextBox id="LTLPassword" runat="server" MaxLength="255"></asp:TextBox></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
				<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<TR>
				<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<TR>
				<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
			</TR>
		</asp:Panel>
		<TR>
			<td class="content" align="right" width="75%" colSpan="6">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>
