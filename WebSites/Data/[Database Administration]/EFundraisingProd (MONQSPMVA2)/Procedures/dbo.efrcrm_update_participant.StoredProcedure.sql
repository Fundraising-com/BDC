USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_participant]    Script Date: 02/14/2014 13:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_update_participant] @Participant_id int, @First_name varchar(50), @Last_name varchar(50), @Create_date datetime AS
begin

update Participant set First_name=@First_name, Last_name=@Last_name, Create_date=@Create_date where Participant_id=@Participant_id

end
GO
