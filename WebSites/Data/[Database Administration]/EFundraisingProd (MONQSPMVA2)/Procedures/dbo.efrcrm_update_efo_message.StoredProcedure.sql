USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_message]    Script Date: 02/14/2014 13:07:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Message
CREATE PROCEDURE [dbo].[efrcrm_update_efo_message] @Message_ID int, @Participant_ID int, @Is_Read bit, @Date_Sent smalldatetime, @Date_Received smalldatetime, @From_Name varchar(50), @From_Email varchar(50), @To_Name varchar(50), @To_Email varchar(50), @Subject varchar(20), @Body text, @Content_Type varchar(20) AS
begin

update EFO_Message set Participant_ID=@Participant_ID, Is_Read=@Is_Read, Date_Sent=@Date_Sent, Date_Received=@Date_Received, From_Name=@From_Name, From_Email=@From_Email, To_Name=@To_Name, To_Email=@To_Email, Subject=@Subject, Body=@Body, Content_Type=@Content_Type where Message_ID=@Message_ID

end
GO
