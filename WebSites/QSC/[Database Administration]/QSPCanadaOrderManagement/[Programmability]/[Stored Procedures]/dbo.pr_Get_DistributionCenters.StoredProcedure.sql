USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_DistributionCenters]    Script Date: 06/07/2017 09:19:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_DistributionCenters]

AS

SELECT
	*
FROM
	DistributionCenter
ORDER BY
	[Name]
GO
