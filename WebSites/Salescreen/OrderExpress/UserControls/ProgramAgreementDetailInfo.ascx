<%@ Register TagPrefix="uc3" TagName="ToolBar" Src="~/UserControls/ToolBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProgramAgreementInfo" Src="ProgramAgreementInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AuditControlInfo" Src="AuditControlInfo.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramAgreementDetailInfo" Codebehind="ProgramAgreementDetailInfo.ascx.cs" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<uc1:ProgramAgreementInfo id="ProgramAgreementInfo1" runat="server"></uc1:ProgramAgreementInfo>
			<br>
		</TD>
	</TR>
	<TR>
		<TD align="center">
			<uc1:AuditControlInfo id="AuditControlInfo1" HideHistoryLink=False runat="server"></uc1:AuditControlInfo>
			
		</TD>
	</TR>
	<TR>
		<td align="center">
			<uc3:ToolBar id="QSPToolBar" runat="server"></uc3:ToolBar>
		</td>
	</TR>
</TABLE>
