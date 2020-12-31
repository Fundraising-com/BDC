<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FieldSuppliesContractMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.FieldSuppliesContractMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
		<td><asp:label id="Label4" cssclass="csPlainText" runat="server">Applicability *</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlFSApplicabilityId" runat="server" required="True" initialtext="Please select..."
				initialvalue="0" errormsgrequired="The field Applicability is required."></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label10" cssclass="csPlainText" runat="server">Distribution *</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlFSDistributionLevelID" runat="server" required="True" initialtext="Please select..."
				initialvalue="0" errormsgrequired="The field Distribution is required."></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label16" cssclass="csPlainText" runat="server">FS Qty. %</asp:label></td>
		<td><cc1:textboxinteger id="tbxFSExtraLimitRate" runat="server"></cc1:textboxinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label13" cssclass="csPlainText" runat="server">Catalog Price *</asp:label></td>
		<td><cc1:textboxfloat id="tbxCatalogPrice" runat="server" required="True" errormsgrequired="The field Catalog Price is required."
				errormsgregexp="The field Catalog Price has to be a number."></cc1:textboxfloat></td>
	</tr>
	<tr>
		<td><asp:label id="Label11" cssclass="csPlainText" runat="server">Brochure *</asp:label></td>
		<td><asp:checkbox id="chkFSIsBrochure" runat="server"></asp:checkbox></td>
	</tr>
	<tr>
		<td><asp:label id="Label12" cssclass="csPlainText" runat="server">Content Catalog</asp:label></td>
		<td><cc1:dropdownlistreq id="ddlFSContentCatalogCode" runat="server" initialtext="Please select..."></cc1:dropdownlistreq></td>
	</tr>
	<tr>
		<td><asp:label id="Label15" cssclass="csPlainText" runat="server">Tax Region</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlTaxRegionID" runat="server" autopostback="True" showfirstline="False" onselectedindexchanged="ddlTaxRegionID_SelectedIndexChanged"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label17" cssclass="csPlainText" runat="server">Province</asp:label></td>
		<td><cc1:dropdownlistprovince id="ddlFSProvinceCode" runat="server" textfirstrow="Please select..." code="CA"
				enabled="False"></cc1:dropdownlistprovince></td>
	</tr>
	<tr>
		<td style="HEIGHT: 34px" colspan="2"></td>
	</tr>
	<tr>
		<td align="right" colspan="2"><asp:button id="btnSubmit" cssclass="boxlook" runat="server" text="Save" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" cssclass="boxlook" runat="server" text="Cancel" causesvalidation="False" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>
