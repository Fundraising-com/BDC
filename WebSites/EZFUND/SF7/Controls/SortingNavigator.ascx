<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%@ Import Namespace = "StoreFront.SystemBase"%>
<%@ Control %>
<%' © 2005 Structured Solutions %>
<%
' This sample demonstrates a simple menu with roll-over effects
%>

<script runat="server">
    Public Class DisplayOrderSorter
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim item1 As StructuredSolutions.WebControls.NavigatorItem
            Dim item2 As StructuredSolutions.WebControls.NavigatorItem
            item1 = CType(x, StructuredSolutions.WebControls.NavigatorItem)
            item2 = CType(y, StructuredSolutions.WebControls.NavigatorItem)
			      'Tee 1/3/2007 default null value to 0
			      'AFM 9/28/2007 default null value to empty string
			      If item1.Description Is Nothing Then
				      item1.Description = ""
            End If
            If item2.Description Is Nothing Then
				      item2.Description = ""
            End If
            ' No matter what comparison you make, always return these values
            '  1 if item1 "is greater than" item2
            '  0 if item1 "is equal to" item2
            ' -1 if item1 "is less than" item2
            ' This code compares the description
            Return String.Compare(item1.Description, item2.Description)
        End Function
    End Class

    ' Sorts the top level categories
    Private Sub TopLevelCreated(ByVal sender As Object, ByVal e As StructuredSolutions.WebControls.NavigatorItemListEventArgs)
        e.Items.Sort(New DisplayOrderSorter)
    End Sub

    ' Sorts sub-categories
    Private Sub SubcategoriesCreated(ByVal sender As Object, ByVal e As StructuredSolutions.WebControls.NavigatorItemListEventArgs)
        e.Items.Sort(New DisplayOrderSorter)
    End Sub

</script>
<sfaddons:Navigator id="Navigator" runat="server" CssClass="navigator"
  OnTopLevelCreated="TopLevelCreated" OnSubcategoriesCreated="SubcategoriesCreated">
  <!-- Template for top-level categories -->
  <CategoryTemplate>
    <div class='categorybox' 
		     onmouseover='this.className="overcategorybox";' 
		     onmouseout='this.className="categorybox";'>
      <a href="<%# ResolveURL("../" & StoreFrontConfiguration.GetCategoryLink(Container.CategoryID, Container.DisplayName)) %>">
        <%# Container.DisplayName %>
      </a>
    </div>
  </CategoryTemplate>
  <!-- Template for sub-categories -->
  <SubcategoryTemplate>
    <div class='subcategorybox' 
		     style='padding-left: <%# 5 + Container.Level * 10 %>px' 
		     onmouseover='this.className="oversubcategorybox";' 
		     onmouseout='this.className="subcategorybox";'>
      <a href="<%# ResolveURL("../" & StoreFrontConfiguration.GetCategoryLink(Container.CategoryID, Container.DisplayName)) %>">
        <%# Container.DisplayName %>
      </a>
    </div>
  </SubcategoryTemplate>
  <!-- Template for the selected top-level category -->
  <SelectedCategoryTemplate>
    <div class='selectedcategorybox' 
		     onmouseover='this.className="overselectedcategorybox";' 
		     onmouseout='this.className="selectedcategorybox";'>
      <a href="<%# ResolveURL("../" & StoreFrontConfiguration.GetCategoryLink(Container.CategoryID, Container.DisplayName)) %>">
        <%# Container.DisplayName %>
      </a>
    </div>
  </SelectedCategoryTemplate>
  <!-- Template for the selected sub-categories -->
  <SelectedSubcategoryTemplate>
    <div class='selectedsubcategorybox' 
		     style='padding-left: <%# 5 + Container.Level * 10 %>px' 
		     onmouseover='this.className="overselectedsubcategorybox";' 
		     onmouseout='this.className="selectedsubcategorybox";'>
      <a href="<%# ResolveURL("../" & StoreFrontConfiguration.GetCategoryLink(Container.CategoryID, Container.DisplayName)) %>">
        <%# Container.DisplayName %>
      </a>
    </div>
  </SelectedSubcategoryTemplate>
  <!-- This template is inserted between each item -->
  <SeparatorTemplate></SeparatorTemplate>
</sfaddons:Navigator>
