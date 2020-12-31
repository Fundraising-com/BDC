USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Season_SelectOneByDate]    Script Date: 06/07/2017 09:33:29 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Season_SelectOneByDate]

	@dDate	datetime

AS

SELECT	[ID],
		[Country],
		[Name],
		[FiscalYear],
		[Season],
		[StartDate],
		[EndDate],
		[DateChanged],
		[UserIDChanged]

FROM		[Season]
WHERE	@dDate BETWEEN StartDate AND EndDate
AND		Season <> 'Y'
GO
