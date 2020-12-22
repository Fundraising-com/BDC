<%@ Register TagPrefix="uc1" TagName="PaymentInfoSummary" Src="../../Components/User/PaymentInformation/PaymentInfoSummary.ascx" %>
<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.Online.ConfigurePartner._Default" %>
<%@ Register TagPrefix="uc1" TagName="eSubsPartner" Src="../../Components/User/Partner/eSubsPartner.ascx" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Register TagPrefix="uc1" TagName="SearchForm" Src="../../Components/User/Search/SearchForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserInformation" Src="../../Components/User/BasicUserInformation/UserInformation.ascx" %>
<efundraising:masterpage id="MasterPage1" master="~/MasterPages/CrmView.ascx" runat="server">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD class="FrameBorder" height="1"></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1"></TD>
				<TD class="WhiteBack">
					<TABLE cellSpacing="10" cellPadding="0" border="0">
						<TR>
							<TD vAlign="middle">Partner Name :
							</TD>
							<TD vAlign="middle">
								<asp:DropDownList id="PartnersDropDownList" runat="server" DataTextField="Name" DataValueField="PartnerId"
									AutoPostBack="True" onselectedindexchanged="PartnersDropDownList_SelectedIndexChanged"></asp:DropDownList></TD>
						</TR>
					</TABLE>
				</TD>
				<TD class="FrameBorder" width="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1"></TD> 
				<TD class="WhiteBack">
					<TABLE cellPadding="10" border="0">
						<TR>
							<TD>
								<uc1:eSubsPartner id="ESubsPartner1" runat="server"></uc1:eSubsPartner></TD>
						</TR>
						<TR>
							<TD>
								<asp:Panel id="PaymentInfoPanel" runat="server" Visible="False">
									<uc1:PaymentInfoSummary id="PaymentInfoSummary1" runat="server"></uc1:PaymentInfoSummary>
								</asp:Panel></TD>
						</TR>
						<TR>
							<TD align="right">
								<asp:Button id="CancelButton" runat="server" Text="Cancel" CausesValidation="false" NOVALIDATION onclick="CancelButton_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
								<asp:Button id="SaveButton" runat="server" Text="Save" onclick="SaveButton_Click"></asp:Button></TD>
						</TR>
					</TABLE>
				</TD>
				<TD class="FrameBorder" width="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD class="FrameBorder" height="1"></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
		</TABLE>
	</efundraising:Content>
</efundraising:masterpage>
