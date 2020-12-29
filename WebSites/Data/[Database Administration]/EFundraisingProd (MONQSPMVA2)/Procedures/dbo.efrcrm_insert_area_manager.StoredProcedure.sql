USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_area_manager]    Script Date: 02/14/2014 13:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Area_Manager
CREATE PROCEDURE [dbo].[efrcrm_insert_area_manager] @Area_Manager_ID int OUTPUT, @Area_Manager_Name varchar(25) AS
begin

insert into Area_Manager(Area_Manager_Name) values(@Area_Manager_Name)

select @Area_Manager_ID = SCOPE_IDENTITY()

end
GO
