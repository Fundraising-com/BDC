USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetSeasonEndDate]    Script Date: 06/07/2017 09:33:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GetSeasonEndDate]
  @date datetime
AS
BEGIN
  		SELECT EndDate
		FROM QSPCanadaCommon.dbo.Season
		WHERE StartDate <= @date
		AND EndDate >= @date
        AND Season <> 'Y';
END
GO
