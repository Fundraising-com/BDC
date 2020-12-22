<%@ Register TagPrefix="uc1" TagName="OrganizationHeaderDetailForm" Src="OrganizationHeaderDetailForm.ascx" %>
<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.OrganizationDetail" Codebehind="OrganizationDetail.ascx.cs" %>

<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<uc1:OrganizationHeaderDetailForm id="HeaderDetail" runat="server"></uc1:OrganizationHeaderDetailForm>
			<br>
		</TD>
	</TR>
	</tr>
	<tr>
		<td align="center">
			<table border="0" cellpadding="0" cellspacing="0" width="400">
				<tr>
					<td>
						<asp:ImageButton id="imgBtnDelete" runat="server" CausesValidation="False" ImageUrl="~/images/btnDelete.gif"
							AlternateText="Delete"></asp:ImageButton>
					</td>
					<td>
						<asp:ImageButton id="imgBtnSave" runat="server" CausesValidation="False" ImageUrl="~/images/btnSave.gif"
							AlternateText="Save"></asp:ImageButton>
					</td>
					<td>
						<asp:ImageButton id="imgBtnCancel" runat="server" CausesValidation="False" ImageUrl="~/images/btnCancel.gif"
							AlternateText="Cancel"></asp:ImageButton>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</TABLE>
