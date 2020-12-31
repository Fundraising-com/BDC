USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_LetterTemplate_SelectAll]    Script Date: 06/07/2017 09:20:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Benoit Nadon
-- Create date: 10/04/2006
-- Description:	Gets all Letter Templates
-- =============================================
CREATE PROCEDURE [dbo].[pr_LetterTemplate_SelectAll]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT		lt.ID,
				lt.Name,
				lt.Description,
				lt.Status,
				lt.ReportName,
				lt.ViewName,
				lt.ExtendedTable
	FROM		LetterTemplate lt
	ORDER BY	lt.Name
END
GO
