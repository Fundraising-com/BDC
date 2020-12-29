USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_mailing_name]    Script Date: 02/14/2014 13:08:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Mailing_Name
CREATE PROCEDURE [dbo].[efrcrm_update_mailing_name] @Mailing_Name_ID int, @List_Name varchar(25), @List_ID int, @Contact_Name varchar(50), @Title varchar(35), @School_Name varchar(60), @School_Address varchar(70), @City varchar(30), @State_Code varchar(4), @Zip varchar(15), @Phone_Number varchar(15), @Fax_Number varchar(15), @Email varchar(50), @School_Type varchar(2) AS
begin

update Mailing_Name set List_Name=@List_Name, List_ID=@List_ID, Contact_Name=@Contact_Name, Title=@Title, School_Name=@School_Name, School_Address=@School_Address, City=@City, State_Code=@State_Code, Zip=@Zip, Phone_Number=@Phone_Number, Fax_Number=@Fax_Number, Email=@Email, School_Type=@School_Type where Mailing_Name_ID=@Mailing_Name_ID

end
GO
