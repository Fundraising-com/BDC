USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Season_SelectOne]    Script Date: 06/07/2017 09:33:29 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Season_SelectOne]

	@iID	int

AS

SELECT		[ID],
		[Country],
		[Name],
		[FiscalYear],
		[Season],
		[StartDate],
		[EndDate],
		[DateChanged],
		[UserIDChanged],
		[DefaultConversionRate]

FROM		[Season]
WHERE		[ID] = @iID
GO
