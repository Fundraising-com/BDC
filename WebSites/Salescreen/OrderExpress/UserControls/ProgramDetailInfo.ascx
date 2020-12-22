<%@ Reference Control="~/UserControls/ToolBar.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramDetailInfo" Codebehind="ProgramDetailInfo.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProgramInfo" Src="ProgramInfo.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<td>
			<uc1:ProgramInfo id="ctrlProgramInfo" runat="server" />
			<br>
		</td>
	</TR>
	<tr>
		<td align="center">
			<uc1:ToolBar id="QSPToolBar" runat="server"></uc1:ToolBar>
		</td>
	</tr>
</TABLE>
