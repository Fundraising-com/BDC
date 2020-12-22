<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.ProgramInfo" Codebehind="ProgramInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td class="SectionPageTitleInfo" colSpan="2"><asp:label id="Label3" runat="server">
			Program Information
			</asp:label></td>
	</tr>	
	<tr>
		<td><br>
			<table id="Table3" cellSpacing="0" cellPadding="3" width="600" border="0">
				<tr>
					<td class="StandardLabel" width="150">Program&nbsp;ID&nbsp;#&nbsp;:&nbsp;</td>
					<td width="90%"><asp:label id="lblProgramID" CssClass="DescInfoLabel" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel">Program&nbsp;Name&nbsp;:&nbsp;</td>
					<td><asp:label id="lblProgramName" CssClass="DescLabel" runat="server" Font-Bold="True"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel">Program&nbsp;Type&nbsp;:&nbsp;</td>
					<td><asp:label id="lblProgramTypeName" CssClass="DescInfoLabel" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel">Description&nbsp;:&nbsp;</td>
					<td valign="top" height="70px"><asp:label id="lblDescription" CssClass="DescInfoLabel" runat="server"></asp:label></td>
				</tr>								
				<tr>
					<td><br>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
