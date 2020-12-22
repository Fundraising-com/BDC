<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" CodeBehind="AdminSection.aspx.cs" Inherits="EFundraisingCRMWeb.Sales.SalesScreen.AdminSection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../Ressources/Css/style.css" rel="stylesheet" type="text/css" />
</head>
<body background="../../Ressources/Images/bodyBg2.jpg">
    <form id="form1" runat="server">
    
    
      <style type="text/css">
        table#text  
        {   
        	font: bold;
        	color: #FFFFFF
        }

        .Grid
        {
        	font: bold;
        	color: #FFFFFF
        }
       
        h1 
        {        	
         font: italic normal 1.4em georgia, sans-serif;
	     letter-spacing: 1px; 
	     margin-bottom: 4; 
	     color: #7D775C;
        }   
        
        div#container {              height: 886px;
          } 	
        div#spacer {height="105"}
        div#header {style="background-color: #CCCCCC"}
        
          .style2
          {
              width: 140px;
          }
        
          .style4
          {
              width: 140px;
              height: 26px;
          }
          .style5
          {
              height: 26px;
          }
                  
          .style6
          {
              width: 356px;
          }
                  
          .style7
          {
              height: 26px;
              width: 33px;
          }
          .style8
          {
              width: 33px;
          }
                  
    </style>
    
    
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="container">
     <div id="header" >
     
     <hr/>
         <h1 style="font-size: large; font-weight: bold; color: #FFFFFF">
             <table style="width:100%;">
                 <tr>
                     <td>
                         <asp:Image ID="Image2" runat="server" 
                             ImageUrl="~/Ressources/Images/qspLogo.gif" />
                     </td>
                     <td>
                         &nbsp;</td>
                              <td>
                                  <asp:ImageButton ID="EfundImageButton" runat="server" 
                                      ImageUrl="~/Ressources/Images/efund_small.gif" 
                                      onclick="EfundImageButton_Click" />
                              </td>
                          </tr>
                      </table>
         </h1>
         <h1 style="font-size: large; font-weight: bold; color: #FFFFFF">Order Express Product Manager</h1>
     <hr/>
     
     </div> 
     <table id="text">
     <tr>
     <td id="text">Category</td>
                <td class="style6"><asp:DropDownList ID="CategoryDropDownList" runat="server" Width="370" 
                        AutoPostBack="True" onselectedindexchanged="UpdateGrid" /></td>
    </tr>
    <tr>
      <td>Item</td>
                <td class="style6"><asp:DropDownList ID="ItemDropDownList" runat="server" Width="370" 
                 AutoPostBack="True" onselectedindexchanged="UpdateGrid" /></td>         
     <tr>
       <td>Profit Rate</td>
                <td class="style6">
        <asp:DropDownList ID="ProfitDropDownList" runat="server" onselectedindexchanged="UpdateGrid"
            Width="170" AutoPostBack="True" /></td>         
     </tr>
     <tr>
       <td>Country</td>
                <td class="style6">
        <asp:DropDownList ID="CountryDropDownList" runat="server" 
            Width="170"  onselectedindexchanged="UpdateGrid" AutoPostBack="True" /></td>         
     </tr>
   </table>
   
            <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="CategoryDropDownList"
            Category="Category"  PromptText="Please select a category"  LoadingText="[Loading categories...]"
            ServicePath="ProductManager.asmx" ServiceMethod="GetDropDownContents" />
            
            
            <ajaxToolkit:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ItemDropDownList"
            Category="Items"  PromptText="Please select an item"  LoadingText="[Loading items...]"
            ServicePath="ProductManager.asmx" ServiceMethod="GetDropDownContents" ParentControlID="CategoryDropDownList" />
            
            <ajaxToolkit:CascadingDropDown ID="CascadingDropDown3" runat="server" TargetControlID="ProfitDropDownList"
            Category="Profit"  PromptText="Please select a profit rate"  LoadingText="[Loading rates...]"
            ServicePath="ProductManager.asmx" ServiceMethod="GetDropDownContents" />
   
            <ajaxToolkit:CascadingDropDown ID="CascadingDropDown4" runat="server" TargetControlID="CountryDropDownList"
            Category="Country"  PromptText="Please select a country"  LoadingText="[Loading countries...]"
            ServicePath="ProductManager.asmx" ServiceMethod="GetDropDownContents" />
        <div>
        
        <p>
        
       

            &nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    
             <ContentTemplate>
                <fieldset id="fieldset" >
                <legend id="legend" runat="server" 
                        style="font-family: 'Century Gothic'; color: #33CC33; font-weight: bolder">Results Found
                </legend>
                
                    
                   
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <img src="../../Ressources/Images/activity.gif" />
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" 
                                ForeColor="White" Text="Processing Request..."></asp:Label>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:GridView ID="ItemGridView" runat="server" AllowPaging="True" DataKeyNames="catalog_item_id,catalog_item_category_id,country_code,product_id, commission_rate_id, isGroup, catalog_item_category_catalog_item_id"
                        AutoGenerateColumns="False" CssClass="Grid" GridLines="None" 
                        onrowcommand="ItemGridView_RowCommand" 
                        onpageindexchanging="ItemGridView_PageIndexChanging" 
                        onrowdatabound="ItemGridView_RowDataBound" 
                        onrowediting="ItemGridView_RowEditing" 
                        onselectedindexchanged="ItemGridView_SelectedIndexChanged">
                        <Columns>
                                                    
                            <asp:TemplateField HeaderText="Category Name">
                                <ItemTemplate>
                                    <asp:DropDownList ID="categoryGridDropDownList" runat="server" BorderStyle="Solid" 
                                        BorderWidth="1px" Columns="10" CssClass="NormalText normalTextBox" 
                                        ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="ProductNameTextBox" runat="server" BorderStyle="Solid" 
                                        BorderWidth="1px" Columns="30" CssClass="NormalText normalTextBox" 
                                        text='<%# DataBinder.Eval(Container.DataItem, "catalog_item_name") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="productCodeTextBox" runat="server" BorderStyle="Solid" 
                                        BorderWidth="1px" Columns="10" CssClass="NormalText normalTextBox" 
                                        text='<%# DataBinder.Eval(Container.DataItem, "catalog_item_code") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comm. Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="CommissionTextBox" runat="server" BorderStyle="Solid" 
                                        BorderWidth="1px" Columns="10" CssClass="NormalText normalTextBox" 
                                        text='<%# DataBinder.Eval(Container.DataItem, "commission_rate_value") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Profit">
                                <ItemTemplate>
                                    <asp:TextBox ID="ProfitRateTextBox" runat="server" BorderStyle="Solid" 
                                        BorderWidth="1px" Columns="10" CssClass="NormalText normalTextBox" 
                                        text='<%# DataBinder.Eval(Container.DataItem, "profit_rate_value") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="is_paid_upfront" 
                                HeaderText="Upfront" ReadOnly="True" />
                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="countryImage" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="PriceTextBox" runat="server" BorderStyle="Solid" 
                                        BorderWidth="1px" Columns="10" CssClass="NormalText normalTextBox" 
                                        text='<%# DataBinder.Eval(Container.DataItem, "price","{0:C2}") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                         
                            <asp:CommandField ButtonType="Image" ShowSelectButton="True" 
                                SelectImageUrl="~/Ressources/Images/small_save.jpg" />
                             <asp:CommandField ButtonType="Image" ShowDeleteButton="True"
                                DeleteImageUrl="~/Ressources/Images/disabled_yellow.gif" />
                             <asp:CommandField ButtonType="Image" ShowEditButton="True"
                                EditImageUrl="~/Ressources/Images/disabled.gif" />
                             
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="saveLabel" runat="server" Text="Item Saved" visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                             
                            
                        </Columns>
                        <PagerStyle Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    
          
                </fieldset>
                
            </ContentTemplate>
         
            <Triggers>
               <asp:AsyncPostBackTrigger ControlID="countryDropDownList" EventName="SelectedIndexChanged" />
               <asp:AsyncPostBackTrigger ControlID="profitDropDownList" EventName="SelectedIndexChanged" />
               <asp:AsyncPostBackTrigger ControlID="CategoryDropDownList" EventName="SelectedIndexChanged" />
               <asp:AsyncPostBackTrigger ControlID="ItemDropDownList" EventName="SelectedIndexChanged" />
            </Triggers>
            
            </asp:UpdatePanel>
           
            
           
                    
               <p>  <img alt="" src="../../Ressources/Images/disabled_yellow.gif" 
                     style="width: 15px; height: 14px" />
               <asp:Label ID="Label16" runat="server" ForeColor="White" 
                       Text=" = Delete the commissions only" Font-Italic="True"></asp:Label>
                       <br>
                        <img alt="" src="../../Ressources/Images/disabled.gif" 
                     style="width: 15px; height: 14px" />
               <asp:Label ID="Label9" runat="server" ForeColor="White" 
                       Text=" = Disables the product" Font-Italic="True"></asp:Label>
                       <br>
                       <asp:TextBox 
                           ID="TextBox1" runat="server" BorderColor="Red" BorderStyle="Dashed" 
                       BorderWidth="3px" Columns="2"></asp:TextBox>
                       <asp:Label ID="Label1" runat="server" ForeColor="White" 
                       Text=" = Changes will affect all items in same category" Font-Italic="True"></asp:Label>
                
                     <p>
   
               
                         &nbsp;<ajaxToolkit:TabContainer 
                  runat="server" ID="Tabs" Height="176px"  ActiveTabIndex="1" 
                      Width="725px" BorderColor="#FF0066" BackColor="#99FF33">
            <ajaxToolkit:TabPanel runat="server" ID="Panel1" HeaderText="Insert Product">
                <HeaderTemplate>
                    Insert Product
                </HeaderTemplate>
                <ContentTemplate><asp:UpdatePanel ID="updatePanel2" runat="server"><ContentTemplate><table><tr>
                    <td class="style2">
                        <asp:Label ID="Label7" runat="server" Text="Product Code"></asp:Label>
                    </td><td>
                        <asp:TextBox ID="TextBoxProductCode" runat="server"></asp:TextBox>
                    </td><td>&nbsp;</td><td class="style3">
                    <asp:Label ID="Label13" runat="server" Text="Catalog Category"></asp:Label>
                    </td><td class="style1"><asp:DropDownList ID="CatalogCategoryDropDownList" runat="server"></asp:DropDownList></td></tr><tr>
                    <td class="style2">
                        <asp:Label ID="Product" runat="server" Text="Product Type"></asp:Label>
                    </td><td>
                        <asp:DropDownList ID="DropDownListProductType" runat="server">
                        </asp:DropDownList>
                    </td><td>&nbsp;</td><td class="style3">
                    <asp:Label ID="Label12" runat="server" Text="Vendor"></asp:Label>
                    </td><td class="style1"><asp:DropDownList ID="VendorDropDownList" runat="server"></asp:DropDownList></td></tr><tr>
                    <td class="style2">
                        <asp:Label ID="Label8" runat="server" Text="Product Name"></asp:Label>
                    </td><td>
                        <asp:TextBox ID="TextBoxProductName" runat="server"></asp:TextBox>
                    </td><td></td><td class="style3">
                    <asp:Label ID="Label5" runat="server" Text="Price"></asp:Label>
                    </td><td class="style1"><asp:TextBox ID="TextBoxPrice" runat="server"></asp:TextBox></td></tr><tr>
                    <td class="style2">
                        <asp:Label ID="Label10" runat="server" Text="IVITEM"></asp:Label>
                    </td><td>
                        <asp:TextBox ID="ivitemTextBox" runat="server"></asp:TextBox>
                    </td><td></td><td class="style3">&nbsp;</td><td class="style1">&#160;</td></tr><tr>
                    <td class="style2">
                        <asp:Label ID="Label15" runat="server" Text="IVICOUP"></asp:Label>
                    </td><td>
                        <asp:TextBox ID="ivicoupTextBox" runat="server"></asp:TextBox>
                    </td><td>&nbsp;</td><td class="style3">
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="INSERT" />
                    </td><td class="style1"><asp:Label ID="LabelError" runat="server" ForeColor="Red" Text="Error" 
                            Visible="False"></asp:Label></td></tr></table></ContentTemplate></asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            
            <ajaxToolkit:TabPanel BackColor="Aqua" runat="server" ID="Panel3" HeaderText="Insert Commission" >
                <ContentTemplate><table><tr><td colspan="2" class="style2">&nbsp;<asp:Label ID="InsertCommissionLabel" 
                        runat="server" Font-Bold="True" ForeColor="#336699" 
                        Text="Add commission For Selected Product" Width="280px"></asp:Label>
                    </td><td>&nbsp;</td></tr><tr><td class="style4">Profit Rate</td>
                    <td class="style7"><asp:TextBox ID="profitRateTextBox" runat="server"></asp:TextBox></td><td class="style5">
                    &nbsp;</td></tr><tr><td class="style2"><asp:Label ID="Label6" runat="server" Text="Commission Rate"></asp:Label></td>
                        <td class="style8"><asp:TextBox ID="commRateTextbox" runat="server"></asp:TextBox></td><td>&#160;</td></tr><tr><td class="style2"><asp:Label ID="Label19" runat="server" Text="Paid UpFront"></asp:Label></td>
                    <td class="style8"><asp:DropDownList ID="paidUpfrontDropDownList" runat="server"></asp:DropDownList></td><td>&#160;</td></tr><tr><td class="style2"><asp:Label ID="Label11" runat="server"
                             Text="Country"></asp:Label></td><td class="style8"><asp:DropDownList ID="country2DropDownList" runat="server"></asp:DropDownList></td><td><asp:Button ID="insertCommissionButton" runat="server" Text="Insert" 
                            onclick="insertCommissionButton_Click" /></td></tr><tr><td class="style2">&#160;</td>
                        <td class="style8">
                        <asp:Label ID="CommissionErrorLabel" runat="server"></asp:Label></td><td>&#160;</td></tr></table> 
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        
            <ajaxToolkit:TabPanel runat="server" ID="Panel2" HeaderText="Other">
                <HeaderTemplate>
                    Other
                </HeaderTemplate>
                <ContentTemplate><table><tr><td class="style2">&#160;</td><td>&#160;</td><td>&#160;</td><td>&#160;</td></tr><tr><td class="style4"><asp:Label ID="Label2" runat="server" Text="New Category Name"></asp:Label></td><td class="style5"><asp:TextBox ID="TextBoxCategoryName" runat="server"></asp:TextBox></td><td class="style5">&#160;</td><td 
                        class="style5"><asp:Button ID="Button2" runat="server" 
                        OnClick="InsertCatalogButton_Click" Text="Insert" /></td></tr><tr><td class="style2"><asp:Label ID="Label4" runat="server"
                             Text="New Vendor Name"></asp:Label></td><td><asp:TextBox ID="TextBoxVendorName" runat="server"></asp:TextBox></td><td>&#160;</td><td><asp:Button ID="Button3" runat="server" OnClick="Button2_Click" 
                                Text="Insert" /></td></tr><tr><td class="style2">
                        &nbsp;</td><td>&#160;</td><td>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr></table> 
                   
                   
                   
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
           
        </ajaxToolkit:TabContainer>
        
         </p>
     
       
       
        </div> 
    </div>

   

      <p>
          &nbsp;</p>

   

    </form>
      </body>
</html>
