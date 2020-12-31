<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DefaultLetterTemplateGenerationControl.ascx.cs" Inherits="QSPFulfillment.CustomerService.DefaultLetterTemplateGenerationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table>
	<TR>
		<td>&nbsp;</td>
		<TD width="250" colSpan="2"><asp:radiobutton id="rbRemitBatch" onclick="SetEnabledLetterBatchType()" Text="Remit Batch ID" GroupName="LetterBatchType"
				Checked="True" CssClass="csPlainText" runat="server"></asp:radiobutton><br>
		</TD>
		<TD><br>
			<cc1:textboxinteger id="tbxRunID" runat="server"></cc1:textboxinteger><asp:rangevalidator id="RangeValidator5" runat="server" MinimumValue="1" MaximumValue="2147483647" ControlToValidate="tbxRunID"
				Type="Integer" ErrorMessage="Remit Batch ID must be between 1 and 2147483647.">*</asp:rangevalidator></TD>
	</TR>
	<TR>
		<td></td>
		<TD><asp:radiobutton id="rbDateRange" onclick="SetEnabledLetterBatchType()" Text="Date Range" GroupName="LetterBatchType"
				CssClass="csPlainText" runat="server"></asp:radiobutton></TD>
		<td align="right"><asp:label id="Label1" CssClass="csPlainText" runat="server">From:</asp:label></td>
		<TD><uc1:dateentry id="dteDateFrom" runat="server" EmptyValue="1995-01-01"></uc1:dateentry></TD>
	</TR>
	<TR>
		<td colSpan="2"></td>
		<TD align="right"><asp:label id="Label7" CssClass="csPlainText" runat="server">To:</asp:label></TD>
		<TD><uc1:dateentry id="dteDateTo" runat="server" EmptyValue="1995-01-01"></uc1:dateentry></TD>
	</TR>
</table>
