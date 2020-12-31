<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AccountMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.AccountMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="VendorMaintenanceControl" Src="VendorMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AddressListMaintenanceControl" Src="AddressListMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PhoneListMaintenanceControl" Src="PhoneListMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccountGeneralInformationControl" Src="AccountGeneralInformationControl.ascx" %>
<div style="TEXT-ALIGN: center">
	<asp:button id="btnSubmitTop" runat="server" cssclass="boxlook" text="Save" onclick="btnSubmit_Click"></asp:button>
	<asp:button id="btnSaveNewTop" runat="server" cssclass="boxlook" text="Save as new group" visible="False" onclick="btnSaveNew_Click"></asp:button>
	<asp:button id="btnNewCampaignTop" runat="server" cssclass="boxlook" text="New campaign for this group" onclick="btnNewCampaign_Click"></asp:button>
</div>
<br>
<asp:label id="lblAccountID" cssclass="csPlainText" runat="server" font-bold="True"></asp:label>
<br>
<br>
<uc1:accountgeneralinformationcontrol id="ctrlAccountGeneralInformationControl" runat="server"></uc1:accountgeneralinformationcontrol>
<br>
<br>
<table width="100%">
	<tr>
		<td style="WIDTH: 50%; TEXT-ALIGN: left" valign="top">
			<uc1:addresslistmaintenancecontrol id="ctrlAddressListMaintenanceControl" runat="server"></uc1:addresslistmaintenancecontrol>
		</td>
		<td style="WIDTH: 50%; TEXT-ALIGN: right" valign="top">
			<table id="Table3" cellspacing="0" cellpadding="0" width="90%" bgcolor="#cecece" border="0">
				<tr>
					<td>
						<table class="CSTable" id="Table4" cellspacing="1" cellpadding="2" width="100%">
							<tr>
								<td valign="top" height="20"><asp:label id="lblTitle2" cssclass="CSTitle" runat="server">Phone Information</asp:label></td>
							</tr>
							<tr bgcolor="#ffffff">
								<td valign="top">
									<div style="OVERFLOW: auto; HEIGHT: 405px">
										<uc1:phonelistmaintenancecontrol id="ctrlPhoneListMaintenanceControl" runat="server" ShowMainPhoneReadOnly="True"></uc1:phonelistmaintenancecontrol>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<br>
<br>
<div id="divVendorInformation" runat="server">
	<uc1:vendormaintenancecontrol id="ctrlVendorMaintenanceControl" runat="server"></uc1:vendormaintenancecontrol>
	<br>
	<br>
</div>
<div style="TEXT-ALIGN: center">
	<asp:button id="btnSubmitBottom" runat="server" cssclass="boxlook" text="Save" onclick="btnSubmit_Click"></asp:button>
	<asp:button id="btnSaveNewBottom" runat="server" cssclass="boxlook" text="Save as new group"
		visible="False" onclick="btnSaveNew_Click"></asp:button>
	<asp:button id="btnNewCampaignBottom" runat="server" cssclass="boxlook" text="New campaign for this group" onclick="btnNewCampaign_Click"></asp:button>
</div>
<br>
