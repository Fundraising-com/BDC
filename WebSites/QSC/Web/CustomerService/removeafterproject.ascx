<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="NewSubStep2" Src="action/NewSubStep2.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="removeafterproject.ascx.cs" Inherits="QSPFulfillment.CustomerService.removeafterproject" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<br>
<TABLE id="Table1" cellSpacing="0" cellPadding="2" border="0" width="100%">
	<TR>
		<TD>
			<asp:Label id="Label1" runat="server" cssClass="CSPlainText">Title Code</asp:Label></TD>
		<TD>
			<asp:Label id="lblTitleCode" runat="server" cssClass="CSPlainText"></asp:Label></TD>
		<TD></A></TD>
	<TR>
		<TD>
			<asp:Label id="Label2" runat="server" cssClass="CSPlainText">Magazine Title</asp:Label></TD>
		<TD>
			<asp:Label id="lblMagazineTitle" runat="server" cssClass="CSPlainText"></asp:Label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label3" runat="server" cssClass="CSPlainText">Term</asp:Label></TD>
		<TD>
			<asp:Label id="lblTerm" runat="server" cssClass="CSPlainText"></asp:Label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label4" runat="server" cssClass="CSPlainText">New or Renewal</asp:Label></TD>
		<TD>
			<asp:DropDownList id="ddlNewRenewal" runat="server">
				<asp:ListItem Value="N">New</asp:ListItem>
				<asp:ListItem Value="R">Renewal</asp:ListItem>
			</asp:DropDownList></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label5" runat="server" cssClass="CSPlainText">Price</asp:Label></TD>
		<TD>
			<cc1:Currency id="tbxPrice" runat="server" Required="True"></cc1:Currency></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label6" runat="server" cssClass="CSPlainText">Catalog Price</asp:Label></TD>
		<TD>
			<asp:Label id="lblCatalogPrice" runat="server" cssClass="CSPlainText"></asp:Label></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label7" runat="server" cssClass="CSPlainText">Price override reason</asp:Label></TD>
		<TD>
			<asp:DropDownList id="ddlPriceOverrideReason" runat="server"></asp:DropDownList></TD>
		<TD>
			<asp:Button id="btnBack" runat="server" Text="Change Selection"></asp:Button></TD>
	</TR>
</TABLE>
