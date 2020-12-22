<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System      :   StoreFront 2000 Version 4.04.0
'   Date        :   1.6.2000
'   Author      :   LaGarde, Incorporated
'   Description :   StoreFront Search Output Routines
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
<html>
<body>
<!--#include file="SFLib/design.inc"-->
<% If Request.QueryString("ORDER_FLAG") = "1" Then %>
<!--#include file="thanks_head.htm"-->
<div align=<%=TableAlign%>>
  <!--webbot bot="HTMLMarkup" startspan -->
	<% If Request("ORDER_FLAG") = "1" Then %>
		<% If Request("Alert") = "0" Then %>
		<font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>><i><b>
		Thank You
		<br>
			<% If Request("Quantity") = "1" Then%><%= Request("Quantity") %> &nbsp;
			<%= Request("DESCRIPTION") %><br>has been added to your order!<br>
			<% ElseIf Request("Quantity") > "1" Then %><%= Request("Quantity") %>
			 &nbsp;
			<%= Request("DESCRIPTION") %><br>have been added to your order!<br>
			<% End If %>
		<%= Request("MESSAGE") %></b></i></font>
		<% End If %>
		<% If Request("Alert") = "1" Then %>
		
		<%= Request("Quantity") %>
		<% End If %> 
	<% End If %>
	</font><!--webbot bot="HTMLMarkup" endspan -->
	</div>
	<% Else %>
<% If Not RSSearchResult.EOF Then %>
<!--#include file="search_results_head.htm"-->
<%'//************** Search Results Paragraph ***************************%>
<div align=<%=TableAlign%>>
  <table border=<%=BorderSize%> bordercolor=<%=BorderColor%> cellpadding=<%=CellPadding%> cellspacing=<%=CellSpacing%> width=<%=TableWidth%> bgcolor=<%=TableBG%>>
    <tr>
      <td align="center" bgcolor="<%=CellColor%>"><%=CellFontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=CellFontColor%> size=<%=FontSize%>>Search Results:</font>&nbsp;</td>
    </tr>
    <tr>
      <td><%=FontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>Your search for <% If Request("CATEGORY") = "" Then %> <%If lblCategoryActive = "1" Then%><%=lblCategory%><%Else%>Category<%End If%> = ALL <% Else %> <%If lblCategoryActive = "1" Then%><%=lblCategory%><%Else%>Category<%End If%> = <%= Request("CATEGORY") %> <% End If %>&nbsp;
        <% If Request("MANUFACTURER") = "" Then %> <%If lblManufacturerActive = "1" Then%><%=lblManufacturer%><%Else%>Manufacturer<%End If%> = ALL <% Else %> <%If lblManufacturerActive = "1" Then%><%=lblManufacturer%><%Else%>Manufacturer<%End If%> = <%= Request("MANUFACTURER") %> <% End If %>&nbsp;
        <% If Request("DESCRIPTION") = "" Then %> <%If lblDescriptionActive = "1" Then%><%=lblDescription%><%Else%>Description<%End If%> = (none specified) <% Else %><%If lblDescriptionActive = "1" Then%><%=lblDescription%><%Else%>Description<%End If%> = <%= Request("DESCRIPTION") %> <% End If %>&nbsp;
        <% If Request("PRODUCT_ID") = "" Then %> <%If lblProductIDActive = "1" Then%><%=lblProductID%><%Else%>Product ID<%End If%> = (none specified) <% Else %><%If lblProductIDActive = "1" Then%><%=lblProductID%><%Else%>Product ID<%End If%> = <%= Request("PRODUCT_ID") %> <% End If%>.</font>&nbsp;</td>
    </tr>
    <tr>
      <td align=center><hr size=8 color=<%=CellColor%>>&nbsp;&nbsp;</td>
    </tr>
  </table>
</div>

<%'//**********BEGIN OUTPUT SEARCH RESULTS ****************%>
<% If Not RSSearchResult.EOF Then %>
<div align=<%=TableAlign%>>
  <table border=<%=BorderSize%> bordercolor=<%=BorderColor%> cellpadding=<%=CellPadding%> width=<%=TableWidth%> cellspacing=<%=CellSpacing%> bgcolor=<%=TableBG%>>
    <% 'Do While Not (RSSearchResult is nothing) %><% 
RowCount = RSSearchResult.PageSize
Do While Not RSSearchResult.EOF and RowCount > 0 
%>
    <% If PageNo > 1 Then %>
    <% End If %>
    <% If RowCount = 0 Then %>
    <% End If %>
    <!--webbot bot="HTMLMarkup" startspan -->
<form action="<%=Session("sfPath") %>" method="POST" id=form2 name=form2>
  <input type="hidden" name="PRODUCT_ID" value="<%= RSSearchResult("PRODUCT_ID") %>">
  <input type="hidden" name="SRCH_ID" value="<%= Request("PRODUCT_ID") %>">
  <input type="hidden" name="ORDER_FLAG" value="1">
  <input type="hidden" name="SRCH_DESCRIPTION" value="<%= Request("DESCRIPTION") %>">
  <input type="hidden" name="SRCH_MANUFACTURER" value="<%= Request("MANUFACTURER") %>">
  <input type="hidden" name="SRCH_CATEGORY" value="<%= Request("CATEGORY") %>">
  <input type="hidden" name="PageNo" value="<%= Trim(PageNo) %>">
  <input type="hidden" name="RowCount" value="<%= RowCount %>">
  <input type="hidden" name="ScrollAction" value="<%= PageNo-1%>">
  <input type="hidden" name="ScrollAction" value="<%= PageNo+1 %>">
<!--webbot bot="HTMLMarkup" endspan -->
    <%'***********here we go****************************************%>
 
    <tr>
      <td rowspan="5">
        <%If lblLinkActive = "1" and InStr(RSSearchResult("LINK"),".") Then %>
        <a href="<%= RSSearchResult("LINK") %>"><%End If%><%If lblImageActive = "1" Then%><% If InStr(RSSearchResult("IMAGE_PATH"),".") Then %><img src="<%= RSSearchResult("IMAGE_PATH") %>" border="0" align="center" valign="center"><% ElseIf lblLinkActive = "1" and InStr(RSSearchResult("LINK"),".") Then %><%=lblLINK%><% End If %><% End If %><%If lblLinkActive = "1" and InStr(RSSearchResult("LINK"),".") Then %></a>
        <% else %>&nbsp;<%End If%>&nbsp;&nbsp;
      </td>

      <td align="left" valign="top">
&nbsp;&nbsp;
        <%If lblProductIDActive = "1" Then%><%=FontStyle%>
        <font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>><small><b>
        <%= lblPRODUCTID %> :</b></small>&nbsp;<%=RSSearchResult("PRODUCT_ID")%></font>
        <% End If %>&nbsp;&nbsp;
      </td>

      <td align="left" valign=top nowrap>
&nbsp;&nbsp;<%If lblCategoryActive = "1" Then%>
        <%=FontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
        <small><b><u><%=lblCATEGORY%></u></b></small></font><%Else%>&nbsp;<%End If%>
      </td>

      <td align="left" valign=top nowrap>
&nbsp;&nbsp;<%If lblManufacturerActive = "1" Then%><%=FontStyle%>
        <font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>><small><b><u>
        <%=lblMANUFACTURER%></u></b></small></font><%Else%>&nbsp;<%End If%>
      </td>

    </tr>
    <tr>
    
      <td valign="top" align="left">&nbsp;
      </td> 
   
      <td align="left" nowrap>
&nbsp;&nbsp;<%If lblCategoryActive = "1" Then%><%=FontStyle%>
        <font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
        <%=RSSearchResult("CATEGORY")%>&nbsp;<%Else%>&nbsp;<% End If %>
        </td>

        <td align="left" nowrap>&nbsp;&nbsp;<%If lblManufacturerActive = "1" Then%><%=FontStyle%>
          <font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
          <%=RSSearchResult("MFG")%>&nbsp;<%Else%>&nbsp;<% End If %>
          </td>

        </tr>
        <tr>

          <td colspan="3" valign="top" align="left"><%If lblDescriptionActive = "1" Then%>
            <%=FontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
            <small><u><b><%= RSSearchResult("DESCRIPTION") %></b></u></small></font>
            <%Else%>&nbsp;<%End If%><br>
            <%If lblLngDescriptionActive = "1" Then%><%=FontStyle%>
            <font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
            <%= RSSearchResult("LONG_DESCRIPTION") %></font><%Else%>&nbsp;<%End If%>
          </td>    

        </tr>
        <tr>
   
          <td align="left" valign="bottom" nowrap>&nbsp;&nbsp;
            <%If Trim(RSSearchResult("AttSwitch")) = "1" Then %>
            <% If lblAttributeAActive = "1" and RSSearchResult("AttributeA") <> "" Then %>
            <%=FontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
            <small><b><%=lblAttributeA %></b></small></font>
            <hr size=1 color=<%=CellColor%>><%End If%><% End If %>
          </td>

          <td align="left" valign="bottom" nowrap>&nbsp;&nbsp;
            <%If Trim(RSSearchResult("AttSwitch")) = "1" Then %>
            <%If lblAttributeBActive = "1" and RSSearchResult("AttributeB") <> "" Then %>
            <%=FontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
            <small><b><%=lblAttributeB %></b></small></font><hr size=1 color=<%=CellColor%>>
            <%End If%><% End If %>
          </td>

          <td align="left" valign="bottom" nowrap>&nbsp;&nbsp;
            <%If Trim(RSSearchResult("AttSwitch")) = "1" Then %>
            <%If lblAttributeCActive = "1" and RSSearchResult("AttributeC") <> "" Then %>
            <%=FontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
            <small><b><%=lblAttributeC %></b></small></font><hr size=1 color=<%=CellColor%>>
            <%End If%><% End If %>
          </td>

        </tr>

        <tr>

          <td align=left nowrap>&nbsp;&nbsp;<% If Trim(RSSearchResult("AttSwitch")) = "1" Then %><%If lblAttributeAActive = "1" and RSSearchResult("AttributeA") <> "" Then %><select name="AttributeA">
              <% If InStr(RSSearchResult("ATTRIBUTEA"),",") Then
dim AttA, a
AttA = split(RSSearchResult("ATTRIBUTEA"),",")
a = 0
for each element in AttA
%>
              <option><%=AttA(a)%></option>
              <%
a = a + 1
next
%>
              <% Else %>
              <option><%=RSSearchResult("ATTRIBUTEA")%></option>
              <%End If%>
            </select>
            <%End If%><% End If %>
          </td>

          <td align=left nowrap>&nbsp;&nbsp;<% If Trim(RSSearchResult("AttSwitch")) = "1" Then %><%If lblAttributeBActive = "1" and RSSearchResult("AttributeB") <> "" Then %><select name="AttributeB">
              <% If InStr(RSSearchResult("ATTRIBUTEB"),",") Then
dim AttB, b
AttB = split(RSSearchResult("ATTRIBUTEB"),",")
b = 0
for each element in AttB
%>
              <option><%=AttB(b)%></option>
              <%
b = b + 1
next
%>
              <% Else %>
              <option><%=RSSearchResult("ATTRIBUTEB")%></option>
              <%End If%>
            </select>
            <%End If%><% End If %>
          </td>

          <td align=left nowrap>&nbsp;&nbsp;<% If Trim(RSSearchResult("AttSwitch")) = "1" Then %><%If lblAttributeCActive = "1" and RSSearchResult("AttributeC") <> "" Then %>
            <select name="AttributeC">
              <% If InStr(RSSearchResult("ATTRIBUTEC"),",") Then
dim AttC, c
AttC = split(RSSearchResult("ATTRIBUTEC"),",")
c = 0
for each element in AttC
%>
              <option><%=AttC(c)%></option>
              <%
c = c + 1
next
%>
              <% Else %>
              <option><%=RSSearchResult("ATTRIBUTEC")%></option>
              <%End If%>
            </select>
            <%End If%><% End If %>
          </td>
        </tr>
        <tr>
          <td valign="top" align="left">&nbsp;
          </td>    
          <td valign="top" align="left">&nbsp;
          </td>    

          <td align="right" nowrap>&nbsp;&nbsp;<%=FontStyle%>
            <font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
            <%=lblPRICE%>:&nbsp;<%= FormatCurrency(RSSearchResult("PRICE")) %></font>&nbsp;
          </td>

          <td align="right" nowrap>&nbsp;&nbsp;<%=FontStyle%>
            <font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
Quantity:&nbsp;</font><input type="textbox" name="QUANTITY" size="2">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          </td>
        </tr>
        <tr>
          <td valign="bottom" colspan="4" align=right><input type="image" name="Submit" src="images/order.gif" alt="Add to Cart" align="middle" border="0" width="101" height="20"></td>
        </tr>
        <tr>
          <td colspan="4" align=center><hr size=8 color=<%=CellColor%>>&nbsp;</td>
        </tr>
        <!--webbot bot="HTMLMarkup" startspan -->
</form>
<!--webbot bot="HTMLMarkup" endspan -->

        <% 
RowCount = RowCount - 1
RSSearchResult.MoveNext
Loop
%>
      </table>
    </div>
    <% 
	'Set RSSearchResult = RSSearchResult.NextRecordSet
	'Loop
	Connection.Close
	'Set RSSearchResult = nothing
	Set Connection = nothing
%>
    <div align=<%=TableAlign%>>
      <table border=<%=BorderSize%> bordercolor=<%=BorderColor%> cellpadding=<%=CellPadding%> cellspacing=<%=CellSpacing%> width=<%=TableWidth%> bgcolor=<%=TableBG%>>
        <tr>
          <td>
            <form action="<%= FormAction %>?" method="get">
              <input type="hidden" name="DESCRIPTION" value="<%= Request("DESCRIPTION") %>">
              <input type="hidden" name="MANUFACTURER" value="<%= Request("MANUFACTURER") %>">
              <input type="hidden" name="PRODUCT_ID" value="<%= Request("PRODUCT_ID") %>">
              <input type="hidden" name="CATEGORY" value="<%= Request("CATEGORY") %>">
              <input type="hidden" name="SQLStmt" value="<%= SQLStmt %>">
              <% If PageNo > 1 Then %>
              <p><input type="submit" name="ScrollAction" value="<%="Page " & PageNo-1%>">
              <% End If %>
              <% If RowCount = 0 AND (PageNo * intPageSize) <> intNumRecs Then %> 
              <p><input type="submit" name="ScrollAction" value="<%="Page " & PageNo+1 %>"> 
              <% End If %></p>
&nbsp;</td>
            </tr>
          </form>
        </table>
      </div>
      <!--#include file="search_results_foot.htm"--><% End If %><% ElseIf RSSearchResult.EOF or RSSearchResult is nothing Then %><!--#include file="search_results_head.htm"-->
      <%=FontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
      <p>I'm Sorry, your search for&nbsp; <% If Request("CATEGORY") = "" Then %> <%If lblCategoryActive = "1" Then%><%=lblCategory%><%Else%>Category<%End If%> = ALL <% Else %> <%If lblCategoryActive = "1" Then%><%=lblCategory%><%Else%>Category<%End If%> = <%= Request("CATEGORY") %> <% End If %>
      <% If Request("MANUFACTURER") = "" Then %>and <%If lblManufacturerActive = "1" Then%><%=lblManufacturer%><%Else%>Manufacturer<%End If%> = ALL <% Else %> and <%If lblManufacturerActive = "1" Then%><%=lblManufacturer%><%Else%>Manufacturer<%End If%> = <%= Request("MANUFACTURER") %> <% End If %>
      <% If Request("DESCRIPTION") = "" Then %> and <%If lblDescriptionActive = "1" Then%><%=lblDescription%><%Else%>Description<%End If%> = (none specified) <% Else %> and <%If lblDescriptionActive = "1" Then%><%=lblDescription%><%Else%>Description<%End If%> = <%= Request("DESCRIPTION") %> <% End If %> 
      <% If Request("PRODUCT_ID") = "" Then %> and <%If lblProductIDActive = "1" Then%><%=lblProductID%><%Else%>Product ID<%End If%> = (none specified) <% Else %> and <%If lblProductIDActive = "1" Then%><%=lblProductID%><%Else%>Product ID<%End If%> = <%= Request("PRODUCT_ID") %> <% End If%> produced no matches.</p></font>
      <!--#include file="search_results_foot.htm"-->
      <% End If %>
      <% End If %>
      <% If Request("ORDER_FLAG") = "1" Then %>
      <p>
      <div align=<%=TableAlign%>>
        <table border=<%=BorderSize%> bordercolor=<%=BorderColor%> cellpadding=<%=CellPadding%> cellspacing=<%=CellSpacing%> width=<%=TableWidth%> bgcolor=<%=TableBG%>>
          <form action="<%= FormAction %>?" method="get" id=form1 name=form1>
            <tr>
              <td bgcolor="<%=CellColor%>" colspan="2" align="center">
                <input type="hidden" name="DESCRIPTION" value="<%= Request("SRCH_DESCRIPTION") %>"><input type="hidden" name="MANUFACTURER" value="<%= Request("SRCH_MANUFACTURER") %>"><input type="hidden" name="CATEGORY" value="<%= Request("SRCH_CATEGORY") %>"><input type="hidden" name="PRODUCT_ID" value="<%= Request("SRCH_ID") %>"><% 
PageNo = Request.QueryString("PageNo")
RowCount = Request.QueryString("RowCount")
  %>
                <% If PageNo > 1 Then %> <input type="hidden" name="ScrollAction" value="<%="Page " & PageNo %>"> <% End If %><input type="submit" name="CONTINUE SEARCH" value="Continue Search">

                <% Connection.Close 
   Set Connection = Nothing
%>
	&nbsp;</td>
            </tr>
          </form>
        </table>
        <p>
        <!--#include file="thanks_foot.htm"-->
        </p>
      </div>

      <% End If %></center>
    </body>
  </html>




