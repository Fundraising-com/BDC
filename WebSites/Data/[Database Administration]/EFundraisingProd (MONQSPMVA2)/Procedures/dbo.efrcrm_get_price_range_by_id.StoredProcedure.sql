USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_price_range_by_id]    Script Date: 02/14/2014 13:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Price_Range
CREATE PROCEDURE [dbo].[efrcrm_get_price_range_by_id] @Price_Range_ID int AS
begin

select Price_Range_ID, Package_ID, Minimum_Qty, Maximum_Qty, Unit_Price_Sold from Price_Range where Price_Range_ID=@Price_Range_ID

end
GO
