USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_FieldManager_Email_List]    Script Date: 06/07/2017 09:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_FieldManager_Email_List] AS

SELECT
	[FMID],
	[FirstName],
	[LastName],
	[MiddleInitial],
	[Email]
  FROM 
	[dbo].[FieldManager]
 WHERE
	[DeletedTF] <> 1
ORDER BY
	[LastName] ASC
	,[FirstName] ASC
	,[MiddleInitial] ASC
	,[FMID] ASC
GO
