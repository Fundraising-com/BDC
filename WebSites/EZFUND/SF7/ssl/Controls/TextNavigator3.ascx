<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%@ Control %>
<%' © 2005 Structured Solutions %>
<%
' This sample demonstrates how to list multiple properties for each item
' in the Navigator menu.
%>
<sfaddons:Navigator id="Navigator" runat="server" CssClass="navigator">

  <CategoryTemplate>
    <div class='categorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overcategorybox";' 
         onmouseout='this.className="categorybox";'>
         
      <%# Container.DisplayName %>
     (<%# Container.TotalProductCount %>)
     
    </div>
  </CategoryTemplate>
  
  <SubcategoryTemplate>
    <div class='subcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="oversubcategorybox";' 
         onmouseout='this.className="subcategorybox";'>
         
      <%# Container.DisplayName %>
     (<%# Container.TotalProductCount %>)
     
    </div>
  </SubcategoryTemplate>
  
  <SelectedCategoryTemplate>
    <div class='selectedcategorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedcategorybox";' 
         onmouseout='this.className="selectedcategorybox";'>
         
      <%# Container.DisplayName %>
     (<%# Container.TotalProductCount %>)
     
    </div>
  </SelectedCategoryTemplate>
  
  <SelectedSubcategoryTemplate>
    <div class='selectedsubcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedsubcategorybox";' 
         onmouseout='this.className="selectedsubcategorybox";'>
         
      <%# Container.DisplayName %>
     (<%# Container.TotalProductCount %>)
     
    </div>
  </SelectedSubcategoryTemplate>
  
</sfaddons:Navigator>
