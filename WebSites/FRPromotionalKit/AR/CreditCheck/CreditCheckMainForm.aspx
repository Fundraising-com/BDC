<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page language="c#" Codebehind="CreditCheckMainForm.aspx.cs" AutoEventWireup="True" Inherits="efundraising.EFundraisingCRMWeb.AR.CreditCheck.CreditCheckMainForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CreditCheckMainForm</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="WIDTH: 822px; HEIGHT: 792px" cellSpacing="0" cellPadding="0"
				width="822" border="0">
				<TR>
					<TD style="HEIGHT: 44px" vAlign="top" bgColor="#edede1">
						<TABLE id="Table1" style="WIDTH: 824px; HEIGHT: 26px" cellSpacing="0" cellPadding="0" width="824"
							border="0">
							<TR>
								<TD style="HEIGHT: 14px"></TD>
								<TD style="HEIGHT: 14px" vAlign="top" bgColor="#006699"><ie:tabstrip id="CreditCheckTabStrip" runat="server" TabDefaultStyle="background-color:#006699;font-family:verdana;font-weight:bold;font-size:8pt;color:#ffffff;width:180;height:18;text-align:center;"
										TabHoverStyle="background-color:#0678B1;font-family:verdana;font-weight:bold;font-size:9pt;color:#ffffff;width:180;height:21;text-align:center;"
										TabSelectedStyle="background-color:#EDEDE1;font-family:verdana;font-weight:bold;font-size:8pt;color:#000000;width:180;height:21;text-align:center;"
										Width="393px" Height="32px" AutoPostBack="True" onselectedindexchange="CreditCheckTabStrip_SelectedIndexChange">
										<ie:Tab Text="Credit Requests"></ie:Tab>
										<ie:TabSeparator></ie:TabSeparator>
										<ie:Tab Text="Credit Reports"></ie:Tab>
										<ie:TabSeparator></ie:TabSeparator>
									</ie:tabstrip></TD>
								<TD style="HEIGHT: 14px"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left"><IFRAME id="frameCreditCheck" style="WIDTH: 804px; HEIGHT: 100%" frameBorder="0" scrolling="no"
							runat="server"></IFRAME>
					</TD>
				</TR>
			</TABLE>
			
		</form>
	</body>
</HTML>
