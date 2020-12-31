<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DefaultSingleContractMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.DefaultSingleContractMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table3" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellpadding="3">
	<tr>
		<td><br>
			<asp:label id="Label1" cssclass="csPlainText" runat="server">Product Code </asp:label></td>
		<td><br>
			<asp:label id="lblProductCode" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label2" cssclass="csPlainText" runat="server">Product Name </asp:label></td>
		<td><asp:label id="lblProductName" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label5" cssclass="csPlainText" runat="server">Year </asp:label></td>
		<td><asp:label id="lblYear" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label6" cssclass="csPlainText" runat="server">Season </asp:label></td>
		<td><asp:label id="lblSeason" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label7" cssclass="csPlainText" runat="server">Oracle Code </asp:label></td>
		<td><asp:label id="lblOracleCode" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="LabelVendorProductCode" cssclass="csPlainText" runat="server">Vendor Product Code </asp:label></td>
		<td><asp:label id="lblLabelVendorProductCodeValue" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label9" cssclass="csPlainText" runat="server"> Product Contract Status *</asp:label></td>
		<td><asp:radiobuttonlist id="rblContractStatus" cssclass="csPlainText" runat="server" repeatdirection="Horizontal">
				<asp:listitem value="30600" selected="True">Active</asp:listitem>
				<asp:listitem value="30601">Inactive</asp:listitem>
				<asp:listitem value="30602">Pending</asp:listitem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td style="HEIGHT: 22px"></td>
		<td style="HEIGHT: 22px"></td>
	</tr>
	<tr>
		<td><asp:label id="Label26" cssclass="csPlainText" runat="server">Comment</asp:label></td>
		<td><cc1:textboxsearch id="tbxComment" runat="server" maxlength="200" width="98%" height="112px" textmode="MultiLine"
				parametername="Comment" contenttype="string"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td style="HEIGHT: 22px"></td>
		<td style="HEIGHT: 22px"></td>
	</tr>
	<tr>
		<td><asp:label id="Label13" cssclass="csPlainText" runat="server">Catalog Price</asp:label></td>
		<td><cc1:textboxfloat id="tbxCatalogPrice" runat="server" required="True" errormsgrequired="The field Catalog Price is required."
				errormsgregexp="The field Catalog Price has to be a number." emptyvalue="0"></cc1:textboxfloat></td>
	</tr>
    <tr>
		<td style="HEIGHT: 22px"></td>
		<td style="HEIGHT: 22px"></td>
	</tr>
	<tr>
		<td><asp:label id="lblAddlHandlingFee" cssclass="csPlainText" runat="server">S&amp;H Fee</asp:label></td>
		<td><cc1:textboxfloat id="tbxAddlHandlingFee" runat="server" required="False" errormsgregexp="The field S&amp;H Fee has to be a number." emptyvalue="0"></cc1:textboxfloat></td>
	</tr>
    <tr>
		<td style="HEIGHT: 22px"></td>
		<td style="HEIGHT: 22px"></td>
	</tr>    
	<tr>
		<td><asp:label id="LabelInternetApproval" runat="server" cssclass="csPlainText">Internet Approval</asp:label></td>
		<td><asp:radiobuttonlist id="rblInternetApproval" runat="server" cssclass="csPlainText" repeatdirection="Horizontal">
				<asp:listitem value="True" selected="True">Yes</asp:listitem>
				<asp:listitem value="False">No</asp:listitem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td style="HEIGHT: 34px" colspan="2"></td>
	</tr>
	<tr>
		<td align="right" colspan="2"><asp:button id="btnSubmit" cssclass="boxlook" runat="server" text="Save" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" cssclass="boxlook" runat="server" text="Cancel" causesvalidation="False" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>
