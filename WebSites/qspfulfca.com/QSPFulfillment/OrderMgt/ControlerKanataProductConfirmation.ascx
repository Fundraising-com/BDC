<%@ Register TagPrefix="uc2" TagName="OrderQualifier" Src="../OrderMgt/UC/OrderQualifier.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ControlerProductSelectForKanata" Src="../OrderMgt/ControlerProductSelectForKanata.ascx" %>
<%@ Register TagPrefix="uc4" TagName="Address" Src="../Common/Address.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerKanataProductConfirmation.ascx.cs" Inherits="QSPFulfillment.OrderMgt.ControlerKanataProductConfirmation" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc2" Namespace="QSP.WebControl" Assembly="QSP.WebControl" %>
<%@ Register TagPrefix="uc3" TagName="DatePicker" Src="../Common/DateEntry.ascx" %>
<%@ Register TagPrefix="uc2" TagName="AddressHygiene" Src="../Common/AddressHygiene.ascx" %>
<P>
<P></P>
<asp:button id="btnBackTop" runat="server" text="Back" causesvalidation="False" onclick="btnBack_Click"></asp:button>
<P></P>
<TABLE id="Table2" style="WIDTH: 296px; HEIGHT: 24px" cellSpacing="1" cellPadding="1" width="296"
	border="0">
	<TR>
		<TD style="WIDTH: 110px"><asp:label id="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small">Ship To</asp:label></TD>
		<TD><asp:radiobuttonlist id="rblShipTo" runat="server" Height="8px" CssClass="CSPlainText" repeatdirection="Horizontal"
				autopostback="True" onselectedindexchanged="rblShipTo_SelectedIndexChanged">
				<asp:ListItem Value="School" Selected="True">School</asp:ListItem>
				<asp:ListItem Value="FM">FM</asp:ListItem>
				<asp:ListItem Value="Other">Other</asp:ListItem>
			</asp:radiobuttonlist></TD>
	</TR>
</TABLE>
<asp:label id="lblErrorMessage" runat="server" Font-Bold="True" Font-Size="X-Small" Width="296px"
	ForeColor="Red">Please Select ship to:</asp:label>
<P></P>
<P></P>
<div id="divShiptoName" runat="server">
	<TABLE id="Table1" style="WIDTH: 672px; HEIGHT: 32px" cellSpacing="1" cellPadding="1" width="672"
		border="1">
		<TBODY>
			<TR>
				<TD style="WIDTH: 164px; HEIGHT: 4px"><asp:label id="lblFirstName" runat="server" CssClass="CSPlainText">First Name</asp:label></TD>
				<TD style="HEIGHT: 4px"><asp:textbox id="tbxFirstName" runat="server" Width="208px"></asp:textbox></TD>
				<TD style="HEIGHT: 4px"><asp:label id="lblLastName" runat="server" CssClass="CSPlainText">Last Name</asp:label></TD>
				<TD style="HEIGHT: 4px"><asp:textbox id="tbxLastName" runat="server" Width="200px"></asp:textbox></TD>
			</TR>
			<TR>
				<TD style="WIDTH: 164px; HEIGHT: 14px"><asp:label id="lblEmail" runat="server" CssClass="CSPlainText">Email</asp:label></TD>
				<TD style="HEIGHT: 14px"><asp:textbox id="tbxEmail" runat="server" Width="208px"></asp:textbox></TD>
				<TD style="HEIGHT: 14px"></TD>
				<TD style="HEIGHT: 14px"></TD>
			</TR>
		</TBODY>
	</TABLE>
</div>
<div id="divShipToAddress" runat="server">
	<TABLE id="tblShipToAddress" style="WIDTH: 672px; HEIGHT: 8px" cellSpacing="1" cellPadding="1"
		width="672" border="1">
		<TBODY>
  <TR>
    <TD width="100%">
<asp:label id=lblContactAddress runat="server" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">Contact Address</asp:label></TD></TR>
			<TR>
				<TD width="100%"><uc4:address id="AddressControl" runat="server"></uc4:address></TD>
			</TR>
		</TBODY>
	</TABLE>
</div>
<DIV runat="server">&nbsp;</DIV>
<TABLE id="Table3" style="WIDTH: 672px; HEIGHT: 32px" cellSpacing="1" cellPadding="1" width="672"
	border="0">
	<TR>
		<TD style="WIDTH: 251px; HEIGHT: 1px" width="251"><asp:label id="lblBillTo" runat="server" Font-Names="Verdana" Height="9px" Width="48px" font-size="X-Small"
				font-bold="True" cssclass="CSPlainText">Bill To:</asp:label></TD>
		<TD style="WIDTH: 318px; HEIGHT: 1px"><asp:label id="lblDeliveryDate" runat="server" Font-Names="Verdana" font-size="X-Small" font-bold="True"
				cssclass="CSPlainText">Delivery Date:</asp:label></TD>
		<TD style="HEIGHT: 1px" width="499"><asp:label id="lblOrderQualifier" runat="server" font-size="X-Small" font-bold="True" cssclass="CSPlainText"></asp:label></TD>
	</TR>
	<TR>
		<TD vAlign="middle" noWrap colSpan="1" rowSpan="1"><asp:radiobuttonlist id="rblBillTo" runat="server" Height="16px" CssClass="CSPlainText" autopostback="True"
				Width="104px" RepeatDirection="Horizontal">
				<asp:ListItem Value="School" Selected="True">School</asp:ListItem>
				<asp:ListItem Value="FM">FM</asp:ListItem>
			</asp:radiobuttonlist></TD>
		<TD style="WIDTH: 318px" height="1"><uc3:datepicker id="dteDeliveryDate" runat="server" columns="10"></uc3:datepicker></TD>
		<TD width="499" height="1"><uc2:orderqualifier id="ddlOrderQualifier" runat="server"></uc2:orderqualifier></TD>
	</TR>
</TABLE>
<div runat="server"><uc1:controlerproductselectforkanata id="ctrlControlerProductSelectForKanataConf" runat="server"></uc1:controlerproductselectforkanata></div>
<asp:button id="btnSave" runat="server" text="Save" Width="80px" onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;
<asp:button id="btnConfirm" runat="server" text="Submit to Warehouse" Width="160px" onclick="btnConfirm_Click"></asp:button><asp:hyperlink id="hypPrint" onclick="this.style.visibility = 'hidden';window.print();this.style.visibility = 'visible'"
	runat="server" visible="False" NavigateUrl="javascript: void(0);" ImageUrl="../CustomerService/images/print.gif"></asp:hyperlink>
<P><asp:button id="btnBackBottom" runat="server" text="Back" causesvalidation="False" onclick="btnBack_Click"></asp:button></P></TR></TBODY>
<DIV></DIV>
