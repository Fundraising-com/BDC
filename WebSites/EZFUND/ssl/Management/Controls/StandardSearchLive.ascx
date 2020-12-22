<%@ Control Language="vb" AutoEventWireup="false" Codebehind="StandardSearchLive.ascx.vb" Inherits="StoreFront.StoreFront.StandardSearchLive" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table id="tblSearch" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<tr>
		<td class="Content" id="AlphaCell" noWrap align="left" width="100%"><asp:repeater id="rpAlpha" runat="server">
				<SeparatorTemplate>
					|
				</SeparatorTemplate>
				<ItemTemplate>
					<asp:LinkButton Runat="server" OnClick =AlphaSearch CommandArgument ='<%#DataBinder.Eval(Container.DataItem,"sAlpha")%>' >
						<%#DataBinder.Eval(Container.DataItem,"sAlpha")%>
					</asp:LinkButton>
				</ItemTemplate>
			</asp:repeater></td>
	</tr>
	<tr>
		<TD class="Content" width="1"><IMG height="5" src="images/clear.gif"></TD>
	</tr>
	<tr>
		<td class="Content" align="left" width="100%"><asp:label id="lblSearch" runat="server">Search: </asp:label><asp:textbox id="txtKeyword" runat="server"></asp:textbox><asp:linkbutton id="btnGo" onclick="GoSearch" tabIndex="1" Runat="server">
				<asp:Image BorderWidth="0" ID="imgGo" runat="server" ImageUrl="../images/icon_go.gif" AlternateText="Go"></asp:Image>
			</asp:linkbutton><br>
		</td>
	</tr>
</table>
<table id="searchTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="1">&nbsp;</TD>
		<td class="ContentTableHeader" noWrap align="left" width="100%"><asp:label id="lblTitle" runat="server"></asp:label></td>
		<TD class="ContentTableHeader" width="1">&nbsp;</TD>
		<td class="ContentTableHeader" id="SortCell" noWrap align="right"><asp:label id="lblSortBy" runat="server"></asp:label></td>
		<TD class="ContentTableHeader" width="1">&nbsp;</TD>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	</tr>
	<tr>
		<td colSpan="7"><asp:datagrid id="DataGrid2" runat="server" GridLines="None" CellPadding="0" BorderWidth="0px" ItemStyle-Width="100%" Width="100%" AllowPaging="True" AutoGenerateColumns="False" ShowHeader="False">
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
													<asp:CheckBox Runat="server" Visible="False" ID="chk" AutoPostBack="True"></asp:CheckBox></td>
												<td>
												<td class="content" align="left" width="50%">
													<asp:Label id="One" CssClass="content" Runat="server">
														<%#DataBinder.Eval(Container.DataItem,"Name") %>
													</asp:Label>
												</td>
												<td class="content" align="left" width="50%">
													<asp:Label id="Two" CssClass="content" Runat="server">
														<%#DataBinder.Eval(Container.DataItem,"Code") %>
													</asp:Label>
												</td>
												<td class="Content" align="right" width="20%" id="btnCell " nowrap>
													<asp:TextBox ID="txtID" Runat="server" Visible="False"></asp:TextBox>
													<asp:LinkButton ID="btnEdit" Runat="server" OnClick=EditItem CommandArgument='<%#DataBinder.Eval(Container.DataItem,"uid") %>'>
														<asp:Image BorderWidth="0" ID="imgEdit" runat="server" ImageUrl="../images/edit.jpg" AlternateText="Edit"></asp:Image>
													</asp:LinkButton>
													<br>
													<asp:LinkButton ID="btnDelete" Runat="server" OnClick=DeleteItem CommandArgument='<%#DataBinder.Eval(Container.DataItem,"uid") %>' CausesValidation=False>
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
			</asp:datagrid></td>
	</tr>
</table>
