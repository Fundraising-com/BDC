<%@ Page Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.ImageViewer" Codebehind="ImageViewer.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta HTTP-EQUIV="Pragma" CONTENT="no-cache">
<LINK href="Styles.css" type="text/css" rel="stylesheet">
<script type="text/javascript" src="Script/wz_dragdrop.js"></script> 
<title>Image Viewer</title>
</head>
<body onload="scale()">
    <form id="form1" runat="server">
        <asp:Label ID="lblError" runat="server" Text="" Visible=false CssClass="LabelError"></asp:Label>
        <table width=750 height=550 border=0 cellspacing=0 cellpadding=0>
            <tr valign=middle>
                <td align=center>
                    <img runat=server id="img" name="img"/>    
                </td>
            </tr>
        </table>
    </form>
<script type="text/javascript">
<!--
SET_DHTML("img");
function scale()
{
    var ratio;
    var maxWidth = 750;
    var maxHeight = 550;
    var width = dd.elements['img'].w;
    var height = dd.elements['img'].h;
    
    if (width < maxWidth)
    {
        ratio = maxWidth / width;
        width = width * ratio;
        height = height * ratio;
    }
    
    if (height < maxHeight)
    {
        ratio = maxHeight / height  ;
        width = width * ratio;
        height = height * ratio;
    }
    
    if (width > maxWidth)
    {
        ratio = width / maxWidth;
        width = width / ratio;
        height = height / ratio;
    }
    
    if (height > maxHeight)
    {
        ratio = height / maxHeight;
        width = width / ratio;
        height = height / ratio;
    }
    
    dd.elements.img.resizeTo(width,height);
    dd.elements.img.moveTo(25,25);
    
    
    window.resizeTo(width+50,height+50)
    
   
}
//-->
</script> 
</body>
</html>
