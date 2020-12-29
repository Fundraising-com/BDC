USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_billing_company_by_id]    Script Date: 02/14/2014 13:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Billing_Company
CREATE  PROCEDURE [dbo].[efrcrm_get_billing_company_by_id] @Billing_Company_ID int AS
begin

select Billing_Company_ID, Billing_Company_Code, Billing_Company_Name, Street_Address, City_Name, State_Code, Zip_Code, Country_Code, Telephone_Number, Email, Web, Logo, Invoice_Title, Invoice_Footer, culture_id from Billing_Company where Billing_Company_ID=@Billing_Company_ID

end
GO
