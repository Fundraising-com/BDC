<%@ Register TagPrefix="uc1" TagName="UserInformation" Src="../../Components/User/BasicUserInformation/UserInformation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchForm" Src="../../Components/User/Search/SearchForm.ascx" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="efundraising.EFundraisingCRMWeb.Online.ProcessChecks._Default" %>
<%@ Register TagPrefix="cc1" Namespace="efundraising.Web.UI.InputControls" Assembly="efundraising.Web.UI.InputControls" %>
<form><!--<efundraising:masterpage id="MasterPage1" runat="server" master="~/MasterPages/CrmView.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">-->
		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD class="FrameBorder" height="1"></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="185"></TD>
				<TD class="FrameBody" height="185">
					<TABLE cellSpacing="10" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="top"><B>Event Ids:</B>
								<asp:TextBox id="EventIdsTextBox" Runat="server" CssClass="Normaltext"></asp:TextBox></TD>
							<TD vAlign="top"><B>Start Date</B>&nbsp;&nbsp;
								<cc1:DateTextBox id="DateTextBox1" runat="server"></cc1:DateTextBox><!--DisableControl="True" ReadOnly="True" Enabled="False" --></TD>
							<TD vAlign="top"><B>End Date</B>&nbsp;&nbsp;
								<cc1:DateTextBox id="DateTextBox2" runat="server"></cc1:DateTextBox></TD>
						</TR>
					</TABLE>
					<TABLE cellSpacing="10" cellPadding="0" width="100%" border="0">
						<TR>
							<TD vAlign="top">
								<asp:Button id="ProcessButton" runat="server" CssClass="buttonFlat CursorPointer" ToolTip="Press here to start pre-payment process"
									ButtonType="PushButton" Text="Process Pre-payments"></asp:Button></TD>
						</TR>
						<TR>
							<TD>
								<asp:Button id="DPCButton" style="MARGIN-LEFT: 0px" Runat="server" CssClass="buttonFlat CursorPointer"
									ToolTip="Generate Double PostCard Check File" ButtonType="PushButton" Text="Generate Check File"
									CausesValidation="false" NOVALIDATION></asp:Button></TD>
						</TR>
					</TABLE>
					<TABLE cellSpacing="10" cellPadding="0" width="100%" border="0">
						<TR>
							<TD align="center">
								<asp:Button id="Button2" runat="server" Text="CA" ForeColor="Red" Font-Bold="True" Font-Size="40pt"></asp:Button>
								<asp:Label id="Label1" runat="server" CssClass="BigTextBold Active"></asp:Label></TD>
						</TR>
					</TABLE>
					<asp:Button id="Button1" style="MARGIN-LEFT: 0px" Runat="server" CssClass="buttonFlat CursorPointer"
						ToolTip="Reissue" ButtonType="PushButton" Text="Reissue" CausesValidation="false" NOVALIDATION></asp:Button>
						
						</TD>
				<TD class="FrameBorder" width="1" height="185"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD class="FrameBorder" height="1">
					<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="1">
						<TR>
							<TD>Payments</TD>
							<TD>
								<asp:TextBox id="paymentTextBox" runat="server"></asp:TextBox></TD>
							<TD></TD>
						</TR>
						<TR>
							<TD>MONTH</TD>
							<TD>
								<asp:TextBox id="MonthTextBox" runat="server">7</asp:TextBox></TD>
							<TD>
								<asp:TextBox id="YearTextBox" runat="server" Width="40px" Rows="4">2008</asp:TextBox></TD>
						</TR>
						<TR>
							<TD>Events</TD>
							<TD>
								<asp:TextBox id="eventTextBox" runat="server"></asp:TextBox></TD>
							<TD></TD>
						</TR>
						<TR>
						<TR>
						<TD height="10">
				
				              <asp:Button id="validateButton" runat="server" Text="Validate"></asp:Button>
				
				     </TD>
				        <TD>
								<asp:TextBox id="fileNameTextBox" runat="server">.txt</asp:TextBox></TD>
				     
				     </TR>
					
						
					</TABLE>
					
				</TD>
				
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
		</TABLE>
	<!--</efundraising:Content>
</efundraising:masterpage>-->
</form>
