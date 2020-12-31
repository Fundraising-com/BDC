USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_DistributionCenter]    Script Date: 06/07/2017 09:19:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_DistributionCenter]

@DistributionCenterId int

AS

SELECT
	*
FROM
	DistributionCenter
WHERE
	Id = @DistributionCenterId
ORDER BY
	[Name]
GO
