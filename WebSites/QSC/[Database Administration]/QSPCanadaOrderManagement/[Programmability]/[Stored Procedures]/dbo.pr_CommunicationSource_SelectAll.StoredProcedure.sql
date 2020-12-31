USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CommunicationSource_SelectAll]    Script Date: 06/07/2017 09:19:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'CommunicationSource'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_CommunicationSource_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
SELECT
	[Instance],
	[Description]
FROM [dbo].[CommunicationSource]
ORDER BY 
	[Description] ASC
GO
