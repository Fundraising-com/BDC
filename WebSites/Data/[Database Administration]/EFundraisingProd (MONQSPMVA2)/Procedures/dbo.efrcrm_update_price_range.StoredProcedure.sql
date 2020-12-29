USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_price_range]    Script Date: 02/14/2014 13:08:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Price_Range
CREATE PROCEDURE [dbo].[efrcrm_update_price_range] @Price_Range_ID int, @Package_ID int, @Minimum_Qty int, @Maximum_Qty int, @Unit_Price_Sold smallmoney AS
begin

update Price_Range set Package_ID=@Package_ID, Minimum_Qty=@Minimum_Qty, Maximum_Qty=@Maximum_Qty, Unit_Price_Sold=@Unit_Price_Sold where Price_Range_ID=@Price_Range_ID

end
GO
