<%@ Page Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.Loading" Codebehind="Loading.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Loading</title>
    <script>
        function SetImg1()
        {
            //alert("1");
            var a = document.getElementById("imgGear");
            a.src = "images/1.gif";
            self.setTimeout("SetImg2();",250);
        }
        function SetImg2()
        {
           //alert("2");
           var a = document.getElementById("imgGear");
           a.src = "images/2.gif";
           self.setTimeout("SetImg3();",250);
        }
        function SetImg3()
        {
            //alert("3");
            var a = document.getElementById("imgGear");
            a.src = "images/3.gif";
            self.setTimeout("SetImg1();",250);
        }
        function StartAnimation()
        {   
            //alert("start");
            self.setTimeout("SetImg2();",250);  
        }        
        
    </script>
</head>
<body onload="StartAnimation()" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
    <form id="form1" runat="server">
    <div>   
        <table style="BORDER-RIGHT: navy thin solid; BORDER-TOP: navy thin solid; BORDER-LEFT: navy thin solid; BORDER-BOTTOM: navy thin solid;">
            <tr>
                <td>
                    <img id="imgGear" src="images/1.gif" />&nbsp;</td>
                <td>
                    <span style="font-weight: bold; font-size: 15pt; font-family: Arial, Verdana">Processing</span>
                    <br />
                    <br />
                    <span style="font-size: 10pt; font-family: Arial, Verdana">The server is processing 
                        your request.</span>
                </td>
            </tr>            
        </table>
    </div>
    </form>
</body>
</html>
