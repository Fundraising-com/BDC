<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AccountLookUp.ascx.cs" Inherits="QSPFulfillment.CommonWeb.UC.AccountLookUp" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:textbox id=tbAccount Runat="server" Columns="10" MaxLength="10" />
<a href="javascript:;" onclick="AcctLookWin=window.open('/QSPFulfillment/AcctMgt/AccountList.aspx?caller=<%=this.ID%>_tbAccount','AcctLookWin','width=1000,height=600,left=270,top=180');AcctLookWin.focus()" Runat="server" ID="ahrefPopUp"></a>
<asp:RequiredFieldValidator id="rqAccount" runat="server" ControlToValidate="tbAccount" display="Dynamic" Text="*" ErrorMessage="Please select a group #" />
<asp:RegularExpressionValidator id="regAccount" runat="server" ControlToValidate="tbAccount" Display="Dynamic" Text="*" ErrorMessage="Please select a group #, without formatting." ValidationExpression="\d*" />
