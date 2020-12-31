USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectSeason]    Script Date: 06/07/2017 09:18:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectSeason]
	@iYear		int,
	@zSeason	varchar(1)
AS
	SELECT	top 1
			s.ID,
			s.Country,
			s.Name,
			s.FiscalYear,
			s.Season,
			coalesce(s.StartDate, '1995-01-01') AS StartDate,
			coalesce(s.EndDate, '1995-01-01') AS EndDate,
			s.DateChanged,
			s.UserIDChanged,
			COALESCE(s.DefaultConversionRate, 1.00) AS DefaultConversionRate
	FROM		QSPCanadaCommon..Season s
	WHERE	s.FiscalYear = @iYear
	AND		s.Season = @zSeason
GO
