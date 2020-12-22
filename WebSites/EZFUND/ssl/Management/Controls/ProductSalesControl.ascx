<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ProductSalesControl.ascx.vb" Inherits="StoreFront.StoreFront.ProductSalesControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:DataList id="DataList1" runat="server" Width="100%">
	<ItemTemplate>
		<table class="content" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td class="Content" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td width="100%" colSpan="4"><IMG height="5" src="images/clear.gif"></td>
				<td class="Content" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td colspan="2" class="ContentTableHeader" align="left">
					&nbsp;<%#DataBinder.Eval(Container.DataItem,"ProductName")%></td>
				<td colspan="2" class="ContentTableHeader" align="right">
					&nbsp;<%#DataBinder.Eval(Container.DataItem,"ProductCode")%>&nbsp;&nbsp;</td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td width="100%" colSpan="4"><IMG height="5" src="images/clear.gif"></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
				<td class="content" width="25%" align="center">&nbsp;Total Units Sold:
				</td>
				<td class="content" width="25%" align="center"><%#DataBinder.Eval(Container.DataItem,"Units")%>
				</td>
				<td class="content" width="25%" align="center">&nbsp;Total Sales:
				</td>
				<td class="content" width="25%" align="center" nowrap><%#Format(DataBinder.Eval(Container.DataItem,"Total"),"c")%>
				</td>
				<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td width="100%" colSpan="4"><IMG height="5" src="images/clear.gif"></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" colSpan="6" height="1"><IMG height="1" src="images/clear.gif"></td>
			</tr>
		</table>
	</ItemTemplate>
</asp:DataList>
