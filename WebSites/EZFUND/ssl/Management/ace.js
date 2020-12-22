//*********************************	
var oSelSave;
var sSelType; 
function saveSelection()
	{
	//idContent.focus() //or <BODY onselectstart...>
	oSelSave = idContent.document.selection.createRange()
	sSelType = idContent.document.selection.type
	}
function saveToHiddenField() {
	Form1.hdnText.value = idContent.document.body.innerHTML
	//alert("hdnText:" + Form1.hdnText.value);
	}
function restoreSelection()
	{
	oSelSave.select()
	}
//*********************************

function drawIcon(left,width,id,title,command)
	{
	document.write("<span style=\"width: "+width+";height:22;\">")
	document.write("	<span unselectable='on' style=\"position:absolute;clip: rect(0 "+width+" 22 0)\">")	
	document.write("	<img unselectable='on' id=\""+id+"\" ") 
	document.write("	title=\""+title+"\" onclick=\""+command+"\" src=\"images/ace.gif\" ")
	document.write("		style=\"position:absolute;top:0;left:-"+left+";\" ")
	document.write("		onmouseover=\"if(this.en==false)return;this.style.pixelTop=-22\" ")
	document.write("		onmousedown=\"if(this.en==false)return;this.style.pixelTop=-44\" ")
	document.write("		onmouseup=\"if(this.en==false)return;this.style.pixelTop=-22\" ")
	document.write("		onmouseout=\"if(this.en==false)return;this.style.pixelTop=0\" ")
	document.write("		width=\"1151\" ")
	document.write("		height=\"110\">")
	document.write("		<table width="+width+"><tr><td unselectable='on' onselectstart='return false' style=\"position:absolute;top:4;left:7;\" id=\""+id+"Text\"></td></tr></table>")
	document.write("	</span>")	
	document.write("</span>")
	} 

function initEditor()
	{	
	var sHTML = "" /* "<link rel=\"stylesheet\" href=\"ace.css\" type=\"text/css\">" +
				"<style>" +
				"	body {border: 1px lightgrey solid;}" +
				"</style>" +
				"<body oncontextmenu=\"return false\">" +
				"	<div></div>" +
				"</body>" */
		idContent.document.designMode = "on"
		idContent.document.open("text/html","replace")
		idContent.document.write(sHTML)
		idContent.document.close()

		idContent.document.execCommand("2D-Position", true, true);
		idContent.document.execCommand("MultipleSelection", true, true);
		idContent.document.execCommand("LiveResize", true, true);

		//idContent.document.body.leftmargin=0
			
			/*
			//for(i=0;i<document.all.length;i++) 
			//	{
			//	if(document.all(i).unselectable != "off") 
			//		{
			//		document.all(i).unselectable = "on";
			//		}
			//	}	
			//idContent.unselectable = "off";		
			*/
			LoadDoc()

			idWord.document.designMode = "on"
				var sATag = idContent.document.body.all.tags("A");
				for (var i = sATag.length - 1; i >= 0; i--) 
					{
					oA = sATag[i];
					//oA.href = (oA.href).replace(sBaseUrl,sBaseUrlNew);
					//oA.href = vbReplace(oA.href,sBaseUrl,sBaseUrlNew); //11/13/02
					var iLen = oA.href.indexOf("[");
						var iEnd = oA.href.indexOf("]") + 1;
						var NewLink;
						if(iLen > 0 && iEnd > 0)
						{
						NewLink =  oA.href.substr(iLen , iEnd - iLen +1 )
							
						oA.href =NewLink;
						}
					
					}
					
				var sImgTag = idContent.document.body.all.tags("IMG");
				for (var i = sImgTag.length - 1; i >= 0; i--) 
					{
					oA = sImgTag[i];
					//oA.src = (oA.src).replace(sBaseUrl,sBaseUrlNew);
					oA.src = vbReplace(oA.src,sBaseUrl,sBaseUrlNew); // 11/13/02
					}
			//btnSave.disabled = false
			saveToHiddenField()	

	}
function doCmd(sCmd,sOption)
	{
	popupHide()
	idContent.focus()

	var oSel = idContent.document.selection.createRange()
	var sType = idContent.document.selection.type	
	var oTarget = (sType == "None" ? idContent.document : oSel)
	oSel.select()//tambahan

	if (sCmd=="DialogImage") 
		{
		var popleft=((document.body.clientWidth - 440) / 2)+window.screenLeft; 
		var poptop=(((document.body.clientHeight - 460) / 2))+window.screenTop-40;		
		window.open("default_image.aspx","NewWindow","scrollbars=NO,width=480,height=520,left="+popleft+",top="+poptop)
		return true;
		}

	oTarget.execCommand(sCmd, false, sOption)
	saveToHiddenField()
	}

	
function doCmd2(sCmd,sOption)
	{
	popupHide()
	
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	
	
	var oTarget = (sType == "None" ? idContent.document : oSel)
	oSel.select()//tambahan

	oTarget.execCommand(sCmd, false, sOption)
	saveToHiddenField()
	}

function UpdateImage(inpImgURL,inpImgAlt,inpImgAlign,inpImgBorder,inpImgWidth,inpImgHeight,inpHSpace,inpVSpace)
	{
	var oSel	= idContent.document.selection.createRange()
	var sType = idContent.document.selection.type	
	if ((oSel.item) && (oSel.item(0).tagName=="IMG")) 
		{
		oSel.item(0).style.width="";
		oSel.item(0).style.height="";
				
		oSel.item(0).src = inpImgURL
		if(inpImgAlt!="")
			oSel.item(0).alt = inpImgAlt
		else
			oSel.item(0).removeAttribute("alt",0)
		oSel.item(0).align = inpImgAlign
		oSel.item(0).border = inpImgBorder
		if(inpImgWidth!="")
			oSel.item(0).width = inpImgWidth
		else
			oSel.item(0).removeAttribute("width",0)			
		if(inpImgHeight!="")
			oSel.item(0).height = inpImgHeight
		else
			oSel.item(0).removeAttribute("height",0)			
		if(inpHSpace!="")
			oSel.item(0).hspace = inpHSpace
		else
			oSel.item(0).removeAttribute("hspace",0)			
		if(inpVSpace!="")
			oSel.item(0).vspace = inpVSpace
		else
			oSel.item(0).removeAttribute("vspace",0)			
		}	
	}
function InsertImage(inpImgURL,inpImgAlt,inpImgAlign,inpImgBorder,inpImgWidth,inpImgHeight,inpHSpace,inpVSpace)
	{
	doCmd("InsertImage",inpImgURL);
	oSel = idContent.document.selection.createRange()
	sType = idContent.document.selection.type	
	if ((oSel.item) && (oSel.item(0).tagName=="IMG")) 
		{
		if(inpImgAlt!="")
			oSel.item(0).alt = inpImgAlt
		oSel.item(0).align = inpImgAlign
		oSel.item(0).border = inpImgBorder
		if(inpImgWidth!="")
			oSel.item(0).width = inpImgWidth 
		if(inpImgHeight!="")
			oSel.item(0).height = inpImgHeight
		if(inpHSpace!="")
			oSel.item(0).hspace = inpHSpace
		if(inpVSpace!="")
			oSel.item(0).vspace = inpVSpace
		}	
	}

function popupShow(width,title,html)
	{
	popupHide();
	//construct & display popup
	var sHTML = "<link rel=\"stylesheet\" href=\"ace.css\" type=\"text/css\">" +
				"<style>" +
				"	select{height: 22px; top:2;	font:8pt verdana,arial,sans-serif}" +	
				"	body {border: 0px lightgrey solid;}" +
				"	.bar{	border-top: #99ccff 1px solid; background: #316AC5; WIDTH: 100%; border-bottom: #000000 1px solid;height: 20px}" +
				"	.cellSymbol{border:solid black 1.0pt; border-left:none; padding:0in 2pt 0in 2pt; font-size:10pt; font-family:'Tahoma';}" +
				"	td {	font:8pt verdana,arial,sans-serif}" +
				"	div	{	font:10pt tahoma,arial,sans-serif}" +
				"</style>" +
				"<body style=\"overflow:hidden\" oncontextmenu=\"return false\" topmargin=0 leftmargin=0 rightmargin=0 bottommargin=0 ONSELECTSTART=\"return event.srcElement.tagName=='INPUT'\">" +
				"<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"table-layout: fixed\" bgcolor=Gainsboro ID=\"tblPopup\">" +
				"<col width="+width+"><col width=13>" +
				"<tr>" +
				"<td>" +
				"	<div class=\"bar\" style=\"padding-left: 5px;\">" +
				"	<font size=2 face=tahoma color=white><b>"+title+"</b></font>" +
				"	</div>" +
				"</td>" +
				"<td style=\"cursor:hand\" onclick=\"parent.popupHide()\">" +
				"	<div class=\"bar\">" +
				"	<font size=2 face=tahoma color=white><b>X</b></font>" +
				"	</div>" +
				"</td>" +
				"</tr>" +
				"<tr>" +
				"<td colspan=2 style=\"border-left: #336699 1px solid;border-right: #336699 1px solid;border-bottom: #336699 1px solid;\" valign=top>" +
				"	<br>" +
				"	<div id=\"divPopup\" align=center></div>" +
				"	<br>" +
				"</td>" +
				"</tr>" +
				"</table>" +
				"</body>"
		
	idContentTmp.document.open("text/html","replace")
	idContentTmp.document.write(sHTML)
	idContentTmp.document.close()
	document.all.idContentTmp.style.zIndex = 2
	document.all.idContentTmp.style.visibility = ""
	
	idContentTmp.document.all.tblPopup.focus()//for idContent lost focus

	//fill popup content
	idContentTmp.divPopup.innerHTML = html
	
	//set dimension
	document.all.idContentTmp.style.width = idContentTmp.document.all.tblPopup.offsetWidth
	document.all.idContentTmp.style.height = idContentTmp.document.all.tblPopup.offsetHeight

	//set position
	if(document.body.clientHeight - document.all.idContentTmp.offsetHeight > 0)
		document.all.idContentTmp.style.pixelTop=((document.body.clientHeight - document.all.idContentTmp.offsetHeight) / 2);
	else
		document.all.idContentTmp.style.pixelTop=0
	if(document.body.clientWidth - document.all.idContentTmp.offsetWidth > 0)
		document.all.idContentTmp.style.pixelLeft=(document.body.clientWidth - document.all.idContentTmp.offsetWidth) / 2 ;
	else
		document.all.idContentTmp.style.pixelLeft=0

	/*
	alert(idContentTmp.document.all.length)
	for (i=0; i<idContentTmp.document.all.length; i++)
		{
		if(idContentTmp.document.all(i).unselectable != "on")
		idContentTmp.document.all(i).unselectable = "on";		
		}
	*/
	}
	
function popupHide()
	{
	document.all.idContentTmp.style.visibility = "hidden"
	saveToHiddenField()
	}
	
function displayParagraphBox() //OK
	{
	saveSelection()
	
	var sHTML = "<table align=center border=0 width=100% height=100% cellpadding=0 cellspacing=0>" +
				"<tr onclick=\"parent.doCmd2('FormatBlock','<H1>');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center height=10><h1>Header 1</h1></td></tr>" +
				"<tr onclick=\"parent.doCmd2('FormatBlock','<H2>');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><h2>Header 2</h2></td></tr>" +
				"<tr onclick=\"parent.doCmd2('FormatBlock','<H3>');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><h3>Header 3</h3></td></tr>" +
				"<tr onclick=\"parent.doCmd2('FormatBlock','<H4>');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><h4>Header 4</h4></td></tr>" +
				"<tr onclick=\"parent.doCmd2('FormatBlock','<H5>');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><h5>Header 5</h5></td></tr>" +
				"<tr onclick=\"parent.doCmd2('FormatBlock','<H6>');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><h6>Header 6</h6></td></tr>" +
				"<tr onclick=\"parent.doCmd2('FormatBlock','<PRE>');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><pre>Preformatted</pre></td></tr>" +
				"<tr onclick=\"parent.doCmd2('FormatBlock','<P>');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center height=10><p>Normal</p></td></tr>" +
				"</table>"	
	popupShow(253,"Paragraph Style",sHTML)
	}
function displayFontNameBox() //OK
	{
	saveSelection()
	
	var sHTML = "<table align=center border=0 width=100% height=100% cellpadding=0 cellspacing=2>" +
				"<tr onclick=\"parent.doCmd2('fontname','Arial');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"Arial\" size=1>Arial</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontname','Arial Black');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"Arial Black\" size=1>Arial Black</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontname','Arial Narrow');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"Arial Narrow\" size=1>Arial Narrow</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontname','Comic Sans MS');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"Comic Sans MS\" size=1>Comic Sans MS</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontname','Courier New');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"Courier New\" size=1>Courier New</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontname','System');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"System\" size=1>System</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontname','Tahoma');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"Tahoma\" size=1>Tahoma</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontname','Times New Roman');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"Times New Roman\" size=1>Times New Roman</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontname','Verdana');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"Verdana\" size=1>Verdana</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontname','Wingdings');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font face=\"Wingdings\" size=1>Wingdings</font></td></tr>" +
				"</table>"
	popupShow(213,"Font Name",sHTML)
	}
	
function editEmail()
{
	saveSelection()
		
	var sHTML="";
	var s1;
	var s2;
	var rows;
	sHTML += "<table border=1 cellspacing=0 cellpadding=0 style=\"border-collapse:collapse;border:none;border-left:'solid windowtext 1.0pt'\">"
	if (sEMailType == "0") 
	{
		// email a friend
		s1 = new Array("RecipientName", "SenderName", "ProductLink", "ProductName", "ProductDescription", "ProductImage", "PersonalMessage", "StoreName", "StoreURL")
		s2 = new Array("Recipient Name", "Sender Name", "Product Link", "Product Name", "Product Description", "Product Image", "Personal Message", "Store Name", "Store URL")
		rows = 9;
	}
	else if (sEMailType == "1")
	{	
		// password reminder
		s1 = new Array("RecipientFirstName", "RecipientLastName", "RecipientEmailAddress","Password", "StoreName", "StoreURL")
		s2 = new Array("Recipient First Name", "Recipient Last Name", "Recipient E-MailAddress", "Password", "Store Name", "Store URL")
		rows = 6;
	}
	else if (sEMailType == "2")
	{
		// wishlist
		s1 = new Array("RecipientName","SenderName","WishList", "PersonalMessage","StoreName","StoreURL")
		s2 = new Array("Recipient Name","Sender Name","Wish List", "Personal Message","Store Name","Store URL")
		rows = 6;
	}
	else if (sEMailType == "3")
	{
		// wishlist components
		s1 = new Array("ProductID","ProductName","ProductDescription", "ProductAttributes","Price","SalePrice", "ProductLink")
		s2 = new Array("Product ID","Product Name","Product Description", "Product Attributes","Price","Sale Price", "Link to Product")
		rows = 7;
	}
	else if (sEMailType == "4")
	{
		// customer email
		s1 = new Array("CustomerFirstName", "CustomerLastName", "OrderID","OrderTotal","BillingInfo","ProductsShippingInfo","OrderDetailsLink","StoreName","StoreURL")
		s2 = new Array("Customer First Name", "Customer Last Name", "Order ID","Order Total","Billing Information","Products Shipping Information","Order Details Link","Store Name","Store URL")
		rows = 9;
	}
	else if (sEMailType == "5")
	{
		// vendor email
		s1 = new Array("OrderID","OrderTotal","BillingInfo","ProductsShippingInfo","StoreName","StoreURL")
		s2 = new Array("Order ID","Order Total","Billing Information","Products Shipping Information","Store Name","Store URL")
		rows = 6;
	}
	else if (sEMailType == "6")
	{
		// merchant email	
		s1 = new Array("OrderID","OrderTotal","BillingInfo","ProductsShippingInfo","OrderDetailsLink","StoreName","StoreURL")
		s2 = new Array("Order ID","Order Total","Billing Information","Products Shipping Information","Order Details Link", "Store Name","Store URL")
		rows = 6;
	}
	else if (sEMailType == "7")
	{
		// products
		s1 = new Array("ProductID","ProductName","ProductAttributes","ProductPrice","ProductQuantity")
		s2 = new Array("Product ID","Product Name","Product Attributes","Product Price","Product Quantity")
		rows = 5;
	}
	else if (sEMailType == "8")
	{
		// billing
		s1 = new Array("BillingName","BillingCompany","BillingAddress1","BillingAddress2","BillingCity","BillingState","BillingZip","BillingCountry","BillingPhone","BillingFax","BillingEMail","BillingPaymentMethod")
		s2 = new Array("Billing Name","Billing Company","Billing Address 1","Billing Address 2","Billing City","Billing State","Billing Zip","Billing Country","Billing Phone","Billing Fax","Billing EMail","Billing Payment Method")
		rows = 12;
	}
	else if (sEMailType == "9")
	{
		// shipping
		s1 = new Array("ShippingName","ShippingCompany","ShippingAddress1","ShippingAddress2","ShippingCity","ShippingState","ShippingZip","ShippingCountry","ShippingPhone","ShippingFax","ShippingEMail","ShippingMethod","ShippingProducts", "ShippingInstructions")
		s2 = new Array("Shipping Name","Shipping Company","Shipping Address 1","Shipping Address 2","Shipping City","Shipping State","Shipping Zip","Shipping Country","Shipping Phone","Shipping Fax","Shipping EMail","Shipping Method", "Shipping Products", "Special Instructions")
		rows = 14;
	}
	else if (sEMailType == "10")
	{
		// confirm order total
		s1 = new Array("OrderMerchandiseTotal", "OrderDiscounts", "OrderSubtotal", "OrderLocalTax", "OrderStateProvinceTax", "OrderCountryTax", "OrderShipping", "OrderHandling", "OrderTotal", "OrderGiftCertificate", "OrderGrandTotal")
		s2 = new Array("Order Merchandise Total", "Order Discounts", "Order Subtotal", "Order Local Tax", "Order State Province Tax", "Order Country Tax", "Order Shipping", "Order Handling", "Order Total", "Order Gift Certificate", "Order Grand Total")
	rows = 11;	
	}
	else if (sEMailType == "11")
	{
		// low stock 
		s1 = new Array("ProductID", "ProductCode", "ProductName" , "ProductInventoryCount" , "ManufacturerName", "VendorName", "ProductLink", "StoreName", "StoreURL")
		s2 = new Array("Prodcut ID" , "Product Code", "Product Name", "Product Inventory Count" , "Manufacturer Name" , "Vendor Name", "Product Link", "Store Name", "Store URL")
		rows = 9;
	}
	else if (sEMailType == "PromoMail")
		{
		// Promo Mail
		s1 = new Array("CustomerFirstName", "CustomerLastName", "CustomerEMail", "OrderedProducts", "StoreName", "StoreURL", "UnsubscribeURL")
		s2 = new Array("Customer First Name", "Customer Last Name", "Customer E-Mail", "Ordered Products", "Store Name", "Store URL", "Unsubscribe URL")
		rows = 7;
	}
	
	for(var i=0;i<rows;i++)
	{
		sHTML += "<tr>";		
		sHTML += "<td class=cellSymbol align=center onclick=\"parent.insertSymbol(\'[" + s1[i] + "]\');\" style=\"cursor:hand\">" + s2[i] + "</td>"				
		sHTML += "</tr>";		
	}
	sHTML += "</table>"	
	//alert(sHTML);
	popupShow(560,"Edit E-Mail",sHTML)
}	


function displayFontSizeBox() //OK
	{
	saveSelection()
	
	var sHTML = "<table align=center border=0 width=100% height=100% cellpadding=0 cellspacing=0>" +
				"<tr onclick=\"parent.doCmd2('fontsize',1);\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font size=1>Size 1</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontsize',2);\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font size=2>Size 2</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontsize',3);\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font size=3>Size 3</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontsize',4);\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font size=4>Size 4</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontsize',5);\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font size=5>Size 5</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontsize',6);\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font size=6>Size 6</font></td></tr>" +
				"<tr onclick=\"parent.doCmd2('fontsize',7);\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\"><td align=center><font size=7>Size 7</font></td></tr>" +
				"</table>"
	popupShow(253,"Font Size",sHTML)
	}

function displayFgColorBox() //OK
	{
	saveSelection()
	
	var sHTML = writeColorPicker("idForeColor","parent.doCmd2('ForeColor',idForeColor.value);",0);
	popupShow(433,"Foreground Color",sHTML)
	}
function displayBox() //OK
	{
	saveSelection()
	
	var sHTML = writeColorPicker("idBackColor","parent.doCmd2('BackColor',idBackColor.value);",0);
	popupShow(433,"Background Color",sHTML)
	
	}
function displayDocBgColorBox()
	{
	saveSelection()
	
	var sHTML = writeColorPicker("idDocBackColor","parent.idContent.document.body.style.background=idDocBackColor.value;parent.popupHide()",0);
	popupShow(433,"Document Background Color",sHTML)

	}
function writeColorPicker(Name,Command,Type) //OK
	{
	var OutputID = Name
	var TextID = Name + "_1"
	var ColorID = Name + "_2"
	var c1 = new Array("FFFFCC","FFCC66","FF9900","FFCC99","FF6633","FFCCCC","CC9999","FF6699","FF99CC","FF66CC","FFCCFF","CC99CC","CC66FF","CC99FF","9966CC","CCCCFF","9999CC","3333FF","6699FF","0066FF","99CCFF","66CCFF","99CCCC","CCFFFF","99FFCC","66CC99","66FF99","99FF99","CCFFCC","33FF33","66FF00","CCFF99","99FF00","CCFF66","CCCC66","FFFFFF")
	var c2 = new Array("FFFF99","FFCC00","FF9933","FF9966","CC3300","FF9999","CC6666","FF3366","FF3399","FF00CC","FF99FF","CC66CC","CC33FF","9933CC","9966FF","9999FF","6666FF","3300FF","3366FF","0066CC","3399FF","33CCFF","66CCCC","99FFFF","66FFCC","33CC99","33FF99","66FF66","99CC99","00FF33","66FF33","99FF66","99FF33","CCFF00","CCCC33","CCCCCC")
	var c3 = new Array("FFFF66","FFCC33","CC9966","FF6600","FF3300","FF6666","CC3333","FF0066","FF0099","FF33CC","FF66FF","CC00CC","CC00FF","9933FF","6600CC","6633FF","6666CC","3300CC","0000FF","3366CC","0099FF","00CCFF","339999","66FFFF","33FFCC","00CC99","00FF99","33FF66","66CC66","00FF00","33FF00","66CC00","99CC66","CCFF33","999966","999999")
	var c4 = new Array("FFFF33","CC9900","CC6600","CC6633","FF0000","FF3333","993333","CC3366","CC0066","CC6699","FF33FF","CC33CC","9900CC","9900FF","6633CC","6600FF","666699","3333CC","0000CC","0033FF","6699CC","3399CC","669999","33FFFF","00FFCC","339966","33CC66","00FF66","669966","00CC00","33CC00","66CC33","99CC00","CCCC99","999933","666666")
	var c5 = new Array("FFFF00","CC9933","996633","993300","CC0000","FF0033","990033","996666","993366","CC0099","FF00FF","990099","996699","660099","663399","330099","333399","000099","0033CC","003399","336699","0099CC","006666","00FFFF","33CCCC","009966","00CC66","339933","336633","33CC33","339900","669933","99CC33","666633","999900","333333")
	var c6 = new Array("CCCC00","996600","663300","660000","990000","CC0033","330000","663333","660033","990066","CC3399","993399","660066","663366","330033","330066","333366","000066","000033","003366","006699","003333","336666","00CCCC","009999","006633","009933","006600","003300","00CC33","009900","336600","669900","333300","666600","000000")
	
	
	var sHTML = "";
	sHTML += ("<table border=0 width=370 align=center cellpadding=0 cellspacing=0><tr><td width=45>  <table border=1 cellpadding=0 cellspacing=0><tr><td id='"+ColorID+"' width=25 height=25></td></tr></table>   <div id='"+TextID+"'><font size=1>&nbsp;</font></div> </td><td>&nbsp;</td><td>")
	sHTML += ("<table cellpadding=0 cellspacing=1 bgcolor=black>")
	if(Type==0)
		{
		for(var i=1;i<=6;i++)
			{
			sHTML += ("<tr>")
			for(var r=0;r<eval("c"+i).length;r++)
				{
				var colour = eval("c"+i+"[r]")
				sHTML += ("<td onclick=\""+OutputID+".value='"+colour+"';"+Command+"\" style=\"cursor:hand;background-color:"+colour+";\" width=9 height=6 onmouseover=\""+ColorID+".style.background='#"+colour+"';"+TextID+".innerHTML='<font size=1>"+colour+"</font>'\"></td>")
				}	
			sHTML += ("</tr>")
			}
		}
	if(Type==1)
		{
		for(var i=1;i<=6;i++)
			{
			sHTML += ("<tr>")
			for(var r=0;r<eval("c"+i).length;r++)
				{
				var colour = eval("c"+i+"[r]")
				sHTML += ("<td onclick=\""+ColorID+".style.background='#"+colour+"';"+TextID+".innerHTML='<font size=1>"+colour+"</font>';"+OutputID+".value='"+colour+"';"+Command+"\" style=\"cursor:hand;background-color:"+colour+";\" width=9 height=6 onmouseover=\"\"></td>")
				}	
			sHTML += ("</tr>")
			}
		}
	sHTML += ("</table>")
	sHTML += ("<input type=hidden id='"+OutputID+"'>")
	sHTML += ("</td></tr></table>")	
	return sHTML;		

	}



/*
1. link bila diinsertkan yg ada link yg lain/proses copy paste maka ada http://localhost/...... (
ie5 replace => pake vbscript
ie55 replace => pake javascript replace 
replace case-sensitive) => sudah di-apply LCase & toLowerCase
*/
var displayMode = "RICH"
function setDisplayMode()// OK
	{
	popupHide()
	if(displayMode=='RICH')
		{
		idContent.document.body.clearAttributes;
		idContent.document.body.style.fontFamily = 'Courier New';
		idContent.document.body.style.fontSize = '12px';
		idContent.document.body.innerText = idContent.document.body.innerHTML;
		//idContent.document.body.innerHTML = "<DIV>" + idContent.document.body.innerHTML + "</DIV>"
		displayMode = 'HTML';
		idToolbar.style.display = "none"
		} 
	else 
		{
		idContent.document.body.clearAttributes;
		idContent.document.body.style.fontFamily = '';
		idContent.document.body.style.fontSize = '';
		idContent.document.body.innerHTML = idContent.document.body.innerText;
		var sBaseUrl=document.Form1.hdnBaseURL.value;
		var sBaseUrlNew = document.Form1.hdnBaseURLNew.value;
		var sATag = idContent.document.body.all.tags("A");
		for (var i = sATag.length - 1; i >= 0; i--) 
			{
			oA = sATag[i];
			//oA.href = (oA.href).replace(sBaseUrl,sBaseUrlNew);
			//oA.href = vbReplace(oA.href,sBaseUrl,sBaseUrlNew); 11/13
			var iLen = oA.href.indexOf("[");
			var iEnd = oA.href.indexOf("]") + 1;
			var NewLink;
			if(iLen > 0 && iEnd > 0)
			  {
			  NewLink =  oA.href.substr(iLen , iEnd - iLen +1 )
				
			   oA.href =NewLink;
			  }
					
					
			}
			
		var sImgTag = idContent.document.body.all.tags("IMG");
		for (var i = sImgTag.length - 1; i >= 0; i--) 
			{
			oA = sImgTag[i];
			//oA.src = (oA.src).replace(sBaseUrl,sBaseUrlNew);
			
			oA.src = vbReplace(oA.src,sBaseUrl,sBaseUrlNew)
			}
		
		displayMode = 'RICH';
		idToolbar.style.display = "block"
		}
	}


function applyStyle(Style)// OK
	{
	popupHide()
	//get selection
	//idContent.focus() // => popupHide()
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	

	//if (sType!="Control"&&sType!="None")
	if(oSel.parentElement != null)//yg dipilih text bukan control & ada isinya
		{
		oSel.select()//kalo tdk ada => Error : Incompatible markup pointers for this operation.
		var sApplied = "<font class='"+Style+"'>"+oSel.text+"</font>"
		oSel.pasteHTML(sApplied)
		}
	}
function displayStyleBox() //OK
	{
	saveSelection()
	
	var val;
	var sHTML = "<table align=center border=0 width=100% height=100% cellpadding=0 cellspacing=0>";
	for(var i=0;i<document.styleSheets(0).rules.length;i++)
		{
		h=document.styleSheets(0).rules.item(i).selectorText
		var selArr = h.split(".")
		var itemCount = selArr.length
		//alert(h + " " + itemCount)
		if(itemCount>1)
			{
			val = selArr[1]
			}
		else
			{
			val = h
			}
		sHTML += "<tr onclick=\"parent.applyStyle('"+val+"');\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:'hand'\"><td align=\"center\"><span class=\""+val+"\">" + val + "</span></td></tr>";
		}	
	sHTML += "</table>"
	popupShow(230,"Select Style (from CSS)",sHTML)
	}	
	
function insertSymbol(Symbol)// OK
	{
	popupHide()
	//idContent.focus() // => popupHide()
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType   =  sSelType//idContent.document.selection.type	
	oSel.select()
	if(oSel.parentElement)
		oSel.pasteHTML(Symbol)
	saveToHiddenField()
	}
	
function displaySymbolBox() //OK
	{
	saveSelection()
	
	var s1 = new Array("&nbsp;","!","&quot;","#","$","%","&amp;","'","(",")","*","+",",","-",".","/","0","1","2","3","4","5","6","7","8","9",":",";","&lt;","=","&gt;","?")
	var s2 = new Array("@","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","[","\\","]","^","_")
	var s3 = new Array("`","a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","{","|","}","~","&#9633;")
	var s4 = new Array("€","&#9633;","‚","ƒ","„","…","†","‡","ˆ","‰","Š","‹","Œ","&#9633;","Ž","&#9633;","&#9633;","&#8242;","&#8242;","&#8243;","&#8243;","•","–","—","˜","™","š","›","œ","&#9633;","ž","Ÿ")
	var s5 = new Array("&nbsp;","¡","¢","£","¤","¥","¦","§","¨","©","ª","«","¬","­","®","¯","°","±","²","³","´","µ","¶","·","¸","¹","º","»","¼","½","¾","¿")
	var s6 = new Array("À","Á","Â","Ã","Ä","Å","Æ","Ç","È","É","Ê","Ë","Ì","Í","Î","Ï","Ð","Ñ","Ò","Ó","Ô","Õ","Ö","×","Ø","Ù","Ú","Û","Ü","Ý","Þ","ß")
	var s7 = new Array("à","á","â","ã","ä","å","æ","ç","è","é","ê","ë","ì","í","î","ï","ð","ñ","ò","ó","ô","õ","ö","÷","ø","ù","ú","û","ü","ý","þ","ÿ")
	var sHTML="";
	sHTML += "<table border=1 cellspacing=0 cellpadding=0 style=\"border-collapse:collapse;border:none;border-left:'solid windowtext 1.0pt'\">"
	for(var i=1;i<=7;i++)
		{
		sHTML += "<tr>"
		for(var j=0;j<32;j++)
			{
			//onmouseover menyebabkan pembatas hilang di IE5.0
			//sHTML += "<td class=cellSymbol align=center onclick=\"parent.insertSymbol(this.innerText);\" onmouseover=\"this.style.color='tomato'\" onmouseout=\"this.style.color='black'\" style=\"cursor:hand\">" + eval("s"+i)[j] + "</td>"
			sHTML += "<td class=cellSymbol align=center onclick=\"parent.insertSymbol(this.innerText);\" style=\"cursor:hand\">" + eval("s"+i)[j] + "</td>"
			}
		sHTML += "</tr>"
		}
	sHTML += "</table>"
	popupShow(560,"Select Symbol",sHTML)
	}

function CreateHyperlink()
	{
	//Get URL typed by user
	var inpURL = idContentTmp.idLinkURL.value 
	var inpURLType = idContentTmp.idLinkType.value 
	var sURL = inpURLType + inpURL
	
	//Use the previous active selection
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	

	/***********************************************************************/
	/*				idLinkImageBorder
	/***********************************************************************/
	if ((oSel.item) && (oSel.item(0).tagName=="IMG")) //If image is selected
		{
		oSel.item(0).width = oSel.item(0).offsetWidth //kasih attribute width
		oSel.item(0).height = oSel.item(0).offsetHeight //kasih attribute height
		oSel.item(0).border = idContentTmp.idLinkImageBorder.value 
		}

	if(inpURL!="")//If there is URL typed by user
		{
		if (oSel.parentElement) //If text selection
			{
			if(idContentTmp.btnLinkAction.value == "Insert")
				{
				if(oSel.text!="")
					{
					//noop
					}
				else
					{
					var oSelTmp = oSel.duplicate()
					oSel.text = sURL	//displayed text = sURL
					oSel.setEndPoint("StartToStart",oSelTmp)
					oSel.select()
					sType="Text"
					}
				}
			if(idContentTmp.btnLinkAction.value == "Update") //pasti ada A element
				{
				if(oSel.text!="")
					{
					//noop
					//oEl = GetElement(oSel.parentElement(),"A")
					//oEl.innerText = sURL
					}
				else
					{
					//noop
					//oEl = GetElement(oSel.parentElement(),"A")
					//oEl.innerText = sURL
					}
				}
			}

		/***********************************************************************/
		/*				idLinkType & idLinkURL (sURL)
		/***********************************************************************/
		oSel.execCommand("CreateLink",false,sURL)
	
		//After A element created, then add the Target
		//oSel = document.selection.createRange()
		if (oSel.parentElement)
			{
			oEl = GetElement(oSel.parentElement(),"A")
			}
		else 
			{
			oEl = GetElement(oSel.item(0),"A")
			}
		if(oEl)
			{
			if(idContentTmp.idLinkTarget.value=="")
				{
				oEl.removeAttribute("target",0)
				}
			else
				{
				oEl.target = idContentTmp.idLinkTarget.value;
				}
			}
		idContent.focus()	
		oSel.select()//tambahan			
		}
	//Activate layer again
	//if(isLayerActive)oElStore.setActive();
	}
var oElStore;
var isLayerActive;
function displayLinkBox() //OK
	{
	saveSelection()
	/*
	idContentTmp.focus() jalan (benar), block pd idContent hilang, & idContentTmp selectable (benar), idContent focus popuphide jalan (benar).
	idContentTmp.focus() tdk jalan, block pd idContent tetap ada (benar), & idContentTmp unselectable, idContent focus popuphide tdk jalan.
	*/
	var sHTML =		"<table cellpadding=\"0\" cellspacing=\"3\" align=center>" +
					"<tr>" +
					"<td colspan=2>" +
					"		&nbsp;" +
					"		<select id=\"idLinkType\" NAME=\"idLinkType\">" +
					"		<option value=\"\" selected></option>" +
					"		<option value=\"http://\" selected>http://</option>" +
					"		<option value=\"https://\">https://</option>" +
					"		<option value=\"mailto:\">mailto:</option>" +
					"		<option value=\"ftp://\">ftp://</option>" +
					"		<option value=\"news:\">news:</option>" +
					"		</select>" +
					"		<input type=text size=30 id=idLinkURL value=\"\" style=\"height: 19px;font:8pt verdana,arial,sans-serif\" NAME=\"idLinkURL\">" +
					"</td>" +
					"</tr>" +		
					"<tr>" +
					"<td>" +
					"		&nbsp;&nbsp;Target : " +
					"		<select id=\"idLinkTarget\" NAME=\"idLinkTarget\">" +
					"		<option value=\"\" selected></option>" +
						//	<option value=\"_self\" selected>self</option>" +
						//	<option value=\"_parent\">parent</option>" +
					"		<option value=\"_blank\">blank</option>" +
					"		</select>" +
					"		<div id=\"idLinkImage\" style=\"display:none\">" +
					"</td>" +
					"<td>" +
					"		&nbsp;&nbsp;Image Border : " +
					"		<select id=\"idLinkImageBorder\" NAME=\"idLinkImageBorder\">" +
					"		<option value=\"0\" selected>0</option>" +
					"		<option value=\"1\">1</option>" +
					"		<option value=\"2\">2</option>" +
					"		<option value=\"3\">3</option>" +
					"		<option value=\"4\">4</option>" +
					"		<option value=\"5\">5</option>" +
					"		</select>" +
					"		</div>" +
					"</td>" +
					"</tr>" +
					"<tr>" +
					"<td align=center colspan=2><br>" +
					"		<input type=\"button\" value=\"Cancel\" onclick=\"parent.popupHide();\" style=\"height: 22px;font:8pt verdana,arial,sans-serif\">" +
					"		<input type=\"button\" id=\"btnLinkAction\" name=\"btnLinkAction\" value=\"Insert\" onclick=\"parent.CreateHyperlink();parent.popupHide();\" style=\"height: 22px;font:8pt verdana,arial,sans-serif\">" +
					"		</td>" +
					"</tr>" +
					"</table>"
	popupShow(310,"Insert/Edit Link",sHTML)
	
	
	var sURL;
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	

	if (oSel.parentElement)//If text is selected on a layer
		{
		oEl = GetElement(oSel.parentElement(),"DIV")//!
		oElStore = oEl;//Store the active layer, so we can activate it after editing links
		isLayerActive=true;
		}
	else//If control is selected
		{
		isLayerActive=false;
		}	

	/*****************************************************************************/
	/*					idLinkImage & idLinkImageBorder
	/*****************************************************************************/
	//Is the selection image or not ?
	if (oSel.parentElement)//If not an image (such as text)
		{
		oEl = GetElement(oSel.parentElement(),"A")//Get A element if any
		idContentTmp.idLinkImage.style.display = "none";//do not display Image features on the Link Dialog
		}
	else //If a control
		{
		oEl = GetElement(oSel.item(0),"A")//Get A element if any
		if ((oSel.item) && (oSel.item(0).tagName=="IMG")) //If an image
			{
			idContentTmp.idLinkImage.style.display = "block"; //display Image features on the Link Dialog
			idContentTmp.idLinkImageBorder.value = oSel.item(0).border; //get image border
			}
		}

	/*****************************************************************************/
	/*					idLinkTarget & idLinkType & idLinkURL
	/*****************************************************************************/
	//Is there an A element ?
	if (oEl)//If Yes
		{
		idContentTmp.btnLinkAction.value = "Update"
		sURL = oEl.href //get image url
		idContentTmp.idLinkTarget.value = oEl.target;//get image target
		if(sURL.indexOf(":")!=-1)
			{
			switch(sURL.split(":")[0])
				{
				
				case "http":
					idContentTmp.idLinkType.value = "http://";
					idContentTmp.idLinkURL.value = sURL.substr(7);
					break;
				case "https":
					idContentTmp.idLinkType.value = "https://";
					idContentTmp.idLinkURL.value = sURL.substr(8);
					break;
				case "mailto":
					idContentTmp.idLinkType.value = "mailto:";
					idContentTmp.idLinkURL.value = sURL.split(":")[1];
					break;
				case "ftp":
					idContentTmp.idLinkType.value = "ftp://";
					idContentTmp.idLinkURL.value = sURL.substr(6);
					break;
				case "news":
					idContentTmp.idLinkType.value = "news:";
					idContentTmp.idLinkURL.value = sURL.split(":")[1];
					break;
				}
			}
		else
			{
			idContentTmp.idLinkType.value = "";
			idContentTmp.idLinkURL.value = sURL;
			}
		}
	else //If No A element
		{
		idContentTmp.btnLinkAction.value = "Insert"
		
		idContentTmp.idLinkTarget.value = ""
		idContentTmp.idLinkType.value = ""
		idContentTmp.idLinkURL.value = ""
		idContentTmp.idLinkImageBorder.value = 0
		}
	//idContentTmp.idLinkURL.focus();//tambahan	
	}

function pasteFromWord()// OK
	{
	idContent.focus() // => popupHide()
	var oSel	= idContent.document.selection.createRange()
	if(oSel.parentElement)//yg dipilih text bukan control
		{
		idWord.focus()
		idWord.document.execCommand("SelectAll")
		idWord.document.execCommand("Paste")
		
		oSel.pasteHTML(cleanFromWord())
		oSel.execCommand("SelectAll")
		}
	}
function cleanFromWord()
	{
   	for (var i = 0; i < idWord.document.body.all.length; i++) 
		{
		idWord.document.body.all[i].removeAttribute("className","",0);
		idWord.document.body.all[i].removeAttribute("style","",0);
		}
    var sHTML = idWord.document.body.innerHTML;
    alert(sHTML)
    //sHTML = sHTML.replace(/<\?xml:.*?\/>/g, "");
    //sHTML = sHTML.replace(/<o:p>&nbsp;<\/o:p>/g, "");
    //sHTML = sHTML.replace(/o:/g, "");
    //sHTML = sHTML.replace(/<st1:.*?>/g, ""); 
   saveToHiddenField();
   	return  sHTML;
	}
	
function GetElement(oElement,sMatchTag) 
	{
	while (oElement!=null && oElement.tagName!=sMatchTag) 
		{
		if(oElement.id=="idContent") return null;
		oElement = oElement.parentElement
		}
	return oElement
	}


function setDefaultColor(Name,Color)
	{
	var OutputID = Name
	var TextID = Name + "_1"
	var ColorID = Name + "_2"
	eval("idContentTmp."+OutputID).value = Color;
	eval("idContentTmp."+TextID).innerHTML = "<font size=1>"+Color+"</font>"
	eval("idContentTmp."+ColorID).style.background = Color;
	}
function displayBgColor()
	{
	if(idContentTmp.divBgColorPick.style.display=="none")
		{
		//check if it was already written
		if(idContentTmp.divBgColorPick.innerHTML=="")
		
			{
			//if not written yet, then write
			idContentTmp.divBgColorPick.innerHTML = writeColorPicker("inpBgColorPick","inpBgColor.value=inpBgColorPick.value",1);
			//parent.displayBgColor => writeColorPicker dilakukan di IFRAME (dimana ada inpBgColor & inpBgColorPick)
			//makanya inpBgColor.value=inpBgColorPick.value tdk perlu pake parent
			
			//set default color
			setDefaultColor("inpBgColorPick",idContentTmp.inpBgColor.value)
			}
		else
			{
			//already written
			//set default color
			setDefaultColor("inpBgColorPick",idContentTmp.inpBgColor.value)
			}
		//display
		idContentTmp.divBgColorPick.style.display="block"
		}
	else
		{
		//hide
		idContentTmp.divBgColorPick.style.display='none'
		}
	}
		
function displayBorderColor()
	{
	if(idContentTmp.divBorderColorPick.style.display=="none")
		{
		//check if it was already written
		if(idContentTmp.divBorderColorPick.innerHTML=="")
			{
			//if not written yet, then write
			idContentTmp.divBorderColorPick.innerHTML = writeColorPicker("inpBorderColorPick","inpBorderColor.value=inpBorderColorPick.value",1);
			//set default color
			setDefaultColor("inpBorderColorPick",idContentTmp.inpBorderColor.value)
			}
		else
			{
			//already written
			//set default color
			setDefaultColor("inpBorderColorPick",idContentTmp.inpBorderColor.value)
			}
		//display
		idContentTmp.divBorderColorPick.style.display="block"
		}
	else
		{
		//hide
		idContentTmp.divBorderColorPick.style.display='none'
		}
	}	
			
function displayBgColor2()
	{
	if(idContentTmp.divBgColor2Pick.style.display=="none")
		{
		//check if it was already written
		if(idContentTmp.divBgColor2Pick.innerHTML=="")
			{
			//if not written yet, then write
			idContentTmp.divBgColor2Pick.innerHTML = writeColorPicker("inpBgColor2Pick","inpBgColor2.value=inpBgColor2Pick.value",1);
			//set default color
			setDefaultColor("inpBgColor2Pick",idContentTmp.inpBgColor2.value)
			}
		else
			{
			//already written
			//set default color
			setDefaultColor("inpBgColor2Pick",idContentTmp.inpBgColor2.value)
			}
		//display
		idContentTmp.divBgColor2Pick.style.display="block"
		}
	else
		{
		//hide
		idContentTmp.divBgColor2Pick.style.display='none'
		}
	}	
		
function displayBorderColor2()
	{
	if(idContentTmp.divBorderColor2Pick.style.display=="none")
		{
		//check if it was already written
		if(idContentTmp.divBorderColor2Pick.innerHTML=="")
			{
			//if not written yet, then write
			idContentTmp.divBorderColor2Pick.innerHTML = writeColorPicker("inpBorderColor2Pick","inpBorderColor2.value=inpBorderColor2Pick.value",1);
			//set default color
			setDefaultColor("inpBorderColor2Pick",idContentTmp.inpBorderColor2.value)
			}
		else
			{
			//already written
			//set default color
			setDefaultColor("inpBorderColor2Pick",idContentTmp.inpBorderColor2.value)
			}
		//display
		idContentTmp.divBorderColor2Pick.style.display="block"
		}
	else
		{
		//hide
		idContentTmp.divBorderColor2Pick.style.display='none'
		}
	}	
			
function displayCellBgColor()
	{
	if(idContentTmp.divCellBgColorPick.style.display=="none")
		{
		//check if it was already written
		if(idContentTmp.divCellBgColorPick.innerHTML=="")
			{
			//if not written yet, then write
			idContentTmp.divCellBgColorPick.innerHTML = writeColorPicker("inpCellBgColorPick","inpCellBgColor.value=inpCellBgColorPick.value",1);
			//set default color
			setDefaultColor("inpCellBgColorPick",idContentTmp.inpCellBgColor.value)
			}
		else
			{
			//already written
			//set default color
			setDefaultColor("inpCellBgColorPick",idContentTmp.inpCellBgColor.value)
			}
		//display
		idContentTmp.divCellBgColorPick.style.display="block"
		}
	else
		{
		//hide
		idContentTmp.divCellBgColorPick.style.display='none'
		}
	}			
	
			
							
function displayTableBox()
	{
	saveSelection()
	
	var sHTML = "" +
	"<table border=\"0\" cellpadding=\"4\" cellspacing=\"2\" style=\"TABLE-LAYOUT: fixed\" align=center height=405>" +
	"<col width=438>" +
	"<tr>" +
	"<td align=center valign=top>" +
	
	
	"	<table border=0 style=\"table-layout:fixed\" bgcolor=Gainsboro width=402 height=20 cellpadding=0 cellspacing=0>" +
	"	<col width=130><col width=130><col width=130><col width=42>" +
	"	<tr>" +
	"	<td align=center id=\"tab1\" style=\"border-top: darkgray 1px solid; border-left: darkgray 1px solid;\">" +
	"		&nbsp;" +
	"	</td>" +
	"	<td align=center id=\"tab2\" style=\"border-top: darkgray 1px solid; border-left: darkgray 1px solid;\">" +
	"		&nbsp;" +
	"	</td>" +
	"	<td align=center id=\"tab3\" style=\"border-top: darkgray 1px solid; border-left: darkgray 1px solid;  border-right: darkgray 1px solid;\">" +
	"		&nbsp;" +
	"	</td>" +
	"	<td style=\"border-bottom: darkgray 1px solid;\">&nbsp;</td>" +
	"	</tr>" +
	"	</table>" +



	"	<!-- Insert Table Panel -->" +
	"	<div id=\"idInsertTable\">" +
	"	<table border=0 style=\"table-layout:fixed\" width=420 cellpadding=2 cellspacing=2 bgcolor=\"#ececec\" style=\"border-left: darkgray 1px solid; border-bottom: darkgray 1px solid; border-right: darkgray 1px solid;\">" +
	"	<col width=80><col width=130><col width=80><col width=130>" +

	"	<tr><td>Rows:</td>" +
	"		<td><INPUT type=\"text\" id=inpRows name=inpRows size=2 value=2></td>" +
	"		<td>Columns:</td>" +
	"		<td><INPUT type=\"text\" id=inpCols name=inpCols size=2 value=2></td></tr>" +

	"	<tr><td>Width:</td>" +
	"		<td><INPUT type=\"text\" id=\"inpWidth\" name=inpWidth size=2>" +
	"		<SELECT ID=\"inpWidthMe\" NAME=\"inpWidthMe\">" +
	"			<OPTION value=\"\">pixels</OPTION>" +
	"			<OPTION value=\"%\">percent</OPTION>" +
	"		</SELECT>" +
	"		</td>" +
	"		<td>Height:</td>" +
	"		<td><INPUT type=\"text\" id=\"inpHeight\" name=inpHeight size=2>" +
	"		<SELECT ID=\"Select2\" NAME=\"inpHeightMe\">" +
	"			<OPTION value=\"\">pixels</OPTION>" +
	"			<OPTION value=\"%\">percent</OPTION>" +
	"		</SELECT>" +
	"		</td></tr>" +
	
	"	<tr><td colspan=4>Background Color:" +
	"		<INPUT type=\"text\" id=\"inpBgColor\" name=inpBgColor size=6>" +
	"		<INPUT type=\"button\" onclick=\"parent.displayBgColor()\" value=\"Pick\" ID=\"Button3\" NAME=\"Button3\">" +
	"		</td></tr>" +	
	"	<tr><td colspan=4>" +
	"		<div id=\"divBgColorPick\" style=\"display:none\"></div>" +
	"		</td></tr>" +
			
	"	<tr><td colspan=4>Background Image URL:" +
	"		<INPUT type=\"text\" id=inpBgImage name=inpBgImage size=30></td></tr>" +
				
	"	<tr><td>Border Size:</td>" +
	"		<td><INPUT type=\"text\" id=inpBorder name=inpBorder size=2 value=1></td>" +				
	"		<td>Alignment:</td>" +
	"		<td>" +
	"		<SELECT ID=\"inpTblAlign\" NAME=\"inpTblAlign\">" +
	"			<OPTION value=\"\"></OPTION>" +
	"			<OPTION value=\"left\">Left</OPTION>" +
	"			<OPTION value=\"center\">Center</OPTION>" +
	"			<OPTION value=\"right\">Right</OPTION>" +
	"		</SELECT>" +
	"		</td></tr>" +
			
	"	<tr><td colspan=4>Border Color:" +
	"		<INPUT type=\"text\" id=inpBorderColor name=inpBorderColor size=6 value=\"#000000\">" +
	"		<INPUT type=\"button\" onclick=\"parent.displayBorderColor()\" value=\"Pick\" ID=\"Button4\" NAME=\"Button4\">" +
	"		</td></tr>" +
	"	<tr><td colspan=4>" +
	"		<div id=\"divBorderColorPick\" style=\"display:none\"></div>" +			
	"		</td></tr>" +
													
	"	<tr><td>Cell Padding:</td>" +
	"		<td><INPUT type=\"text\" id=inpPadding name=inpPadding size=2 value=0></td>" +
	"		<td>Cell Spacing:</td>" +
	"		<td><INPUT type=\"text\" id=inpSpacing name=inpSpacing size=2 value=0></td></tr>" +
		
	"	<tr><td colspan=4><hr></td></tr>" +

	"	<tr><td>&nbsp;</td>" +
	"		<td align=right><INPUT type=\"button\" value=\"Cancel\" onclick=\"parent.popupHide();\"></td>" +
	"		<td><INPUT type=\"button\" value=\"&nbsp;&nbsp;OK&nbsp;&nbsp;\" onclick=\"if(parent.TableInsert())parent.popupHide();\"></td>" +
	"		<td>&nbsp;</td></tr>" +
	"	</table>" +
	"	</div>" +
	"	<!-- /Insert Table Panel -->" +
	
		
		
	"	<!-- Edit Table Panel -->" +
	"	<div id=\"idEditTable\" align=left style=\"display:none\">" +
	"	<table border=0 style=\"table-layout:fixed\" width=420 cellpadding=2 cellspacing=2 bgcolor=\"#ececec\" style=\"border-left: darkgray 1px solid; border-bottom: darkgray 1px solid; border-right: darkgray 1px solid;\" ID=\"Table2\">" +
	"	<col width=80><col width=130><col width=80><col width=130>" +
		
	"	<tr><td colspan=4>Background Color:" +
	"		<INPUT type=\"text\" id=\"inpBgColor2\" name=inpBgColor2 size=6>" +
	"		<INPUT type=\"button\" onclick=\"parent.displayBgColor2()\" value=\"Pick\" ID=\"Button1\" NAME=\"Button1\">" +
	"		</td></tr>" +		
	"	<tr><td colspan=4>" +
	"		<div id=\"divBgColor2Pick\" style=\"display:none\"></div>" +		
	"		</td></tr>" +
			
	"	<tr><td colspan=4>Background Image URL:" +
	"		<INPUT type=\"text\" id=inpBgImage2 name=inpBgImage2 size=30></td></tr>" +

	"	<tr><td>Width:</td>" +
	"		<td><INPUT type=\"text\" id=\"inpWidth2\" name=inpWidth2 size=2>" +
	"		<SELECT ID=\"inpWidth2Me\" NAME=\"inpWidth2Me\">" +
	"			<OPTION value=\"\">pixels</OPTION>" +
	"			<OPTION value=\"%\">percent</OPTION>" +
	"		</SELECT>" +	
	"		</td>" +
	"		<td>Height:</td>" +
	"		<td><INPUT type=\"text\" id=\"inpHeight2\" name=inpHeight2 size=2>" +
	"		<SELECT ID=\"Select1\" NAME=\"inpHeight2Me\">" +
	"			<OPTION value=\"\">pixels</OPTION>" +
	"			<OPTION value=\"%\">percent</OPTION>" +
	"		</SELECT>" +	
	"		</td></tr>" +
			
	"	<tr><td>Cell Padding:</td>" +
	"		<td><INPUT type=\"text\" id=inpPadding2 name=inpPadding2 size=2 value=0></td>" +
	"		<td>Cell Spacing:</td>" +
	"		<td><INPUT type=\"text\" id=inpSpacing2 name=inpSpacing2 size=2 value=0></td></tr>" +
			
	"	<tr><td>Border Size:</td>" +
	"		<td><INPUT type=\"text\" id=inpBorder2 name=inpBorder2 size=2 value=1></td>" +
	"		<td>Alignment :</td>" +
	"		<td>" +
	"		<SELECT ID=\"inpTblAlign2\" NAME=\"inpTblAlign2\">" +
	"			<OPTION value=\"left\">Left</OPTION>" +
	"			<OPTION value=\"center\">Center</OPTION>" +
	"			<OPTION value=\"right\">Right</OPTION>" +
	"		</SELECT>" +
	"		</td></tr>" +
			
	"	<tr><td colspan=4>Border Color:" +
	"		<INPUT type=\"text\" id=\"inpBorderColor2\" name=inpBorderColor2 size=6>" +
	"		<INPUT type=\"button\" onclick=\"parent.displayBorderColor2()\" value=\"Pick\" ID=\"Button7\" NAME=\"Button7\">" +
	"		</td></tr>" +
	"	<tr><td colspan=4>" +
	"		<div id=\"divBorderColor2Pick\" style=\"display:none\"></div>" +		
	"		</td></tr>" +

	"	<tr><td colspan=4><hr></td></tr>" +
		
	"	<tr><td>&nbsp;</td>" +
	"		<td align=right><INPUT type=\"button\" value=\"Cancel\" onclick=\"parent.popupHide();\" id=\"Button5\" name=button1></td>" +
	"		<td><INPUT type=\"button\" value=\"&nbsp;&nbsp;OK&nbsp;&nbsp;\" onclick=\"if(parent.TableUpdate())parent.popupHide();\" id=\"Button6\" name=button1></td>" +
	"		<td>&nbsp;</td></tr>" +
	"	</table>" +
	"	</div>" +
	"	<!-- /Edit Table Panel -->" +



	"	<!-- Edit Cell Panel -->" +
	"	<div id=\"idEditCell\" align=left style=\"display:none\">" +
	"	<table border=0 style=\"table-layout:fixed\" width=420 cellpadding=2 cellspacing=2 bgcolor=\"#ececec\" style=\"border-left: darkgray 1px solid; border-bottom: darkgray 1px solid; border-right: darkgray 1px solid;\" ID=\"Table3\">" +
	"	<col width=80><col width=130><col width=80><col width=130>" +

	"	<tr><td colspan=4>Cell Background Color :" +
	"		<INPUT type=\"text\" id=\"inpCellBgColor\" name=inpCellBgColor size=6>" +
	"		<INPUT type=\"button\" onclick=\"parent.displayCellBgColor()\" value=\"Pick\" ID=\"Button2\" NAME=\"Button1\">" +
	"		</td></tr>" +		
	"	<tr><td colspan=4>" +
	"		<div id=\"divCellBgColorPick\" style=\"display:none\"></div>" +			
	"		</td></tr>" +

	"	<tr><td colspan=4>Cell Background Image URL :" +
	"		<INPUT type=\"text\" id=\"inpCellBgImage\" name=inpCellBgImage size=30></td></tr>" +	
			
	"	<tr><td>Cell Width :</td>" +
	"		<td><INPUT type=\"text\" id=\"inpCellWidth\" name=inpCellWidth size=2>" +
	"		<SELECT ID=\"inpCellWidthMe\" NAME=\"inpCellWidthMe\">" +
	"			<OPTION value=\"\">pixels</OPTION>" +
	"			<OPTION value=\"%\">percent</OPTION>" +
	"		</SELECT>" +		
	"		</td>" +
	"		<td>Cell Height :</td>" +
	"		<td><INPUT type=\"text\" id=\"inpCellHeight\" name=inpCellHeight size=2>" +
	"		<SELECT ID=\"inpCellHeightMe\" NAME=\"inpCellHeightMe\">" +
	"			<OPTION value=\"\">pixels</OPTION>" +
	"			<OPTION value=\"%\">percent</OPTION>" +
	"		</SELECT>" +	
	"		</td></tr>" +
						
	"	<tr><td>Alignment :</td>" +
	"		<td>" +
	"		<SELECT ID=\"inpCellAlign\" NAME=\"inpCellAlign\">" +
	"			<OPTION value=\"left\">Left</OPTION>" +
	"			<OPTION value=\"center\">Center</OPTION>" +
	"			<OPTION value=\"right\">Right</OPTION>" +
	"		</SELECT>" +
	"		</td>" +
	"		<td colspan=2>Vertical Alignment :" +
	"		<SELECT ID=\"inpCellVAlign\" NAME=\"inpCellVAlign\">" +
	"			<OPTION value=\"top\">Top</OPTION>" +
	"			<OPTION value=\"middle\">Middle</OPTION>" +
	"			<OPTION value=\"bottom\">Bottom</OPTION>" +
	"		</SELECT>" +
	"		</td></tr>" +
		
	"	<tr><td>Wrap Text :</td>" +
	"		<td><INPUT type=\"checkbox\" ID=\"inpCellWrap\" NAME=\"inpCellWrap\"></td>" +
	"		<td colspan=2></td></tr>" +

	"	<tr><td>Insert Row:</td>" +
	"		<td colspan=3>" +
	"		<INPUT style=\"width=95\" type=\"button\" onclick=\"parent.TableInsertRow('above');parent.popupHide();\" value=\"Above\" id=\"btnInsertRow\" NAME=\"btnInsertRow\">" +
	"		<INPUT style=\"width=95\" type=\"button\" onclick=\"parent.TableInsertRow('below');parent.popupHide();\" value=\"Below\" id=\"btnInsertRow\" NAME=\"btnInsertRow\">" +
	"		</td></tr>" +
	"	<tr><td>Insert Column:</td>" +
	"		<td colspan=3>" +
	"		<INPUT style=\"width=95\" type=\"button\" onclick=\"parent.TableInsertCol('left');parent.popupHide();\" value=\"to the Left\" id=\"btnInsertColumn\" NAME=\"btnInsertColumn\">" +
	"		<INPUT style=\"width=95\" type=\"button\" onclick=\"parent.TableInsertCol('right');parent.popupHide();\" value=\"to the Right\" id=\"btnInsertColumn\" NAME=\"btnInsertColumn\">" +
	"		</td></tr>" +
		
	"	<tr><td>Delete:</td>" +
	"		<td colspan=3>" +
	"		<INPUT style=\"width=95\" type=\"button\" onclick=\"parent.TableDeleteRow();parent.popupHide();\" value=\"Delete Row\" id=\"btnDeleteRow\" NAME=\"btnDeleteRow\">" +
	"		<INPUT style=\"width=95\" type=\"button\" onclick=\"parent.TableDeleteCol();parent.popupHide();\" value=\"Delete Column\" id=\"btnDeleteColumn\" NAME=\"btnDeleteColumn\">" +
	"		<INPUT style=\"width=95\" type=\"button\" onclick=\"parent.TableDeleteCell();parent.popupHide();\" value=\"Delete Cell\" id=\"btnDeleteCell\" NAME=\"btnDeleteCell\">" +
	"		</td></tr>" +

	"	<tr><td>Span:</td>" +
	"		<td colspan=3>" +
	"		<INPUT style=\"width=95\" type=\"button\" onclick=\"parent.TableColSpan();parent.popupHide();\" value=\"Columns Span\" id=\"btnColumnsSpan\" NAME=\"btnColumnsSpan\">" +
	"		<INPUT style=\"width=95\" type=\"button\" onclick=\"parent.TableRowSpan();parent.popupHide();\" value=\"Rows Span\" id=\"btnRowsSpan\" NAME=\"btnRowsSpan\">" +
	"		</td></tr>" +
		
	"	<tr><td colspan=4><hr></td></tr>" +
			
	"	<tr><td>&nbsp;</td>" +
	"		<td align=right><INPUT type=\"button\" value=\"Cancel\" onclick=\"parent.popupHide();\"></td>" +
	"		<td><INPUT type=\"button\" value=\"&nbsp;&nbsp;OK&nbsp;&nbsp;\" onclick=\"if(parent.CellUpdate())parent.popupHide();\"></td>" +
	"		<td>&nbsp;</td></tr>" +
			
	"	</table>" +
	"	</div>" +
	"	<!-- /Edit Cell Panel -->" +
	
	
	
	"</td>" +
	"</tr>" +
	"</table>"

	popupShow(440,"Create/Edit Table",sHTML)
	
	
	
	//set default value
	idContentTmp.inpRows.value = 2;
	idContentTmp.inpCols.value = 2;
	idContentTmp.inpTblAlign.value = "";
	idContentTmp.inpWidth.value = "";
	idContentTmp.inpHeight.value = "";
	idContentTmp.inpPadding.value = 0;
	idContentTmp.inpSpacing.value = 0;
	idContentTmp.inpBorder.value = 1;
	idContentTmp.inpBorderColor.value = "";
	idContentTmp.inpBgImage.value = "";
	idContentTmp.inpBgColor.value = "";

	
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	

	var oBlock = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TABLE") : GetElement(oSel.item(0),"TABLE"))
	if (oBlock!=null) //If inside existing table
		{
		//Get existing table properties
		idContentTmp.inpTblAlign2.value = oBlock.align
		
		var st = oBlock.width
		if(st.indexOf("%")!=-1)
			{
			idContentTmp.inpWidth2.value = st.substring(0,st.indexOf("%")) //remove last %
			idContentTmp.inpWidth2Me.value = "%"
			}
		else
			{
			idContentTmp.inpWidth2.value = oBlock.width
			idContentTmp.inpWidth2Me.value = ""
			}
			
		var st2 = oBlock.height
		if(st2.indexOf("%")!=-1)
			{
			idContentTmp.inpHeight2.value = st2.substring(0,st2.indexOf("%")) //remove last %
			idContentTmp.inpHeight2Me.value = "%"
			}
		else
			{
			idContentTmp.inpHeight2.value = oBlock.height
			idContentTmp.inpHeight2Me.value = ""
			}			
		idContentTmp.inpPadding2.value = oBlock.cellPadding
		idContentTmp.inpSpacing2.value = oBlock.cellSpacing
		idContentTmp.inpBorder2.value = oBlock.border
		
		idContentTmp.inpBorderColor2.value = (oBlock.borderColor).substring(1)
		idContentTmp.inpBgImage2.value = oBlock.background
		idContentTmp.inpBgColor2.value = (oBlock.bgColor).substring(1)

		if(oSel.parentElement != null)//yg dipilih text, bukan control(=table)
			{//Get existing cell properties
			
			var oTD = GetElement(oSel.parentElement(),"TD");
			
			if(oTD==null)return; //jika yg di select adl text tapi meliputi lebih dari satu cell
				
			var st3 = oTD.width //HTML : width
			if(st3.indexOf("%")!=-1)
				{
				idContentTmp.inpCellWidth.value = st3.substring(0,st3.indexOf("%")) //remove last %
				idContentTmp.inpCellWidthMe.value = "%"
				}
			else
				{
				idContentTmp.inpCellWidth.value = oTD.width
				idContentTmp.inpCellWidthMe.value = ""
				}
			var st4 = oTD.height //HTML : height
			if(st4.indexOf("%")!=-1)
				{
				idContentTmp.inpCellHeight.value = st4.substring(0,st4.indexOf("%")) //remove last %
				idContentTmp.inpCellHeightMe.value = "%"
				}
			else
				{
				idContentTmp.inpCellHeight.value = oTD.height
				idContentTmp.inpCellHeightMe.value = ""
				}			
			
			idContentTmp.inpCellAlign.value = oTD.align //HTML : align
			idContentTmp.inpCellVAlign.value = oTD.vAlign //HTML : vAlign
			idContentTmp.inpCellBgImage.value = oTD.background //HTML : background
			idContentTmp.inpCellBgColor.value = (oTD.bgColor).substring(1)	//HTML : bgColor 	
			idContentTmp.inpCellWrap.checked = !oTD.noWrap;	
			
			
			//set the tab
			TabEditCell()
			}
		else
			{
			//set the tab
			TabEditTable()		
			
			//disable create table & edit table
			TabDisable1()
			}
		}
	else
		{
		//set the tab
		TabInsertTable()
		
		//disable edit table & edit cell
		TabDisable2()
		}		
	}	
	
function TabInsertTable()
	{
	idContentTmp.divBorderColorPick.style.display = "none";
	idContentTmp.divBgColorPick.style.display = "none";	
		
	idContentTmp.idInsertTable.style.display="block";
	idContentTmp.idEditTable.style.display="none";
	idContentTmp.idEditCell.style.display="none";

	idContentTmp.tab1.style.cursor = "";
	idContentTmp.tab1.style.background = "#ececec";
	idContentTmp.tab1.style.color = "darkslateblue";
	idContentTmp.tab1.style.borderBottom = "";
	idContentTmp.tab1.innerHTML = "<b>Create Table</b>"

	idContentTmp.tab2.style.background = "#bebebe";
	idContentTmp.tab2.style.color = "darkslateblue";
	idContentTmp.tab2.style.borderBottom = "darkgray 1px solid";
	idContentTmp.tab2.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabEditTable()'><b><u>Edit Table</u></b></div>"
	
	idContentTmp.tab3.style.background = "#bebebe";
	idContentTmp.tab3.style.color = "darkslateblue";
	idContentTmp.tab3.style.borderBottom = "darkgray 1px solid";
	idContentTmp.tab3.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabEditCell()'><b><u>Edit Cell</u></b></div>"
	
	//when the control (table) is selected, the focus shouldn't be displayed. 
	//Then set the focus to another control.
	//inpRows.focus();
	}
function TabEditTable()
	{	
	idContentTmp.divBorderColor2Pick.style.display = "none";
	idContentTmp.divBgColor2Pick.style.display = "none";	
		
	idContentTmp.idInsertTable.style.display="none";
	idContentTmp.idEditTable.style.display="block";
	idContentTmp.idEditCell.style.display="none";

	idContentTmp.tab1.style.background = "#bebebe";
	idContentTmp.tab1.style.color = "darkslateblue";
	idContentTmp.tab1.style.borderBottom = "darkgray 1px solid";
	idContentTmp.tab1.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabInsertTable()'><b><u>Create Table</u></b></div>"

	idContentTmp.tab2.style.cursor = "";
	idContentTmp.tab2.style.background = "#ececec";
	idContentTmp.tab2.style.color = "darkslateblue";
	idContentTmp.tab2.style.borderBottom = "";
	idContentTmp.tab2.innerHTML = "<b>Edit Table</b>"
	
	idContentTmp.tab3.style.background = "#bebebe";
	idContentTmp.tab3.style.color = "darkslateblue";
	idContentTmp.tab3.style.borderBottom = "darkgray 1px solid";
	idContentTmp.tab3.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabEditCell()'><b><u>Edit Cell</u></b></div>"
	
	//when the control (table) is selected, the focus shouldn't be displayed. 
	//Then set the focus to another control.
	//inpBgColor2.focus() 	
	}
function TabEditCell()
	{
	idContentTmp.idEditCell.style.display="block";
	idContentTmp.divCellBgColorPick.style.display = "none";
		
	idContentTmp.idInsertTable.style.display="none";
	idContentTmp.idEditTable.style.display="none";
	idContentTmp.idEditCell.style.display="block";
	
	idContentTmp.tab1.style.background = "#bebebe";
	idContentTmp.tab1.style.color = "darkslateblue";
	idContentTmp.tab1.style.borderBottom = "darkgray 1px solid";
	idContentTmp.tab1.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabInsertTable()'><b><u>Create Table</u></b></div>"
	
	idContentTmp.tab2.style.background = "#bebebe";
	idContentTmp.tab2.style.color = "darkslateblue";
	idContentTmp.tab2.style.borderBottom = "darkgray 1px solid";
	idContentTmp.tab2.innerHTML = "<div style='cursor:hand;width=100%;' onclick='parent.TabEditTable()'><b><u>Edit Table</u></b></div>"
	
	idContentTmp.tab3.style.cursor = "";
	idContentTmp.tab3.style.background = "#ececec";
	idContentTmp.tab3.style.color = "darkslateblue";
	idContentTmp.tab3.style.borderBottom = "";
	idContentTmp.tab3.innerHTML = "<b>Edit Cell</b>"	
	
	//when the control (table) is selected, the focus shouldn't be displayed. 
	//Then set the focus to another control.
	//inpCellBgColor.focus()
	}
function TabDisable1()
	{
	idContentTmp.tab1.style.cursor = "";
	idContentTmp.tab1.style.background = "#bebebe";
	idContentTmp.tab1.style.color = "darkgray";
	idContentTmp.tab1.style.borderBottom = "";
	idContentTmp.tab1.innerHTML = "<b><u>Create Table</u></b>"

	idContentTmp.tab3.style.cursor = "";
	idContentTmp.tab3.style.background = "#bebebe";
	idContentTmp.tab3.style.color = "darkgray";
	idContentTmp.tab3.style.borderBottom = "";
	idContentTmp.tab3.innerHTML = "<b><u>Edit Cell</u></b>"
	}
function TabDisable2()
	{
	idContentTmp.tab2.style.cursor = "";
	idContentTmp.tab2.style.background = "#bebebe";
	idContentTmp.tab2.style.color = "darkgray";
	idContentTmp.tab2.style.borderBottom = "";
	idContentTmp.tab2.innerHTML = "<b><u>Edit Table</u></b>"

	idContentTmp.tab3.style.cursor = "";
	idContentTmp.tab3.style.background = "#bebebe";
	idContentTmp.tab3.style.color = "darkgray";
	idContentTmp.tab3.style.borderBottom = "";
	idContentTmp.tab3.innerHTML = "<b><u>Edit Cell</u></b>"		
	}
	
function TableInsert()
	{
	if(!(IsPosIntNotZero(idContentTmp.inpRows.value) &&
		IsPosIntNotZero(idContentTmp.inpCols.value) &&
		IsPosIntNotZero(idContentTmp.inpWidth.value) &&
		IsPosIntNotZero(idContentTmp.inpHeight.value) &&
		IsPosInt(idContentTmp.inpBorder.value) &&
		IsPosInt(idContentTmp.inpPadding.value) &&
		IsPosInt(idContentTmp.inpSpacing.value))) 
		{
		alert("Invalid input.");
		return false;
		}
			
	var sHTML = ""
		+ "<TABLE "
		+ (((idContentTmp.inpBorder.value=="") || (idContentTmp.inpBorder.value=="0")) ? "class=\"NOBORDER\"" : "")
		+	(idContentTmp.inpTblAlign.value != "" ? "align=\"" + idContentTmp.inpTblAlign.value + "\" " : "")		
		+	(idContentTmp.inpWidth.value != "" ? "width=\"" + idContentTmp.inpWidth.value + idContentTmp.inpWidthMe.value + "\" " : "")
		+	(idContentTmp.inpHeight.value != "" ? "height=\"" + idContentTmp.inpHeight.value + idContentTmp.inpHeightMe.value + "\" " : "")		
		+	(idContentTmp.inpPadding.value != "" ? "cellPadding=\"" + idContentTmp.inpPadding.value + "\" " : "")
		+	(idContentTmp.inpSpacing.value != "" ? "cellSpacing=\"" + idContentTmp.inpSpacing.value + "\" " : "")
		+	(idContentTmp.inpBorder.value != "" ? "border=\"" + idContentTmp.inpBorder.value + "\" " : "")
		+	(idContentTmp.inpBorderColor.value != "" ? "bordercolor=\"" + idContentTmp.inpBorderColor.value + "\" " : "")
		+	(idContentTmp.inpBgImage.value != "" ? "background=\"" + idContentTmp.inpBgImage.value + "\" " : "")
		+	(idContentTmp.inpBgColor.value != "" ? "bgColor=\"" + idContentTmp.inpBgColor.value + "\" " : "")
		+ ">"
	for (var i=0; i < idContentTmp.inpRows.value; i++) 
		{
		sHTML += "<TR>"
		for (var j=0; j < idContentTmp.inpCols.value; j++)
		sHTML += "<TD>&nbsp;</TD>"
		sHTML += "</TR>"
		}
	sHTML += "</TABLE>"
	
	//idContent.focus()
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	
	
	if (sType=="Control")oSel.item(0).outerHTML = sHTML
	else oSel.pasteHTML(sHTML)
	return true;
	}
function TableUpdate()
	{
	if(!(IsPosIntNotZero(idContentTmp.inpWidth2.value) &&
		IsPosIntNotZero(idContentTmp.inpHeight2.value) &&
		IsPosInt(idContentTmp.inpPadding2.value) &&
		IsPosInt(idContentTmp.inpSpacing2.value) &&
		IsPosInt(idContentTmp.inpBorder2.value))) 
		{
		alert("Invalid input.");
		return false;		
		}
	
	//idContent.focus()
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type		
		
	var oBlock = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TABLE") : GetElement(oSel.item(0),"TABLE"))
	if (oBlock!=null) 
		{
		oBlock.align = idContentTmp.inpTblAlign2.value
		if(idContentTmp.inpWidth2.value!="")
			{
			oBlock.style.width=""; //remove style width
			oBlock.width = idContentTmp.inpWidth2.value + idContentTmp.inpWidth2Me.value;
			}
		else
			{
			oBlock.width = ""
			}
		if(idContentTmp.inpHeight2.value!="")
			{
			oBlock.style.height=""; //remove style height
			oBlock.height = idContentTmp.inpHeight2.value + idContentTmp.inpHeight2Me.value;
			}
		else
			{
			oBlock.height = ""
			}		
		oBlock.cellPadding = idContentTmp.inpPadding2.value
		oBlock.cellSpacing = idContentTmp.inpSpacing2.value
		oBlock.border = idContentTmp.inpBorder2.value
		oBlock.borderColor = idContentTmp.inpBorderColor2.value
		oBlock.background = idContentTmp.inpBgImage2.value
		oBlock.bgColor = idContentTmp.inpBgColor2.value
		}
			
	oSel.select()
	return true;
	
	}
function CellUpdate()
	{
	if(!(IsPosIntNotZero(idContentTmp.inpCellWidth.value) &&
		IsPosIntNotZero(idContentTmp.inpCellHeight.value))) 
		{
		alert("Invalid input.");
		return false;
		}

	//idContent.focus()
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type		
				
	var oTD = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TD") : GetElement(oSel.item(0),"TD"))
	if (oTD!=null)
		{
		if(idContentTmp.inpCellWidth.value!="")
			{
			//oTD.style.width=""; //remove style width => tdk perlu
			oTD.width = idContentTmp.inpCellWidth.value + idContentTmp.inpCellWidthMe.value;
			}
		else
			{
			oTD.width = ""
			}
		if(idContentTmp.inpCellHeight.value!="")
			{
			//oTD.style.height=""; //remove style height => tdk perlu
			oTD.height = idContentTmp.inpCellHeight.value + idContentTmp.inpCellHeightMe.value;
			}
		else
			{
			oTD.height = ""
			}		
		
		oTD.align = idContentTmp.inpCellAlign.value ;
		oTD.vAlign = idContentTmp.inpCellVAlign.value;
		oTD.background = idContentTmp.inpCellBgImage.value;
		oTD.bgColor = idContentTmp.inpCellBgColor.value ;
		oTD.noWrap = !idContentTmp.inpCellWrap.checked;
		}

	oSel.select()
	return true;
	}	
	
function IsPosInt(sInput)
	{
	if(sInput=="") return true;//must be empty or number greater than or equal 0
	var sTmp = sInput.toString();
	for(var i=0;i<sTmp.length;i++)
		{
		var sChar = sTmp.charAt(i);
		if(sChar<"0"||sChar>"9") return false;
		}
	return true;
	}
function IsPosIntNotZero(sInput)//must be empty or number greater than or equal 1
	{
	if(sInput=="") return true;
	var sTmp = sInput.toString();
	for(var i=0;i<sTmp.length;i++)
		{
		var sChar = sTmp.charAt(i);
		if(sChar<"0"||sChar>"9") return false;
		}
	if(sInput*1==0) {return false}
	else {return true};
	}
	
function TableInsertRow(type)
	{
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	
	
	var oBlock = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TABLE") : GetElement(oSel.item(0),"TABLE"))
	
	var numofrows = oBlock.rows.length; //num of rows
	
	//Dia ada di row mana ?
	var i,iRow;
	var oTR = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TR") : GetElement(oSel.item(0),"TR"))	
	for (i=0;i<numofrows;i++)
		{
		if(oTR==oBlock.rows[i]) iRow=i;
		}	
	
	var numofcols = oBlock.rows[0].cells.length //num of cols
	
	var elRow;
	if(type=="above") elRow = oBlock.insertRow(iRow); //insert above
	if(type=="below") elRow = oBlock.insertRow(iRow+1); //insert below
	
	for (i=0;i<numofcols;i++)
		{
		try
			{
			var elCell = elRow.insertCell()
			elCell.innerHTML = "&nbsp;"
			}
		catch(e)
			{}
		}	

	}
function TableDeleteRow()
	{
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	

	var oBlock = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TABLE") : GetElement(oSel.item(0),"TABLE"))
	var oTR = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TR") : GetElement(oSel.item(0),"TR"))	

	try
		{
		var iRow;
		for (var i=0;i<oBlock.rows.length;i++) //num of rows
			{
			if(oTR==oBlock.rows[i])oBlock.deleteRow(i);
			}
		}
	catch(e)
		{
		return;
		}
	}
function TableInsertCol(type)
	{
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	
	
	var oBlock = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TABLE") : GetElement(oSel.item(0),"TABLE"))

	//Di row yg aktif, dia ada di col mana ?
	var oTD = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TD") : GetElement(oSel.item(0),"TD"))	
	var iCol;
	iCol = oTD.cellIndex; //insert left
	if(type=="right") iCol=iCol+1; //insert right
		
		
	var numofrows = oBlock.rows.length; //num of rows
	for (var i=0;i<numofrows;i++) //num of rows
		{
		try
			{
			var elCell = oBlock.rows[i].insertCell(iCol) 
			elCell.innerHTML = "&nbsp;"
			}
		catch(e)
			{}		
		}	
	}
function TableDeleteCol()
	{
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	

	var oBlock = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TABLE") : GetElement(oSel.item(0),"TABLE"))
	var oTD = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TD") : GetElement(oSel.item(0),"TD"))	
	try
		{
		//Dia ada di row mana ?
		var iRow;
		var oTR = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TR") : GetElement(oSel.item(0),"TR"))	
		for (var i=0;i<oBlock.rows.length;i++) //num of rows
			{
			if(oTR==oBlock.rows[i]) iRow=i;
			}	
		//Di row yg aktif, dia ada di col mana ?
		var iCol = oTD.cellIndex;
		for (var i=0;i<oBlock.rows.length;i++) //num of rows
			{
			oBlock.rows[i].deleteCell(iCol);
			}
		}
	catch(e)
		{
		return;
		}					
	}
function TableDeleteCell()
	{
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	
	
	var oBlock = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TABLE") : GetElement(oSel.item(0),"TABLE"))
	var oTD = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TD") : GetElement(oSel.item(0),"TD"))	
	try
		{
		//Dia ada di row mana ?
		var iRow;
		var oTR = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TR") : GetElement(oSel.item(0),"TR"))	
		for (var i=0;i<oBlock.rows.length;i++) //num of rows
			{
			if(oTR==oBlock.rows[i]) iRow=i;
			}	
		//Di row yg aktif, dia ada di col mana ?
		var iCol = oTD.cellIndex;
		oBlock.rows[iRow].deleteCell(iCol);
		}
	catch(e)
		{
		return;
		}
	}
function TableColSpan()
	{
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	

	var oTD = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TD") : GetElement(oSel.item(0),"TD"))	
	oTD.colSpan = 2;
	}
function TableRowSpan()
	{
	var oSel	= oSelSave//idContent.document.selection.createRange()
	var sType = sSelType//idContent.document.selection.type	

	var oTD = (oSel.parentElement != null ? GetElement(oSel.parentElement(),"TD") : GetElement(oSel.item(0),"TD"))	
	oTD.rowSpan = 2;
	}
