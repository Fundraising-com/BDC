<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PreviewTopSubBanner.ascx.vb" Inherits="StoreFront.StoreFront.PreviewTopSubBanner" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="PreviewMenubar" Src="PreviewMenuBar.ascx" %>
<div runat="server" visible="false">
	<asp:Label id="Label" runat="server" CssClass="TopSubBanner" Visible="false">PageName</asp:Label>
	<uc1:PreviewMenubar id="CMenubar1" runat="server" Callpage="top"></uc1:PreviewMenubar>
	<asp:Image id="PageImage" runat="server" Visible="False"></asp:Image>
</div>

<div class="sub-nav">
	<ul>
		<li class="item01"><a href="#">Home</a></li>
		<li class="item02"><a href="#">About Us</a></li>
		<li class="item03"><a href="#">Contact Us</a></li>
		<li class="item04"><a href="#">FAQ</a></li>
	</ul>
</div>


