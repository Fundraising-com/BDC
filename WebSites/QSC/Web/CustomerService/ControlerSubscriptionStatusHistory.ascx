<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerSubscriptionStatusHistory.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerSubscriptionStatusHistory" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<table cellspacing="0" cellpadding="1" width="90%" bgcolor="#000000" border="0">
	<tr>
		<td>
			<table cellspacing="0" width="100%" bgcolor="#ffffff" border="0">
				<tr>
					<td class="CSTableHeader">Subscription Status History</td>
				</tr>
				<tr>
					<td>
						<table cellspacing="0" cellpadding="2" width="90%" border="0">
							<tr>
								<td width="50%"><asp:label id="lblOESentTitle" runat="server" cssclass="csPlainText">OE Report Sent</asp:label></td>
								<td width="50%"><asp:label id="lblOESent" runat="server" cssclass="csPlainText"></asp:label></td>
							</tr>
							<tr id="trOEDate" runat="server">
								<td width="50%"><asp:label id="lblOEDateTitle" runat="server" cssclass="csPlainText">OE Report Date</asp:label></td>
								<td width="50%"><asp:label id="lblOEDate" runat="server" cssclass="csPlainText"></asp:label></td>
							</tr>
						</table>
						<br>
						<asp:label id="lblMessage" runat="server"></asp:label><cc2:datagridobject id="dtgMain" runat="server" width="100%" cssclass="CSTableSubHeader" gridlines="None"
							borderstyle="None" showfooter="True" autogeneratecolumns="False" searchmode="0">
							<itemstyle cssclass="CSTableSubHeader"></itemstyle>
							<headerstyle font-bold="True" cssclass="CSTableSubHeader"></headerstyle>
							<columns>
								<asp:boundcolumn datafield="RemitBatchID" headertext="Run ID"></asp:boundcolumn>
								<asp:templatecolumn headertext="Date">
									<itemtemplate>
										<asp:label id="lblDate" runat="server" text='<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateChanged")) != new DateTime(1995, 1, 1) ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateChanged")).ToString("MM/dd/yyyy") : String.Empty %>'>
										</asp:label>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:boundcolumn datafield="StatusDescription" headertext="Status"></asp:boundcolumn>
							</columns>
						</cc2:datagridobject></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
