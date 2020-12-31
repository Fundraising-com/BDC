<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerSearchMagazine.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerSearchMagazine" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<div id="divSearchTitle">
	<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<td>&nbsp;</td>
			<TD><asp:label id="lblSearchDescription" CssClass="CSPlainText" runat="server">Title</asp:label><br>
				<cc1:textboxsearch id="tbxSearchDescription" runat="server" ParameterName="MagazineTitle"></cc1:textboxsearch>&nbsp;&nbsp;&nbsp;</TD>
			<TD><asp:label id="Label2" CssClass="CSPlainText" runat="server">Product Line</asp:label><br>
				<cc1:dropdownlistsearch id="ddlProductType" runat="server" ParameterName="ProductType" onload="ddlProductType_Load"></cc1:dropdownlistsearch>&nbsp;&nbsp;&nbsp;
			</TD>
			<TD align="center">
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD align="center">&nbsp;&nbsp;&nbsp;<asp:button id="btnSearch" runat="server" Text="Search"></asp:button>
						</TD>
						<TD align="center">&nbsp;&nbsp;&nbsp;<INPUT onclick="Reset('divSearchTitle')" type="button" value="Reset"></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
</div>
