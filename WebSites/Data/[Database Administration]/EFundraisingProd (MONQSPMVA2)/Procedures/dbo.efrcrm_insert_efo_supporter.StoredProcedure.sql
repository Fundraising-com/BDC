USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_supporter]    Script Date: 02/14/2014 13:06:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Supporter
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_supporter] @Supporter_ID int OUTPUT, @Name varchar(50), @Participant_ID int, @Email varchar(75), @Is_Email_Good bit, @Is_Active bit, @Comments varchar(150), @Email_Sent bit, @Is_Deletable bit, @Relation varchar(25) AS
begin

insert into EFO_Supporter(Name, Participant_ID, Email, Is_Email_Good, Is_Active, Comments, Email_Sent, Is_Deletable, Relation) values(@Name, @Participant_ID, @Email, @Is_Email_Good, @Is_Active, @Comments, @Email_Sent, @Is_Deletable, @Relation)

select @Supporter_ID = SCOPE_IDENTITY()

end
GO
