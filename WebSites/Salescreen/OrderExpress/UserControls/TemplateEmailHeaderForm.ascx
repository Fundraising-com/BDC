<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.TemplateEmailHeaderForm" Codebehind="TemplateEmailHeaderForm.ascx.cs" %>

<script language="javascript" src="../Script/tiny_mce/tiny_mce.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
		tinyMCE.init(
						{
							mode : "specific_textareas",
							textarea_trigger : "textarea_trigger",
							theme : "advanced",
							theme_advanced_toolbar_location : "top",
							theme_advanced_buttons1 :"code,preview,separator,undo,redo,separator",
							theme_advanced_buttons2 :"fontselect,fontsizeselect,forecolor,backcolor,separator,tablecontrols",
							theme_advanced_buttons3 :"bold,italic,underline,strikethrough,sub,sup,separator,justifyleft,justifycenter,justifyright,justifyfull,separator,bullist,numlist,outdent,indent,separator,link,unlink,separator,hr,charmap,separator,cleanup",
							theme_advanced_toolbar_align : "left",
							verify_html : false,
							force_br_newlines : true,
							plugins : "preview,table",
							extended_valid_elements : "table[bordercolor|bgcolor]",
							table_color_fields : true,
							plugin_preview_width : "400",
							plugin_preview_height : "300"

							//entity_encoding : "raw"
						}
					);
</script>
<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td><br>
		</td>
	</tr>
	<tr>
		<td>
			<table id="Table2" cellSpacing="0" cellPadding="3" width="600" border="0">
				<tr>
					<td class="StandardLabel" style="WIDTH: 125px" width="125">Template&nbsp;ID&nbsp;#&nbsp;:&nbsp;</td>
					<td width="90%"><asp:label id="lblTemplateID" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td class="StandardLabel" style="WIDTH: 125px">Template&nbsp;Name&nbsp;:&nbsp;</td>
					<td><asp:textbox id="txtTemplateName" runat="server" Font-Bold="True" MaxLength="200" Width="400px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtTemplateName"
							ErrorMessage="The Template Name is required">*</asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td class="StandardLabel" style="WIDTH: 125px">Description&nbsp;:&nbsp;</td>
					<td><asp:textbox id="txtDescription" runat="server" Width="400px"></asp:textbox></td>
				</tr>
				<tr>
					<td class="StandardLabel" style="WIDTH: 125px">From&nbsp;:&nbsp;</td>
					<td><asp:textbox id="txtFrom" runat="server" Width="400px"></asp:textbox></td>
				</tr>
				<tr>
					<td class="StandardLabel" style="WIDTH: 125px">Subject&nbsp;:&nbsp;</td>
					<td><asp:textbox id="txtSubject" runat="server" Width="400px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject" ErrorMessage="The Subject is required">*</asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td class="StandardLabel" style="WIDTH: 125px" vAlign="top">HTML Body&nbsp;:&nbsp;</td>
					<td><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="HtmlEditor" ErrorMessage="The HTML Body is required"
							Enabled="False">*</asp:requiredfieldvalidator><textarea id="HtmlEditor" style="WIDTH: 100%; HEIGHT: 300px" name="HtmlEditor" rows="15" cols="20"
							runat="server" textarea_trigger="true"></textarea><br>
						<asp:label id="Label1" Runat="server" CssClass="StandardLabel">Available Tag(s) :</asp:label><br>
						<asp:label id="lblAvailableTag" Runat="server"></asp:label><br>
					</td>
				</tr>
			</table>
			<table style="BORDER-RIGHT: lightgrey thin solid; BORDER-TOP: lightgrey thin solid; BORDER-LEFT: lightgrey thin solid; BORDER-BOTTOM: lightgrey thin solid"
				width="100%">
				<tr>
					<td class="StandardLabel" style="WIDTH: 241px">Stored Procedure Name&nbsp;:&nbsp;</td>
					<td><asp:textbox id="txtStoredProcName" runat="server" Width="200px"></asp:textbox></td>
				</tr>
				<tr>
					<td class="StandardLabel" style="WIDTH: 241px">Stored Procedure Parameter 
						Name&nbsp;:&nbsp;</td>
					<td><asp:textbox id="txtStoredProcParameterName" runat="server" Width="200px"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WIDTH: 241px"></td>
					<td align="right"><asp:button id="btnValidateSP" Runat="server" CausesValidation="False" Text="Refresh Tags" onclick="btnValidateSP_Click"></asp:button></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
