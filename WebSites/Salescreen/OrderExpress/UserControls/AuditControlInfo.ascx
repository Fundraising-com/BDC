<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.AuditControlInfo" Codebehind="AuditControlInfo.ascx.cs" %>

<table id="tblFormDetailTitle" cellSpacing="3" cellPadding="0" width="650" border="0">
    <tr align="left">
		<td class="SectionPageTitleInfo" colspan=2><asp:label id="lblTitleAccountInfo" runat="server">
				Audit Information
			</asp:label></td>
	</tr>
	<tr>
		<td>
			<TABLE cellSpacing="0" cellPadding="0" border="0" width=325>
				<TR >
					<TD>
						<asp:label id="lblLabelCreateName" CssClass="StandardLabel" runat="server">
						    Created&nbsp;By:&nbsp;
						</asp:label>
					</TD>
					<TD>
						<asp:Label id="lblCreateName" runat="server" CssClass="DescInfoLabel"></asp:Label>
					</TD>
				</TR>
				<TR >
					<TD>
						<asp:label id="Label1" CssClass="StandardLabel" runat="server">Created&nbsp;At:&nbsp;</asp:label></TD>
					<TD>
						<asp:Label id="lblCreateDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
					</TD>
				</TR>
				
			</TABLE>
		</td>
		<td>
			<TABLE cellSpacing="0" cellPadding="0" border="0" width=325>
				<TR >
					<TD>
						<asp:label id="lblLabelUpdateName" CssClass="StandardLabel" runat="server">
						    Updated&nbsp;By:&nbsp;
						</asp:label>
					</TD>
					<TD>
						<asp:Label id="lblUpdateName" runat="server" CssClass="DescInfoLabel"></asp:Label>
					</TD>
				</TR>
				<TR >
					<TD>
						<asp:label id="Label4" CssClass="StandardLabel" runat="server">Updated&nbsp;At:&nbsp;</asp:label></TD>
					<TD>
						<asp:Label id="lblUpdateDate" runat="server" CssClass="DescInfoLabel"></asp:Label>
					</TD>
				</TR>				
				
			</TABLE>
		</td>
	</tr>
	<tr>
		<td align="center" colspan=2>
            &nbsp;<asp:ImageButton ID="imgBtnViewHistory" runat="server" ImageUrl="~/images/btnViewHistory.gif" /></td>
	</tr>
</table>
