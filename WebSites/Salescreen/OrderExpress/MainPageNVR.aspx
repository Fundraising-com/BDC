<%@ Register TagPrefix="uc1" TagName="FAQ_Displayer" Src="~/UserControls/FAQ_Displayer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SessionInfo" Src="~/UserControls/SessionInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MenuBar" Src="~/UserControls/MenuBar.ascx" %>
<%@ Register TagPrefix="uc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Page language="c#" Inherits="QSP.OrderExpress.Web.MainPageNVR" ValidateRequest="false" Codebehind="MainPageNVR.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QSP Order Express Main Page</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script>
			var ToSetFocus;
		</script>
	</HEAD>
	<body class="StandardBody" bottomMargin="0" leftMargin="0" topMargin="0" onload="InitMenu()"
		rightMargin="0" onscroll="Scroll()" onbeforeunload="ShowWatitingFrame()">
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
								<td colSpan="2"><uc1:menubar id="QSPFormMenuBar" runat="server"></uc1:menubar></td>
							</tr>
							<tr height="100%">
								<TD vAlign="top" align="left" width="100%" height="100%"><IMG height="20" src="images/spacer.gif">
									<br>
									<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
										<TBODY>
											<TR>
												<TD style="WIDTH: 20px" vAlign="top" width="20">
													<P><IMG height="20" src="images/spacer.gif">
													</P>
												</TD>
												<TD vAlign="top" align="left">
													<TABLE cellSpacing="0" cellPadding="0" width="90%" border="0">
														<TR>
															<TD>
																<table cellSpacing="0" cellPadding="0" width="800" border="0">
																	<tr>
																		<td>
																			<table cellSpacing="0" cellPadding="0" border="0">
																				<tr>
																					<td valign=top>
																						<asp:Image ID="imgIcon" Runat="server" Height="40px"></asp:Image>&nbsp;&nbsp;
																					</td>
																					<td valign=bottom>
																						<asp:label id="lblSectionTitle" runat="server" CssClass="SectionTitleLabel">Login:</asp:label>&nbsp;&nbsp;
																					</td>
																					<td valign=bottom>
																						<asp:label id="lblPageTitle" runat="server" CssClass="PageTitleLabel"></asp:label>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																	<tr>
																		<td><br>
																			<asp:label id="lblDirectionTitle" runat="server" CssClass="DirectionTitleLabel">
																				Directions:
																			</asp:label>
																		</td>
																	</tr>
																	<tr>
																		<td>
																			<asp:label id="lblInstruction" runat="server" CssClass="DirectionLabel"></asp:label>
																		</td>
																	</tr>
																</table>
															</TD>
														</TR>
														<TR>
															<TD>
																<table cellPadding="4" width="100%">
																	<tr>
																		<td>
																			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TR>
																					<TD><asp:validationsummary id="ValSum" runat="server" HeaderText="<br>Error Summary, please correct the following errors"
																							CssClass="LabelError"></asp:validationsummary><asp:label id="lblMessage" runat="server" CssClass="LabelError" ForeColor="Red"></asp:label></TD>
																				</TR>
																				<TR>
																					<TD>
																						<br>
																						<asp:placeholder id="plHoldBodyPage" runat="server"></asp:placeholder><br>
																					</TD>
																				</TR>
																			</TABLE>
																		</td>
																	</tr>
																</table>
															</TD>
														</TR>
														
													</TABLE>
												</TD>
												<td>&nbsp;&nbsp;&nbsp;
												</td>
												<td vAlign="top" width="184"><uc1:sessioninfo id="QSPForm_SessionInfo" runat="server" Visible="False"></uc1:sessioninfo><br>
													<uc1:faq_displayer id="QSPForm_FAQ_Displayer" runat="server" Visible="False"></uc1:faq_displayer></td>
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
		<iframe id="WaitingFrame" style="Z-INDEX: 255;LEFT: 25%; position: absolute;top:100px;visibility:hidden;border:navy thin solid " height=156 frameborder=no src=loading.aspx scrolling=no></iframe>
		<script>
            if(ToSetFocus != null)
	            ToSetFocus.focus();
		</script>
		<script>
		function ShowWatitingFrame()
		{
		    document.getElementById('WaitingFrame').style.visibility = "visible";
		}
		function Scroll() 
		{
              var el = document.getElementById('WaitingFrame');
              var topEdge = document.body.scrollTop + 100
              el.style.pixelTop = topEdge;
              el.Top = topEdge;
        }  
        </script>
	</body>
</HTML>
<script>
if(ToSetFocus != null)
	ToSetFocus.focus();
</script>
