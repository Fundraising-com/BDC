<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="EfundraisingCRM.Sales.SalesScreen.WebForm1" %>

<%@ Register Src="../../Components/User/Package/ProductLookUp2.ascx" TagName="ProductLookUp"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<uc1:ProductLookUp id="ProductLookUp1" runat="server"></uc1:ProductLookUp></div>
    </form>
</body>
</html>
