<%@ Control %>
<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%' © 2005 Structured Solutions %>
<%
' This sample demonstrates using the ItemCreated event to add special
' symbols to a item in the Navigator menu.
%>
<script runat="server">
  Sub ItemCreated(ByVal sender As Object, _
    ByVal e As StructuredSolutions.WebControls.NavigatorItemEventArgs)
    
    If Not e.Item.FindControl("Arrow") Is Nothing Then
      If e.Item.SubcategoryCount > 0 Then
        If e.Item.Selected Then
          CType(e.Item.FindControl("Arrow"), Label).Text = "^"
        Else
          CType(e.Item.FindControl("Arrow"), Label).Text = ">"
        End If
      Else
        CType(e.Item.FindControl("Arrow"), Label).Text = "&nbsp;"
      End If
    End If
    
  End Sub
</script>

<sfaddons:Navigator id="Navigator" runat="server" CssClass="navigator"
  OnItemCreated="ItemCreated">

  <CategoryTemplate>
    <div class='categorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overcategorybox";' 
         onmouseout='this.className="categorybox";'>
         
      <div>
        <div style="float:right">
          <asp:Label ID="Arrow" Runat="server" />
        </div>
        <%# Container.DisplayName %>
      </div>
      
    </div>
  </CategoryTemplate>

  <SubcategoryTemplate>
    <div class='subcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="oversubcategorybox";' 
         onmouseout='this.className="subcategorybox";'>
      
      <div>
        <div style="float:right">
          <asp:Label ID="Arrow" Runat="server" />
        </div>
        <%# Container.DisplayName %>
      </div>
      
    </div>
  </SubcategoryTemplate>

  <SelectedCategoryTemplate>
    <div class='selectedcategorybox' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedcategorybox";' 
         onmouseout='this.className="selectedcategorybox";'>
         
      <div>
        <div style="float:right">
          <asp:Label ID="Arrow" Runat="server" />
        </div>
        <%# Container.DisplayName %>
      </div>
      
    </div>
  </SelectedCategoryTemplate>

  <SelectedSubcategoryTemplate>
    <div class='selectedsubcategorybox' 
         style='padding-left: <%# 5 + Container.Level * 10 %>px' 
         onclick='window.location="<%# Container.Link %>"' 
         onmouseover='this.className="overselectedsubcategorybox";' 
         onmouseout='this.className="selectedsubcategorybox";'>
         
      <div>
        <div style="float:right">
          <asp:Label ID="Arrow" Runat="server" />
        </div>
        <%# Container.DisplayName %>
      </div>
      
    </div>
  </SelectedSubcategoryTemplate>

</sfaddons:Navigator>
