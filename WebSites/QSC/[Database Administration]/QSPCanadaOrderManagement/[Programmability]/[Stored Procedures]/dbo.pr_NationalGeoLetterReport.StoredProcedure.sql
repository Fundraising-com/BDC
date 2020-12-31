USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_NationalGeoLetterReport]    Script Date: 06/07/2017 09:20:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_NationalGeoLetterReport] AS

select	t.CampaignID,
	t.MagPrice,
	p.Product_Sort_Name AS MagName,
	pd.QSP_Price,
	pd.Nbr_Of_Issues,
	t.New,
	t.Price,
	t.Reason,
	t.Customer,
	t.Type,
	t.Usr,
	ltrim(rtrim(t.FN)) AS FN,
	ltrim(rtrim(t.LN)) AS LN,
	ltrim(rtrim(t.Addr1)) AS Addr1,
	ltrim(rtrim(t.Addr2)) AS Addr2,
	ltrim(rtrim(t.City)) AS City,
	ltrim(rtrim(t.State)) AS State,
	ltrim(rtrim(t.Zip)) AS Zip,
	t.Ord_Qua
from	TimeGeo1129GiveSub t,
	QSPCanadaProduct..Pricing_Details pd,
	QSPCanadaProduct..Product p
where	pd.MagPrice_Instance = t.MagPrice
and	p.Product_Code = pd.Product_Code
and	p.Product_Year = pd.Pricing_Year
and	p.Product_Season = pd.Pricing_Season

UNION ALL

select	t.CampaignID,
	t.MagPrice,
	p.Product_Sort_Name AS MagName,
	pd.QSP_Price,
	pd.Nbr_Of_Issues,
	t.New,
	t.Price,
	t.Reason,
	t.Customer,
	t.Type,
	t.Usr,
	ltrim(rtrim(t.FN)) AS FN,
	ltrim(rtrim(t.LN)) AS LN,
	ltrim(rtrim(t.Addr1)) AS Addr1,
	ltrim(rtrim(t.Addr2)) AS Addr2,
	ltrim(rtrim(t.City)) AS City,
	ltrim(rtrim(t.State)) AS State,
	ltrim(rtrim(t.Zip)) AS Zip,
	t.Ord_Qua
from	Time1130GiveSub t,
	QSPCanadaProduct..Pricing_Details pd,
	QSPCanadaProduct..Product p
where	pd.MagPrice_Instance = t.MagPrice
and	p.Product_Code = pd.Product_Code
and	p.Product_Year = pd.Pricing_Year
and	p.Product_Season = pd.Pricing_Season
GO
