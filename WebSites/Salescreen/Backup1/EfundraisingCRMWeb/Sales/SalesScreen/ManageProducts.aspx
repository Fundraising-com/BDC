<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="EfundraisingCRM.Sales.SalesScreen.ManageProducts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
   

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link type="text/css" href="igoogle-classic.css" rel="stylesheet" /> 
     
</head>
<body>
    
    <form id="form1" runat="server">
    
    <style type="text/css">
        h1 
        {        	
         font: italic normal 1.4em georgia, sans-serif;
	     letter-spacing: 1px; 
	     margin-bottom: 0; 
	     color: #7D775C;
        }
         
        #container {text-align="center"}
        #spacer {height="105"}
        
               
        
    </style>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="container">
       <div id="header">
          <p>
          <h1>Order Express Product Manager</h1>
          </p>
          <table>
             <tr>
                 <td>Category:</td>
                 <td><asp:DropDownList ID="categoryDropDownList" runat="server" 
                         onselectedindexchanged="categoryDropDownList_SelectedIndexChanged"/></td>
              </tr>
              <tr>
                 <td>Item:</td>
                 <td><asp:DropDownList ID="itemDropDownList" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="itemDropDownList_SelectedIndexChanged"/></td>
              </tr>
              <tr>
                 <td>Profit %:</td>
                 <td><asp:DropDownList ID="profitDropDownList" runat="server"/></td>
              </tr>
              <tr>
                 <td>Currency:</td>
                 <td><asp:DropDownList ID="currencyDropDownList" runat="server"/></td>
              </tr>   
              
           
           </table>
           
           <!--<ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="CategoryDropDownList"
            Category="Category"  PromptText="Please select a category"  LoadingText="[Loading categories...]"
            ServicePath="ProductManager.asmx" ServiceMethod="GetDropDownContents" />
            <ajaxToolkit:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="itemDropDownList"
            Category="Item"  PromptText="Please select an item"  LoadingText="[Loading items...]"
            ServicePath="ProductManager.asmx" ServiceMethod="GetDropDownContentsPageMethod" />
            <ajaxToolkit:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="profitDropDownList"
            Category="Profit"  PromptText="Please select a profit rate"  LoadingText="[Loading profits...]"
            ServicePath="ProductManager.asmx" ServiceMethod="GetDropDownContents" />-->
                
              
           
         
       
           <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                
              
           
         
       
        <div id="spacer">
        
        </div>
        <div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="inline">
            <ContentTemplate>
                <asp:GridView 
                ID="gv" runat="server" 
                CssClass="igoogle igoogle-classic" AutoGenerateColumns="true">
                <RowStyle CssClass="data-row" />
                <AlternatingRowStyle CssClass="alt-data-row" />
                <HeaderStyle CssClass="header-row" />
           
            </asp:GridView>
            </ContentTemplate>
               <Triggers>
                <asp:AsyncPostBackTrigger ControlID="itemDropDownList" EventName="SelectedIndexChanged" />
            </Triggers>
            </asp:UpdatePanel>
        </div> 
        </div>
    </div>
    </form>
</body>
</html>
