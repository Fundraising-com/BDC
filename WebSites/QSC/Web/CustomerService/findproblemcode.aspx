<%@ Register TagPrefix="uc1" TagName="ControlerProblemCode" Src="ControlerProblemCode.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerSearchProblemCode" Src="ControlerSearchProblemCode.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>findproblemcode</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link REL="stylesheet" HREF="../Includes/QSPFulfillment.css" TYPE="text/css">
		<!--#include file="ShowHide.js"-->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
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
													<TABLE height="100%" cellSpacing="1" cellPadding="2">
														<TR>
															<TD vAlign="top" height="20">Search
															</TD>
														</TR>
														<TR bgColor="#ffffff">
															<TD vAlign="top">
																<uc1:ControlerSearchProblemCode id="ctrlControlerSearchProblemCode" runat="server"></uc1:ControlerSearchProblemCode></TD>
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
												Information Area
											</TD>
										</TR>
										<TR bgcolor="#ffffff">
											<TD valign="top">
												<uc1:ControlerProblemCode id="ctrlControlerProblemCode" runat="server"></uc1:ControlerProblemCode>
											<TD>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		</FORM>
	</body>
</HTML>
