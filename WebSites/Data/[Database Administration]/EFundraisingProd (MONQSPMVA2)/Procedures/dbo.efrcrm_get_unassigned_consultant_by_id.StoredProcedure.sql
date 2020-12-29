USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_unassigned_consultant_by_id]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Unassigned_Consultant
CREATE PROCEDURE [dbo].[efrcrm_get_unassigned_consultant_by_id] @Lead_ID int AS
begin

select Lead_ID, Old_Consultant_ID, New_Consultant_ID, Unassigned_Date, Unassignation_ID from Unassigned_Consultant where Lead_ID=@Lead_ID

end
GO
