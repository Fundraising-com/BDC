<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Register TagPrefix="uc1" TagName="SearchForm" Src="../../Components/User/Search/SearchForm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserInformation" Src="../../Components/User/BasicUserInformation/UserInformation.ascx" %>
<%@ Page language="c#" Codebehind="Preview.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.Online.EmailTemplate.Preview" %>
<%@ Register TagPrefix="uc1" TagName="Preview" Src="../../Components/User/EmailTemplate/Preview.ascx" %>
<efundraising:masterpage id="MasterPage1" runat="server" master="~/MasterPages/CrmView.ascx">
	<efundraising:Content id="Content1" runat="server" ContentPlaceHolderID="cph_PageContent">
		<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<TR>
				<TD class="FrameBorder" width="1" height="1"></TD>
				<TD class="FrameBorder" height="1"></TD>
				<TD class="FrameBorder" width="1" height="1"></TD>
			</TR>
			<TR>
				<TD class="FrameBorder" width="1"></TD>
				<TD class="FrameBody">
					<TABLE cellSpacing="10" cellPadding="0" border="0">
						<TR>
							<TD vAlign="top">
								<uc1:Preview id="Preview1" runat="server"></uc1:Preview></TD>
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
