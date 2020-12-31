<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerIncidentActionComments.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerIncidentActionComments" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD>
			<asp:TextBox id="tbxComments" runat="server" TextMode="MultiLine" Width="627px" Height="64px"
										style="FONT-SIZE:8pt; COLOR:#000000; FONT-FAMILY:verdana,arial"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD align="center"><br>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width=100%  border="0">
				<TR>
					<TD align=center>
						<asp:Button id="btnSave" runat="server" Text="Save" onclick="btnSave_Click"></asp:Button></TD>
					<TD align=center>
						<asp:Button id="btnCancel" runat="server" Text="Cancel"></asp:Button></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
