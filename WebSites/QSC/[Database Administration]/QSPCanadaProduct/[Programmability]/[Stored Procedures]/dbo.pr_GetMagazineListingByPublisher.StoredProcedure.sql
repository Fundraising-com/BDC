USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetMagazineListingByPublisher]    Script Date: 06/07/2017 09:17:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GetMagazineListingByPublisher] 

@iPublisherID int

AS

DECLARE @season 	varchar(1)
DECLARE @year 	int
DECLARE @now 	smalldatetime

SET @now = getDate()

SELECT 

	@year = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 6 THEN YEAR(CONVERT(smalldatetime,@now))+ 1
		WHEN MONTH(CONVERT(smalldatetime,@now)) <= 6 THEN YEAR(CONVERT(smalldatetime,@now))
		ELSE
			0
		END
	,@season = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 6 THEN 'F'
		WHEN MONTH(CONVERT(smalldatetime,@now)) <= 6 THEN 'S'
		ELSE ''
		END



SET @year = 2005	-- TO REMOVE IN PROD
SET @season = 'F'	-- TO REMOVE IN PROD

IF @iPublisherID <> 0
BEGIN
	select	distinct
		p.Product_Code as UMC,
		p.Product_Sort_Name + CASE pd.Status WHEN 30600 THEN '' WHEN 30601 THEN ' (inactive)' ELSE ' (pending)' END as Name,
		pd.Nbr_of_Issues as NumberOfIssues,
		p.Pub_Nbr as PublisherID,
		p.Fulfill_House_Nbr as FulfillmentHouseID
	from	Product p,
		Pricing_Details pd
	where	p.Pub_Nbr = @iPublisherID
	and	p.Product_Season = @season
	and	p.Product_Year = @year
	and	p.Type = 46001
	and	pd.Product_Code = p.Product_Code
	and	pd.Pricing_Season = p.Product_Season
	and	pd.Pricing_Year = p.Product_Year
	order by	Name
END
ELSE
BEGIN
	select	distinct
		p.Product_Code as UMC,
		p.Product_Sort_Name + CASE pd.Status WHEN 30600 THEN '' WHEN 30601 THEN ' (inactive)' ELSE ' (pending)' END as Name,
		pd.Nbr_of_Issues as NumberOfIssues,
		p.Pub_Nbr as PublisherID,
		p.Fulfill_House_Nbr as FulfillmentHouseID
	from	Product p,
		Pricing_Details pd
	where	p.Product_Season = @season
	and	p.Product_Year = @year
	and	p.Type = 46001
	and	pd.Product_Code = p.Product_Code
	and	pd.Pricing_Season = p.Product_Season
	and	pd.Pricing_Year = p.Product_Year
	order by	Name
END
GO
