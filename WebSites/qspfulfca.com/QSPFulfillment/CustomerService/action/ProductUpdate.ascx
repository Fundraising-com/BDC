<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ProductUpdate.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.ProductUpdate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ControlerMagazineTerm" Src="../ControlerMagazineTerm.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<uc1:controlermagazineterm id="ctrlControlerMagazineTerm" ShowSearch="True" ProductType="-1" runat="server"></uc1:controlermagazineterm><br>
<div id="divStep2" runat="server"><BR>
	<BR>
	<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0">
		<TR>
			<TD style="WIDTH: 120px"><asp:label id="Label1" runat="server" cssClass="CSPlainText">Title Code</asp:label></TD>
			<TD><asp:label id="lblTitleCode" runat="server" cssClass="CSPlainText"></asp:label></TD>
		<TR>
			<TD style="WIDTH: 120px"><asp:label id="Label2" runat="server" cssClass="CSPlainText">Magazine Title</asp:label></TD>
			<TD><asp:label id="lblMagazineTitle" runat="server" cssClass="CSPlainText"></asp:label></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 120px"><asp:label id="Label3" runat="server" cssClass="CSPlainText">Term</asp:label></TD>
			<TD><asp:label id="lblTerm" runat="server" cssClass="CSPlainText"></asp:label></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 120px"></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 120px"><asp:label id="Label5" runat="server" cssClass="CSPlainText">Price</asp:label></TD>
			<TD><asp:label id="lblPrice" runat="server" cssClass="CSPlainText"></asp:label></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 120px"><asp:label id="Label6" runat="server" cssClass="CSPlainText">Catalog Price</asp:label></TD>
			<TD><asp:label id="lblCatalogPrice" runat="server" cssClass="CSPlainText"></asp:label><asp:customvalidator id="Validation" Runat="server"></asp:customvalidator></TD>
		</TR>
	</TABLE>
</div>
<BR>
<BR>
<div style="TEXT-ALIGN: right"><asp:button id="btnBack" Runat="server" Text="Back" onclick="btnBack_Click"></asp:button></div>
