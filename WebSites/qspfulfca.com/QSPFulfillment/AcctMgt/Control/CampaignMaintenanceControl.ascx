<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CampaignMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.CampaignMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSPFulfillment.AcctMgt.Control" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="uc1" TagName="CampaignGeneralInformationControl" Src="CampaignGeneralInformationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FieldSuppliesMaintenanceControl" Src="FieldSuppliesMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CampaignProgramMaintenanceControl" Src="CampaignProgramMaintenanceControl.ascx" %>
<div style="TEXT-ALIGN: center">
	<asp:button id="btnSubmitTop" runat="server" text="Save" cssclass="boxlook"></asp:button>
	<asp:button id="btnSaveNewTop" runat="server" cssclass="boxlook" text="Save as new campaign"
		visible="False"></asp:button>
</div>
<br>
<div id="divReportLinksTop" runat="server" style="TEXT-ALIGN: right">
	<cc1:casummaryhyperlink id="hypCASummaryTop" runat="server">Print Sum. Form</cc1:casummaryhyperlink>
	&nbsp;
	<cc1:confirmationagreementlinkbutton id="hypCATop" runat="server">Preview / Print CA</cc1:confirmationagreementlinkbutton>
	<br>
	<br>
</div>
<asp:label id="lblAccountInfo" runat="server" cssclass="csPlainText" font-bold="True" font-size="Larger"></asp:label><br>
<asp:label id="lblCampaignID" runat="server" cssclass="csPlainText" font-bold="True"></asp:label>
<br>
<asp:label id="lblOrderCount" runat="server" cssclass="csPlainText" 
    font-bold="True"></asp:label>
<br>
<uc1:campaigngeneralinformationcontrol id="ctrlCampaignGeneralInformationControl" runat="server"></uc1:campaigngeneralinformationcontrol><br>
<br>
<div style="TEXT-ALIGN: center">
	<table id="Table3" cellspacing="0" cellpadding="0" width="100%" bgcolor="#cecece" border="0">
		<tr>
			<td>
				<table class="CSTable" id="Table4" cellspacing="1" cellpadding="2" width="100%">
					<tr>
						<td valign="top" height="20"><asp:label id="lblTitle2" runat="server" cssclass="CSTitle">Program Selection / Field Supplies Maintenance</asp:label></td>
					</tr>
					<tr bgcolor="#ffffff">
						<td valign="top" width="60%">
							<uc1:campaignprogrammaintenancecontrol id="ctrlCampaignProgramMaintenanceControl" runat="server"></uc1:campaignprogrammaintenancecontrol>
						</td>
						<td valign="top" width="40%">
							<uc1:fieldsuppliesmaintenancecontrol id="ctrlFieldSuppliesMaintenanceControl" runat="server"></uc1:fieldsuppliesmaintenancecontrol>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br>
	<br>
</div>
<div id="divReportLinksBottom" runat="server" style="TEXT-ALIGN: right">
	<cc1:casummaryhyperlink id="hypCASummaryBottom" runat="server">Print Sum. Form</cc1:casummaryhyperlink>
	&nbsp;
	<cc1:confirmationagreementlinkbutton id="hypCABottom" runat="server">Preview / Print CA</cc1:confirmationagreementlinkbutton>
	<br>
	<br>
</div>
<div style="TEXT-ALIGN: center">
	<asp:button id="btnSubmitBottom" runat="server" text="Save" cssclass="boxlook"></asp:button>
	<asp:button id="btnSaveNewBottom" runat="server" cssclass="boxlook" text="Save as new campaign"
		visible="False"></asp:button>
</div>
<br>
