USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_tmp_total_adjustment]    Script Date: 02/14/2014 13:08:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Tmp_total_adjustment
CREATE PROCEDURE [dbo].[efrcrm_update_tmp_total_adjustment] @Sales_ID int, @Adjustment_Amount numeric AS
begin

update Tmp_total_adjustment set Adjustment_Amount=@Adjustment_Amount where Sales_ID=@Sales_ID

end
GO
