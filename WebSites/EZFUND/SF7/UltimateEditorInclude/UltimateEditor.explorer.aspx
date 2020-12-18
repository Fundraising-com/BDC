<%@ Page Language="C#" AutoEventWireup="true" Inherits="Karamasoft.WebControls.UltimateEditor.Explorer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
	<title>UltimateEditor Explorer</title>
	<style>
	body {background-color:#ECE9D8;font-family:Tahoma;font-size:11px}
	input {font-family:Tahoma;font-size:11px}
	.cursordefault {cursor:default}
	.explorerheader {background-color:#ECE9D8;border-bottom:1px solid #ACA899;padding:3px;padding-left:8px}
	.file {background-color:#FFFFFF;color:#00008B;padding:2px;cursor:pointer}
	.fileselect {background-color:#00008B;color:#FFFFFF;padding:2px;cursor:pointer}
	.btndisabled {-moz-opacity:0.25;filter:alpha(opacity=25)}
	.section {margin-top:10px}
	</style>
	<script src="UltimateEditor.explorer.js" type="text/javascript"></script>
</head>
<body onload="Body_OnLoad(',<%=ImageFileExtensions%>,','<%=IncludePath%>','<%=FileSourceID%>','<%=FileSizeID%>','<%=AllowFileOverwriteID%>','<%=AllowFileUploadID%>','<%=AllowNewFolderID%>','<%=hfNewFolderName.ClientID%>','<%=imgPreview.ClientID%>','frmPreview','<%=ibCreateFolder.ClientID%>','<%=fileToUpload.ClientID%>','<%=btnUpload.ClientID%>','<%=hfAllowFileOverwrite.ClientID%>','<%=hfAllowFileUpload.ClientID%>','<%=hfAllowNewFolder.ClientID%>','<%=FileType%>','file','fileselect','btndisabled')">
	<form id="form1" runat="server">
		<asp:Label ID="lblCurrentDir" runat="server" Font-Bold="True" CssClass="cursordefault"></asp:Label>
		<table border="1" cellpadding="0" cellspacing="0" bordercolor="#CAC7B9" style="width:100%;margin-top:5px;background-color:#FFFFFF;border-collapse:collapse">
			<tr>
				<td class="explorerheader"><asp:ImageButton ID="ibUpDir" runat="server" ImageUrl="Images/Explorer/up.gif" BorderWidth="0" AlternateText="Up One Level" ToolTip="Up One Level" onmouseover="this.src='Images/Explorer/upover.gif'" onmouseout="this.src='Images/Explorer/up.gif'" OnClick="ibUpDir_Click" /><asp:ImageButton ID="ibCreateFolder" runat="server" ImageUrl="Images/Explorer/create.gif" BorderWidth="0" AlternateText="Create New Folder" ToolTip="Create New Folder" onmouseover="this.src='Images/Explorer/createover.gif'" onmouseout="this.src='Images/Explorer/create.gif'" OnClick="ibCreateFolder_Click" OnClientClick="return CreateFolder_Click();" /></td>
				<td class="explorerheader cursordefault">Preview</td>
			</tr>
			<tr class="cursordefault">
				<td style="width:40%" valign="top">
				     <asp:Panel ID="pnlFolderContent" runat="server" Width="100%" style="overflow:auto"></asp:Panel>
				</td>
				<td align="center" style="height:300px">
					<asp:Image ID="imgPreview" runat="server" style="display:none" />
					<iframe id="frmPreview" style="display:none;width:250px;height:250px" onload="TemplatePreview_OnLoad()"></iframe>
				</td>
			</tr>
		</table>
		<fieldset class="section" style="padding:2px">
			<legend>Upload a file to the selected folder?</legend>
			<table border="0" cellpadding="0" cellspacing="0" align="center">
				<tr>
					<td>File to Upload:&nbsp;</td>
					<td><asp:FileUpload ID="fileToUpload" runat="server" Width="340px" size="50" />&nbsp;</td>
					<td><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /></td>
				</tr>
			</table>
		</fieldset>
		<div align="center" class="section">
			<asp:Button ID="btnOK" runat="server" Text="Select File" Width="100px" OnClientClick="OK_Click();return false;" />
			<asp:Button ID="btnCancel" runat="server" Text="Close Window" Width="100px" OnClientClick="Cancel_Click();return false;" />
		</div>
		<span style="display:none"><asp:Button ID="btnChangeDir" runat="server" Text="Button" OnClick="btnChangeDir_Click" /></span>
		<asp:HiddenField ID="hfCurrentDir" runat="server" />
		<asp:HiddenField ID="hfNewFolderName" runat="server" />
		<asp:HiddenField ID="hfAllowFileOverwrite" runat="server" />
		<asp:HiddenField ID="hfAllowFileUpload" runat="server" />
		<asp:HiddenField ID="hfAllowNewFolder" runat="server" />
	</form>
</body>
</html>
