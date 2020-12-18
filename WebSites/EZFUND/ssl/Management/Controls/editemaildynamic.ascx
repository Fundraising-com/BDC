<%@ Control Language="vb" AutoEventWireup="false" Codebehind="editemaildynamic.ascx.vb" Inherits="StoreFront.StoreFront.editemaildynamic" TargetSchema="http://schemas.microsoft.com/intellisense/nav4-0" %>
<div align="center">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td>
				<!-- STEP 1 : PREPARE FORM -->
				<!-- /STEP 1 -->
				<!-- STEP 2 : PREPARE CONTENT EDITOR -->
				<iframe id="idWord" name="idWord" style="Z-INDEX:-1;VISIBILITY:hidden;WIDTH:1px;POSITION:absolute;HEIGHT:1px"></iframe>
				<iframe name="idContentTmp" style="Z-INDEX: -1; VISIBILITY: hidden; POSITION: absolute"></iframe>
				<table id="idToolbar" bgcolor="#e2e2e2" cellpadding="0" cellspacing="0" class="tblCoolbar" width="100%">
					<tr>
						<td>
							<script language="JavaScript">							
							//drawIcon(0,57,"btnStyle","Apply Style","displayStyleBox()")
							drawIcon(57,106,"btnParagraph","Paragraph","displayParagraphBox()")
							drawIcon(163,110,"btnFontName","Font Name","displayFontNameBox()")
							drawIcon(273,96,"btnFontSize","Font Size","displayFontSizeBox()")
							drawIcon(392,23,"btnCut","Cut","doCmd('Cut');")
							drawIcon(415,23,"btnCopy","Copy","doCmd('Copy');")
							drawIcon(438,23,"btnPaste","Paste","doCmd('Paste');")
							drawIcon(461,23,"btnUndo","Undo","doCmd('Undo')")
							drawIcon(484,23,"btnRedo","Redo","doCmd('Redo')")
							drawIcon(1059,23,"btnAbsolute","Edit Email","editEmail()")	
							document.write("<br>")
							drawIcon(507,23,"btnBold","Bold","doCmd('Bold');")
							drawIcon(530,23,"btnItalic","Italic","doCmd('italic');")
							drawIcon(553,23,"btnUnderline","Underline","doCmd('underline');")
							drawIcon(576,23,"btnStrikethrough","Strikethrough","doCmd('strikethrough');")
							drawIcon(599,23,"btnSuperscript","Superscript","doCmd('superscript');")
							drawIcon(622,23,"btnSubscript","Subscript","doCmd('subscript');")
							drawIcon(668,23,"btnJustifyLeft","Justify Left","doCmd('justifyleft');")
							drawIcon(691,23,"btnJustifyCenter","Justify Center","doCmd('justifycenter');")
							drawIcon(714,23,"btnJustifyRight","Justify Right","doCmd('justifyright');")
							drawIcon(737,23,"btnJustifyFull","Justify Full","doCmd('justifyfull');")
							drawIcon(760,23,"btnInsertOrderedList","Ordered List","doCmd('insertorderedlist');")
							drawIcon(783,23,"btnInsertUnorderedList","Unordered List","doCmd('insertunorderedlist');")
							drawIcon(806,23,"btnIndent","Indent","doCmd('indent');")
							drawIcon(829,23,"btnOutdent","Outdent","doCmd('outdent');")	
							drawIcon(852,23,"btnHorizontalLine","Horizontal Line","doCmd('InsertHorizontalRule')")
							drawIcon(875,23,"btnTable","Create Table","displayTableBox()")
							drawIcon(898,23,"btnExternalLink","External Link","displayLinkBox()")
							drawIcon(944,23,"btnUnlink","Unlink","doCmd('unlink')")
							drawIcon(990,23,"btnInternalImage","Insert/update Image","doCmd('DialogImage')")
							drawIcon(1013,23,"btnForeground","Foreground","displayFgColorBox()")
							//drawIcon(1036,23,"btnBackground","Background","displayBgColorBox();")
							//drawIcon(1059,23,"btnAbsolute","Make Absolute","doCmd('absoluteposition')")
							drawIcon(1082,23,"btnRemoveFormat","Remove Formatting","doCmd('removeformat')")
							drawIcon(645,23,"btnInsertSymbol","Insert Symbol","displaySymbolBox()")	
							//drawIcon(1105,23,"btnDocumentBackground","Document Background","displayDocBgColorBox()")	
							
							</script>
							<!--Email Subject<input type=text size=40 id=idEmailSubject value="" style="height: 19px;font:8pt verdana,arial,sans-serif" NAME="idEmailSubject">-->
						</td>
					</tr>
				</table>
				<iframe name="idContent" class="editArea" id="idContent" contentEditable="true" onfocus="popupHide()" width="100%" height="400" onblur="saveToHiddenField()"></iframe>
			</td>
		</tr>
		<tr>
			<td>
				<table id="idToolbarBottom" class="tblCoolbar" bgcolor="#e2e2e2" cellpadding="0" cellspacing="0" width="100%">
					<tr>
						<td align="left">
							<input type="checkbox" onclick="setDisplayMode()" id="chkDisplayMode" name="chkDisplayMode">
							HTML
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</div>
