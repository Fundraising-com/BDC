USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProductInformation_SelectOne]    Script Date: 06/07/2017 09:17:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ProductInformation_SelectOne]

@zUMC		varchar(4),
@zSeason		varchar(1),
@iYear			int,
@iNumberOfIssues	int

 AS

select 	top 1 p.Product_Code as UMC,
	p.Product_Sort_Name as Name,
	p.Status,
	p.Lang as Language,
	coalesce(p.category_code, 0) as CategoryID,
	coalesce(p.DaysLeadTime, 0) as DaysLeadTime,
	coalesce(p.Currency, 801) as Currency,
	coalesce(p.Nbr_Of_Issues_Per_Year, 1) as nbrofissuesperyear,
	COALESCE(TMGGST.TAX_REGISTRATION_NUMBER, '') [GST_Registration_Nbr],
	COALESCE(TMGHST.TAX_REGISTRATION_NUMBER, '') [HST_Registration_Nbr],
	COALESCE(TMGPST.TAX_REGISTRATION_NUMBER, '') [PST_Registration_Nbr],
	p.Fulfill_House_Nbr
from		product p
LEFT JOIN	QSPCanadaCommon..TaxMagRegistration TMGGST
			ON	TMGGST.TITLE_CODE = Product_Code
			AND	TMGGST.TAX_ID = 1
LEFT JOIN	QSPCanadaCommon..TaxMagRegistration TMGHST
			ON	TMGHST.TITLE_CODE = Product_Code
			AND	TMGHST.TAX_ID = 2
LEFT JOIN	QSPCanadaCommon..TaxMagRegistration TMGPST
			ON	TMGPST.TITLE_CODE = Product_Code
			AND	TMGPST.TAX_ID = 3
where	p.Product_Season = @zSeason
	and p.Product_Year = @iYear
	and p.Product_Code = @zUMC
GO
