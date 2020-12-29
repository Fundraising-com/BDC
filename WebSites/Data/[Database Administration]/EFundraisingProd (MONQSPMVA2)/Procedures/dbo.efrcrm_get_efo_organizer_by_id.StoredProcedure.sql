USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_organizer_by_id]    Script Date: 02/14/2014 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Organizer
CREATE PROCEDURE [dbo].[efrcrm_get_efo_organizer_by_id] @Organizer_ID int AS
begin

select Organizer_ID, Name, User_Name, Password, Title, Email, Best_Time_To_Call, Evening_Phone, Day_Phone, Fax_Number, Entry_Date, Comments, Organization_ID, School_ID from EFO_Organizer where Organizer_ID=@Organizer_ID

end
GO
