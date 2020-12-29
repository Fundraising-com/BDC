USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_associate_mentors]    Script Date: 02/14/2014 13:03:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Associate_Mentor
CREATE PROCEDURE [dbo].[efrcrm_get_associate_mentors] AS
begin

select Associate_ID, Mentor_ID, Start_Date, End_Date from Associate_Mentor

end
GO
