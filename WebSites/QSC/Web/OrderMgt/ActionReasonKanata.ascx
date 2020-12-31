<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ActionReasonKanata.ascx.cs" Inherits="QSPFulfillment.OrderMgt.ActionReason" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<script language="javascript">
		
	function SetIDIncident(ID)
	{
		var tbxReferToIncident = document.getElementById('ctrlActionReason_tbxReferToIncident');
		tbxReferToIncident.value = ID;
		
	}
	function SetProblemCode(PCID,Description)
	{
		var tbxProblemCode = document.getElementById('ctrlActionReason_tbxProblemCode');
		tbxProblemCode.value = PCID;
		
		var lblDescription = document.getElementById('ctrlActionReason_lblProblemDescription');
		lblDescription.innerHTML = Description;
		
		tbxProblemCode.focus();
	
	}
</script>
<table>
	<tr>
		<td><img height="1" src="images/spacer.gif" width="4"></td>
		<td>
			<table id="Table2" cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td style="HEIGHT: 89px" align="left"><asp:panel id="Panel1" runat="server">
							<table id="Table1" cellspacing="0" cellpadding="0" border="0">
								<tr>
									<td><br>
										<asp:label id="lblCommunication" runat="server" cssclass="csPlainText">Communication Channel</asp:label></td>
								</tr>
								<tr>
									<td>
										<asp:dropdownlist id="ddlCommunicationChanel" runat="server"></asp:dropdownlist></td>
								</tr>
								<tr>
									<td>
										<asp:label id="lblProblemCode" runat="server" cssclass="csPlainText">Problem Code</asp:label></td>
								</tr>
								<tr>
									<td>
										<asp:textbox id="tbxProblemCode" runat="server" width="40px" maxlength="3"></asp:textbox>
										<asp:rangevalidator id="RangeValidator5" runat="server" minimumvalue="1" maximumvalue="2147483647" controltovalidate="tbxProblemCode"
											type="Integer" errormessage="Problem Code must be between 1 and 2147483647.">*</asp:rangevalidator>
										<asp:HyperLink id=hypFindProblemCode Runat="server" ImageUrl='<%#"images/find"+(this.Enabled==false?"_disabled":"")+".gif"%>' NavigateUrl="javascript:void(0);">
										</asp:hyperlink></td>
								</tr>
								<tr>
									<td>
										<asp:label id="lblProblemDescription" runat="server" cssclass="csPlainText"></asp:label></td>
								</tr>
								<tr>
									<td>
										<asp:label id="lblComment" runat="server" cssclass="csPlainText">Comment</asp:label></td>
								</tr>
								<tr>
									<td>
										<asp:textbox id="tbxComment" runat="server" maxlength="500" textmode="MultiLine"></asp:textbox></td>
								</tr>
								<tr>
									<td>
										<asp:label id="Label2" runat="server" cssclass="csPlainText">Refer To Incident</asp:label></td>
								</tr>
								<tr>
									<td>
										<asp:textbox id="tbxReferToIncident" runat="server" width="60px"></asp:textbox>
										<asp:rangevalidator id="RangeValidator1" runat="server" minimumvalue="1" maximumvalue="2147483647" controltovalidate="tbxReferToIncident"
											type="Integer" errormessage="Refer To Incident must be between 1 and 2147483647.">*</asp:rangevalidator>
										<asp:HyperLink id=hypFindIncident Runat="server" ImageUrl='<%#"images/find" + (this.Enabled==false?"_disabled":"") + ".gif"%>' NavigateUrl="javascript:void(0);">
										</asp:hyperlink></td>
								</tr>
								<tr>
									<td>
										<asp:label id="lblCommunicationSource" runat="server" cssclass="csPlainText">Communication Source</asp:label></td>
								</tr>
								<tr>
									<td>
										<asp:dropdownlist id="ddlCommunicationSource" runat="server"></asp:dropdownlist></td>
								</tr>
								<tr>
									<td>
										<asp:label id="Label3" runat="server" cssclass="csPlainText">Status</asp:label></td>
								</tr>
								<tr>
									<td>
										<asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></td>
								</tr>
							</table>
						</asp:panel></td>
				</tr>
				<tr>
					<td align="center"><br>
						<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td align="center"><asp:button id="btnSave" runat="server" text="Save"></asp:button></td>
								<td align="center"><asp:button id="btnNew" runat="server" text="New"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
