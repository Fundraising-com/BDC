<%@ Register TagPrefix="uc1" TagName="PostalAddressDisabled" Src="../Common/PostalAddressDisabled.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerAddressHistory.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerAddressHistory" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc2" TagName="PostalAddress" Src="../Common/PostalAddress.ascx" %>
<br>
<asp:DataList id="dtlMain" runat="server" Width="120px">
	<HeaderTemplate>
	</HeaderTemplate>
	<ItemTemplate>
		<table>
			<tr>
				<td>
					<asp:Label Runat=server CssClass="csPlainText" Text='<%#DataBinder.Eval(Container,"DataItem.LastName")+ (DataBinder.Eval(Container,"DataItem.LastName").ToString()== String.Empty? " ": ", ") + DataBinder.Eval(Container,"DataItem.FirstName")%>'>
					</asp:Label>
				</td>
			</tr>
			<tr>
				<td>
					<uc1:PostalAddressDisabled id="Postaladdressdisabled1" runat="server" pPostalCode='<%# DataBinder.Eval(Container,"DataItem.Zip")%>' pStreet1='<%# DataBinder.Eval(Container,"DataItem.Address1")%>' pStreet2='<%# DataBinder.Eval(Container,"DataItem.Address2")%>' pCountry='<%# DataBinder.Eval(Container,"DataItem.Country")%>' pCity='<%#DataBinder.Eval(Container,"DataItem.City")%>' pStateProvince='<%#DataBinder.Eval(Container,"DataItem.State")%>' >
					</uc1:PostalAddressDisabled>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label Runat=server ID=lblCreationDate CssClass="csPlainText" Text='<%#DataBinder.Eval(Container,"DataItem.AddressCreationDate","{0:MM/dd/yyyy}")%>'>
					</asp:Label>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label Runat="server" ID="Label1" CssClass="csPlainText" Text="- - - - - - - - - - -"></asp:Label>
				</td>
			</tr>
		</table>
	</ItemTemplate>
</asp:DataList>
