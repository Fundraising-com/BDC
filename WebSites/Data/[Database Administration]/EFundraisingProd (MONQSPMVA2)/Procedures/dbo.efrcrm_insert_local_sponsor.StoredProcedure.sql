USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_local_sponsor]    Script Date: 02/14/2014 13:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Local_Sponsor
CREATE PROCEDURE [dbo].[efrcrm_insert_local_sponsor] @Brand_ID int OUTPUT, @Local_Sponsor_ID int, @Salutation varchar(10), @First_Name varchar(50), @Middle_Initial varchar(50), @Last_Name varchar(50), @Title varchar(50), @Street_Address varchar(100), @City_Name varchar(50), @State_Code varchar(10), @Zip_Code varchar(10), @Country_Code varchar(10), @Day_Phone varchar(20), @Day_Time_Call varchar(20), @Evening_Phone varchar(20), @Evening_Time_Call varchar(20), @Alternate_Phone varchar(50), @Fax_Number varchar(50), @Email varchar(50), @Approval_Date datetime, @Comment text, @Sponsor_Consultant_ID int, @Last_Contact datetime, @Local_Sponsor_Steps_Id int AS
begin

insert into Local_Sponsor(Local_Sponsor_ID, Salutation, First_Name, Middle_Initial, Last_Name, Title, Street_Address, City_Name, State_Code, Zip_Code, Country_Code, Day_Phone, Day_Time_Call, Evening_Phone, Evening_Time_Call, Alternate_Phone, Fax_Number, Email, Approval_Date, Comment, Sponsor_Consultant_ID, Last_Contact, Local_Sponsor_Steps_Id) values(@Local_Sponsor_ID, @Salutation, @First_Name, @Middle_Initial, @Last_Name, @Title, @Street_Address, @City_Name, @State_Code, @Zip_Code, @Country_Code, @Day_Phone, @Day_Time_Call, @Evening_Phone, @Evening_Time_Call, @Alternate_Phone, @Fax_Number, @Email, @Approval_Date, @Comment, @Sponsor_Consultant_ID, @Last_Contact, @Local_Sponsor_Steps_Id)

select @Brand_ID = SCOPE_IDENTITY()

end
GO
