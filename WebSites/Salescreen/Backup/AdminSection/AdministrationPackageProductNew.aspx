<%@ Page language="c#" Codebehind="AdministrationPackageProductNew.aspx.cs" AutoEventWireup="false" Inherits="AdminSection.AdministrationPackageProductNew" %>
<%@ Register TagPrefix="uc1" TagName="PackageProductLinkInfo" Src="Components/User/Administration/PackageProductLinkInfo.ascx" %>
<%@ Register TagPrefix="contentpanel" Namespace="efundraising.Web.UI.UIControls" Assembly="efundraising.Web.UI.UIControls" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<body>
    <form id="form1" runat="server">
    		<TABLE id="Table1" style="WIDTH: 392px; HEIGHT: 143px" height="143" cellSpacing="0" cellPadding="0"
			border="0">
			<TR>
				<TD class="ContentHeader" colSpan="2" height="23"><A style="COLOR: #333333" href="www.efundraising.com">Home</A>
					&gt; <A style="COLOR: #333333" href="AdministrationProductPackage.aspx">Administration</A>&nbsp;&gt; 
					Create New Relation</TD>
			</TR>
			<TR>
				<TD height="22"></TD>
			</TR>
			<TR>
				<TD height="19">
					<uc1:packageproductlinkinfo id="PackageProductLinkInfo1" runat="server"></uc1:packageproductlinkinfo></TD>
			</TR>
			<TR>
				<TD vAlign="top" height="19"><BR>
					<asp:Label id="errorLabel" runat="server" Visible="False" ForeColor="Red">ErrorLabel</asp:Label></TD>
			</TR>
			<TR>
				<TD align="right">
					<asp:button id="CloseButton" runat="server" Visible="False" Text="Close" CausesValidation="False"></asp:button>
					<asp:button id="BackButton" runat="server" Width="64px" Visible="False" Text="Go Back" CausesValidation="False"></asp:button>
					<asp:button id="SaveButton" runat="server" Width="59px" Text="Save" 
                        onclick="SaveButton_Click1"></asp:button></TD>
			</TR>
			<TR>
				<TD align="right">
					&nbsp;</TD>
			</TR>
		</TABLE>
	</body>
</form>
