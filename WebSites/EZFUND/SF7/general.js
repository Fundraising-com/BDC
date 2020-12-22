var bAllow = false;

	function enterKeyPressLive(evt){				
		evt = (evt) ? evt : event;
		var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : 
        ((evt.which) ? evt.which : 0));
		if (charCode == 13){
			__doPostBack('StandardSearchLive1:btnGo','');
		}
   }

	function enterKeyPress(evt){				
		evt = (evt) ? evt : event;
		var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : 
        ((evt.which) ? evt.which : 0));
        if (charCode == 13){  			    						
			try{
			var temp = window.document.Form2.elements["Standardsearchcontrol1:txtKeyword"].type;
			__doPostBack('Standardsearchcontrol1:btnGo','');			
			}
			catch(er){
				try{
						var temp = window.document.Form2.elements["StandardSearchControl1:txtKeyword"].type;
						__doPostBack('StandardSearchControl1:btnGo','');
				}
				catch(er2){
						__doPostBack('StandardSearchControl2:btnGo','');
				}			
			}
        }
	}

var AddProductPopup = "AddProductPopUp.aspx";
function popUpWindow()
{
	var sFeatures, h, w, myThanks, i
	h = window.screen.availHeight 
	w = window.screen.availWidth 
	sFeatures = "height=" + h*.25 + ",width=" + h*.50 + ",screenY=" + (h*.30) + ",screenX=" + (w*.33) + ",top=" + (h*.30) + ",left=" + (w*.33) + ",resizable,scrollbars=yes"
	myThanks = window.open(AddProductPopup,"",sFeatures)
}
function popUpInventory(spage)
{
	var sFeatures, h, w, myThanks, i
	h = window.screen.availHeight 
	w = window.screen.availWidth 
	sFeatures = "height=" + 200 + ",width=" + h*.50 + ",screenY=" + (h*.30) + ",screenX=" + (w*.33) + ",top=" + (h*.30) + ",left=" + (w*.33) + ",resizable,scrollbars=no,location=no"
	myThanks = window.open(spage,"",sFeatures)
}
function closePopUpWindow()
{
	window.close();
}

function CheckoutFromPopUp()
{
	window.opener.location.href = "ShoppingCart.aspx";
	window.close();
}
function ConfirmCancel(msg){
var resp

resp = window.confirm(msg);
//window.document.openerForm.DelFlag.value="1";
return resp;
}


function doHelp(topic){
	window.focus()
	var w = window.screen.availWidth;
	var h = window.screen.availHeight;
	window.resizeTo(.75*w,h);
	window.moveTo(0.0);
	var sFeatures = "height=" + (h-(.05*h)) + ",width=" + w*.23 + ",resizable,scrollbars=1,top=0,left=" + w*.75; 
	var helpPane = window.open(topic,"helpPane",sFeatures)
	helpPane.focus();
}


function SetValidationSearchResults(str)
			{var e
			if (str == ""){str="::::::::::";}			
			ResetForm(window.document.Form2);
			for (var i = 0; i < window.document.Form2.length; i++) 
				{
				e = window.document.Form2.elements[i];
				if (e.name.indexOf(str)>-1)
					{
					//if (e.type != "radio")
					//	{
						if (e.type == "select-one" || e.type == "radio") 
							{e.title="Product Attribute";
							e.required=true;}
						if (e.type == "text" && (e.name.indexOf("txtQty")>-1 ||  e.name.indexOf("txtQuantity")>-1))
						
							{e.title="Product Quantity";
							e.quantitybox=true;
							e.required=true;
							}
					//	}
					}
				}
				
				return ValidateForm(window.document.Form2)
			}
		
function SetValidationProductBot(str,str2)
			{var e
			ResetForm(window.document.frmBot);
			if (str2=="")
				{str2="::::::::"}//this is a string that should not be in anything, so if it is blank, it shouldn't effect the if statement
			for (var i = 0; i < window.document.frmBot.length; i++) 
				{
				e = window.document.frmBot.elements[i];
				if (e.name.indexOf(str)>-1 || str2==e.name.substr(e.name.indexOf(str2)))   //contains str or ends with str2 (this satisfies productbots qty textboxes, so that one id might contain another)
					{
					//if (e.type != "radio")
					//	{
					
					
						if (e.type == "select-one" || e.type == "radio") 
							{
							if(e.type == "radio")
							{
							 e.title="Product Attribute";
							}							
							else
							{
							 e.title=e[0].text;
							}
							 
							e.required=true;
							}
						if (e.type == "text" && e.name.indexOf("txtQty")>-1)
							{e.title="Product Quantity";
							e.quantitybox=true;
							e.required=true;
							}
					//	}
					}
				}
			//	
				return ValidateForm(window.document.frmBot)
//return false
			}


function isNumber(value) {
	for (var i=0; i < value.length; i++) {
		a = parseInt(value.charAt(i));
		if (isNaN(a)) {
			return false;			
			break;
		}
	}
	return true;
}

function isCardNumValid(num) {
	var num1, num2, tempNum;
	
	if (!isNumber(num)) {
		return true;
	}
	num1 = ""
	if (!(num.length%2==0)) {
		for(var j=0; j < num.length; j++) {
			if ((j+1)%2==0){
				tempNum = 2 * num.charAt(j);
			}
			else {
				tempNum = 1 * num.charAt(j);
			}
			num1 = num1 + tempNum.toString();
		}
	}
	else{
		for(var j=0; j < num.length; j++){
			if ((j+1)%2==0){
				tempNum = 1 * num.charAt(j);
			}
			else{
				tempNum = 2 * num.charAt(j);
			}
			num1 = num1 + tempNum.toString();
		}
	}
	num2 = 0;
	for (var j = 0; j < num1.length; j++) {
		num2 = num2 + parseInt(num1.charAt(j));
	}
	if (num2%10==0) {
		return false;
	}
	else {
		return true;
	}
}

function specialCase(obj) {
	if (isCardNumValid(obj.value)) {
		bAllow = false;
		alert("The Credit Card Number is in an invalid format.");
	}
	else
		bAllow = true;

}


function isDate(strDate)
{
if (dateCheck(strDate, '%m/%d/%y') || dateCheck(strDate, '%d/%m/%y') ||
    dateCheck(strDate, '%m-%d-%y') || dateCheck(strDate, '%d-%m-%y'))
{return true;}
else
{return false;}
	
}
function ResetForm(form)
{
	var e
	
	for (var i = 0; i < form.length; i++) {
		e = form.elements[i];
		e.required=false;
		e.number=false;
		e.email=false;
		e.creditcardnumber=false;
		e.phonenumber=false;
		e.password=false;
		e.ssn=false;
		e.quantitybox=false;
	}
}
function checkValue(objForm){
  if (objForm.txtCardNumber == null)
  {
	if (objForm.txtPONumber == null)
	{
	  // ECheck is there
	 bAllow = true; 
	}
	else
	{
		// PO Number on page
		if (objForm.txtPONumber.value == "")
		{
			bAllow = false;
			Alert("Please enter something in PO");
		}
		else
			bAllow = true;
	}
  }
  else
  {
    if (objForm.txtCardNumber.value == "")
    {
      if (objForm.txtPONumber == null)
      {
		// Just Credit Card
		bAllow = false;
		alert("Please put in credit card");
      }
      else
      {
		if (objForm.txtPONumber.value == "")
		{
			bAllow = false;
			alert("Please put in something");
		}
		else
		{
			bAllow = true;
		}
      }
    }
    else
    {
		bAllow = true;
    }
  }

  return bAllow;
}

function stripChar(sValue, sChar) {
	var i, tempChar, buildString;
	buildString = ""
	for (var i=0; i<sValue.length; i++) {
		tempChar = sValue.charAt(i);
		if (tempChar != sChar) {
			buildString = buildString + tempChar;
		}
	}
	return buildString;
}
function ValidateForm(form) {
	var e, title, empty_fields, char_check, invalid_faxNumber,invalid_card, month, year, invalid_date, eMail, invalid_eMail 
	var strDate
	var iQuantity, quantity_check, checkSpecial, tempError, special_Error, msg, upperLine, lowerLine
	var num, invalid_phoneNumber, passwd_mismatch,invalid_ssn
	var strRadioButtonNames
	var RadioExists
	var RadioCnt
	var RadioChecked
	var ProdAttrCount
	ProdAttrCount=1;
	strRadioButtonNames=";"
	msg = "";
	empty_fields = "";
	char_check = "";
	special_Error = "";
	tempError = "";
	num = form.length
	var PasswordSet
	var PasswordStr
	for (var i = 0; i < form.length; i++) {
		e = form.elements[i]
		if ((e.title == null)||(e.title == "")) {
			title = e.name;
		}
		else {
			title = e.title;
		}
		
		if (((e.type == "text") || (e.type == "textarea")||(e.type == "password")) && !e.special && !e.disabled) {
			
			if (e.value.length <= 0 && e.required) {
				empty_fields += "\n            " + title;
				
				continue;
			}
			if (e.number) {
				num = e.value;
				num = stripChar(num, ".");
				num = stripChar(num, ",");
				if (!isNumber(num) || num.length>10) {
					char_check += "\n             " + title;
				}
			}
			if (e.creditcardnumber) {
				e.value = stripChar(e.value, " "); 
				e.value = stripChar(e.value, "-"); 
				invalid_card = isCardNumValid(e.value);
			}
			
			
			if (e.email) {
				eMail = e.value;
				if (eMail.length > 0)
				{
					if ((eMail.indexOf("@") != -1) && (eMail.indexOf(".") != -1)) {
						invalid_eMail = false;
					}
					else {
						invalid_eMail = true;
					}
				}	
			}
                        if (e.name == "txtEmail") {
				eMail = e.value;
				if ((eMail.indexOf("@") != -1) && (eMail.indexOf(".") != -1)) {
					invalid_eMail = false;
				}
				else {
				  
					invalid_eMail = true;
				}
			}	
			

			if (e.phonenumber) 
			{
				if (e.value !="") 
				{
				var isFax = e.title.indexOf("Fax");
				if(isFax >= 0 )
				 {
				   num = e.value;
					num = stripChar(num, " ");
					num = stripChar(num, "-");
					num = stripChar(num, "+");
					if (num.length < 10) 
					{
					invalid_faxNumber = true;
					}	
				 }
				else
				 {		
				
					num = e.value;
					num = stripChar(num, " ");
					num = stripChar(num, "-");
					num = stripChar(num, "+");
					if (num.length < 10) 
					{
					invalid_phoneNumber = true;
					}	
				  }
				}
			}
			if (e.ssn) 
			{
				if (e.value !="") 
				{
					num = e.value;
					num = stripChar(num, "-");
					if (num.length != 9 || !isNumber(num)) 
					{
					invalid_ssn = true;
					}	
				}
			}
			if (e.date) 
			{
				if (e.value !="") 
				{
					strDate= e.value;

					if (!isDate(strDate)) 
					{
					invalid_date = true;
					}	
				}
			}
		}
		if (e.quantitybox) {
			iQuantity = e.value;
			if (!isNumber(iQuantity)) {
				quantity_check = true;
			}
			if (parseInt(iQuantity) < 0) {
				quantity_check = true;
			}
			if ((iQuantity) < 1) {
				quantity_check = true;
			}

		}
		if (e.password)
			{
			if (PasswordSet==true)
				{
				if (e.value != PasswordStr)
					{
					passwd_mismatch = true;}
				}
			else
				{
				PasswordSet=true;
				PasswordStr=e.value;}
			}
		//if (e.special) {
		//	checkSpecial = specialCase(e, form);
		//	if (tempError != checkSpecial) {
		//		special_Error = special_Error + checkSpecial
		//	}
		//	tempError = checkSpecial;
		//}


		if (e.type=="select-one"){	
			if (e.required) {
		
				if (e.options[e.options.selectedIndex].value == "" ||e.options[e.options.selectedIndex].value == "-1" || (e.options[e.options.selectedIndex].value == "NONE" && e.name.indexOf("cboShipping")>-1) ||(e.options[e.options.selectedIndex].value == "0" && e.name.indexOf("cboShipChoices")>-1)) {
					empty_fields += "\n            " + title;
					continue;
				}
			}
		}
		if (e.type == "radio" && e.required) {
			if (strRadioButtonNames.indexOf(";" + e.name + ";") == -1)
			{//title=title + " " + ProdAttrCount
			//ProdAttrCount=++ProdAttrCount;
			RadioExists=true;
			RadioChecked=false;
			RadioCnt=0
			strRadioButtonNames=strRadioButtonNames + e.name + ";"
			if (form.elements[e.name].checked==true) {
				RadioChecked=true;
			}
			else if (form.elements[e.name].checked==false){
				empty_fields += "\n            " + title;
			}
			else
			{			
			do
				{
				if (form.elements[e.name][RadioCnt]==undefined) {
						RadioExists=false;
						}
					else
					{
						if (form.elements[e.name][RadioCnt].checked==true)
						{RadioChecked=true;}
						
					}
				RadioCnt=++RadioCnt
				}while(RadioExists==true);
						if (RadioChecked==false)
						{empty_fields += "\n            " + title;
							continue;
						}
					}
				}
		}
	}
	
	if (!empty_fields && !char_check && !invalid_ssn && !special_Error && !invalid_card && !invalid_date && !invalid_eMail && !quantity_check && !invalid_phoneNumber && !passwd_mismatch) {return true}
	
	msg = "The form was not submited due to the following error(s).\n";
	
	upperLine = "\n_________________________________________________________\n\n";
	lowerLine = "_________________________________________________________\n";
	
	if (empty_fields) {
		msg += upperLine;
		msg += "The following field(s) must be filled in:\n";
		msg += lowerLine;
		msg += empty_fields;
	}
	if (char_check) {
		msg += upperLine;
		msg += "The following field(s) need a numeric value 10 characters long or less:\n";
		msg += lowerLine;
		msg += char_check;
	}
	if (quantity_check) {
		msg += upperLine;
		msg += "Please Enter a Positive Integer.\n"
		msg += lowerLine;
	}
	if (invalid_card) {
		msg += upperLine;
		msg += "The Credit Card Number is an invalid format.\n";
		msg += lowerLine;
	}
	if (invalid_date) {
		msg += upperLine;
		msg += "Please Enter a Valid Date.\n";
		msg += lowerLine;
	}
	if (invalid_eMail) {
		msg += upperLine;
		msg += "The Email Address is in an invalid format.\n";
		msg += lowerLine;
	}
	if (invalid_phoneNumber) {
		msg += upperLine;
		msg += "Please enter a valid Phone Number with area code.\n";
		msg += lowerLine;
	}
	if (invalid_faxNumber) {
		msg += upperLine;
		msg += "Please enter a valid Fax Number with area code.\n";
		msg += lowerLine;
	}
	if (invalid_ssn) {
		msg += upperLine;
		msg += "Please enter a valid Social Security Number.\n";
		msg += lowerLine;
	}
	if (special_Error) {
		msg += upperLine;
		msg += special_Error + "\n";
		msg += lowerLine;
	}
	if (passwd_mismatch) {
		msg += upperLine;
		msg += "Your passwords did not match. Please enter them again.\n";
		msg += lowerLine;
	}		
	alert(msg);
return false;
}

function ExpirationDateMessage()
{
	    var  msg, upperLine, lowerLine
	    msg = "The form was not submited due to the following error(s).\n";
		upperLine = "\n_________________________________________________________\n\n";
		lowerLine = "_________________________________________________________\n";		
		msg += upperLine;
		msg += "Expiration Date is not valid.\n";
		msg += lowerLine;		
		alert(msg);
		return false;
}

//2425
function DateIssueNumMessage()
{
	    var  msg, upperLine, lowerLine
	    msg = "The form was not submited due to the following error(s).\n";
		upperLine = "\n_________________________________________________________\n\n";
		lowerLine = "_________________________________________________________\n";		
		msg += upperLine;
		msg += "Either a Start Date or Issue Number is required for Switch and Solo payments.\n";
		msg += lowerLine;		
		alert(msg);
		return false;
}

function StartDateMessage()
{
	    var  msg, upperLine, lowerLine
	    msg = "The form was not submited due to the following error(s).\n";
		upperLine = "\n_________________________________________________________\n\n";
		lowerLine = "_________________________________________________________\n";		
		msg += upperLine;
		msg += "Start Date is not valid.\n";
		msg += lowerLine;		
		alert(msg);
		return false;
}


/* Here's the list of tokens we support:
   m (or M) : month number, one or two digits.
   mm (or MM) : month number, strictly two digits (i.e. April is 04).
   d (or D) : day number, one or two digits.
   dd (or DD) : day number, strictly two digits.
   y (or Y) : year, two or four digits.
   yy (or YY) : year, strictly two digits.
   yyyy (or YYYY) : year, strictly four digits.
   mon : abbreviated month name (April is apr, Apr, APR, etc.)
   Mon : abbreviated month name, mixed-case (i.e. April is Apr only).
   MON : abbreviated month name, all upper-case (i.e. April is APR only).
   mon_strict : abbreviated month name, all lower-case (i.e. April is apr 
         only).
   month : full month name (April is april, April, APRIL, etc.)
   Month : full month name, mixed-case (i.e. April only).
   MONTH: full month name, all upper-case (i.e. APRIL only).
   month_strict : full month name, all lower-case (i.e. april only).
   h (or H) : hour, one or two digits.
   hh (or HH) : hour, strictly two digits.
   min (or MIN): minutes, one or two digits.
   mins (or MINS) : minutes, strictly two digits.
   s (or S) : seconds, one or two digits.
   ss (or SS) : seconds, strictly two digits.
   ampm (or AMPM) : am/pm setting.  Valid values to match this token are
         am, pm, AM, PM, a.m., p.m., A.M., P.M.
*/
// Be careful with this pattern.  Longer tokens should be placed before shorter
// tokens to disambiguate them.  For example, parsing "mon_strict" should 
// result in one token "mon_strict" and not two tokens "mon" and a literal
// "_strict".

var tokPat=new RegExp("^month_strict|month|Month|MONTH|yyyy|YYYY|mins|MINS|mon_strict|ampm|AMPM|mon|Mon|MON|min|MIN|dd|DD|mm|MM|yy|YY|hh|HH|ss|SS|m|M|d|D|y|Y|h|H|s|S");

// lowerMonArr is used to map months to their numeric values.

var lowerMonArr={jan:1, feb:2, mar:3, apr:4, may:5, jun:6, jul:7, aug:8, sep:9, oct:10, nov:11, dec:12}

// monPatArr contains regular expressions used for matching abbreviated months
// in a date string.

var monPatArr=new Array();
monPatArr['mon_strict']=new RegExp(/jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec/);
monPatArr['Mon']=new RegExp(/Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec/);
monPatArr['MON']=new RegExp(/JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC/);
monPatArr['mon']=new RegExp("jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec",'i');

// monthPatArr contains regular expressions used for matching full months
// in a date string.

var monthPatArr=new Array();
monthPatArr['month']=new RegExp(/^january|february|march|april|may|june|july|august|september|october|november|december/i);
monthPatArr['Month']=new RegExp(/^January|February|March|April|May|June|July|August|September|October|November|December/);
monthPatArr['MONTH']=new RegExp(/^JANUARY|FEBRUARY|MARCH|APRIL|MAY|JUNE|JULY|AUGUST|SEPTEMBER|OCTOBER|NOVEMBER|DECEMBER/);
monthPatArr['month_strict']=new RegExp(/^january|february|march|april|may|june|july|august|september|october|november|december/);

// cutoffYear is the cut-off for assigning "19" or "20" as century.  Any
// two-digit year >= cutoffYear will get a century of "19", and everything
// else gets a century of "20".

var cutoffYear=50;

// FormatToken is a datatype we use for storing extracted tokens from the
// format string.

function FormatToken (token, type) {
this.token=token;
this.type=type;
}

function parseFormatString (formatStr) {
var tokArr=new Array;
var tokInd=0;
var strInd=0;
var foundTok=0;
    
while (strInd < formatStr.length) {
if (formatStr.charAt(strInd)=="%" &&
(matchArray=formatStr.substr(strInd+1).match(tokPat)) != null) {
strInd+=matchArray[0].length+1;
tokArr[tokInd++]=new FormatToken(matchArray[0],"symbolic");
} else {

// No token matched current position, so current character should 
// be saved as a required literal.

if (tokInd>0 && tokArr[tokInd-1].type=="literal") {

// Literal tokens can be combined.Just add to the last token.

tokArr[tokInd-1].token+=formatStr.charAt(strInd++);
}
else {
tokArr[tokInd++]=new FormatToken(formatStr.charAt(strInd++), "literal");
      }
   }
}
return tokArr;
}

/* buildDate does all the real work.It takes a date string and format string,
 tries to match the two up, and returns a Date object (with the supplied date
 string value).If a date string doesn't contain all the fields that a Date
 object contains (for example, a date string with just the month), all
 unprovided fields are defaulted to those characteristics of the current
 date. Time fields that aren't provided default to 0.Thus, a date string
 like "3/30/2000" in "%mm/%dd/%yyyy" format results in a Date object for that
 date at midnight.formatStr is a free-form string that indicates special
 tokens via the % character.Here are some examples that will return a Date
 object:

 buildDate('3/30/2000','%mm/%dd/%y') // March 30, 2000
 buildDate('March 30, 2000','%Mon %d, %y') // Same as above.
 buildDate('Here is the date: 30-3-00','Here is the date: %dd-%m-%yy')

 If the format string does not match the string provided, an error message
 (i.e. String object) is returned.Thus, to see if buildDate succeeded, the
 caller can use the "typeof" command on the return value.For example,
 here's the dateCheck function, which returns true if a given date is
 valid,and false otherwise (and reports an error in the false case):

 function dateCheck(dateStr,formatStr) {
 var myObj=buildDate(dateStr,formatStr);
 if (typeof myObj=="object") {
 // We got a Date object, so good.
 return true;
 } else {
 // We got an error string.
 //alert(myObj);
 return false;
 }
 }

*/

function buildDate(dateStr,formatStr) {
// parse the format string first.
var tokArr=parseFormatString(formatStr);

var strInd=0;
var tokInd=0;
var intMonth;
var intDay;
var intYear;
var intHour;
var intMin;
var intSec;
var ampm="";
var strOffset;

// Create a date object with the current date so that if the user only
// gives a month or day string, we can still return a valid date.

var curdate=new Date();
intMonth=curdate.getMonth()+1;
intDay=curdate.getDate();
intYear=curdate.getFullYear();

// Default time to midnight, so that if given just date info, we return
// a Date object for that date at midnight.

intHour=0;
intMin=0;
intSec=0;

// Walk across dateStr, matching the parsed formatStr until we find a 
// mismatch or succeed.

while (strInd < dateStr.length && tokInd < tokArr.length) {

// Start with the easy case of matching a literal.

if (tokArr[tokInd].type=="literal") {
if (dateStr.indexOf(tokArr[tokInd].token,strInd)==strInd) {

// The current position in the string does match the format 
// pattern.

strInd+=tokArr[tokInd++].token.length;
continue;
}
else {

// ACK! There was a mismatch; return error.

return "\"" + dateStr + "\" does not conform to the expected format: " + formatStr;
   }
}

// If we get here, we're matching to a symbolic token.
switch (tokArr[tokInd].token) {
case 'm':
case 'M':
case 'd':
case 'D':
case 'h':
case 'H':
case 'min':
case 'MIN':
case 's':
case 'S':

// Extract one or two characters from the date-time string and if 
// it's a number, save it as the month, day, hour, or minute, as
// appropriate.

curChar=dateStr.charAt(strInd);
nextChar=dateStr.charAt(strInd+1);
matchArr=dateStr.substr(strInd).match(/^\d{1,2}/);
if (matchArr==null) {

// First character isn't a number; there's a mismatch between
// the pattern and date string, so return error.

switch (tokArr[tokInd].token.toLowerCase()) {
case 'd': var unit="day"; break;
case 'm': var unit="month"; break;
case 'h': var unit="hour"; break;
case 'min': var unit="minute"; break;
case 's': var unit="second"; break;
}
return "Bad " + unit + " \"" + curChar + "\" or \"" + curChar +
nextChar + "\".";
}
strOffset=matchArr[0].length;
switch (tokArr[tokInd].token.toLowerCase()) {
case 'd': intDay=parseInt(matchArr[0],10); break;
case 'm': intMonth=parseInt(matchArr[0],10); break;
case 'h': intHour=parseInt(matchArr[0],10); break;
case 'min': intMin=parseInt(matchArr[0],10); break;
case 's': intSec=parseInt(matchArr[0],10); break;
}
break;
case 'mm':
case 'MM':
case 'dd':
case 'DD':
case 'hh':
case 'HH':
case 'mins':
case 'MINS':
case 'ss':
case 'SS':

// Extract two characters from the date string and if it's a 
// number, save it as the month, day, or hour, as appropriate.

strOffset=2;
matchArr=dateStr.substr(strInd).match(/^\d{2}/);
if (matchArr==null) {

// The two characters aren't a number; there's a mismatch 
// between the pattern and date string, so return an error
// message.

switch (tokArr[tokInd].token.toLowerCase()) {
case 'dd': var unit="day"; break;
case 'mm': var unit="month"; break;
case 'hh': var unit="hour"; break;
case 'mins': var unit="minute"; break;
case 'ss': var unit="second"; break;
}
return "Bad " + unit + " \"" + dateStr.substr(strInd,2) + 
"\".";
}
switch (tokArr[tokInd].token.toLowerCase()) {
case 'dd': intDay=parseInt(matchArr[0],10); break;
case 'mm': intMonth=parseInt(matchArr[0],10); break;
case 'hh': intHour=parseInt(matchArr[0],10); break;
case 'mins': intMin=parseInt(matchArr[0],10); break;
case 'ss': intSec=parseInt(matchArr[0],10); break;
}
break;
case 'y':
case 'Y':

// Extract two or four characters from the date string and if it's
// a number, save it as the year.Convert two-digit years to four
// digit years by assigning a century of '19' if the year is >= 
// cutoffYear, and '20' otherwise.

if (dateStr.substr(strInd,4).search(/\d{4}/) != -1) {

// Four digit year.

intYear=parseInt(dateStr.substr(strInd,4),10);
strOffset=4;
}
else {
if (dateStr.substr(strInd,2).search(/\d{2}/) != -1) {

// Two digit year.

intYear=parseInt(dateStr.substr(strInd,2),10);
if (intYear>=cutoffYear) {
intYear+=1900;
}
else {
intYear+=2000;
}
strOffset=2;
}
else {

// Bad year; return error.

return "Bad year \"" + dateStr.substr(strInd,2) + 
"\". Must be two or four digits.";
   }
}
break;
case 'yy':
case 'YY':

// Extract two characters from the date string and if it's a 
// number, save it as the year.Convert two-digit years to four 
// digit years by assigning a century of '19' if the year is >= 
// cutoffYear, and '20' otherwise.

if (dateStr.substr(strInd,2).search(/\d{2}/) != -1) {

// Two digit year.

intYear=parseInt(dateStr.substr(strInd,2),10);
if (intYear>=cutoffYear) {
intYear+=1900;
}
else {
intYear+=2000;
}
strOffset=2;
} else {
// Bad year; return error
return "Bad year \"" + dateStr.substr(strInd,2) + 
"\". Must be two digits.";
}
break;
case 'yyyy':
case 'YYYY':

// Extract four characters from the date string and if it's a 
// number, save it as the year.

if (dateStr.substr(strInd,4).search(/\d{4}/) != -1) {

// Four digit year.

intYear=parseInt(dateStr.substr(strInd,4),10);
strOffset=4;
}
else {

// Bad year; return error.

return "Bad year \"" + dateStr.substr(strInd,4) + 
"\". Must be four digits.";
}
break;
case 'mon':
case 'Mon':
case 'MON':
case 'mon_strict':

// Extract three characters from dateStr and parse them as 
// lower-case, mixed-case, or upper-case abbreviated months,
// as appropriate.

monPat=monPatArr[tokArr[tokInd].token];
if (dateStr.substr(strInd,3).search(monPat) != -1) {
intMonth=lowerMonArr[dateStr.substr(strInd,3).toLowerCase()];
}
else {

// Bad month, return error.

switch (tokArr[tokInd].token) {
case 'mon_strict': caseStat="lower-case"; break;
case 'Mon': caseStat="mixed-case"; break;
case 'MON': caseStat="upper-case"; break;
case 'mon': caseStat="between Jan and Dec"; break;
}
return "Bad month \"" + dateStr.substr(strInd,3) + 
"\". Must be " + caseStat + ".";
}
strOffset=3;
break;
case 'month':
case 'Month':
case 'MONTH':
case 'month_strict':

// Extract a full month name at strInd from dateStr if possible.

monPat=monthPatArr[tokArr[tokInd].token];
matchArray=dateStr.substr(strInd).match(monPat);
if (matchArray==null) {

// Bad month, return error.

return "Can't find a month beginning at \"" +
dateStr.substr(strInd) + "\".";
}

// It's a good month.

intMonth=lowerMonArr[matchArray[0].substr(0,3).toLowerCase()];

strOffset=matchArray[0].length;
break;
case 'ampm':
case 'AMPM':
matchArr=dateStr.substr(strInd).match(/^(am|pm|AM|PM|a\.m\.|p\.m\.|A\.M\.|P\.M\.)/);
if (matchArr==null) {

// There's no am/pm in the string.Return error msg.

return "Missing am/pm designation.";
}

// Store am/pm value for later (as just am or pm, to make things
// easier later).

if (matchArr[0].substr(0,1).toLowerCase() == "a") {

// This is am.

ampm = "am";
}
else {
ampm = "pm";
}
strOffset = matchArr[0].length;
break;
}
strInd += strOffset;
tokInd++;
}
if (tokInd != tokArr.length || strInd != dateStr.length) {

/* We got through the whole date string or format string, but there's 
 more data in the other, so there's a mismatch. */

return "\"" + dateStr + "\" is either missing desired information or has more information than the expected format: " + formatStr;
}

// Make sure all components are in the right ranges.

if (intMonth < 1 || intMonth > 12) {
return "Month must be between 1 and 12.";
}
if (intDay < 1 || intDay > 31) {
return "Day must be between 1 and 31.";
}

// Make sure user doesn't put 31 for a month that only has 30 days

if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && intDay == 31) {
return "Month "+intMonth+" doesn't have 31 days!";
}

// Check for February date validity (including leap years) 

if (intMonth == 2) {

// figure out if "year" is a leap year; don't forget that
// century years are only leap years if divisible by 400

var isleap=(intYear%4==0 && (intYear%100!=0 || intYear%400==0));
if (intDay > 29 || (intDay == 29 && !isleap)) {
return "February " + intYear + " doesn't have " + intDay + 
" days!";
   }
}

// Check that if am/pm is not provided, hours are between 0 and 23.

if (ampm == "") {
if (intHour < 0 || intHour > 23) {
return "Hour must be between 0 and 23 for military time.";
   }
}
else {

// non-military time, so make sure it's between 1 and 12.

if (intHour < 1|| intHour > 12) {
return "Hour must be between 1 and 12 for standard time.";
   }
}

// If user specified amor pm, convert intHour to military.

if (ampm=="am" && intHour==12) {
intHour=0;
}
if (ampm=="pm" && intHour < 12) {
intHour += 12;
}
if (intMin < 0 || intMin > 59) {
return "Minute must be between 0 and 59.";
}
if (intSec < 0 || intSec > 59) {
return "Second must be between 0 and 59.";
}
return new Date(intYear,intMonth-1,intDay,intHour,intMin,intSec);
}
function dateCheck(dateStr,formatStr) {
var myObj = buildDate(dateStr,formatStr);
if (typeof myObj == "object") {

// We got a Date object, so good.

return true;
}
else {

// We got an error string.

//alert(myObj);
return false;
   }
}

 //begin Mod 6.9 - Anonymous Checkout v1.0 - Junu
 function isKey(e, k)
{
   var key;
   if(window.event) //IE
   {
      e = window.event;
      key = e.keyCode;
   }
   else //Mozilla
   {
      key = e.which;
   }
   if (key == k)
   {
     return true;
   }
   return false;
}
 function isEnterKey(e)
{
   return isKey(e, 13);
}

function postBack(e, eventTarget, eventArgument)
{
   e.returnValue=false;
   e.cancel = true;
   __doPostBack(eventTarget, eventArgument);
}
 //end Mod 6.9 - Anonymous Checkout v1.0 - Junu


//  End -->

// begin: JDB - Dynamic Image Suite
function Attribute(sAttributeName, aoControl, sDisplayType, aoOptions, aoOptionSuffixes)
{
	this.AttributeName = sAttributeName;
	this.Controls = aoControl;
	this.DisplayType = sDisplayType;
	this.Options = aoOptions;
	this.OptionSuffixes = aoOptionSuffixes;
	this.GetSelectedText = function()
	{
		var sText = "";
		if(this.Controls[0] != null)
		{
			if(this.DisplayType == 0)
			{
				if(this.Controls[0].selectedIndex >= 0)
				{
					var sValue = this.Controls[0].options[this.Controls[0].selectedIndex].value;
					for(iOptionIndex = 0; iOptionIndex < this.Options.length; iOptionIndex++)
					{
						if(this.Options[iOptionIndex].value == sValue)
						{
							sText = this.Options[iOptionIndex].text;
							break;
						}
					}
				}
			}
			else
			{
				for(iControlIndex = 0; iControlIndex < this.Controls.length; iControlIndex++)
				{
					if(this.Controls[iControlIndex].checked)
					{
						sText = this.Options[iControlIndex].text;
						break;
					}
				}
			}
		}
		return sText;
	};
	this.GetSelectedValue = function()
	{
		var sValue = "";
		if(this.Controls[0] != null)
		{
			if(this.DisplayType == 0)
			{
				if(this.Controls[0].selectedIndex >= 0)
				{
					sValue = this.Controls[0].options[this.Controls[0].selectedIndex].value;
				}
			}
			else
			{
				for(iControlIndex = 0; iControlIndex < this.Controls.length; iControlIndex++)
				{
					if(this.Controls[iControlIndex].checked)
					{
						sValue = this.Controls[iControlIndex].value;
						break;
					}
				}
			}
		}
		return sValue;
	};
	this.SetSelectedText = function(sText)
	{
		var sValue = -1;
		for(iOptionIndex = 0; iOptionIndex < this.Options.length; iOptionIndex++)
		{
			if(this.Options[iOptionIndex].text == sText)
			{
				sValue = this.Options[iOptionIndex].value;
				break;
			}
		}
		return this.SetSelectedValue(sValue);
	};
	this.SetSelectedValue = function(sValue)
	{
		var bFound = false;
		if(this.Controls[0] != null)
		{
			if(this.DisplayType == 0)
			{
				for(iOptionIndex = 0; iOptionIndex < this.Controls[0].options.length; iOptionIndex++)
				{
					if(this.Controls[0].options[iOptionIndex].value == sValue)
					{
						bFound = true;
						break;
					}
				}
				if(!bFound)
				{
					iOptionIndex = 0;
				}
				this.Controls[0].selectedIndex = iOptionIndex;
			}
			else
			{
				for(iControlIndex = 0; iControlIndex < this.Controls.length; iControlIndex++)
				{
					if(this.Controls[iControlIndex].value == sValue && !this.Controls[iControlIndex].disabled)
					{
						this.Controls[iControlIndex].checked = true;
						bFound = true;
						break;
					}
				}
				if(!bFound && sValue.length > 0)
				{
					for(iControlIndex = 0; iControlIndex < this.Controls.length; iControlIndex++)
					{
						if(!this.Controls[iControlIndex].disabled)
						{
							this.Controls[iControlIndex].checked = true;
							break;
						}
					}
				}
			}
		}
		return bFound;
	};
	this.UpdateOptions = function(oInvalidOptions, oOutOfStockOptions)
	{
		if(this.Controls[0] != null)
		{
			if(this.DisplayType == 0)
			{
				for(iOptionIndex = this.Controls[0].options.length - 1; iOptionIndex >= 0; iOptionIndex--)
				{
					this.Controls[0].remove(iOptionIndex);
				}
				for(iOptionIndex = 0; iOptionIndex < this.Options.length; iOptionIndex++)
				{
					var bUnavailable = false;
					for(iInvalidOptionIndex = 0; iInvalidOptionIndex < oInvalidOptions.length; iInvalidOptionIndex++)
					{
						if(this.Options[iOptionIndex].value == oInvalidOptions[iInvalidOptionIndex])
						{
							bUnavailable = true;
							break;
						}
					}
					var bOutOfStock = false
					if(!bUnavailable)
					{
						for(iOutOfStockOptionIndex = 0; iOutOfStockOptionIndex < oOutOfStockOptions.length; iOutOfStockOptionIndex++)
						{
							if(this.Options[iOptionIndex].value == oOutOfStockOptions[iOutOfStockOptionIndex])
							{
								bOutOfStock = true;
								break;
							}
						}
					}
					if(!bUnavailable)
					{
						if(bOutOfStock)
						{
							var oOutOfStockOption = new Option(this.Options[iOptionIndex].text + this.OptionSuffixes[iOptionIndex] + " " + sOutOfStock, this.Options[iOptionIndex].value);
							oOutOfStockOption.style.color = "#CCCCCC";
							this.Controls[0].options.add(oOutOfStockOption);
						}
						else
						{
							var oOption = new Option(this.Options[iOptionIndex].text + this.OptionSuffixes[iOptionIndex], this.Options[iOptionIndex].value);
							this.Controls[0].options.add(oOption);
						}
					}
				}
			}
			else
			{
				for(iControlIndex = 0; iControlIndex < this.Controls.length; iControlIndex++)
				{
					var bUnavailable = false;
					for(iInvalidOptionIndex = 0; iInvalidOptionIndex < oInvalidOptions.length; iInvalidOptionIndex++)
					{
						if(this.Controls[iControlIndex].value == oInvalidOptions[iInvalidOptionIndex])
						{
							bUnavailable = true;
							break;
						}
					}
					var bOutOfStock = false
					if(!bUnavailable)
					{
						for(iOutOfStockOptionIndex = 0; iOutOfStockOptionIndex < oOutOfStockOptions.length; iOutOfStockOptionIndex++)
						{
							if(this.Controls[iControlIndex].value == oOutOfStockOptions[iOutOfStockOptionIndex])
							{
								bOutOfStock = true;
								break;
							}
						}
					}
					var sSuffix = ""
					if(bUnavailable)
					{					
							sSuffix = sInvalid;
					}
					else
					{
						if(bOutOfStock)
						{
							sSuffix = sOutOfStock;
						}
					}
					this.Controls[iControlIndex].title = sSuffix;
					if(bUnavailable || bOutOfStock)
					{
						if(bUnavailable)
						{
							this.Controls[iControlIndex].disabled = true;
							this.Controls[iControlIndex].checked = false;
						}
						else
						{
							this.Controls[iControlIndex].disabled = false;
						}
					}
					else
					{
						this.Controls[iControlIndex].disabled = false;
					}
				}
			}
		}
	}
}

function CheckAttributeValues(oAttributeArray, bSuppressErrorMessage)
{
	var iAttributeCount = oAttributeArray.length - 2;
	var oInvalidCombinations = oAttributeArray[oAttributeArray.length - 2];
	var oOutOfStockCombinations = oAttributeArray[oAttributeArray.length - 1];
	if(oInvalidCombinations.length > 0 || oOutOfStockCombinations.length > 0)
	{
		var sAttributeSelection = "";
		var bPreviousValueStillExists = true;
		var sErrorAttributeName = ""
		for(var iAttributeControlIndex = 0; iAttributeControlIndex < oAttributeArray.length - 2; iAttributeControlIndex++)
		{
			if(sAttributeSelection.length > 0)
			{
				sAttributeSelection += ","
			}
			var sSelectedValue = oAttributeArray[iAttributeControlIndex].GetSelectedValue();

			var iCombinationCount = 1;
			for(var iCombinationIndex = iAttributeControlIndex; iCombinationIndex < oAttributeArray.length - 3; iCombinationIndex++)
			{
				iCombinationCount *= oAttributeArray[iCombinationIndex + 1].Options.length;
			}

			var oTemp = GetDistinctOptions(oAttributeArray[iAttributeControlIndex].Options, oInvalidCombinations, oOutOfStockCombinations, sAttributeSelection, iAttributeControlIndex, iAttributeCount, iCombinationCount);
			var oDistinctInvalidOptions = oTemp[0];
			var oDistinctOutOfStockOptions = oTemp[1];

			var sPreviousValue = oAttributeArray[iAttributeControlIndex].GetSelectedValue();
			oAttributeArray[iAttributeControlIndex].UpdateOptions(oDistinctInvalidOptions, oDistinctOutOfStockOptions);
			if(sPreviousValue.length)
			{
				bPreviousValueStillExists = bPreviousValueStillExists && oAttributeArray[iAttributeControlIndex].SetSelectedValue(sPreviousValue);
				if(!bPreviousValueStillExists)
				{
					sSelectedValue = oAttributeArray[iAttributeControlIndex].GetSelectedValue();
					if(sErrorAttributeName.length <= 0)
					{
						sErrorAttributeName = oAttributeArray[iAttributeControlIndex].AttributeName;
					}
				}
			}

			if(sSelectedValue.length > 0)
			{
				sAttributeSelection += sSelectedValue;
			}
			else
			{
				sAttributeSelection = ""
				break;
			}
		}
		
		if(!bSuppressErrorMessage && !bPreviousValueStillExists)
		{
			DisplayErrorMessage(sErrorAttributeName);
		}
		SetSwatch(oAttributeArray);
	}
}

function GetDistinctOptions(oOptions, oInvalidCombinations, oOutOfStockCombinations, sAttributeSelection, iAttributeIndex, iAttributeCount, iCombinationCount)
{
	var iDistinctInvalidIndex = 0;
	var iDistinctOutOfStockIndex = 0;
	var oDistinctInvalidOptions = new Array();
	var oDistinctOutOfStockOptions = new Array();
	var iOffset = iAttributeCount - iAttributeIndex;
	for(var iOptionIndex = 0; iOptionIndex < oOptions.length; iOptionIndex++)
	{
		var iInvalidCount = 0;
		for(iInvalidIndex = 0; iInvalidIndex < oInvalidCombinations.length; iInvalidIndex++)
		{
			if(oInvalidCombinations[iInvalidIndex].indexOf(sAttributeSelection + oOptions[iOptionIndex].value) == 0)
			{
				iInvalidCount++;
			}
		}
		var iOutOfStockCount = 0;
		for(iOutOfStockIndex = 0; iOutOfStockIndex < oOutOfStockCombinations.length; iOutOfStockIndex++)
		{
			if(oOutOfStockCombinations[iOutOfStockIndex].indexOf(sAttributeSelection + oOptions[iOptionIndex].value) == 0)
			{
				iOutOfStockCount++;
			}
		}
		if((iInvalidCount + iOutOfStockCount) >= iCombinationCount)
		{
			if(iInvalidCount == iCombinationCount)
			{
				oDistinctInvalidOptions[iDistinctInvalidIndex] = oOptions[iOptionIndex].value;
				iDistinctInvalidIndex++;
			}
			else
			{
				oDistinctOutOfStockOptions[iDistinctOutOfStockIndex] = oOptions[iOptionIndex].value;
				iDistinctOutOfStockIndex++;
			}
		}
	}
	return new Array(oDistinctInvalidOptions, oDistinctOutOfStockOptions);
}

function SetAttributeValues(oAttributeArray, sAttributeNames, sOptions)
{
	var oAttributeNames = sAttributeNames.split(",");
	var oOptions = sOptions.split(",");
	for(var iAttributeControlIndex = 0; iAttributeControlIndex < oAttributeArray.length - 2; iAttributeControlIndex++)
	{
		for(var iAttributeNameIndex = 0; iAttributeNameIndex < oAttributeNames.length; iAttributeNameIndex++)
		{
			if(oAttributeArray[iAttributeControlIndex].AttributeName == oAttributeNames[iAttributeNameIndex])
			{
				var bOptionExists = oAttributeArray[iAttributeControlIndex].SetSelectedText(oOptions[iAttributeNameIndex]);
				if(!bOptionExists)
				{
					DisplayErrorMessage(oAttributeArray[iAttributeControlIndex].AttributeName);
				}
				CheckAttributeValues(oAttributeArray, true);
			}
		}
	}
}

function DisplayErrorMessage(sAttributeName)
{
	alert(sInvalidOrOutOfStock.replace("[AttributeName]", sAttributeName));
}

function Swatch(sImage, sSwatchID, sOptions, sAttributeNames)
{
	this.Image = sImage;
	this.SwatchID = sSwatchID;
	this.Options = sOptions.split(',');
	this.AttributeNames = sAttributeNames.split(',');
	this.IsMatch = function(sOptions, sAttributeNames)
	{
		var aoOptions = sOptions.split(',');
		var aoAttributeNames = sAttributeNames.split(',');

		var sSelectedOptions = "";
		for(var iAttributeIndex = 0; iAttributeIndex < this.AttributeNames.length; iAttributeIndex++)
		{
			for(var iAttributeNamesIndex = 0; iAttributeNamesIndex < aoAttributeNames.length; iAttributeNamesIndex++)
			{
				if(aoAttributeNames[iAttributeNamesIndex] == this.AttributeNames[iAttributeIndex])
				{
					if(aoOptions.length > iAttributeNamesIndex)
					{
						if(sSelectedOptions.length > 0)
						{
							sSelectedOptions += ',';
						}
						sSelectedOptions += aoOptions[iAttributeNamesIndex];
					}
					break;
				}
			}
		}
		var sOptions = "";
		for(iOptionIndex = 0; iOptionIndex < this.Options.length; iOptionIndex++)
		{
			if(sOptions.length > 0)
			{
				sOptions += ',';
			}
			sOptions += this.Options[iOptionIndex];
		}
		return (sSelectedOptions == sOptions);
	};
	this.ChangeImage = function()
	{
		chgImg('imgProductImage', this.Image, this.SwatchID);
	};
}

var aoSwatch;
function SetSwatch(oAttributeArray)
{
	if(aoSwatch != undefined)
	{
		var sAttributeNames = ""
		var sOptions = ""
		for(var iAttributeIndex = 0; iAttributeIndex < oAttributeArray.length - 2; iAttributeIndex++)
		{
			if(sAttributeNames.length > 0)
			{
				sAttributeNames += ','
			}
			sAttributeNames += oAttributeArray[iAttributeIndex].AttributeName;
			var sSelectedText = oAttributeArray[iAttributeIndex].GetSelectedText();
			if(sSelectedText.length > 0)
			{
				if(sOptions.length > 0)
				{
					sOptions += ','
				}
				sOptions += sSelectedText;
			}
		}
		for(var iSwatchIndex = 0; iSwatchIndex < aoSwatch.length; iSwatchIndex++)
		{
			if(aoSwatch[iSwatchIndex].IsMatch(sOptions, sAttributeNames))
			{
				aoSwatch[iSwatchIndex].ChangeImage();
			}
		}
	}
}
// end: JDB - Dynamic Image Suite
// begin: JDB - Customer Bundle Rules
function BundleOption(iProductID, sProductName, oControl, iSelectableQuantity, iProductDetailCount)
{
	this.ProductID = iProductID;
	this.ProductName = sProductName;
	this.Control = oControl;
	this.SelectableQuantity = iSelectableQuantity;
	this.ProductDetailCount = iProductDetailCount;
	this.GetSelectedValue = function()
	{
		if(this.Control.checked)
		{
			return this.ProductID;
		}
		else
		{
			return "";
		}
	};
	this.GetRecurrenceCount = function()
	{
		return iTotalCombinations / this.ProductDetailCount * this.SelectableQuantity;
	};
}

function CheckBundleRuleValues(oBundleRuleArray, bSuppressErrorMessage)
{
	var iBundleRuleCount = oBundleRuleArray.length - 2;
	var oInvalidCombinations = oBundleRuleArray[oBundleRuleArray.length - 2];
	var oOutOfStockItems = oBundleRuleArray[oBundleRuleArray.length - 1];
	if(oInvalidCombinations.length > 0 || oOutOfStockItems.length > 0)
	{
		var sBundleRuleSelection = "";
		var sErrorProductNames = ""
		var iSelectionCount = 0;
		var iSelectionTotal = 0;
		for(var iBundleRuleIndex = 0; iBundleRuleIndex < oBundleRuleArray.length - 2; iBundleRuleIndex++)
		{
			oBundleRuleArray[iBundleRuleIndex].Control.disabled = false;
			oBundleRuleArray[iBundleRuleIndex].Control.title = "";
		}
		for(var iBundleRuleIndex = 0; iBundleRuleIndex < oBundleRuleArray.length - 2; iBundleRuleIndex++)
		{
			iSelectionTotal += oBundleRuleArray[iBundleRuleIndex].SelectableQuantity;
			iBundleRuleIndex += oBundleRuleArray[iBundleRuleIndex].ProductDetailCount - 1;
		}
		for(var iBundleRuleIndex = 0; iBundleRuleIndex < oBundleRuleArray.length - 2; iBundleRuleIndex++)
		{
			if(sBundleRuleSelection.length > 0 && sBundleRuleSelection.substring(sBundleRuleSelection.length - 1, sBundleRuleSelection.length) != ",")
			{
				sBundleRuleSelection += ","
			}
			
			if(iSelectionCount == iSelectionTotal - 1)
			{
				var sInvalidProductIDs = GetInvalidBundleOptions(oInvalidCombinations, sBundleRuleSelection);
				var oInvalidProductIDs = sInvalidProductIDs.split(",");
				for(iInvalidProductIndex = 0; iInvalidProductIndex < oInvalidProductIDs.length; iInvalidProductIndex++)
				{
					var sErrorProductName = DisableBundleOption(oBundleRuleArray, oInvalidProductIDs[iInvalidProductIndex], sInvalid);
					if(sErrorProductName.length > 0)
					{
						if(sErrorProductNames.length > 0)
						{
							sErrorProductNames += ","
						}
						sErrorProductNames += sErrorProductName;
					}
				}
				break;
			}

			var sSelectedValue = oBundleRuleArray[iBundleRuleIndex].GetSelectedValue();
			if(sSelectedValue.length > 0)
			{
				iSelectionCount++;
			}
			sBundleRuleSelection += sSelectedValue;
		}

		for(var iOutOfStockItemIndex = 0; iOutOfStockItemIndex < oOutOfStockItems.length; iOutOfStockItemIndex++)
		{
			DisableBundleOption(oBundleRuleArray, oOutOfStockItems[iOutOfStockItemIndex], sOutOfStock);
		}

		if(sErrorProductNames.length > 0)
		{
			var oErrorProductNames = sErrorProductNames.split(",");
			if(!bSuppressErrorMessage && oErrorProductNames.length > 0)
			{
				for(iErrorProductIndex = 0; iErrorProductIndex < oErrorProductNames.length; iErrorProductIndex++)
				{
					DisplayErrorMessage(oErrorProductNames[iErrorProductIndex]);
				}
			}
		}
	}
}

function DisableBundleOption(oBundleRuleArray, iProductID, sMessage)
{
	var sErrorProductName = "";
	for(var iBundleRuleIndex = 0; iBundleRuleIndex < oBundleRuleArray.length - 2; iBundleRuleIndex++)
	{
		if(oBundleRuleArray[iBundleRuleIndex].ProductID == iProductID)
		{
			if(oBundleRuleArray[iBundleRuleIndex].Control.checked)
			{
				oBundleRuleArray[iBundleRuleIndex].Control.checked = false;
				sErrorProductName = oBundleRuleArray[iBundleRuleIndex].ProductName;
			}
			oBundleRuleArray[iBundleRuleIndex].Control.disabled = true;
			oBundleRuleArray[iBundleRuleIndex].Control.title = sMessage;
		}
	}
	return sErrorProductName;
}

function GetInvalidBundleOptions(oInvalidCombinations, sBundleRuleSelection)
{
	var sInvalidProductIDs = "";
	for(iInvalidIndex = 0; iInvalidIndex < oInvalidCombinations.length; iInvalidIndex++)
	{
		if(oInvalidCombinations[iInvalidIndex].indexOf(sBundleRuleSelection) == 0)
		{
			var oInvalid = oInvalidCombinations[iInvalidIndex].split(",");
			if(sInvalidProductIDs.length > 0)
			{
				sInvalidProductIDs += ",";
			}
			sInvalidProductIDs += oInvalid[oInvalid.length - 1];
		}
	}
	return sInvalidProductIDs;
}

function Factorial(i)
{
	var iFactorial = 1;
	for(iIndex = 1; iIndex <= i; iIndex++)
	{
		iFactorial *= iIndex;
	}
	return iFactorial;
}
// end: JDB - Customer Bundle Rules
