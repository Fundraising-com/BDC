USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_organizer]    Script Date: 02/14/2014 13:07:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Organizer
CREATE PROCEDURE [dbo].[efrcrm_update_efo_organizer] @Organizer_ID int, @Name varchar(50), @User_Name varchar(50), @Password varchar(15), @Title varchar(15), @Email varchar(75), @Best_Time_To_Call varchar(20), @Evening_Phone varchar(15), @Day_Phone varchar(15), @Fax_Number varchar(15), @Entry_Date smalldatetime, @Comments varchar(150), @Organization_ID int, @School_ID int AS
begin

update EFO_Organizer set Name=@Name, User_Name=@User_Name, Password=@Password, Title=@Title, Email=@Email, Best_Time_To_Call=@Best_Time_To_Call, Evening_Phone=@Evening_Phone, Day_Phone=@Day_Phone, Fax_Number=@Fax_Number, Entry_Date=@Entry_Date, Comments=@Comments, Organization_ID=@Organization_ID, School_ID=@School_ID where Organizer_ID=@Organizer_ID

end
GO
