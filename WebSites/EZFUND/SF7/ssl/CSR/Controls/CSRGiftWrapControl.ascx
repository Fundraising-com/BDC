<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRGiftWrapControl.ascx.vb" Inherits="StoreFront.StoreFront.CSRGiftWrapControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<td class="Content" align="left" width="100%">
			<table class="Instructions" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="ContentTableHeader" noWrap align="left"><asp:label id="lblId" runat="server" CssClass="ContentTableHeader">Product ID/Name</asp:label></td>
					<td class="ContentTableHeader" noWrap align="right"><asp:label id="lblPrice" runat="server" CssClass="ContentTableHeader">GiftWrap Price</asp:label></td>
				</tr>
				<tr>
					<td class="content" colSpan="2"><asp:datalist id="dlGW" runat="server" ShowFooter="False" ShowHeader="False" Width="100%" BorderWidth="0px"
							BorderColor="Black" CellPadding="0">
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
										<TD class="Content" noWrap align="right" valign="top">Message:&nbsp;
										</TD>
										<TD class="Content" noWrap align="left" colSpan="4">
											<asp:TextBox CssClass=content TextMode=MultiLine Rows=8 columns=48 id=txtMessage runat="server" MaxLength =100 Text='<%#DataBinder.Eval(Container.DataItem,"Message")%>'>
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
						</asp:datalist></td>
				</tr>
			</table>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<td class="Content" colSpan="5"><IMG height="5" src="images/clear.gif"></td>
	</tr>
	<tr>
		<TD class="Content" align="right" colSpan="2"><asp:linkbutton id="cmdContinue" Runat="server">
				<asp:Image ImageUrl="../images/Continue.gif" BorderWidth="0" ID="imgContinue" Runat="server"
					AlternateText="Continue"></asp:Image>
			</asp:linkbutton>&nbsp;
			<asp:linkbutton id="cmdCancel" Runat="server">
				<asp:Image ImageUrl="../images/Cancel.gif" BorderWidth="0" ID="imgCancel" Runat="server" AlternateText="Cancel"></asp:Image>
			</asp:linkbutton></TD>
	</tr>
</table>
