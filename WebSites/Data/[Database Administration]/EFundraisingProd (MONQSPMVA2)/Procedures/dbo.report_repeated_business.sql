USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[report_repeated_business]    Script Date: 2/1/2017 10:53:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: 2017-01-25
-- Description:	Returns the amount of Sales done on the two date frames given, where the second are repeated leads from the first one, by Consultant name
-- =============================================
ALTER PROCEDURE [dbo].[report_repeated_business]
	@DATE1START DATETIME
	,@DATE1END DATETIME
	,@DATE2START DATETIME
	,@DATE2END DATETIME
	,@ShowFCsOnly BIT
	,@CountryCode NVARCHAR(5) = NULL
	,@StateCode NVARCHAR(5) = NULL
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		*
		FROM
		(
			SELECT
			L.lead_id AS [LeadIdT1]
			,C.consultant_id AS [ConsultantIdT1]
			,SUM(S.total_amount) AS [TotalAmountT1] -- update JF DEC 2017
			--,SUM(S.total_amount + S.shipping_fees) AS [TotalAmountT1] OLD LINE
			FROM
			sale S (NOLOCK)
			JOIN lead L (NOLOCK) ON S.lead_id = L.lead_id
			JOIN consultant C (NOLOCK) ON L.consultant_id = C.consultant_id
			WHERE
				S.actual_ship_date BETWEEN @DATE1START AND @DATE1END
				AND S.total_amount > 50
				AND (
					@ShowFCsOnly = 0
					OR (@ShowFCsOnly = 1 
						AND C.department_id = 7 
						AND C.is_active = 1
						AND C.ext_consultant_id IS NOT NULL
						AND C.division_id = 1)
					)
				AND (@CountryCode IS NULL OR L.country_code = @CountryCode)
				AND (@StateCode IS NULL OR L.state_code = @StateCode)
			GROUP BY
			L.lead_id
			,C.consultant_id
			,C.name
		) AS T1
		LEFT JOIN 
		(
			SELECT
			S.lead_id AS [LeadIdT2]
			,SUM(S.total_amount) AS [TotalAmountT2] -- update JF DEC 2017
			--,SUM(S.total_amount + S.shipping_fees) AS [TotalAmountT2] OLD LINE
			FROM
			sale S (NOLOCK)
			WHERE
				S.actual_ship_date BETWEEN @DATE2START AND @DATE2END
				AND S.total_amount > 50
			GROUP BY
			S.lead_id
		) AS T2 ON T1.[LeadIdT1] = T2.[LeadIdT2]
END
