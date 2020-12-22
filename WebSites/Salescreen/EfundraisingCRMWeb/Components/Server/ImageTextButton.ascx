<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageTextButton.ascx.cs" Inherits="EfundraisingCRM.Components.Server.ImageTextButton" %>

 <style type="text/css">

a.lnkSubmit:active {
margin:0px 0px 0px 0px;
background:url(../../images/li_bg1.jpg) left center no-repeat;
padding: 0em 1.2em; 
font: 8pt "tahoma"; 
color: #336699; 
text-decoration: none; 
font-weight: normal; 
letter-spacing: 0px;
}

a.lnkSubmit:link {
margin:0px 0px 0px 0px;
background:url(../../images/li_bg1.jpg) left center no-repeat;
padding: 0em 1.2em; 
font: 8pt "tahoma"; 
color: #336699; 
text-decoration: none; 
font-weight: normal; 
letter-spacing: 0px;
}


a.lnkSubmit:visited {
margin:0px 0px 0px 0px;
background:url(../../images/li_bg1.jpg) left center no-repeat;
padding: 0em 1.2em; 
font: 8pt "tahoma"; 
color: #336699; 
text-decoration: none; 
font-weight: normal; 
letter-spacing: 0px;
}


a.lnkSubmit:hover {
margin:0px 0px 0px 0px;
background:url(../../images/li_bg1.jpg) left center no-repeat;
padding: 0em 1.2em; 
font: 8pt "tahoma"; 
color: #000000; 
text-decoration: none; 
font-weight: normal; 
letter-spacing: 0px;

}
</style>

<asp:LinkButton CssClass="lnkSubmit" ID="lnkButton" runat="server">SUBMIT</asp:LinkButton>
