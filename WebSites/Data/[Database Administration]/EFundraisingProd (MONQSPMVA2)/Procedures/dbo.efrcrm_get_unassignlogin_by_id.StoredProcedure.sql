USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_unassignlogin_by_id]    Script Date: 02/14/2014 13:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for UnAssignLogin
CREATE PROCEDURE [dbo].[efrcrm_get_unassignlogin_by_id] @UnAssignLogin_Id int AS
begin

select UnAssignLogin_Id, User_Name, Consultant_Id, Lead_Id, Unassignment_TimeStamp from UnAssignLogin where UnAssignLogin_Id=@UnAssignLogin_Id

end
GO
