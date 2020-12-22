<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Register TagPrefix="uc1" TagName="PackageInfo" Src="Components/User/Administration/PackageInfo.ascx" %>

<%@ Register TagPrefix="uc1" TagName="DescInfo" Src="Components/User/Administration/PackageDescInfo.ascx" %>
<%@ Page language="c#" Codebehind="AdministrationPackageEdit.aspx.cs" AutoEventWireup="false" Inherits="AdminSection.AdministrationPackageEdit" %>
<%@ Register TagPrefix="uc1" TagName="RelationsDatagrid" Src="Components/User/Administration/RelationsDatagrid.ascx" %>

<body>
    <form id="form1" runat="server">	
    	<TABLE height="210" cellSpacing="0" cellPadding="0" border="0">
			<TR>
				<TD class="ContentHeader" colSpan="2" height="23"><A style="COLOR: #333333" href="http://www.efundraising.com">Home</A>
					&gt; <A style="COLOR: #333333" href="AdministrationProductPackage.aspx">Administration</A>
					&gt; Edit&nbsp;Package
				</TD>
				<TD class="ContentHeader" height="30"></TD>
			</TR>
			<TR>
				<TD height="22"></TD>
			</TR>
			<TR>
				<TD vAlign="top" height="28">
					<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="300" border="0">
						<TR>
							<TD width="158">
								<uc1:packageinfo id="PackageInfo1" runat="server"></uc1:packageinfo></TD>
							<TD vAlign="top" width="37"></TD>
							<TD vAlign="top">
								<asp:image id="PackageImage" runat="server"></asp:image></TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD height="9">
					<uc1:RelationsDatagrid id="RelationsDatagrid1" runat="server"></uc1:RelationsDatagrid></TD>
			</TR>
			<TR>
				<TD height="21">
					<TABLE class="NormalText" id="Table1" height="26" cellSpacing="1" cellPadding="1" width="504"
						bgColor="whitesmoke" border="0">
						<TR>
							<TD width="146">Select a Culture</TD>
							<TD bgColor="whitesmoke">
								<asp:dropdownlist id="CultureDropDownList" runat="server" Width="168px" AutoPostBack="True"></asp:dropdownlist></TD>
						</TR>
					</TABLE>
					<asp:Label id="errorLabel" runat="server" Visible="False" ForeColor="Red">ErrorLabel</asp:Label></TD>
			</TR>
			<TR>
				<TD vAlign="top" height="1">
					<uc1:DescInfo id="PackageDescInfo1" runat="server"></uc1:DescInfo></TD>
			</TR>
			<TR>
				<TD align="right">
					<asp:button id="CloseButton" runat="server" Visible="False" Text="Close" CausesValidation="False"></asp:button>
					<asp:button id="SaveButton" runat="server" Text="Save" 
                        onclick="SaveButton_Click1"></asp:button></TD>
			</TR>
		</TABLE>
	</form>
</body>
