<%@ Register TagPrefix="uc1" TagName="CatalogMaintenanceStepInfosControl" Src="Control/CatalogMaintenanceStepInfosControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CatalogMaintenanceStepsContainerControl" Src="Control/CatalogMaintenanceStepsContainerControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CatalogMaintenanceStepsMenuControl" Src="Control/CatalogMaintenanceStepsMenuControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page language="c#" Codebehind="CatalogMaintenance.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.MarketingMgt.CatalogMaintenance" %>
<html>
	<head>
		<title>Catalog Maintenance</title>
		<link rel="stylesheet" href="../Includes/QSPFulfillment.css" type="text/css">
	</head>
	<body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0"
		onload="return window_onunload()">
		<!--#include file="../CustomerService/fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<table height="95%" cellpadding="3" width="100%" border="0">
				<tr>
					<td valign="top" width="1">
						<table height="100%" cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td valign="top">
									<div id="divSteps" style="DISPLAY: block">
										<table height="100%" cellspacing="0" cellpadding="0" bgcolor="#cecece" border="0">
											<tr>
												<td>
													<table height="100%" cellspacing="1">
														<!--
														<TR>
															<TD vAlign="top" height="20"><img src="images/step1.gif"><br>
															</TD>
														</TR>
														-->
														<tr bgcolor="#ffffff">
															<td valign="middle" height="100%">
																<uc1:catalogmaintenancestepsmenucontrol id="ctrlCatalogMaintenanceStepsMenuControl" runat="server"></uc1:catalogmaintenancestepsmenucontrol>
															</td>
														</tr>
														<tr bgcolor="#ffffff">
															<td>
																<uc1:catalogmaintenancestepinfoscontrol id="ctrlCatalogMaintenanceStepInfosControl" runat="server"></uc1:catalogmaintenancestepinfoscontrol>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</div>
									<div id="divSearch2" style="DISPLAY: none">
										<table height="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#cecece">
											<tr>
												<td><img src="images/spacer.gif" width="2" height="1">
												</td>
											</tr>
										</table>
									</div>
								</td>
							</tr>
						</table>
					</td>
					<td valign="top">
						<table cellpadding="0" cellspacing="0" border="0" bgcolor="#cecece" width="100%" height="100%">
							<tr>
								<td>
									<table width="100%" height="100%" cellspacing="1" cellpadding="0" border="0">
										<!--
										<TR>
											<TD height="20">
												<img src="images/step2.gif">
											</TD>
										</TR>
										-->
										<tr bgcolor="#ffffff">
											<td valign="top">
												<div style="PADDING-LEFT: 15px; WIDTH: 98%; PADDING-TOP: 25px">
													<uc1:catalogmaintenancestepscontainercontrol id="ctrlCatalogMaintenanceStepsContainerControl" runat="server"></uc1:catalogmaintenancestepscontainercontrol>
												</div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="hidDataBind" runat="server" value="0">
			<asp:validationsummary id="ValidationSummary1" runat="server" showmessagebox="True" showsummary="False"></asp:validationsummary>
		</form>
		<!--#include file="../CustomerService/errorwindow.js"-->
	</body>
</html>
