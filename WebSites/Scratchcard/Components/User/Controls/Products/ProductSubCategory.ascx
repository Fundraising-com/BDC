<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProductSubCategory.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Products.ProductSubCategory" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%" class="subtitle" cellpadding="0" cellspacing="0">
	<tr>
		<td width="1"><asp:Image Runat="server" ImageUrl="../../../Resources/Images/_fund_/_classic_/en-US/spacer.gif"
				alt="fundraising.com spacer" Height="20" Width="1" ID="Image1" NAME="Image1"></asp:Image></td>
		<td align="center" valign="middle"><asp:Label Runat="server" ID="CategoryLabel" CssClass="subtitle"></asp:Label></td>
	</tr>
	<tr>
		<td align="center" valign="top" colspan="2">
			<asp:Table Runat="server" Width="100%" id="ProductsTable" CssClass="NormalText" BackColor="#FFFFFF"></asp:Table>
		</td>
	</tr>
</table>
