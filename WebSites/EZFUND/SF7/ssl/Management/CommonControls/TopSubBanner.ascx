<%@ Control ClassName="TopSubBanner1" Language="vb" AutoEventWireup="false" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" CodeBehind="TopSubBanner.ascx.vb" Inherits="StoreFront.StoreFront.TopSubBanner1" %>
<%@ Import Namespace = "StoreFront.SystemBase"%>
<!-- Top Sub Banner Start -->
<table cellspacing="0" width="100%">
	<tr>
		<td class="TopSubBanner" align="center"><%= Me.Title %></td>
		<td class="TopSubBanner" style="text-align: right;" width="20%"><a href="<%=StoreFrontConfiguration.SSLPath%>management/default.aspx?ReloadXML=1">Apply to Site</a> | <asp:LinkButton CssClass="TopSubBanner" id="lbSignOut" runat="server">Sign Out</asp:LinkButton> &nbsp;</td>
	</tr>
</table>
<!-- Top Sub Banner End -->
