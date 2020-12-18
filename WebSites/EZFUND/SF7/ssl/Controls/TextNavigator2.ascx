<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%@ Control %>
<%' © 2005 Structured Solutions %>
<%
' This sample displays all the properties for each menu item.
%>
<sfaddons:Navigator id="Navigator" runat="server" CssClass="navigator">

  <CategoryTemplate>
    <div class='categorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overcategorybox";'
         onmouseout='this.className="categorybox";'>
<pre>
DisplayName:       <%# Container.DisplayName %>
CategoryID:        <%# Container.CategoryID %>
Description:       <%# Container.Description %>
ImagePath:         <%# Container.ImagePath %>
ItemType:          <%# Container.ItemType %>
Level:             <%# Container.Level %>
Link:              <%# Container.Link %>
ProductCount:      <%# Container.ProductCount %>
Selected:          <%# Container.Selected %>
SubcategoryCount:  <%# Container.SubcategoryCount %>
TotalProductCount: <%# Container.TotalProductCount %>
</pre>      
    </div>
  </CategoryTemplate>
  
  <SubcategoryTemplate>
    <div class='subcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="oversubcategorybox";' 
         onmouseout='this.className="subcategorybox";'>
<pre>
DisplayName:       <%# Container.DisplayName %>
CategoryID:        <%# Container.CategoryID %>
Description:       <%# Container.Description %>
ImagePath:         <%# Container.ImagePath %>
ItemType:          <%# Container.ItemType %>
Level:             <%# Container.Level %>
Link:              <%# Container.Link %>
ProductCount:      <%# Container.ProductCount %>
Selected:          <%# Container.Selected %>
SubcategoryCount:  <%# Container.SubcategoryCount %>
TotalProductCount: <%# Container.TotalProductCount %>
</pre>      
    </div>
  </SubcategoryTemplate>

  <SelectedCategoryTemplate>
    <div class='selectedcategorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedcategorybox";' 
         onmouseout='this.className="selectedcategorybox";'>
<pre>
DisplayName:       <%# Container.DisplayName %>
CategoryID:        <%# Container.CategoryID %>
Description:       <%# Container.Description %>
ImagePath:         <%# Container.ImagePath %>
ItemType:          <%# Container.ItemType %>
Level:             <%# Container.Level %>
Link:              <%# Container.Link %>
ProductCount:      <%# Container.ProductCount %>
Selected:          <%# Container.Selected %>
SubcategoryCount:  <%# Container.SubcategoryCount %>
TotalProductCount: <%# Container.TotalProductCount %>
</pre>      
    </div>
  </SelectedCategoryTemplate>

  <SelectedSubcategoryTemplate>
    <div class='selectedsubcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedsubcategorybox";' 
         onmouseout='this.className="selectedsubcategorybox";'>
<pre>
DisplayName:       <%# Container.DisplayName %>
CategoryID:        <%# Container.CategoryID %>
Description:       <%# Container.Description %>
ImagePath:         <%# Container.ImagePath %>
ItemType:          <%# Container.ItemType %>
Level:             <%# Container.Level %>
Link:              <%# Container.Link %>
ProductCount:      <%# Container.ProductCount %>
Selected:          <%# Container.Selected %>
SubcategoryCount:  <%# Container.SubcategoryCount %>
TotalProductCount: <%# Container.TotalProductCount %>
</pre>      
    </div>
  </SelectedSubcategoryTemplate>
  
</sfaddons:Navigator>
