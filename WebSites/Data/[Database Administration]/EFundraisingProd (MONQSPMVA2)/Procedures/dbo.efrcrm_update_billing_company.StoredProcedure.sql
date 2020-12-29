USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_billing_company]    Script Date: 02/14/2014 13:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Billing_Company
CREATE PROCEDURE [dbo].[efrcrm_update_billing_company] @Billing_Company_ID int, @Billing_Company_Code varchar(20), @Billing_Company_Name varchar(50), @Street_Address varchar(100), @City_Name varchar(50), @State_Code varchar(10), @Zip_Code varchar(10), @Country_Code varchar(10), @Telephone_Number varchar(20), @Email varchar(50), @Web varchar(50), @Logo text, @Invoice_Title varchar(20), @Invoice_Footer text AS
begin

update Billing_Company set Billing_Company_Code=@Billing_Company_Code, Billing_Company_Name=@Billing_Company_Name, Street_Address=@Street_Address, City_Name=@City_Name, State_Code=@State_Code, Zip_Code=@Zip_Code, Country_Code=@Country_Code, Telephone_Number=@Telephone_Number, Email=@Email, Web=@Web, Logo=@Logo, Invoice_Title=@Invoice_Title, Invoice_Footer=@Invoice_Footer where Billing_Company_ID=@Billing_Company_ID

end
GO
