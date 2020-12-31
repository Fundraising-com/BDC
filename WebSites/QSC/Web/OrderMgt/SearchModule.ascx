<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SearchModule.ascx.cs" Inherits="QSPFulfillment.OrderMgt.SearchModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0"  border="0" height="40">
	<TR>
		<TD><asp:Label Runat="server" ID="lblFrom" CssClass="CSDirections">&nbsp;&nbsp;&nbsp;From:&nbsp;</asp:Label></TD>
		<TD>
			<uc1:DateEntry id="ctrlDateEntryFrom" runat="server" Required="false"></uc1:DateEntry></TD>

		<TD><asp:Label Runat="server" ID="lblTo" CssClass="CSDirections">&nbsp;&nbsp;&nbsp;To:&nbsp;</asp:Label></TD>
		<TD>
			<uc1:DateEntry id="ctrlDateEntryTo" runat="server" Required="false"></uc1:DateEntry></td>
		<td>
			<asp:LinkButton id="lbtnSearch" runat="server" onclick="lbtnSearch_Click">&nbsp;&nbsp;&nbsp;&nbsp;Search</asp:LinkButton></TD>

	</TR>
</TABLE>

