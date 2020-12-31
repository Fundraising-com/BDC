<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="ControlerAddress" Src="../ControlerAddress.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerMagazineTerm" Src="../ControlerMagazineTerm.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NewSubTimeStaffOrLoonie.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.NewSubTimeStaffOrLoonie" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div id="divStep2" runat="server">&nbsp;</div>
<DIV runat="server"><uc1:controleraddress id="ctrlControlerAddress" runat="server"></uc1:controleraddress><BR>
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
</DIV>
<BR>
<BR>
<div style="TEXT-ALIGN: right">&nbsp;</div>
