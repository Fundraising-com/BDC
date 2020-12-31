<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerGrossSalesPerformance.ascx.cs" Inherits="QSPFulfillment.Reports.ControlerGrossSalesPerformance" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<!--
<script language="javascript">

	function SetTitleCode(Code,Description)
	{
		var tbxTitleCode = document.getElementById('ctrlControlerGenerateSwitchLetter_tbxTitleCode');
		tbxTitleCode.value = Code;
		
		var lblDescription = document.getElementById('ctrlControlerGenerateSwitchLetter_lblCodeDescription');
		lblDescription.innerHTML = Description;
		
		tbxTitleCode.focus();
	
	}

</script>
-->
<div style="TEXT-ALIGN: center">
	<table id="Table1" cellspacing="0" cellpadding="0" width="500" border="0">
		<tr>
			<td><asp:label id="lblTitle" runat="server" cssclass="CSPageTitle">Gross Sales Performance Report</asp:label></td>
		</tr>
	</table>
	<br>
	<table cellspacing="0" cellpadding="1" width="500" bgcolor="#000000" border="0">
		<tr>
			<td>
				<table id="Table1ss" cellspacing="0" cellpadding="2" width="100%" bgcolor="#ffffff" border="0">
					<tr>
						<td>&nbsp;</td>
						<td width="250"><br>
							<asp:label id="lblFMId" runat="server" cssclass="csPlainText">FM ID</asp:label></td>
						<td><cc1:dropdownlistsearch id="ddlFieldManager" runat="server" contenttype="string" parametername="FMID" width="229px"></cc1:dropdownlistsearch><br>
							<asp:label id="lblFieldManager" runat="server" cssclass="csPlainText" visible="False"></asp:label>
						</td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblDateFrom" runat="server" cssclass="csPlainText">Date From (Campaign Start Date)</asp:label></td>
						<td><uc1:dateentry id="ctrlDateEntryFrom" runat="server" contenttype="DateTime" parametername="DateFrom"></uc1:dateentry></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblDateTo" runat="server" cssclass="csPlainText">Date To (Campaign Start Date)</asp:label></td>
						<td><uc1:dateentry id="ctrlDateEntryTo" runat="server" contenttype="DateTime" parametername="DateTo"></uc1:dateentry></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblProvince" runat="server" cssclass="csPlainText">Province</asp:label></td>
						<td><cc1:dropdownlistprovince id="ddlProvince" runat="server" contenttype="string" parametername="ProvinceCode"
								code="CA" width="229px"></cc1:dropdownlistprovince></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblCity" runat="server" cssclass="csPlainText">City</asp:label></td>
						<td><cc1:textboxsearch id="tbxCity" runat="server" contenttype="string" parametername="City" maxlength="50"
								width="229px"></cc1:textboxsearch></td>
					<tr>
						<td></td>
						<td><asp:label id="lblPostalCode" runat="server" cssclass="csPlainText">Postal Code</asp:label></td>
						<td><cc1:postalcode id="tbxPostalCode" runat="server" contenttype="string" parametername="PostalCode"
								width="229px"></cc1:postalcode></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblGroupClassCode" runat="server" cssclass="csPlainText">Group Class Code</asp:label></td>
						<td><cc1:dropdownlistsearch id="ddlGroupClassCode" runat="server" contenttype="string" parametername="GroupClassCode"
								autopostback="True" width="229px"></cc1:dropdownlistsearch></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblGroupCodeName" runat="server" cssclass="csPlainText">Group Code Name</asp:label></td>
						<td><cc1:dropdownlistsearch id="ddlGroupCodeName" runat="server" contenttype="string" parametername="GroupCodeName"
								width="229px"></cc1:dropdownlistsearch></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblStaffIndicator" runat="server" cssclass="csPlainText">Staff Indicator</asp:label></td>
						<td><cc1:dropdownlistsearch id="ddlStaffIndicator" runat="server" contenttype="int" parametername="StaffIndicator"
								width="229px"></cc1:dropdownlistsearch></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblCampaignLanguage" runat="server" cssclass="csPlainText">Campaign Language</asp:label></td>
						<td><cc1:dropdownlistsearch id="ddlCampaignLanguage" runat="server" contenttype="string" parametername="CampaignLanguage"
								width="229px"></cc1:dropdownlistsearch></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblProgramsCA" runat="server" cssclass="csPlainText">Programs from CA</asp:label></td>
						<td><cc1:dropdownlistsearch id="ddlProgramsFromCampaign" runat="server" contenttype="int" parametername="ProgramsFromCampaign"
								width="229px"></cc1:dropdownlistsearch></td>
					</tr>
					<tr>
						<td style="HEIGHT: 2px"></td>
						<td style="HEIGHT: 2px"><asp:label id="lblIncentivesPrograms" runat="server" cssclass="csPlainText">Incentives Programs</asp:label></td>
						<td style="HEIGHT: 2px"><cc1:dropdownlistsearch id="ddlIncentivesPrograms" runat="server" contenttype="int" parametername="IncentivesPrograms"
								width="229px"></cc1:dropdownlistsearch></td>
					</tr>
					<tr>
						<td></td>
						<td><asp:label id="lblCatalogueCode" runat="server" cssclass="csPlainText">Catalogue Code</asp:label></td>
						<td><cc1:dropdownlistsearch id="ddlCatalogCode" runat="server" contenttype="string" parametername="CatalogCode"
								width="229px"></cc1:dropdownlistsearch></td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br>
	<table cellspacing="0" cellpadding="2" width="500" bgcolor="#ffffff" border="0">
		<tr>
			<td align="center"><asp:button id="btnPreview" runat="server" text="Preview"></asp:button></td>
		</tr>
	</table>
	<cc2:rsgeneration id="rsGenerationGrossSalesPerformance" runat="server" reportname="GrossSalesPerformance" mode="PopUp"></cc2:rsgeneration>
</div>
