<%@ Control Language="c#" AutoEventWireup="True" Codebehind="VendorMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.VendorMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" bgcolor="#cecece" border="0">
	<tr>
		<td>
			<table class="CSTable" id="Table4" cellspacing="1" cellpadding="2" width="100%">
				<tr>
					<td valign="top" height="20"><asp:label id="lblTitle2" runat="server" cssclass="CSTitle">Vendor Information</asp:label></td>
				</tr>
				<tr bgcolor="#ffffff">
					<td valign="top">
						<table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
							<tr>
								<td width="50%">
									<asp:label id="Label1" runat="server" cssclass="csPlainText">Vendor Number</asp:label></td>
								<td width="50%">
									<asp:textbox id="tbxVendorNumber" runat="server" columns="30" maxlength="30"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td width="50%">
									<asp:label id="Label2" runat="server" cssclass="csPlainText">Vendor Site Name</asp:label></td>
								<td width="50%">
									<asp:textbox id="tbxVendorSiteName" runat="server" columns="30" maxlength="15"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td width="50%">
									<asp:label id="Label3" runat="server" cssclass="csPlainText">Vendor Pay Group</asp:label></td>
								<td width="50%">
									<asp:textbox id="tbxVendorPayGroup" runat="server" columns="30" maxlength="25"></asp:textbox>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
