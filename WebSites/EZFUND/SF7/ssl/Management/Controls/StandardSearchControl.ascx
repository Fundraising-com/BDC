<%@ Control Language="vb" AutoEventWireup="false" Codebehind="StandardSearchControl.ascx.vb" Inherits="StoreFront.StoreFront.StandardSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table border="0" cellpadding="0" cellspacing="0" id="tblSearch" runat="server" width="100%">
	<tr>
		<td id="AlphaCell" class="Content" align="left" width="100%"></td>
	</tr>
	<tr>
		<td class="Content" align="left" width="100%">
			<asp:Label id="lblSearch" runat="server">Search: </asp:Label>
			<asp:TextBox id="txtKeyword" runat="server"></asp:TextBox>
			<asp:LinkButton ID="btnGo" Runat="server" OnClick="GoSearch" TabIndex="1">
				<asp:Image BorderWidth="0" ID="imgGo" runat="server" ImageUrl="../images/icon_go.gif" AlternateText="Go"></asp:Image>
			</asp:LinkButton>
			<br>
		</td>
	</tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" id="searchTable">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="1">&nbsp;</TD>
		<td class="ContentTableHeader" align="left" width="100%" nowrap>
			<asp:Label id="lblTitle" runat="server"></asp:Label></td>
		<TD class="ContentTableHeader" width="1">&nbsp;</TD>
		<td class="ContentTableHeader" align="right" nowrap id="SortCell"><asp:Label id="lblSortBy" runat="server">Sort By:&nbsp;</asp:Label></td>
		<TD class="ContentTableHeader" width="1">&nbsp;</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<td colspan="7">
			<asp:DataGrid id="DataGrid2" runat="server" ShowHeader="False" AutoGenerateColumns="False" AllowPaging="True"
				Width="100%" ItemStyle-Width="100%" BorderWidth="0px" CellPadding="0" GridLines="None">
				<ItemStyle Width="100%"></ItemStyle>
				<PagerStyle HorizontalAlign="Right" CssClass="ContentTableHeader" Mode="NumericPages"></PagerStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<table border="0" cellpadding="0" cellspacing="0" width="100%">
								<tr>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="Content" width="1" colspan="3"><IMG src="images/clear.gif" height="5"></TD>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
								</tr>
								<tr>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="Content" width="1">&nbsp;</TD>
									<td class="Content" width="100%">
										<table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" id="Table1">
											<tr id="trRow">
												<td class="Content" align="center" width="2%" id="chkCell">
													<asp:CheckBox Runat="server" ID="chk" AutoPostBack="True" OnCheckedChanged="CheckChange"></asp:CheckBox></td>
												<td>
													<table border="0" cellpadding="0" cellspacing="0" width="100%" id="tblInner" runat="server">
														<tr id="trInner">
														</tr>
													</table>
												</td>
												<td class="Content" align="right" width="20%" id="btnCell">
													<asp:TextBox ID="txtID" Runat="server" Visible="False"></asp:TextBox>
													<asp:LinkButton ID="btnEdit" Runat="server" OnClick=EditItem CommandName='<%# Container.ItemIndex %>'>
														<asp:Image BorderWidth="0" ID="imgEdit" runat="server" ImageUrl="../images/edit.jpg" AlternateText="Edit"></asp:Image>
													</asp:LinkButton>
													&nbsp;
													<asp:LinkButton ID="btnDelete" Runat="server" OnClick=DeleteItem CommandName='<%# Container.ItemIndex %>' CausesValidation=False>
														<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="../images/delete.jpg" AlternateText="Delete"></asp:Image>
													</asp:LinkButton>
												</td>
											</tr>
										</table>
									</td>
									<TD class="Content" width="1">&nbsp;</TD>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
								</tr>
								<tr>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
									<TD class="Content" width="1" colspan="3"><IMG src="images/clear.gif" height="5"></TD>
									<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
								</tr>
								<tr id="Bar">
									<TD class="ContentTable" width="1" colspan="5"><IMG src="images/clear.gif" width="1"></TD>
								</tr>
							</table>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</td>
	</tr>
</table>
