<%@ Control Language="c#" AutoEventWireup="True" CodeFile="ProductPackageTreeView.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Package.ProductPackageTreeView" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="0">
	<TR>
		<TD><asp:treeview id="TreeView1" runat="server" AutoSelect="True" 
                CssClass="NormalText" ForeColor="Black" 
                onselectednodechanged="TreeView1_SelectedNodeChanged" ontreenodecheckchanged="TreeView1_TreeNodeCheckChanged" 
               ></asp:treeview></TD>
	</TR>
	<TR>
		<TD></TD>
	</TR>
</TABLE>
