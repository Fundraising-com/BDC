<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ShippingValueBasedControl.ascx.vb" Inherits="StoreFront.StoreFront.ShippingValueBasedControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<TABLE id="tblVBShippinb" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="7">&nbsp;
				<asp:Label id="ErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label></TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="5">&nbsp;&nbsp;Value 
				Based Shipping Levels&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="5"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<asp:repeater id="ValueShipping" runat="server">
			<HeaderTemplate>
			</HeaderTemplate>
			<ItemTemplate>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="Content" noWrap align="left">&nbsp;&nbsp; For Orders Up To:&nbsp;&nbsp;</TD>
					<TD class="Content" align="left">
						<asp:textbox id="MaxTotal" Width="45" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MaxTotal") %>'>
						</asp:textbox>&nbsp;&nbsp;</TD>
					<TD class="Content" align="right">&nbsp;&nbsp;Shipping Charge:&nbsp;&nbsp;</TD>
					<TD class="Content" align="left">
						<asp:textbox id="Charge" Width="45" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Amount") %>'>
						</asp:textbox>&nbsp;&nbsp;</TD>
					<TD class="Content" align="right">
						<asp:LinkButton ID="cmdDelete" Runat="server" OnClick="deleteRow" CommandName='<%# Container.ItemIndex %>'>
							<asp:Image BorderWidth="0" ID="imgDelete" runat="server" ImageUrl="../images/icon_delete.gif" AlternateText="Delete"></asp:Image>
						</asp:LinkButton>
						&nbsp;&nbsp;</TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<TR>
					<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</TR>
			</ItemTemplate>
			<FooterTemplate>
			</FooterTemplate>
		</asp:repeater>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="Content" noWrap align="left">&nbsp;&nbsp; For Orders Up To:&nbsp;&nbsp;</TD>
			<TD class="Content" align="left">
				<asp:textbox id="NewMaxTotal" Width="45" runat="server"></asp:textbox>&nbsp;&nbsp;</TD>
			<TD class="Content" align="right">&nbsp;&nbsp;Shipping Charge:&nbsp;&nbsp;</TD>
			<TD class="Content" align="left">
				<asp:textbox id="NewCharge" Width="45" runat="server"></asp:textbox>&nbsp;&nbsp;</TD>
			<TD class="Content" align="right">
				<asp:LinkButton ID="cmdAdd" Runat="server" CommandName="Add">
					<asp:Image BorderWidth="0" ID="imgAdd" runat="server" ImageUrl="../images/icon_add.gif" AlternateText="Add"></asp:Image>
				</asp:LinkButton>
				&nbsp;&nbsp;</TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTable" colSpan="7" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="7">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="right" width="75%" colSpan="7">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>
