<%@ Control Language="vb" AutoEventWireup="false" Codebehind="attributechoice.ascx.vb" Inherits="StoreFront.StoreFront.attributechoice" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="ContentTableHeader" width="100%">&nbsp;<asp:label id="lblTitle" CssClass="ContentTableHeader" Runat="server">&nbsp;Templates</asp:label>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center">
				<TR>
					<td class="content" colSpan="5">&nbsp;</td>
				</TR>
				<TR>
					<td class="content" colSpan="5">
						<TABLE cellSpacing="1" cellPadding="2" width="100%" align="center">
							<TR>
								<TD class="content" align="left" nowrap>&nbsp;Attribute Template:</TD>
								<TD class="content" align="left">&nbsp;<asp:dropdownlist id="DDTemplates" runat="server" DataValueField="id" DataTextField="name" AutoPostBack="True"></asp:dropdownlist></TD>
								<TD class="content" align="left" width="100%">&nbsp;
									<asp:LinkButton ID="cmdApply" Runat="server">
										<asp:Image BorderWidth="0" ID="imgApply" runat="server" ImageUrl="../images/apply.jpg" AlternateText="Apply"></asp:Image>
									</asp:LinkButton>
								</TD>
							</TR>
						</TABLE>
					</td>
				</TR>
			</table>
		</td>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<TR>
		<TD class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></TD>
	</TR>
</table>
