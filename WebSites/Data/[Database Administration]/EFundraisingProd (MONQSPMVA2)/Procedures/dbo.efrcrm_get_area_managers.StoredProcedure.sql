USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_area_managers]    Script Date: 02/14/2014 13:03:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Area_Manager
CREATE PROCEDURE [dbo].[efrcrm_get_area_managers] AS
begin

select Area_Manager_ID, Area_Manager_Name from Area_Manager

end
GO
