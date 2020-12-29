USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[leads_to_reassign]    Script Date: 02/14/2014 13:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	March 18, 2004
Description:	This stored procedure will re-assignment the leads to a consultant or NULL.  This stored procedure 
		is the last part of the Lead re-assignment tool.
*/
CREATE PROCEDURE [dbo].[leads_to_reassign]
	@strLeadIDs VARCHAR(7900)
	, @intConsultantID INT = NULL
AS
DECLARE @strSQL VARCHAR(8000)
SET @strSQL = 'UPDATE Lead SET Consultant_ID = ' + CONVERT( VARCHAR(15), @intConsultantID ) + ' WHERE Lead_ID IN ( ' + @strLeadIDs + ' )'
EXEC( @strSQL )
--PRINT @strSQL
GO
