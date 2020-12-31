<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="InfoSearchMagazine.ascx.cs" Inherits="QSPFulfillment.CustomerService.InfoSearchMagazine" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table>
	<TR>
		<TD><asp:label id="Label14" runat="server" CssClass="csPlainText">Product Code</asp:label></TD>
	</TR>
	<TR>
		<TD>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD><cc1:textboxsearch id="tbxTitleCode" runat="server" ParameterName="ProductCode"></cc1:textboxsearch></TD>
					<TD><asp:HyperLink id=hypFindMagazine Runat="server" ImageUrl='<%#"images/find"+(this.Enabled==false?"_disabled":"")+".gif"%>' NavigateUrl="javascript:void(0);">
						</asp:HyperLink></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD><asp:label id="Label13" runat="server" CssClass="csPlainText">Product Name</asp:label></TD>
	</TR>
	<TR>
		<TD><cc1:textboxsearch id="tbxTitle" runat="server" ParameterName="Title"></cc1:textboxsearch></TD>
	</TR>
</table>
