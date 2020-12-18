<%@ Register TagPrefix="uc1" TagName="CInventoryControl" Src="Controls/CInventoryControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Inventory.aspx.vb" Inherits="StoreFront.StoreFront.Inventory"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Inventory Status</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<LINK href="<%=ThemesPath()%>Styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="general.js"></script>
	</HEAD>
	<body class="GeneralPage">
		<form id="Form2" method="post" runat="server">
			<table id="PageTable" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td id="PageCell">
						<table class="GeneralTable" id="PageSubTable" cellSpacing="0" runat="server" width="100%">
							<tr>
								<td class="TopBanner" width="100%">
									<!-- Top Banner Start --> Stock Status 
									<!-- Top Banner End -->
								</td>
							</tr>
							<tr>
								<td class="Content" vAlign="top" id="ContentCell">
									<!-- Content Start -->
									<TABLE class="ContentTableHeader" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR vAlign="top" class="ContentTableHeader">
											<TD width="90%" class="ContentTableHeader">
												&nbsp;Current Stock Levels
											</TD>
										</TR>
										<tr>
											<td class="content">
												<P id="ErrorAlignment" runat="server" align="center">
													<asp:Label id="ErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:Label>
												</P>
											</td>
										</tr>
										<TR vAlign="top" class="Content">
											<TD width="100%" class="Content">
												<br>
												<uc1:CInventoryControl id="CInventoryControl1" runat="server"></uc1:CInventoryControl>
												<br>
											</TD>
										</TR>
									</TABLE>
									<!-- Content End -->
								</td>
							</tr>
							<tr>
								<td class="Footer" align="middle">
									<!-- Footer Start -->
									<asp:LinkButton ID="btnClose" Runat="server">
										<asp:Image BorderWidth="0" ID="imgClose" Runat="server" AlternateText="Close"></asp:Image>
									</asp:LinkButton>
									<!-- Footer End --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
