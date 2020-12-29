USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_commission_earning]    Script Date: 02/14/2014 13:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Commission_Earning
CREATE PROCEDURE [dbo].[efrcrm_insert_commission_earning] @Commission_Earning_ID int OUTPUT, @Sales_ID int, @Product_Description varchar(255), @Payment_Amount decimal, @Payment_Entry_Date datetime, @Commission_Amount varchar(125), @Commission_Rate decimal, @Payment_No int, @Consultant_ID int, @Record_Entry_Date datetime, @Associate_ID int, @Sales_Amount decimal, @Currency_Code varchar(10), @Exchange_Rate decimal, @Commission_Amount_Ca varchar(125), @Lead_ID int, @Sale_Date datetime AS
begin

insert into Commission_Earning(Sales_ID, Product_Description, Payment_Amount, Payment_Entry_Date, Commission_Amount, Commission_Rate, Payment_No, Consultant_ID, Record_Entry_Date, Associate_ID, Sales_Amount, Currency_Code, Exchange_Rate, Commission_Amount_Ca, Lead_ID, Sale_Date) values(@Sales_ID, @Product_Description, @Payment_Amount, @Payment_Entry_Date, @Commission_Amount, @Commission_Rate, @Payment_No, @Consultant_ID, @Record_Entry_Date, @Associate_ID, @Sales_Amount, @Currency_Code, @Exchange_Rate, @Commission_Amount_Ca, @Lead_ID, @Sale_Date)

select @Commission_Earning_ID = SCOPE_IDENTITY()

end
GO
