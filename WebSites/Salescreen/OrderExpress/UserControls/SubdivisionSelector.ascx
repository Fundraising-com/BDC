<%@ Control Language="c#" Inherits="QSP.OrderExpress.Web.UserControls.SubdivisionSelector" Codebehind="SubdivisionSelector.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<td style="WIDTH: 219px"><asp:listbox id="lbxSubdivision" runat="server" Width="232px" Height="88px"></asp:listbox></td>
		<td vAlign="middle" align="center" width="55"><input style="FONT-WEIGHT: bold; WIDTH: 50px" onclick="remove()" type="button" value="<"><br>
			<input style="FONT-WEIGHT: bold; WIDTH: 50px" onclick="add()" type="button" value=">">
		</td>
		<td><asp:listbox id="lbxSelectedSubdivision" runat="server" Width="232px" Height="88px"></asp:listbox></td>
	</tr>
</table>
<input id="hidSelectedSubdivision" type="hidden" name="hidSelectedSubdivision" runat="server">
<script>
var lbxSelectedSubdivision = document.getElementById('<%=this.lbxSelectedSubdivision.ClientID%>');
var lbxSubdivision = document.getElementById('<%=this.lbxSubdivision.ClientID%>');
var hidSelectedSubdivision = document.getElementById('<%=this.hidSelectedSubdivision.ClientID%>');
function add()
{	
	
	var selectedIndex = lbxSubdivision.selectedIndex;
	if(selectedIndex != -1)
	{
		var oOption = document.createElement("OPTION");
		lbxSelectedSubdivision.options.add(oOption);
		oOption.innerText = lbxSubdivision.options[selectedIndex].innerText;
		oOption.value = lbxSubdivision.options[selectedIndex].value;
		
		//remove the option from the Subdivision list
		//lbxSubdivision.options.remove[selectedIndex];
		lbxSubdivision.options[selectedIndex] = null;
		
		SetSelectedSubdivision();
	}
	else
	{
		alert("You must select one Subdivision");
	}
}
function remove()
{
	var selectedIndex = lbxSelectedSubdivision.selectedIndex;
	if(selectedIndex != -1)
	{
		var oOption = document.createElement("OPTION");
		lbxSubdivision.options.add(oOption);
		oOption.innerText = lbxSelectedSubdivision.options[selectedIndex].innerText;
		oOption.value = lbxSelectedSubdivision.options[selectedIndex].value;
		
		//remove the option from the Subdivision list
		//lbxSubdivision.options.remove[selectedIndex];
		lbxSelectedSubdivision.options[selectedIndex] = null;
		SetSelectedSubdivision();
	}
	else
	{
		alert("You must select one Subdivision");
	}
}
// Use this function to keep track of the SelectedSubdivision 
// between post back
function SetSelectedSubdivision()
{
	hidSelectedSubdivision.value = "";
	for(var i=0;i<lbxSelectedSubdivision.options.length;i++)
	{
		hidSelectedSubdivision.value += lbxSelectedSubdivision.options[i].value + ", ";	
	}	
}

</script>
