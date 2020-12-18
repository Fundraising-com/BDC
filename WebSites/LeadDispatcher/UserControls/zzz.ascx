<%@ Control Language="c#" AutoEventWireup="True" Codebehind="zzz.ascx.cs" Inherits="CRMWeb.UserControls.zzz" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="AllAccountsForLead" Src="AllAccountsForLead.ascx" %>
<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="300" border="0" style="WIDTH: 300px; HEIGHT: 600px">
	<TR>
		<TD style="HEIGHT: 3px">
			<TABLE id="Table4" style="WIDTH: 160px; HEIGHT: 38px" cellSpacing="0" cellPadding="0" width="160"
				border="0">
				<TR>
					<TD style="WIDTH: 62px">
						<asp:label id="Label16" runat="server" Width="56px" ForeColor="#6695C3" Font-Bold="True" Font-Names="Microsoft Sans Serif"
							Font-Size="8pt">Lead ID:</asp:label></TD>
					<TD>
						<asp:textbox id="txtLeadID" runat="server" Width="72px" BorderStyle="Solid" BorderColor="#6695C3"
							BorderWidth="1px"></asp:textbox></TD>
					<TD>
						<asp:ImageButton id="cmdGo" runat="server" Width="32px" ImageUrl="../images/Continue.gif"></asp:ImageButton></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD vAlign="top">
			<TABLE id="Table1" style="WIDTH: 214px; HEIGHT: 50px" borderColor="#6695c3" cellSpacing="0"
				cellPadding="0" width="214" bgColor="#f7f7f7" border="1">
				<TR>
					<TD vAlign="top">
						<asp:label id="lblNoAccounts" Font-Bold="True" ForeColor="IndianRed" Width="208px" runat="server"
							Visible="False">No Accounts Found</asp:label>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="300" border="0">
							<TR>
								<TD>
									<uc1:AllAccountsForLead id="AllAccountsForLead1" runat="server"></uc1:AllAccountsForLead></TD>
							</TR>
							<TR>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
