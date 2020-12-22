<%@ Page Language="C#" AutoEventWireup="true" Inherits="QSP.OrderExpress.Web.Test"
    CodeBehind="Test.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="ChargeList2" Src="UserControls/OrderChargeList2.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="V2/Forms/OrganizationSearch.aspx">Organization list</a><br />
        <a href="V2/Forms/OrganizationView.aspx?&OrganizationId=127561">Organization view: 127561</a><br />
        <br />
        <a href="">Account list</a><br />
        <br />
        <a href="Logout.aspx">Logout</a><br />
        <hr />
        <a href="NewOrderDetailDisplay.aspx?&OrderID=2473497">Order 2473497</a><br />
        <a href="NewOrderDetailDisplay.aspx?&OrderID=2451521">Order 2451521</a><br />
        <a href="NewOrderDetailDisplay.aspx?&OrderID=2102361">Order 2102361</a><br />
    </div>
    <hr />
    <a href="default.aspx">home</a><br />
    <uc1:ChargeList2 ID="ChargeList2" runat="server"></uc1:ChargeList2>
</body>
</html>
<ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</ajaxtoolkit:ToolkitScriptManager>
<asp:textbox runat="server"></asp:textbox>
