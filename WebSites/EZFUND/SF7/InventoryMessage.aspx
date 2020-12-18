<%@ Page Language="vb" AutoEventWireup="false" Codebehind="InventoryMessage.aspx.vb" Inherits="StoreFront.StoreFront.InventoryMessage"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Inventory</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 7.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
		<% Me.PageHeader %>
		<script language="javascript">
<!--
         
       
//-->
		</script>
	</HEAD>
	<body class="GeneralPage">
		
		<form id="InventoryInfo" Runat="server">
			<asp:Panel ID="boConfirm" Runat="server">
<TABLE class="GeneralTable" cellSpacing="0" width="100%">
					<TR>
						<TD class="TopBanner" width="100%"><!-- Top Banner Start --> Stock Alert <!-- Top Banner End --></TD>
					</TR>
					<TR>
						<TD class="Content" id="ContentCell" vAlign="top"><!-- Content Start -->
							<TABLE class="ContentTableHeader" id="tblInventory" cellSpacing="0" cols="2" cellPadding="0" width="100%" Runat="server">
								<TR class="ContentTableHeader" vAlign="top">
									<TD class="ContentTableHeader" width="90%" colSpan="2">&nbsp;&nbsp;Currently Out of 
										Stock!
									</TD>
								</TR>
								<TR class="Content" vAlign="top">
									<TD class="Content" width="90%" colSpan="2">&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD class="content" colSpan="2">
										<P id="ErrorAlignment" align="center" runat="server">
											<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
									</TD>
								</TR>
								<TR class="Content" vAlign="top">
									<TD class="Content" width="100%" colSpan="2">
										<TABLE class="Content" cellSpacing="2" cellPadding="2" width="100%">
											<TR>
												<TD><%#DataBinder.Eval(me,"Message")%></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR class="Content" vAlign="top">
									<TD class="Content" width="90%" colSpan="2">&nbsp;&nbsp;
									</TD>
								</TR>
								<TR class="Footer" id="CanBackOrder" vAlign="top" Runat="server">
									<TD class="Footer" align="right" height="11" Runat="server">
										<asp:LinkButton id="cmdNo" Runat="server">
											<asp:Image BorderWidth="0" ID="imgNo" Runat="server" AlternateText="No Thanks"></asp:Image>
										</asp:LinkButton>&nbsp;&nbsp;
									</TD>
									<TD class="Footer" align="left" height="11" Runat="server">&nbsp;&nbsp;
										<asp:LinkButton id="cmdBackOrder" Runat="server">
											<asp:Image BorderWidth="0" ID="imgBackOrder" Runat="server" AlternateText="Back Order"></asp:Image>
										</asp:LinkButton></TD>
								</TR>
								<TR class="Footer" id="TrClose" vAlign="top" Runat="server">
									<TD class="Content" id="Td2" align="middle" colSpan="2" Runat="server">&nbsp;&nbsp;
										<asp:LinkButton id="cmdClose" Runat="server">
											<asp:Image BorderWidth="0" ID="imgClose" Runat="server" AlternateText="Ok"></asp:Image>
										</asp:LinkButton></TD>
								</TR>
							</TABLE> <!-- Content End --></TD>
					</TR>
				</TABLE></TD></TR></TABLE>
			</asp:Panel>
			<asp:Panel ID="Confirmation" Runat="server" Visible="False">
				<TABLE id="PageTable" cellSpacing="0" width="100%" runat="server">
					<TR>
						<TD id="PageCell">
							<TABLE class="GeneralTable" id="PageSubTable" cellSpacing="0" width="100%" runat="server">
								<TR>
									<TD class="TopBanner" width="100%"><!-- Top Banner Start --> Thank You! 
										<!-- Top Banner End --></TD>
								</TR>
								<TR>
									<TD class="Content" vAlign="top" width="100%"><!-- Content Start -->
										<P id="P1" runat="server">
											<asp:Label id="lblErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label></P>
										<TABLE cellSpacing="3" cellPadding="5" width="100%" border="0">
											<TR>
												<TD class="Content">
													<asp:label id="lblDisplay" runat="server" text="Confirmation Message"></asp:label></TD>
											</TR>
										</TABLE> <!-- Content End --></TD>
								</TR>
								<TR>
									<TD class="Content" align="middle"><!-- Footer Start -->
										<asp:LinkButton id="btnCheckout" Runat="server">
											<asp:Image BorderWidth="0" ID="imgCheckout" Runat="server" AlternateText="Checkout"></asp:Image>
										</asp:LinkButton>
										<asp:LinkButton id="btnClose" Runat="server">
											<asp:Image BorderWidth="0" ID="Image1" Runat="server" AlternateText="Close"></asp:Image>
										</asp:LinkButton><!-- Footer End --></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
		</form>
	</body>
</HTML>
