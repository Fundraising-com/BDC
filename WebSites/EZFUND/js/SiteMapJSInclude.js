<SCRIPT LANGUAGE=javascript>
<!--
function SiteMapOnChange(dropdown)
{
	var selindex = dropdown.selectedIndex
	var baseURL = dropdown.options[selindex].value
	if (baseURL == "" || baseURL == "(n/a)") {
		//alert("You must select a valid option!");
	}
	else {
		top.location.href = baseURL;
	}
    
	return true;
}
//-->
</SCRIPT>
