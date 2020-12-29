USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_advertising_support_contact]    Script Date: 02/14/2014 13:07:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Advertising_Support_Contact
CREATE PROCEDURE [dbo].[efrcrm_update_advertising_support_contact] @Advertising_Support_Contact_ID int, @Advertising_Support_ID int, @First_Name varchar(35), @Last_Name varchar(35), @Phone_Number varchar(25), @Fax_Number varchar(25), @Email varchar(50) AS
begin

update Advertising_Support_Contact set Advertising_Support_ID=@Advertising_Support_ID, First_Name=@First_Name, Last_Name=@Last_Name, Phone_Number=@Phone_Number, Fax_Number=@Fax_Number, Email=@Email where Advertising_Support_Contact_ID=@Advertising_Support_Contact_ID

end
GO
