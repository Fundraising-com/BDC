<%@ Control Language="c#" AutoEventWireup="True" CodeFile="ProductLookUp.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Package.ProductLookUp" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script>
		function fnCallDialog(thePath, theAdjTextBox, theHiddenTextBox) 
		{
			var myObject = new Object();
			myObject.MyWindow = window;
			myObject.AdjTextBox = theAdjTextBox;
			myObject.HiddenTextBox = theHiddenTextBox;
			showModelessDialog(thePath + 'Components/User/Package/ProductSearch.aspx',myObject,'status:false;dialogWidth:600px;dialogHeight:600px');
			//showModelessDialog("../../Debug.aspx",window,"status:false;dialogWidth:500px;dialogHeight:500px");
		}
		
		
		function fnUpdate(theAdjTextBox, theHiddenTextBox, theValue, theHiddenValue)
		{
			document.getElementById(theAdjTextBox).value = theValue;
			document.getElementById(theHiddenTextBox).value = theHiddenValue;
		}
		
	function ShowProductSearch()
	{
	alert ("This is a Javascript Alert")
	}	
	function ShowProductSearch(btn)
	{
	

		var theHeader = GetHeaderControlID(btn.id);
		var calFrame =document.getElementById (theHeader + '_productsearchframe');
		var productidHidden = document.getElementById (theHeader + '_productidHidden');
		var saleidHidden = document.getElementById (theHeader + '_saleidHidden');
		var txtDate = document.getElementById (theHeader + '_TextBox1');
		var rHidden = document.getElementById(theHeader + '_rowNoHidden');
		if (calFrame != null)
		{
			if(calFrame.style.display == 'none')
			{
				var LeftD=0;
				var TopD=0;
				var AbsoluteItem;
				for(var n2=btn; n2&&n2.tagName!='BODY'; n2=n2.offsetParent)
				{
					if(n2.style.position.toUpperCase() == "ABSOLUTE" || n2.style.overflowY.toUpperCase() == "AUTO")
						break;
					LeftD+=n2.offsetLeft;
					TopD+=n2.offsetTop;
				}
				//var txtDate = btn.parentElement.parentElement.firstChild.firstChild;
				//var productidHidden = btn.parentElement.parentElement.nextSibling.firstChild.firstChild;
				calFrame.style.display = 'block';
				calFrame.style.top = TopD + btn.offsetHeight-400;
				calFrame.style.left = LeftD-50;
				//calFrame.src = thePath + 'Components/User/PickDay.aspx?date=' + productidHidden.value + '&name=' + txtDate.id;
				//alert(productidHidden.value + '&name=' + txtDate.id	+ '&sid=' + saleidHidden.value + '&rn=' + rHidden.value);
				calFrame.src = thePath + 'Components/User/Package/ProductSearch.aspx?pid=' + productidHidden.value + '&name=' + txtDate.id
								+ '&sid=' + saleidHidden.value + '&rn=' + rHidden.value;
				
			}
			else
				calFrame.style.display = 'none';
		}
	}

	function ShowTextOnCard(btn)
	{
	

		var theHeader = GetHeaderControlID(btn.id);
		var calFrame =document.getElementById (theHeader + '_productsearchframe');
		var productidHidden = document.getElementById (theHeader + '_productidHidden');
		var saleidHidden = document.getElementById (theHeader + '_saleidHidden');
		var txtDate = document.getElementById (theHeader + '_TextBox1');
		var rHidden = document.getElementById(theHeader + '_rowNoHidden');
		if (calFrame != null)
		{
			if(calFrame.style.display == 'none')
			{
				var LeftD=0;
				var TopD=0;
				var AbsoluteItem;
				for(var n2=btn; n2&&n2.tagName!='BODY'; n2=n2.offsetParent)
				{
					if(n2.style.position.toUpperCase() == "ABSOLUTE" || n2.style.overflowY.toUpperCase() == "AUTO")
						break;
					LeftD+=n2.offsetLeft;
					TopD+=n2.offsetTop;
				}
				//var txtDate = btn.parentElement.parentElement.firstChild.firstChild;
				//var productidHidden = btn.parentElement.parentElement.nextSibling.firstChild.firstChild;
				calFrame.style.display = 'block';
				calFrame.style.top = TopD + btn.offsetHeight-400;
				calFrame.style.left = LeftD+20;
				//calFrame.src = thePath + 'Components/User/PickDay.aspx?date=' + productidHidden.value + '&name=' + txtDate.id;
				//alert(productidHidden.value + '&name=' + txtDate.id	+ '&sid=' + saleidHidden.value + '&rn=' + rHidden.value);
				calFrame.src = thePath + 'Components/User/Sales/TextOnCard.aspx?pid=' + productidHidden.value + '&name=' + txtDate.id
								+ '&sid=' + saleidHidden.value + '&rn=' + rHidden.value;
				
			}
			else
				calFrame.style.display = 'none';
		}
	}
	
	function GetHeaderControlID(theObj)
	{		
		var lastIndex = theObj.lastIndexOf('_');
		return theObj.substring(0, lastIndex);
	}
</script>
<DIV id="myDiv">
	<table id="table1" runat="server" cellSpacing="0" cellPadding="0" border="0">
		<tr>
			<td><asp:textbox id="TextBox1" AutoPostBack="false" runat="server" readonly="True" CssClass="NormalText normalTextBox"
					Columns="40" BorderStyle="Solid"></asp:textbox></td>
			<td id="imgTD" align="right" width="20" runat="server">
				<IMG class="imgButton" id="calendarImage" src="../../../Ressources/Images/search2.gif"
					runat="server"></td>
		</tr>
		<tr>
			<td colSpan="2"><input id="productidHidden" type="hidden" name="productidHidden" runat="server">
				<input id="saleidHidden" type="hidden" name="saleidHidden" runat="server"> <input id="rowNoHidden" type="hidden" name="rowNoHidden" runat="server"></td>
		</tr>
	</table>
	<iframe id="productsearchframe" style="BORDER-RIGHT: #e4ecf3 2px solid; BORDER-TOP: #e4ecf3 2px solid; DISPLAY: none; Z-INDEX: 1000; BORDER-LEFT: #e4ecf3 2px solid; WIDTH: 344px; BORDER-BOTTOM: #e4ecf3 2px solid; POSITION: absolute; HEIGHT: 612px"
		frameBorder="0" width="600" scrolling="auto" height="520" runat="server"></iframe>
</DIV>
