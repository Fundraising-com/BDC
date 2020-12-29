USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_local_sponsor]    Script Date: 02/14/2014 13:08:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Local_Sponsor
CREATE PROCEDURE [dbo].[efrcrm_update_local_sponsor] @Brand_ID int, @Local_Sponsor_ID int, @Salutation varchar(10), @First_Name varchar(50), @Middle_Initial varchar(50), @Last_Name varchar(50), @Title varchar(50), @Street_Address varchar(100), @City_Name varchar(50), @State_Code varchar(10), @Zip_Code varchar(10), @Country_Code varchar(10), @Day_Phone varchar(20), @Day_Time_Call varchar(20), @Evening_Phone varchar(20), @Evening_Time_Call varchar(20), @Alternate_Phone varchar(50), @Fax_Number varchar(50), @Email varchar(50), @Approval_Date datetime, @Comment text, @Sponsor_Consultant_ID int, @Last_Contact datetime, @Local_Sponsor_Steps_Id int AS
begin

update Local_Sponsor set Local_Sponsor_ID=@Local_Sponsor_ID, Salutation=@Salutation, First_Name=@First_Name, Middle_Initial=@Middle_Initial, Last_Name=@Last_Name, Title=@Title, Street_Address=@Street_Address, City_Name=@City_Name, State_Code=@State_Code, Zip_Code=@Zip_Code, Country_Code=@Country_Code, Day_Phone=@Day_Phone, Day_Time_Call=@Day_Time_Call, Evening_Phone=@Evening_Phone, Evening_Time_Call=@Evening_Time_Call, Alternate_Phone=@Alternate_Phone, Fax_Number=@Fax_Number, Email=@Email, Approval_Date=@Approval_Date, Comment=@Comment, Sponsor_Consultant_ID=@Sponsor_Consultant_ID, Last_Contact=@Last_Contact, Local_Sponsor_Steps_Id=@Local_Sponsor_Steps_Id where Brand_ID=@Brand_ID

end
GO
