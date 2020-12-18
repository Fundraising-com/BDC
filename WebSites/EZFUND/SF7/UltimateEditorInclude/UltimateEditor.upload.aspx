<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<script language="C#" runat="server">
void OK_Click(object sender, System.EventArgs e)
{
	string ue9r = Request.QueryString["ue9r"];
	string clientScriptStr = "";
	HttpPostedFile postedFile = fileSource.PostedFile;
	string contentType = postedFile.ContentType.Trim();

	if (postedFile.FileName == "")
	{
		clientScriptStr = "alert('You must choose a file.');";
	}
	else if (ue9r == "image" && !contentType.StartsWith("image/"))
	{
		clientScriptStr = "alert('You can choose only image files.');";
	}
	else if (ue9r == "flash" && contentType.IndexOf("flash") == -1)
	{
		clientScriptStr = "alert('You can choose only Flash files.');";
	}
	else if (ue9r == "wmp" && !contentType.StartsWith("video/") && !contentType.StartsWith("audio/"))
	{
		clientScriptStr = "alert('You can choose only audio and video files.');";
	}
	else if (postedFile != null)
	{
		try
		{
			int ueb86 = Convert.ToInt32(Request.QueryString["ueb86"]);
			int fSize = postedFile.ContentLength;
			if (fSize > ueb86 * 1024) {
				string fSizeMaxStr = "";
				if (ueb86 < 1024) {
					fSizeMaxStr = ueb86 + " KB";
				}
				else {
					fSizeMaxStr = Math.Round((ueb86 * 1.0 / 1024), 2) + " MB";
				}
				clientScriptStr = "alert('Maximum file size allowed to be uploaded is " + fSizeMaxStr + ".');";
			}
			else {
				string uew3 = Request.QueryString["uew3"];
				string dirName = System.Guid.NewGuid().ToString();
				string fullDir = Server.MapPath(uew3) + "\\" + dirName;
				string fName = System.IO.Path.GetFileName(postedFile.FileName);
				string fPath = fullDir + "\\" + fName;
				System.IO.Directory.CreateDirectory(fullDir);
				postedFile.SaveAs(fPath);

				string fileSrc = uew3 + "/" + dirName + "/" + fName;
				string fileURLBase = Request.Url.GetLeftPart(UriPartial.Authority);
				string imgTitle = txtAlt.Text;
				string imgAlign = ddlImageAlign.SelectedItem.Value;
				string imgBorder = txtBorder.Text;
				string imgWidth = txtImageWidth.Text;
				string imgHeight = txtImageHeight.Text;
				string imgHSpace = txtHSpace.Text;
				string imgVSpace = txtVSpace.Text;
				string mediaWidth = txtMediaWidth.Text;
				string mediaHeight = txtMediaHeight.Text;
				string mediaLoop = rblMediaLoop.SelectedItem.Value;
				string mediaAlign = ddlMediaAlign.SelectedItem.Value;
				string flashQuality = (ue9r == "flash") ? ddlFlashQuality.SelectedItem.Value : "";
				string flashBackgroundColor = (ue9r == "flash") ? txtFlashBackgroundColor.Text : "";
				string mediaID = txtMediaID.Text;
				clientScriptStr = "OK_Click('" + fileSrc + "', '" + fileURLBase + "', '" + dirName + "', '" + fName + "', " + fSize + ", '" + imgTitle + "', '" + imgAlign + "', '" + imgBorder + "', '" + imgWidth + "', '" + imgHeight + "', '" + imgHSpace + "', '" + imgVSpace + "', '" + mediaWidth + "', '" + mediaHeight + "', '" + mediaLoop + "', '" + mediaAlign + "', '" + flashQuality + "', '" + flashBackgroundColor + "', '" + mediaID + "');";
				CleanUp();
			}
		}
		catch
		{
			clientScriptStr = "alert('File cannot be uploaded.\\n\\nPlease make sure that ASPNET user (NT AUTHORITY\\\\NETWORK SERVICE for Win2003) has write permission on your UltimateEditorInclude\\\\Upload directory.');";
		}
	}
	if (clientScriptStr != "" && !this.IsClientScriptBlockRegistered("clientScript")) {
		this.RegisterClientScriptBlock("clientScript", SurroundScript(clientScriptStr));
	}
}

string SurroundScript(string scriptStr) {
	return ("<script language=JavaScript>" + scriptStr + "<" + "/" + "script>");
}

void CleanUp() {
	txtAlt.Text = "";
	ddlImageAlign.SelectedIndex = 0;
	txtBorder.Text = "";
	txtImageWidth.Text = "";
	txtImageHeight.Text = "";
	txtHSpace.Text = "";
	txtVSpace.Text = "";
	txtMediaWidth.Text = "200";
	txtMediaHeight.Text = "200";
	rblMediaLoop.SelectedIndex = 0;
	ddlMediaAlign.SelectedIndex = 0;
	ddlFlashQuality.SelectedIndex = 1;
	txtFlashBackgroundColor.Text = "#FFFFFF";
	txtMediaID.Text = "";
}
	</script>
	<head>
		<title>UltimateEditor Insert Image</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<style>
.UploadTable { background-color:#ECE9D8;font-family:Tahoma;font-size:11px;border-style:solid;border-color:#2457CA;border-width:2px; }
.UploadTable TD { font-family:Tahoma;font-size:11px }
.UploadTable FIELDSET { padding:7px }
.UploadTable INPUT, SELECT { font-family:Tahoma;font-size:11px }
		</style>
		<script language="javascript">
<!--
var ue8r = "<%=Request.QueryString["ue8r"]%>";
var ue9r = "<%=Request.QueryString["ue9r"]%>";

function uea8(elementId) {
	if (document.all) {
		return document.all[elementId];
	}
	else if (document.getElementById) {
		return document.getElementById(elementId);
	}
}

function CloseThisDialog() {
	parent.window.uea8(ue8r).style.visibility = "hidden";
}

function OK_Click(fileSrc, fileURLBase, dirName, fName, fSize, imgTitle, imgAlign, imgBorder, imgWidth, imgHeight, imgHSpace, imgVSpace, mediaWidth, mediaHeight, mediaLoop, mediaAlign, flashQuality, flashBackgroundColor, mediaID) {
	var curEditorObj = parent.window.curEditorObj;
	if (ue9r == "image") {
		curEditorObj.ue1s(fileSrc, fileURLBase, imgTitle, imgAlign, imgBorder, imgWidth, imgHeight, imgHSpace, imgVSpace);
	}
	else if (ue9r == "flash" || ue9r == "wmp") {
		curEditorObj.uec24(ue9r, fileSrc, fileURLBase, mediaWidth, mediaHeight, mediaLoop, mediaAlign, flashQuality, flashBackgroundColor, mediaID);
	}
	else {
		curEditorObj.ue2s(dirName, fName, fSize);
	}
	CloseThisDialog();
}

function Cancel_Click() {
	parent.window.curEditorObj.ue3s();
	CloseThisDialog();
}

function Page_Onload() {
	if (ue9r == "image") {
		uea8("divInsertImage").style.display = "block";
	}
	else if (ue9r == "flash" || ue9r == "wmp") {
		uea8("divInsertMedia").style.display = "block";
		if (ue9r == "wmp") {
			uea8("trFlashQuality").style.display = "none";
			uea8("trFlashBackgroundColor").style.display = "none";
		}
	}

	var iframeElem = parent.window.uea8(ue8r);
	var dialogElem = uea8("tblUpload");
	iframeElem.style.width = dialogElem.offsetWidth + "px";
	iframeElem.style.height = dialogElem.offsetHeight + "px";
	parent.window.ue4s(iframeElem);
	try {
		uea8("fileSource").focus();
	}
	catch (e) {
	}
}
//-->
		</script>
	</head>
	<body onload="Page_Onload()">
		<form id="Form1" method="post" enctype="multipart/form-data" runat="server">
			<table id="tblUpload" class="UploadTable" border="0" cellpadding="6" cellspacing="0">
				<tr style="background-color:#2457CA">
					<td style="padding-left:4px;font-family:Tahoma;font-size:11px;font-weight:bold;color:#FFFFFF"><%=Request.QueryString["ue0s"]%></td>
				</tr>
				<tr>
					<td>
						<table border="0" cellpadding="0" cellspacing="5">
							<tr>
								<td>File:</td>
								<td><input id="fileSource" type="file" name="fileSource" runat="server" size="59"></td>
							</tr>
						</table>
						<div id="divInsertImage" style="display:none">
							<table border="0" cellpadding="0" cellspacing="5">
								<tr>
									<td nowrap>Alternate text:</td>
									<td><asp:TextBox ID="txtAlt" Runat="Server" EnableViewState="False" style="width:328px" /></td>
								</tr>
								<tr>
									<td colspan="2">
										<table border="0" cellpadding="0" cellspacing="0">
											<tr valign="top">
												<td>
													<fieldset>
														<legend>Layout</legend>
														<table border="0" cellpadding="0" cellspacing="4">
															<tr>
																<td>Alignment:</td>
																<td><asp:DropDownList id="ddlImageAlign" runat="server">
																		<asp:ListItem Value="">Not set</asp:ListItem>
																		<asp:ListItem Value="left">Left</asp:ListItem>
																		<asp:ListItem Value="right">Right</asp:ListItem>
																		<asp:ListItem Value="texttop">Texttop</asp:ListItem>
																		<asp:ListItem Value="absmiddle">Absmiddle</asp:ListItem>
																		<asp:ListItem Value="baseline">Baseline</asp:ListItem>
																		<asp:ListItem Value="absbottom">Absbottom</asp:ListItem>
																		<asp:ListItem Value="bottom">Bottom</asp:ListItem>
																		<asp:ListItem Value="middle">Middle</asp:ListItem>
																		<asp:ListItem Value="top">Top</asp:ListItem>
																	</asp:DropDownList>
																</td>
																<td style="width:10px">&nbsp;</td>
																<td>Width:</td>
																<td><asp:TextBox ID="txtImageWidth" Runat="Server" style="width:45px" /></td>
															</tr>
															<tr>
																<td nowrap>Border size:</td>
																<td><asp:TextBox ID="txtBorder" Runat="Server" style="width:78px" /></td>
																<td></td>
																<td>Height:</td>
																<td><asp:TextBox ID="txtImageHeight" Runat="Server" style="width:45px" /></td>
															</tr>
														</table>
													</fieldset>
												</td>
												<td style="width:14px">&nbsp;</td>
												<td>
													<fieldset>
														<legend>Spacing</legend>
														<table border="0" cellpadding="0" cellspacing="4">
															<tr>
																<td>Horizontal:</td>
																<td><asp:TextBox ID="txtHSpace" Runat="Server" style="width:45px" /></td>
															</tr>
															<tr>
																<td>Vertical:</td>
																<td><asp:TextBox ID="txtVSpace" Runat="Server" style="width:45px" /></td>
															</tr>
														</table>
													</fieldset>
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
						<div id="divInsertMedia" style="display:none">
							<table border="0" cellpadding="0" cellspacing="4">
								<tr>
									<td>Width:</td>
									<td><asp:TextBox ID="txtMediaWidth" Runat="Server" Text="200" style="width:45px" /></td>
								</tr>
								<tr>
									<td>Height:</td>
									<td><asp:TextBox ID="txtMediaHeight" Runat="Server" Text="200" style="width:45px" /></td>
								</tr>
								<tr>
									<td>Loop:</td>
									<td>
										<asp:RadioButtonList id="rblMediaLoop" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
											<asp:ListItem Value="true" Selected="True">Yes&nbsp;</asp:ListItem>
											<asp:ListItem Value="false">No</asp:ListItem>
										</asp:RadioButtonList>
									</td>
								</tr>
								<tr>
									<td>Alignment:</td>
									<td><asp:DropDownList id="ddlMediaAlign" runat="server">
											<asp:ListItem Value="">Not set</asp:ListItem>
											<asp:ListItem Value="left">Left</asp:ListItem>
											<asp:ListItem Value="right">Right</asp:ListItem>
											<asp:ListItem Value="texttop">Texttop</asp:ListItem>
											<asp:ListItem Value="absmiddle">Absmiddle</asp:ListItem>
											<asp:ListItem Value="baseline">Baseline</asp:ListItem>
											<asp:ListItem Value="absbottom">Absbottom</asp:ListItem>
											<asp:ListItem Value="bottom">Bottom</asp:ListItem>
											<asp:ListItem Value="middle">Middle</asp:ListItem>
											<asp:ListItem Value="top">Top</asp:ListItem>
										</asp:DropDownList>
									</td>
								</tr>
								<tr id="trFlashQuality">
									<td>Quality:</td>
									<td><asp:DropDownList id="ddlFlashQuality" runat="server">
											<asp:ListItem Value="best">Best</asp:ListItem>
											<asp:ListItem Value="high" Selected="True">High</asp:ListItem>
											<asp:ListItem Value="medium">Medium</asp:ListItem>
											<asp:ListItem Value="autohigh">Autohigh</asp:ListItem>
											<asp:ListItem Value="autolow">Autolow</asp:ListItem>
											<asp:ListItem Value="low">Low</asp:ListItem>
										</asp:DropDownList>
									</td>
								</tr>
								<tr id="trFlashBackgroundColor">
									<td>Background color:</td>
									<td><asp:TextBox ID="txtFlashBackgroundColor" Runat="Server" Text="#FFFFFF" style="width:60px" /></td>
								</tr>
								<tr>
									<td>ID:</td>
									<td><asp:TextBox ID="txtMediaID" Runat="Server" /></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Button id="btnOK" runat="server" Text="OK" onclick="OK_Click" style="width:80px"></asp:Button>&nbsp;<input type="button" id="btnCancel" value="Cancel" onclick="Cancel_Click()" style="width:80px">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
