USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_brand_by_id]    Script Date: 02/14/2014 13:03:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Brand
CREATE PROCEDURE [dbo].[efrcrm_get_brand_by_id] @Brand_ID int AS
begin

select Brand_ID, Name, Promotion from Brand where Brand_ID=@Brand_ID

end
GO
