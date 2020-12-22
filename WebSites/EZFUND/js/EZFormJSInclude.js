function strip(number) {
	var sOut = '';
	mask = '1234567890';
	for (count = 0; count <= number.length; count++) {
		if (mask.indexOf(number.substring(count, count+1),0) != -1 ) sOut += number.substring(count,count+1);
	}
	return sOut;
}

function ValidateZipField(theForm) {
  
	if (theForm.Zip.value == "") {
		alert("Please enter a value for the \"Zip code\" field.");
		theForm.Zip.focus();
		return (false);
	}

	if (theForm.Zip.value.length < 5 ||
		(theForm.Zip.value.length > 5 && theForm.Zip.value.length < 10)) {
		alert("Please enter either 5 digits or ZIP+4 in the \"Zip code\" field, (as \"nnnnn\" or \"nnnnn-nnnn\").");
		theForm.Zip.focus();
		return (false);
	}

	var checkOK = "0123456789";
	var checkStr = theForm.Zip.value;
	var allValid = true;
		
	for (i = 0;  i < checkStr.length;  i++) {
		ch = checkStr.charAt(i);
		if (i == 5 && ch != '-') {
			allValid = false;
			break;
		}
		for (j = 0;  j < checkOK.length;  j++) {
			if (ch == '-' && i == 5)
				break;
			if (ch == checkOK.charAt(j))
				break;
		}		
		if (j == checkOK.length) {
			allValid = false;
			break;
		}
	}
  
	if (!allValid) {
		alert("Please enter either 5 digits or ZIP+4 in the \"Zip code\" field, (as \"nnnnn\" or \"nnnnn-nnnn\").");
		theForm.Zip.focus();
	}
	return (allValid);
}

function ValidateEmailField(theForm) {

	if (theForm.Email.value == "") {
		alert("Please enter a value for the \"Email Address\" field.");
		theForm.Email.focus();
		return (false);
	}

	if (theForm.Email.value.length < 5) {
		alert("Please enter at least 5 characters in the \"Email Address\" field.");
		theForm.Email.focus();
		return (false);
	}

	var checkOK = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@.-_";
	var checkStr = theForm.Email.value;
	var HasAtSign = false;
	var HasDot = false;
	var allValid = true;

	for (i = 0;  i < checkStr.length;  i++) {
		ch = checkStr.charAt(i);
		if (ch == '@' && HasAtSign) {
			HasAtSign = false;
			break;
		}
		if (ch == '@') {
			HasAtSign = true;
		}
		if (ch == '.') {
			HasDot = true;
		}
		for (j = 0;  j < checkOK.length;  j++)
			if (ch == checkOK.charAt(j))
				break;
		if (j == checkOK.length) {
			allValid = false;
			break;
		}
	}
  
	if (allValid && (!HasAtSign || !HasDot)) {
		alert("Please enter at least one \".\" and exactly one \"@\" character in the \"Email Address\" field, (as \"address@domain.com\").");
		theForm.Email.focus();
		return (false);
	}
  
	if (!allValid) {
		alert("Please enter only letter, digit and \"@.-_\" characters in the \"Email Address\" field, (as \"address@domain.com\").");
		theForm.Email.focus();
	}
	return (allValid);
}

function ValidatePhone1Field(theForm) {
  
	if (theForm.Phone1.value == "") {
		alert("Please enter a value for the \"(daytime) phone\" field.");
		theForm.Phone1.focus();
		return (false);
	}

	if (theForm.Phone1.value.length < 12) {
		alert("Please enter at least 10 digits in the \"(daytime) phone\" field, (as \"nnn-nnn-nnnn\").");
		theForm.Phone1.focus();
		return (false);
	}

	var checkOK = "0123456789";
	var checkStr = theForm.Phone1.value;
	var allValid = true;
		
	for (i = 0;  i < checkStr.length;  i++) {
		if (i > 11) break;
		ch = checkStr.charAt(i);
		if ((i == 3 && ch != '-') || (i == 7 && ch != '-')) {
			allValid = false;
			break;
		}
		for (j = 0;  j < checkOK.length;  j++) {
			if ((ch == '-' && i == 3) || (ch == '-' && i == 7))
				break;
			if (ch == checkOK.charAt(j))
				break;
		}		
		if (j == checkOK.length) {
			allValid = false;
			break;
		}
	}
  
	if (!allValid) {
		alert("Please enter at least 10 digits in the \"(daytime) phone\" field, (as \"nnn-nnn-nnnn\").");
		theForm.Phone1.focus();
	}
	return (allValid);
}

function ValidateMembersField(theForm) {
  
	if (theForm.MemberCount.value == "") {
		alert("Please enter a number for the \"number of members\" field.");
		theForm.MemberCount.focus();
		return (false);
	}

	var checkOK = "0123456789,";
	var checkStr = theForm.MemberCount.value;
	var allValid = true;
		
	for (i = 0;  i < checkStr.length;  i++) {
		ch = checkStr.charAt(i);
		for (j = 0;  j < checkOK.length;  j++)
			if (ch == checkOK.charAt(j))
				break;
		if (j == checkOK.length) {
			allValid = false;
			break;
		}
	}
  
	if (!allValid) {
		alert("Please enter digits in the \"number of members\" field.");
		theForm.MemberCount.focus();
	}
	return (allValid);
}

function ValidateSalesAmountField(theForm) {
  
	if (theForm.SalesAmount.value == "") {
		alert("Please enter a number for the \"money to raise\" field.");
		theForm.SalesAmount.focus();
		return (false);
	}

	var checkOK = "0123456789,";
	var checkStr = theForm.SalesAmount.value;
	var allValid = true;
		
	for (i = 0;  i < checkStr.length;  i++) {
		ch = checkStr.charAt(i);
		for (j = 0;  j < checkOK.length;  j++)
			if (ch == checkOK.charAt(j))
				break;
		if (j == checkOK.length) {
			allValid = false;
			break;
		}
	}
  
	if (!allValid) {
		alert("Please enter digits in the \"money to raise\" field.");
		theForm.SalesAmount.focus();
	}
	return (allValid);
}

function ValidateProfitAmountField(theForm) {
  
	if (theForm.ProfitAmount.value == "") {
		alert("Please enter a number for the \"money to raise\" field.");
		theForm.ProfitAmount.focus();
		return (false);
	}

	var checkOK = "0123456789,";
	var checkStr = theForm.ProfitAmount.value;
	var allValid = true;
		
	for (i = 0;  i < checkStr.length;  i++) {
		ch = checkStr.charAt(i);
		for (j = 0;  j < checkOK.length;  j++)
			if (ch == checkOK.charAt(j))
				break;
		if (j == checkOK.length) {
			allValid = false;
			break;
		}
	}
  
	if (!allValid) {
		alert("Please enter digits in the \"money to raise\" field.");
		theForm.ProfitAmount.focus();
	}
	return (allValid);
}

function ValidateSalesStartDateField(theForm) {
  
	if (theForm.SalesStartDate.value == "") {
		alert("Please enter a date for the \"sales start\" field.");
		theForm.SalesStartDate.focus();
		return (false);
	}
	if (!isvaliddate(theForm.SalesStartDate.value)) {
		alert("Sales start date is not a valid date.");
		theForm.SalesStartDate.focus();
		return (false);
	}
	return (true);
}

function ValidatePrizesRequiredField(theForm) {

	var rad_val = "";

	for (var i=0; i < theForm.PrizesRequired.length; i++) {
	   if (theForm.PrizesRequired[i].checked) {
	      rad_val = theForm.PrizesRequired[i].value;
	      }
	}

	if (rad_val == "") {
		alert("Please answer the \"prizes required\" question.");
		theForm.PrizesRequired[1].focus();
		return (false);
	}
	return (true);
}

function ValidatePrimaryProgramField(theForm) {

	var rad_val = "";

	for (var i=0; i < theForm.PrimaryProgram.length; i++) {
	   if (theForm.PrimaryProgram[i].checked) {
	      rad_val = theForm.PrimaryProgram[i].value;
	      }
	}

	if (rad_val == "") {
		alert("Please select a \"primary program\" field.");
		theForm.PrimaryProgram[1].focus();
		return (false);
	}
	return (true);
}

function ValidateProductField(theForm) {

	if (theForm.EZFormType.value != "ALL" && theForm.EZFormType.value != "BROCHURE" && theForm.EZFormType.value != "FROZEN") {
		return (true);
	}

	var nbrItems = 0;
	var nbrAllowed = 0;
	var nbrChecked = 0;

	nbrItems = 3;
	nbrAllowed = 2;

	if (theForm.EZFormType.value == "ALL") {
		nbrItems = 3;
		nbrAllowed = 3;
	}

	if (theForm.EZFormType.value == "BROCHURE") {
		nbrItems = 6;
		nbrAllowed = 2;
	}

	if (theForm.EZFormType.value == "ALL") {
		for (i = 0;  i < nbrItems;  i++) {
			if (theForm.Product[i]) {
				if (theForm.Product[i].selectedIndex > 0) {
					nbrChecked++;
				}
			}
		}
	}
	else {
		for (i = 0;  i < nbrItems;  i++) {
			if (theForm.Product[i]) {
				if (theForm.Product[i].checked) {
					nbrChecked++;
				}
			}
		}
	}

	if (nbrChecked <= 0) {
		alert("Please select one of the \"Product\" options.");
		theForm.Product[0].focus();
		return (false);
	}

	if (nbrChecked > nbrAllowed) {
		alert("Please select a maximum of " + nbrAllowed + " \"Product\" options.");
		theForm.Product[0].focus();
		return (false);
	}

	return (true);
}

function ValidateEZForm(theForm) {

	if (theForm.Contact.value == "") {
		alert("Please enter a value for the \"Contact\" field.");
		theForm.Contact.focus();
		return (false);
	}

	if (theForm.Title.value == "") {
		alert("Please enter a value for the \"Title\" field.");
		theForm.Title.focus();
		return (false);
	}

	if (theForm.OrgName.value == "") {
		alert("Please enter a value for the \"Organization name\" field.");
		theForm.OrgName.focus();
		return (false);
	}

	if (theForm.Address1.value == "") {
		alert("Please enter a value for the \"Address 1\" field.");
		theForm.Address1.focus();
		return (false);
	}

	if (theForm.City.value == "") {
		alert("Please enter a value for the \"City\" field.");
		theForm.City.focus();
		return (false);
	}

	if (theForm.City.value.length < 2) {
		alert("Please enter at least 2 characters in the \"City\" field.");
		theForm.City.focus();
		return (false);
	}

	if (theForm.State.selectedIndex <= 0) {
		alert("Please select one of the \"State\" options.");
		theForm.State.focus();
		return (false);
	}

	if (!ValidateZipField(theForm)) {
		return (false);
	}

	if (!ValidateEmailField(theForm)) {
		return (false);
	}

	if (!ValidatePhone1Field(theForm)) {
		return (false);
	}

	if (!ValidateProductField(theForm)) {
		return (false);
	}

	if (!ValidateMembersField(theForm)) {
		return (false);
	}

	if (!ValidateSalesAmountField(theForm)) {
		return (false);
	}

	if (theForm.StartDate.selectedIndex <= 0) {
		alert("Please select one of the \"Start date\" options.");
		theForm.StartDate.focus();
		return (false);
	}

	if (theForm.Referral.selectedIndex <= 0) {
		alert("Please select one of the \"referral\" options.");
		theForm.Referral.focus();
		return (false);
	}
		
	return (true);
}

function ValidateEZSellingKitForm(theForm) {

	if (!ValidatePrimaryProgramField(theForm)) {
		return (false);
	}

	if (!ValidateMembersField(theForm)) {
		return (false);
	}

	if (!ValidateProfitAmountField(theForm)) {
		return (false);
	}

	if (!ValidateSalesStartDateField(theForm)) {
		return (false);
	}
	
	if (!ValidatePrizesRequiredField(theForm)) {
		return (false);
	}

	if (theForm.Contact.value == "") {
		alert("Please enter a value for the \"Contact\" field.");
		theForm.Contact.focus();
		return (false);
	}

	if (theForm.Title.value == "") {
		alert("Please enter a value for the \"Title\" field.");
		theForm.Title.focus();
		return (false);
	}

	if (theForm.OrgType.selectedIndex <= 0) {
		alert("Please select one of the \"Organization type\" options.");
		theForm.OrgType.focus();
		return (false);
	}

	if (theForm.OrgName.value == "") {
		alert("Please enter a value for the \"Organization name\" field.");
		theForm.OrgName.focus();
		return (false);
	}

	if (theForm.Address1.value == "") {
		alert("Please enter a value for the \"Address 1\" field.");
		theForm.Address1.focus();
		return (false);
	}

	if (theForm.City.value == "") {
		alert("Please enter a value for the \"City\" field.");
		theForm.City.focus();
		return (false);
	}

	if (theForm.City.value.length < 2) {
		alert("Please enter at least 2 characters in the \"City\" field.");
		theForm.City.focus();
		return (false);
	}

	if (theForm.State.selectedIndex <= 0) {
		alert("Please select one of the \"State\" options.");
		theForm.State.focus();
		return (false);
	}

	if (!ValidateZipField(theForm)) {
		return (false);
	}

	if (!ValidateEmailField(theForm)) {
		return (false);
	}

	if (!ValidatePhone1Field(theForm)) {
		return (false);
	}

	if (theForm.Referral.selectedIndex <= 0) {
		alert("Please select one of the \"referral\" options.");
		theForm.Referral.focus();
		return (false);
	}

	return (true);
}
