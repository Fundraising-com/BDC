<%@ Register TagPrefix="cc1" Namespace="StoreFront.UITools" Assembly="UITools" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CCartControl.ascx.vb" Inherits="StoreFront.StoreFront.CCartControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" debug="True"%>
<cc1:DynamicCartDisplay id="DynaCart" runat="server" 
	Editable="True" 
	DisplayMultiShipCheck="True" 
	ReOrderBtnDisplay="False" 
	BuyNowBtnDisplay="False" 
	ImagePath="../images/" 
	CheckBoxClass="ContentTableHeader" 
	GiftWrapDetail="True" 
	DisplayGiftWrapRow="True" 
	BundledProductsDisplay="True" 
	GiftWrapBtnDisplay="True" 
	HeadingClass="ContentTableHeader" 
	BorderClass="ContentTable" 
	HorizontalClass="ContentTableHorizontal" 
	OptionsLabel="Options" 
	PriceLabel='<%# CustomizePriceLabel("Price") %>'
	ProductLabel='<%# CustomizeProductLabel("Product") %>'
	QuantityLabel="Qty" 
	StatusLabel='<%# CustomizeStatusLabel("Status") %>' 
	TotalLabel="Total" 
	StatusColumnDisplay="True" 
	SavedCartBtnDisplay="True"></cc1:DynamicCartDisplay>
