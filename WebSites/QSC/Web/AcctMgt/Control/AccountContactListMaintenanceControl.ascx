<%@ Register TagPrefix="uc1" TagName="ContactMaintenanceControl" Src="ContactMaintenanceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ContactListControl" Src="ContactListControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AccountContactListMaintenanceControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.AccountContactListMaintenanceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div style="TEXT-ALIGN: center"><asp:button id="btnSubmitTop" runat="server" cssclass="boxlook" text="Save" onclick="btnSubmit_Click"></asp:button>&nbsp;<asp:button id="btnCloseTop" runat="server" cssclass="boxlook" text="Close" causesvalidation="False" onclick="btnCancel_Click"></asp:button></div>
<br>
<br>
<uc1:contactlistcontrol id="ctrlContactListControl" runat="server" showcopyto="false" showselect="true"></uc1:contactlistcontrol><br>
<br>
<asp:button id="btnAddNew" runat="server" text="Add New Contact" cssclass="boxlook" onclick="btnAddNew_Click"></asp:button><br>
<br>
<br>
<table id="tblContactMaintenance" cellspacing="0" cellpadding="0" width="75%" bgcolor="#cecece"
	border="0" runat="server" visible="false">
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
								<td valign="top"><uc1:contactmaintenancecontrol id="ctrlContactMaintenanceControl" runat="server" fixedmode="true" isprimaryvisible="true"
										showonephone="False" isphonerequired="True" addressclientvisible="False"></uc1:contactmaintenancecontrol></td>
							</tr>
							<tr>
								<td>
									<asp:label id="lblPrimaryNote" runat="server" cssclass="CSPlainText" font-bold="True">NOTE: Setting a contact as primary will update all current and future campaign contacts for this group.</asp:label>
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
<div style="TEXT-ALIGN: center"><asp:button id="btnSubmitBottom" runat="server" cssclass="boxlook" text="Save" onclick="btnSubmit_Click"></asp:button>&nbsp;<asp:button id="btnCloseBottom" runat="server" cssclass="boxlook" text="Close" causesvalidation="False" onclick="btnCancel_Click"></asp:button></div>
