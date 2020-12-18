<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="efundraising.EFundraisingCRMWeb.Contacts.Home._Default" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Register TagPrefix="uc1" TagName="SearchForm" Src="../../Components/User/Search/SearchForm.ascx" %>
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
					Home
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
