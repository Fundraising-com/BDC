<%@ Reference Control="ProgramDetailInfo.ascx" %>
<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProgramForm" Src="ProgramForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramDetail" Codebehind="ProgramDetail.ascx.cs" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td>
			<uc1:ProgramForm id="ProgramForm_Ctrl" runat="server"></uc1:ProgramForm>
			<br>
		</td>
	</tr>
	<tr>
		<td align="center">
			<uc1:ToolBar id="QSPToolBar" runat="server"></uc1:ToolBar>
		</td>
	</tr>
</table>
