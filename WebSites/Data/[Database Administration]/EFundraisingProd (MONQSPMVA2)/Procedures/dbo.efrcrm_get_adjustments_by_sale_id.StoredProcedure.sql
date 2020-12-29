USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_adjustments_by_sale_id]    Script Date: 02/14/2014 13:03:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Adjustment
CREATE PROCEDURE [dbo].[efrcrm_get_adjustments_by_sale_id] @Sales_ID int AS
begin

select Sales_ID, Adjustment_No, Reason_ID, Adjustment_Date, Adjustment_Amount, Comment, Adjustment_On_Shipping, Adjustment_On_Taxes, Adjustment_On_Sale_Amount, charge_id from Adjustment where Sales_ID=@Sales_ID

end
GO
