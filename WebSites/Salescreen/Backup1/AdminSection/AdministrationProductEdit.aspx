<%@ Register TagPrefix="uc1" TagName="RelationsDatagrid" Src="Components/User/Administration/RelationsDatagrid.ascx" %>
<%@ Page language="c#" Codebehind="AdministrationProductEdit.aspx.cs" AutoEventWireup="false" Inherits="AdminSection.AdministrationProductEdit" %>
<%@ Register TagPrefix="uc1" TagName="PackageDescInfo" Src="Components/User/Administration/PackageDescInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProductInfo" Src="Components/User/Administration/ProductInfo.ascx" %>
<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<body>
    <form id="form1" runat="server">	
		<TABLE class="NormalText" id="Table1" style="WIDTH: 465px; HEIGHT: 192px" height="192"
			cellSpacing="0" cellPadding="0" border="0">
			<TR>
				<TD class="ContentHeader" colSpan="2" height="23"><A style="COLOR: #333333" href="http://www.efundraising.com">Home</A>
					&gt; <A style="COLOR: #333333" href="AdministrationProductPackage.aspx">Administration</A>
					&gt; Edit&nbsp;Product
				</TD>
				<TD class="ContentHeader" height="27"></TD>
			</TR>
			<TR>
				<TD class="ContentHeader" width="351" height="23"></TD>
				<TD class="ContentHeader" height="23"></TD>
			</TR>
			<TR>
				<TD vAlign="top" height="38">
					<TABLE class="NormalText" id="Table3" cellSpacing="0" cellPadding="0" width="300" border="0">
						<TR>
							<TD width="156">
								<uc1:productinfo id="ProductInfo1" runat="server"></uc1:productinfo></TD>
							<TD vAlign="top" width="40"></TD>
							<TD vAlign="top" align="center">
								<asp:image id="ProductImage" runat="server"></asp:image></TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD height="11">
					<uc1:RelationsDatagrid id="RelationsDatagrid1" runat="server"></uc1:RelationsDatagrid></TD>
			</TR>
			<TR>
				<TD height="1">
					<TABLE class="NormalText" id="Table1" style="WIDTH: 464px; HEIGHT: 26px" cellSpacing="1"
						cellPadding="1" width="464" bgColor="whitesmoke" border="0">
						<TR>
							<TD width="144">Select a Culture</TD>
							<TD bgColor="whitesmoke">
								<asp:dropdownlist id="CultureDropDownList" runat="server" Width="168px" AutoPostBack="True"></asp:dropdownlist></TD>
						</TR>
					</TABLE>
					<asp:Label id="errorLabel" runat="server" Visible="False" ForeColor="Red">ErrorLabel</asp:Label></TD>
			</TR>
			<TR>
				<TD height="14">
					<uc1:PackageDescInfo id="ProductDescInfo1" runat="server"></uc1:PackageDescInfo></TD>
			</TR>
			<TR>
				<TD height="14"></TD>
			</TR>
			<TR>
				<TD align="right" height="21">
					<asp:button id="CloseButton" runat="server" Text="Close" CausesValidation="False"></asp:button>
					<asp:button id="SaveButton" runat="server" Text="Save"></asp:button></TD>
			</TR>
			<TR>
				<TD align="right">
					&nbsp;</TD>
			</TR>
		</TABLE>
	</form>
</body>
