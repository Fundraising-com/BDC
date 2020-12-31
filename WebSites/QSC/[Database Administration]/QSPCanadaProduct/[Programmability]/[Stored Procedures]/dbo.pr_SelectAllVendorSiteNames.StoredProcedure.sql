USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllVendorSiteNames]    Script Date: 06/07/2017 09:18:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllVendorSiteNames] AS

SELECT DISTINCT
		coalesce(p.VendorSiteName, '') VendorSiteName

FROM		Product p

ORDER BY	coalesce(p.VendorSiteName, '')
GO
