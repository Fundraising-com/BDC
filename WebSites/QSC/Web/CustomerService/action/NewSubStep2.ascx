<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NewSubStep2.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.NewSubStep2" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../../Common/AddressHygiene.ascx" %>
<br>
<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0">
	<TR>
		<TD>
			<asp:Label id="Label12" cssClass="csPlainText" runat="server">First Name</asp:Label></TD>
		<TD>
			<asp:TextBox id="tbxFirstName" runat="server"></asp:TextBox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label13" cssClass="csPlainText" runat="server">Last Name</asp:Label></TD>
		<TD>
			<asp:TextBox id="tbxLastName" runat="server"></asp:TextBox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label14" cssClass="csPlainText" runat="server"> Address Line 1</asp:Label></TD>
		<TD>
			<asp:TextBox id="tbxStreet1" runat="server"></asp:TextBox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label15" cssClass="csPlainText" runat="server">Address Line 2</asp:Label></TD>
		<TD>
			<asp:TextBox id="tbxStreet2" runat="server"></asp:TextBox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label33" cssClass="csPlainText" runat="server">City</asp:Label></TD>
		<TD>
			<asp:TextBox id="tbxCity" runat="server"></asp:TextBox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label16" cssClass="csPlainText" runat="server">Postal Code</asp:Label></TD>
		<TD>
			<cc1:PostalCode id="tbxPostalCode" runat="server" MaxLength="6"></cc1:PostalCode></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label17" cssClass="csPlainText" runat="server">Province</asp:Label></TD>
		<TD>
			<cc1:dropdownlistprovince id="ddlProvince" runat="server" Code="CA"></cc1:dropdownlistprovince></TD>
		<TD></TD>
	</TR>
	<tr>
		<td colSpan="2" style="HEIGHT: 45px">
			<asp:button id="btnValidateAddress" runat="server" Text="Validate Address" CausesValidation="False" onclick="btnValidateAddress_Click"></asp:button></td>
	</tr>
	<tr>
		<td colspan="2" style="HEIGHT: 22px">
			<uc2:addressHygiene id="ctrlAddressHygiene" runat="server" visible="false"></uc2:addressHygiene>
			<asp:Label id="AddressHygieneStatusLabel" runat="server" ForeColor="Blue"></asp:Label></td>
	</tr>
	<TR>
		<TD colspan="3">&nbsp;</TD>
	</TR>
	<TR>
		<TD><asp:label id="Label1" cssClass="CSPlainText" runat="server">Title Code</asp:label></TD>
		<TD><asp:label id="lblTitleCode" cssClass="CSPlainText" runat="server"></asp:label></TD>
		<TD></A></TD>
	<TR>
		<TD><asp:label id="Label2" cssClass="CSPlainText" runat="server">Magazine Title</asp:label></TD>
		<TD><asp:label id="lblMagazineTitle" cssClass="CSPlainText" runat="server"></asp:label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label3" cssClass="CSPlainText" runat="server">Term</asp:label></TD>
		<TD><asp:label id="lblTerm" cssClass="CSPlainText" runat="server"></asp:label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label4" cssClass="CSPlainText" runat="server">New or Renewal</asp:label></TD>
		<TD><asp:dropdownlist id="ddlNewRenewal" runat="server">
				<asp:ListItem Value="N">New</asp:ListItem>
				<asp:ListItem Value="R">Renewal</asp:ListItem>
			</asp:dropdownlist></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label5" cssClass="CSPlainText" runat="server">Price</asp:label></TD>
		<TD><cc1:currency id="tbxPrice" runat="server" Required="True"></cc1:currency></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label6" cssClass="CSPlainText" runat="server">Catalog Price</asp:label></TD>
		<TD><asp:label id="lblCatalogPrice" cssClass="CSPlainText" runat="server"></asp:label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD><asp:label id="Label7" cssClass="CSPlainText" runat="server">Price override reason</asp:label></TD>
		<TD><asp:dropdownlist id="ddlPriceOverrideReason" runat="server"></asp:dropdownlist></TD>
		<TD><asp:button id="btnBack" runat="server" CausesValidation="false" Text="Change Selection" onclick="btnBack_Click"></asp:button></TD>
	</TR>
</TABLE>
