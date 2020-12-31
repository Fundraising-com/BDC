<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="IncentivesCumulativeMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.MarketingMgt.Control.IncentivesCumulativeMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<br>
<table id="Table3" style="BORDER-RIGHT: silver 1px solid; BORDER-TOP: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-BOTTOM: silver 1px solid"
	cellpadding="3">
	<tr>
		<td><asp:label id="Label10" cssclass="csPlainText" runat="server">Product Type *</asp:label></td>
		<td><cc1:dropdownlistreq id="ddlProductType" runat="server" required="True" errormsgrequired="Please select a Product Type."
				initialvalue="0" parametername="ProductLine" contenttype="int" autopostback="True" onselectedindexchanged="ddlProductType_SelectedIndexChanged"></cc1:dropdownlistreq></td>
	</tr>
	<tr>
		<td><br>
			<asp:label id="Label1" cssclass="csPlainText" runat="server">Product Code *</asp:label></td>
		<td><br>
			<cc1:textboxreq id="tbxUMCCode" runat="server" maxlength="20" required="True" errormsgrequired="The field UMC Code is mandatory."></cc1:textboxreq></td>
	<tr>
		<td><asp:label id="Label16" cssclass="csPlainText" runat="server">Year *</asp:label></td>
		<td><cc1:dropdownlistreq id="ddlYear" runat="server" required="True" errormsgrequired="Please select a year."
				initialvalue="0"></cc1:dropdownlistreq></td>
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
		<td>
			<cc1:dropdownlistreq id="ddlLanguage" runat="server" required="True" errormsgrequired="The field Language is required.">
				<asp:listitem selected="True"></asp:listitem>
				<asp:listitem value="EN">English</asp:listitem>
				<asp:listitem value="FR">French</asp:listitem>
				<asp:listitem value="EN/FR">English/French</asp:listitem>
			</cc1:dropdownlistreq>
		</td>
	</tr>
	<tr>
		<td><asp:label id="Label9" cssclass="csPlainText" runat="server">Status *</asp:label></td>
		<td><asp:radiobuttonlist id="rblStatus" cssclass="csPlainText" runat="server" repeatdirection="Horizontal">
				<asp:listitem value="30600" selected="True">Active</asp:listitem>
				<asp:listitem value="30601">Inactive</asp:listitem>
				<asp:listitem value="30602">Pending</asp:listitem>
			</asp:radiobuttonlist></td>
	</tr>
	<tr>
		<td><asp:label id="Label11" cssclass="csPlainText" runat="server"> Comment</asp:label></td>
		<td><cc1:textboxsearch id="tbxComment" runat="server" maxlength="200" parametername="Note" contenttype="string"
				width="98%" textmode="MultiLine" height="50px"></cc1:textboxsearch></td>
	</tr>
	<tr>
		<td><asp:label id="Label4" cssclass="csPlainText" runat="server">Oracle Code</asp:label></td>
		<td><cc1:textboxreq id="tbxOracleCode" runat="server" required="True" maxlength="50" errormsgrequired="The field Oracle Code is required."></cc1:textboxreq></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label5" runat="server" cssclass="csPlainText">Level Attained *</asp:label></td>
		<td>
			<cc1:textboxreq id="tbxPrizeLevel" runat="server" required="True" maxlength="10" errormsgrequired="The field Level Attained is required."></cc1:textboxreq>
		</td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label6" runat="server" cssclass="csPlainText">Qty Required *</asp:label></td>
		<td>
			<cc1:textboxinteger id="tbxPrizeLevelQuantity" runat="server" required="True" maxlength="10" errormsgrequired="The field Quantity Required is required."></cc1:textboxinteger>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 22px"></td>
		<td style="HEIGHT: 22px"></td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label7" runat="server" cssclass="csPlainText">English Description</asp:label></td>
		<td>
			<asp:textbox id="tbxEnglishDescription" runat="server" columns="40" maxlength="50"></asp:textbox>
		</td>
	</tr>
	<tr>
		<td>
			<asp:label id="Label12" runat="server" cssclass="csPlainText">French Description</asp:label></td>
		<td>
			<asp:textbox id="tbxFrenchDescription" runat="server" columns="40" maxlength="50"></asp:textbox>
		</td>
	</tr>
	<tr>
		<td style="HEIGHT: 34px" colspan="2"></td>
	</tr>
	<tr>
		<td style="BORDER-TOP: silver 1px solid" align="right" colspan="2"><asp:button id="btnSubmit" cssclass="boxlook" runat="server" text="Submit" onclick="btnSubmit_Click"></asp:button><asp:button id="btnCancel" cssclass="boxlook" runat="server" text="Cancel" causesvalidation="False" onclick="btnCancel_Click"></asp:button></td>
	</tr>
</table>
