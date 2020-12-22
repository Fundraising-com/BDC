<%
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   System      :   StoreFront 2000 Version 4.04.0
'   Date        :   1.6.2000
'   Author      :   LaGarde, Incorporated
'   Description :   StoreFront Promotional Mail Subscription Routines.
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
	Dim DSN_Name
	DSN_Name = Session("DSN_Name")
	set Connection = Server.CreateObject("ADODB.Connection")
	Connection.Open DSN_Name
	ORDER_ID = Session("ORDER_ID")
%>

<%
	PREV_ID = Trim(Request.Cookies("CustID"))

	If PREV_ID <> "" Then
	SQLStmt = "SELECT * FROM Customer WHERE CUSTOMER_ID = " & PREV_ID & ""
	Set RSPrevOrd = Connection.Execute(SQLStmt)
%>
<!--#include file="SFLib/cookie.inc"-->
<!--#include file="SFLib/design.inc"-->
<%
	End If 
	
'****** GET COUNTRIES FOR SHIP TO LIST **********************	
	SQL = "SELECT country, country_abb FROM locales WHERE (active = '1' " _
		& "AND country <> NULL)"
	set RSCountry = Connection.Execute (SQL)

'****** GET STATES FOR SHIP TO LIST *************************
	SQL = "SELECT state, state_abb FROM locales WHERE (active = '1' " _
		& "AND state <> NULL)"
	set RSState = Connection.Execute (SQL)

%>
<!--#include file="SFLib/design.inc"-->

<%


	If Request("Action") = "1" Then
	

	NAME = Replace(Request("NAME"),"'","''")
	COMPANY = Replace(Request("COMPANY"),"'","''")
	ADDRESS_1 = Replace(Request("ADDRESS_1"),"'","''")
	ADDRESS_2 = Replace(Request("ADDRESS_2"),"'","''")
	CITY = Replace(Request("CITY"),"'","''")
	STATE = Replace(Request("STATE"),"'","''")
	COUNTRY = Replace(Request("COUNTRY"), "'","''")
	ZIP = Request("ZIP")
	PHONE = Request("PHONE")
	E_MAIL = Request("E_MAIL")

	SQL = "UPDATE CUSTOMER SET NAME = '" & NAME & "', " _
		& "COMPANY = '" & COMPANY & "', " _
		& "ADDRESS_1 = '" & ADDRESS_1 & "', " _
		& "ADDRESS_2 = '" & ADDRESS_2 & "', " _
		& "CITY = '" & CITY & "', " _
		& "STATE = '" & STATE & "', " _
		& "ZIP = '" & ZIP & "', " _
		& "COUNTRY = '" & COUNTRY & "', " _
		& "PHONE = '" & PHONE & "', " _
		& "E_MAIL = '" & E_MAIL & "', " _
		& "MAIL_SUBSCRIBED = '1' " _
		& "WHERE CUSTOMER_ID = " & ORDER_ID & ""

	Set RSSetMail = Connection.Execute(SQL)
	Set RSSetMail = nothing
%>
<!--#include file="mail_head_confirm.htm"-->

 <table border=<%=BorderSize%> bordercolor=<%=BorderColor%> cellpadding=<%=CellPadding%> cellspacing=<%=CellSpacing%> width=<%=TableWidth%> bgcolor=<%=TableBG%>>
      
    <tr>
      <td align="right"><font size="2"><b>Name: </b></font></td>
      <td><%= Request("NAME") %></td>
    </tr>
    <tr>
      <td align="right"><font size="2"><b>Company: </b></font></td>
      <td><%= Request("COMPANY") %></td>
    </tr>
    <tr>
      <td align="right"><font size="2"><b>Address: </b></font></td>
      <td><%=Request("ADDRESS_1") %></td>
    </tr>
    <tr>
      <td align="right"><font size="2"><b>Address: </b></font></td>
      <td><%= Request("ADDRESS_2") %></td>
    </tr>
    <tr>
      <td align="right"><font size="2"><b>City: </b></font></td>
      <td><%= Request("CITY") %></td>
    </tr>
    <tr>
      <td align="right"><font size="2"><b>State or Province: </b></font></td>
      <td><%= Request("STATE") %></td>
    </tr>
    <tr>
      <td align="right"><font size="2"><b>Country:</td>
        <td><%= Request("COUNTRY") %></td>
        </tr>
        <tr>
          <td align="right"><font size="2"><b>Zip or Postal Code: </b></font></td>
          <td><%= Request("ZIP") %></td>
        </tr>

        <tr>
          <td align="right"><font size="2"><b>Phone: </b></font></td>
          <td><%= Request("PHONE") %></td>
        </tr>
        <tr>
          <td align="right"><font size="2"><b>E-Mail Address: </b></font></td>
          <td><%= Request("E_MAIL") %></td>
        </tr>
      </table>
      <% Else %>
      
     <!--#include file="mail_head.htm"-->

      <%=FontStyle%><font face=<%=FontType1&","&FontType2%> color=<%=FontColor%> size=<%=FontSize%>>
      <div align=<%=TableAlign%>>
        <table border=<%=BorderSize%> bordercolor=<%=BorderColor%> cellpadding=<%=CellPadding%> cellspacing=<%=CellSpacing%> width=<%=TableWidth%> bgcolor=<%=TableBG%>>
          <form action="mail.asp?Action=1" method="post" id=form1 name=form1>
    
            <tr>
              <td align="right"><font size="2"><b>Name: </b></font></td>
              <td><input type="text" name="NAME" value="<%= P_NAME %>" size="40"></td>
            </tr>
            <tr>
              <td align="right"><font size="2"><b>Company: </b></font></b></font></td>
          <td><input type="text" name="COMPANY" value="<%= P_COMPANY %>" size="40"></td>

        </tr>
        <tr>
          <td align="right"><font size="2"><b>Address: </b></font></td>
          <td><input type="text" name="ADDRESS_1" value="<%= P_ADDRESS1 %>" size="40"></td>
        </tr>
        <tr>
          <td align="right"><font size="2"><b>Address: </b></font></td>
          <td><input type="text" name="ADDRESS_2" value="<%= P_ADDRESS2 %>" size="40"></td>
        </tr>
        <tr>
          <td align="right"><font size="2"><b>City: </b></font></td>
          <td><input type="text" name="CITY" value="<%= P_CITY %>" size="40"></td>
        </tr>
        <tr align="center">
          <td align="right"><font size="2"><b>State or Province: </b></font></b></font></td>
          <td align="left"><select name="State" size="1">
               	<%
		CurrentRecord = 0
		Do While NOT RSState.EOF
		%>
		      <option value="<%= RSState("State_Abb") %>"><%= RSState("State") %></option>
		      <%
		RSState.MoveNext
		CurrentRecord = CurrentRecord = 1
		Loop
		%>	
            </select></td>
        </tr>
      <% Set RSState = nothing %>
        <tr align="center">
          <td align="right"><font size="2"><b>Country</b></font></td>
          <td align="left"><select name="COUNTRY" size="1">
    	      <%
		CurrentRecord = 0
		Do While NOT RSCountry.EOF
		%>
		      <option value="<%= RSCountry("Country_Abb") %>"><%= RSCountry("Country") %></option>
		      <%
		RSCountry.MoveNext
		CurrentRecord = CurrentRecord = 1
		Loop
		%>	

            </select></td>
        </tr>
      <% Set RSCountry = nothing %>
      <tr>
        <td align="right"><font size="2"><b>Zip or Postal Code: </b></font></td>
        <td><input type="text" name="ZIP" value="<%= P_ZIP %>" size="40"></td>
      </tr>
  
      <tr>
        <td align="right"><font size="2"><b>Phone: </b></font></td>
        <td><input type="text" name="PHONE" value="<%= P_PHONE %>" size="40"></td>
      </tr>
      <tr>
        <td align="right"><font size="2"><b>E-Mail Address: </b></font></td>
        <td><input type="text" name="E_MAIL" value="<%= P_EMAIL %>" size="40"></td>
      </tr>
      <tr>
        <td align="right">&nbsp;</td>
        <td><input type="submit" name="Subscribe" value="subscribe"></td>
      </tr>
    
    </form>
  </table>
  </font>
</div>

<% End If %>
<% Connection.Close 
   Set Connection = Nothing
%>
<!--#include file="mail_foot.htm"-->



