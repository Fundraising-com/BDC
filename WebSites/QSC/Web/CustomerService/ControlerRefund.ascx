<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerRefund.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerRefund" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="PostalAddressDisabled" Src="../Common/PostalAddressDisabled.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerAddress" Src="ControlerAddress.ascx" %>

<asp:DataList id="dtlRefund" runat="server">
	<HeaderTemplate>
	</HeaderTemplate>
	<ItemTemplate>
		<table>
            <tr>
                <td>
                    <asp:CheckBox Enabled="false" id="cbxSelectRefund" runat="server" 
                    Text="Select Refund" Visible="false"></asp:CheckBox>
                </td>
            </tr>
            <tr>
				<td>
					<asp:Label Runat=server ID=RefundID CssClass="csPlainText" Text='<%#DataBinder.Eval(Container,"DataItem.Refund_ID")%>' Visible="false"></asp:Label>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="Label1" Runat=server CssClass="csPlainText" Text='<%#DataBinder.Eval(Container,"DataItem.LastName")+ (DataBinder.Eval(Container,"DataItem.LastName").ToString()== String.Empty? " ": ", ") + DataBinder.Eval(Container,"DataItem.FirstName")%>'>
					</asp:Label>
				</td>
			</tr>
			<tr>
				<td>
					<uc1:PostalAddressDisabled id="Postaladdressdisabled1" runat="server" pPostalCode='<%# DataBinder.Eval(Container,"DataItem.PostalCode")%>' pStreet1='<%# DataBinder.Eval(Container,"DataItem.Address1")%>' pStreet2='<%# DataBinder.Eval(Container,"DataItem.Address2")%>' pCountry='<%# DataBinder.Eval(Container,"DataItem.Country")%>' pCity='<%#DataBinder.Eval(Container,"DataItem.City")%>' pStateProvince='<%#DataBinder.Eval(Container,"DataItem.Province")%>' >
					</uc1:PostalAddressDisabled>
				</td>
			</tr>		
			<tr>
			    <td>
			        <asp:label id=lblRefundAmountTitle runat="server" CssClass="CSPlainText">Refund Amount</asp:Label>
			    </td>	
				<td>
					<asp:Label Runat=server ID=lblRefundAmount CssClass="csPlainText" Text='<%#DataBinder.Eval(Container,"DataItem.Amount")%>'></asp:Label>
				</td>
			</tr>
			<tr>
			    <td>
			        <asp:label id=lblRefundCreateDateTitle runat="server" CssClass="CSPlainText">Refund Creation Date</asp:Label>
			    </td>	
				<td>
					<asp:Label Runat=server ID=lblRefundCreateDate CssClass="csPlainText" Text='<%#DataBinder.Eval(Container,"DataItem.CreateDate","{0:MM/dd/yyyy}")%>'></asp:Label>
				</td>
			</tr>
			<tr>
			    <td>
			        <asp:label id=lblRefundSentDateTitle runat="server" CssClass="CSPlainText">Refund Sent Date</asp:Label>
			    </td>	
				<td>
					<asp:Label Runat=server ID=lblRefundSentDate CssClass="csPlainText" Text='<%#DataBinder.Eval(Container,"DataItem.SentDate","{0:MM/dd/yyyy}")%>'></asp:Label>
				</td>
			</tr>
			<tr>
			    <td>
			        <asp:label id=lblRefundChequeNumberTitle runat="server" CssClass="CSPlainText">Cheque #</asp:Label>
			    </td>	
				<td>
					<asp:Label Runat=server ID=lblRefundChequeNumber CssClass="csPlainText" Text='<%#DataBinder.Eval(Container,"DataItem.ChequeNumber")%>'></asp:Label>
				</td>
			</tr>
			<tr>
			    <td>
			        <asp:label id=lblRefundChequeStatusTitle runat="server" CssClass="CSPlainText">Cheque Status</asp:Label>
			    </td>	
				<td>
					<asp:Label Runat=server ID=lblRefundChequeStatus CssClass="csPlainText" Text='<%#DataBinder.Eval(Container,"DataItem.ChequeStatus").Equals(System.DBNull.Value)?"Waiting to be sent":DataBinder.Eval(Container,"DataItem.ChequeStatus")%>'></asp:Label>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label Runat="server" ID="Label2" CssClass="csPlainText" Text="- - - - - - - - - - -"></asp:Label>
				</td>
			</tr>
		</table>
	</ItemTemplate>
</asp:DataList>