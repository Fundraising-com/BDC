USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Programs_byCountry]    Script Date: 06/07/2017 09:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_Programs_byCountry]
  @CountryIn CountryCode_UDDT
AS

SELECT
	ID   AS ProgramID,
	Name AS ProgramName
FROM
	Program
WHERE
	Country = @CountryIn
GO
