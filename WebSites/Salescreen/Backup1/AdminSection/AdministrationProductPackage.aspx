<%@ Register TagPrefix="efundraising" Namespace="efundraising.Web.UI.MasterPages" Assembly="efundraising.Web.UI.MasterPages" %>
<%@ Register TagPrefix="contentpanel" Namespace="efundraising.Web.UI.UIControls" Assembly="efundraising.Web.UI.UIControls" %>
<%@ Page language="c#" Codebehind="AdministrationProductPackage.aspx.cs" AutoEventWireup="false" Inherits="AdminSection.AdministrationProductPackage" %>

<body>
    <form id="form1" runat="server">
    
    		<TABLE id="Table1" height="86" cellSpacing="0" cellPadding="0" width="480" border="0">
			<TR>
				<TD class="ContentHeader" colSpan="2" height="23"><A style="COLOR: #333333" href="http://www.efundraising.com">Home</A>
					&gt; <A style="COLOR: #333333" href="AdministrationProductPackage.aspx">Administration</A>
					&gt; Manage Packages &amp; Products
				</TD>
				<TD class="ContentHeader"></TD>
			</TR>
			<TR>
				<TD width="351">
					<H1 id="product">
						&nbsp;</H1>
				</TD>
			</TR>
			<TR>
				<TD vAlign="top" width="351">
					<asp:treeview id="PackageTreeview" AutoSelect="True" CssClass="NormalText" 
                        ForeColor="Black" runat="server" 
                        onselectednodechanged="PackageTreeview_SelectedNodeChanged" 
                        ShowLines="True"></asp:treeview></TD>
				<TD vAlign="top" align="left">&nbsp;&nbsp;
					</TD>
			</TR>
			<TR>
				<TD vAlign="top" width="351" height="25"></TD>
				<TD vAlign="top" align="left" height="25"></TD>
			</TR>
			<TR>
				<TD vAlign="top" width="351">
					<asp:Button id="RefreshButton" runat="server" Text="Refresh" 
                        onclick="RefreshButton_Click1"></asp:Button>
					<asp:Button id="UpdateButton" runat="server" Text="Update Sale Screen " 
                        onclick="UpdateButton_Click"></asp:Button></TD>
				<TD vAlign="top" align="left"></TD>
			</TR>
		</TABLE>
	</form>
</body>
