USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_billing_company]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Billing_Company
CREATE PROCEDURE [dbo].[efrcrm_insert_billing_company] @Billing_Company_ID int OUTPUT, @Billing_Company_Code varchar(20), @Billing_Company_Name varchar(50), @Street_Address varchar(100), @City_Name varchar(50), @State_Code varchar(10), @Zip_Code varchar(10), @Country_Code varchar(10), @Telephone_Number varchar(20), @Email varchar(50), @Web varchar(50), @Logo text, @Invoice_Title varchar(20), @Invoice_Footer text AS
begin

insert into Billing_Company(Billing_Company_Code, Billing_Company_Name, Street_Address, City_Name, State_Code, Zip_Code, Country_Code, Telephone_Number, Email, Web, Logo, Invoice_Title, Invoice_Footer) values(@Billing_Company_Code, @Billing_Company_Name, @Street_Address, @City_Name, @State_Code, @Zip_Code, @Country_Code, @Telephone_Number, @Email, @Web, @Logo, @Invoice_Title, @Invoice_Footer)

select @Billing_Company_ID = SCOPE_IDENTITY()

end
GO
