USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllPublishers]    Script Date: 06/07/2017 09:18:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllPublishers] AS
SELECT	p.Pub_Nbr,
		p.Pub_Status,
		p.Pub_Name,
		QSPCanadaOrderManagement.dbo.UDF_ReplaceAccents(p.Pub_Name) AS Pub_Name_WithoutAccents,
		p.Pub_Addr_1,
		p.Pub_Addr_2,
		p.Pub_City,
		p.Pub_State,
		p.Pub_Zip + CASE COALESCE(p.Pub_Zip_Four, '') WHEN '' THEN '' ELSE '-' + p.Pub_Zip_Four END AS Pub_Zip,
		p.Pub_CountryCode
FROM		Publishers p
ORDER BY	p.Pub_Name
GO
