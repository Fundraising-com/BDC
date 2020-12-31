USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_FulfillmentHouseInformation_SelectOne]    Script Date: 06/07/2017 09:17:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_FulfillmentHouseInformation_SelectOne]

@zUMC varchar(4),
@zSeason varchar(1),
@iYear int

AS

SELECT	p.Fulfill_House_Nbr

FROM		Product p

WHERE	p.Product_Code = @zUMC
AND		p.Product_Season = @zSeason
AND		p.Product_Year = @iYear
GO
