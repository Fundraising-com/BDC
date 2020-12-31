USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Account_GetDMID]    Script Date: 06/07/2017 09:33:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Account_GetDMID]
(
	@AccountID INT,
	@AsOfDate	DATETIME
)

RETURNS VARCHAR(4)

AS

BEGIN
	
	RETURN
	(
		SELECT		TOP 1 fm.DMID
		FROM		QSPCanadaCommon..CAccount acc
		JOIN		QSPCanadaCommon..Campaign camp
						ON	camp.BillToAccountID = acc.Id
		JOIN		QSPCanadaCommon..FieldManager fm
						ON	fm.FMID = camp.FMID
		WHERE		camp.Status = 37002
		AND			acc.Id = @AccountID
		AND			camp.StartDate < (	SELECT	s.EndDate
										FROM	QSPCanadaCommon..Season s
										WHERE	s.Season IN ('F', 'S')
										AND		@AsOfDate BETWEEN s.StartDate AND s.EndDate)
		ORDER BY	camp.StartDate DESC
	)

END
GO
