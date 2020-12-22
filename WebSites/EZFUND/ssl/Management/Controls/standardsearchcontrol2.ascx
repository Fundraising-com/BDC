<%@ Control Language="vb" AutoEventWireup="false" Codebehind="standardsearchcontrol2.ascx.vb" Inherits="StoreFront.StoreFront.standardsearchcontrol2" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<table id="tblSearch" cellSpacing="0" cellPadding="0" border="0" runat="server">
	<tr>
		<td class="Content" id="AlphaCell"></td>
	</tr>
	<tr>
		<td class="Content"><asp:label id="lblSearch" runat="server">Search: </asp:label><asp:textbox id="txtKeyword" runat="server"></asp:textbox>
			<asp:LinkButton ID="btnGo" Runat="server" onclick="GoSearch">
				<asp:Image BorderWidth="0" ID="imgGo" runat="server" ImageUrl="../images/icon_go.gif" AlternateText="Go"></asp:Image>
			</asp:LinkButton>
		</td>
	</tr>
</table>
<br>
<table id="searchTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<tr>
		<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		<TD class="ContentTableHeader" width="1">&nbsp;</TD>
		<td class="ContentTableHeader" noWrap align="left" width="100%"><asp:label id="lblTitle" runat="server"></asp:label></td>
		<TD class="ContentTableHeader" width="1">&nbsp;</TD>
		<td class="ContentTableHeader" id="SortCell" noWrap align="right"><asp:label id="lblSortBy" runat="server">Sort By:&nbsp;</asp:label></td>
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
													<asp:CheckBox Runat="server" ID="chk" AutoPostBack="True" OnCheckedChanged="CheckChange"></asp:CheckBox></td>
												<td>
													<table border="0" cellpadding="0" cellspacing="0" width="100%" id="tblInner" runat="server">
														<tr id="trInner">
														</tr>
													</table>
												</td>
												<td class="Content" align="right" width="20%" id="btnCell">
													<asp:TextBox ID="txtID" Runat="server" Visible="False"></asp:TextBox>
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
