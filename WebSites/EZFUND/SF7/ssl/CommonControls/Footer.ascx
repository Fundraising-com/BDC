<%@ Import Namespace = "StoreFront.SystemBase"%>
<%@ Register TagPrefix="uc1" TagName="CartList" Src="../Controls/CartList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CMenubar" Src="../Controls/CMenubar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SimpleSearch" Src="../Controls/SimpleSearch.ascx" %>
<%@ Control Language="vb" EnableViewState = False AutoEventWireup="false" Codebehind="Footer.ascx.vb" Inherits="StoreFront.StoreFront.Footer" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<div id="MenuContainer"><uc1:CMenubar id="CMenubar1" runat="server" Callpage="top"></uc1:CMenubar></div>

<p>Copyright &copy; 2007 Your Company. All Rights Reserved.</p>

<div class="powered">
	<a href="http://www.lagarde.com/"><asp:image ID="imgLagardeLogo" Runat="server" AlternateText="Powered By StoreFront 7 - By LaGarde"
			BorderWidth="0" /></a>
</div>