USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_promotion_cost]    Script Date: 02/14/2014 13:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Promotion_Cost
CREATE PROCEDURE [dbo].[efrcrm_update_promotion_cost] @Promotion_ID int, @Period_Month int, @Period_Year int, @Cost decimal AS
begin

update Promotion_Cost set Period_Month=@Period_Month, Period_Year=@Period_Year, Cost=@Cost where Promotion_ID=@Promotion_ID

end
GO
