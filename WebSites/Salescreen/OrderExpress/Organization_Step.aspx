<%@ Register TagPrefix="uc1" TagName="OrgStep_Detail" Src="~/UserControls/OrgStep_Detail.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrgStep_MDRSchool" Src="~/UserControls/OrgStep_MDRSchool.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrgStep_Confirmation" Src="~/UserControls/OrgStep_Confirmation.ascx" %>
<%@ Page language="c#" Inherits="QSP.OrderExpress.Web.Organization_Step" Codebehind="Organization_Step.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="OrgStep_Continue" Src="~/UserControls/OrgStep_Continue.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrgStep_PostalAddress" Src="~/UserControls/OrgStep_PostalAddress.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrgStep_PhoneNumber" Src="~/UserControls/OrgStep_PhoneNumber.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OrgStep_EmailAddress" Src="~/UserControls/OrgStep_EmailAddress.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Organization Detail Form</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onscroll="Scroll()" onbeforeunload="ShowWatitingFrame()">
		<form id="OrganizationDetail" method="post" runat="server">
			<TABLE id="TblPage" height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
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
																		<TD align="left"><asp:image id="imgTitle" runat="server" ImageUrl="~/images/title_organization_detail.gif"></asp:image></TD>
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
																				<TR>
																					<TD><IMG src="images/setup/spacer.gif" width="10">
																						<asp:validationsummary id="ValSum" runat="server" HeaderText="Correct the following error to proceed."
																							CssClass="LabelError"></asp:validationsummary><asp:label id="lblMessage" runat="server" CssClass="LabelError" ForeColor="Red"></asp:label></TD>
																				</TR>
																				<tr id="trCampInfoTitle" runat="server">
																					<td align="left"> <!--Section Body --><br>
																						<table id="tblCampInfoTitle" cellSpacing="0" cellPadding="0" border="0">
																							<tr>
																								<td><asp:label id="Label1" runat="server" CssClass="StandardLabel">
																										Organization from MDR School :
																									</asp:label></td>
																								<td><asp:label id="lblMDRSchoolPID" runat="server" CssClass="StandardLabel" ForeColor="#993300">
																										00000
																									</asp:label></td>
																								<td>&nbsp;-&nbsp;
																								</td>
																								<td><asp:label id="lblMDRSchoolName" runat="server" CssClass="StandardLabel" ForeColor="#993300">
																										MDR School Name
																									</asp:label></td>
																							</tr>
																						</table>
																						<br>
																					</td>
																				</tr>
																				<TR>
																					<TD><uc1:orgstep_mdrschool id="MDRSchool_Step" runat="server"></uc1:orgstep_mdrschool></TD>
																				</TR>
																				<TR>
																					<TD><uc1:orgstep_detail id="OrgDetail_Step" runat="server"></uc1:orgstep_detail></TD>
																				</TR>
																				<tr>
																					<td><uc1:orgstep_postaladdress id="PostalAddress_Step" runat="server"></uc1:orgstep_postaladdress></td>
																				</tr>
																				<tr>
																					<td><uc1:orgstep_phonenumber id="PhoneNumber_Step" runat="server"></uc1:orgstep_phonenumber></td>
																				</tr>
																				<tr>
																					<td><uc1:orgstep_emailaddress id="EmailAddress_Step" runat="server"></uc1:orgstep_emailaddress></td>
																				</tr>
																				<tr>
																					<td><uc1:orgstep_confirmation id="Confirmation_Step" runat="server"></uc1:orgstep_confirmation></td>
																				</tr>
																				<tr>
																					<td><uc1:orgstep_continue id="Continue_Step" runat="server"></uc1:orgstep_continue></td>
																				</tr>
																			</TABLE>
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
