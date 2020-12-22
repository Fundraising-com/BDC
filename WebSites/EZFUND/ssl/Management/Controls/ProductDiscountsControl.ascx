<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductDiscountsControl.ascx.vb" Inherits="StoreFront.StoreFront.ProductDiscountsControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input id="ProdUID" type="hidden" name="ProdUID" runat="server">
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Sale 
				Price&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="content" align="right" colSpan="1">Activate 
				Sale:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="1"><asp:checkbox id="ActivateSale" runat="server"></asp:checkbox></td>
			<td class="content" align="right" colSpan="1">Sale Price:&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td class="content" align="left" colSpan="1"><asp:textbox id="SalePrice" runat="server"></asp:textbox></td>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<TR>
			<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
		</tr>
		<asp:repeater id="VolumeDiscounts" Runat="server">
			<HeaderTemplate>
				<TR>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="ContentTableHeader" noWrap align="left" colSpan="4">&nbsp;&nbsp;Volume 
						Discounts&nbsp;
					</TD>
					<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="content" width="1" colSpan="4"><IMG height="5" src="images/clear.gif"></TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<TR>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<td class="content" align="left" colSpan="4">Discount:&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:dropdownlist id="DiscountType" runat="server"></asp:dropdownlist></td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
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
					<td class="content" align="left" colspan="4" nowrap>For&nbsp;Purchases&nbsp;Of&nbsp;&nbsp;
						<asp:textbox id="BreakLevel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BreakLevel") %>'>
						</asp:textbox>&nbsp;&nbsp;Or&nbsp;More,&nbsp;Discount&nbsp;Items:&nbsp;&nbsp;
						<asp:Textbox id="Amount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Amount") %>'>
						</asp:Textbox>
					</td>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</TR>
			</ItemTemplate>
			<FooterTemplate>
				<TR>
					<TD class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></TD>
				</TR>
				<tr>
					<TD class="Content" width="1" colSpan="6">&nbsp;</TD>
				</tr>
			</FooterTemplate>
		</asp:repeater>
		<TR>
			<td class="content" align="right" width="75%" colSpan="6">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>
