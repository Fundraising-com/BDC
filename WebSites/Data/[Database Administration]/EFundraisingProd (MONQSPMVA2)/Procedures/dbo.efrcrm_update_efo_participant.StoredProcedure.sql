USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_participant]    Script Date: 02/14/2014 13:07:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Participant
CREATE PROCEDURE [dbo].[efrcrm_update_efo_participant] @Participant_ID int, @Name varchar(50), @Campaign_ID int, @Email varchar(50), @Comments varchar(150), @Email_Sent bit, @Is_Active bit, @Is_Default bit, @Is_Deletable bit AS
begin

update EFO_Participant set Name=@Name, Campaign_ID=@Campaign_ID, Email=@Email, Comments=@Comments, Email_Sent=@Email_Sent, Is_Active=@Is_Active, Is_Default=@Is_Default, Is_Deletable=@Is_Deletable where Participant_ID=@Participant_ID

end
GO
