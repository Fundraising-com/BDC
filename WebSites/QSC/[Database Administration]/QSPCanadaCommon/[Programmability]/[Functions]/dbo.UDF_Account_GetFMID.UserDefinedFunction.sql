USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Account_GetFMID]    Script Date: 06/07/2017 09:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Account_GetFMID]
(
	@AccountID	INT,
	@AsOfDate	DATETIME
)

RETURNS VARCHAR(4)

AS

BEGIN
	
	RETURN
	(
		SELECT		TOP 1 camp.fmid
		FROM		QSPCanadaCommon..CAccount acc
		JOIN		QSPCanadaCommon..Campaign camp
						ON	camp.BillToAccountID = acc.Id
		WHERE		camp.Status IN (37002, 37004)
		AND			acc.Id = @AccountID
		AND			camp.StartDate < (	SELECT	s.EndDate
										FROM	QSPCanadaCommon..Season s
										WHERE	s.Season IN ('F', 'S')
										AND		@AsOfDate BETWEEN s.StartDate AND s.EndDate)
		ORDER BY	camp.StartDate DESC
	)

END
GO
