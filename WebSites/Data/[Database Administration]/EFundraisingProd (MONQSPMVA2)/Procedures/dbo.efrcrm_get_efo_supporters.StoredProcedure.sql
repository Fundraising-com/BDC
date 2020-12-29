USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_supporters]    Script Date: 02/14/2014 13:04:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Supporter
CREATE PROCEDURE [dbo].[efrcrm_get_efo_supporters] AS
begin

select Supporter_ID, Name, Participant_ID, Email, Is_Email_Good, Is_Active, Comments, Email_Sent, Is_Deletable, Relation from EFO_Supporter

end
GO
