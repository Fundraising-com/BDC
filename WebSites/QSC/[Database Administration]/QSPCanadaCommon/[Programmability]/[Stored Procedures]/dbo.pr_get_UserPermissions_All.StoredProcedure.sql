USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_UserPermissions_All]    Script Date: 06/07/2017 09:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_UserPermissions_All] AS 

  select 
	CUP.FirstName, 
	CUP.LastName, 
	CUP.Instance, 
	PERMS.PName
    from 
	dbo.UserPermissions PERMS
	LEFT JOIN dbo.CUserProfile CUP 
	ON PERMS.ProfileID = CUP.Instance
   where 
	PERMS.DeletedTF <> 1 
	AND CUP.Deleted_Tf <> 1
	AND CUP.DefectorTF <> 1
order by 
	PERMS.PNAME ASC,
	PERMS.ProfileId asc
GO
