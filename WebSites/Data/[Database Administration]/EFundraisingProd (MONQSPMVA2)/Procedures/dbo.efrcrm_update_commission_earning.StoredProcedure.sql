USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_commission_earning]    Script Date: 02/14/2014 13:07:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Commission_Earning
CREATE PROCEDURE [dbo].[efrcrm_update_commission_earning] @Commission_Earning_ID int, @Sales_ID int, @Product_Description varchar(255), @Payment_Amount decimal, @Payment_Entry_Date datetime, @Commission_Amount varchar(125), @Commission_Rate decimal, @Payment_No int, @Consultant_ID int, @Record_Entry_Date datetime, @Associate_ID int, @Sales_Amount decimal, @Currency_Code varchar(10), @Exchange_Rate decimal, @Commission_Amount_Ca varchar(125), @Lead_ID int, @Sale_Date datetime AS
begin

update Commission_Earning set Sales_ID=@Sales_ID, Product_Description=@Product_Description, Payment_Amount=@Payment_Amount, Payment_Entry_Date=@Payment_Entry_Date, Commission_Amount=@Commission_Amount, Commission_Rate=@Commission_Rate, Payment_No=@Payment_No, Consultant_ID=@Consultant_ID, Record_Entry_Date=@Record_Entry_Date, Associate_ID=@Associate_ID, Sales_Amount=@Sales_Amount, Currency_Code=@Currency_Code, Exchange_Rate=@Exchange_Rate, Commission_Amount_Ca=@Commission_Amount_Ca, Lead_ID=@Lead_ID, Sale_Date=@Sale_Date where Commission_Earning_ID=@Commission_Earning_ID

end
GO
