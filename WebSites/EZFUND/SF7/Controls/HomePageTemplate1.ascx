<%@ Import Namespace = "StoreFront.SystemBase"%>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="HomePageTemplate1.ascx.vb" Inherits="StoreFront.StoreFront.HomePageTemplate1" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<%@ Register TagPrefix="uc1" TagName="FeaturedCategories" Src="FeaturedCategories.ascx" %>
<div class="df-welcome">
	<h1>Welcome</h1>
	<img src="images/df-welcomeimg.gif">
	<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. In gravida. Maecenas 
		faucibus sagittis massa. Quisque euismod leo at arcu. Curabitur fermentum, elit 
		rutrum convallis tristique, enim orci convallis lacus, nec malesuada turpis 
		erat eget ligula. Vestibulum pretium, risus sit amet nonummy pulvinar, pede leo 
		suscipit urna, sed cursus mauris risus sed dui. Curabitur tempor pede non 
		ligula. Nullam erat. Aenean rhoncus. Donec semper, lectus et rhoncus nonummy, 
		erat nulla pretium sapien, eu tempus ligula quam ut sem.</p>
</div>
<span class="df-cats">
<uc1:FeaturedCategories id="FeaturedCategories1" runat="server"></uc1:FeaturedCategories>
</span>