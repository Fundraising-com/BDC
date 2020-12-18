<%@ Register TagPrefix="contentpanel" Namespace="efundraising.Web.UI.UIControls" Assembly="efundraising.Web.UI.UIControls" %>
<%@ Page language="c#" Codebehind="AdministrationProductNew.aspx.cs" AutoEventWireup="false" Inherits="AdminSection.AdministrationProductNew" %>
<%@ Register TagPrefix="uc1" TagName="PackageDescInfo" Src="Components/User/Administration/PackageDescInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProductInfo" Src="Components/User/Administration/ProductInfo.ascx" %>
<%@ Register TagPrefix="contentpanel" Namespace="efundraising.Web.UI.UIControls" Assembly="efundraising.Web.UI.UIControls" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<body>
    <form id="form1" runat="server">	
    		<TABLE height="210" cellSpacing="0" cellPadding="0" border="0">
			<TR>
				<TD class="ContentHeader" colSpan="2" height="28"><A style="COLOR: #333333" href="http://www.efundraising.com">Home</A>
					&gt; <A style="COLOR: #333333" href="AdministrationProductPackage.aspx">Administration</A>
					&gt; Create New Product
				</TD>
				<TD class="ContentHeader" height="28"></TD>
			</TR>
			<TR>
				<TD height="24"></TD>
			</TR>
			<TR>
				<TD vAlign="top" height="28">
					<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="300" border="0">
						<TR>
							<TD width="187">
								<uc1:ProductInfo id="ProductInfo1" runat="server"></uc1:ProductInfo></TD>
							<TD vAlign="top">
								<asp:image id="ProductImage" runat="server"></asp:image></TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD height="9"></TD>
			</TR>
			<TR>
				<TD height="21">
					<TABLE id="Table1" height="26" cellSpacing="1" cellPadding="1" width="504" bgColor="whitesmoke"
						border="0">
						<TR>
							<TD width="144">Select a Culture</TD>
							<TD bgColor="whitesmoke">
								<asp:dropdownlist id="CultureDropDownList" runat="server" Width="168px" AutoPostBack="True"></asp:dropdownlist></TD>
						</TR>
					</TABLE>
					<asp:Label id="errorLabel" runat="server" ForeColor="Red" Visible="False">ErrorLabel</asp:Label></TD>
			</TR>
			<TR>
				<TD vAlign="top" height="1">
					<uc1:PackageDescInfo id="ProductDescInfo1" runat="server"></uc1:PackageDescInfo></TD>
			</TR>
			<TR>
				<TD align="right">
					<asp:button id="CloseButton" runat="server" Visible="False" Text="Close" CausesValidation="False"></asp:button>
					<asp:button id="SaveButton" runat="server" Text="Save"></asp:button></TD>
			</TR>
		</TABLE>
	</form>
</body>
