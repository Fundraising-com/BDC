<%@ Register TagPrefix="uc1" TagName="PostalAddress" Src="../Common/PostalAddress.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerAddressHistory" Src="ControlerAddressHistory.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerAddress" Src="ControlerAddress.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerRefund" Src="ControlerRefund.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MainAddressInfo.ascx.cs" Inherits="QSPFulfillment.CustomerService.MainAddressInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>


<br>
<TABLE id=Table4 cellSpacing=0 cellPadding=0 width="100%" border=0>
  <TR>
    <TD vAlign=top align=center><br>
      <TABLE class=CSTable id=Table3 cellSpacing=0 cellPadding=2 width="90%" 
      >
        <TR class=CSTableHeader>
          <TD>Recipient&nbsp;Address </TD></TR>
        <TR>
          <TD><uc1:controleraddress id=ctrlControlerAddressRecipient runat="server"></uc1:controleraddress></TD></TR></TABLE></TD>
    <TD vAlign=top align=center><br>
      <table class=CSTable cellSpacing=0 cellPadding=2 width="90%" 
      >
        <TR class=CSTableHeader>
          <TD>Bill To&nbsp;Address </TD></TR>
        <TR>
          <TD><uc1:controleraddress id=ctrlControlerAddressCustomer runat="server"></uc1:controleraddress></TD></TR></TABLE></TD></TR>
  <TR>
    <TD vAlign=top align=center><BR>
      <TABLE class=CSTable id=Table2 cellSpacing=0 cellPadding=2 width="90%" 
      >
        <TR class=CSTableHeader>
          <TD>Recipient&nbsp;Address History </TD></TR>
        <TR>
          <TD><uc1:controleraddresshistory id=ctrlControlerAddressHistoryRecipient runat="server"></uc1:controleraddresshistory></TD></TR></TABLE><br 
      ></TD>
    <TD vAlign=top align=center><br>
      <table class=CSTable cellSpacing=0 cellPadding=2 width="90%" 
      >
        <TR class=CSTableHeader>
          <TD>Bill To&nbsp;Address History </TD></TR>
        <TR>
          <TD><uc1:controleraddresshistory id=ctrlControlerAddressHistoryBillTo runat="server"></uc1:controleraddresshistory></TD></TR></TABLE></TD></TR>
  <TR>
    <TD vAlign=top align=center colSpan=2><br 
      >
      <TABLE class=CSTable id=Table1 cellSpacing=0 cellPadding=2 width="45%" 
      >
        <TR class=CSTableHeader>
          <TD>Refund&nbsp;Information </TD></TR>
        <TR>
          <TD>
            <table cellSpacing=0 cellPadding=0 width="100%">
              <tr>
                <td colSpan=2><uc1:controlerrefund id=ctrlControlerRefund runat="server" showcheckboxes="false"></uc1:controlerrefund><br 
                  ></TD></TR>
              </TABLE></TD></TR></TABLE></TD></TR></TABLE>
