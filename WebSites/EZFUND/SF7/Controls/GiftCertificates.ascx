<%@ Control Language="vb" AutoEventWireup="false" Codebehind="GiftCertificates.ascx.vb" Inherits="StoreFront.StoreFront.GiftCertificates" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="ContentTableHeader">&nbsp;</TD>
		<TD class="ContentTableHeader" noWrap colSpan="5">Gift Certificates</TD>
		<TD class="ContentTableHeader">&nbsp;</TD>
		<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content" colSpan="3">&nbsp;</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content" colSpan="3">&nbsp;</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<td vAlign="top" width="50%">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD class="subHeadings" noWrap>Apply&nbsp;a Gift Certificate:</TD>
				</tr>
				<tr>
					<td class="Content" noWrap>Gift Certificate Code:&nbsp;<asp:textbox id="txtGiftCertificateCode" Runat="server" MaxLength="100"></asp:textbox></td>
				</tr>
				<tr>
					<td class="Content" noWrap align="right"><asp:linkbutton id="btnApply" Runat="server">
							<asp:Image BorderWidth="0" ID="imgApply" Runat="server" AlternateText="Apply"></asp:Image>
						</asp:linkbutton></td>
				</tr>
			</table>
		</td>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content">&nbsp;</TD>
		<td vAlign="top" width="50%">
			<table id="GiftsApplied" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<TD class="subHeadings" noWrap>Gift Certificates Applied</TD>
				</tr>
				<tr>
					<TD class="Content" noWrap>&nbsp;</TD>
				</tr>
				<tr>
					<td><asp:datalist id="GiftCertificateTable" runat="server" Width="100%">
							<ItemTemplate>
								<table runat="server" id="Table4" border="0" cellpadding="0" cellspacing="0" width="100%">
									<tr>
										<td width="100%" class="Content" align="left">
											<%# DataBinder.Eval(Container.DataItem,"Code") %>
											(<%# PriceDisplay2(DataBinder.Eval(Container.DataItem,"DollarOff")) %>)
										</td>
										<td class="Content" align="right">
											<asp:LinkButton ID="GiftCertificateRemove" Runat="server" OnClick="RemoveGiftCertificate" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'>
												<asp:Image BorderWidth="0" ID="imgGiftCertificateRemove" Runat="server" AlternateText="Remove"></asp:Image>
											</asp:LinkButton>
										</td>
									</tr>
									<tr>
										<td colspan="2" class="Content">
											<%# PriceDisplay2(DataBinder.Eval(Container.DataItem,"AmountUsed"))%>
											Applied To Order,
											<%# PriceDisplay2(DataBinder.Eval(Container.DataItem,"DollarOff") - DataBinder.Eval(Container.DataItem,"AmountUsed")) %>
											Remaining
										</td>
									</tr>
								</table>
							</ItemTemplate>
						</asp:datalist></td>
				</tr>
			</table>
		</td>
		<TD class="Content">&nbsp;</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content" colSpan="3">&nbsp;</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
		<TD class="Content" colSpan="3">&nbsp;</TD>
		<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
	</TR>
	<TR>
		<td class="ContentTable" colSpan="9" height="1"><IMG height="1" src="images/clear.gif"></td>
	</TR>
</TABLE>
