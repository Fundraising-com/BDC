USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_del_FieldManager]    Script Date: 06/07/2017 09:33:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_del_FieldManager]
	@FMID varchar(4)
AS

UPDATE 
	[dbo].[FieldManager] 
   SET 
	[DeletedTF] = 1
WHERE 
	[FMID]      = @FMID
GO
