USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_participant_by_id]    Script Date: 02/14/2014 13:04:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Participant
CREATE PROCEDURE [dbo].[efrcrm_get_efo_participant_by_id] @Participant_ID int AS
begin

select Participant_ID, Name, Campaign_ID, Email, Comments, Email_Sent, Is_Active, Is_Default, Is_Deletable from EFO_Participant where Participant_ID=@Participant_ID

end
GO
