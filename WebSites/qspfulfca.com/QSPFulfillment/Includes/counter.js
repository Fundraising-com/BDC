function textCounter( field, countfield, maxlimit ) {
	if ( field.value.length > maxlimit )
	{
		field.value = field.value.substring( 0, maxlimit );
		alert( 'Your comment may not exceed ' + maxlimit + ' characters in length.' );
	}
	else
	{
		countfield.value = maxlimit - field.value.length;   //input box
	}
}

function textCounterWrapper(maxlimit) {
	textCounter(document.Form1.elements['comment'], document.Form1.elements['counter'], maxlimit);
}