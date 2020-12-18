<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%@ Control %>
<%  ' Copyright © 2008 Structured Solutions %>
<%
' This sample demonstrates a simple menu with roll-over styling effects
%>
<sfaddons:Navigator id="Navigator" runat="server" CssClass="navigator">

  <!-- Template for top-level categories -->
  <CategoryTemplate>
    <div class='categorybox' 
         onclick='window.location="<%# Container.Link %>";return false;' 
         onmouseover='this.className="overcategorybox";' 
         onmouseout='this.className="categorybox";'>
         
      <a href="<%# Container.Link %>">
        <%# Container.DisplayName %>
      </a>
      
    </div>
  </CategoryTemplate>
  
  <!-- Template for sub-categories -->
  <SubcategoryTemplate>
    <div class='subcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>";return false;' 
         onmouseover='this.className="oversubcategorybox";' 
         onmouseout='this.className="subcategorybox";'>
         
      <a href="<%# Container.Link %>">
        <%# Container.DisplayName %>
      </a>
      
    </div>
  </SubcategoryTemplate>
  
  <!-- Template for the selected top-level category -->
  <SelectedCategoryTemplate>
    <div class='selectedcategorybox' 
         onclick='window.location="<%# Container.Link %>";return false;' 
         onmouseover='this.className="overselectedcategorybox";' 
         onmouseout='this.className="selectedcategorybox";'>
         
      <a href="<%# Container.Link %>">
        <%# Container.DisplayName %>
      </a>
      
    </div>
  </SelectedCategoryTemplate>
  
  <!-- Template for the selected sub-categories -->
  <SelectedSubcategoryTemplate>
    <div class='selectedsubcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>";return false;' 
         onmouseover='this.className="overselectedsubcategorybox";' 
         onmouseout='this.className="selectedsubcategorybox";'>
         
      <a href="<%# Container.Link %>">
        <%# Container.DisplayName %>
      </a>
      
    </div>
  </SelectedSubcategoryTemplate>
  
  <!-- The HTML in this template is inserted between each item -->
  <SeparatorTemplate></SeparatorTemplate>
  
</sfaddons:Navigator>
