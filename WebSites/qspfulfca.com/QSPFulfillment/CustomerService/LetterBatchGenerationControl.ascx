<%@ Register TagPrefix="cc2" Namespace="QSPFulfillment.CustomerService" Assembly="QSPFulfillment"  %>
<%@ Register TagPrefix="cc1" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc1" TagName="DateEntry" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="QSPFulfillment.CommonWeb" Assembly="QSPFulfillment" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LetterBatchGenerationControl.ascx.cs" Inherits="QSPFulfillment.CustomerService.LetterBatchGenerationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div style="TEXT-ALIGN: center">
	<table bgcolor="#000000" cellpadding="1" cellspacing="0" border="0" width="500">
		<tr>
			<td>
				<TABLE id="Table1ss" cellSpacing="0" cellPadding="2" border="0" width="100%" bgcolor="#ffffff">
					<TR>
						<TD class="CSTableHeader" colSpan="2">Generate Letter Batch</TD>
					</TR>
					<tr>
						<td colspan="2" style="padding: 10px;">
							<cc2:lettertemplateselectiondropdownlist id="ddlLetterTemplateSelection" runat="server" AutoPostBack="True" InitialText="Please select..."
								InitialValue="0" Required="False"></cc2:lettertemplateselectiondropdownlist>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:PlaceHolder ID="plhLetterTemplateGenerationControl" Runat="server"></asp:PlaceHolder>
						</td>
					</tr>
					<TR>
						<td align="center" style="padding: 10px;">
							<asp:Button id="btnPreview" runat="server" Text="Preview"></asp:Button>
						</td>
						<TD align="center" style="padding: 10px;">
							<asp:Button id="btnGenerate" runat="server" Text="Generate"></asp:Button>
						</TD>
					</TR>
				</TABLE>
			</td>
		</tr>
	</table>
	<br>
</div>
