USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_commission_paid_by_id]    Script Date: 02/14/2014 13:04:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Commission_Paid
CREATE PROCEDURE [dbo].[efrcrm_get_commission_paid_by_id] @Commission_Year int AS
begin

select Commission_Year, Commission_Month, Consultant_ID, Sales_ID, AR_Status_ID, Total_Card_Sold, Sales_Amount, Consultant_Commission_Amount from Commission_Paid where Commission_Year=@Commission_Year

end
GO
