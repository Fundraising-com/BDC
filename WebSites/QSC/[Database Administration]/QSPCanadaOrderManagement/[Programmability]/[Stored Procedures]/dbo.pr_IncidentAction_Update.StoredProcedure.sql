USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IncidentAction_Update]    Script Date: 06/07/2017 09:20:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'IncidentAction'
-- Gets: @iIncidentInstance int
-- Gets: @iActionInstance int
-- Gets: @sComments varchar(255)
-- Gets: @sUserIDCreated varchar(4)
-- Gets: @daDateCreated datetime
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_IncidentAction_Update]
	@iInstance int =0,
	@iIncidentInstance int =0,
	@iActionInstance int =0,
	@sComments varchar(500)= '',
	@sUserIDCreated varchar(4) = ''
AS

-- UPDATE an existing row in the table.
UPDATE [dbo].[IncidentAction]
SET 
	[Comments] = @sComments,
	[UserIDCreated] = @sUserIDCreated,
	[DateCreated] = getdate()
WHERE
	[Instance] = @iInstance
GO
