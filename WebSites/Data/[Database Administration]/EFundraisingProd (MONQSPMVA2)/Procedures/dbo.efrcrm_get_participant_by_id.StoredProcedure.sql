USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_participant_by_id]    Script Date: 02/14/2014 13:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_participant_by_id] @Participant_id int AS
begin

select Participant_id, First_name, Last_name, Create_date from Participant where Participant_id=@Participant_id

end
GO
