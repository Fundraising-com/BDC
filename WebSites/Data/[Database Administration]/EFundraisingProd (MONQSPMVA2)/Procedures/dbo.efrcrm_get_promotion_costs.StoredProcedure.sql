USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotion_costs]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion_Cost
CREATE PROCEDURE [dbo].[efrcrm_get_promotion_costs] AS
begin

select Promotion_ID, Period_Month, Period_Year, Cost from Promotion_Cost

end
GO
