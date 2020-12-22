<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionTimeOut.aspx.cs" Inherits="EfundraisingCRM.SessionTimeOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <span class="Apple-style-span" 
            style="border-collapse: separate; color: rgb(0, 0, 0); font-family: arial; font-size: 13px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 15px; orphans: 2; text-align: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-border-horizontal-spacing: 0px; -webkit-border-vertical-spacing: 0px; -webkit-text-decorations-in-effect: none; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0; ">
        <h1 jscontent="heading" style="font-size: 1.5em; margin-bottom: 1.5em; ">
            Your session has expired</h1>
        <div id="errorSummary" jsselect="summary" style="margin-bottom: 2.5em; ">
            <p jseval="this.innerHTML = $this.msg;">
                This web page requires data that you entered earlier in order to be properly 
                displayed. Since your session timed out, you will need to reload the sale 
                screen.
            </p>
        </div>
        </span>
    
    </div>
    </form>
</body>
</html>
