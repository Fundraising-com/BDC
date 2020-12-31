<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SeasonListMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.Fulfillment.Control.SeasonListMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="SeasonListControl" Src="SeasonListControl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ControlerYesNoConfirmation" Src="../../Fulfillment/Control/ControlerYesNoConfirmation.ascx" %>
<%@ Register TagPrefix="uc3" TagName="SeasonMaintenanceControl" Src="SeasonMaintenanceControl.ascx" %>
<uc1:seasonlistcontrol id="ctrlSeasonListControl" runat="server" showselect="true"></uc1:seasonlistcontrol><br>
<br>
<table>
	<tr>
		<td>
			<asp:button id="btnAddNew" runat="server" text="Add New Season" cssclass="boxlook" onclick="btnAddNew_Click"></asp:button>
		</td>
		<td>
			<asp:button id="btnInsertNewFiscalYear" runat="server" text="Insert New Fiscal Year" cssclass="boxlook" onclick="btnInsertNewFiscalYear_Click"></asp:button>
		</td>
	</tr>
</table>
<br>
<br>
<table id="tblSeasonMaintenance" cellspacing="0" cellpadding="0" width="75%" bgcolor="#cecece"
	border="0" runat="server" visible="false">
	<tr>
		<td>
			<table class="CSTable" id="tblSeasonMaintenance2" cellspacing="1" cellpadding="2" width="100%">
				<tr>
					<td valign="top" colspan="2" height="20"><asp:label id="lblTitle2" cssclass="CSTitle" runat="server">Season Details</asp:label></td>
				</tr>
				<tr bgcolor="#ffffff">
					<td>
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td valign="top">
									<uc3:SeasonMaintenanceControl id="ctrlSeasonMaintenanceControl" runat="server"></uc3:SeasonMaintenanceControl>
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
<uc2:ControlerYesNoConfirmation id="ctrlControlerYesNoConfirmation" runat="server"></uc2:ControlerYesNoConfirmation>