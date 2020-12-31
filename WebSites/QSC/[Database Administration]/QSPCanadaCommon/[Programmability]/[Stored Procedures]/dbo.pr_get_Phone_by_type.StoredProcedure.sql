USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Phone_by_type]    Script Date: 06/07/2017 09:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_Phone_by_type]
	@type int,
	@PhoneListId int
AS

SELECT 
	*
FROM 
	Phone
WHERE 
	Type = @type
	AND PhoneListId = @PhoneListId
GO
