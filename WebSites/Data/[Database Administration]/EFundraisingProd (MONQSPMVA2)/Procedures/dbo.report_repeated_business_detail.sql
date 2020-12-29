USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[report_repeated_business]    Script Date: 2/14/2017 12:58:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: 2017-02-14
-- Description:	Returns detailed sales for the Repeated Business Report on LISA
-- =============================================
ALTER PROCEDURE [dbo].[report_repeated_business_detail]
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
			,L.country_code AS [Country1]
			,L.state_code AS [State1]
         ,L.city as [City]
			,C.consultant_id AS [ConsultantIdT1]
			,C.name AS [ConsultantNameT1]
			,S.sales_id AS [SaleID1]
			,S.actual_ship_date [ActualShipDate1]
			,SI.sales_item_no [SaleItemNumber1]
			,SI.quantity_sold AS [Quantity1]
			,SC.description AS [Product1]
			,SI.sales_amount AS [Amount1]
			FROM
			sale S (NOLOCK)
			JOIN sales_item SI (NOLOCK) ON S.sales_id = SI.sales_id
			JOIN scratch_book SC (NOLOCK) ON SI.scratch_book_id = SC.scratch_book_id
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
		) AS T1
		LEFT JOIN 
		(
			SELECT
			S.lead_id AS [LeadIdT2]
			,S.sales_id AS [SaleID2]
			,S.actual_ship_date [ActualShipDate2]
			,SI.sales_item_no [SaleItemNumber2]
			,SI.quantity_sold AS [Quantity2]
			,SC.description AS [Product2]
			,SI.sales_amount AS [Amount2]
			FROM
			sale S (NOLOCK)
			JOIN sales_item SI (NOLOCK) ON S.sales_id = SI.sales_id
			JOIN scratch_book SC (NOLOCK) ON SI.scratch_book_id = SC.scratch_book_id
			WHERE
				S.actual_ship_date BETWEEN @DATE2START AND @DATE2END
				AND S.total_amount > 50
		) AS T2 ON T1.[LeadIdT1] = T2.[LeadIdT2]
END
