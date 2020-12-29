USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_unassigned_consultant_sale]    Script Date: 02/14/2014 13:08:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Unassigned_Consultant_Sale
CREATE PROCEDURE [dbo].[efrcrm_update_unassigned_consultant_sale] @Unassignation_ID int, @Sale_ID int, @Old_Consultant_ID int, @New_Consultant_ID int, @Unassigned_Date datetime AS
begin

update Unassigned_Consultant_Sale set Sale_ID=@Sale_ID, Old_Consultant_ID=@Old_Consultant_ID, New_Consultant_ID=@New_Consultant_ID, Unassigned_Date=@Unassigned_Date where Unassignation_ID=@Unassignation_ID

end
GO
