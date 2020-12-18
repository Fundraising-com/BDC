<%@ Reference Control="TemplateEmailDetailInfo.ascx" %>
<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.TemplateEmailDetail" Codebehind="TemplateEmailDetail.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="TemplateHeaderForm" Src="TemplateEmailHeaderForm.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<uc1:TemplateHeaderForm id="HeaderDetail" runat="server"></uc1:TemplateHeaderForm>
			<br>
		</TD>
	</TR>
	</TR>
	<tr>
		<td align="center">
			<uc1:ToolBar id="QSPToolBar" runat="server"></uc1:ToolBar>
		</td>
	</tr>
</TABLE>
