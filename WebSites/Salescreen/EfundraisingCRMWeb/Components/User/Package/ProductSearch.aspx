<%@ Page language="c#" CodeFile="ProductSearch.aspx.cs" AutoEventWireup="True" Inherits="EFundraisingCRMWeb.Components.User.Package.ProductSearch" smartNavigation="True"%>
<%@ Register TagPrefix="uc1" TagName="ProductGrid" Src="ProductGrid.ascx" %>
<%@ Register Src="ProductPackageTreeView.ascx" TagName="ProductPackageTreeView" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    
    <link href="../../../Ressources/Css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="0">
          <tr>
              <td style="width: 298px">
                  Enter Name or Product Code</td>
          </tr>
          <tr>
              <td style="width: 298px">
                  <asp:TextBox ID="SearchTextBox" runat="server"></asp:TextBox>
                  <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" /></td>
          </tr>
	<TR>
		<TD style="width: 298px">
            <asp:Button ID="ProductsButton" runat="server" Text="Products" Width="82px" OnClick="ProductsButton_Click" />
            <asp:Button ID="ResultsButton" runat="server" OnClick="Button2_Click" Text="Results" Width="82px" />
            <asp:Button ID="DetailButton" runat="server" OnClick="DetailButton_Click" Text="Detail" Width="82px" />
            &nbsp;&nbsp;
        </TD>
	</TR>
          <tr>
              <td style="width: 298px">
                  <asp:ImageButton ID="SelectButton" runat="server" ImageUrl="~/Ressources/Images/add_11.gif"
                                  OnClick="SelectButton_Click" />
                  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Ressources/Images/close_11.gif"
                                  OnClick="ImageButton1_Click2" /></td>
          </tr>
	<TR>
		<TD style="width: 298px">
            <asp:MultiView ID="MultiView1" runat="server">
              <asp:View ID="TreeViewView" runat="server">
                  <table>
                      <tr>
                          <td style="width: 133px">
                  <uc1:ProductPackageTreeView ID="ProductPackageTreeView1" runat="server" />
                          </td>
                      </tr>
                      <tr>
                          <td style="width: 133px">
                          </td>
                      </tr>
                  </table>
                </asp:View>
                <asp:View ID="GridView" runat="server">
                    &nbsp;&nbsp;
                    <uc1:ProductGrid ID="ProductGrid1" runat="server" />
                </asp:View>
                <asp:View ID="DetailView" runat="server">
            <table border="1" bordercolor="#000000" cellpadding="0" cellspacing="0" height="360"
                style="width: 282px; height: 360px" width="282">
                <tr>
                    <td style="height: 358px" valign="top">
                        <table id="Table3" border="0" cellpadding="1" cellspacing="1" style="width: 272px;
                            height: 280px" width="272">
                            <tr>
                                <td style="height: 40px">
                                    <asp:Label ID="ProductNameLabel" runat="server" CssClass="NormalTextBold" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="productDetailLabel" runat="server" CssClass="SmallText"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="height: 196px" valign="bottom">
                                    <table id="Table2" border="0" cellpadding="1" cellspacing="1" style="width: 272px;
                                        height: 47px" width="272">
                                        <tr>
                                            <td style="width: 93px">
                                                <asp:Label ID="Label4" runat="server" CssClass="FrameTitleColor">Product Code</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="ProductCodeTextbox" runat="server" BorderStyle="None" CssClass="SmallText"
                                                    ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px">
                                                <asp:Label ID="Label2" runat="server" CssClass="FrameTitleColor">Product Class</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="ProductClassTextbox" runat="server" BorderStyle="None" CssClass="SmallText"
                                                    ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px">
                                                <asp:Label ID="Label1" runat="server" CssClass="FrameTitleColor">Supplier</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="SupplierTextbox" runat="server" BorderStyle="None" CssClass="SmallText"
                                                    ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="Table4" border="0" cellpadding="1" cellspacing="1" style="width: 272px;
                                        height: 22px" width="272">
                                        <tr>
                                            <td style="width: 91px">
                                            </td>
                                            <td align="right" style="width: 74px">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Width="144px">Product was not found!</asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="bottom">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
                </asp:View>
            </asp:MultiView>&nbsp;
            <input id="hdProductId" runat="server" name="hdProductId" type="hidden" value="-1" />
            <input id="hdProductName" runat="server" name="hdProductName" type="hidden" /></TD>
	</TR>
</TABLE>

    </form>
</body>
</html>
