USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_advertising_support_contact]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Advertising_Support_Contact
CREATE PROCEDURE [dbo].[efrcrm_insert_advertising_support_contact] @Advertising_Support_Contact_ID int OUTPUT, @Advertising_Support_ID int, @First_Name varchar(35), @Last_Name varchar(35), @Phone_Number varchar(25), @Fax_Number varchar(25), @Email varchar(50) AS
begin

insert into Advertising_Support_Contact(Advertising_Support_ID, First_Name, Last_Name, Phone_Number, Fax_Number, Email) values(@Advertising_Support_ID, @First_Name, @Last_Name, @Phone_Number, @Fax_Number, @Email)

select @Advertising_Support_Contact_ID = SCOPE_IDENTITY()

end
GO
