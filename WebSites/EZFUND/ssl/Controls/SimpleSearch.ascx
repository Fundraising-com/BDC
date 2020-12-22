<%@ Control Language="vb" enableViewState="False" AutoEventWireup="false" Codebehind="SimpleSearch.ascx.vb" Inherits="StoreFront.StoreFront.SimpleSearch" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:Panel Runat="server" CssClass="<%=CssCls%>" HorizontalAlign="Center" BorderWidth="0" id="Panel1">
	<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD class="<%=CssCls%>">&nbsp;</TD>
			<TD class="<%=CssCls%>" noWrap width="100%">Search:</TD>
		</TR>
		<TR>
			<TD class="<%=CssCls%>">&nbsp;</TD>
			<TD class="<%=CssCls%>" noWrap>
				<asp:TextBox id="txtSimpleSearch" CssClass="Content" Columns="12" runat="server" size="12"></asp:TextBox>
				<asp:LinkButton id="btnSearch" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSearch" Runat="server" AlternateText="Search"></asp:Image>
				</asp:LinkButton></TD>
			<TD 
class="<%=CssCls%>">&nbsp;</TD>
		</TR>
	</TABLE>
</asp:Panel>
