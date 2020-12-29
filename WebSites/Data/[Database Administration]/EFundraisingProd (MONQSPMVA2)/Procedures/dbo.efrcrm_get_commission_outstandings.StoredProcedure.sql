USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_commission_outstandings]    Script Date: 02/14/2014 13:04:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Commission_Outstanding
CREATE PROCEDURE [dbo].[efrcrm_get_commission_outstandings] AS
begin

select Sales_ID, Consultant_ID, Sales_Date, Shipped_Date, Status, Payment_Term, First_Name, Last_Name, Organization, Day_Phone, Outstanding_Amount, Currency_Code, Outstanding_Commission from Commission_Outstanding

end
GO
