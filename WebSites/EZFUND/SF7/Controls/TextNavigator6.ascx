<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%@ Control %>
<%' © 2005 Structured Solutions %>
<%
' This sample does not use any CSS styling. Categories are displayed
' in an unordered list.
%>
<ul>
  <sfaddons:Navigator id="Navigator" runat="server">
  
    <CategoryTemplate>
      <li>
        <a href='<%# Container.Link %>'><%# Container.DisplayName %></a>
      </li>
    </CategoryTemplate>
    
    <SubcategoryTemplate>
      <a href='<%# Container.Link %>'><%# Container.DisplayName %></a>
    </SubcategoryTemplate>
    
    <SelectedCategoryTemplate>
      <li>
        <a href='<%# Container.Link %>'><b><%# Container.DisplayName %></b></a>
      </li>
    </SelectedCategoryTemplate>
    
    <SelectedSubcategoryTemplate>
      <a href='<%# Container.Link %>'><b><%# Container.DisplayName %></b></a>
    </SelectedSubcategoryTemplate>
    
    <SeparatorTemplate>
      <br>
    </SeparatorTemplate>
    
  </sfaddons:Navigator>
</ul>