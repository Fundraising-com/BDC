USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_product_by_top_parent_package_id_and_page_name]    Script Date: 02/14/2014 13:05:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrstore_get_product_by_top_parent_package_id_and_page_name] 
    @Top_Parent_Package_id smallint
    , @Page_Name Varchar(400) 
AS
begin

select p.Product_id, p.Parent_product_id, p.Scratch_book_id, p.[name], p.Raising_potential, p.Product_code, p.Enabled, p.Is_inner, p.Create_date
 from Product p inner join Product_desc pd on p.product_id=pd.product_id and pd.culture_code='en-us' where pd.Page_Name=@Page_Name

end
GO
