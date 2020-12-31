USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_ProductInformation_Update]    Script Date: 06/07/2017 09:17:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ProductInformation_Update]

	@zUMC			varchar(4),
	@zSeason			varchar(1),
	@iYear				int,
	@zNewUMC			varchar(4),
	@zMagazineTitleName		varchar(55),
	@zLanguage			varchar(2),
	@iCategoryID			int,
	@iDaysLeadTime		int,
	@iNbrOfIssuesPerYear		int,
	@zGSTRegistrationNumber	varchar(20),
	@zHSTRegistrationNumber	varchar(20),
	@zPSTRegistrationNumber	varchar(20)

AS

	UPDATE	Product

	SET		Product_Code = @zNewUMC,
			Product_Sort_Name = @zMagazineTitleName,
			Lang = @zLanguage,
			Status = 30600,
			Category_Code = @iCategoryID,
			DaysLeadTime = @iDaysLeadTime,
			Nbr_Of_Issues_Per_Year = @iNbrOfIssuesPerYear

	WHERE	Product_Code = @zUMC
	AND		Product_Season = @zSeason
	AND		Product_Year = @iYear

	EXEC pr_RegisterTaxMag @zUMC, @zGSTRegistrationNumber, @zHSTRegistrationNumber, @zPSTRegistrationNumber
GO
