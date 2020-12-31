USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProblemCode_Delete]    Script Date: 06/07/2017 09:20:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will delete an existing row from the table 'ProblemCode'
-- using the Primary Key. 
-- Gets: @iInstance int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_ProblemCode_Delete]
	@iInstance int
AS
SET NOCOUNT ON
-- DELETE an existing row from the table.
DELETE FROM [dbo].[ProblemCode]
WHERE
	[Instance] = @iInstance
GO
