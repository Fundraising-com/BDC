USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_product_culture]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Product_culture
CREATE PROCEDURE [dbo].[efrstore_update_product_culture] @Product_id int, @Culture_code nvarchar(10) AS
begin

update Product_culture set Culture_code=@Culture_code where Product_id=@Product_id

end
GO
