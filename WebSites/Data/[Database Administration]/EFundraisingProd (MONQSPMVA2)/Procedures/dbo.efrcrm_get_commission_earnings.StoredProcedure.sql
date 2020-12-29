USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_commission_earnings]    Script Date: 02/14/2014 13:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Commission_Earning
CREATE PROCEDURE [dbo].[efrcrm_get_commission_earnings] AS
begin

select Commission_Earning_ID, Sales_ID, Product_Description, Payment_Amount, Payment_Entry_Date, Commission_Amount, Commission_Rate, Payment_No, Consultant_ID, Record_Entry_Date, Associate_ID, Sales_Amount, Currency_Code, Exchange_Rate, Commission_Amount_Ca, Lead_ID, Sale_Date from Commission_Earning

end
GO
