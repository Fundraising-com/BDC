USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_unassignlogins]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for UnAssignLogin
CREATE PROCEDURE [dbo].[efrcrm_get_unassignlogins] AS
begin

select UnAssignLogin_Id, User_Name, Consultant_Id, Lead_Id, Unassignment_TimeStamp from UnAssignLogin

end
GO
