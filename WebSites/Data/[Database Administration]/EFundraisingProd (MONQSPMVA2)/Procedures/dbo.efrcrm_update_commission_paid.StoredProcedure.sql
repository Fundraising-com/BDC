USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_commission_paid]    Script Date: 02/14/2014 13:07:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Commission_Paid
CREATE PROCEDURE [dbo].[efrcrm_update_commission_paid] @Commission_Year smallint, @Commission_Month smallint, @Consultant_ID int, @Sales_ID int, @AR_Status_ID int, @Total_Card_Sold int, @Sales_Amount decimal, @Consultant_Commission_Amount decimal AS
begin

update Commission_Paid set Commission_Month=@Commission_Month, Consultant_ID=@Consultant_ID, Sales_ID=@Sales_ID, AR_Status_ID=@AR_Status_ID, Total_Card_Sold=@Total_Card_Sold, Sales_Amount=@Sales_Amount, Consultant_Commission_Amount=@Consultant_Commission_Amount where Commission_Year=@Commission_Year

end
GO
