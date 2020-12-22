<%@ LANGUAGE="VBScript" %>
<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System      :   StoreFront 2000 Version 4.3.5
'   Date        :   11.8.99
'   Author      :   LaGarde, Incorporated
'   Description :   StoreFront Credit Card Processor Error Handling Routines
'   Notes       :   There are no configurable elements in this file.
'
'                         COPYRIGHT NOTICE
'
'   The contents of this file is protected under the United States
'   copyright laws as an unpublished work, and is confidential and
'   proprietary to LaGarde, Incorporated.  Its use or disclosure in 
'   whole or in part without the expressed written permission of 
'   LaGarde, Incorporated is expressely prohibited.
'
'   (c) Copyright 1998 by LaGarde, Incorporated.  All rights reserved.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
%>
<% 
DSN_NAME = Session("DSN_NAME") 
PATH = Request("ReOrderPath")
%>
<!--#include file="ccerrormsg.htm"-->
<div align=<%=TableAlign%>>
  <table border=<%=BorderSize%> bordercolor=<%=BorderColor%> cellpadding=<%=CellPadding%> cellspacing=<%=CellSpacing%> width=<%=TableWidth%> bgcolor=<%=TableBG%>>
    <tr>
      <td colspan="2" align="center" bgcolor="<%=CellColor%>"><%=CellFontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=CellFontColor%> size=<%=FontSize%>><b>Payment Advice</font>&nbsp;</td>
      </tr>
      <tr>
        <td valign=top>The following error was reported by the payment processing service:</td>
        <td>&nbsp;<%= Request("ProcErrMsg") %><BR>&nbsp;<%= Request("ProcErrLoc") %></td>
        </tr>
      </table>
    </div>
  </body>






