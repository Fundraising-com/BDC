<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TaxControl.ascx.vb" Inherits="StoreFront.StoreFront.TaxControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="tblVBShippinb" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TBODY>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;Country 
				Tax&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<asp:repeater id="CountryTax" runat="server">
			<HeaderTemplate>
			</HeaderTemplate>
			<ItemTemplate>
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" noWrap align="left">
						<input type=hidden id="CountryName" runat=server value='<%# DataBinder.Eval(Container.DataItem,"Name")%>' NAME="Hidden2">
						<input type=hidden id="CountryUid" runat=server value='<%# DataBinder.Eval(Container.DataItem,"DestinationID")%>'>
						&nbsp;<%# DataBinder.Eval(Container.DataItem,"Name") %>
					</TD>
					<TD class="Content" align="center">Tax Rate:&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:textbox id="CountryRate" Width="45" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Rate") %>' CssClass="Content">
						</asp:textbox>&nbsp;&nbsp;%</TD>
					<TD class="Content" align="right">
						<asp:LinkButton ID="cmdDeleteCountry" Runat="server" OnClick="DeleteCountry" CommandName='<%# Container.ItemIndex %>'>
							<asp:Image BorderWidth="0" ID="imgDeleteCountry" runat="server" ImageUrl="../images/icon_delete.gif" AlternateText="Delete Country"></asp:Image>
						</asp:LinkButton>
						&nbsp;&nbsp;</TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</TR>
			</ItemTemplate>
			<FooterTemplate>
			</FooterTemplate>
		</asp:repeater>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="Content" noWrap align="left">&nbsp;<asp:dropdownlist id="Countries" Runat="server" CssClass="Content"></asp:dropdownlist></TD>
			<TD class="Content" align="middle">Tax Rate:&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="NewCountryRate" runat="server" Width="45" CssClass="Content"></asp:textbox>&nbsp;&nbsp;%</TD>
			<TD class="Content" align="right">
				<asp:LinkButton ID="cmdAddCountry" Runat="server">
					<asp:Image BorderWidth="0" ID="imgAddCountry" runat="server" ImageUrl="../images/icon_add.gif" AlternateText="Add Country"></asp:Image>
				</asp:LinkButton>
				&nbsp;&nbsp;</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="5">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;State/Province 
				Tax&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<asp:repeater id="StateTax" runat="server">
			<HeaderTemplate>
			</HeaderTemplate>
			<ItemTemplate>
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" noWrap align="left">
						<input type=hidden id="StateName" runat=server value='<%# DataBinder.Eval(Container.DataItem,"Name")%>' NAME="Hidden2">
						<input type=hidden id="StateUid" runat=server value='<%# DataBinder.Eval(Container.DataItem,"DestinationID")%>' NAME="Hidden1">&nbsp;<%# DataBinder.Eval(Container.DataItem,"Name") %></TD>
					<TD class="Content" align="center">Tax Rate:&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:textbox id="StateRate" Width="45" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Rate") %>' CssClass="Content">
						</asp:textbox>&nbsp;&nbsp;%</TD>
					<TD class="Content" align="right">
						<asp:LinkButton ID="cmdDeleteState" Runat="server" OnClick="DeleteState" CommandName='<%# Container.ItemIndex %>'>
							<asp:Image BorderWidth="0" ID="imgDeleteState" runat="server" ImageUrl="../images/icon_delete.gif" AlternateText="Delete State"></asp:Image>
						</asp:LinkButton>
						&nbsp;&nbsp;</TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</TR>
			</ItemTemplate>
			<FooterTemplate>
			</FooterTemplate>
		</asp:repeater>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="Content" noWrap align="left">&nbsp;<asp:dropdownlist id="States" Runat="server" CssClass="Content"></asp:dropdownlist></TD>
			<TD class="Content" align="middle">Tax Rate:&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="NewStateRate" runat="server" Width="45" CssClass="Content"></asp:textbox>&nbsp;&nbsp;%</TD>
			<TD class="Content" align="right">
				<asp:LinkButton ID="cmdAddState" Runat="server">
					<asp:Image BorderWidth="0" ID="imgAddState" runat="server" ImageUrl="../images/icon_add.gif" AlternateText="Add State"></asp:Image>
				</asp:LinkButton>
				&nbsp;&nbsp;</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="5">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="3">&nbsp;&nbsp;Local 
				Tax&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="3"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<asp:repeater id="LocalTax" runat="server">
			<HeaderTemplate>
			</HeaderTemplate>
			<ItemTemplate>
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" noWrap align="left">
						<input type=hidden id="LocalName" runat=server value='<%# DataBinder.Eval(Container.DataItem,"Name")%>' NAME="Hidden2">
						<input type=hidden id="LocalUid" runat=server value='<%# DataBinder.Eval(Container.DataItem,"DestinationID")%>'>&nbsp;<%# DataBinder.Eval(Container.DataItem,"Name") %></TD>
					<TD class="Content" align="middle">Tax Rate:&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:textbox id="LocalRate" Width="45" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Rate") %>' CssClass="Content">
						</asp:textbox>&nbsp;&nbsp;%</TD>
					<TD class="Content" align="right">
						<asp:LinkButton ID="cmdDeleteLocal" Runat="server" OnClick="DeleteLocal" CommandName='<%# Container.ItemIndex %>'>
							<asp:Image BorderWidth="0" ID="imgDeleteLocal" runat="server" ImageUrl="../images/icon_delete.gif" AlternateText="Delete Local"></asp:Image>
						</asp:LinkButton>
						&nbsp;&nbsp;</TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</TR>
			</ItemTemplate>
			<FooterTemplate>
			</FooterTemplate>
		</asp:repeater>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="Content" noWrap align="left">&nbsp;<asp:textbox id="NewLocalCode" Runat="server" CssClass="Content" MaxLength="100"></asp:textbox></TD>
			<TD class="Content" align="middle">Tax Rate:&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="NewLocalRate" runat="server" Width="45" CssClass="Content"></asp:textbox>&nbsp;&nbsp;%</TD>
			<TD class="Content" align="right">
				<asp:LinkButton ID="cmdAddLocal" Runat="server">
					<asp:Image BorderWidth="0" ID="imgAddLocal" runat="server" ImageUrl="../images/icon_add.gif" AlternateText="Add Local"></asp:Image>
				</asp:LinkButton>
				&nbsp;&nbsp;</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="5">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="right" width="75%" colSpan="7">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>
