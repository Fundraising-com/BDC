<%@ Page language="c#" Codebehind="Home.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.Home" %>
<%@ Register  TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Home</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="Includes/QSPFulfillment.css" type="text/css">
	    <style type="text/css">
            .style1
            {
                color: red;
            }
        </style>
	</head>
	<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" border="0">
		<form id="Form1" method="post" runat="server">
			<!-- #include file="Includes/Menu.inc" -->
			<p align="center">
				<br>
				<br>
				<br>
				<table border="0" width="730" cellpadding="0" cellspacing="0">
					<tr>
						<td valign="left">
							<img src="Images/QSPLogo.jpg" style="height: 57px; width: 213px">
						</td>
						<td valign="top" align="center">
						</td>
						<td valign="top" align="right" bgcolor="#ffffff">
							<asppanel id="pan" visible="false" runat="server">
                                <table style="FONT-SIZE: 8pt" bgcolor="#000000" cellspacing="1" width="261">
								    <tr bgcolor="#cccccc" height="17">
									    <td bgcolor="#ffffff" style="FONT-WEIGHT: bold; COLOR: #b53800">Aug. 2004</td>
									    <td>Important Dates and Deadlines</td>
								    </tr>
								    <tr bgcolor="#ffffff">
									    <td align="left" class="content" colspan="2">
										    <table id="tblDates" runat="server" border="0" cellspacing="1" cellpadding="2" style="FONT-SIZE: 8pt">
										    </table>
									    </td>
								    </tr>
							    </table>
							</asppanel>
						</td>
					</tr>
					<tr>
						<td colspan="3" align="center">
                            
							<br>
							<br>
							<span class="CSPageTitle">Welcome to QSP's<asp:label id="lblFMWelcome" runat="server">&nbsp;FM</asp:label>&nbsp;Management 
								System </span>
							<br>
							<br>
							<br>
						</td>
					</tr>
					<tr>
						<td colspan="3" height="1" bgcolor="#666666">
							<img src="images/spacer.gif" height="1" width="1"><br>
						</td>
					</tr>
					<tr>
						<td colspan="3" bgcolor="#ffffff">
							<!-- "New" SECTION -->
							<br>
							<br>
							<table>
								<tr>
									<td valign="top" style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; COLOR: red; FONT-VARIANT: small-caps">News:</td>
									<td id="tdNew" style="FONT-SIZE: 8pt" runat="server" valign="top" class="bodytext"></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</p>
		</form>
	</body>
</html>
