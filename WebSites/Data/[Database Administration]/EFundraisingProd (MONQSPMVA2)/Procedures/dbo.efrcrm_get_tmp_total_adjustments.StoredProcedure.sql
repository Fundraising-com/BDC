USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tmp_total_adjustments]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Tmp_total_adjustment
CREATE PROCEDURE [dbo].[efrcrm_get_tmp_total_adjustments] AS
begin

select Sales_ID, Adjustment_Amount from Tmp_total_adjustment

end
GO
