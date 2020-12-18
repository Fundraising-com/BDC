/**
 * EditLive.js
 * Copyright (c) 1999-2000 Ephox
 * 
 * Author: Dan
 * Date: 05/05/2000
 * Revision: 1.2
 *
 * This software is provided "AS IS," without a warranty of any kind.
 */

		
function EditLive_compatible() {
	//get info about the user's browser
	getBrowserInfo();
	
	if (navigator.IsWindows) {
	   if ((navigator.clientVersion.substring(0, 1) >= 4)) {
	      return(true);
	   }else{ // old version of browser
	      return(false);
	   }
	} else { // non-Windows machine
	      return(false);
	}
}		
		
function EditLive_installed() {
	//get info about the user's browser

	getBrowserInfo();
	
	if (navigator.IsMSIE) {
	   if (GotEditLive()) {
	      return(true);
	   }else{
	      return(false);
	   }
	} else {
	   plugins = navigator.plugins;
	   if(plugins["NPEditLive plugin"]) {
	      return(true);
	   } else {
	      return(false);
	   }
	}
}		


function EditLive_init(objname, x, y) {
   if (navigator.IsMSIE) {
	document.write('<OBJECT classid="CLSID:BAEEE938-A83E-11D3-9CE4-0000B48A5E83" id="' + objname + '"');
	document.writeln(' width="' + x + '" height="' + y + '"></OBJECT>');
   } else {
	//document.write('<ILAYER ID="layer1">')
	document.write('<EMBED TYPE="application/x-editlive-plugin" NAME="' + objname + '"');
	document.writeln(' WIDTH="' + x + '" HEIGHT="' + y + '">')
	//document.write('</ILAYER>');
   }
}
 
function EditLive(label, x, y) {
	
	//get info about the user's browser
	getBrowserInfo();
	
	//test whether or not the user has EditLive installed
    if(EditLive_installed()) {
    
    
			//create the HTML for the plug-in or ActiveX control
			EditLive_init(label,x,y);

			this.label = label;

			//properties
			this.setAllowFontColor = setAllowFontColor;
			this.getAllowFontColor = getAllowFontColor;
			this.setAllowFontSize = setAllowFontSize;
			this.getAllowFontSize = getAllowFontSize;
			this.setAllowPageProperties = setAllowPageProperties;
			this.getAllowPageProperties = getAllowPageProperties;
			this.setAllowTables = setAllowTables;
			this.getAllowTables = getAllowTables;
			this.setAllowUnderline= setAllowUnderline;
			this.getAllowUnderline = getAllowUnderline;
			this.setEditLiveMode= setEditLiveMode;
			this.getEditLiveMode = getEditLiveMode;
			this.setFTPInitialDirectory= setFTPInitialDirectory;
			this.getFTPInitialDirectory = getFTPInitialDirectory;
			this.setFTPPassword = setFTPPassword;
			this.getFTPPassword = getFTPPassword;
			this.setFTPServer = setFTPServer;
			this.getFTPServer = getFTPServer;
			this.setFTPServerPort = setFTPServerPort;
			this.getFTPServerPort = getFTPServerPort;
			this.setFTPUsername = setFTPUsername;
			this.getFTPUsername = getFTPUsername;
			this.setImageMode = setImageMode;
			this.getImageMode = getImageMode;
			this.getIsDirty = getIsDirty;
			this.setSource = setSource;
			this.getSource = getSource;
			this.setSourceAll = setSourceAll;
			this.getSourceAll = getSourceAll;
			this.setStyleSheet = setStyleSheet;
			this.getStyleSheet = getStyleSheet;
			this.setStylesOnlyMode = setStylesOnlyMode;
			this.getStylesOnlyMode = getStylesOnlyMode;
			this.getVersionBuild = getVersionBuild;
			this.getVersionMajor = getVersionMajor;
			this.getVersionMinor = getVersionMinor;
			this.setWebRoot = setWebRoot;
			this.setFTPImageRoot = setFTPImageRoot;
			this.getFTPImageRoot = getFTPImageRoot;
			this.getText = getText;

			//methods
			this.clearFontList = clearFontList;
			this.loadFile = loadFile;
			this.loadFont = loadFont;
			this.saveFile = saveFile;
			this.uploadImages = uploadImages;
			this.insertHTMLAtCursor = insertHTMLAtCursor;
			this.insertImageAtCursor = insertImageAtCursor;
			this.insertHyperlinkAtCursor = insertHyperlinkAtCursor;

			if (navigator.IsMSIE) { //ie
			    this.editlive = document.all(this.label);
			    this.getprefix = "";
			    this.getpostfix = "";
			    this.setprefix = "";
			    this.setpostfixleft = " = ";
			    this.setpostfixright = "";
			} else { //netscape
				this.editlive = document.embeds[this.label];
				this.getprefix = "get";
				this.getpostfix = "()";
				this.setprefix = "set";
				this.setpostfixleft = "(";
				this.setpostfixright = ")";
			}
		
	} else {   //editLive is not installed
		    
		    document.writeln('<table width=600 height=400 bgcolor=#ffffcc><tr align=center><td>');
			document.writeln('<p>EditLive! is not installed or an error has occurred trying to load EditLive!</p>')
			document.writeln('<p>Exit your browser and try again.');
			document.writeln('</td></tr></table>');
	
	}

   //pause while plug-in is initialized
   setTimeout('wait()',1000);
   //window.alert('waitdone');
}

/*
 *
 *  PROPERTIES
 *
 */


function setAllowFontColor(allowFontColor) {
    setProperty(this, "AllowFontColor", allowFontColor);
}
function getAllowFontColor() {
    return getProperty(this, "AllowFontColor");
}


function setAllowFontSize(allowFontSize) {
    setProperty(this, "AllowFontSize", allowFontSize);
}
function getAllowFontSize() {
    return getProperty(this, "AllowFontSize");
}


function setAllowPageProperties(allowPageProperties) {
    setProperty(this, "AllowPageProperties", allowPageProperties);
}
function getAllowPageProperties() {
    return getProperty(this, "AllowPageProperties");
}


function setAllowTables(allowTables) {
    setProperty(this, "AllowTables", allowTables);
}
function getAllowTables() {
    return getProperty(this, "AllowTables");
}


function setAllowUnderline(allowUnderline) {
    setProperty(this, "AllowUnderline", allowUnderline);
}
function getAllowUnderline() {
    return getProperty(this, "AllowUnderline");
}


function setEditLiveMode(invar) {
    setProperty(this, "EditLiveMode", invar);
}
function getEditLiveMode() {
    return getProperty(this, "EditLiveMode");
}



function setFTPInitialDirectory(invar) {
    setProperty(this, "FTPInitialDirectory", invar);
}
function getFTPInitialDirectory() {
    return getProperty(this, "FTPInitialDirectory");
}

function setFTPImageRoot(invar) {
    if ((this.getVersionMajor() == 1) && (this.getVersionMinor() < 2)) {
        alert("This property requires EditLive! 1.2 which you are not running.\nPlease download EditLive! 1.2 from http://www.editlive.com");
    } else {
        setProperty(this, "FTPImageRoot", invar);
    }
}
function getFTPImageRoot() {
    if ((this.getVersionMajor() == 1) && (this.getVersionMinor() < 2)) {
        alert("This property requires EditLive! 1.2 which you are not running.\nPlease download EditLive! 1.2 from http://www.editlive.com");
    } else {
        return getProperty(this, "FTPImageRoot");
    }
}

function setFTPPassword(ftpPassword) {
    setProperty(this, "FTPPassword", ftpPassword);
}
function getFTPPassword() {
    return getProperty(this, "FTPPassword");
}


function setFTPServer(ftpServer) {
    setProperty(this, "FTPServer", ftpServer);
}
function getFTPServer() {
    return getProperty(this, "FTPServer");
}


function setFTPServerPort(invar) {
    setProperty(this, "FTPServerPort", invar);
}
function getFTPServerPort() {
    return getProperty(this, "FTPServerPort");
}


function setFTPUsername(ftpUsername) {
    setProperty(this, "FTPUsername", ftpUsername);
}
function getFTPUsername() {
    return getProperty(this, "FTPUsername");
}


function setImageMode(invar) {
    setProperty(this, "ImageMode", invar);
}
function getImageMode() {
    return getProperty(this, "ImageMode");
}


function getIsDirty() {
    return getProperty(this, "IsDirty");
}


function setSource(html) {
	//html = hackHTML(html);
    setProperty(this, "Source", html);
}
function getSource() {
    return getProperty(this, "Source");
}


function setSourceAll(html) {
	//html = hackHTML(html);
    setProperty(this, "SourceAll", html);
}
function getSourceAll() {
    return getProperty(this, "SourceAll");
}

function hackHTML(inString)
{

	outString = filter('\r', '', inString)
	outString = filter('\n','', outString)
	outString = filter("'", "\'", outString)
	//outString = filter('"', '\"', outString)
	return outString;
}
function filter(inTag, outTag, inString) {
	split = inString.split(inTag)
	var outString = '';
	if (split.length > 0) {
		for(i=0; i<split.length; i++) {
			if (i==split.lenth) {
				outString += split[i];
			} else {
				outString += split[i] + outTag;
			}
		}	
		return outString;
	} else {
		return inString;
	}
}


function setStyleSheet(invar) {
    setProperty(this, "StyleSheet", invar);
}
function getStyleSheet() {
    return getProperty(this, "StyleSheet");
}


function setStylesOnlyMode(invar) {
    setProperty(this, "StylesOnlyMode", invar);
}
function getStylesOnlyMode() {
    return getProperty(this, "StylesOnlyMode");
}


function getText() {
    if ((this.getVersionMajor() == 1) && (this.getVersionMinor() < 2)) {
        alert("This property requires EditLive! 1.2 which you are not running.\nPlease download EditLive! 1.2 from http://www.editlive.com");
    } else {
        return getProperty(this, "Text");
    }
}


function getVersionBuild() {
    return getProperty(this, "VersionBuild");
}


function getVersionMajor() {
    return getProperty(this, "VersionMajor");
}


function getVersionMinor() {
    return getProperty(this, "VersionMinor");
}


function setWebRoot(webRoot) {
    setProperty(this, "WebRoot", webRoot);
}
function getWebRoot() {
    return getProperty(this, "WebRoot");
}


/*
 *
 *  METHODS
 *
 */

function clearFontList() {
        this.editlive.ClearFontList();
}

function loadFile(pathIn) {
    if (document.all) { //ie
        document.all(this.label).LoadFile(pathIn);
    } else { //netscape
	document.embeds[this.label].LoadFile(pathIn);
    }
}

function loadFont(fontName) {
    if (document.all) { //ie
        document.all(this.label).LoadFont(fontName);
    } else { //netscape
	document.embeds[this.label].LoadFont(fontName);
    }
}


function saveFile() {
    if (document.all) { //ie
        document.all(this.label).SaveFile();
    } else { //netscape
	document.embeds[this.label].SaveFile();
    }
}


function uploadImages() {
    if (document.all) { //ie
        document.all(this.label).UploadImages();
    } else { //netscape
	document.embeds[this.label].UploadImages();
    }
}

function insertHTMLAtCursor(htmlString) {
    if ((this.getVersionMajor() == 1) && (this.getVersionMinor() < 2)) {
        alert("This method requires EditLive! 1.2 which you are not running.\nPlease download EditLive! 1.2 from http://www.editlive.com");
    } else {
        if (document.all) { //ie
            document.all(this.label).InsertHTMLAtCursor(htmlString);
        } else { //netscape
            document.embeds[this.label].InsertHTMLAtCursor(htmlString);
        }
    }
}

function insertImageAtCursor(imageString) {
    if ((this.getVersionMajor() == 1) && (this.getVersionMinor() < 2)) {
        alert("This method requires EditLive! 1.2 which you are not running.\nPlease download EditLive! 1.2 from http://www.editlive.com");
    } else {
        if (document.all) { //ie
            document.all(this.label).InsertImageAtCursor(imageString);
        } else { //netscape
            document.embeds[this.label].InsertImageAtCursor(imageString);
        }
    }
}

function insertHyperlinkAtCursor(linkString) {
    if ((this.getVersionMajor() == 1) && (this.getVersionMinor() < 2)) {
        alert("This method requires EditLive! 1.2 which you are not running.\nPlease download EditLive! 1.2 from http://www.editlive.com");
    } else {
        if (document.all) { //ie
            document.all(this.label).InsertHyperlinkAtCursor(linkString);
        } else { //netscape
            document.embeds[this.label].InsertHyperlinkAtCursor(linkString);
        }
    }
}


/**
 *
 *     SUPPORT FUNCTIONS
 *
*/

function setProperty(obj, property, value) {
    execstr = "obj.editlive." + obj.setprefix + property + obj.setpostfixleft;
    if (typeof(value) == 'string') {
  		  var propVal = escape(value)
		  execstr += " unescape('" + propVal + "') "
	} else {
		execstr += value
	}
    execstr += obj.setpostfixright + ";";
    eval(execstr);
}

function getProperty(obj, property) {
    return eval("obj.editlive." + obj.getprefix + property + obj.getpostfix);
}
 
function getBrowserInfo() {

	navigator.IsWindows = false;
	navigator.IsMac = false;
	navigator.IsMSIE = false;
	navigator.IsMSIEDHTML = false;

	var version = navigator.appVersion;
		
	if (version != "") {
		var iParen = version.indexOf("(", 0);
		var sUsrAgent = new String(navigator.userAgent);
		sUsrAgent = sUsrAgent.toLowerCase();
    
		navigator.clientVersion = version.substring(0, iParen - 1);
		if (sUsrAgent.indexOf("msie", 0) > 0) {
			navigator.IsMSIE = true;
			if (navigator.clientVersion.substring(0, 1) >= 4) {
				navigator.IsMSIEDHTML = true;
			}
		}
    
		if (sUsrAgent.indexOf("win", 0) > 0) {
		  navigator.IsWindows = true;
		} else {
		  if (sUsrAgent.indexOf("macintosh", 0) > 0) {
		    navigator.IsMac = true;
		  }
		}
	}
} 
	
function wait() {
	var x
	x =1
	for (i=0;i<10000;i++) {
		x++;
	}
}

