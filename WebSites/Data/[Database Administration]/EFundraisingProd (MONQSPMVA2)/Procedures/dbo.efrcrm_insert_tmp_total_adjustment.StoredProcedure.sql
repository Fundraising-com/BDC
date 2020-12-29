USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_tmp_total_adjustment]    Script Date: 02/14/2014 13:07:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Tmp_total_adjustment
CREATE PROCEDURE [dbo].[efrcrm_insert_tmp_total_adjustment] @Sales_ID int OUTPUT, @Adjustment_Amount numeric AS
begin

insert into Tmp_total_adjustment(Adjustment_Amount) values(@Adjustment_Amount)

select @Sales_ID = SCOPE_IDENTITY()

end
GO
