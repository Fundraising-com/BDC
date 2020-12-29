USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_adjustment]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Adjustment
CREATE PROCEDURE [dbo].[efrcrm_insert_adjustment] @Sales_ID int, @Adjustment_No int, @Reason_ID int, @Adjustment_Date datetime, @Adjustment_Amount decimal(15,4), @Comment text, @Adjustment_On_Shipping decimal(15,4), @Adjustment_On_Taxes decimal(15,4), @Adjustment_On_Sale_Amount decimal(15,4), @charge_id int AS
begin
insert into Adjustment(sales_id, adjustment_No, Reason_ID, Adjustment_Date, Adjustment_Amount, Comment, Adjustment_On_Shipping, Adjustment_On_Taxes, Adjustment_On_Sale_Amount, charge_id) values(@sales_id, @Adjustment_No, @Reason_ID, @Adjustment_Date, @Adjustment_Amount, @Comment, @Adjustment_On_Shipping, @Adjustment_On_Taxes, @Adjustment_On_Sale_Amount, @charge_id)
end
GO
