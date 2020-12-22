<%@ Page language="c#" AutoEventWireup="false" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.HtmlControls" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="System.Configuration" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Server Information</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="C#" runat="server">
			
		
			private void Page_Load(object sender, System.EventArgs e)
			{
				// Put user code to initialize the page here
				lblServerName.Text = Server.MachineName;
				lblSessionID.Text =  Session.SessionID;
				//Request.ServerVariables[""];
				lblLabelIPAddress.Text = "";
			}
			
			#region Web Form Designer generated code
			override protected void OnInit(EventArgs e)
			{
				//
				// CODEGEN: This call is required by the ASP.NET Web Form Designer.
				//
				try
				{
					InitializeComponent();
					base.OnInit(e);
				}
				catch(Exception ex)
				{
					lblMessage.Text = ex.Message;
				}
			}
			
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{    
				this.Load += new System.EventHandler(this.Page_Load);

			}
			#endregion
		</script>
  </HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblErrorPage" height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
				border="0">
				<tr>
					<td vAlign="top" align="center" width="100%" height="100%">
						<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="0">
							<TR>
								<TD width="100%"><uc1:header id="Header1" runat="server"></uc1:header></TD>
							</TR>
							<TR>
								<TD bgColor="white"></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="center" width="100%" height="100%">
									<table class="Login" id="tblWelcome" cellSpacing="0" cellPadding="0" width="550" align="middle"
										border="0" runat="server">
										<tr>
											<td style="FONT-SIZE: 20px" rowSpan="7">&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td colSpan="2"><br>
												<asp:label id="lblTitle" runat="server" CssClass="Login" Font-Size="16"> Server Information.
												</asp:label><br><br>
											</td>
										</tr>
										<tr>
											<td colSpan="2">
												<asp:Label id="lblInstruction" runat="server" Visible="False" Font-Size="12pt" Font-Bold="True"></asp:Label>
											</td>
										</tr>
										<tr>
											<td valign=top>
												<table border=0 cellpadding=0 cellspacing=0>
													<tr>
														<td>
														<asp:Label id="lblLabelServerName" runat="server"  Font-Size="12pt" Font-Bold="True">Server Name:</asp:Label>
														</td>
														<td valign=top>
															&nbsp;&nbsp;<asp:Label id="lblServerName" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
														</td>
													</tr>
												</table>
												<br>
											</td>	
										</tr>
										<tr>
											<td valign=top>
												<table border=0 cellpadding=0 cellspacing=0>
													<tr>
														<td>
														<asp:Label id="lblLabelIPAddress" runat="server"  Font-Size="12pt" Font-Bold="True">IP Address:</asp:Label>
														</td>
														<td valign=top>
															&nbsp;&nbsp;<asp:Label id="lblIPAddress" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
														</td>
													</tr>
												</table>
												<br>
											</td>	
										</tr>
										<tr>
											<td valign=top>
												<table border=0 cellpadding=0 cellspacing=0>
													<tr>
														<td>
														<asp:Label id="lblLabelSessionID" runat="server"  Font-Size="12pt" Font-Bold="True">SesssionID :</asp:Label>
														</td>
														<td valign=top>
															&nbsp;&nbsp;<asp:Label id="lblSessionID" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
														</td>
													</tr>
												</table>
												<br>
											</td>
										</tr>
										<tr>
											<TD align="center" colspan="2"><br>
												<asp:Label id="lblMessage" runat="server" CssClass="eRewardsError"></asp:Label>
											</TD>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="center" width="100%" height="100%"></TD>
							</TR>
							<TR>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD><uc1:Footer id="Footer1" runat="server"></uc1:Footer></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
<Script language=C#>
		
		
</Script>
