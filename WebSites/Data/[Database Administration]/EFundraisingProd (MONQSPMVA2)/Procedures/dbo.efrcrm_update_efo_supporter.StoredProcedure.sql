USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_supporter]    Script Date: 02/14/2014 13:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Supporter
CREATE PROCEDURE [dbo].[efrcrm_update_efo_supporter] @Supporter_ID int, @Name varchar(50), @Participant_ID int, @Email varchar(75), @Is_Email_Good bit, @Is_Active bit, @Comments varchar(150), @Email_Sent bit, @Is_Deletable bit, @Relation varchar(25) AS
begin

update EFO_Supporter set Name=@Name, Participant_ID=@Participant_ID, Email=@Email, Is_Email_Good=@Is_Email_Good, Is_Active=@Is_Active, Comments=@Comments, Email_Sent=@Email_Sent, Is_Deletable=@Is_Deletable, Relation=@Relation where Supporter_ID=@Supporter_ID

end
GO
