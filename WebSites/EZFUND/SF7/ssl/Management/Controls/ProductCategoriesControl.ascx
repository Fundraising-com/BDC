<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductCategoriesControl.ascx.vb" Inherits="StoreFront.StoreFront.ProductCategoriesControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input id="ProdUID" type="hidden" name="ProdUID" runat="server">
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TBODY>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="ContentTableHeader" noWrap align="left" colSpan="2">&nbsp;&nbsp;Select 
				Category(s)&nbsp;
			</TD>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</TR>
		<tr>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
			<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
		</tr>
		<asp:Repeater id="Categories" runat="server">
			<ItemTemplate>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="content" align="right">
						<asp:CheckBox ID="Active" Runat="server"></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					<TD class="content" align="left" width="100%">
						<input type=hidden id="CatUID" value='<%# DataBinder.Eval(Container.DataItem,"ID") %>' runat=server NAME="CatUID"><%# DataBinder.Eval(Container.DataItem,"Name") %></TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
				<tr>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
					<TD class="content" width="1" colSpan="2"><IMG height="5" src="images/clear.gif"></TD>
					<TD class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></TD>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<TR>
			<TD class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></TD>
		</TR>
		<tr>
			<TD class="Content" width="1" colSpan="4">&nbsp;</TD>
		</tr>
		<TR>
			<td class="content" align="right" width="75%" colSpan="4">
				<asp:LinkButton ID="cmdSave" Runat="server">
					<asp:Image BorderWidth="0" ID="imgSave" runat="server" ImageUrl="../images/save.jpg" AlternateText="Save"></asp:Image>
				</asp:LinkButton>
			</td>
		</TR>
	</TBODY></TABLE>
