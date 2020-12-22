<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.Header" Codebehind="Header.ascx.cs" %>
<asp:Table WIDTH="100%" Runat="server" id="tblHeader" CellPadding="0" CellSpacing="0" BackImageUrl="~/images/header_Pattern.gif">
	<asp:TableRow>
		<asp:TableCell>
			<TABLE WIDTH="100%" BORDER="0" CELLPADDING="0" CELLSPACING="0">
				<TR>
					<TD>
						<TABLE BORDER="0" CELLPADDING="0" CELLSPACING="0">
							<TR>
								<TD>
									<asp:image ImageAlign="Top" Runat="server" ImageUrl="~/images/Header_Left.gif"></asp:image></TD>
								<TD>
									<asp:image ImageAlign="Top" Runat="server" ImageUrl="~/images/Header_Middle1.gif"></asp:image></TD>
								<TD>
									<asp:image ImageAlign="Top" Runat="server" ImageUrl="~/images/Header_Middle2.gif"></asp:image></TD>
							</TR>
						</TABLE>
					</TD>
					<TD>
						<asp:image ImageAlign="Top" Runat="server" ImageUrl="~/images/Header_Right.gif" ID="Image1"></asp:image>
					</TD>
				</TR>
				<TR>
					<TD COLSPAN="2" height=20px align="right" valign="top" background="images/MenuBar_Pattern.gif">
						&nbsp;<br>
					</TD>
				</TR>
			</TABLE>
		</asp:TableCell>
	</asp:TableRow>
</asp:Table>
