USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_product_culture]    Script Date: 02/14/2014 13:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Product_culture
CREATE PROCEDURE [dbo].[efrstore_insert_product_culture] @Product_id int OUTPUT, @Culture_code nvarchar(10) AS
begin

insert into Product_culture(Culture_code) values(@Culture_code)

select @Product_id = SCOPE_IDENTITY()

end
GO
