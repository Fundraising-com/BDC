USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_products_cultures]    Script Date: 02/14/2014 13:07:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Products_cultures
CREATE PROCEDURE [dbo].[efrcrm_insert_products_cultures] @Product_id int OUTPUT, @Culture_id tinyint AS
begin

insert into Products_cultures(Culture_id) values(@Culture_id)

select @Product_id = SCOPE_IDENTITY()

end
GO
