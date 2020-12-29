USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_price_ranges]    Script Date: 02/14/2014 13:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Price_Range
CREATE PROCEDURE [dbo].[efrcrm_get_price_ranges] AS
begin

select Price_Range_ID, Package_ID, Minimum_Qty, Maximum_Qty, Unit_Price_Sold from Price_Range

end
GO
