<%@ Register TagPrefix="uc1" TagName="PreviewMenuBar" Src="PreviewMenuBar.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PreviewFooterNav.ascx.vb" Inherits="StoreFront.StoreFront.PreviewFooterNav" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<div runat="server" visible="false" ID="Div1"><uc1:PreviewMenuBar id="CMenubar1" runat="server" Callpage="top"></uc1:PreviewMenuBar></div>
<ul>
	<li class="item01">
		<a href="#">Home</a>
	<li class="item02">
		<a href="#">About Us</a>
	<li class="item03">
		<a href="#">Contact Us</a>
	<li class="item04">
		<a href="#">FAQ</a>
	<li class="item05">
		<a href="#">Privacy Policy</a></li>
</ul>
<p>Copyright © 2007 Your Company. All Rights Reserved.</p>
<div class="powered">
	<a href="#">
		<asp:image ID="imgLagardeLogo" Runat="server" AlternateText="Powered By StoreFront 7 - By LaGarde"
			BorderWidth="0" /></a>
</div>
