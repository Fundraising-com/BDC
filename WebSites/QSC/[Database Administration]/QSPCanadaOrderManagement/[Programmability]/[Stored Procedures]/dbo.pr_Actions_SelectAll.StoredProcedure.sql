USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Actions_SelectAll]    Script Date: 06/07/2017 09:19:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'Actions'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Actions_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Instance],
	[Description],
	[ReponsibleDeptInstance],
	[IsNotifyPublisherPrint],
	[IsActionUserUpdatable]
FROM [dbo].[Action]
ORDER BY 
	[Instance] ASC
GO
