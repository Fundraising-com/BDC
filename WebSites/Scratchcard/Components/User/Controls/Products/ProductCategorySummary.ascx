<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProductCategorySummary.ascx.cs" Inherits="GA.BDC.WEB.ScratchcardWeb.Components.User.Controls.Products.ProductCategorySummary" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table border="0" width="100%">
	<tr>
		<td valign="top" class="normaltext">
			<asp:Image id="CategoryImage" runat="server" imageurl="../../../Resources/Images/_fund_/_classic_/en-US/fundraising-products/fundraising-products-chocolate.jpg"></asp:Image>
		</td>
		<td valign="top" class="normaltext">
			<asp:Label Runat="server" ID="CategoryLabel" CssClass="NormalText"></asp:Label>
			<br>
			<br>
			More about
			<asp:HyperLink Runat="server" ID="CategoryLink"></asp:HyperLink>
		</td>
	</tr>
</table>
<asp:Image id="Image2" runat="server" imageurl="../../../Resources/Images/_fund_/_classic_/en-US/stroke.gif" alt="fundraising.com spacer"></asp:Image>
<br>
