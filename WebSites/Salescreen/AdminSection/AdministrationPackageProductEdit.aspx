<%@ Page language="c#" Codebehind="AdministrationPackageProductEdit.aspx.cs" AutoEventWireup="false" Inherits="AdminSection.AdministrationPackageProductEdit" %>
<%@ Register TagPrefix="contentpanel" Namespace="efundraising.Web.UI.UIControls" Assembly="efundraising.Web.UI.UIControls" %>
<%@ Register TagPrefix="buttonpanel" Namespace="efundraising.Web.UI.UIControls" Assembly="efundraising.Web.UI.UIControls" %>
<%@ Register TagPrefix="uc1" TagName="PackageProductLinkInfo" Src="Components/User/Administration/PackageProductLinkInfo.ascx" %>
<%@ Register TagPrefix="contentpanel" Namespace="efundraising.Web.UI.UIControls" Assembly="efundraising.Web.UI.UIControls" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<body>
    <form id="form1" runat="server">	
		<TABLE id="Table1" height="105" cellSpacing="0" cellPadding="0" border="0">
			<TR>
				<TD class="ContentHeader" colSpan="2" height="23"><A style="COLOR: #333333" href="http://www.efundraising.com">Home</A>
					&gt; <A style="COLOR: #333333" href="AdministrationProductPackage.aspx">Administration</A>&nbsp;&gt;&nbsp;Edit 
					Relation</TD>
			</TR>
			<TR>
				<TD height="22"></TD>
			</TR>
			<TR>
				<TD>
					<uc1:packageproductlinkinfo id="PackageProductLinkInfo1" runat="server"></uc1:packageproductlinkinfo></TD>
			</TR>
			<TR>
				<TD><BR>
				</TD>
			</TR>
			<TR>
				<TD align="right">
					<asp:button id="CloseButton" runat="server" CausesValidation="False" Text="Close" Visible="False"></asp:button>
					<asp:button id="BackButton" runat="server" Text="Go Back" Visible="False"></asp:button>
					<asp:button id="SaveButton" runat="server" Text="Save"></asp:button></TD>
			</TR>
			<TR>
				<TD align="right"></TD>
			</TR>
		</TABLE>
	</body>
</form>
