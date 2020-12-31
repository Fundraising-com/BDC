<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AccountGeneralInformationControl.ascx.cs" Inherits="QSPFulfillment.AcctMgt.Control.AccountGeneralInformationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="LastContactViewerControl" Src="LastContactViewerControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<table id="Table3" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecece" border="0">
	<tr>
		<td>
			<table class="CSTable" id="Table4" cellSpacing="1" cellPadding="2" width="100%">
				<tr>
					<td vAlign="top" height="20"><asp:label id="lblTitle2" runat="server" cssclass="CSTitle">General Group Information</asp:label></td>
				</tr>
				<tr bgColor="#ffffff">
					<td vAlign="top">
						<table id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0">
							<tr>
								<td style="WIDTH: 19.88%" vAlign="middle"><asp:label id="Label2" runat="server" cssclass="CSPlainText">Name</asp:label></td>
								<td style="WIDTH: 25.22%" vAlign="middle"><cc1:textboxreq id="tbxName" runat="server" required="True" errormsgrequired="The field Name is mandatory."
										maxlength="50" columns="25"></cc1:textboxreq></td>
								<td style="WIDTH: 25%" vAlign="middle"><asp:label id="Label5" runat="server" cssclass="CSPlainText">Master Group ID</asp:label></td>
								<td style="WIDTH: 25%" vAlign="middle"><cc1:textboxinteger id="tbxParentGroupID" runat="server" errormsgrequired="The field Parent Group ID is mandatory."
										maxlength="25" errormsgregexp="The field Parent Group ID has to be a number."></cc1:textboxinteger><input class="boxlook" id="btnParentIDSearch" type="button" value="Search" runat="server">
								</td>
							</tr>
							<tr>
								<td style="WIDTH: 19.88%; HEIGHT: 16px" vAlign="middle"><asp:label id="Label4" runat="server" cssclass="CSPlainText"> Language</asp:label></td>
								<td style="WIDTH: 25.22%; HEIGHT: 16px" vAlign="middle"><cc1:dropdownlistreq id="ddlLanguage" runat="server" required="True" errormsgrequired="The field Language is mandatory."
										initialtext="Please select...">
										<asp:listitem selected="True" value="">Please select...</asp:listitem>
										<asp:listitem value="EN">EN</asp:listitem>
										<asp:listitem value="FR">FR</asp:listitem>
									</cc1:dropdownlistreq></td>
								<td style="WIDTH: 25%; HEIGHT: 16px" vAlign="middle"><asp:label id="Label8" runat="server" cssclass="CSPlainText">Status</asp:label></td>
								<td style="WIDTH: 25%; HEIGHT: 16px" vAlign="middle"><cc1:dropdownlistreq id="ddlStatus" runat="server" required="True" errormsgrequired="The field Status is mandatory."
										initialtext="Please select..." initialvalue="0"></cc1:dropdownlistreq></td>
							</tr>
							<TR>
								<TD style="WIDTH: 19.88%; HEIGHT: 16px" vAlign="middle"><asp:label id="Label1s" runat="server" cssclass="CSPlainText">Group Class</asp:label></TD>
								<TD style="WIDTH: 25.22%; HEIGHT: 16px" vAlign="middle"><asp:DropDownList id="ddlGroupClass" runat="server" required="True"  errormsgrequired="The field Group Class is mandatory." autopostback="True"></asp:DropDownList></TD>
								<TD style="WIDTH: 25%; HEIGHT: 16px" vAlign="middle"><asp:label id="Label13" runat="server" cssclass="CSPlainText" Visible="False">Field Manager</asp:label></TD>
								<TD style="WIDTH: 25%; HEIGHT: 16px" vAlign="middle"><cc1:dropdownlistreq id="ddlFM" runat="server" errormsgrequired="The FM info is mandatory." Width="160px"
										Visible="False"></cc1:dropdownlistreq></TD>
							</TR>
							<tr>
								<td style="WIDTH: 19.88%" vAlign="middle"><asp:label id="Label6" runat="server" cssclass="CSPlainText">Group Code</asp:label></td>
								<td style="WIDTH: 25.22%" vAlign="middle"><cc1:dropdownlistreq id="ddlGroupCode" runat="server" required="True" errormsgrequired="The field Group Code is mandatory."
										initialtext="Please select..." AutoPostBack="False"></cc1:dropdownlistreq></td>
								<td style="WIDTH: 25%" vAlign="middle" rowSpan="3"><asp:label id="Label3s" runat="server" cssclass="CSPlainText">Contacts</asp:label></td>
								<td style="WIDTH: 25%" vAlign="middle" rowSpan="3"><uc1:lastcontactviewercontrol id="ctrlLastContactViewerControl" runat="server"></uc1:lastcontactviewercontrol><input class="boxlook" id="btnEditContacts" disabled type="button" value="Edit Contacts"
										name="btnEditContacts" runat="server">
								</td>
							<tr>
								<td style="WIDTH: 19.88%" vAlign="middle"><asp:label id="Label7" runat="server" cssclass="CSPlainText">Private Organization</asp:label></td>
								<td style="WIDTH: 25.22%" vAlign="middle"><asp:radiobuttonlist id="rblPrivateOrganization" runat="server" cssclass="csPlainText" repeatdirection="Horizontal">
										<asp:listitem value="True">Yes</asp:listitem>
										<asp:listitem value="False" selected="True">No</asp:listitem>
									</asp:radiobuttonlist></td>
							<tr>
								<td style="WIDTH: 19.88%" vAlign="middle"><asp:label id="Label9" runat="server" cssclass="CSPlainText">Enrollment</asp:label></td>
								<td style="WIDTH: 25.22%" vAlign="middle"><cc1:textboxinteger id="tbxEnrollment" runat="server" required="True" errormsgrequired="The field Enrollment is mandatory."
										maxlength="50" errormsgregexp="The field Enrollment has to be a number."></cc1:textboxinteger></td>
							<tr>
								<td style="WIDTH: 19.88%" vAlign="middle"><asp:label id="Label10" runat="server" cssclass="CSPlainText">Email</asp:label></td>
								<td style="WIDTH: 25.22%" vAlign="middle"><cc1:email id="tbxEmail" runat="server" maxlength="75" errormsgrequired="The field E-mail is mandatory."
										errormsgregexp="The field E-mail address is invalid."></cc1:email></td>
								<td style="WIDTH: 25%" vAlign="middle" rowSpan="2"><asp:label id="Label11" runat="server" cssclass="CSPlainText">Comments</asp:label></td>
								<td style="WIDTH: 25%" vAlign="middle" rowSpan="2"><asp:textbox id="tbxComments" runat="server" maxlength="1000" textmode="MultiLine" width="100%"
										height="80px"></asp:textbox></td>
							</tr>
							<tr>
								<td style="WIDTH: 19.88%" vAlign="middle"><asp:label ID="Label12" runat="server" cssclass="CSPlainText">Profit Cheque Payable To</asp:label></td>
								<td style="WIDTH: 25.22%" vAlign="middle"><asp:textbox id="tbxProfitChequePayee" runat="server" maxlength="50" width="70%"></asp:textbox></td></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
