<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.CatalogItemCategorySubList" Codebehind="CatalogItemCategorySubList.ascx.cs" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl.DataGridControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="SearchModule" Src="~/UserControls/SearchModule.ascx" %>

<TABLE id="Table5" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td Class="SectionPageTitleInfo">
			<asp:label id="Label4" runat="server">
				Categoy Tree View
			</asp:label></td>
	</tr>
	<TR>
		<TD><!--Tree View  --><br>
			<asp:TreeView id="trvwCategory" runat="server" width="250px" height="300px" CssClass="StandardLabel"
				BorderStyle="Inset" BackColor="White" Font-Names="verdana">
				<Nodes>
				    <asp:TreeNode Text="Node1"></asp:TreeNode>
				    <asp:TreeNode Text="Node2"></asp:TreeNode>
					<asp:TreeNode Text="Node3"></asp:TreeNode>				    
				</Nodes>
			</asp:TreeView>
		</TD>
	</TR>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<td colSpan="2"><br>
						<asp:label id="lblTotal" runat="server" CssClass="TotalListLabel">
							Number of Category(s):
						</asp:label></td>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
