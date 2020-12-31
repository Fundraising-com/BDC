<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment" %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="ControlerRefund" Src="../ControlerRefund.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CancelCustomerRefund.ascx.cs" Inherits="QSPFulfillment.CustomerService.action.CancelCustomerRefund" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<TABLE class=CSTable id=Table3 cellSpacing=0 cellPadding=2 width="45%" 
      >
        <TR class=CSTableHeader>
          <TD>Select Refund to Cancel:</TD></TR>
        <TR>
          <TD>
            <table cellSpacing=0 cellPadding=0 width="100%">
              <tr>
                <td colSpan=2><uc1:controlerrefund id=ctrlControlerRefund runat="server" ShowCheckBoxes="true"></uc1:controlerrefund><br 
                  ></TD></TR>
              </TABLE></TD></TR>
</table>
</TD></TR></TBODY></TABLE>
