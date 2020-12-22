<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.FAQ_Displayer" Codebehind="FAQ_Displayer.ascx.cs" %>
<TABLE cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD>
			<TABLE WIDTH="100%" BORDER="0" CELLPADDING="0" CELLSPACING="0" background="images/InfoTab_TopMiddle.gif">
				<TR>
					<TD>
						<asp:image Runat="server" ImageUrl="~/images/InfoTab_TopLeft.gif" WIDTH="10" HEIGHT="20" id="Image1"></asp:image></TD>
					<TD align="left" WIDTH="100%">
						<IMG SRC="images/FAQ_Title.gif" HEIGHT="20"></TD>
					<TD>
						<asp:image Runat="server" ImageUrl="~/images/InfoTab_TopRight.gif" WIDTH="10" HEIGHT="20" id="Image2"></asp:image></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD bgColor="#fdf6ee" width="100%">
			<table cellPadding="2" width="100%" height="120">
				<tr>
					<td>
						<asp:datalist id="dtLst" runat="server">
							<ItemTemplate>
								<asp:HyperLink id=HypLnk runat="server" Font-Names="Verdana" Font-Size="XX-Small" Text='<%# DataBinder.Eval(Container, "DataItem.FAQ") %>' NavigateUrl="javascript: void(0);">
								</asp:HyperLink>
							</ItemTemplate>
						</asp:datalist>
					</td>
				</tr>
			</table>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE WIDTH="100%" BORDER="0" CELLPADDING="0" CELLSPACING="0" background="images/InfoTab_BottomMiddle.gif">
				<TR>
					<TD>
						<asp:image Runat="server" ImageUrl="~/images/InfoTab_BottomLeft.gif" WIDTH="10" HEIGHT="20" id="Image3"></asp:image></TD>
					<TD align="left" WIDTH="100%">
						<asp:image Runat="server" WIDTH="150px" ImageUrl="~/images/InfoTab_BottomMiddle.gif" HEIGHT="20"
							id="Image5"></asp:image></TD>
					<TD>
						<asp:image Runat="server" ImageUrl="~/images/InfoTab_BottomRight.gif" WIDTH="10" HEIGHT="20"
							id="Image4"></asp:image></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
