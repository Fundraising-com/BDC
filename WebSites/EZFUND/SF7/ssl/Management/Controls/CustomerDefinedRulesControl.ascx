<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CustomerDefinedRulesControl.ascx.vb" Inherits="StoreFront.StoreFront.CustomerDefinedRulesControl" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<input id="hdnID" type="hidden" name="hdnID" runat="server">
<table cellSpacing="0" cellPadding="0" width="100%">
	<TBODY>
		<TR>
			<TD class="Content" colSpan="3" height="10"></TD>
		</TR>
		<TR>
			<td class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
		</TR>
		<tr>
			<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
			<td class="Content" align="left" height="10">
				<asp:repeater id="rCombinations" Runat="server">
					<HeaderTemplate>
						<TABLE CellPadding="0" Width="100%" border="0">
							<tr>
								<td class="Contenttableheader" width="6%" style="text-align: center;">Valid</td>
								<td class="Contenttableheader" width="94%">&nbsp;Combination</td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<TR>
							<TD class="Content" colSpan="2">&nbsp;</TD>
						</TR>
						<tr>
							<td class="Content" width="6%" align="center"><asp:CheckBox id="chkValid" runat="server" TextAlign="Left"></asp:CheckBox></td>
							<td class="Content" width="94%">&nbsp;<input id="hdnProductIDs" type="hidden" name="hdnProductIDs" runat="server"><%#Container.DataItem%></td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
</table>
</FooterTemplate> </asp:repeater>
<asp:Label ID="lblMessage" Runat="server"></asp:Label>
</TD>
<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
</TR>
<TR>
	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
	<TD class="Content" height="10"></TD>
	<TD class="ContentTableHeader" width="1"><IMG src="images/clear.gif" width="1"></TD>
</TR>
<TR>
	<td class="ContentTable" colSpan="3" height="1"><IMG height="1" src="images/clear.gif"></td>
</TR>
<TR>
	<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
	<TD height="10"></TD>
	<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
</TR>
<TR>
	<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
	<TD class="Content" align="left">
		<asp:linkbutton id="btnSave" Runat="server">
			<img border="0" src="images/save.jpg">
		</asp:linkbutton>
	</TD>
	<TD class="Content" width="1"><IMG src="images/clear.gif" width="1"></TD>
</TR>
</TBODY></TABLE>
