USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ErrorMessage_SelectAll]    Script Date: 06/07/2017 09:19:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'ErrorMessage'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_ErrorMessage_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[ID],
	[Description]
FROM [dbo].[ErrorMessage]
ORDER BY 
	[ID] ASC
GO
