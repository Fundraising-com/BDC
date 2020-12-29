USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_participant]    Script Date: 02/14/2014 13:06:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Participant
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_participant] @Participant_ID int OUTPUT, @Name varchar(50), @Campaign_ID int, @Email varchar(50), @Comments varchar(150), @Email_Sent bit, @Is_Active bit, @Is_Default bit, @Is_Deletable bit AS
begin

insert into EFO_Participant(Name, Campaign_ID, Email, Comments, Email_Sent, Is_Active, Is_Default, Is_Deletable) values(@Name, @Campaign_ID, @Email, @Comments, @Email_Sent, @Is_Active, @Is_Default, @Is_Deletable)

select @Participant_ID = SCOPE_IDENTITY()

end
GO
