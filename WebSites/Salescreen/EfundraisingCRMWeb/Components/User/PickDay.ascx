<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PickDay.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.PickDay" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript">

	function ShowNetCalendar(btn)
	{
		var theHeader = GetHeaderControlID(btn.id);
				
		var calFrame =document.getElementById (theHeader + '_Calendarframe');
		var hiddenDate = document.getElementById (theHeader + '_dateTimeHidden');
		var txtDate = document.getElementById (theHeader + '_TextBox1');
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
				//var hiddenDate = btn.parentElement.parentElement.nextSibling.firstChild.firstChild;
				calFrame.style.display = 'block';
				calFrame.style.top = TopD + btn.offsetHeight;
				calFrame.style.left = LeftD - 160;
				calFrame.src = thePath + 'Components/User/PickDay.aspx?date=' + hiddenDate.value + '&name=' + txtDate.id;
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
	<table border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td><asp:TextBox Columns="20" CssClass="NormalText normalTextBox" readonly="True" id="TextBox1" runat="server"
					AutoPostBack="false" BorderStyle="Solid" Width="108px"></asp:TextBox><asp:requiredfieldvalidator id="TextBox1RFV" ErrorMessage="Date cannot be empty" runat="server" ControlToValidate="TextBox1"
					Enabled="False">*</asp:requiredfieldvalidator></td>
			<td width="20" id="imgTD" runat="server" align="right"><img id="calendarImage" runat="server" onclick="javascript:ShowNetCalendar(this)" class="imgButton"
					src="../../Ressources/images/UserControls/icon_calendar.gif"></td>
		</tr>
		<tr>
			<td colspan="2"><input type="hidden" id="dateTimeHidden" runat="server" name="dateTimeHidden"></td>
		</tr>
	</table>
	<iframe runat="server" id="Calendarframe" FRAMEBORDER="0" width="200" height="200" scrolling="no"
		style="BORDER-RIGHT:0px; BORDER-TOP:0px; DISPLAY:none; Z-INDEX:300; LEFT:0px; BORDER-LEFT:0px; BORDER-BOTTOM:0px; POSITION:absolute">
	</iframe>
</DIV>
