<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%@ Control %>
<%' © 2005 Structured Solutions %>
<%
' This sample demonstrates a simple menu with roll-over effects
%>
<sfaddons:Navigator id="Navigator" runat="server" CssClass="navigator">

  <!-- Template for top-level categories -->
  <CategoryTemplate>
    <div class='categorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overcategorybox";' 
         onmouseout='this.className="categorybox";'>
        <%# Container.DisplayName %>
    </div>
  </CategoryTemplate>
  
  <!-- Template for sub-categories -->
  <SubcategoryTemplate>
    <div class='subcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="oversubcategorybox";' 
         onmouseout='this.className="subcategorybox";'>
        <%# Container.DisplayName %>
    </div>
  </SubcategoryTemplate>
  
  <!-- Template for the selected top-level category -->
  <SelectedCategoryTemplate>
    <div class='selectedcategorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedcategorybox";' 
         onmouseout='this.className="selectedcategorybox";'>
        <%# Container.DisplayName %>
    </div>
  </SelectedCategoryTemplate>
  
  <!-- Template for the selected sub-categories -->
  <SelectedSubcategoryTemplate>
    <div class='selectedsubcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedsubcategorybox";' 
         onmouseout='this.className="selectedsubcategorybox";'>
        <%# Container.DisplayName %>
    </div>
  </SelectedSubcategoryTemplate>
  
  <!-- This template is inserted between each item -->
  <SeparatorTemplate></SeparatorTemplate>
  
</sfaddons:Navigator>
