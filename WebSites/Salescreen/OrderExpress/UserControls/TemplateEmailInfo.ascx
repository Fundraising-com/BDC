<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.TemplateEmailInfo" Codebehind="TemplateEmailInfo.ascx.cs" %>

<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td align="left"> <!--Section Title --></td>
	</tr>
	<tr>
		<td>
			<table id="Table3" cellSpacing="0" cellPadding="3" width="600" border="0">
				<tr>
					<td class="StandardLabel" style="WIDTH: 125px" width="125">Template&nbsp;ID&nbsp;#&nbsp;:&nbsp;</td>
					<td width="90%"><asp:label id="lblTemplateID" runat="server" CssClass="DescLabel"></asp:label></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel" style="WIDTH: 125px">Template&nbsp;Name&nbsp;:&nbsp;</td>
					<td valign="top" ><asp:label id="lblTemplateName" runat="server" CssClass="DescLabel" Font-Bold="True"></asp:label></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel" style="WIDTH: 125px">Description&nbsp;:&nbsp;</td>
					<td valign="top" ><asp:label id="lblDescription" runat="server" CssClass="DescLabel"></asp:label></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel" style="WIDTH: 125px">From&nbsp;:&nbsp;</td>
					<td valign="top" ><asp:label id="lblFrom" runat="server" CssClass="DescLabel"></asp:label></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel" style="WIDTH: 125px">Subject&nbsp;:&nbsp;</td>
					<td valign="top"  bgcolor="white" style="BORDER-RIGHT: thin inset; BORDER-TOP: thin inset; BORDER-LEFT: thin inset; BORDER-BOTTOM: thin inset"><asp:label id="lblSubject" runat="server" CssClass="DescLabel"></asp:label></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel" style="WIDTH: 125px">HTML&nbsp;Body&nbsp;:&nbsp;</td>
					<td valign="top"  bgcolor="white" style="BORDER-RIGHT: thin inset; BORDER-TOP: thin inset; BORDER-LEFT: thin inset; BORDER-BOTTOM: thin inset"><div style="OVERFLOW:auto;HEIGHT:192px"><asp:label id="lblBodyHTML" runat="server" CssClass="DescLabel"></asp:label></div></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel" style="WIDTH: 125px">Stored&nbsp;Proc&nbsp;Name&nbsp;:&nbsp;</td>
					<td valign="top" ><asp:label id="lblStoredProcName" runat="server" CssClass="DescLabel"></asp:label></td>
				</tr>
				<tr>
					<td valign="top" class="StandardLabel" style="WIDTH: 125px">Stored&nbsp;Proc&nbsp;Parameter&nbsp;Name&nbsp;:&nbsp;</td>
					<td valign="top" ><asp:label id="lblStoredProcParameterName" runat="server" CssClass="DescLabel"></asp:label></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
