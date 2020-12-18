<%@ Control Language="vb" AutoEventWireup="false" Codebehind="AffiliatePaymentlistCtrl.ascx.vb" Inherits="StoreFront.StoreFront.AffiliatePaymentlistCtrl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="AddressLabel" Src="../../Controls/AddressLabel.ascx" %>
<P id="ErrorAlignment" runat="server"><asp:label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></P>
<P id="MessageAlignment" runat="server"><asp:label id="Message" runat="server" CssClass="Messages" Visible="False"></asp:label></P>
<asp:datalist id="ddAffiliates" Width="100%" Runat="server">
	<ItemTemplate>
		<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
			<TR>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;Payments For 
					Affiliate &nbsp;<asp:Label ID="affName" Runat="server" CssClass="ContentTableHeader">
						<%# DataBinder.Eval(Container.DataItem,"Name") %>
					</asp:Label>
					<asp:Label ID="lblId" Visible="False" Runat="server" CssClass="ContentTableHeader">
						<%# DataBinder.Eval(Container.DataItem,"ID") %>
					</asp:Label></TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="Content" colSpan="3">
					<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0" runat="server">
						<tr>
							<td class="content" colspan="2">&nbsp;</td>
						</tr>
						<tr>
							<td class="content" width="50%" rowspan="2">
								<uc1:AddressLabel id="Addresslabel1" runat="server"></uc1:AddressLabel></td>
							<td class="content" width="50%" nowrap align="left" valign="middle">&nbsp;Commission 
								Percent:<%# DataBinder.Eval(Container.DataItem,"PayOut") %>&nbsp;&nbsp;&nbsp;&nbsp;</td>
						</tr>
						<tr>
							<td class="content" width="50%" nowrap align="left" valign="top">&nbsp;Commission 
								Flat Fee:<%# format(DataBinder.Eval(Container.DataItem,"MinumimPayOut"),"c") %>&nbsp;&nbsp;&nbsp;&nbsp;</td>
						</tr>
						<tr>
							<td class="content" width="50%">&nbsp;
								<asp:LinkButton ID="cmdEdit" Runat="server" OnClick=Edit CommandArgument= '<%# DataBinder.Eval(Container.DataItem,"ID") %>'>
									<asp:Image BorderWidth="0" ID="imgEdit" runat="server" ImageUrl="../images/edit.jpg" AlternateText="Edit"></asp:Image>
								</asp:LinkButton>
							</td>
							<td class="content" width="50%">&nbsp;</td>
						</tr>
						<tr>
							<td class="content" colspan="2">&nbsp;</td>
						</tr>
					</TABLE>
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR>
				<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
			<tr>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="Content" width="50%">
					<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
						<tr>
							<TD class="content" colspan="3">&nbsp;</TD>
						</tr>
						<tr>
							<TD class="Headings" noWrap align="left" colspan="3">&nbsp;Payment History
							</TD>
						</tr>
						<tr>
							<TD class="content" colspan="3">&nbsp;</TD>
						</tr>
						<tr>
							<TD class="content">
								<asp:DataList ID="ddPayments" Runat="server" Width="100%">
									<ItemTemplate>
										<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
											<tr>
												<TD class="content">
													<asp:Label ID="lblDate" Runat="server" CssClass="content">
														<%# DataBinder.Eval(Container.DataItem,"PaymentDate") %>
													</asp:Label>
												</TD>
												<TD class="content" noWrap>&nbsp;
													<asp:Label ID="lblPayment" Runat="server" CssClass="content">
														<%# format(DataBinder.Eval(Container.DataItem,"Amount"),"c") %>
													</asp:Label>
												</TD>
												<TD class="content" noWrap>
													<asp:LinkButton ID="cmdVoid" Runat="server" OnClick =Void CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'>
														<asp:Image BorderWidth="0" ID="imgVoid" runat="server" ImageUrl="../images/void.jpg" AlternateText="Void"></asp:Image>
													</asp:LinkButton>
												</TD>
											</tr>
											<tr>
												<TD class="content" colspan="3">&nbsp;</TD>
											</tr>
										</TABLE>
									</ItemTemplate>
								</asp:DataList>
							</TD>
						</tr>
					</TABLE>
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				<TD class="Content" width="50%">
					<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
						<tr>
							<TD class="content" colspan="2">&nbsp;</TD>
						</tr>
						<tr>
							<TD class="Headings" noWrap align="left" colspan="2">&nbsp;Pending Payment
							</TD>
						</tr>
						<tr>
							<TD class="content" colspan="2">&nbsp;</TD>
						</tr>
						<tr>
							<TD class="content">&nbsp;
								<asp:Label ID="txtPayment" Runat="server" CssClass="content" Text='<%# format(DataBinder.Eval(Container.DataItem,"CurrentEarnings"),"c")%>'>
								</asp:Label></TD>
							<TD class="content">&nbsp;
								<asp:LinkButton ID="cmdPay" Runat="server" CommandArgument= '<%# DataBinder.Eval(Container.DataItem,"ID") %>' OnClick=Pay>
									<asp:Image BorderWidth="0" ID="imgPay" runat="server" ImageUrl="../images/pay.jpg" AlternateText="Pay"></asp:Image>
								</asp:LinkButton>
							</TD>
						</tr>
						<tr>
							<TD class="content" noWrap>
								&nbsp;</TD>
						</tr>
					</TABLE>
				</TD>
				<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			</tr>
			<TR>
				<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
			</TR>
		</TABLE>
	</ItemTemplate>
</asp:datalist>
