<%@ Page language="c#" Inherits="QSP.OrderExpress.Web.MDRSchoolDetailInfo" Codebehind="MDRSchoolDetailInfo.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="MDRSchoolInfo" Src="~/UserControls/MDRSchoolInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MDR School Information</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottommargin=0 topmargin=0 leftmargin=0 rightmargin=0>
		<form id="Form1" method="post" runat="server">
			<TABLE id="TblStep1" height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
				border="0">
				<tr>
					<td vAlign="top" align="left" width="100%" height="100%">
						<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="0">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td><uc1:header id="Header1" runat="server"></uc1:header></td>
											<td></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td bgColor="#ffffff" colSpan="2"></td>
							</tr>
							<tr height="100%">
								<TD vAlign="top" align="left" width="100%" height="100%"><IMG height="20" src="images/spacer.gif">
									<br>
									<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
										<TBODY>
											<TR>
												<TD style="WIDTH: 20px" vAlign="top" width="20">
													<P><IMG height="20" src="images/spacer.gif"></P>
												</TD>
												<TD vAlign="top" align="left">
													<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" background="images/titlebg.gif" border="0">
																	<TR>
																		<TD align="left">
																			<asp:Image id="imgTitle" ImageUrl="~/images/title_mdr_school_detail.gif" runat="server"></asp:Image></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" background="images/generaltable_02.gif"
																	border="0">
																	<TR>
																		<TD><IMG height="20" src="images/generaltable_01.gif" width="15"></TD>
																		<TD><IMG height="20" src="images/generaltable_02.gif" width="482"></TD>
																		<TD align="right"><IMG height="20" src="images/generaltable_03.gif" width="17"></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD bgColor="#fdf6ee" colSpan="3">
																<table cellPadding="4" width="100%">
																	<tr>
																		<td><IMG src="images/directions.gif"><br>
																			<asp:label id="lblInstruction" runat="server" cssclass="directions">																			
																			Please enter the campaign information in the following form and then click on "go to step 2".
																			</asp:label>
																			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<tr>
																					<td><br>
																						<asp:label id="lblMDRSchoolTitle" runat="server" CssClass="StandardLabel" ForeColor="#993300"></asp:label>
																						<br>
																					</td>
																				</tr>
																				<!--<TR>
																					<TD><IMG src="images/setup/spacer.gif" width="10">&nbsp;<asp:label id="lblMessage" runat="server" CssClass="LabelError" ForeColor="Red"></asp:label></TD>
																				</TR>-->
																				<TR>
																					<TD>
																						<uc1:MDRSchoolInfo runat="server" ID="MDRSchoolInfo1"></uc1:MDRSchoolInfo>
																						<br>
																					</TD>
																				</TR>
																			</TABLE>
																		</td>
																	</tr>
																	<tr>
																		<td align="center">
																			<table border="0" cellpadding="0" cellspacing="0" width="400">
																				<tr>
																					<td align="center">
																						<asp:HyperLink id="hypLnkClose" runat="server" ImageUrl="~/images/btnOKBig.gif" NavigateUrl="javascript:window.close();">Cancel</asp:HyperLink>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</table>
															</TD>
														</TR>
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" background="images/generaltable_05.gif"
																	border="0">
																	<TR>
																		<TD><IMG height="13" src="images/generaltable_04.gif" width="15"></TD>
																		<TD><IMG height="13" src="images/generaltable_05.gif" width="483"></TD>
																		<TD align="right"><IMG height="13" src="images/generaltable_06.gif" width="15"></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<tr>
															<td></td>
														</tr>
													</TABLE>
												</TD>
											</TR>
										</TBODY>
									</TABLE>
								</TD>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td><uc1:footer id="Footer1" runat="server"></uc1:footer><INPUT id="hidChange" type="hidden" value="0" name="hidChange" runat="server"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
