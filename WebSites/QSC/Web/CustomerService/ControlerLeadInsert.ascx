<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerLeadInsert.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerLeadInsert" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<asp:Label id="lblMessage" CssClass="CSDirections" runat="server"></asp:Label>
<TABLE class="CSTable" id="Table2" cellSpacing="0" cellPadding="2">
	<TR>
		<br>
		<TD class="CSTableHeader" colSpan="2">Lead Information</TD>
	</TR>
	<TR>
		<TD align="center">
			<TABLE id="Table3">
				<TR>
					<TD colSpan="2"></TD>
				</TR>
				<TR>
					<TD><BR>
						<asp:Label id="Label1" runat="server" cssClass="csPlainText">Contact Name</asp:Label></TD>
					<TD><BR>
						<asp:TextBox id="tbxContactName" runat="server" maxlength="100"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label2" runat="server" cssClass="csPlainText">Home Phone #</asp:Label></TD>
					<TD>
						<cc1:Phone id="tbxContactHomePhoneNumber" runat="server"></cc1:Phone></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label8" runat="server" cssClass="csPlainText">Work Phone # </asp:Label></TD>
					<TD>
						<cc1:Phone id="tbxContactWorkPhoneNumber" runat="server"></cc1:Phone></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label14" runat="server" cssClass="csPlainText">Fax # </asp:Label></TD>
					<TD>
						<cc1:Phone id="tbxContactFaxNumber" runat="server"></cc1:Phone></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label9" runat="server" cssClass="csPlainText">Email</asp:Label></TD>
					<TD>
						<cc1:EMail id="tbxContactEMail" runat="server"></cc1:EMail></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label10" runat="server" cssClass="csPlainText">School/Group</asp:Label></TD>
					<TD>
						<asp:TextBox id="tbxSchoolGroup" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label33" runat="server" cssClass="csPlainText">City</asp:Label></TD>
					<TD>
						<asp:TextBox id="tbxCity" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label7" runat="server" cssClass="csPlainText">Postal Code</asp:Label></TD>
					<TD>
						<cc1:PostalCode id="tbxPostalCode" runat="server"></cc1:PostalCode></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label5" runat="server" cssClass="csPlainText">Province</asp:Label></TD>
					<TD>
						<cc1:dropdownlistprovince id="ddlProvince" runat="server" Code="CA"></cc1:dropdownlistprovince></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label11" runat="server" cssClass="csPlainText"> What are you interested in? </asp:Label></TD>
					<TD>
						<asp:TextBox id="tbxIntersedinWhat" runat="server" TextMode="MultiLine"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>
						<asp:Label id="Label12" runat="server" cssClass="csPlainText">Where did you hear about QSP ?</asp:Label></TD>
					<TD>
						<asp:TextBox id="tbxWhereHearAboutQSP" runat="server" TextMode="MultiLine"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 22px">
						<asp:Label id="Label13" runat="server" cssClass="csPlainText">Comments</asp:Label></TD>
					<TD style="HEIGHT: 22px">
						<asp:TextBox id="tbxComments" runat="server" TextMode="MultiLine"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 22px"></TD>
					<TD style="HEIGHT: 22px"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">
						<asp:Button id="btnSave" runat="server" Text="Save" onclick="btnSave_Click"></asp:Button>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
