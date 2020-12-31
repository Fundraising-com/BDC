<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DefaultGST_HSTContractMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.DefaultGST_HSTContractMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table3" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellpadding="3">
	<tr>
		<td><br>
			<asp:label id="Label1" runat="server" cssclass="csPlainText">Product Code </asp:label></td>
		<td><br>
			<asp:label id="lblProductCode" runat="server" cssclass="csPlainText"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label2" runat="server" cssclass="csPlainText">Product Name </asp:label></td>
		<td><asp:label id="lblProductName" runat="server" cssclass="csPlainText"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label5" runat="server" cssclass="csPlainText">Year </asp:label></td>
		<td><asp:label id="lblYear" runat="server" cssclass="csPlainText"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label6" runat="server" cssclass="csPlainText">Season </asp:label></td>
		<td><asp:label id="lblSeason" runat="server" cssclass="csPlainText"></asp:label></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label7" cssclass="csPlainText" runat="server">Oracle Code </asp:label></td>
		<td>
			<asp:label id="lblOracleCode" cssclass="csPlainText" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:label id="Label9" runat="server" cssclass="csPlainText"> Product Contract Status *</asp:label></td>
		<td><asp:radiobuttonlist id="rblContractStatus" runat="server" cssclass="csPlainText" repeatdirection="Horizontal">
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
		<td><asp:label id="Label19" runat="server" cssclass="csPlainText">Listing Level</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlListingLevel" runat="server" contenttype="int" parametername="ListingLevel"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label20" runat="server" cssclass="csPlainText">Listing Copy Text</asp:label></td>
		<td><cc1:textboxsearch id="tbxListingCopyText" runat="server" contenttype="string" parametername="ListingCopyText"
				textmode="MultiLine" height="112px" width="98%" maxlength="500"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td style="HEIGHT: 22px"></td>
		<td style="HEIGHT: 22px"></td>
	</tr>
	<tr>
		<td><asp:label id="Label21" runat="server" cssclass="csPlainText">Advertising in QSP Catalog</asp:label></td>
		<td><asp:radiobuttonlist id="rblAdvertising" runat="server" cssclass="csPlainText" repeatdirection="Horizontal">
				<asp:listitem value="1" selected="True">Yes</asp:listitem>
				<asp:listitem value="0">No</asp:listitem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td><asp:label id="Label22" runat="server" cssclass="csPlainText">Ad Page Size</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlAdPageSize" runat="server" contenttype="int" parametername="AdPageSize"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label23" runat="server" cssclass="csPlainText">Ad / Payment Currency</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlAdPaymentCurrency" runat="server" contenttype="int" parametername="AdPaymentCurrency"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td><asp:label id="Label24" runat="server" cssclass="csPlainText">Ad Cost</asp:label></td>
		<td><cc1:textboxfloat id="tbxAdCost" runat="server" errormsgregexp="The field Ad Cost has to be a number."
				emptyvalue="0"></cc1:textboxfloat></td>
	</tr>
	<tr>
		<td style="HEIGHT: 22px"></td>
		<td style="HEIGHT: 22px"></td>
	</tr>
	<tr>
		<td><asp:label id="Label26" runat="server" cssclass="csPlainText">Comment</asp:label></td>
		<td><cc1:textboxsearch id="tbxComment" runat="server" contenttype="string" parametername="Comment" textmode="MultiLine"
				height="112px" width="98%" maxlength="200"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td style="HEIGHT: 22px"></td>
		<td style="HEIGHT: 22px"></td>
	</tr>
	<tr>
		<td><asp:label id="Label13" runat="server" cssclass="csPlainText">QSP Price</asp:label></td>
		<td>
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td width="50%"><span class="csPlainText">GST:&nbsp;&nbsp;</span>
						<cc1:textboxfloat id="tbxQSPPriceGST" runat="server" required="True" errormsgregexp="The field GST QSP Price has to be a number."
							errormsgrequired="The field GST QSP Price is required." emptyvalue="0"></cc1:textboxfloat>
					</td>
					<td width="50%"><span class="csPlainText">HST:&nbsp;&nbsp;</span>
						<cc1:textboxfloat id="tbxQSPPriceHST" runat="server" required="True" errormsgregexp="The field HST QSP Price has to be a number."
							errormsgrequired="The field HST QSP Price is required." emptyvalue="0"></cc1:textboxfloat>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td><asp:label id="Label34" runat="server" cssclass="csPlainText">Internet Approval</asp:label></td>
		<td><asp:radiobuttonlist id="rblInternetApproval" runat="server" cssclass="csPlainText" repeatdirection="Horizontal">
				<asp:listitem value="True" selected="True">Yes</asp:listitem>
				<asp:listitem value="False">No</asp:listitem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td><asp:label id="Label35" runat="server" cssclass="csPlainText">ABC Code</asp:label></td>
		<td><cc1:textboxsearch id="tbxABCCode" runat="server" contenttype="string" parametername="ABCCode" maxlength="20"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td><asp:label id="Label4" runat="server" cssclass="csPlainText">Premium</asp:label></td>
		<td><cc1:dropdownlistinteger id="ddlPremium" runat="server"></cc1:dropdownlistinteger></td>
	</tr>
	<tr>
		<td style="HEIGHT: 34px" colspan="2"></td>
	</tr>
	<tr>
		<td align="right" colspan="2"><asp:button id="btnSubmit" runat="server" text="Save" cssclass="boxlook" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" runat="server" text="Cancel" causesvalidation="False" cssclass="boxlook" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>
