USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_PublisherInformation_Insert]    Script Date: 06/07/2017 09:17:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_PublisherInformation_Insert]

	@iPublisherID		int,
	@zUMC		varchar(4),
	@zSeason		varchar(1),
	@iYear			int

AS

	UPDATE	Product
	SET		Pub_Nbr = @iPublisherID
	WHERE	Product_Code = @zUMC
	AND		Product_Season = @zSeason
	AND		Product_Year = @iYear
GO
