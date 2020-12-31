<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CampaignContactListMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.CampaignContactListMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ContactListControl" Src="ContactListControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ContactMaintenanceControl" Src="ContactMaintenanceControl.ascx" %>
<div style="TEXT-ALIGN: center"><asp:button id="btnSubmitTop" runat="server" cssclass="boxlook" text="Save" onclick="btnSubmit_Click"></asp:button>&nbsp;<asp:button id="btnCancelTop" runat="server" cssclass="boxlook" text="Close" causesvalidation="False" onclick="btnCancel_Click"></asp:button></div>
<br>
<br>
<uc1:contactlistcontrol id="ctrlContactListControl" runat="server" showcopyto="true" showselect="false"></uc1:contactlistcontrol>
<br>
<br>
<asp:checkbox id="chkSameAsShipTo" text="Same as Ship To" cssclass="csPlainText" runat="server"
	textalign="Left"></asp:checkbox>
<br>
<br>
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" bgcolor="#cecece" border="0">
	<tr>
		<td>
			<table class="CSTable" id="Table4" cellspacing="1" cellpadding="2" width="100%">
				<tr>
					<td valign="top" colspan="2" height="20"><asp:label id="lblTitle2" cssclass="CSTitle" runat="server">Contact Information</asp:label></td>
				</tr>
				<tr bgcolor="#ffffff">
					<td>
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td style="WIDTH: 50%" valign="top"><uc1:contactmaintenancecontrol id="ctrlContactMaintenanceControlShipTo" runat="server" fixedmode="true" contacttype="Ship To Contact"
										addressclientvisible="false"></uc1:contactmaintenancecontrol></td>
								<td style="WIDTH: 50%" valign="top"><uc1:contactmaintenancecontrol id="ctrlContactMaintenanceControlBillTo" runat="server" fixedmode="true" contacttype="Bill To Contact"
										addressclientvisible="false" required="false"></uc1:contactmaintenancecontrol></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
<br>
<div style="TEXT-ALIGN: center"><asp:button id="btnSubmitBottom" runat="server" cssclass="boxlook" text="Save" onclick="btnSubmit_Click"></asp:button>&nbsp;<asp:button id="btnCancelBottom" runat="server" cssclass="boxlook" text="Close" causesvalidation="False" onclick="btnCancel_Click"></asp:button></div>
