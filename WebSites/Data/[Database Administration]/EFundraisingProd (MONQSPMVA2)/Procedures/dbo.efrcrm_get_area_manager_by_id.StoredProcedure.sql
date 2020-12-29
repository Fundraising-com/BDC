USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_area_manager_by_id]    Script Date: 02/14/2014 13:03:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Area_Manager
CREATE PROCEDURE [dbo].[efrcrm_get_area_manager_by_id] @Area_Manager_ID int AS
begin

select Area_Manager_ID, Area_Manager_Name from Area_Manager where Area_Manager_ID=@Area_Manager_ID

end
GO
