<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ShippingMethodsControl.ascx.vb" Inherits="StoreFront.StoreFront.ShippingMethodsControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input id="CarrierCode" type="hidden" name="CarrierCode" runat="server">
<asp:repeater id="Methods" runat="server">
	<HeaderTemplate>
		<TABLE id="tblShippingMethods" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TBODY>
				<tr>
					<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
				</tr>
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Shipping 
						Methods&nbsp;
					</TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="Content" noWrap align="left">&nbsp;&nbsp; <input type="hidden" id="MethodCode" runat="server" value='<%# DataBinder.Eval(Container.DataItem,"Code") %>'>
				<asp:checkbox id="Activate" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem,"IsActive") %>'>
				</asp:checkbox>&nbsp;&nbsp;</TD>
			<TD class="Content" align="left"><%# DataBinder.Eval(Container.DataItem,"Method") %>&nbsp;</TD>
			<TD class="Content" align="right">&nbsp;&nbsp;Charge % of Returned 
				Rate:&nbsp;&nbsp;</TD>
			<TD class="Content" align="left">&nbsp;&nbsp;
				<asp:textbox id="Rate" Width="45" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Rates") %>'>
				</asp:textbox>&nbsp;&nbsp;</TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
	</ItemTemplate>
	<FooterTemplate>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		</TBODY> </TABLE>
	</FooterTemplate>
</asp:repeater>
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<td class="content" align="right" width="75%" colSpan="6">
			<asp:LinkButton ID="cmdSave" Runat="server">
				<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
			</asp:LinkButton>
		</td>
	</TR>
</TABLE>
<asp:Panel ID="UPS" Runat="server"><BR><BR>
UPS®, UPS &amp; Shield Design® 
      and UNITED PARCEL SERVICE® are registered trademarks of United Parcel 
      Service of America, Inc.
</asp:Panel>
