<%@ Register TagPrefix="cc1" Namespace="skmMenu" Assembly="skmMenu"  %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="main" Src="main.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="True" enableSessionState="true" Inherits="QSPFulfillment.CustomerService._default" %>
<%@ Register TagPrefix="uc1" TagName="ActionReason" Src="ActionReason.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerListAction" Src="ControlerListAction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerAction" Src="ControlerAction.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerCurrentInfo" Src="ControlerCurrentInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="searchmodule" Src="searchmodule.ascx" %>

<HTML>
  <HEAD>
		<TITLE>Customer Service</TITLE>
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
  </HEAD>
	<BODY topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginheight="0" marginwidth="0"
		onload="return window_onunload()">
		<script language="javascript">
			var communicationstep1 = false;
			var communicationstep3 = false;
			var showstep1= false;
			var showstep3= false;
			var newsession=false;
			
			function RefreshAction(actionid)
			{
				
				var hidDataBind = document.getElementById('hidDataBind');
				hidDataBind.value = actionid;
				document.Form1.submit();
				
			}
			function AsActionAlreadyOpen()
			{
				var hidDataBind = document.getElementById('hidDataBind');
			
				if(hidDataBind.value == "0")
					return false;
				else
					return true;
			}
			
		</script>
		<!--#include file="fctjavascriptall.js"-->
		<form id="Form1" method="post" runat="server">
			<!-- #include file="../Includes/Menu.inc" -->
			<TABLE height="100%" cellPadding="3" width="100%" border="0">
				<TR>
					<TD vAlign="top" width="1">
						<TABLE height="100%" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD vAlign="top">
									<DIV id="divSearch" style="DISPLAY: block">
										<TABLE height="100%" cellSpacing="0" cellPadding="0" bgColor="#cecece" border="0">
											<TR>
												<TD>
													<TABLE height="100%" cellSpacing="1">
														<TR>
															<TD vAlign="top" height="20"><img src="images/step1.gif"><br>
															</TD>
														</TR>
														<TR bgColor="#ffffff">
															<TD vAlign="top"><uc1:searchmodule id="ctrlSearchModule" runat="server"></uc1:searchmodule></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</DIV>
									<DIV id="divSearch2" style="DISPLAY: none">
										<TABLE height="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#cecece">
											<TR>
												<TD><IMG src="images/spacer.gif" width="2" height="1">
												</TD>
											</TR>
										</TABLE>
									</DIV>
								</TD>
								<TD valign="top"><A href="javascript:showhide('divSearch', '1')"><BR>
										<BR>
										<IMG src="images/showhide.gif" border="0"></A>
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD valign="top">
						<TABLE cellpadding="0" cellspacing="0" border="0" bgcolor="#cecece" width="100%" height="100%">
							<TR>
								<TD>
									<TABLE width="100%" height="100%" cellspacing="1" cellpadding="0" border="0">
										<TR>
											<TD height="20">
												<img src="images/step2.gif">
											</TD>
										</TR>
										<TR bgcolor="#ffffff">
											<TD valign="top">
												<uc1:main id="ctrlMain" runat="server"></uc1:main>
											<TD>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD width="1" valign="top">
						<TABLE cellpadding="0" cellspacing="0" border="0" height="100%">
							<TR>
								<TD valign="top"><A href="javascript:showhide('divAction', '3')"><BR>
										<BR>
										<IMG src="images/showhideright.gif" border="0"></A>
								</TD>
								<TD valign="top">
									<DIV id="divAction" style="DISPLAY: none">
										<TABLE border="0" cellpadding="0" cellspacing="0" bgcolor="#cecece" height="100%">
											<TR>
												<TD>
													<TABLE cellpadding="0" cellspacing="1" height="100%">
														<TR>
															<TD valign="top" height="20">
																<img src="images/step3.gif">
															</TD>
														</TR>
														<TR bgColor="#ffffff">
															<TD valign="top">
																<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD height="13">
																			<img src="images/problemcode.gif"><br>
																			<uc1:ActionReason id="ctrlActionReason" runat="server"></uc1:ActionReason></TD>
																	</TR>
																	<TR>
																		<TD><br>
																		</TD>
																	</TR>
																	<TR>
																		<TD><img src="images/actions.gif"><uc1:controleraction id="ctrlControlerAction" runat="server"></uc1:controleraction>
																			<br>
																		</TD>
																	</TR>
																	<TR>
																		<TD>
																			<font class="Font8BoldV">&nbsp;Actions already taken :</font><uc1:ControlerListAction id="ctrlControlerListAction" runat="server"></uc1:ControlerListAction></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</DIV>
									<DIV id="divAction2" style="DISPLAY: block">
										<TABLE height="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="#cecece">
											<TR>
												<TD><IMG src="images/spacer.gif" width="2" height="1">
												</TD>
											</TR>
										</TABLE>
									</DIV>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<input type=hidden id="hidDataBind" runat="server" value=0>
		</form>
		<!--#include file="search_tabs.js"-->
		<!--#include file="ShowHide.js"-->
		<!--#include file="errorwindow.js"-->
	</BODY>
</HTML>
