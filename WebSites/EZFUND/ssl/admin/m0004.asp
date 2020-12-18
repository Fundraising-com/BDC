<%	
	option explicit 
	Response.Buffer = True
	'--------------------------------------------------------------------
'@BEGINVERSIONINFO

'@APPVERSION: 5.10.10

'@FILENAME: m0004.asp
	
'@FILEVERSION: 1.0.1

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
%>
<html>
<head>
<script language="javascript">
function fillform(formValue,sPaymentString) {
	var iresult;	
	aPay = new Array();
	aPay2 = new Array();
	aPay = sPaymentString.split(";");
	for (var i=0; i < aPay.length; i++) {
		iresult = (aPay[i]).charAt(0);
		iresult2 = (aPay[i]).charAt(1);
		if (iresult2 != ",") {
			iresult += iresult2;
		}
		if (iresult == formValue) {
			aPay2 = aPay[i].split(",");		
			form1.TransMethod.value = aPay2[0];
			form1.ServerPath.value = aPay2[2];
			form1.Login.value = aPay2[3];
			form1.Password.value = aPay2[4];
		}
	} 
}
function checkphonefax(order,phonefaxr,phonefaxnr) {
	if (phonefaxr == true && phonefaxnr == true) {
		alert('Please select only one phone fax option.');
		
		if (order == 1) {
		form1.PhoneFaxR.checked = true;
		form1.PhoneFaxNR.checked = false;
		}
		else if (order == 0) {
		form1.PhoneFaxR.checked = false;
		form1.PhoneFaxNR.checked = true;
		}

	}
}
</script>
<title>SF Menu Page</title>
</head>
<!--#include file="../SFLib/db.conn.open.asp"-->
<!--#include file="../SFLib/adovbs.inc"-->
<!--#include file="../SFLib/incDesign.asp"-->
<!--#include file="../SfLib/incGeneral.asp"-->
<!--#include file="incAdmin.asp"-->
<body background="<%= C_BKGRND %>" bgproperties="fixed" bgcolor="<%= C_BGCOLOR %>" link="<%= C_LINK %>" vlink="<%= C_VLINK %>" alink="<%= C_ALINK %>" onLoad>
<%
	Dim sPaymentType, sPaymentServerPath, sPaymentLogin, sPaymentPassword,bCardEncode,bCreditCard,bECheck,sMerchantType
	Dim bCOD, sCODAmount, bPhoneFaxRecorded, bInternetCash, bPO, bPhoneFax, bPhoneFaxNon_Recorded
	Dim bVisa, bMC, bDiscover, bAMEX, bDiners, bCarteBlanche, aIC, sInternetCashServerPath, sInternetCashMercID, sInternetCashMerchantKey
	
	If Request.Form("Submit.x") <> "" Then	
		sPaymentType			= Trim(Request.Form("TransMethod"))
		sPaymentServerPath		= Trim(Request.Form("ServerPath"))
		sPaymentLogin			= Trim(Request.Form("Login"))
		sPaymentPassword		= Trim(Request.Form("Password"))
		sMerchantType			= Trim(Request.Form("MerchantType"))
		bCardEncode				= Trim(Request.Form("EncodeCard"))
		bCreditCard				= Trim(Request.Form("CreditCard"))
		bECheck					= Trim(Request.Form("eCheck"))
		bCOD					= Trim(Request.Form("COD"))
		sCODAmount				= Trim(Request.Form("CODAmount"))
		bPhoneFaxRecorded		= Trim(Request.Form("PhoneFaxR"))
		bPhoneFaxNon_Recorded	= Trim(Request.Form("PhoneFaxNR"))				
		bInternetCash			= Trim(Request.Form("InternetCash"))
		bPO						= Trim(Request.Form("PO"))
		If bCreditCard = "" Then bCreditCard = 0
		If bECheck = "" Then bECheck = 0
		If bPhoneFaxRecorded = "" Then bPhoneFaxRecorded = 0
		If bPhoneFaxNon_Recorded = "" Then bPhoneFaxNon_Recorded = 0
		If bInternetCash <> "" Then 
			sInternetCashServerPath = Trim(Request.Form("InternetCashServerPath"))
			sInternetCashMercID = Trim(Request.Form("InternetCashMercID"))
			sInternetCashMerchantKey = Trim(Request.Form("InternetCashMercKey"))
		Else bInternetCash = 0	
		End If	
		If bPO = "" Then bPO = 0
		If bCOD = "" Then bCOD = 0	
		Call setUpdateAdminPayment(sPaymentType,sPaymentServerPath,sPaymentLogin,sPaymentPassword,sMerchantType,bCardEncode,bCreditCard,bECheck,bCOD,sCODAmount,bPhoneFaxRecorded,bPhoneFaxNon_Recorded,bInternetCash,sInternetCashServerPath,sInternetCashMercID,sInternetCashMerchantKey,bPO)
	
		' Set credit cards
		bVisa		= Trim(Request.Form("Visa"))
		bMC			= Trim(Request.Form("MC"))
		bDiscover	= Trim(Request.Form("Discover"))
		bAMEX		= Trim(Request.Form("AMEX"))
		bDiners		= Trim(Request.Form("Diners"))
		bCarteBlanche = Trim(Request.Form("CarteBlanche"))
		Call setUpdateCC(bVisa,bMC,bDiscover,bAMEX,bDiners,bCarteBlanche,bCreditCard)
	End If
	
		' Variable Declarations
		Dim aRS, iRow, sPaymentString, sPayString, sJavaScript, sPaymentTypes, aAdmin, bCardEncoded, sTransServer, sLogin, sPassword
		Dim bPhoneFaxR, bPhoneFaxNR, sTransMethod, sCreditCard
	
		aRs = getTransactionMethods() 'getList(1,"sfSelectValues", "slctvalTransMethod") 
		sPaymentTypes = getPaymentMethods()
		sCreditCard = getCreditCards()
	
			If Instr(1,sPaymentTypes,"Credit Card") Then
				bCreditCard = 1
			Else 
				bCreditCard = 0	
			End If
			If Instr(1,sPaymentTypes,"eCheck") Then
				bECheck = 1
			Else 
				bECheck = 0	
			End If
			If Instr(1,sPaymentTypes,"COD") Then
				bCOD = 1
			Else 
				bCOD = 0	
			End If
			If Instr(1,sPaymentTypes,"PhoneFaxRecorded") Then
				bPhoneFaxR = 1
			Else 
				bPhoneFaxR = 0	
			End If
			If Instr(1,sPaymentTypes,"PhoneFaxNon-Recorded") Then
				bPhoneFaxNR = 1
			Else
				bPhoneFaxNR = 0	
			End If
			If Instr(1,sPaymentTypes,"InternetCash") Then
				bInternetCash = 1
			Else
				bInternetCash = 0	
			End If
			If Instr(1,sPaymentTypes,"PO") Then
				bPO = 1
			Else 
				bPO = 0	
			End If				
	
			If Instr(1,sCreditCard,"Visa") Then
				bVisa = 1 
			Else 
				bVisa = 0	
			End If	

			If Instr(1,sCreditCard,"MasterCard") Then
				bMC = 1 
			Else 
				bMC = 0	
			End If	

			If Instr(1,sCreditCard,"American Express") Then
				bAMEX = 1 
			Else 
				bAMEX = 0	
			End If		

			If Instr(1,sCreditCard,"Discover") Then
				bDiscover = 1 
			Else 
				bDiscover = 0	
			End If	

			If Instr(1,sCreditCard,"Diners Club") Then
				bDiners = 1 
			Else 
				bDiners = 0	
			End If	

			If Instr(1,sCreditCard,"Carte Blanche") Then
				bCarteBlanche = 1 
			Else 
				bCarteBlanche = 0	
			End If				

			' collect merchant type and credit card encoding info
			aAdmin				= CheckCardMerchant()
			bCardEncoded		= Trim(aAdmin(0,0))
			sMerchantType		= Trim(aAdmin(1,0))
			sCODAmount			= Trim(aAdmin(2,0))
			sTransMethod		= Trim(aAdmin(3,0)) 					
			sTransServer		= Trim(aAdmin(4,0))			
			sLogin				= Trim(aAdmin(5,0))
			sPassword			= Trim(aAdmin(6,0))
			aIC					= InternetCash()
			sInternetCashServerPath = aIC(0)
			sInternetCashMercID = aIC(1)
			sInternetCashMerchantKey = aIC(2)
%>
<form method="post" name="form1" action="m0004.asp">
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
    <td align="middle" background="<%= C_BKGRND2 %>" bgcolor="<%= C_BGCOLOR2 %>"><b><font face="<%= C_FONTFACE2 %>" color="<%= C_FONTCOLOR2 %>" SIZE="<%= C_FONTSIZE2 %>">Transaction Processing</font></b></td>        
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR3 %>" background="<%= C_BKGRND3 %>"><font face="<%= C_FONTFACE3 %>" color="<%= C_FONTCOLOR3 %>" size="<%= C_FONTSIZE3 %>"><b>Instructions: </b> Select the payment methods that you will be accepting payment in, and use the options below to configure a payment processing service.  You can find more information about using payment processing services and selecting payment methods in the StoreFront help files and <a href=http://www.storefront.net/software/support/kbase.asp>StoreFront Knowledge Base.</a>
</font></td>    
    </tr>
    <tr>
    <td bgcolor="<%= C_BGCOLOR4 %>" background="<%= C_BKGRND4 %>" width="100%" nowrap>
        <table border="0" width="100%" cellpadding="4" cellspacing="0">
        <% If Request.Form("Submit.x") <> "" Then %>
        <tr>
        <td width="100%" colspan="3" align="center" height="90" valign="center">
			<table width="60%" cellpadding="1" cellspacing="0" bgcolor="<%= C_BORDERCOLOR1 %>">
			<tr><td width="100%">
				<table cellpadding="5" cellspacing="0" bgcolor="<%= C_BGCOLOR4 %>" width="100%">
					<tr><td width="100%" bgcolor="<%= C_BGCOLOR4 %>" align="center" background="<%= C_BKGRND5 %>">
					<b><font face="<%= C_FONTFACE5 %>" color="#992222" size="<%= C_FONTSIZE5+2 %>"><b>Database Updated</font>
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
        <td width="100%" colspan="3" bgcolor="<%= C_BGCOLOR5 %>" background="<%= C_BKGRND5 %>"><b><font face="<%= C_FONTFACE5 %>" color="<%= C_FONTCOLOR5 %>" size="<%= C_FONTSIZE5+1 %>">Transaction Setup</font></b></td>
        </tr>
        <tr>
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Transaction Method:</font></td>
        <%
        iRow = 0
		For iRow = 0 to (UBound(aRs, 2))
			sPaymentString = sPaymentString & aRs(0,iRow) & "," & aRs(1,iRow) & "," & aRs(2,iRow) & "," & aRs(3,iRow) & "," & aRs(4,iRow) & ";"
			If Trim(sTransMethod) = Trim(aRS(0,iRow)) Then
				sPayString = sPayString & "<option selected value="& aRS(0,iRow) &">"& aRS(1,iRow) &"</option>"
			Else
				sPayString = sPayString & "<option value="& aRS(0,iRow) &">"& aRS(1,iRow) &"</option>"
			End If		
		Next
	    sJavaScript = "javascript:fillform(form1.TransMethod.value,'" & sPaymentString & "')"
        %>
        <td colspan="2"><select name="TransMethod" style="<%= C_FORMDESIGN %>" onChange="<%= sJavaScript%>">
          <%= sPayString %> 
          </select>
          </font>
        </td>
        </tr>
        <tr>
        <td align="right" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Payment Server Path:</font></td>
        <td colspan="2"><input name="ServerPath" style="<%= C_FORMDESIGN %>" size="40" value="<%= sTransServer %>"></td>
        </tr>
        <tr>
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Login:</font></td>
        <td colspan="2"><input name="Login" style="<%= C_FORMDESIGN %>" value="<%= sLogin %>" size="40"></td>        
        </tr>
        <tr>
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Password:</font></td>
        <td colspan="2"><input type="Passwd" name="Password" style="<%= C_FORMDESIGN %>" value="<%= sPassword %>" size="40"></td>
        </tr> 
        <tr>
        <td align="right" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Merchant Type:</font></td>
        <td colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input CHECKED name="MerchantType" type="radio" value="authonly" <% If sMerchantType = "normal_auth" Then %>Checked<%End If%>>AuthOnly<input name="MERCHANT_TYPE" type="radio" value="authcapture" <% If sMerchantType = "authcapture" Then %>checked<%End If%>>AuthCapture</font></td>		
        </tr>
        <tr>
        <td align="center" colspan="3"><hr noshade color="#000000" size="1" width="90%"></td>
        </tr>     
        <tr>      
        <td align="center" colspan="3"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Supported Credit Cards:</font></td>
        </tr>
        <tr>
        <td colspan=3" align="center">
        <p>
			<table width="50%">
			<tr><td><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
				<input type="checkbox" value="1" name="Visa" <%If bVisa = 1 Then %>checked<%End If%>>Visa
				<br><input type="checkbox" value="1" name="MC" <%If bMC = 1 Then %>checked<%End If%>>Mastercard
				<br><input type="checkbox" value="1" name="Discover" <%If bDiscover = 1 Then %>checked<%End If%>>Discover
				</font></td>
				<td><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">
				<input type="checkbox" value="1" name="AMEX" <%If bAMEX = 1 Then %>checked<%End If%>>American Express
				<br><input type="checkbox" value="1" name="Diners" <%If bDiners = 1 Then %>checked<%End If%>>Diner's Club
				<br><input type="checkbox" value="1" name="CarteBlanche" <%If bCarteBlanche = 1 Then %>checked<%End If%>>Carte Blanche
				</font></td>
			</tr>
			</table>
        </td>
        </tr>        
        
        <tr>
        <td align="center" colspan="3"><hr noshade color="#000000" size="1" width="90%"></td>
        </tr>  
        <tr>      
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Encode Credit Card Data:</font></td>
        <td colspan="2"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"><input type="radio" value="1" name="EncodeCard" <%If bCardEncoded = 1 Then %>checked<%End If%>>Yes <input type="radio" name="EncodeCard" value="0" <%If bCardEncoded = 0 Then %>checked<%End If%>>No</font></td>
        </tr>
        <tr>
        <td align="center" colspan="3"><hr noshade color="#000000" size="1" width="90%"></td>
        </tr>
        <tr>
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Credit Card:</font></td>
        <td><input type="checkbox" name="CreditCard" value="1" <% If bCreditCard = 1 Then%>checked <%End If%>></td>
        <td></td>
        </tr>
        <tr>
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Electronic Check:</font></td>
        <td><input type="checkbox" name="eCheck" value="1" <% If bECheck = 1 Then%>checked <%End If%>></td>
        <td></td>
        </tr>
        <tr>
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Purchase Order:</font></td>
        <td><input type="checkbox" name="PO" value="1" <% If bPO = 1 Then%>checked <%End If%>></td>
        <td></td>
        </tr>        
        <tr>
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">COD:</font></td>
        <td><input type="checkbox" name="COD" value="1" <% If bCOD = 1 Then%>checked <%End If%>></td>
        <td>
        	<table width="100%" border="0">
			<tr>
				<td width="30%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>"> Amount: </font></td>
				<td width="70%" align="left"><input name="CODAmount" style="<%= C_FORMDESIGN %>" size="10" value="<%= sCODAmount%>"></font></td>
			</tr>
			</table>	
		</td>		
        </tr>
        <tr>
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Phone or Fax: Recorded</font></td>
        <td><input type="checkbox" name="PhoneFaxR" value="1" <% If bPhoneFaxR = 1 Then%>checked <%End If%> onClick="javascript:checkphonefax('1',form1.PhoneFaxNR.checked,form1.PhoneFaxR.checked)"></td>
		<td></td>
        </tr>
        <tr>
        <td align="right"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Phone or Fax: Non-Recorded</font></td>
        <td><input type="checkbox" name="PhoneFaxNR" value="1" <% If bPhoneFaxNR = 1 Then%>checked <%End If%> onClick="javascript:checkphonefax('0',form1.PhoneFaxNR.checked,form1.PhoneFaxR.checked)"></td>
        <td></td>
        </tr>
        <tr>
        <td colspan="3"><hr noshade color="#000000" size="1" width="90%">
        </td>
        </tr>
        <tr>
        <td align="right" valign="top"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Internet Cash:</font></td>
        <td valign="top"><input type="checkbox" name="InternetCash" value="1" <% If bInternetCash = 1 Then%>checked <%End If%>></td>
        <td>
			<table width="100%" border="0">
			<tr>
				<td width="30%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Server Path: </font></td>
				<td width="70%" align="left"><input type="text" name="InternetCashServerPath" style="<%= C_FORMDESIGN %>" size="30" value="<%= sInternetCashServerPath %>"></td>
			</tr>
			<tr>
				<td width="30%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Merchant ID: </font></td>
				<td width="70%" align="left"><input type="text" name="InternetCashMercID" style="<%= C_FORMDESIGN %>" size="30" value="<%= sInternetCashMercID %>"></td>
			</tr>
			<tr>
				<td width="30%"><font face="<%= C_FONTFACE4 %>" color="<%= C_FONTCOLOR4 %>" size="<%= C_FONTSIZE4 %>">Merchant Key: </font></td>
				<td width="70%" align="left"><input type="text" name="InternetCashMercKey" style="<%= C_FORMDESIGN %>" size="30" value="<%= sInternetCashMerchantKey %>"></td>
			</tr>
			</table>
        </td>        
        </tr>
        <tr>
        <td colspan="3"><hr noshade color="#000000" size="1" width="90%">
        </td>
        </tr>
        <tr>
        <td width="100%" align="center" valign="top" colspan="3"><input type="image" border="0" src="images/submit.gif" name="Submit" WIDTH="108" HEIGHT="21"></td>
        </tr>        
        </table><br>
        </form>
    </td>
    </tr>
	<tr>
			<td align="center" bgcolor="<%= C_BGCOLOR7 %>" background="<%= C_BKGRND7 %>"><font face="<%= C_FONTFACE7 %>" color="<%= C_FONTCOLOR7 %>" size="<%= C_FONTSIZE7 %>"><b><a href="menu.asp">Main Menu</a> | <a href="../../search.asp">Your Store</a></b></font></td>
    </tr>
</table>
</td>
</tr>
</table>
</body>

</html>



