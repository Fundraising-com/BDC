<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PreviewTopBanner.ascx.vb" Inherits="StoreFront.StoreFront.PreviewTopBanner" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>

<div class="logo">
	<a href="#">
		<asp:Label id="StoreName" runat="server" CssClass="TopBanner" visible="false">StoreName</asp:Label>
		<asp:Image id="StoreImage" runat="server" Visible="False"></asp:Image>
	</a>
</div>

<div class="top-nav">
	<ul>
		<li class="item01"><a href="#">Home</a></li>
		<li class="item02"><a href="#">About Us</a></li>
		<li class="item03"><a href="#">Contact Us</a></li>
		<li class="item04"><a href="#">FAQ</a></li>
	</ul>
</div>