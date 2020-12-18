<%@ Import Namespace = "StoreFront.SystemBase"%>
<%@ Control Language="vb" enableViewState="False" AutoEventWireup="false" Codebehind="SimpleSearch.ascx.vb" Inherits="StoreFront.StoreFront.SimpleSearch" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>

<div class="search">
	<ul>
		<li class="head">Search<span>:</span></li>
		<li class="textbox"><asp:TextBox id="txtSimpleSearch" runat="server"></asp:TextBox></li>
		<li class="button"><asp:LinkButton id="btnSearch" Runat="server"><asp:Image BorderWidth="0" ID="imgSearch" Runat="server" AlternateText="Search"></asp:Image></asp:LinkButton></li>
		<li class="advsearch"><a href="<%=StoreFrontConfiguration.SiteURL%>Search.aspx?Advanced=1&WebID=<%=Session("WebID")%>">Advanced Search</a></li>
	</ul>
</div>