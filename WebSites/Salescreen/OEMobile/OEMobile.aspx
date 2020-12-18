<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OEMobile.aspx.cs" Inherits="OEMobile" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <mobile:Form id="Form1" runat="server">
        <mobile:Label ID="Label1" Runat="server" Font-Name="Arial" Font-Size="Normal">Welcome To OrderExpress Mobile</mobile:Label> 
        <mobile:Label ID="Label3" Runat="server" Font-Name="Arial" Font-Size="Small">Please enter your username and password</mobile:Label>
        <mobile:Label ID="lblError" Runat="server" Font-Name="Arial" Font-Size="Small" Font-Bold="True" ForeColor="Red"></mobile:Label>
        <mobile:Label ID="Label2" Runat="server" Font-Size="Small">User Name : </mobile:Label> 
        <mobile:TextBox ID="txtUserName" Runat="server"></mobile:TextBox> 
        <mobile:Label ID="Label4" Runat="server" Font-Size="Small">Password : </mobile:Label> 
        <mobile:TextBox ID="txtPassword" Runat="server" Password="True"> </mobile:TextBox> 
        <mobile:Command ID="Command1" Runat="server" OnClick="Command1_Click">Submit</mobile:Command>
    </mobile:Form>
</body>
</html>
