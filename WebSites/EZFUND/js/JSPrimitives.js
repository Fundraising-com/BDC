// JSPrimitives.js -- really fundamental stuff needed by lots of our JavaScript

// Classic IsInt from days of yore; only the bracing is changed...

function IsInt(numstr, allowNegatives) {
	var isValid = true;

	if (numstr+"" == "undefined" || numstr+"" == "null" || numstr+"" == "")	{
		return false;
	}

	if (allowNegatives+"" == "undefined" || allowNegatives+"" == "null") {
		allowNegatives = true;
	}

	numstr += "";	
	for (i = 0; i < numstr.length; i++) {
    	if (!((numstr.charAt(i) >= "0") && (numstr.charAt(i) <= "9") || (numstr.charAt(i) == "-"))) {
			isValid = false;
			break;
		}
		else if ((numstr.charAt(i) == "-" && i != 0) || (numstr.charAt(i) == "-" && !allowNegatives)) {
			isValid = false;
			break;
		}
	}

	return isValid;
}

// String functions like their VB or C equivalents
// (don't forget JS is case-sensitive!)

function ltrim(sString) {
	var s = sString;
	while (s.length > 0 && s.substring(0,1) == " ") {
		s = s.substring(1,s.length);
	}
	return s;
}

function rtrim(sString) {
	var s = sString;
	while (s.length > 0 && s.substring(s.length-1,s.length) == " ") {
		s = s.substring(0,s.length-1);
	}
	return s;
}

function trim(sString) {
	return ltrim(rtrim(sString));
}

function isdigit(sChar) {
	return (("0123456789".indexOf(sChar)) >= 0);
}

function isalpha(sChar) {
	return (("abcdefghijklmnopqrstuvwxyz".indexOf(sChar.toLowerCase())) >= 0);
}

function isalnum(sChar) {
	return (isdigit(sChar) || isalpha(sChar));
}

function isalldigit(sString) {
	var i;
	if (sString == "") return false;	// arbitrary
	for (i=0; i<sString.length; i++) {
		if (!isdigit(sString.charAt(i))) return false;
	}
	return true;
}

function isallalpha(sString) {
	var i;
	if (sString == "") return false;	// arbitrary
	for (i=0; i<sString.length; i++) {
		if (!isalpha(sString.charAt(i))) return false;
	}
	return true;
}

function isallalnum(sString) {
	var i;
	if (sString == "") return false;	// arbitrary
	for (i=0; i<sString.length; i++) {
		if (!isalnum(sString.charAt(i))) return false;
	}
	return true;
}

function isvaliddate(sString) {
	var dte, i, n, c;
	
	// allow only numeric-slash-dash dates
	n = sString.length;
	for (i=0; i<n; i++) {
		c = sString.charAt(i);
		if (c=='/' || c=='-' || isdigit(c)) {
			// valid char
		} else {
			return false;
		}
	}

	dte = new Date(sString);

	// NOTE: VB allows dates w/o year, implying current year; this accommodates that behavior
	if (isNaN(dte)) {
		dte = new Date();
		dte = new Date(sString + '/' + dte.getYear());
	}
	if (isNaN(dte)) {
		dte = new Date();
		dte = new Date(sString + '-' + dte.getYear());
	}
	return !isNaN(dte);
}
