<%
'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: Menu.asp
	
'@FILEVERSION: 1.0.1

'@VERSIONDATETIME: 2/21/01

'@DESCRIPTION:   web Admin menu

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2000, 2001 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO	

%>

<html>
<head>
<title>SF Menu Page</title>
</head>
<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SFLib/adovbs.inc"-->
<!--#include file="incAdmin.asp"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="../SfLib/incDesign.asp"-->
<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">

    <table border="0" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="<%= C_WIDTH %>" align="center">
    <tr>
    <td>
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
<%	If C_BNRBKGRND = "" Then %>
		<td align="middle" background="<%= C_BKGRND1 %>" bgcolor="<%= C_BGCOLOR1 %>"><b><font face="<%= C_FONTFACE1 %>" color="<%= C_FONTCOLOR1 %>" SIZE="<%= C_FONTSIZE1 %>"><%= C_STORENAME %>&nbsp;</font></b></td>
<%	Else %>
		<td align="middle" bgcolor="<%= C_BNRBGCOLOR %>"><img src="<%= C_BNRBKGRND %>" border="0"></td>
<%	End If %>            
        </tr>
        <tr>
        <td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Storefront HTML Menu</font></b></td>    
        </tr>
        <tr>
		<td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>" align="middle"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Database Management</b></font></td>        
        </tr>
        <tr>
        <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>"><br>
            <table border="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="75%" align="center">
            <tr>
            <td width="100%" align="middle" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>" colspan="2"><b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Daily Store Managment</font></b></td>
            </tr>
            <tr>
            <td colspan=2 width="100%" align="middle" bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>">
                <p><br>
                <font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
				<A href="sfReports.asp">Sales Reports</a><br>	
                <a href="m0022.asp">Administer Sales and Discounts</a><br>
                <a href="promo_mail.asp">Promotional Mail</a><br>
				<a href="m0023.asp">Affiliate Partner Administration</a></font>         
                <br><br></p>
            </td>
            </tr>
            <tr>
            <td colspan=2 width="50%" align="middle" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Store Configuration</font></b></td>
            </tr>
            <tr>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>">
                <p><br>
                <font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
				<A href="m0000.asp">General Configuration</a><br>	
                <A href="m0001.asp">Mail Configuration</a><br>
                <A href="m0007.asp">Geographical Settings</a>                
            </font>
                <font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
                <br><br></p>
                </font>
            </td>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">
                <A href="m0004.asp">Transaction Processing</a>
                <br>
                <A href="m0002.asp">Shipping Configuration</a><br>
                <A href="m0003.asp">Tax Configuration</a>                
            </font></td>
            </tr>
            <tr>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Product</font></b></td>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><b><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>">Category</font></b></td>
            </tr>
            <tr>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>">
                <p><br>
                <font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
                <A href="m0009.asp">Add Products</a><br>
                <A href="m0010.asp">Edit/Delete Products</a><br>
                <A href="m0011.asp">List All Products</a></font>
                <br><br></p>
            </td>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" valign="top">
                <p><br>
                <font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
                <A href="m0012.asp">Add Category</a><br>
                <A href="m0013.asp">Edit/Delete Category</a><br>
                <A href="m0014.asp">List All Categories</a></font>
                <br><br></p>
            </td>
            </tr>
            <tr>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>" valign="top"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Manufacturer</b></font></td>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>" valign="top"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Vendor</b></font></td>
            </tr>
            <tr>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" valign="top">
                <p><br>
                <font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
                <A href="m0015.asp">Add Manufacturer</a><br>
                <A href="m0016.asp">Edit/Delete Manufacturer</a><br>
                <A href="m0017.asp">List All Manufacturers</a></font>
                <br><br></p>
            </td>
            <td width="50%" align="middle" bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" valign="top">
                <p><br>
                <font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
                <A href="m0018.asp">Add Vendor</a><br>
                <A href="m0019.asp">Edit/Delete Vendor</a><br>
                <A href="m0020.asp">List All Vendors</a></font>
				<br><br></p>
            </td>
            </tr>
            </table>
            <br>
            <CENTER></CENTER></td>
        <tr>
		<td bgcolor="<%= C_BGCOLOR7 %>" background="<%= C_BKGRND7 %>"><font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>"><p align="center"><b><a href="menu.asp">Main Menu</a> | <a href="../../search.asp">Your Store</a></b></font></p></td>
        </tr>
        </table>
    </td>
    </tr>
    </table>

</body>

</html>



