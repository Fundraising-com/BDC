USE [QSPCanadaFinance]
GO
/****** Object:  View [dbo].[vw_GetNetForReporting_GAO]    Script Date: 06/07/2017 09:16:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetNetForReporting_GAO] AS 
SELECT vw1.campaignid, vw1.orderqualifierid, vw1.section_type_id, vw2.GAO_Accounting_year, vw1.accounting_year, vw1.accounting_period, vw1.invoice_date, vw1.NetSale, vw1.Units
FROM vw_GetNetForReporting vw1
JOIN QSpCanadaFinance.dbo.vw_GAO_Mapping_Account_Year vw2 
	ON vw2.Historical_Account_Year = vw1.accounting_year AND vw2.ACCOUNTING_PERIOD = vw1.ACCOUNTING_PERIOD
GO
