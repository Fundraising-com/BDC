<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TransactionControl.ascx.vb" Inherits="StoreFront.StoreFront.TransactionControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<asp:DataList id="DataList1" runat="server" Width="100%">
	<ItemTemplate>
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD class="content" colspan="3" width="1"><IMG height="5" src="images/clear.gif"></TD>
			</TR>
			<tr>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="ContentTableHeader" align="left" width="50%">&nbsp;Order ID:<%#DataBinder.Eval(Container.DataItem,"OrderID")%></td>
				<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
				<td class="ContentTableHeader" align="right" width="50%">&nbsp;<%#DataBinder.Eval(Container.DataItem,"OrderDate")%>&nbsp;&nbsp;</td>
				<td class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" noWrap align="left" width="50%">Authorization No.:<%#DataBinder.Eval(Container.DataItem,"AuthorizationNo")%>
				</td>
				<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
				<td class="content" noWrap align="left" width="50%">Success.:<%#DataBinder.Eval(Container.DataItem,"Success")%>
				</td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" noWrap align="left" width="50%">Customer Transaction No.:<%#DataBinder.Eval(Container.DataItem,"CustTransNo")%>
				</td>
				<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
				<td class="content" noWrap align="left" width="50%">Merchant Transaction No.:<%#DataBinder.Eval(Container.DataItem,"MerchTransNo")%>
				</td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" noWrap align="left" width="50%">AVS Result:<%#DataBinder.Eval(Container.DataItem,"AVSResult")%>
				</td>
				<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
				<td class="content" align="left" rowspan="3" valign="top" width="50%">AUX Message:<%#DataBinder.Eval(Container.DataItem,"AUXMessage")%>
				</td>
				<td class="ContentTable" width="1" rowspan="3"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" colSpan="2" height="1"><IMG height="1" src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" noWrap align="left" width="50%">CVV Result:<%#DataBinder.Eval(Container.DataItem,"CVVResult")%>
				</td>
				<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
				<td class="content" noWrap align="left" width="50%">&nbsp;
				</td>
				<td class="Content" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" noWrap align="left" width="50%">Action Code:<%#DataBinder.Eval(Container.DataItem,"ActionCode")%>
				</td>
				<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
				<td class="content" noWrap align="left" width="50%">Retrieval Code:<%#DataBinder.Eval(Container.DataItem,"RetrievalCode")%>
				</td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
			</tr>
			<tr>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
				<td class="content" align="left" width="50%">Error Message:<%#DataBinder.Eval(Container.DataItem,"ErrorMessage")%>
				</td>
				<td class="ContentTable" height="1"><IMG height="1" src="images/clear.gif"></td>
				<td class="content" noWrap align="left" width="50%">Error Location:<%#DataBinder.Eval(Container.DataItem,"ErrorLocation")%>
				</td>
				<td class="ContentTable" width="1"><IMG src="images/clear.gif" width="1"></td>
			</tr>
			<tr>
				<td class="ContentTable" colSpan="4" height="1"><IMG height="1" src="images/clear.gif"></td>
			</tr>
		</table>
	</ItemTemplate>
</asp:DataList>