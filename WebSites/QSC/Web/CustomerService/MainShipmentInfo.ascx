<%@ Register TagPrefix="uc1" TagName="ControlerShipementDetail" Src="ControlerShipementDetail.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerContactInformation" Src="ControlerContactInformation.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerShipmentInformation" Src="ControlerShipmentInformation.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MainShipmentInfo.ascx.cs" Inherits="QSPFulfillment.CustomerService.MainShipmentInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<br><TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD valign=top align="center">
			<TABLE class=CSTable id=Table4 cellSpacing=0 cellPadding=2 width="90%">
				<TR>
				<TD class=CSTableHeader colSpan=2>Shipment Information</TD></TR>
				<TR>
				<TD>
				<uc1:ControlerShipmentInformation id=ctrlControlerShipmentInformation runat="server"></uc1:ControlerShipmentInformation></TD></TR></TABLE>
		</TD>
		<TD valign=top  align="center">
      <TABLE class=CSTable id=Table3 cellSpacing=0 cellPadding=2 width="90%">
        <TR>
          <TD class=CSTableHeader colSpan=2>Contact&nbsp;Information</TD></TR>
        <TR>
          <TD class=CSTableHeader colSpan=2></TD></TR>
        <TR>
          <TD>
<uc1:ControlerContactInformation id=ctrlControlerContactInformation runat="server"></uc1:ControlerContactInformation></TD></TR></TABLE></TD>
	</TR>
	<TR>
		<TD colspan=2 align="center"><br>
      <TABLE class=CSTable id=Table2 cellSpacing=0 cellPadding=2 width="95%">
        <TR>
          <TD class=CSTableHeader colSpan=2>Shipment Detail</TD></TR>
        <TR>
          <TD >
<uc1:ControlerShipementDetail id=ctrlControlerShipementDetail runat="server"></uc1:ControlerShipementDetail></TD></TR></TABLE>
</TD>
		
	</TR>
</TABLE>
