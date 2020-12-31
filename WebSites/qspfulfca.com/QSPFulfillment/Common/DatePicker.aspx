<%@ Page language="c#" Codebehind="DatePicker.aspx.cs" AutoEventWireup="True" Inherits="QSPFulfillment.CommonWeb.DatePicker" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>DatePicker</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body>
    <form runat="server">
      <asp:Calendar ID="CalDate" OnSelectionChanged="Change_Date" autopostback="True" Runat="server" SelectionMode=Day />
      <input type="hidden" id="CallingControl" runat="server" NAME="CallingControl" />
      <input type="hidden" id="CallingControlButton" runat="server" NAME="CallingControlButton" />
    </form>
  </body>
</html> 