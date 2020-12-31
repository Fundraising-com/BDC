USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Action_SelectOne]    Script Date: 06/07/2017 09:19:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'Action'
-- based on the Primary Key.
-- Gets: @iInstance int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Action_SelectOne]
	@iInstance int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[Instance],
	[Description],
	[ReponsibleDeptInstance],
	[IsNotifyPublisherPrint],
	[IsActionUserUpdatable],
	[Message],
	[CommentsIsRequired]
FROM [dbo].[Action]
WHERE
	[Instance] = @iInstance
GO
