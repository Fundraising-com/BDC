<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ControlerFindIncident.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerFindIncident" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ControlerIncidentHistory" Src="ControlerIncidentHistory.ascx" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<uc1:ControlerIncidentHistory id="ctrlControlerIncidentHistory" runat="server"></uc1:ControlerIncidentHistory>
		</TD>
	</TR>
	<TR>
		<TD align="center"><a href='javascript:void(0);' onClick='<%="javascript:Close();"%>'>Close</a></TD>
	</TR>
</TABLE>
