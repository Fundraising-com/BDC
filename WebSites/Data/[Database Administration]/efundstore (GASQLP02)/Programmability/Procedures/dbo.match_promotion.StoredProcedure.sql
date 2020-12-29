USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[match_promotion]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	May 13, 2004
Description:	This stored procedure ?
*/
CREATE PROCEDURE [dbo].[match_promotion]
	@strScriptName VARCHAR(100)
AS
/* TODO: point to the local version of the promotion table */
	SELECT
		promotion_id 
	FROM 	promotion 
	WHERE
		script_name = @strScriptName
GO
