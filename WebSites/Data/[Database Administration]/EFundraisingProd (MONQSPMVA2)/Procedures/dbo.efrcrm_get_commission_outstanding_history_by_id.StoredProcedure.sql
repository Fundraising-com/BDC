USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_commission_outstanding_history_by_id]    Script Date: 02/14/2014 13:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Commission_Outstanding_History
CREATE PROCEDURE [dbo].[efrcrm_get_commission_outstanding_history_by_id] @Sales_ID int AS
begin

select Sales_ID, Month, Year, Consultant_ID, Sales_Date, Shipped_Date, Status, Payment_Term, First_Name, Last_Name, Organization, Day_Phone, Outstanding_Amount, Currency_Code, Outstanding_Commission from Commission_Outstanding_History where Sales_ID=@Sales_ID

end
GO
