USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_message]    Script Date: 02/14/2014 13:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Message
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_message] @Message_ID int OUTPUT, @Participant_ID int, @Is_Read bit, @Date_Sent smalldatetime, @Date_Received smalldatetime, @From_Name varchar(50), @From_Email varchar(50), @To_Name varchar(50), @To_Email varchar(50), @Subject varchar(20), @Body text, @Content_Type varchar(20) AS
begin

insert into EFO_Message(Participant_ID, Is_Read, Date_Sent, Date_Received, From_Name, From_Email, To_Name, To_Email, Subject, Body, Content_Type) values(@Participant_ID, @Is_Read, @Date_Sent, @Date_Received, @From_Name, @From_Email, @To_Name, @To_Email, @Subject, @Body, @Content_Type)

select @Message_ID = SCOPE_IDENTITY()

end
GO
