<%@ Control Language="vb"  AutoEventWireup="false" Codebehind="CGiftWrapControl.ascx.vb" Inherits="StoreFront.StoreFront.CGiftWrapControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="Content" align="left" width="100%">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="Instructions">
				<tr>
					<td class="ContentTableHeader" align="left" nowrap>
						<asp:Label CssClass="ContentTableHeader" id="lblId" runat="server">Product ID/Name</asp:Label>
					</td>
					<td class="ContentTableHeader" align="right" nowrap>
						<asp:Label CssClass="ContentTableHeader" id="lblPrice" runat="server">GiftWrap Price</asp:Label>
					</td>
				</tr>
				<tr>
					<td class="content" colspan="2">
						<asp:datalist id="dlGW" CellPadding="0" BorderColor="Black" BorderWidth="0px" Width="100%" runat="server" ShowHeader="False" ShowFooter="False">
							<ItemStyle Wrap="False" VerticalAlign="Top"></ItemStyle>
							<ItemTemplate>
								<table cellSpacing="0" cellPadding="0" width="100%">
									<tr>
										<td class="Content" colSpan="5"><IMG height="5" src="images/clear.gif"></td>
									</tr>
									<tr>
										<td class="Content" colSpan="5"><IMG height="5" src="images/clear.gif"></td>
									</tr>
									<TR>
										<TD class="Content" noWrap align="left">
											<asp:CheckBox id="chkGW" runat="server" Text="GiftWrap" Checked='<%#DataBinder.Eval(Container.DataItem,"IsChecked")%>'>
											</asp:CheckBox></TD>
										<TD class="Content" noWrap align="left" colSpan="2">&nbsp;&nbsp;&nbsp;Gift Message</TD>
									</TR>
									<TR>
										<TD></TD>
										<TD class="Content" noWrap align="right">To:&nbsp;
										</TD>
										<TD class="Content" noWrap align="left">
											<asp:TextBox CssClass=content id=txtTo runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MessageTo")%>' MaxLength=50>
											</asp:TextBox></TD>
										<TD class="Content" noWrap align="right">From:&nbsp;
										</TD>
										<TD class="Content" noWrap align="left">
											<asp:TextBox CssClass=content id=txtFrom runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"MessageFrom")%>' MaxLength=50>
											</asp:TextBox>&nbsp;&nbsp;</TD>
									</TR>
									<TR>
										<TD noWrap></TD>
										<TD class="Content" noWrap align="right">Message:&nbsp;
										</TD>
										<TD class="Content" noWrap align="left" colSpan="4">
											<asp:TextBox CssClass=content id=txtMessage runat="server" Width=90% MaxLength =100 Text='<%#DataBinder.Eval(Container.DataItem,"Message")%>'>
											</asp:TextBox>&nbsp;&nbsp;</TD>
									</TR>
									<tr>
										<td class="Content" colSpan="5"><IMG height="5" src="images/clear.gif"></td>
									</tr>
									<tr>
										<td class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
									</tr>
								</table>
							</ItemTemplate>
						</asp:datalist>
					</td>
				</tr>
			</table>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<td class="Content" colSpan="5"><IMG height="5" src="images/clear.gif"></td>
	</tr>
	<tr>
		<TD class="Content" align="right" colspan="2">
			<asp:LinkButton ID="cmdContinue" Runat="server">
				<asp:Image BorderWidth="0" ID="imgContinue" Runat="server" AlternateText="Continue"></asp:Image>
			</asp:LinkButton>
			&nbsp;
			<asp:LinkButton ID="cmdCancel" Runat="server">
				<asp:Image BorderWidth="0" ID="imgCancel" Runat="server" AlternateText="Cancel"></asp:Image>
			</asp:LinkButton>
		</TD>
	</tr>
</table>
