<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="cc3" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerIncidentsManagementReport.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerIncidentsManagementReport" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InfoSearchMagazine" Src="InfoSearchMagazine.ascx" %>
<script language="javascript">

	function SetTitleCode(Code,Description)
	{
		var tbxTitleCode = document.getElementById('ctrlControlerIncidentsManagementReport_tbxTitleCode');
		tbxTitleCode.value = Code;
						
		tbxTitleCode.focus();
	
	}
	function SetProblemCode(PCID,Description)
	{
		var tbxProblemCode = document.getElementById('ctrlControlerIncidentsManagementReport_tbxProblemCode');
		tbxProblemCode.value = PCID;
		
		tbxProblemCode.focus();
	
	}
	function OnClickAction(objectid)
	{
		var rab = document.getElementById(objectid);
		var ddl = document.getElementById('ctrlControlerIncidentsManagementReport_ddlAction');
		if(rab.checked)
		{
			
			ddl.disabled = false;	
			
		}
		else
		{
			ddl.disabled = true;
		}
	}

</script>
<table class="CSTable" id="Table2" cellspacing="0" cellpadding="2">
	<tr>
		<br>
		<td class="CSPageTitle" colspan="2" style="HEIGHT: 22px"><h3>Customer Service Incidents 
				Management Report</h3>
		</td>
	</tr>
	<tr>
		<td align="center">
			<table id="Table3">
				<tr>
					<td colspan="2"></td>
				</tr>
				<tr>
					<td><br>
						<asp:label id="Label1" runat="server" cssclass="csPlainText">Incident Status</asp:label></td>
					<td><br>
						<cc1:dropdownlistsearch id="ddlIncidentStatus" runat="server" parametername="IncidentStatusInstance" contenttype="int"></cc1:dropdownlistsearch></td>
				</tr>
				<tr>
					<td><asp:label id="Label2" runat="server" cssclass="csPlainText">Incident ID from</asp:label></td>
					<td><cc1:textboxsearch id="tbxIncidentIDFrom" runat="server" parametername="IncidentIDFrom" contenttype="int"></cc1:textboxsearch><asp:rangevalidator id="RangeValidator1" runat="server" minimumvalue="1" maximumvalue="2147483647" controltovalidate="tbxIncidentIDFrom"
							type="Integer" errormessage="Problem Code must be between 1 and 2147483647.">*</asp:rangevalidator></td>
				</tr>
				<tr>
					<td><asp:label id="Label8" runat="server" cssclass="csPlainText">Incident ID to</asp:label></td>
					<td><cc1:textboxsearch id="tbxIncidentIDTo" runat="server" parametername="IncidentIDTo" contenttype="int"></cc1:textboxsearch><asp:rangevalidator id="RangeValidator2" runat="server" minimumvalue="1" maximumvalue="2147483647" controltovalidate="tbxIncidentIDTo"
							type="Integer" errormessage="Incident ID from must be between 1 and 2147483647.">*</asp:rangevalidator></td>
				</tr>
				<tr>
					<td><asp:label id="Label14" runat="server" cssclass="csPlainText">Date Incident logged from</asp:label></td>
					<td><uc1:dateentry id="ctrlDateLoggedFrom" parametername="DateIncidentLoggedFrom" contenttype="DateTime"
							runat="server"></uc1:dateentry></td>
				</tr>
				<tr>
					<td><asp:label id="Label9" runat="server" cssclass="csPlainText">Date Incident logged to</asp:label></td>
					<td><uc1:dateentry id="ctrlDateLoggedTo" runat="server" parametername="DateIncidentLoggedTo" contenttype="DateTime"></uc1:dateentry></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label3" cssclass="csPlainText" runat="server">Order ID from</asp:label></td>
					<td>
						<cc1:textboxinteger id="tbxOrderIDFrom" runat="server" emptyvalue="0"></cc1:textboxinteger>
					</td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label4" cssclass="csPlainText" runat="server">Order ID to</asp:label></td>
					<td>
						<cc1:textboxinteger id="tbxOrderIDTo" runat="server" emptyvalue="0"></cc1:textboxinteger>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 19px"><asp:label id="Label10" runat="server" cssclass="csPlainText">Incident logged by user</asp:label></td>
					<td style="HEIGHT: 19px"><cc1:dropdownlistsearch id="ddlLoggedByUser" runat="server" parametername="LoggedByInstance" contenttype="int"></cc1:dropdownlistsearch></td>
				</tr>
				<tr>
					<td><asp:label id="Label33" runat="server" cssclass="csPlainText">Problem Code</asp:label></td>
					<td><cc1:textboxsearch id="tbxProblemCode" runat="server" parametername="ProblemCodeInstance" contenttype="int"></cc1:textboxsearch><asp:rangevalidator id="RangeValidator5" runat="server" minimumvalue="1" maximumvalue="2147483647" controltovalidate="tbxProblemCode"
							type="Integer" errormessage="Problem Code must be between 1 and 2147483647.">*</asp:rangevalidator><asp:hyperlink id="hypFindProblemCode" runat="server" imageurl="images/find.gif" navigateurl="javascript:void(0);"></asp:hyperlink></td>
				</tr>
				<tr>
					<td style="HEIGHT: 16px"><asp:label id="Label7" runat="server" cssclass="csPlainText">Fulfillment House</asp:label></td>
					<td style="HEIGHT: 16px"><cc1:dropdownlistsearch id="ddlFulfillmentHouse" runat="server" autopostback="True" parametername="FulfillmentHouseInstance"
							width="350px" contenttype="int"></cc1:dropdownlistsearch></td>
				</tr>
				<tr>
					<td style="HEIGHT: 10px"><asp:label id="Label5" runat="server" cssclass="csPlainText">Publisher</asp:label></td>
					<td style="HEIGHT: 10px"><cc1:dropdownlistsearch id="ddlPublisher" runat="server" parametername="PublisherInstance" width="350px"
							contenttype="int"></cc1:dropdownlistsearch></td>
				</tr>
				<tr>
					<td><asp:label id="Label11" runat="server" cssclass="csPlainText">Title Code</asp:label></td>
					<td>
						<cc1:textboxsearch id="tbxTitleCode" runat="server" parametername="TitleCodeInstance" contenttype="int"></cc1:textboxsearch><a id="A1" onclick="javasrcipt:Open('Magazine.aspx?IsNewWindow=true&amp;ID=true&amp;IsOnlyMagazine=true')"
							href="javascript:;" runat="server"><img alt="Find" src="images/find.gif" border="0"></a></td>
				</tr>
				<tr>
					<td>
						<asp:label id="lblActionCodes" runat="server" cssclass="csPlainText">Action Codes</asp:label></td>
					<td>
						<table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td>
									<cc1:radiobuttonsearch id="rabPrintAllExceptNPW" runat="server" text='Print all action codes except the one "Notify publisher in writing"'
										groupname="Choice" checked="True" parametername="PrintAll" contenttype="bool" cssclass="csPlainText"></cc1:radiobuttonsearch></td>
							</tr>
							<tr>
								<td style="HEIGHT: 20px">
									<cc1:radiobuttonsearch id="rabJustFlaggedNPW" runat="server" text='Just the ones flagged as "Notify publisher in writing"'
										groupname="Choice" parametername="JustOne" contenttype="bool" cssclass="csPlainText"></cc1:radiobuttonsearch></td>
							</tr>
							<tr>
								<td>
									<cc1:radiobuttonsearch id="rabIndividualAction" runat="server" parametername="ByIndividual" groupname="Choice"
										text="By individual action code, including all without restriction" contenttype="bool" cssclass="csPlainText"></cc1:radiobuttonsearch></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><asp:label id="Label12" runat="server" cssclass="csPlainText">Select Action</asp:label></td>
					<td>
						<cc1:dropdownlistsearch id="ddlAction" runat="server" parametername="ActionInstance" enabled="False" contenttype="int"></cc1:dropdownlistsearch></td>
				</tr>
				<tr>
					<td>
						<asp:label id="Label6" cssclass="csPlainText" runat="server">Remove Automated Actions</asp:label></td>
					<td>
						<cc1:checkboxsearch id="chkRemoveAutomated" runat="server" parametername="RemoveAutomated" contenttype="bool"></cc1:checkboxsearch></td>
				</tr>
            <tr>
					<td>
						<asp:label id="Label13" cssclass="csPlainText" runat="server">Campaign ID</asp:label></td>
					<td>
						<cc1:textboxinteger id="tbxCampaignID" runat="server" emptyvalue="0"></cc1:textboxinteger>
					</td>
				</tr>
            <tr>
					<td>
						<asp:label id="Label15" cssclass="csPlainText" runat="server">Account ID</asp:label></td>
					<td>
						<cc1:textboxinteger id="tbxAccountID" runat="server" emptyvalue="0"></cc1:textboxinteger>
					</td>
				</tr>
				<tr>
					<td style="HEIGHT: 34px" colspan="2"></td>
				</tr>
				<tr>
					<td align="center" colspan="2"><asp:button id="btnSubmit" runat="server" text="Submit"></asp:button></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<cc3:rsgeneration id="rsGenerationSwitchLetter" runat="server" reportname="SwitchLetter" Mode="PopUp"></cc3:rsgeneration>
