USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_messages]    Script Date: 02/14/2014 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Message
CREATE PROCEDURE [dbo].[efrcrm_get_efo_messages] AS
begin

select Message_ID, Participant_ID, Is_Read, Date_Sent, Date_Received, From_Name, From_Email, To_Name, To_Email, Subject, Body, Content_Type from EFO_Message

end
GO
