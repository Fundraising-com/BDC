<%@ Register TagPrefix="uc1" TagName="AddressMaintenanceControl" Src="AddressMaintenanceControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AddressListMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.AddressListMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table3" cellSpacing="0" cellPadding="0" width="90%" bgColor="#cecece" border="0">
	<tr>
		<td>
			<table class="CSTable" id="Table4" cellSpacing="1" cellPadding="2" width="100%">
				<tr>
					<td vAlign="top" height="20"><asp:label id="lblTitle2" cssclass="CSTitle" runat="server">Address Information</asp:label></td>
				</tr>
				<tr bgColor="#ffffff">
					<td vAlign="top">
						<div style="TEXT-ALIGN: center"><uc1:addressmaintenancecontrol id="ctrlAddressMaintenanceControlShipTo" runat="server" defaultaddresstype="54001"></uc1:addressmaintenancecontrol><br>
							<hr width="100%">
							<table style="WIDTH: 100%">
								<tr>
									<td style="WIDTH: 50%"></td>
									<td style="WIDTH: 50%"><input class="boxlook" id="btnCopyFromShipTo" type="button" value="Copy From Ship To" runat="server" onserverclick="btnCopyFromShipTo_ServerClick">
									</td>
								</tr>
							</table>
							<uc1:addressmaintenancecontrol id="ctrlAddressMaintenanceControlBillTo" runat="server" defaultaddresstype="54002"></uc1:addressmaintenancecontrol></div>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
