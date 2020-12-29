USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_participants]    Script Date: 02/14/2014 13:04:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Participant
CREATE PROCEDURE [dbo].[efrcrm_get_efo_participants] AS
begin

select Participant_ID, Name, Campaign_ID, Email, Comments, Email_Sent, Is_Active, Is_Default, Is_Deletable from EFO_Participant

end
GO
