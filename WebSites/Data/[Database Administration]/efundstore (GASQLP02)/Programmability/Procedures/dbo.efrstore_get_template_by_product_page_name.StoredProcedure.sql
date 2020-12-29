USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_template_by_product_page_name]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Template
CREATE PROCEDURE [dbo].[efrstore_get_template_by_product_page_name] @page_name VARCHAR(100) AS
BEGIN

SELECT t.Template_id, t.[Name], t.Path, t.Create_date 
FROM Template t INNER JOIN Product_Desc p ON t.template_id = p.template_id
WHERE p.page_name=@page_name

END
GO
