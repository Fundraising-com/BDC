<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Admin_Menu.ascx.cs" Inherits="CRMWeb.UserControls.Admin_Menu" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<P>
	<TABLE id="Table1" style="WIDTH: 768px; HEIGHT: 25px" cellSpacing="0" cellPadding="0" width="768"
		border="0">
		<TR>
			<TD style="WIDTH: 485px; HEIGHT: 25px" bgColor="#006699">
				<ie:tabstrip id="TabStripAdmin" TabDefaultStyle="background-color:#006699;font-family:verdana;font-weight:bold;font-size:8pt;color:#ffffff;width:79;height:21;text-align:center;"
					TabHoverStyle="background-color:#0678B1;font-family:verdana;font-weight:bold;font-size:9pt;color:#ffffff;width:79;height:21;text-align:center;"
					TabSelectedStyle="background-color:#EDEDE1;font-family:verdana;font-weight:bold;font-size:8pt;color:#000000;width:79;height:21;text-align:center;"
					Width="184px" Height="25px" runat="server" AutoPostBack="True">
					<ie:Tab Text="Promotions"></ie:Tab>
					<ie:TabSeparator></ie:TabSeparator>
					<ie:Tab Text="Harmony"></ie:Tab>
				</ie:tabstrip></TD>
		</TR>
	</TABLE>
</P>
