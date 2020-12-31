USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ActionRules_SelectOne]    Script Date: 06/07/2017 09:19:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'ActionRules'
-- based on the Primary Key.
-- Gets: @iInstance int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_ActionRules_SelectOne]
	@iInstance int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[Instance],
	[ActionInstance],
	[Unique]
FROM [dbo].[ActionRules]
WHERE
	[Instance] = @iInstance
GO
