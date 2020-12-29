USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_child_packages_by_package_name]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Package
CREATE   PROCEDURE [dbo].[efrstore_get_child_packages_by_package_name]
@package_name VARCHAR(100)  
AS
BEGIN

select 	pd.Package_id
	, pd.Culture_code
	, pd.Template_id
	, pd.[name]
	, pd.Short_desc
	, pd.Long_desc
	, pd.Extra_desc
	, pd.Page_name
	, pd.Page_title
	, pd.Image_name
	, pd.Image_alt_text
	, pd.Display_order
	, pd.Enabled
	, pd.Configuration
	, pd.Create_date
from  Package_desc pd 
inner join Package p on pd.Package_id = p.Package_id
inner join Package pp on p.parent_package_id = pp.package_id  
where pp.[name] = @package_name
order by Display_order

/*
SELECT pd.Package_id, pd.Culture_code, pd.Template_id, pd.[name], pd.Short_desc,pd. Long_desc, pd.Extra_desc,pd.Page_name, pd.Page_title, pd.Image_name, pd.Image_alt_text, pd.Display_order, pd.Enabled, pd.Configuration, pd.Create_date
FROM  Package_desc pd INNER JOIN Package p ON pd.Package_id = p.Package_id
WHERE p.parent_package_id = ( SELECT Package_id FROM Package pp WHERE pp.[name] =  @package_name)
ORDER BY Display_order
*/
END
GO
