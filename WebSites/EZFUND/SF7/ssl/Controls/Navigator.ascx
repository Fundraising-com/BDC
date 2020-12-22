<%@ Register TagPrefix="sfaddons" Namespace="StructuredSolutions.WebControls" Assembly="SSNavigator" %>
<%@ Control %>
<%@ Import Namespace="StoreFront.SystemBase" %>
<%' Copyright © 2008 Structured Solutions %>
<sfaddons:Navigator id="Navigator" runat="server">

    <CategoryHeaderTemplate>
        <ul>
    </CategoryHeaderTemplate>

    <CategoryTemplate>
        <li><a href='<%# Container.Link %>'><%# Container.DisplayName %></a>
    </CategoryTemplate>

    <SelectedCategoryTemplate>
        <li><a href='<%# Container.Link %>'><b><%# Container.DisplayName %></b></a>
    </SelectedCategoryTemplate>

    <CategoryFooterTemplate>
        </ul>
    </CategoryFooterTemplate>

    <SubcategoryHeaderTemplate>
        <ul>
    </SubcategoryHeaderTemplate>
    
    <SubcategoryTemplate>
        <li><a href='<%# Container.Link %>'><%# Container.DisplayName %></a>
    </SubcategoryTemplate>

    <SelectedSubcategoryTemplate>
        <li><a href='<%# Container.Link %>'><b><%# Container.DisplayName %></b></a>
    </SelectedSubcategoryTemplate>
    
    <SubcategoryFooterTemplate>
        </ul>
    </SubcategoryFooterTemplate>

</sfaddons:Navigator>
