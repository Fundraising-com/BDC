<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SFLib/ADOVBS.inc"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="../SFLib/incDesign.asp"-->
<!--#include file="incAdmin.asp"-->

<%
'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0003.asp
	
'@FILEVERSION: 1.0.0

'@VERSIONDATETIME: 2/21/01

'@DESCRIPTION:   web Admin tool

'@STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2000, 2001 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO	

For i = 1 to Request.Form("iCtryCounter")
	If Request.Form("DC" & i & ".x") <> "" Then	
		Call setResetTaxCtry(Trim(Request.Form("DC"&i)))
		Exit For
	End If	
Next
	
For i = 1 to Request.Form("iStateCounter")
	If Request.Form("DS" & i & ".x") <> "" Then	
		Call setResetTaxState(Trim(Request.Form("DS"&i)))
	Exit For
	End If
Next	

If Request.Form("Submit.x") <> "" Then
	
	If Trim(Request.Form("iCtryCounter")) <> "" Then
		For i = 1 to Trim(Request.Form("iCtryCounter"))
			Call setCtryTax(Trim(Request.Form("DC"&i)),Trim(Request.Form("CT"&i)))
		Next
	End If

	If Trim(Request.Form("iStateCounter")) <> "" Then
		For i = 1 to Trim(Request.Form("iStateCounter"))
			Call setStateTax(Trim(Request.Form("DS"&i)),Trim(Request.Form("ST"&i)))
		Next	
	End If	

	Dim sCtryAbbr, dCtryTax, sStateAbbr, dStateTax
		sCtryAbbr = Trim(Request.Form("inactiveCtry"))
		dCtryTax = Trim(Request.Form("CtryTax"))
		sStateAbbr = Trim(Request.Form("inactiveState"))
		dStateTax = Trim(Request.Form("StateTax"))
		
		If sCtryAbbr <> "" And dCtryTax <> "" Then
			Call setCtryTax(sCtryAbbr,dCtryTax)
		End If
		
		If sStateAbbr <> "" And dStateTax <> "" Then
			Call setStateTax(sStateAbbr,dStateTax)
		End If
End If
%>
<html>

<head>
<title>SF Menu Page</title>
</head>


<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>">

<form method="post" name="form1">
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
	<td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Tax Configuration</font></b></td>        
    </tr>
    <tr>
	<td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b>This interface will allow you to set tax rates for countries and states.  To set a tax rate for a country or state, select the country or state from the appropriate drop-down list and enter the tax rate in the field to the right of the drop-down.  Enter your tax rates as a decimal value of 1.  For example, a tax rate of 6.25% should be entered as .0625.  After entering the tax rate, click Submit to activate the tax.  
</font></td>    
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
        <table border="0" width="100%" cellpadding="4" cellspacing="0">
        <% If Request.Form("Submit.x") <> "" Then %>
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
		<td width="100%" colspan="2" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Tax Settings</font></b></td>
        </tr>
        
        <% Dim rsTaxCtry, sSQL, iCtryCounter
			sSQL = "SELECT loclctryAbbreviation,loclctryName,loclctryTax,loclctryTaxIsActive,loclctryLocalIsActive FROM sfLocalesCountry WHERE loclctryTaxIsActive = 1 AND loclctryLocalIsActive = 1"
			Set rsTaxCtry = Server.CreateObject("ADODB.RecordSet")
				rsTaxCtry.Open sSQL,cnn,adOpenDynamic,adLockOptimistic,adCmdText

		%>
			<tr><td colspan="2" width="100%" align="center">
			<table cellpadding="10" cellspacing="0" border="0" width="100%">
				
		<% If rsTaxCtry.EOF Then %>
			<tr>
				<td width="50%" align="left" height="50" valign="center"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
				<b>Active Country Tax</b>	
				<br><i>No taxes associated with countries</i>
				</td>   
		<% Else %>
			<tr>
				<td width="50%" align="left" valign="top">
				<table width="100%" cellpadding="5" cellspacing="0">				
	        <%		
				iCtryCounter = 1
				Do While Not rsTaxCtry.EOF				 			
		    %>
			<tr>
			<td width="100%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
			<%= rsTaxCtry.Fields("loclctryName") %>
			</td>
			<td> 
				<input type="text" size="3" name="CT<%= iCtryCounter %>" value="<%= rsTaxCtry.Fields("loclctryTax") %>" style="<%= C_FORMDESIGN %>">
			</td>	
			<td>
				<input type="image" src="images/delete.gif" border="0" name="DC<%= iCtryCounter%>" WIDTH="82" HEIGHT="21">
				<input type="hidden" name="DC<%= iCtryCounter %>" value="<%= rsTaxCtry.Fields("loclctryAbbreviation")%>">
			</td>
			</tr>
	        <%
				rsTaxCtry.MoveNext
				iCtryCounter = iCtryCounter + 1			
				Loop
				closeobj(rsTaxCtry)
			%>
			<input type="hidden" name="iCtryCounter" value="<%= iCtryCounter %>">
			</font>
			</table>
			</td>		
			
	    <% End If %>				

 		<% Dim rsTaxState,iStateCounter
			sSQL = "SELECT loclstAbbreviation,loclstName,loclstTax,loclstTaxIsActive,loclstLocaleIsActive FROM sfLocalesState WHERE loclstTaxIsActive = 1 AND loclstLocaleIsActive = 1"
			Set rsTaxState = Server.CreateObject("ADODB.RecordSet")
				rsTaxState.Open sSQL,cnn,adOpenDynamic,adLockOptimistic,adCmdText
	
			If rsTaxState.EOF Then	
			
		%>
			<td width="50%" align="left" height="50" valign="center"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
				<b>Active State Tax</b>	
				<br><i>No taxes associated with states</i>
			</td>  
			</tr>		
		<%	Else %>      
				
			<td width="50%" align="left" valign="top">
			<table width="100%" cellpadding="5" cellspacing="0">
		<%
			iStateCounter = 1
			Do While Not rsTaxState.EOF 			
		%>
		<tr>
			<td width="100%">
			<font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">		
			<%= rsTaxState.Fields("loclstName") %> 
			</td>
			<td>
			<input type="text" size="3" name="ST<%= iStateCounter %>" value="<%= rsTaxState.Fields("loclstTax") %>" style="<%= C_FORMDESIGN %>">
			</td>
			<td>
				<input type="image" border="0" name="DS<%= iStateCounter%>" src="images/delete.gif" WIDTH="82" HEIGHT="21">
				<input type="hidden" name="DS<%= iStateCounter %>" value="<%= rsTaxState.Fields("loclstAbbreviation") %>">
			</td>
		</tr>			
		<%
			rsTaxState.MoveNext
			iStateCounter = iStateCounter + 1
			Loop
			closeobj(rsTaxState)
        %>
			<input type="hidden" name="iStateCounter" value="<%= iStateCounter %>">
 			</font>
 			</table>
 		</td>
 		</tr>
		<% End If %>       
     	</table>
     	</td></tr>		

	   <tr>
		<td width="100%" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>" colspan="2"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Add Tax Settings To Supported Countries and States</font></b></td>        
        </tr>
        <tr>
        <td width="50%" align="center" valign="top">
        <br>
			<table>
			<tr><td nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Select a country</td>
			<td nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Tax Rate</td>
			</tr>
			<tr>
			<td><select name="inactiveCtry" size="1" style="<%= C_FORMDESIGN %>"><option></option><%= getActiveCountryList(1) %></select></td>
			<td><input type="text" size="5" name="CtryTax" style="<%= C_FORMDESIGN %>"></td>
			</tr>
			</table>
        </font></td>
        
        <td width="50%" align="center" valign="top">
        <br>
			<table>
			<tr><td nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Select a state</td>
			<td nowrap><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Tax Rate</td>
			</tr>
			<tr>
			<td><select name="inactiveState" size="1" style="<%= C_FORMDESIGN %>"><option></option><%= getActiveStateList(1) %></select></td>
			<td><input type="text" size="5" name="StateTax" style="<%= C_FORMDESIGN %>"></td>
			</tr>
			</table>
        </font></td>
        </tr>
                
        <tr>
        <td width="100%" align="center" valign="top" colspan="2"><input type="image" name="Submit" border="0" src="images/submit.gif" WIDTH="108" HEIGHT="21"></td>
        </tr>        
   
    </table>
    </td>
    </tr>    
        <tr>
		<td align="center" bgcolor="<%= C_BGCOLOR7 %>" background="<%= C_BKGRND7 %>"><font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>"><b><a href="menu.asp">Main Menu</a> | <a href="../../search.asp">Your Store</a></b></font></td>
		</tr>
</table>
</td>
</tr>
</table>
</form>

</body>

</html>





