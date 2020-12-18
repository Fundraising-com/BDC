<%@ Import Namespace = "StoreFront.SystemBase"%>
<%@ Control Language="vb" EnableViewState="False" AutoEventWireup="false" Codebehind="CartList.ascx.vb" Inherits="StoreFront.StoreFront.CartList" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>

<ul class="cartnav">
	<li class="account"><a href="<%=StoreFrontConfiguration.SiteURL%>custprofilemain.aspx">My Account</a></li>
	<li class="viewcart"><a href="<%=StoreFrontConfiguration.SiteURL%>shoppingcart.aspx">View Cart</a></li>
	<li class="checkout"><a href="<%=StoreFrontConfiguration.SiteURL%>shoppingcart.aspx">Checkout</a></li>
</ul>
<ul class="shopcart">
	<li class="head">Shopping Cart:</li>
	<li class="items"><asp:Label id="lblCount" runat="server"></asp:Label> <asp:Label id="lblItem" runat="server"></asp:Label> In Cart</li>
	<li class="total">Total: <asp:Label id="lblTotal" runat="server"></asp:Label></li>
</ul>