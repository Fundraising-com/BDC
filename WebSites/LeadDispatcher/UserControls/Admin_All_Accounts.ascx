<%@ Register TagPrefix="uc1" TagName="AllAccountsForLead" Src="AllAccountsForLead.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Admin_All_Accounts.ascx.cs" Inherits="CRMWeb.UserControls.Admin_All_Accounts" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="300" border="0">
	<TR>
		<TD style="HEIGHT: 7px">
			<asp:label id="lblNbUnassigned" ForeColor="#294585" Font-Bold="True" Font-Size="10pt" Font-Names="Microsoft Sans Serif"
				Width="424px" runat="server" BackColor="#F7F7F7">Search For QSP Leads </asp:label>
		</TD>
		<TD style="HEIGHT: 7px"></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 41px">
			<TABLE id="Table2" style="WIDTH: 160px; HEIGHT: 38px" cellSpacing="0" cellPadding="0" width="160"
				border="0">
				<TR>
					<TD style="WIDTH: 62px">
						<asp:label id="Label16" ForeColor="#6695C3" Font-Bold="True" Font-Size="8pt" Font-Names="Microsoft Sans Serif"
							Width="56px" runat="server">Lead ID:</asp:label></TD>
					<TD>
						<asp:textbox id="txtLeadID" Width="72px" runat="server" BorderWidth="1px" BorderColor="#6695C3"
							BorderStyle="Solid"></asp:textbox></TD>
					<TD>
						<asp:ImageButton id="ImageButton1" Width="32px" runat="server" ImageUrl="../images/Continue.gif"></asp:ImageButton></TD>
				</TR>
			</TABLE>
		</TD>
		<TD style="HEIGHT: 41px"></TD>
	</TR>
	<TR>
		<TD>
			<uc1:AllAccountsForLead id="AllAccountsForLead1" runat="server"></uc1:AllAccountsForLead></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD></TD>
	</TR>
</TABLE>
