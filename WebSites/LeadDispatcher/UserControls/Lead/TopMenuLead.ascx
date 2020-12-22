<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TopMenuLead.ascx.cs" Inherits="CRMWeb.UserControls.TopMenuLead" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<HTML>
	<body>
		<TABLE id="Table1" style="WIDTH: 824px; HEIGHT: 26px" cellSpacing="0" cellPadding="0" width="824"
			border="0">
			<TR>
				<TD style="HEIGHT: 25px"></TD>
				<TD style="HEIGHT: 25px" vAlign="top" bgColor="#006699">
					<ie:tabstrip id="TabStripLead" TabDefaultStyle="background-color:#006699;font-family:verdana;font-weight:bold;font-size:8pt;color:#ffffff;width:120;height:21;text-align:center;"
						TabHoverStyle="background-color:#0678B1;font-family:verdana;font-weight:bold;font-size:9pt;color:#ffffff;width:120;height:21;text-align:center;"
						TabSelectedStyle="background-color:#EDEDE1;font-family:verdana;font-weight:bold;font-size:8pt;color:#000000;width:120;height:21;text-align:center;"
						Width="401px" Height="26px" runat="server" AutoPostBack="True">
						<ie:Tab Text="Lead Dispatcher"></ie:Tab>
						<ie:TabSeparator></ie:TabSeparator>
						<ie:Tab Text="Lead Info" TabIndex="1"></ie:Tab>
						<ie:TabSeparator></ie:TabSeparator>
						<ie:Tab Text="Admin" TabIndex="3"></ie:Tab>
					</ie:tabstrip></TD>
				<TD style="HEIGHT: 25px"></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
