<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>New Page 2</title>
</head>

<body>

<p>&nbsp;</p>
<p>&nbsp;</p>
<asp:Panel ID='_12' Runat='server'>
<TABLE cellSpacing=0  cellPadding=0 width=100% border=0>
 <TR>
<TD class='Content' noWrap><SF_ProductID><asp:label id='lblProductCode_12' Runat='server'>Product ID:&nbsp; </asp:label>
<%# DataBinder.Eval(Product,"ProductCode")%>
</SF_ProductID></TD>
</TR>
<TR>
<TD class='Content'>&nbsp;</TD>
</TR>

<TR>
<TD class='Headings' noWrap><SF_ProductName><asp:label id='lblProductName_12' Runat='server'>Product
Name:&nbsp;</asp:label><%# DataBinder.Eval(Product,"Name")%>
</SF_ProductName></TD>
</TR>
<TR>
<TD class='Content'>&nbsp;</TD>
</TR>


<TR>
<TD class='Content'><SF_LongDescription><asp:label id='lblDescription2_12' Runat='server'>Description:&nbsp;<BR></asp:label><%# DataBinder.Eval(Product,"Description")%></asp:label></SF_LongDescription></TD></TR>
<TR><TD class='Content'><xxxximplied_p>&nbsp;</TD>
</TR><TR><td class='Content' id='ImageCell_12' vAlign='top'><SF_SmallImage><asp:HyperLink id='lnkImage_12' Runat='server' NavigateUrl='<%# DataBinder.Eval(me,"DetailLink") %>'>
<img border=0 align=left  src='<%#DataBinder.Eval(Product,"SmallImage") %>'></asp:HyperLink></td></TR><TR><TD class='Content'>&nbsp;</TD></TR><TR><td class='Content' id='ImageCell2_12' vAlign='top'><IMG Runat='server' src='<%# DataBinder.Eval(Product,"LargeImage")%>'></td></TR><TR><TD class='Content'>&nbsp;</TD></TR><TD class='Content'><SF_ProductPrice><asp:Panel Id='pnlPrice_12' runat ='server' OnDataBinding=pnlPrice_DataBinding >
    <TABLE cellSpacing=0  cellPadding=0 width=100% border=0>     <tr id='trRegularPrice_12'  class='content' runat ='server' >
      <td class='Content' align='left'>
       <asp:Label ID='lblRegularPrice_12' Runat='server' >Price:&nbsp;</asp:Label>
       <asp:Label ID='lblRegularPriceDisplay_12' Runat='server' Visible='True'></asp:Label>
     </td>
  </tr>
  <tr id='trSalePrice_12' class='Content' runat ='server' >
	   <td class='Content' align='left'>
	   <asp:Label ID='lblSalePrice_12' Runat='server'>Sale&nbsp;Price:&nbsp;</asp:Label>
	   <asp:Label ID='lblSalePriceDisplay_12' Runat='server' Visible='True'></asp:Label>
   </td>
  </tr>
  <tr id='trCustomPrice_12' class='Content' runat ='server' >
   <td class='Content'>
    <asp:Label ID='lblCustomPrice_12' Runat='server'>Your&nbsp;Price:&nbsp;</asp:Label>
    <asp:Label ID='lblCustomPriceDisplay_12' Runat='server' Visible='True'></asp:Label>
	  </td>
  </tr></Table></asp:Panel></SF_ProductPrice></TD><TR><TD class='Content'>&nbsp;</TD></TR><TR><TD class='Content'><SF_Category><asp:label id='lblCategory_12' Runat='server'>Category(s):&nbsp;<BR></asp:label><%# DataBinder.Eval(Product,"CategoryNames")%></SF_Category></TD>
</TR><TR><TD class='Content'>&nbsp;</TD></TR><TR><TD class='Content' noWrap><SF_Manufacturer><asp:label id='lblManufacturer_12' Runat='server'>Manufacturer:&nbsp;</asp:label><%# DataBinder.Eval(Product,"Manufacturer")%></SF_Manufacturer></TD></TR>
<TR><TD class='Content'>&nbsp;</TD></TR><TR><TD class='Content'><SF_Attributes><uc1:cattributecontrol id='CAttributeControl1_12' runat='server' DisplayType=1 ></uc1:cattributecontrol></SF_Attributes></TD></TR><TR><TD class='Content'>&nbsp;</TD></TR><TR><TD class='Content'><SF_Quantity><asp:textbox id='txtQty_12' runat='server' size='2' Columns='2'></asp:textbox></SF_Quantity></TD></TR><TR><TD class='Content'>&nbsp;</TD></TR><TR><TD class='Content' noWrap><SF_WishList><asp:LinkButton ID='btnAddToSavedCart_12' Runat='server' onclick='AddCart' CommandName='12'><asp:Image BorderWidth='0' ID='imgAddToSavedCart_12' runat='server' AlternateText='Add To Saved Cart' ImageUrl='<%# DataBinder.Eval(me,"AddToSavedCartImage")%>'></asp:Image></asp:LinkButton></SF_WishList></TD></TR><TR><TD class='Content'>&nbsp;</TD></TR><TR><TD class='Content' noWrap><asp:LinkButton ID='btnAddToCart_12' Runat='server' onclick='AddCart' CommandName='12'><asp:Image BorderWidth='0' ID='imgAddToCart_12' runat='server' AlternateText='Add To Cart' ImageUrl='<%# DataBinder.Eval(me,"AddCartImage")%>'></asp:Image></asp:LinkButton></TD></TR><TR><TD class='Content'>&nbsp;</TD></TR></TABLE></asp:Panel>

</body>

</html>
