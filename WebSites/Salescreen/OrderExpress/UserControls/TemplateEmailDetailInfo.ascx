<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.TemplateEmailDetailInfo" Codebehind="TemplateEmailDetailInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TemplateEmailInfo" Src="TemplateEmailInfo.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<td>
			<uc1:TemplateEmailInfo id="ctrlTemplateEmailInfo" runat="server" />
			<br>
		</td>
	</TR>
	<tr>
		<td align="center">
			<uc1:ToolBar id="QSPToolBar" runat="server"></uc1:ToolBar>
		</td>
	</tr>
</TABLE>
