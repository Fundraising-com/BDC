<%@ Register TagPrefix="uc1" TagName="ControlerMagazine" Src="ControlerMagazine.ascx" %>
<%@ Page language="c#" Codebehind="Product.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CustomerService.Product" %>
<%@ Register TagPrefix="uc1" TagName="ControlerSearchMagazine" Src="ControlerSearchMagazine.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Magazine</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
		<!--#include file="fctjavascriptall.js"-->
	</HEAD>
	<BODY onload="return window_onunload()">
		<FORM id="Form2" method="post" runat="server">
			<P>
				<TABLE id="Table1" height="100%" cellPadding="3" width="100%" border="0">
					<TR>
						<TD vAlign="top" width="1">
							<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD vAlign="top">
										<DIV id="divSearch" style="DISPLAY: block">
											<TABLE id="Table3" height="100%" cellSpacing="0" cellPadding="0" bgColor="#cecece" border="0">
												<TR>
													<TD>
														<TABLE id="Table4" height="100%" cellSpacing="1" cellPadding="2">
															<TR>
																<TD vAlign="top" height="20"><IMG src="images/step1.gif">
																</TD>
															</TR>
															<TR bgColor="#ffffff">
																<TD vAlign="top">
																	<uc1:ControlerSearchMagazine id="ctrlControlerSearchMagazine" runat="server"></uc1:ControlerSearchMagazine></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
											</TABLE>
										</DIV>
										<DIV id="divSearch2" style="DISPLAY: none">
											<TABLE id="Table5" height="100%" cellSpacing="0" cellPadding="0" bgColor="#cecece" border="0">
												<TR>
													<TD><IMG height="1" src="images/spacer.gif" width="2">
													</TD>
												</TR>
											</TABLE>
										</DIV>
									</TD>
									<TD vAlign="top"></TD>
								</TR>
							</TABLE>
						</TD>
						<TD vAlign="top">
							<TABLE id="Table6" height="100%" cellSpacing="0" cellPadding="0" width="100%" bgColor="#cecece"
								border="0">
								<TR>
									<TD>
										<TABLE id="Table7" height="100%" cellSpacing="1" cellPadding="0" width="100%" border="0">
											<TR>
												<TD height="20"><IMG src="images/step2.gif">
												</TD>
											</TR>
											<TR bgColor="#ffffff">
												<TD vAlign="top">
													<uc1:ControlerMagazine id="ctrlControlerMagazine" runat="server"></uc1:ControlerMagazine>
												<TD></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</P>
		</FORM>
	</BODY>
</HTML>
