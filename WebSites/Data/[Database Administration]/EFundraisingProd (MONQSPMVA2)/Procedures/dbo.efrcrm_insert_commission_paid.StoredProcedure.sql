USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_commission_paid]    Script Date: 02/14/2014 13:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Commission_Paid
CREATE PROCEDURE [dbo].[efrcrm_insert_commission_paid] @Commission_Year int OUTPUT, @Commission_Month smallint, @Consultant_ID int, @Sales_ID int, @AR_Status_ID int, @Total_Card_Sold int, @Sales_Amount decimal, @Consultant_Commission_Amount decimal AS
begin

insert into Commission_Paid(Commission_Month, Consultant_ID, Sales_ID, AR_Status_ID, Total_Card_Sold, Sales_Amount, Consultant_Commission_Amount) values(@Commission_Month, @Consultant_ID, @Sales_ID, @AR_Status_ID, @Total_Card_Sold, @Sales_Amount, @Consultant_Commission_Amount)

select @Commission_Year = SCOPE_IDENTITY()

end
GO
