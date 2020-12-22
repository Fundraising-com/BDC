<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserInfo" Src="UserInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.UserDetailInfo" Codebehind="UserDetailInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<uc1:UserInfo id="UserInfo1" runat="server"></uc1:UserInfo>
			<br>
		</TD>
	</TR>
	<TR>
		<td align="center">
			<!--
			<table border="0" cellpadding="0" cellspacing="0" id="Table1">
				<tr>
					<td align="center">
						<asp:ImageButton id="imgBtnEdit" runat="server" CausesValidation="False" ImageUrl="~/images/btnEdit.gif"
							AlternateText="Edit"></asp:ImageButton>
					</td>
					<td>
						&nbsp;&nbsp;
					</td>
					<td align="center">
						<asp:HyperLink id="hypLnkClose" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</tr>
			</table>
			-->
			<uc1:ToolBar id="QSPToolBar" runat="server"></uc1:ToolBar>
		</td>
	</TR>
</TABLE>
