USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_supporter_by_id]    Script Date: 02/14/2014 13:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Supporter
CREATE PROCEDURE [dbo].[efrcrm_get_efo_supporter_by_id] @Supporter_ID int AS
begin

select Supporter_ID, Name, Participant_ID, Email, Is_Email_Good, Is_Active, Comments, Email_Sent, Is_Deletable, Relation from EFO_Supporter where Supporter_ID=@Supporter_ID

end
GO
