USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_organizer]    Script Date: 02/14/2014 13:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Organizer
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_organizer] @Organizer_ID int OUTPUT, @Name varchar(50), @User_Name varchar(50), @Password varchar(15), @Title varchar(15), @Email varchar(75), @Best_Time_To_Call varchar(20), @Evening_Phone varchar(15), @Day_Phone varchar(15), @Fax_Number varchar(15), @Entry_Date smalldatetime, @Comments varchar(150), @Organization_ID int, @School_ID int AS
begin

insert into EFO_Organizer(Name, User_Name, Password, Title, Email, Best_Time_To_Call, Evening_Phone, Day_Phone, Fax_Number, Entry_Date, Comments, Organization_ID, School_ID) values(@Name, @User_Name, @Password, @Title, @Email, @Best_Time_To_Call, @Evening_Phone, @Day_Phone, @Fax_Number, @Entry_Date, @Comments, @Organization_ID, @School_ID)

select @Organizer_ID = SCOPE_IDENTITY()

end
GO
