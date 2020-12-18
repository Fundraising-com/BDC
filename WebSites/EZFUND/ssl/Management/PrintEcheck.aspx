<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PrintEcheck.aspx.vb" Inherits="StoreFront.StoreFront.PrintEcheck"%>
<HTML>
	<HEAD>
		<title>
			
			- Print E-Check</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.0.1

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table border="1" cellpadding="3" cellspacing="0">
				<tr>
					<td align="middle">
						<p id="ErrorAlignment" runat="server">
							<font color="#ff0000">
								<asp:label id="ErrorMessage" runat="server" Visible="False" CssClass="ErrorMessages"></asp:label>
							</font>
						</p>
					</td>
				</tr>
				<tr>
					<td align="middle">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td align="middle" colspan="2">
									Payment Authorized by Account Holder. Indemnification Agreement Provided by 
									Depositor.
								</td>
							</tr>
							<tr>
								<td align="left" valign="top" width="50%">
									<b>
										<asp:Label ID="name" Runat="server"></asp:Label></b><br>
									<asp:Label ID="Address" Runat="server"></asp:Label><br>
									<asp:Label ID="CityStateZip" Runat="server"></asp:Label><br>
									<asp:Label ID="Country" Runat="server"></asp:Label>
								</td>
								<td align="right" valign="top">
									<b>Check Number:</b>&nbsp;&nbsp;<asp:Label ID="CheckNumber" Runat="server"></asp:Label><br>
									<br>
									<b>Date:</b>&nbsp;&nbsp;<asp:Label ID="OrderDate" Runat="server"></asp:Label>
									<hr>
								</td>
							</tr>
							<TR>
								<TD vAlign="top" align="left"><B>Pay To:____________________________</B></TD>
								<TD vAlign="top" align="right"></TD>
							</TR>
							<tr>
								<td align="left" valign="top">
									<b>Pay the amount of:</b>
								</td>
								<td align="right" valign="top">
									<b>
										<asp:Label ID="Total" Runat="server"></asp:Label>
									</b>
								</td>
							</tr>
							<tr>
								<td colSpan="2" height="1"><hr>
								</td>
							</tr>
							<tr>
								<td align="left" valign="top">
									<b>
										<asp:Label ID="BankName" Runat="server"></asp:Label>
									</b>
								</td>
								<td align="right" valign="top">
									Electronically Signed By:&nbsp;&nbsp;<b><asp:Label ID="SignedBy" Runat="server"></asp:Label></b><br>
									<hr>
								</td>
							</tr>
							<tr>
								<td align="left" colspan="2"><b><asp:Label Font-Size="16" Font-Name="MICR 013 BT" ID="RoutingNumber" Runat="server"></asp:Label></b></td>
							</tr>
							<tr>
								<td align="middle" colspan="2">&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
