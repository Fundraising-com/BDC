<%@ Control Language="vb" AutoEventWireup="false" Codebehind="LivePerson.ascx.vb" Inherits="StoreFront.StoreFront.LivePerson" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<!-- BEGIN LivePerson Button Code -->
<a runat="server" id="livepersonlink" href='http://server.iad.liveperson.net/hc/[LivePersonAccount]/?cmd=file&file=visitorWantsToChat&site=[LivePersonAccount]&byhref=1' onClick="javascript:window.open('http://server.iad.liveperson.net/hc/[LivePersonAccount]/?cmd=file&file=visitorWantsToChat&site=[LivePersonAccount]&referrer='+document.location,'chat[LivePersonAccount]','width=472,height=320');return false;">
	<img runat="server" id="hcIcon" src='http://server.iad.liveperson.net/hc/[LivePersonAccount]/?cmd=repstate&site=[LivePersonAccount]&imageUrl=http://server.iad.liveperson.net/visitor/storefront&ver=1' name='hcIcon' width="85" height="40" border="0"></a>
<!—- END LivePerson Button code -->
