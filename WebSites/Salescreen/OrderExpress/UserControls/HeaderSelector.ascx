<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.HeaderSelector" Codebehind="HeaderSelector.ascx.cs" %>
<asp:Table WIDTH="100%" Runat="server"  id="tblHeader" CellPadding="0" CellSpacing="0" BackImageUrl="~/images/header_Pattern_Selector.gif">
	<asp:TableRow>
		<asp:TableCell>
			<TABLE WIDTH="100%" BORDER="0" CELLPADDING="0" CELLSPACING="0">
				<TR>
					<TD>
						<TABLE BORDER="0" CELLPADDING="0" CELLSPACING="0">
							<TR>
								<TD>
									<asp:image ImageAlign="Top" Runat="server" ImageUrl="~/images/Header_Left_Selector.gif" ID="Image1"></asp:image></TD>
								<TD>
									<asp:image ImageAlign="Top" Runat="server" ImageUrl="~/images/Header_Middle1_Selector.gif" ID="Image2"></asp:image></TD>
								<TD>
									<asp:image ImageAlign="Top" Runat="server" ImageUrl="~/images/Header_Middle2_Selector.gif" ID="Image3"></asp:image></TD>
							</TR>
						</TABLE>
					</TD>
					<TD>
						<asp:image ImageAlign="Top" Runat="server" ImageUrl="~/images/Header_Right_Selector.gif" ID="Image4"></asp:image>
					</TD>
				</TR>
				<TR>
					<TD COLSPAN="2" height="10px" align="right" valign="top" background="images/MenuBar_Pattern_Selector.gif">
						&nbsp;<br>
					</TD>
				</TR>
			</TABLE>
		</asp:TableCell>
	</asp:TableRow>
</asp:Table>
