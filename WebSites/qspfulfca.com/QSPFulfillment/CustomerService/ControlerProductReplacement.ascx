<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerProductReplacement.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerProductReplacement" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<asp:button id="btnNextStudentTop" runat="server" text="Next Student" onclick="btnNextStudent_Click"></asp:button>
<br>
<br>
<asp:placeholder id="plhOrderHeaders" runat="server"></asp:placeholder>
<br>
<br>
<table>
	<tr>
		<td style="WIDTH: 182px"><asp:label id="Label9" runat="server" cssclass="CSPlainText">Comment</asp:label></td>
		<td><asp:textbox id="tbxComment" runat="server" maxlength="300" height="78px" width="364px" textmode="MultiLine"></asp:textbox></td>
	</tr>
</table>
<br>
<br>
<asp:button id="btnNextStudentBottom" runat="server" text="Next Student" onclick="btnNextStudent_Click"></asp:button>
&nbsp;
<asp:button id="btnConfirm" runat="server" text="Confirm" onclick="btnConfirm_Click"></asp:button>
