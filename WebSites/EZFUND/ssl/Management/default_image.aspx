<%@ Register TagPrefix="uc1" TagName="UploadControl" Src="Controls/UploadControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default_image.aspx.vb" Inherits="StoreFront.StoreFront.default_image"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>
			<% writeTitle() %>
			- Insert/Update Image</title>
		<%
'@BEGINVERSIONINFO

'@APPVERSION: 6.0.0.0

'@STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.

'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'@ENDVERSIONINFO
%>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<style>BODY { FONT-SIZE: xx-small; FONT-FAMILY: Verdana }
	TABLE { FONT-SIZE: xx-small; FONT-FAMILY: Tahoma }
	INPUT { FONT: 8pt verdana,arial,sans-serif }
	SELECT { FONT: 8pt verdana,arial,sans-serif; TOP: 2px; HEIGHT: 22px }
	.bar { BORDER-TOP: #ACB8B8 1px solid; BACKGROUND: #336699; WIDTH: 100%; BORDER-BOTTOM: #000000 1px solid; HEIGHT: 20px }
		</style>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/nav4-0" name="vs_targetSchema">
	</HEAD>
	<body bottomMargin="5" vLink="mediumslateblue" aLink="mediumslateblue" link="blue" bgColor="gainsboro" leftMargin="5" topMargin="5" onload="checkImage()" rightMargin="5">
		<table cellSpacing="0" cellPadding="0" border="0">
			<tr>
				<td vAlign="top">
					<!-- Content -->
					<table>
						<tr>
							<td class="content" align="middle">
								<P id="ErrorAlignment" align="center" runat="server"><font color="#ff0000"><asp:label id="lblErrorMessage" runat="server" CssClass="ErrorMessages" Visible="False"></asp:label></font></P>
							</td>
						</tr>
					</table>
					<table cellSpacing="3" cellPadding="3" align="center" border="0">
						<tr>
							<td align="middle" bgColor="white">
								<div id="divImg" style="OVERFLOW: auto; WIDTH: 150px; HEIGHT: 170px"></div>
							</td>
							<td vAlign="top">
								<form id=form2 name=form2 
            action='<%=Request.ServerVariables("SCRIPT_NAME")%>' method=post 
            >
									<table height="30" cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td><b>Select folder&nbsp;:&nbsp;</b></td>
											<td><select id="catid" onchange="form2.submit()" name="catid">
													<%#DataBinder.Eval(me, "objMainFolder")%>
												</select>
											</td>
										</tr>
									</table>
								</form>
								<table cellSpacing="0" cellPadding="0" width="260" border="0">
									<tr>
										<td>
											<div class="bar" style="PADDING-LEFT: 5px"><font face="tahoma" color="white" size="2"><b>File 
														Name</b></font>
											</div>
										</td>
									</tr>
								</table>
								<div style="BORDER-RIGHT: lightsteelblue 1px solid; OVERFLOW: auto; BORDER-LEFT: #316ac5 1px solid; WIDTH: 260px; BORDER-BOTTOM: lightsteelblue 1px solid; HEIGHT: 120px">
									<%#DataBinder.Eval(me, "HTMLString")%>
								</div>
								<form id="Form1" enctype="multipart/form-data" method="post" runat="server">
									<uc1:UploadControl id="UploadControl1" runat="server"></uc1:UploadControl>
								</form>
							</td>
						</tr>
						<tr>
							<td colSpan="2">
								<hr>
								<table cellSpacing="1" cellPadding="0" width="340" border="0">
									<tr>
										<td>Image source :
										</td>
										<td colSpan="3"><INPUT id="inpImgURL" type="text" size="39" name="inpImgURL"> 
											<!--<font color=red>(you can type your own image path here)</font>--></td>
									</tr>
									<tr>
										<td>Alternate text :
										</td>
										<td colSpan="3"><INPUT id="inpImgAlt" type="text" size="39" name="inpImgAlt"></td>
									</tr>
									<tr>
										<td>Alignment :
										</td>
										<td><select id="inpImgAlign" name="inpImgAlign">
												<option value="" selected>&lt;Not Set&gt;</option>
												<option value="absBottom">absBottom</option>
												<option value="absMiddle">absMiddle</option>
												<option value="baseline">baseline</option>
												<option value="bottom">bottom</option>
												<option value="left">left</option>
												<option value="middle">middle</option>
												<option value="right">right</option>
												<option value="textTop">textTop</option>
												<option value="top">top</option>
											</select>
										</td>
										<td>Image border :</td>
										<td><select id="inpImgBorder" name="inpImgBorder">
												<option value="0" selected>0</option>
												<option value="1">1</option>
												<option value="2">2</option>
												<option value="3">3</option>
												<option value="4">4</option>
												<option value="5">5</option>
											</select>
										</td>
									</tr>
									<tr>
										<td>Width :</td>
										<td><INPUT id="inpImgWidth" type="text" size="2" name="inpImgWidth"></td>
										<td>Horizontal Spacing :</td>
										<td><INPUT id="inpHSpace" type="text" size="2" name="inpHSpace"></td>
									</tr>
									<tr>
										<td>Height :</td>
										<td><INPUT id="inpImgHeight" type="text" size="2" name="inpImgHeight"></td>
										<td>Vertical Spacing :</td>
										<td><INPUT id="inpVSpace" type="text" size="2" name="inpVSpace"></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td align="middle" colSpan="2">
								<table cellSpacing="0" cellPadding="0" align="center">
									<tr>
										<td><INPUT id="Button1" style="FONT: 8pt verdana,arial,sans-serif; HEIGHT: 22px" onclick="self.close();" type="button" value="Cancel" name="Button1"></td>
										<td><span id="btnImgInsert" style="DISPLAY: none"><INPUT id="Button2" style="FONT: 8pt verdana,arial,sans-serif; HEIGHT: 22px" onclick="InsertImage();self.close();" type="button" value="Insert" name="Button2">
											</span><span id="btnImgUpdate" style="DISPLAY: none"><INPUT id="Button3" style="FONT: 8pt verdana,arial,sans-serif; HEIGHT: 22px" onclick="UpdateImage();self.close();" type="button" value="Update" name="Button3">
											</span>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
					<!-- /Content --><br>
				</td>
			</tr>
		</table>
		<script language="JavaScript">
function deleteImage(sURL)
	{
	if (confirm("Delete this document ?") == true) 
		{
		window.navigate("default_image.aspx?action=del&file="+sURL+"&catid="+form2.catid.value);
		}
	}
function selectImage(sURL)
	{
	inpImgURL.value = sURL;
	
	divImg.style.visibility = "hidden"
	divImg.innerHTML = "<img id='idImg' src='" + sURL + "'>";
	

	var width = idImg.width
	var height = idImg.height 
	var resizedWidth = 150;
	var resizedHeight = 170;

	var Ratio1 = resizedWidth/resizedHeight;
	var Ratio2 = width/height;

	if(Ratio2 > Ratio1)
		{
		if(width*1>resizedWidth*1)
			idImg.width=resizedWidth;
		else
			idImg.width=width;
		}
	else
		{
		if(height*1>resizedHeight*1)
			idImg.height=resizedHeight;
		else
			idImg.height=height;
		}
	
	divImg.style.visibility = "visible"
	}
	
function checkImage()
	{
	var oSel = window.opener.idContent.document.selection.createRange()
	var sType = window.opener.idContent.document.selection.type		
	if ((oSel.item) && (oSel.item(0).tagName=="IMG")) //If image is selected 
		{
		selectImage(oSel.item(0).src)
		inpImgURL.value = oSel.item(0).src
		inpImgAlt.value = oSel.item(0).alt
		inpImgAlign.value = oSel.item(0).align
		inpImgBorder.value = oSel.item(0).border
		inpImgWidth.value = oSel.item(0).width
		inpImgHeight.value = oSel.item(0).height
		inpHSpace.value = oSel.item(0).hspace
		inpVSpace.value = oSel.item(0).vspace
		btnImgUpdate.style.display="block";
		}
	else
		{
		btnImgInsert.style.display="block";
		}		
	}
function UpdateImage()
	{
	window.opener.UpdateImage(inpImgURL.value,inpImgAlt.value,inpImgAlign.value,inpImgBorder.value,inpImgWidth.value,inpImgHeight.value,inpHSpace.value,inpVSpace.value);	
	}
function InsertImage()
	{
	window.opener.InsertImage(inpImgURL.value,inpImgAlt.value,inpImgAlign.value,inpImgBorder.value,inpImgWidth.value,inpImgHeight.value,inpHSpace.value,inpVSpace.value);
	}
		</script>
	</body>
</HTML>
