USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_adjustment]    Script Date: 02/14/2014 13:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Adjustment
CREATE PROCEDURE [dbo].[efrcrm_update_adjustment] @Sales_ID int, @Adjustment_No int, @Reason_ID int, @Adjustment_Date datetime, @Adjustment_Amount decimal(15,4), @Comment text, @Adjustment_On_Shipping decimal(15,4), @Adjustment_On_Taxes decimal(15,4), @Adjustment_On_Sale_Amount decimal(15,4) AS
begin

update Adjustment set Adjustment_No=@Adjustment_No, Reason_ID=@Reason_ID, Adjustment_Date=@Adjustment_Date, Adjustment_Amount=@Adjustment_Amount, Comment=@Comment, Adjustment_On_Shipping=@Adjustment_On_Shipping, Adjustment_On_Taxes=@Adjustment_On_Taxes, Adjustment_On_Sale_Amount=@Adjustment_On_Sale_Amount where Sales_ID=@Sales_ID

end
GO
