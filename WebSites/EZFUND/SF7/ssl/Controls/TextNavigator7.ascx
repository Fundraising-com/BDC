<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%@ Control %>
<%' © 2005 Structured Solutions %>
<%
' This sample does not use any CSS styling. Categories are displayed
' as normal text. This might be use at the top of a page to show the
' current sub-categories.
%>
<sfaddons:Navigator id="Navigator" runat="server"
  Depth="Branch">

  <CategoryTemplate>
    <a href='<%# Container.Link %>'><%# Container.DisplayName %></a>
  </CategoryTemplate>
  
  <SubcategoryTemplate>
    <a href='<%# Container.Link %>'><%# Container.DisplayName %></a>
  </SubcategoryTemplate>
  
  <SelectedCategoryTemplate>
    <b><%# Container.DisplayName %></b>
  </SelectedCategoryTemplate>
  
  <SelectedSubcategoryTemplate>
    <b><%# Container.DisplayName %></b>
  </SelectedSubcategoryTemplate>
  
  <SeparatorTemplate>
    &nbsp;|&nbsp;
  </SeparatorTemplate>
  
</sfaddons:Navigator>
