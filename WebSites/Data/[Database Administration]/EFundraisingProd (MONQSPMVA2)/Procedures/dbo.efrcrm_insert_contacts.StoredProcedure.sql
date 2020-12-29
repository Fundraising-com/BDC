USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_contacts]    Script Date: 02/14/2014 13:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Contacts
CREATE PROCEDURE [dbo].[efrcrm_insert_contacts] @Contact_ID int OUTPUT, @First_Name varchar(50), @Last_Name varchar(50), @Phone_Number varchar(20), @Phone_Ext varchar(10), @Street_Address varchar(20), @City varchar(20), @State_Code varchar(10), @Country_Code varchar(10), @Zip_Code varchar(10), @Comments varchar(100) AS
begin

insert into Contacts(First_Name, Last_Name, Phone_Number, Phone_Ext, Street_Address, City, State_Code, Country_Code, Zip_Code, Comments) values(@First_Name, @Last_Name, @Phone_Number, @Phone_Ext, @Street_Address, @City, @State_Code, @Country_Code, @Zip_Code, @Comments)

select @Contact_ID = SCOPE_IDENTITY()

end
GO
