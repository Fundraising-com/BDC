<%@ Register TagPrefix="uc1" TagName="ControlerProductSelect" Src="ControlerProductSelect.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerProductMultiSelect.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerProductMultiSelect" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<uc1:controlerproductselect id="ctrlControlerProductSelect" runat="server" showsearch="True" showcheckboxes="True"
	showterm="True" showlanguage="True" showcatalogname="True" showpriceinformation="False"></uc1:controlerproductselect><br>
<asp:button id="btnAddToList" runat="server" text="Add to the list" onclick="btnAddToList_Click"></asp:button>
<br>
<br>
<br>
<div id="divSelectedList" runat="server">
	<asp:label id="lblSelectedList" runat="server" cssclass="CSPlainText" font-bold="True">Selected Products List:</asp:label>
	<br>
	<uc1:controlerproductselect id="ctrlControlerProductDisplay" runat="server" showsearch="False" showcheckboxes="True"
		showterm="True" showlanguage="True" showcatalogname="True" showpriceinformation="False"></uc1:controlerproductselect><br>
	<asp:button id="btnRemoveFromList" runat="server" text="Remove from the list" onclick="btnRemoveFromList_Click"></asp:button>
	&nbsp;
	<asp:button id="btnClearList" runat="server" text="Clear list" onclick="btnClearList_Click"></asp:button>
	<br>
	<br>
	<br>
</div>
<asp:button id="btnBack" runat="server" text="Back" causesvalidation="False" onclick="btnBack_Click"></asp:button>
&nbsp;
<asp:button id="btnSelectList" runat="server" text="Next" causesvalidation="False" onclick="btnSelectList_Click"></asp:button>
