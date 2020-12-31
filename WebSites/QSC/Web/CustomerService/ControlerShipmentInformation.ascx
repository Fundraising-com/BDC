<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ControlerShipmentInformation.ascx.cs" Inherits="QSPFulfillment.CustomerService.ControlerShipmentInformation" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD>
			<asp:Label id="Label1" CssClass="csPlainText" runat="server">Shipment Id</asp:Label></TD>
		<TD>
			<asp:Label id="lblShipmentID" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label2" CssClass="csPlainText" runat="server">Shipment Date</asp:Label></TD>
		<TD>
			<asp:Label id="lblShipmentDate" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 18px">
			<asp:Label id="Label3" CssClass="csPlainText" runat="server">Delivery Date</asp:Label></TD>
		<TD style="HEIGHT: 18px">
			<asp:Label id="lblExpectedDelevryDate" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label4" CssClass="csPlainText" runat="server">Way Bill To</asp:Label></TD>
		<TD>
			<asp:Label id="lblwaybillno" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 18px">
			<asp:Label id="Label5" CssClass="csPlainText" runat="server">Number of Boxes</asp:Label></TD>
		<TD style="HEIGHT: 18px">
			<asp:Label id="lblnumberboxes" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label6" CssClass="csPlainText" runat="server">Number of Skids</asp:Label></TD>
		<TD>
			<asp:Label id="lblNumberKids" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label7" CssClass="csPlainText" runat="server"> Weigth</asp:Label></TD>
		<TD>
			<asp:Label id="lblWeight" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label8" CssClass="csPlainText" runat="server">Unit Measure</asp:Label></TD>
		<TD>
			<asp:Label id="lblWeightUnitMeasure" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 17px">
			<asp:Label id="Label9" CssClass="csPlainText" runat="server"> Notes</asp:Label></TD>
		<TD style="HEIGHT: 17px">
			<asp:Label id="lblNote" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD style="HEIGHT: 20px">
			<asp:Label id="Label10" CssClass="csPlainText" runat="server">Carrier Name</asp:Label></TD>
		<TD style="HEIGHT: 20px">
			<asp:Label id="lblCarrierName" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
	<TR>
		<TD>
			<asp:Label id="Label11" CssClass="csPlainText" runat="server">Operator Name</asp:Label></TD>
		<TD>
			<asp:Label id="lblOperatorName" CssClass="csPlainText" runat="server"></asp:Label></TD>
	</TR>
</TABLE>
