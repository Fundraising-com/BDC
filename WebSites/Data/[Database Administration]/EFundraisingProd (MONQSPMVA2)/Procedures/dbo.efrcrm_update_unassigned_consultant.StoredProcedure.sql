USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_unassigned_consultant]    Script Date: 02/14/2014 13:08:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Unassigned_Consultant
CREATE PROCEDURE [dbo].[efrcrm_update_unassigned_consultant] @Lead_ID int, @Old_Consultant_ID int, @New_Consultant_ID int, @Unassigned_Date datetime, @Unassignation_ID int AS
begin

update Unassigned_Consultant set Old_Consultant_ID=@Old_Consultant_ID, New_Consultant_ID=@New_Consultant_ID, Unassigned_Date=@Unassigned_Date, Unassignation_ID=@Unassignation_ID where Lead_ID=@Lead_ID

end
GO
