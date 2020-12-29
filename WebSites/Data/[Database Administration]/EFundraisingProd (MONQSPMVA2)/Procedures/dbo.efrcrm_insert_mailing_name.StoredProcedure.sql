USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_mailing_name]    Script Date: 02/14/2014 13:07:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Mailing_Name
CREATE PROCEDURE [dbo].[efrcrm_insert_mailing_name] @Mailing_Name_ID int OUTPUT, @List_Name varchar(25), @List_ID int, @Contact_Name varchar(50), @Title varchar(35), @School_Name varchar(60), @School_Address varchar(70), @City varchar(30), @State_Code varchar(4), @Zip varchar(15), @Phone_Number varchar(15), @Fax_Number varchar(15), @Email varchar(50), @School_Type varchar(2) AS
begin

insert into Mailing_Name(List_Name, List_ID, Contact_Name, Title, School_Name, School_Address, City, State_Code, Zip, Phone_Number, Fax_Number, Email, School_Type) values(@List_Name, @List_ID, @Contact_Name, @Title, @School_Name, @School_Address, @City, @State_Code, @Zip, @Phone_Number, @Fax_Number, @Email, @School_Type)

select @Mailing_Name_ID = SCOPE_IDENTITY()

end
GO
