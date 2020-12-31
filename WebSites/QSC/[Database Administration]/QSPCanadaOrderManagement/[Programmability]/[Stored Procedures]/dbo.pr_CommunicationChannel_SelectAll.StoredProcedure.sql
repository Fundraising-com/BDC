USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CommunicationChannel_SelectAll]    Script Date: 06/07/2017 09:19:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CommunicationChannel'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CommunicationChannel_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Instance],
	[Description]
FROM [dbo].[CommunicationChannel]
where instance <= 4
ORDER BY 
	[Description] DESC
GO
