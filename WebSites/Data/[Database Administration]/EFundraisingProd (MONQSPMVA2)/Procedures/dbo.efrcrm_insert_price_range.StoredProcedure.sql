USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_price_range]    Script Date: 02/14/2014 13:07:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Price_Range
CREATE PROCEDURE [dbo].[efrcrm_insert_price_range] @Price_Range_ID int OUTPUT, @Package_ID int, @Minimum_Qty int, @Maximum_Qty int, @Unit_Price_Sold smallmoney AS
begin

insert into Price_Range(Package_ID, Minimum_Qty, Maximum_Qty, Unit_Price_Sold) values(@Package_ID, @Minimum_Qty, @Maximum_Qty, @Unit_Price_Sold)

select @Price_Range_ID = SCOPE_IDENTITY()

end
GO
