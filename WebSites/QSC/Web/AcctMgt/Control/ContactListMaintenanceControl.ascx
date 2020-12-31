<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ContactListMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.ContactListMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table3" cellspacing="0" cellpadding="0" width="100%" bgcolor="#cecece" border="0">
	<tr>
		<td>
			<table class="CSTable" id="Table4" cellspacing="1" cellpadding="2" width="100%">
				<tr>
					<td valign="top" height="20"><asp:label id="lblTitle2" cssclass="CSTitle" runat="server">Contact Information</asp:label></td>
				</tr>
				<tr bgcolor="#ffffff">
					<td valign="top">
						<asp:placeholder id="plhContactList" runat="server"></asp:placeholder>
						<br>
						<div style="TEXT-ALIGN: center">
							<asp:button id="btnAddNew" runat="server" text="Add a new Contact" cssclass="boxlook" causesvalidation="False" onclick="btnAddNew_Click"></asp:button>
						</div>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
