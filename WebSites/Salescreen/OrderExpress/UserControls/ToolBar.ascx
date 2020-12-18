<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ToolBar" Codebehind="ToolBar.ascx.cs" %>
<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
	<TR>
		<TD>
			<HR width="100%" SIZE="2">
		</TD>
	</TR>
	<TR id="trReadOnlyMode" runat="Server" >
		<TD align="center">
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td align="center">
						<asp:ImageButton id="imgBtnEdit" runat="server" CausesValidation="False" ImageUrl="~/images/BtnEdit.gif"
							AlternateText="Edit"></asp:ImageButton>
					</td>
					<td>
						&nbsp;&nbsp;
					</td>
					<td align="center">
						<asp:HyperLink id="hypLnkClose" runat="server" ImageUrl="~/images/btnClose.gif" NavigateUrl="javascript:window.close();">Close</asp:HyperLink>
					</td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR id="trEditMode" runat="Server" >
		<TD align="center">
			<table border="0" cellpadding="0" cellspacing="0" width="400">
				<tr>
					<td align="center">
						<asp:ImageButton id="imgBtnDelete" runat="server" CausesValidation="False" ImageUrl="~/images/BtnDelete.gif"
							AlternateText="Delete" ></asp:ImageButton>
					</td>
					<td align="center">
						<asp:ImageButton id="imgBtnSave" runat="server" CausesValidation="False" ImageUrl="~/images/btnSave.gif" AlternateText="Save"></asp:ImageButton>
					</td>
					<td align="center">
						<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/BtnCancel.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR>
		<TD>
			<HR width="100%" SIZE="2">
		</TD>
	</TR>
</TABLE>
