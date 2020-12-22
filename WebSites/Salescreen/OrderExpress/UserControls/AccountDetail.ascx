<%@ Reference Control= "ToolBar.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountDetail" Codebehind="AccountDetail.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="AccountHeaderDetailForm" Src= "AccountHeaderDetailForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ToolBar" Src="ToolBar.ascx" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<uc1:AccountHeaderDetailForm id="HeaderDetail" runat="server"></uc1:AccountHeaderDetailForm>
			<br>
		</TD>
	</TR>
	<tr>
		<td align="center">
			<uc1:ToolBar id=QSPToolBar runat="server"></uc1:ToolBar>
		</td>
	</tr>
</TABLE>
