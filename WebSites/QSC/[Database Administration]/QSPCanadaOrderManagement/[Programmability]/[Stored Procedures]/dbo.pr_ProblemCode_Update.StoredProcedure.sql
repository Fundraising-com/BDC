USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProblemCode_Update]    Script Date: 06/07/2017 09:20:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'ProblemCode'
-- Gets: @iInstance int
-- Gets: @sDescription varchar(50)
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_ProblemCode_Update]
	@iInstance int,
	@sDescription varchar(50)
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[ProblemCode]
SET 
	[Description] = @sDescription
WHERE
	[Instance] = @iInstance
GO
