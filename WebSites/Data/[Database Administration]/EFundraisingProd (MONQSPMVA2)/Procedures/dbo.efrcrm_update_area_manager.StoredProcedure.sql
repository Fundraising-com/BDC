USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_area_manager]    Script Date: 02/14/2014 13:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Area_Manager
CREATE PROCEDURE [dbo].[efrcrm_update_area_manager] @Area_Manager_ID int, @Area_Manager_Name varchar(25) AS
begin

update Area_Manager set Area_Manager_Name=@Area_Manager_Name where Area_Manager_ID=@Area_Manager_ID

end
GO
