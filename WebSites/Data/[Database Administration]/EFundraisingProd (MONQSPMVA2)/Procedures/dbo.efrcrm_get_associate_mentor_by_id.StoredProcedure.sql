USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_associate_mentor_by_id]    Script Date: 02/14/2014 13:03:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Associate_Mentor
CREATE PROCEDURE [dbo].[efrcrm_get_associate_mentor_by_id] @Associate_ID int AS
begin

select Associate_ID, Mentor_ID, Start_Date, End_Date from Associate_Mentor where Associate_ID=@Associate_ID

end
GO
