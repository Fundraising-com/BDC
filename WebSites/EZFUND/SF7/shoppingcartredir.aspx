<%@ Page Language="VB" Strict="false" %>
<%@ Import Namespace = "StoreFront.SystemBase"%> 

<script language="vb" runat="server" >

sub page_load()
Try
	Dim myRedir as String = "<"
	myRedir += "script>" & vbcrlf
	myRedir += "__utmLinker('" & Session("myRedir") & "');" & vbcrlf
	myRedir += "<"
	myRedir += "/"
	myRedir += "script>"
	If Not (Session("myRedir") is Nothing) Then
		RegisterStartupScript("redircode", myRedir)
	End If
Catch
	Response.Redirect(Session("myRedir") & "test=debug")
End Try  
End Sub  


</script>
<html>
<head>
<script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
</script>
<script type="text/javascript">
_uacct = "<%=StoreFrontConfiguration.GoogleAnalyticsID%>";
_udn = "none";
_ulink = 1;
urchinTracker();
</script>
</head>
<body id="BodyTag" runat="server">
<form runat="server">
If your browser does not automatically redirect, please click <a href="<%=Session("myRedir")%>">here</a> to continue.
</form>
</body>
</html>
