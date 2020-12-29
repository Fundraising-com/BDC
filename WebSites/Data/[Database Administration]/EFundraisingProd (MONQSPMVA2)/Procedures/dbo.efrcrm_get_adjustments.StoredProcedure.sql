USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_adjustments]    Script Date: 02/14/2014 13:03:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Adjustment
CREATE PROCEDURE [dbo].[efrcrm_get_adjustments] AS
begin

select Sales_ID, Adjustment_No, Reason_ID, Adjustment_Date, Adjustment_Amount, Comment, Adjustment_On_Shipping, Adjustment_On_Taxes, Adjustment_On_Sale_Amount from Adjustment

end
GO
