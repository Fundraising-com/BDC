
function OandaConvert(strName)
{
	if (strName == null)
		return "<a href='javascript: OandaPopUp();'><img src='Images/convert.gif' border=0 AlternateText='Convert'></a>";
	else
		return "<a href='javascript: OandaPopUp();'>" + strName + "</a>";
}

function OandaPopUp()
{
	var sFeatures, h, w, i
	h = window.screen.availHeight 
	w = window.screen.availWidth 
	sFeatures = "height=" + h*.25 + ",width=" + h*.50 + ",screenY=" + (h*.30) + ",screenX=" + (w*.33) + ",top=" + (h*.30) + ",left=" + (w*.33) + ",resizable,scrollbars=yes"
	window.open("Oanda.aspx","",sFeatures)
}

function OandaHideMessage()
{
	var isNav4 = document.layers;
	if (isNav4)
	{
		//Netscape
		document.layers['Message'].visibility = "hide";
	}
	else
	{
		//IE
		document.getElementById('Message').style.visibility = "hidden";
	}
}

function OandaMessage()
{
	var isNav4 = document.layers;
	if (isNav4)
	{
		//Netscape
		document.layers['Message'].visibility = "visible";
	}
	else
	{
		//IE
		document.getElementById('Message').style.visibility = "visible";
	}
}
