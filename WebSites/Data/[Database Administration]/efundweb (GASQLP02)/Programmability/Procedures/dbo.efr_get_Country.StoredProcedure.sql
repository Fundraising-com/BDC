USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_Country]    Script Date: 02/14/2014 13:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_get_Country]

AS

SELECT
	Country_ID
	,Country_Name
	,Currency_Code
	,Country_Code
FROM dbo.Country
	WHERE Country_ID in (3, 15, 14)
GO
