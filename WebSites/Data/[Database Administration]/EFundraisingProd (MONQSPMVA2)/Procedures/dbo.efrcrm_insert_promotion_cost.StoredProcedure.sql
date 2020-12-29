USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_promotion_cost]    Script Date: 02/14/2014 13:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Promotion_Cost
CREATE PROCEDURE [dbo].[efrcrm_insert_promotion_cost] @Promotion_ID int OUTPUT, @Period_Month int, @Period_Year int, @Cost decimal AS
begin

insert into Promotion_Cost(Period_Month, Period_Year, Cost) values(@Period_Month, @Period_Year, @Cost)

select @Promotion_ID = SCOPE_IDENTITY()

end
GO
