USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectDefaultProductTypeByProgramSection]    Script Date: 06/07/2017 09:18:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectDefaultProductTypeByProgramSection]

	@iProgramSectionID		int

AS

DECLARE	@iProductType		int
DECLARE	@iCatalogSectionType	int

SELECT	p.Type,
		COUNT(*) AS ContractsCount
INTO		#ProductTypes
FROM		Pricing_Details pd,
		Product p
WHERE	p.Product_Instance = pd.Product_Instance
AND		pd.ProgramSectionID = @iProgramSectionID
GROUP BY	p.Type

SELECT	TOP 1
		@iProductType = pt.Type
FROM		#ProductTypes pt
WHERE	pt.ContractsCount =
		(SELECT	MAX(pt2.ContractsCount)
		FROM		#ProductTypes pt2)
ORDER BY	pt.Type


IF(@iProductType IS NULL)
BEGIN
	SELECT	@iCatalogSectionType = ps.Type
	FROM		ProgramSection ps
	WHERE	ps.ID = @iProgramSectionID

	SET		@iProductType =	CASE	@iCatalogSectionType
							WHEN	1	THEN	46002
							WHEN	2	THEN	46001
							WHEN	3	THEN	46004
							WHEN	4	THEN	46013
							WHEN	6	THEN	46008
							WHEN	7	THEN	46008
							WHEN	9	THEN	46018
							WHEN	10	THEN	46019
							WHEN	11	THEN	46020
							WHEN	12	THEN	46021
							WHEN	13	THEN	46022
							WHEN	14	THEN	46023
							WHEN	15	THEN	46024
							WHEN	16	THEN	46007
							WHEN	17	THEN	46025
							WHEN	18	THEN	46025
							ELSE				46001
						END
END

SELECT	COALESCE(@iProductType, 46001)

DROP TABLE	#ProductTypes
GO
