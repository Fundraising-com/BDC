USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_contacts]    Script Date: 02/14/2014 13:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Contacts
CREATE PROCEDURE [dbo].[efrcrm_update_contacts] @Contact_ID int, @First_Name varchar(50), @Last_Name varchar(50), @Phone_Number varchar(20), @Phone_Ext varchar(10), @Street_Address varchar(20), @City varchar(20), @State_Code varchar(10), @Country_Code varchar(10), @Zip_Code varchar(10), @Comments varchar(100) AS
begin

update Contacts set First_Name=@First_Name, Last_Name=@Last_Name, Phone_Number=@Phone_Number, Phone_Ext=@Phone_Ext, Street_Address=@Street_Address, City=@City, State_Code=@State_Code, Country_Code=@Country_Code, Zip_Code=@Zip_Code, Comments=@Comments where Contact_ID=@Contact_ID

end
GO
