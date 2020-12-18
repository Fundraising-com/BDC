<%@ Register TagPrefix="uc3" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrderSubList" Src="OrderSubList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccountInfo" Src="AccountInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AuditControlInfo" Src="AuditControlInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AccountDetailInfo" Codebehind="AccountDetailInfo.ascx.cs" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<uc1:AccountInfo id="AccountInfo1" runat="server"></uc1:AccountInfo>
			<br>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<uc1:OrderSubList id="OrderSubList1" runat="server"></uc1:OrderSubList>
			<br>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<uc1:AuditControlInfo HideHistoryLink=false id="AuditControlInfo1" runat="server"></uc1:AuditControlInfo>
			
		</TD>
	</TR>
	<TR>
		<td align="center">
			<uc3:ToolBar id="QSPToolBar" runat="server"></uc3:ToolBar>
		</td>
	</TR>
</TABLE>
