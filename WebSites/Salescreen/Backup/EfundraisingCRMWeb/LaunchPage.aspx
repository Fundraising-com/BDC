<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LaunchPage.aspx.cs" Inherits="EfundraisingCRM.LaunchPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script lang=javascript>
function NewWindowTest(url, windowName){
			var hWnd = window.open(url,windowName,"width=600,height=500,resizable=yes,scrollbars=yes,location=no,top=15, left=15,status=yes");
			hWnd.focus();
}
function NoConfirm() {
    win = top;
    win.opener = top;
    win.close();
}

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     </div>
    </form>
</body>
</html>
