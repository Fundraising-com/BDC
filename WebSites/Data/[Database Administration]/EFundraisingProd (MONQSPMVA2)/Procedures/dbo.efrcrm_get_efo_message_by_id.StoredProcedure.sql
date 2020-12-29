USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_message_by_id]    Script Date: 02/14/2014 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Message
CREATE PROCEDURE [dbo].[efrcrm_get_efo_message_by_id] @Message_ID int AS
begin

select Message_ID, Participant_ID, Is_Read, Date_Sent, Date_Received, From_Name, From_Email, To_Name, To_Email, Subject, Body, Content_Type from EFO_Message where Message_ID=@Message_ID

end
GO
