<%	
	option explicit 
	Response.Buffer = True
'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0007.asp
	
'@FILEVERSION: 1.0.1

'@VERSIONDATETIME: 2/21/01

'@DESCRIPTION:   web Admin tool

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws  and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2000, 2001 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO	
%>
<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SFLib/adovbs.inc"-->
<!--#include file="../SFLib/incDesign.asp"-->
<!--#include file="incAdmin.asp"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="../error_trap.asp"-->
<%
' Variable Declarations
Dim objRS,rsAdminSettings,aCtry,aState,iChange, i,sOandaId,iChecked,ctryList,stateList
	
	' LCID listing
	Set objRS = getList(2,0,0) 
	
	' Admin table
	Set rsAdminSettings = Server.CreateObject("ADODB.recordSet")
	rsAdminSettings.Open "SELECT adminLCID, adminOandaID, adminActivateOanda FROM sfAdmin", cnn, adOpenStatic, adLockOptimistic, adCmdText

	'OANDA Information
	sOandaId = rsAdminSettings.Fields("adminOandaID")
	If trim(rsAdminSettings.Fields("adminActivateOanda")) = "1" Then
		iChecked = "checked"
	Else
		iChecked = ""
	End If
	
	For i = 1 to Request.Form("iCtryCounter")
		If Request.Form("DC" & i & ".x") <> "" Then	
			Call setCountry(Trim(Request.Form("DC"&i)),0)
			iChange = 1
			Exit For
		End If	
		
	Next
	
	For i = 1 to Request.Form("iStateCounter")
		If Request.Form("DS" & i & ".x") <> "" Then	
			Call setState(Trim(Request.Form("DS"&i)),0)
			iChange = 1
		Exit For
		End If		
	Next	
	
	If Request.Form("submitLocation.X") <> "" Then	
		iChange = 1
		'Set Oanda Information
		rsAdminSettings.Fields("adminOandaID") = Trim(Request.Form("txtOandaID"))
		sOandaId = Trim(Request.Form("txtOandaID"))
		If Trim(Request.Form("chkActivateOanda")) = "1" Then
			rsAdminSettings.Fields("adminActivateOanda") = "1"
			iChecked = "checked"
		Else
			rsAdminSettings.Fields("adminActivateOanda") = "0"
			iChecked = ""
		End If
		
		' Set New Location 
		rsAdminSettings.Fields("adminLCID") = Trim(Request.Form("locationValue"))
		rsAdminSettings.Update
		
		' Save New ship location
		aCtry = Split(Trim(Request.Form("CtryList")),",")
		aState = Split(Trim(Request.Form("StateList")),",")
		
		For i = 0 to Ubound(aCtry)
			Call setCountry(trim(aCtry(i)),1)
		Next
		
		For i = 0 to Ubound(aState)
			Call setState(trim(aState(i)),1)
		Next
	End If
		

		
%>
<html>

<head>
<title>SF Menu Page</title>
</head>

<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">
<form method="post">
<table border="0" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>" width="<%= C_WIDTH %>" align="center">
<tr>
<td>
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
    <tr>
<%	If C_BNRBKGRND = "" Then %>
		<td align="middle" background="<%= C_BKGRND1 %>" bgcolor="<%= C_BGCOLOR1 %>"><b><font face="<%= C_FONTFACE1 %>" color="<%= C_FONTCOLOR1 %>" SIZE="<%= C_FONTSIZE1 %>"><%= C_STORENAME %></font></b></td>
<%	Else %>
		<td align="middle" bgcolor="<%= C_BNRBGCOLOR %>"><img src="<%= C_BNRBKGRND %>" border="0"></td>
<%	End If %>        
    </tr>
    <tr>
	<td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">International Settings</font></b></td>        
    </tr>
    <tr>
	<td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b>Use this interface to set the Locale Identifier (LCID) of your web.  The LCID will determine the currency type that is used in your web.  Use the settings under Supported Shipping Destinations to choose the countries you will be accepting orders from and shipping orders to by highlighting the country and state names with your mouse pointer (you can use Shift+left-click and Ctrl+left-click to select multiple countries/states) then clicking the Submit button..  If you need additional help, please consult the StoreFront help files for step-by-step instructions.</font></td>    
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
        <table border="0" width="100%" cellpadding="4" cellspacing="0">
         <% If iChange = 1 Then %>
        <tr>
        <td width="100%" colspan="2" align="center" height="90" valign="center">
			<table width="60%" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>">
			<tr><td width="100%">
				<table cellpadding="5" cellspacing="0" bgcolor="<%= C_BGCOLOR4 %>" width="100%">
				<tr><td width="100%" bgcolor="<%= C_BGCOLOR4 %>" align="center" background="<%= C_BKGRND5 %>">
				<b><font face="<%= C_FONTFACE5 %>" color="#992222" size="<%= C_FONTSIZE5+2 %>"><b>Database Updated
				</font>
				<br><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5 %>"><a href="menu.asp">Return to Menu</b></a>
				</font></b>
				</td></tr>
				</table>
			</td></tr>	
			</table>
        </td>
        </tr>
		<% End If %>
        <tr>
		<td width="100%" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>" colspan="2"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Language Currency/Configuration</font></b></td>        
        </tr>
        <tr>
        <td width="50%" height="60" align="right" valign="center"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Select Country - Name:</font></td>
        <td width="50%" height="60" valign="center">
        <select size="1" style="<%= C_FORMDESIGN %>" name="locationValue">
          
<%	

			Dim sDropDownElement, sLocationIdInUse, sLocationIdToAdd
			sLocationIdInUse = Trim(rsAdminSettings.Fields("adminLCID"))
			
			Do While NOT objRS.EOF
				sLocationIdToAdd = objRS("slctvalLCID")
				sDropDownElement = "<option "
				If sLocationIdToAdd = sLocationIdInUse Then 
					sDropDownElement = sDropDownElement & "selected "
				End If
				
				sDropDownElement = sDropDownElement _
					& "value=" & sLocationIdToAdd & ">" & objRS("slctvalLCIDLabel") & "</option>"	
				Response.Write sDropDownElement
				objRS.MoveNext
			Loop
%>            
        </select></td>
        </tr>
		<tr>
		<td width="100%" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>" colspan="2"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Currency Conversion (must obtain account from OANDA)</font></b></td>        
        </tr>
		<tr>
        <td width="100%" align="center" colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">OANDA User ID:&nbsp;</font><input type="text" name="txtOandaID" value="<%= sOandaId %>" style="<%= C_FORMDESIGN %>"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">&nbsp;Activate&nbsp;</font><input type="checkbox" name="chkActivateOanda" value="1" <%= iChecked %>></td>
        </tr>
        <tr>
			<td width="100%" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>" colspan="2"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Supported Shipping Destinations</font></b></td>        
        </tr>

		<tr>
			<td valign="top" width="50%" align="center"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><br><center><b>Countries</b></center>
			<br>
			<table width="100%">
				<tr>
					<td align="center">
					<%
						Dim rsShipCountry,sSQL, iCtryCounter
						sSQL = "SELECT loclctryAbbreviation, loclctryName FROM sfLocalesCountry WHERE loclctryLocalIsActive = 1 ORDER BY loclctryName"
							Set rsShipCountry = Server.CreateObject("ADODB.RecordSet")
							rsShipCountry.Open sSQL, cnn, adOpenDynamic, adLockOptimistic, adCmdText
					
							If rsShipCountry.EOF Then
					%>	
							<i>No Country Specified</i>
					<%
							Else	
					%>
					
						<table width="100%">
					<%		
								iCtryCounter = 1	
								Do While Not rsShipCountry.EOF																				
					%>	
						<tr><td><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">							
							<%= rsShipCountry.Fields("loclctryName") %></td>
							<td><input type="image" align="center" name="DC<%= iCtryCounter %>" src="images/delete.gif" border="0" WIDTH="82" HEIGHT="21">
							<input type="hidden" name="DC<%= iCtryCounter %>" value="<%= rsShipCountry.Fields("loclctryAbbreviation") %>">
							</td>
						</tr>
					<%		
								rsShipCountry.MoveNext
								iCtryCounter = iCtryCounter + 1
								Loop								
					%>
						<input type="hidden" name="iCtryCounter" value="<%= iCtryCounter%>">
						</table>	
					
					<%	
							End If
							closeobj(rsShipCountry)
					%>		
					</td>
				</tr>
			</table>
			</td>
			
			<td valign="top" width="50%" align="center"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><br><center><b>States/Provinces</b></center>
			<br>
				<table width="100%">
				<tr>
					<td align="center">
					<%
						Dim rsShipState,iStateCounter
						sSQL = "SELECT loclstAbbreviation, loclstName FROM sfLocalesState WHERE loclstLocaleIsActive = 1 ORDER BY loclstName"
							Set rsShipState = Server.CreateObject("ADODB.RecordSet")
							rsShipState.Open sSQL, cnn, adOpenDynamic, adLockOptimistic, adCmdText
					
							If rsShipState.EOF Then
					%>	
							<i>No State Specified</i>
					<%
							Else	
					%>
					
						<table width="100%">
					<%		
							iStateCounter = 1	
							Do While Not rsShipState.EOF																				
					%>	
						<tr><td><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">							
							<%= rsShipState.Fields("loclstName") %></td>
							<td><input type="image" align="center" name="DS<%= iStateCounter %>" src="images/delete.gif" border="0" WIDTH="82" HEIGHT="21">
							<input type="hidden" name="DS<%= iStateCounter %>" value="<%= rsShipState.Fields("loclstAbbreviation") %>">
							</td>
						</tr>
					<%		
								rsShipState.MoveNext
								iStateCounter = iStateCounter + 1
								Loop								
					%>
						<input type="hidden" name="iStateCounter" value="<%= iStateCounter%>">
						</table>	
					
					<%	
							End If
							closeobj(rsShipState)
					%>	
			</td>
			</tr></table>			
		</td>
		</tr>

        <tr>
			<td width="100%" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>" colspan="2"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Add Shipping Destinations</font></b></td>        
        </tr>

        <tr>
        <td width="50%" align="center" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><br><b>Countries</b>
        <p>
        <% 
        ctryList = getActiveCountryList(3)
        If ctryList <> "" Then
        %>
        <select name="CtryList" multiple size="10" style="<%= C_FORMDESIGN %>"><%= ctryList %></select>
        <%End If%>
        </font></td>
        <td width="50%" align="center" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><br><b>States/Provinces</b>	
        <p>
        <%
        stateList = getActiveStateList(3)
        If stateList <> "" Then
        %>
        <select name="StateList" multiple size="10" style="<%= C_FORMDESIGN %>"><%= stateList %></select>
        <% End If %>
        </font></td>	
        </tr>
        
        <tr>
        <td width="100%" height="40" align="center" valign="center" colspan="2"><input type="image" name="submitLocation" border="0" src="images/submit.gif" WIDTH="108" HEIGHT="21"></td>
        </tr>

        </table>
    </td>
    </tr>
        <tr>
		<td bgcolor="<%= C_BGCOLOR7 %>" background="<%= C_BKGRND7 %>"><font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>"><p align="center"><b><a href="menu.asp">Main Menu</a> | <a href="../../search.asp">Your Store</a></b></font></p></td>
        </tr>
</table>
</td>
</tr>
</table>
</form>
</body>
</html>

