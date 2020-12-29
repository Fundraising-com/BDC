USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_participant]    Script Date: 02/14/2014 13:07:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_insert_participant] @Participant_id int OUTPUT, @First_name varchar(50), @Last_name varchar(50) AS
begin

insert into Participant(First_name, Last_name, Create_date) values(@First_name, @Last_name, getdate())

select @Participant_id = SCOPE_IDENTITY()

end
GO
