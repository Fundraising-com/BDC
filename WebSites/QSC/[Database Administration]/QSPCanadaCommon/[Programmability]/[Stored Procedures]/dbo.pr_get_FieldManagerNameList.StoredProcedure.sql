USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_FieldManagerNameList]    Script Date: 06/07/2017 09:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_FieldManagerNameList] AS

SELECT
	[FMID],
	[FirstName],
	[LastName],
	[MiddleInitial]
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
