<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PayerAuth1.aspx.vb" Inherits="StoreFront.StoreFront.PayerAuth1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<script language="javascript" >
	function SubmitParent(sId,MD, paRes){
		if (parent == window)  return;
		else parent.SubmitPayerAuth(sId,MD,paRes);  
	}
</script>  

 <head>
    <title>PayerAuth1</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/nav4-0">
  </head>
  <body MS_POSITIONING="GridLayout">

    <form id="Form1" method="post" runat="server">

    </form>

  </body>
</html>
