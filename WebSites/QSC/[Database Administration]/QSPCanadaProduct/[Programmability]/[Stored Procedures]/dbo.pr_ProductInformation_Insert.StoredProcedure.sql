USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProductInformation_Insert]    Script Date: 06/07/2017 09:17:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ProductInformation_Insert]

	@zUMC			varchar(4),
	@zSeason			varchar(1),
	@iYear				int,
	@zMagazineTitleName		varchar(55),
	@zLanguage			varchar(2),
	@iCategoryID			int,
	@iDaysLeadTime		int,
	@iNbrOfIssuesPerYear		int,
	@zGSTRegistrationNumber	varchar(20),
	@zHSTRegistrationNumber	varchar(20),
	@zPSTRegistrationNumber	varchar(20),
	@zUserName			varchar(50)

AS

	INSERT INTO Product
	(Product_Code,
	Product_Season,
	Product_Year,
	Alpha_Code,
	Product_Name,
	Product_Sort_Name,
	Ages,
	Internet,
	Issue_Rcvd_Dt,
	CoverReceived,
	HighlightCover,
	Featuring,
	Comment,
	CommentDate,
	Mail_Dt,
	Auth_Form_Rtrn_Dt,
	IssueDateUsed,
	Logged_By,
	Log_Dt,
	ProductLine,
	Currency,
	CountryCode,
	Type,
	UnitOfMeasure,
	UOMConvFactor,
	UnitWeight,
	UnitCost,
	OracleCode,
	Prize_Level,
	Prize_Level_Qty_Required,
	Lang,
	Status,
	Category_Code,
	DaysLeadTime,
	Nbr_Of_Issues_Per_Year)
	VALUES
	(@zUMC,
	@zSeason,
	@iYear,
	0,
	'',
	@zMagazineTitleName,
	'',
	'',
	'1995-01-01',
	'',
	0,
	0,
	'',
	'1995-01-01',
	'1/1/95',
	'1995-01-01',
	'',
	@zUserName,
	getdate(),
	1,
	801, --?
	'CA',
	46001,
	'',
	0,
	0.00,
	0.00,
	'',
	'',
	0,
	@zLanguage,
	30600,
	@iCategoryID,
	@iDaysLeadTime,
	@iNbrOfIssuesPerYear)

	EXEC pr_RegisterTaxMag @zUMC, @zGSTRegistrationNumber, @zHSTRegistrationNumber, @zPSTRegistrationNumber
GO
