--Change last season to run past today
--Run the product etl for the specific offer
--revert the proc to what is was originally
--run the product etl so the brochure effective dates get set back to what they should be

USE [QSPCanadaProduct]
GO

ALTER VIEW [dbo].[vw_Season] AS
SELECT [ID]
      ,[Country]
      ,[Name]
      ,[FiscalYear]
      ,[Season]
      ,DATEADD(dd, 15, [StartDate]) [StartDate]
      ,case id when 49 then '2018-01-20' else  DATEADD(dd, 15, [EndDate]) end [EndDate]
      ,[DateChanged]
      ,[UserIDChanged]
      ,[DefaultConversionRate]
  FROM [QSPCanadaCommon].[dbo].[Season]

GO


