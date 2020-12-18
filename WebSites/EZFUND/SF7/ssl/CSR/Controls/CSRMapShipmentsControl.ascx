<%@ Register TagPrefix="uc1" TagName="CSRAddressLabel" Src="../controls/CSRAddressLabel.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CSRMapShipmentsControl.ascx.vb" Inherits="StoreFront.StoreFront.CSRMapShipmentsControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TBODY>
		<TR>
			<TD class="Headings">Ship To Multiple Addresses</TD>
			<td class="Content" align="right">
				<asp:HyperLink ID="lnkManage" Runat="server" NavigateUrl="../CSRManageAddresses.aspx" Target="_top">
					<asp:Image BorderWidth="0" ID="imgManage" ImageUrl="../images/manage_addresses.jpg" Runat="server"
						AlternateText="Manage Addresses"></asp:Image>
				</asp:HyperLink>
			</td>
		</TR>
		<TR>
			<TD class="Content" colSpan="2">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Content" colSpan="2">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
						<td class="ContentTableHeader">&nbsp;</td>
						<td class="ContentTableHeader">Item</td>
						<td class="ContentTableHeader">Ship To</td>
						<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
					</tr>
					<asp:Repeater id="Items" runat="server">
						<ItemTemplate>
							<tr>
								<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
								<td class="Content">&nbsp;</td>
								<td class="Content" valign="top">
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td class="Content">&nbsp;</td>
										</tr>
										<tr>
											<td class="Content" nowrap><%# DataBinder.Eval(Container.DataItem,"Name") %></td>
										</tr>
										<tr>
											<td class="Content" nowrap>
												<asp:DataList ID="dlAttributes" Runat="server">
													<ItemTemplate>
														<table border="0" cellpadding="0" cellspacing="0" width="100%">
															<tr>
																<td class="Content" nowrap>&nbsp;&nbsp;&nbsp;
																	<asp:Label ID="AttName" Runat="server" CssClass="content">
																		<%# DataBinder.Eval(Container.DataItem,"Name") %>
																	</asp:Label>&nbsp;</td>
																<td class="Content" align="left" width="100%" nowrap>
																	<asp:Label ID="AttDetail" Runat="server" CssClass="content"></asp:Label></td>
															</tr>
														</table>
													</ItemTemplate>
												</asp:DataList>
											</td>
										</tr>
										<tr>
											<td class="Content">&nbsp;</td>
										</tr>
										<tr>
											<td class="Content">
												<table runat="server" id="GiftWrapTable" border="0" cellpadding="0" cellspacing="0" width="100%">
													<tr>
														<td class="Content">Gift Wrap</td>
													</tr>
													<tr>
														<td class="Content">&nbsp;</td>
													</tr>
													<tr>
														<td class="Content">
															To:
															<asp:Label Runat="server" ID="MessageTo"></asp:Label>
															From:
															<asp:Label Runat="server" ID="MessageFrom"></asp:Label>
														</td>
													</tr>
													<tr>
														<td class="Content">
															<asp:Label Runat="server" ID="Message"></asp:Label></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
								<td class="Content">
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td class="Content">&nbsp;</td>
										</tr>
										<tr>
											<td class="Content" nowrap>Select From Address Book:&nbsp;&nbsp;
												<asp:HyperLink ID="btnNewManage" Runat="server" NavigateUrl="../CSRManageAddresses.aspx" Target="_top">
													<asp:Image BorderWidth="0" ID="imgNewManage" Runat="server" AlternateText="Add Address" ImageUrl="../images/add_Addresses.jpg"></asp:Image>
												</asp:HyperLink>
												<asp:DropDownList EnableViewState="True" id="Dropdownlist2" DataTextField="NickName" DataValueField="ID"
													runat="server" AutoPostBack="True"></asp:DropDownList></td>
										</tr>
										<tr>
											<td class="Content">&nbsp;</td>
										</tr>
										<tr>
											<td class="Content">
												<uc1:CSRAddressLabel id="Addresslabel2" runat="server"></uc1:CSRAddressLabel></td>
										</tr>
									</table>
								</td>
								<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
							</tr>
							<tr>
								<td class="ContentTableHorizontal" height="1" colspan="5"><IMG src="images/clear.gif" height="1"></td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</table>
			</TD>
		</TR>
		<TR>
			<TD class="Content" colSpan="2">&nbsp;</TD>
		</TR>
		<TR>
			<TD align="right" colSpan="2">
				<asp:LinkButton ID="btnContinue" Runat="server">
					<asp:Image BorderWidth="0" ID="imgContinue" Runat="server" AlternateText="Continue" ImageUrl="../images/continue.jpg"></asp:Image>
				</asp:LinkButton>
			</TD>
		</TR>
		<TR>
			<TD class="Content" colSpan="2">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Headings">Saved Addresses</TD>
			<td class="Content" align="right">
			</td>
		</TR>
		<TR>
			<TD class="Content" colSpan="2">&nbsp;</TD>
		</TR>
		<TR>
			<TD align="left" class="Content" valign="top" colSpan="2">
				<asp:datalist RepeatColumns="3" Width="30%" RepeatDirection="Horizontal" ID="SavedAddresses" Runat="server">
					<ItemTemplate>
						<table height="100%" class="Content">
							<tr>
								<td valign="top" class="Content">
									<asp:Label cssclass="Headings" Runat="server" ID="NickName">
										<%#  DataBinder.Eval(Container.DataItem,"NickName") %>
									</asp:Label><br>
									<uc1:CSRAddressLabel id="Csraddresslabel2" runat="server"></uc1:CSRAddressLabel>
								</td>
							</tr>
						</table>
					</ItemTemplate>
				</asp:datalist>
			</TD>
		</TR>
	</TBODY>
</TABLE>
