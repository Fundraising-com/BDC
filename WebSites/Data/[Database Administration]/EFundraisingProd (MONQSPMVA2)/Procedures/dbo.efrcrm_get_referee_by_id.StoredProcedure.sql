USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_referee_by_id]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Referee
CREATE PROCEDURE [dbo].[efrcrm_get_referee_by_id] @Referee_Id int AS
begin

select Referee_Id, Lead_Id, Entry_Date, First_Name, Last_Name, Email, Phone_Number, Is_Entered from Referee where Referee_Id=@Referee_Id

end
GO
