<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Items.ascx.cs" Inherits="EFundraisingCRMWeb.Components.User.Sales.Items" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="TextOnCard" Src="TextOnCard.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProductLookUp" Src="../Package/ProductLookUP2.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    
<style type="text/css">
    .style1
    {
        height: 5px;
        width: 90px;
    }
    .style2
    {
        width: 90px;
    }
    
     .popupControl{
	background-color:White;
	position:absolute;
	visibility:hidden;
        top: 260px;
        left: 115px;
        right: 631px;
        margin-bottom: 16px;
     }


    .style3
    {
        width: 597px;
        height: 27px;
    }
    .style6
    {
        height: 27px;
    }


</style>

   
   
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="894" border="0"> 

	<TR>
		<TD align="left" colSpan="1"><asp:datagrid id="ItemsDatagrid" CellSpacing="1" DataKeyField="SalesItemId" GridLines="None" runat="server"
				Width="816px" BackColor="WhiteSmoke" HorizontalAlign="Left" BorderWidth="0px" BorderColor="#EDEDE1" AutoGenerateColumns="False"
				AllowSorting="True" PageSize="50" CssClass="NormalText">
				<AlternatingItemStyle CssClass="AlternateItemBackGround"></AlternatingItemStyle>
				<HeaderStyle Font-Bold="True" HorizontalAlign="Left" CssClass="Arm lternateItemBackGround NormalTextBold Passive"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Product">
						<ItemTemplate>
							<uc1:ProductLookUp id="ProductLookUp1" runat="server"></uc1:ProductLookUp>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Profit %">
						<ItemTemplate>
							<asp:DropDownList id="Profit" Width="60px" CssClass="NormalText" Runat="server"></asp:DropDownList>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Product Code">
						<ItemTemplate>
							<asp:TextBox id=ProductCode runat="server" Width="130px" CssClass="NormalText normalTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.ProductCode") %>' BorderStyle="Solid" ReadOnly="True">
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Qty">
						<ItemTemplate>
							<asp:TextBox id=Quantity runat="server" CssClass="NormalText normalTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' BorderStyle="Solid" columns="5">
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Qty Free">
						<ItemTemplate>
							<asp:TextBox id=QuantityFree CssClass="NormalText normalTextBox" columns="5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QuantityFree") %>' BorderStyle="Solid">
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Item Price">
						<ItemTemplate>
							<asp:TextBox id=Price CssClass="NormalText normalTextBox" columns="8" runat="server" ReadOnly="True" BorderStyle="Solid" Text='<%# DataBinder.Eval(Container, "DataItem.Price") %>'>
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total Amount">
						<ItemTemplate>
							<asp:TextBox id=TotalAmount CssClass="NormalText normalTextBox" columns="9" runat="server" BorderStyle="Solid" Text='<%# DataBinder.Eval(Container, "DataItem.TotalAmount") %>' ReadOnly="True">
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total Profit">
						<ItemTemplate>
							<asp:TextBox id=TotalProfit runat="server" CssClass="NormalText normalTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.TotalProfit") %>' BorderStyle="Solid" ReadOnly="True" columns="8">
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:ImageButton id="DeleteImage" ToolTip="Remove Item" runat="server" OnClick="DeleteButton_Click"
								ImageUrl="../../../ressources/Images/SmallRemove.gif"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:ImageButton id="TextOnCardImage" visible="False" ToolTip="Text On Card" runat="server" OnClick="TextOnCardButton_Click"
								ImageUrl="../../../ressources/Images/edit2.gif"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="ProfitValue">
						<ItemTemplate>
							<asp:TextBox id="Textbox1" CssClass="NormalText normalTextBox" ReadOnly=True runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Profit") %>' BorderStyle="Solid">
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="scratchbookid">
						<ItemTemplate>
							<asp:TextBox id=ScratchbookID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ScratchBookID") %>' ReadOnly="True" BorderStyle="Solid" Visible="False">
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="saleitemid">
						<ItemTemplate>
							<asp:TextBox id="SalesItemID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SalesItemID") %>' BorderStyle="Solid" ReadOnly="True" Visible="False">
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn Visible="False" HeaderText="groupName">
						<ItemTemplate>
							<asp:TextBox id="GroupName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GroupName") %>' BorderStyle="Solid" ReadOnly="True" Visible="False">
							</asp:TextBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
			</asp:datagrid></TD>
	</TR>
                            
	<TR>
		<TD>
			<TABLE id="Table2" width="891" border="0">
				<TR>
					<TD style="WIDTH: 597px" vAlign="top">
						<TABLE id="Table3" style="WIDTH: 576px; HEIGHT: 24px" cellSpacing="0" cellPadding="0" width="576"
							border="0">
							<TR>
								<TD style="WIDTH: 111px"><asp:button id="AddItemButton" runat="server" Width="100px" CssClass="buttonFlat" Text="Add New Item"
										tabIndex="8" onclick="AddItemButton_Click"></asp:button></TD>
								<TD style="WIDTH: 1px"></TD>
								<TD><uc1:textoncard id="TextOnCard1" runat="server" Visible="False"></uc1:textoncard></TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="WIDTH: 133px; HEIGHT: 5px" class="NormalText"><asp:label id="Label1" runat="server">Total</asp:label></TD>
					<TD class="style1"><asp:textbox id=TotalTextBox runat="server" CssClass="NormalText normalTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid" ReadOnly="True">
						</asp:textbox></TD>
					<TD style="HEIGHT: 5px"><asp:textbox id=TotalProfitTextBox runat="server" CssClass="NormalText normalTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="8" BorderStyle="Solid" ReadOnly="True">
						</asp:textbox></TD>
					<TD style="HEIGHT: 5px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 597px"><asp:label id="errorLabel" runat="server" ForeColor="Red"></asp:label></TD>
					<TD style="WIDTH: 133px" class="NormalText"><asp:label id="Label2" runat="server">Shipping Fees</asp:label></TD>
					<TD class="style2">
                        <asp:textbox id=ShippingFeeTextBox runat="server" 
                            CssClass="NormalText normalTextBox" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" 
                            BorderStyle="Solid" ReadOnly="True"></asp:textbox></TD>
					<TD>
                        <asp:ImageButton ID="ShippingImageButton" runat="server" Height="25px" 
                            ImageUrl="~/Ressources/Images/TerritoryManagement2.gif" 
                            onclick="ShippingImageButton_Click" />
                    </TD>
					<TD></TD>
				</TR>
				<TR>
					<TD class="style3">
                        <asp:Label ID="HiddenDiscountIDLabel" runat="server" Visible="False"></asp:Label>
                    </TD>
					<TD class="NormalText">
                        <asp:Label ID="DiscountLabel" runat="server" Text="Discount"></asp:Label>
                    </TD>
					<TD>
	                    <asp:textbox id=DiscountTextBox runat="server" 
                            CssClass="NormalText specialTextBox" 
                            Columns="9" 
                            BorderStyle="Solid"></asp:textbox>
                            
                                
                         


</TD>
    
    
    
    
					<TD class="style6 Arm lternateItemBackGround NormalTextBold Passive">Total Adj.
                        </TD>
					<TD class="style6"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 597px">
                        <asp:Label ID="HiddenSurchargeIDLabel" runat="server" Visible="False"></asp:Label>
                            
      <asp:Panel ID="Panel2" runat="server" CssClass="popupControl" Width="150">
            <div style="border: 1px outset white; width: 150px">
                <asp:UpdatePanel runat="server" ID="up2">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="DiscountReasonRadioButtonList" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="DiscountReasonRadioButtonList_SelectedIndexChanged" Font-Size="7">
                            
                        </asp:RadioButtonList>
                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    
  
    
     <%--<ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server"
            TargetControlID="DiscountTextBox"
            PopupControlID="Panel2"
            CommitProperty="value"
            Position="right"
            CommitScript="e.value += ' - do not forget!';" />--%>
            
               <asp:Panel ID="Panel1" runat="server" CssClass="popupControl" Width="150">
            <div style="border: 1px outset white; width: 150px">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="SurchargeReasonRadioButtonList" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="SurchargeReasonRadioButtonList_SelectedIndexChanged" Font-Size="7">
                            
                        </asp:RadioButtonList>
                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    
  
    
    <%-- <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" runat="server"
            TargetControlID="SurchargeTextBox"
            PopupControlID="Panel1"
            CommitProperty="value"
            Position="right"
            CommitScript="e.value += ' - do not forget!';" />--%>
            
                    </TD>
					<TD style="WIDTH: 133px" class="NormalText">
                        <asp:Label ID="SurchargeLabel" runat="server" Text="Surcharge"></asp:Label>
                    </TD>
					<TD class="style2">
                        <asp:textbox id=SurchargeTextBox runat="server" 
                            CssClass="NormalText specialTextBox" 
                            Columns="9" 
                            BorderStyle="Solid" CausesValidation="True"></asp:textbox></TD>
					<TD>
                        <asp:textbox id="AdjusmentTotalTextBox" runat="server" 
                            CssClass="NormalText specialTextBox" 
                            Columns="9" 
                            BorderStyle="Solid" CausesValidation="False" ReadOnly="true"></asp:textbox></TD>
					<TD>&nbsp;</TD>
				</TR>
				<!-- adding promobox -->
                <TR>

					<TD style="WIDTH: 597px"></TD>
					<TD style="WIDTH: 133px" class="NormalText"><asp:label id="Label7" runat="server">Promotion</asp:label></TD>
					<TD class="style2"><asp:textbox id="PromoCodeTextBox" runat="server" CssClass="NormalText normalTextBox" Columns="9" BorderStyle="Solid" ReadOnly="True">
						</asp:textbox></TD>
					<TD>
                        &nbsp;</TD>
					<TD></TD>
				</TR>
                
                <TR>
					<TD style="WIDTH: 597px"></TD>
					<TD style="WIDTH: 133px" class="NormalText"><asp:label id="Label3" runat="server">GST</asp:label></TD>
					<TD class="style2"><asp:textbox id=GSTTextBox runat="server" CssClass="NormalText normalTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid" ReadOnly="True">
						</asp:textbox></TD>
					<TD>
                        &nbsp;</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 597px"></TD>
					<TD style="WIDTH: 133px" class="NormalText"><asp:label id="Label4" runat="server">PST</asp:label></TD>
					<TD class="style2"><asp:textbox id=PSTtextBox runat="server" CssClass="NormalText normalTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid" ReadOnly="True">
						</asp:textbox></TD>
					<TD></TD>
					<TD></TD>
				</TR>

                <!-- added hst -->
                <TR>
					<TD style="WIDTH: 597px"></TD>
					<TD style="WIDTH: 133px" class="NormalText"><asp:label id="Label6" runat="server">HST</asp:label></TD>
					<TD class="style2"><asp:textbox id=HSTtextBox runat="server" CssClass="NormalText normalTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid" ReadOnly="True">
						</asp:textbox></TD>
					<TD></TD>
					<TD></TD>
				</TR>



				<TR>
					<TD style="WIDTH: 597px">
                            
     
                    </TD>
					<TD style="WIDTH: 133px" class="NormalText"><asp:label id="Label5" runat="server">Invoice Amount</asp:label></TD>
					<TD class="style2"><asp:textbox id=InvoiceAmountTextBox runat="server" CssClass="NormalText normalTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Columns="9" BorderStyle="Solid" ReadOnly="True">
						</asp:textbox></TD>
					<TD></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	</TABLE>
