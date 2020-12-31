--Will need to add invs.ProgramType to the select and group by clauses in qspcanadafinance..vw_GetNetForReporting for the query to work.

USE [QSPCanadaOrderManagement]
GO

Drop Procedure [dbo].[InventoryReportingByBrochureAndFM]
GO

CREATE PROC [dbo].[InventoryReportingByBrochureAndFM]
(
@FromDate		DATETIME,
@ToDate			DATETIME
)

AS
SET NOCOUNT ON 

SET @ToDate = DATEADD(dd, 1, @ToDate)

SELECT [FMFirstName]
      ,[FMLastName]
      ,CAST(@FromDate as DATE) as MinDate
      ,CAST(@ToDate as DATE) as MaxDate
      ,SUM([GiftNetSales]) as [GiftNetSales]
      ,SUM([GiftUnits]) as [GiftUnits]
      ,SUM([NaturallyGoodNetSales]) as [NaturallyGoodNetSales]
      ,SUM([NaturallyGoodUnits]) as [NaturallyGoodUnits]
      ,SUM([FestivalNetSales]) as [FestivalNetSales]
      ,SUM([FestivalUnits]) as [FestivalUnits]
      ,SUM([GiftsWeLoveNetSales]) as [GiftsWeLoveNetSales]
      ,SUM([GiftsWeLoveUnits]) as [GiftsWeLoveUnits]
      ,SUM([LifeIsSweetNetSales]) as [LifeIsSweetNetSales]
      ,SUM([LifeIsSweetUnits]) as [LifeIsSweetUnits]
      ,SUM([KitchenCollectionNetSales]) as [KitchenCollectionNetSales]
      ,SUM([KitchenCollectionUnits]) as [KitchenCollectionUnits]
      ,SUM([DonationSales]) as [DonationSales]
      ,SUM([DonationUnits]) as [DonationUnits]
      ,SUM([TumblerSales]) as [TumblerSales]
      ,SUM([TumblerUnits]) as [TumblerUnits]
      ,SUM([OrganicEdiblesNetSales]) as [OrganicEdiblesNetSales]
      ,SUM([OrganicEdiblesUnits]) as [OrganicEdiblesUnits]
      ,SUM([MagazineNetSales]) as [MagazineNetSales]
      ,SUM([MagazineUnits]) as [MagazineUnits]
      ,SUM([CookieDoughNetSales]) as [CookieDoughNetSales]
      ,SUM([CookieDoughUnits]) as [CookieDoughUnits]
      ,SUM([PopcornNetSales]) as [PopcornNetSales]
      ,SUM([PopcornUnits]) as [PopcornUnits]
      ,SUM([JewelryNetSales]) as [JewelryNetSales]
      ,SUM([JewelryUnits]) as [JewelryUnits]
      ,SUM([TRTNetSales]) as [TRTNetSales]
      ,SUM([TRTUnits]) as [TRTUnits]
      ,SUM([TotalNetSales]) as [TotalNetSales]
      ,SUM([TotalUnits]) as [TotalUnits]
 FROM dbo.vw_InventoryReportingNetSales
WHERE invoice_date between @FromDate AND @ToDate
GROUP BY [FMFirstName],[FMLastName]
ORDER BY [FMFirstName],[FMLastName]




GO

