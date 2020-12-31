USE [QSPCanadaProduct]
GO
/****** Object:  View [dbo].[vw_Season]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_Season] AS
SELECT [ID]
      ,[Country]
      ,[Name]
      ,[FiscalYear]
      ,[Season]
      ,DATEADD(dd, 15, [StartDate]) [StartDate]
      ,DATEADD(dd, 15, [EndDate]) [EndDate]
      ,[DateChanged]
      ,[UserIDChanged]
      ,[DefaultConversionRate]
  FROM [QSPCanadaCommon].[dbo].[Season]
GO
