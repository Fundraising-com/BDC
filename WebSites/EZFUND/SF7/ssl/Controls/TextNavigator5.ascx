<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%@ Control %>
<%' © 2005 Structured Solutions %>
<%
' This sample demonstrates using the TopLevelCreated event to add items
' to the Navigator menu.
%>
<script runat="server">
  ' This script shows one way of adding items to the Navigator list. The other
  ' way is to create custom catalogs. Both techniques are described in the help
  ' file.
    Sub TopLevelCreated(ByVal sender As Object, _
        ByVal e As StructuredSolutions.WebControls.NavigatorItemListEventArgs)
        
        Dim item As StructuredSolutions.WebControls.NavigatorItem

        ' Insert one item at the top of the list
        item = New StructuredSolutions.WebControls.NavigatorItem()
        item.DisplayName = "About Us"
        item.Link = Navigator1.GetStoreFrontUrl("AboutUs.aspx")
        e.Items.Insert(0, item)

        ' Add one item to the bottom of the list
        item = New StructuredSolutions.WebControls.NavigatorItem()
        item.DisplayName = "Contact Us"
        item.Link = Navigator1.GetStoreFrontUrl("ContactUs.aspx")
        e.Items.Add(item)
    End Sub
</script>

<sfaddons:Navigator id="Navigator1" runat="server" CssClass="navigator"
  OnTopLevelCreated="TopLevelCreated">
  
  <CategoryTemplate>
    <div class='categorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overcategorybox";' 
         onmouseout='this.className="categorybox";'>
         
         <%# Container.DisplayName %>
         
    </div>
  </CategoryTemplate>
  
  <SubcategoryTemplate>
    <div class='subcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="oversubcategorybox";' 
         onmouseout='this.className="subcategorybox";'>
         
         <%# Container.DisplayName %>
         
    </div>
  </SubcategoryTemplate>
  
  <SelectedCategoryTemplate>
    <div class='selectedcategorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedcategorybox";' 
         onmouseout='this.className="selectedcategorybox";'>
         
         <%# Container.DisplayName %>
         
    </div>
  </SelectedCategoryTemplate>
  
  <SelectedSubcategoryTemplate>
    <div class='selectedsubcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedsubcategorybox";' 
         onmouseout='this.className="selectedsubcategorybox";'>
         
         <%# Container.DisplayName %>
         
    </div>
  </SelectedSubcategoryTemplate>
  
</sfaddons:Navigator>
