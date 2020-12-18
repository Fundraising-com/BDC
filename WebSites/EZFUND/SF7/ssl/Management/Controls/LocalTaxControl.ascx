<%@ Control Language="vb" AutoEventWireup="false" Codebehind="LocalTaxControl.ascx.vb" Inherits="StoreFront.StoreFront.LocalTaxControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
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
				<td colspan="4" class="ContentTableHeader" align="left">
					&nbsp;Local Tax</td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td width="100%" colSpan="4"><IMG height="5" src="images/clear.gif"></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
				<td class="content" width="25%" align="center"><%#DataBinder.Eval(Container.DataItem,"TaxNameType")%>
				</td>
				<td class="content" width="25%" align="center"><IMG width="1" src="images/clear.gif"></td>
				<td class="content" width="25%" align="center">&nbsp;Tax Collected:
				</td>
				<td class="content" width="25%" align="center" nowrap><%#Format(DataBinder.Eval(Container.DataItem,"Tax"),"c")%>
				</td>
				<td class="ContentTable" width="1"><img src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td width="100%" colSpan="4"><IMG height="5" src="images/clear.gif"></td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" colSpan="5" height="1"><IMG height="1" src="images/clear.gif"></td>
			</tr>
		</table>
	</ItemTemplate>
</asp:DataList>
