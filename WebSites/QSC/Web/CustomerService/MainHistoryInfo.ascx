<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MainHistoryInfo.ascx.cs" Inherits="QSPFulfillment.CustomerService.MainHistoryInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ControlerIncidentHistory" Src="ControlerIncidentHistory.ascx" %>
<center><table width="95%"><tr><td align="center">
<P><br>
	<uc1:ControlerIncidentHistory id="ctrlControlerIncidentHistory" runat="server"></uc1:ControlerIncidentHistory></P>
	
	</td></tr></table>
</center>