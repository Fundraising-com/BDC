<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerPaymentInfo.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerPaymentInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<table id="Table1" width="90%" cellspacing="0" cellpadding="2" border="0" class="CSTable">
	<tr class="CSTableHeader">
		<td colspan="3">
			Credit Card Information
		</td>
	</tr>
	<tr>
		<td class="CSTableSubHeader">
			<asp:label id="lbl402" runat="server">Payment Method</asp:label><br>
		</td>
		<td class="CSTableItems">
			<asp:label id="lblPaymentMethod" runat="server"></asp:label><br>
			<cc1:dropdownlistreq id="ddlPaymentMethod" runat="server" visible="False" required="True" initialvalue="0"
				errormsgrequired="The Payment Method is mandatory.">
				<asp:listitem value="0" selected="True">Select</asp:listitem>
				<asp:listitem value="50003">Visa</asp:listitem>
				<asp:listitem value="50004">Master Card</asp:listitem>
			</cc1:dropdownlistreq>
		</td>
		<td></td>
	</tr>
	<tr>
		<td class="CSTableSubHeader">
			<asp:label id="Label9" runat="server">Cardholder's Name</asp:label></td>
		<td class="CSTableItems">
			<asp:label id="lblCardholderName" runat="server"></asp:label>
			<cc1:textboxreq id="tbxCardholderName" runat="server" visible="False" maxlength="80" required="True"
				errormsgrequired="The Cardholder's Name is mandatory"></cc1:textboxreq></td>
		<td></td>
	</tr>
	<tr>
		<td class="CSTableSubHeader">
			<asp:label id="lbl01" runat="server">Number</asp:label></td>
		<td class="CSTableItems">
			<asp:label id="lblNumber" runat="server"></asp:label>
			<cc1:textboxreq id="tbxNumber" runat="server" visible="False" maxlength="19" required="True" errormsgrequired="The Credit Card Number is mandatory."></cc1:textboxreq></td>
		<td></td>
	</tr>
	<tr>
		<td class="CSTableSubHeader" style="HEIGHT: 46px">
			<asp:label id="Label7" runat="server">Expiry</asp:label></td>
		<td class="CSTableItems" style="HEIGHT: 46px">
			<asp:label id="lblExpiry" runat="server"></asp:label>
			<cc1:dropdownlistreq id="ddlMonth" runat="server" visible="False" required="True" initialvalue="Select"
				errormsgrequired="The Expiration Month is mandatory.">
				<asp:listitem value="Select" selected="True">Select</asp:listitem>
				<asp:listitem value="01">01</asp:listitem>
				<asp:listitem value="02">02</asp:listitem>
				<asp:listitem value="03">03</asp:listitem>
				<asp:listitem value="04">04</asp:listitem>
				<asp:listitem value="05">05</asp:listitem>
				<asp:listitem value="06">06</asp:listitem>
				<asp:listitem value="07">07</asp:listitem>
				<asp:listitem value="08">08</asp:listitem>
				<asp:listitem value="09">09</asp:listitem>
				<asp:listitem value="10">10</asp:listitem>
				<asp:listitem value="11">11</asp:listitem>
				<asp:listitem value="12">12</asp:listitem>
			</cc1:dropdownlistreq><asp:label id="lblSlash" runat="server" visible="False">/</asp:label>
			<cc1:dropdownlistreq id="ddlYear" runat="server" visible="False" required="True" initialvalue="Select" errormsgrequired="The Expiration Year is mandatory."></cc1:dropdownlistreq></td>
		<td style="HEIGHT: 46px"></td>
	</tr>
	<tr>
		<td class="CSTableSubHeader">
			<asp:label id="Label8" runat="server">Authorization</asp:label></td>
		<td class="CSTableItems">
			<asp:label id="lblAuthorization" runat="server"></asp:label></td>
		<td></td>
	</tr>
	<tr>
		<td class="CSTableSubHeader">
			<asp:label id="Label10" runat="server">Return Code</asp:label></td>
		<td class="CSTableItems">
			<asp:label id="lblReturnCode" runat="server"></asp:label></td>
		<td class="CSTableItems">
			<asp:label id="lblReturnCodeDesc" runat="server"></asp:label></td>
	</tr>
	<tr id="tableRowCreditCardStatus" runat="server">
		<td>
			<asp:label id="lblStatusTitle" runat="server" text="Status" cssclass="csPlainText">Status</asp:label>
		</td>
		<td>
			<asp:label id="lblStatus" runat="server" cssclass="csPlainText"></asp:label>
		</td>
		<td></td>
	</tr>
	<tr>
		<td>
			<asp:label id="lblAmountTitle" runat="server" text="Status" cssclass="csPlainText">Amount</asp:label>
		</td>
		<td>
			<asp:label id="lblAmount" runat="server" cssclass="csPlainText"></asp:label>
		</td>
		<td></td>
	</tr>
</table>

<asp:DataList id="dtlGiftCard" runat="server">
	<HeaderTemplate>
   <table id="Table" width="200%" cellspacing="0" cellpadding="2" border="0" class="CSTable">
	   <tr class="CSTableHeader">
		   <td colspan="3">
			   Gift Card Information
		   </td>
	   </tr>
   </table>
	</HeaderTemplate>
	<ItemTemplate>
   <table id="Table" width="200%" cellspacing="0" cellpadding="2" border="0" class="CSTable">
	   <tr>
		   <td class="CSTableSubHeader">
			   <asp:label id="lbl01" runat="server">Gift Card Code</asp:label></td>
		   <td class="CSTableItems">
			   <asp:label id="lblGiftCardCode" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.GiftCardCode")%>'>></asp:label>
		   <td></td>
	   </tr>
	   <tr>
		   <td class="CSTableSubHeader">
			   <asp:label id="Label1" runat="server">Amount Applied</asp:label></td>
		   <td class="CSTableItems">
			   <asp:label id="lblGiftCardAmountApplied" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.AmountApplied")%>'></asp:label>
		   <td></td>
	   </tr>
   </table>
   </ItemTemplate>
</asp:DataList>