<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MagazineMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.MagazineMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<table id="Table3" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellpadding="3">
	<tr>
		<td><asp:label id="Label10" cssclass="csPlainText" runat="server">Product Type *</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlProductType" runat="server" required="True" errormsgrequired="Please select a Product Type."
				initialvalue="0" parametername="ProductLine" contenttype="int" autopostback="True"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><br>
			<asp:label id="Label1" cssclass="csPlainText" runat="server">Product Code *</asp:label></td>
		<td><br>
			<cc1:textboxreq id="tbxUMCCode" runat="server" maxlength="20" required="True" errormsgrequired="The field UMC Code is mandatory."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label21" runat="server" cssclass="csPlainText">Remit Code *</asp:label>
		</td>
		<td>
			<cc1:textboxreq id="tbxRemitCode" runat="server" errormsgrequired="The field Remit Code is mandatory."
				required="True" maxlength="20"></cc1:textboxreq>
		</td>
	</tr>
	<tr>
		<td><asp:label id="Label16" cssclass="csPlainText" runat="server">Year *</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlYear" runat="server" required="True" errormsgrequired="Please select a year."
				initialvalue="0"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label17" cssclass="csPlainText" runat="server">Season *</asp:label></td>
		<td><cc1:dropdownlistreq id="ddlSeason" runat="server" required="True" errormsgrequired="Please select a season."></cc1:dropdownlistreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label2" cssclass="csPlainText" runat="server">Product Name *</asp:label></td>
		<td><cc1:textboxreq id="tbxProductName" runat="server" maxlength="55" required="True" errormsgrequired="The field Product Name is mandatory."
				columns="40"></cc1:textboxreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label3" cssclass="csPlainText" runat="server">Language *</asp:label></td>
		<td><asp:radiobuttonlist id="rblLanguage" cssclass="csPlainText" runat="server" repeatdirection="Horizontal">
				<asp:listitem value="EN" selected="True">English</asp:listitem>
				<asp:listitem value="FR">French</asp:listitem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td><asp:label id="Label4" cssclass="csPlainText" runat="server">Category *</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlCategory" runat="server" required="True" errormsgrequired="Please select a Category."
				initialvalue="0" parametername="Category" contenttype="int"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label9" cssclass="csPlainText" runat="server">Status *</asp:label></td>
		<td><asp:radiobuttonlist id="rblStatus" cssclass="csPlainText" runat="server" repeatdirection="Horizontal">
				<asp:listitem value="30600" selected="True">Active</asp:listitem>
				<asp:listitem value="30601">Inactive</asp:listitem>
				<asp:listitem value="30602">Pending</asp:listitem>
				<asp:listitem value="30603">Unremittable</asp:listitem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td><asp:label id="Label33" cssclass="csPlainText" runat="server">Days Lead Time</asp:label></td>
		<td><cc1:textboxinteger id="tbxDaysLeadTime" runat="server" errormsgregexp="The field Days Lead Time has to be a number."
				emptyvalue="0"></cc1:textboxinteger></td>
	</tr>
	<tr>
		<td style="HEIGHT: 16px"><asp:label id="Label7" cssclass="csPlainText" runat="server">Number of issues published per year *</asp:label></td>
		<td style="HEIGHT: 16px"><cc1:textboxinteger id="tbxNumberOfIssues" runat="server" errormsgregexp="The field Number Of Issues has to be a number."
				emptyvalue="0"></cc1:textboxinteger></td>
	</tr>
	<tr>
		<td style="HEIGHT: 10px"><asp:label id="Label5" cssclass="csPlainText" runat="server">Publisher *</asp:label></td>
		<td style="HEIGHT: 10px"><cc1:dropdownlistinteger id="ddlPublisher" runat="server" required="True" errormsgrequired="Please select a Publisher."
				initialvalue="0" parametername="PublisherInstance" contenttype="int" width="350px"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td style="HEIGHT: 10px"><asp:label id="Label18" cssclass="csPlainText" runat="server">Fulfillment House *</asp:label></td>
		<td style="HEIGHT: 10px"><cc1:dropdownlistinteger id="ddlFulfillmentHouse" runat="server" required="True" errormsgrequired="Please select a Fulfillment House."
				initialvalue="0" parametername="FulfillmentHouseInstance" contenttype="int" width="350px" autopostback="True"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td style="HEIGHT: 10px">
			<asp:label id="Label22" runat="server" cssclass="csPlainText">Is QSP Exclusive *</asp:label></td>
		<td style="HEIGHT: 10px">
			<asp:checkbox id="chkIsQSPExclusive" runat="server"></asp:checkbox>
		</td>
	</tr>
	<tr>
		<td><asp:label id="Label11" cssclass="csPlainText" runat="server"> Comment</asp:label></td>
		<td><cc1:textboxsearch id="tbxComment" runat="server" maxlength="200" parametername="Note" contenttype="string"
				width="98%" textmode="MultiLine" height="50px"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td><asp:label id="Label13" cssclass="csPlainText" runat="server">Vendor Number *</asp:label></td>
		<td><cc1:textboxsearch id="tbxVendorNumber" runat="server" parametername="VendorNumber" contenttype="string"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td><asp:label id="Label12" cssclass="csPlainText" runat="server">Vendor Site Name</asp:label></td>
		<td><asp:label id="lblVendorSiteName" cssclass="csPlainText" runat="server"></asp:label><input id="hidVendorSiteName" type="hidden" name="hidVendorSiteName" runat="server"></td>
	</tr>
	<tr>
		<td><asp:label id="Label6" cssclass="csPlainText" runat="server">Pay Group Look Up Code</asp:label></td>
		<td><asp:label id="lblPayGroupLookUpCode" cssclass="csPlainText" runat="server"></asp:label><input id="hidPayGroupLookUpCode" type="hidden" name="hidPayGroupLookUpCode" runat="server">
			<input id="hidPayGroup" type="hidden" name="hidPayGroup" runat="server">
		</td>
	</tr>
	<tr>
		<td><asp:label id="Label8" cssclass="csPlainText" runat="server">Currency *</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlCurrency" runat="server" required="True" errormsgrequired="Please select a Currency."
				initialvalue="0" parametername="Currency" contenttype="int"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label14" cssclass="csPlainText" runat="server">GST Registration Number</asp:label></td>
		<td><asp:textbox id="tbxGST" runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td><asp:label id="Label19" cssclass="csPlainText" runat="server">HST Registration Number</asp:label></td>
		<td>
			<table cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td><asp:textbox id="tbxHST" runat="server"></asp:textbox></td>
					<td style="PADDING-LEFT: 10px"><asp:label id="Label20" style="FONT-WEIGHT: bold" cssclass="csPlainText" runat="server">* The tax registration numbers are the same for all seasons and offers</asp:label></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><asp:label id="Label15" cssclass="csPlainText" runat="server">QST Registration Number</asp:label></td>
		<td><asp:textbox id="tbxPST" runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td style="HEIGHT: 34px" colspan="2"></td>
	</tr>
	<tr>
		<td style="BORDER-TOP: silver 1px solid" align="right" colspan="2"><asp:button id="btnSubmit" cssclass="boxlook" runat="server" text="Submit" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" cssclass="boxlook" runat="server" text="Cancel" causesvalidation="False" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>
