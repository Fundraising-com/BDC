USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_SystemUsers]    Script Date: 06/07/2017 09:33:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[pr_get_SystemUsers] AS

select 
	Instance, 
	FirstName, 
	LastName 
from 
	dbo.CUserProfile
where
	CUserProfileStatusId = 1 and
	Deleted_TF <> 1
order by
	Instance asc
GO
