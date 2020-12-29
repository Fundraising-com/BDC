USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_package_by_top_parent_package_id_and_page_name]    Script Date: 02/14/2014 13:05:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrstore_get_package_by_top_parent_package_id_and_page_name] 
    @Top_Parent_Package_id smallint
    , @Page_Name Varchar(400) 
AS
begin

select 
p.Package_id, p.Parent_package_id, p.[name], p.Profit_percentage, p.Enabled, p.Create_date
from Package p 
    INNER JOIN Package_Desc pd 
        ON p.Package_Id = pd.Package_id 
        and culture_code = 'en-US' 
where pd.Page_Name=@Page_Name
--and parent_package_id=@Top_Parent_Package_id

end
GO
