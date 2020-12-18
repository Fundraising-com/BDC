<%@ Reference Control="TaskDetailInfo.ascx" %>
<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TaskForm" Src="TaskForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.TaskDetail" Codebehind="TaskDetail.ascx.cs" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td>
			<uc1:TaskForm id="TaskForm_Ctrl" runat="server"></uc1:TaskForm>
			<br>
		</td>
	</tr>
	<tr>
		<td align="center">
			<uc1:ToolBar id="QSPToolBar" runat="server"></uc1:ToolBar>
		</td>
	</tr>
</table>
