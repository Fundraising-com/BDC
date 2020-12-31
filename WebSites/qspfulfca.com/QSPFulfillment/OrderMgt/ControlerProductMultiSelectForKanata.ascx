<%@ Register TagPrefix="uc1" TagName="ControlerProductSelectForKanata" Src="../OrderMgt/ControlerProductSelectForKanata.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerProductMultiSelectForKanata.ascx.cs" Inherits="QSPFulfillment.OrderMgt.ControlerProductMultiSelectForKanata" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:button id="btnBackTop" runat="server" text="Back" causesvalidation="False" onclick="btnBack_Click"></asp:button>&nbsp;
<asp:button id="btnSelectListTop" runat="server" text="Next" causesvalidation="False" onclick="btnSelectList_Click"></asp:button>
<P></P>
<uc1:controlerproductselectforkanata id="ctrlControlerProductSelect" runat="server"></uc1:controlerproductselectforkanata><br>
<asp:button id="btnAddToList" runat="server" text="Add Selected Items" Width="128px" onclick="btnAddToList_Click"></asp:button><br>
<br>
<br>
<P></P>
<div id="divSelectedList" runat="server"><asp:label id="lblSelectedList" runat="server" font-bold="True" cssclass="CSPlainText">Selected Products List:</asp:label><br>
	<uc1:controlerproductselectforkanata id="ctrlControlerProductDisplay" runat="server"></uc1:controlerproductselectforkanata><br>
	<asp:button id="btnRemoveFromList" runat="server" text="Remove from the list" onclick="btnRemoveFromList_Click"></asp:button>&nbsp;
	<asp:button id="btnClearList" runat="server" text="Clear list" onclick="btnClearList_Click"></asp:button><br>
	<br>
	<br>
</div>
<asp:button id="btnBackBottom" runat="server" text="Back" causesvalidation="False" onclick="btnBack_Click"></asp:button>&nbsp;
<asp:button id="btnSelectListBottom" runat="server" text="Next" causesvalidation="False" onclick="btnSelectList_Click"></asp:button>
