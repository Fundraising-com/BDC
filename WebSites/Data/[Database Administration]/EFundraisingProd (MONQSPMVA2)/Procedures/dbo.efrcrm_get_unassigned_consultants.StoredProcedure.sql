USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_unassigned_consultants]    Script Date: 02/14/2014 13:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Unassigned_Consultant
CREATE PROCEDURE [dbo].[efrcrm_get_unassigned_consultants] AS
begin

select Lead_ID, Old_Consultant_ID, New_Consultant_ID, Unassigned_Date, Unassignation_ID from Unassigned_Consultant

end
GO
