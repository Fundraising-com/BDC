<%@ Reference Control="LogoDetailInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LogoHeaderForm" Src="LogoHeaderForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.LogoDetail" Codebehind="LogoDetail.ascx.cs" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
    <tr>
        <td>
            &nbsp;</td>
    </tr>
	<TR>
		<TD>
			<uc1:LogoHeaderForm id="ctrlLogoHeaderForm" runat="server"></uc1:LogoHeaderForm><br>
		</TD>
	</TR>
	<tr>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0" width="400" id="Table1">
				<tr>
					<td align="center">
						<asp:ImageButton id="imgBtnDelete" runat="server" CausesValidation="False" ImageUrl="~/images/btnDelete.gif"
							AlternateText="Delete" Visible="False"></asp:ImageButton>
					</td>
					<td align="center">
						<asp:ImageButton id="imgBtnSave" runat="server" ImageUrl="~/images/btnSave.gif" AlternateText="Save"></asp:ImageButton>
					</td>
					<td align="center">
						<asp:HyperLink id="hypLnkCancel" runat="server" ImageUrl="~/images/btnCancel.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
