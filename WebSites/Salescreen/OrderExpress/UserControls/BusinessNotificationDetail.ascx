<%@ Reference Control="BusinessNotificationDetailInfo.ascx" %>
<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BusinessNotificationHeaderForm" Src="BusinessNotificationHeaderForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ToDoDetail" Codebehind="BusinessNotificationDetail.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<uc1:BusinessNotificationHeaderForm id="HeaderDetail" runat="server"></uc1:BusinessNotificationHeaderForm>
			<br>
		</TD>
	</TR>
	<tr>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td><asp:CheckBox Runat="server" ID="chkSendMail" Text="&nbsp;Send&nbsp;Email&nbsp;Notification" CssClass="StandardLabel"></asp:CheckBox></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="center">
			<uc1:ToolBar id="QSPToolBar" runat="server"></uc1:ToolBar>
		</td>
	</tr>
</TABLE>
