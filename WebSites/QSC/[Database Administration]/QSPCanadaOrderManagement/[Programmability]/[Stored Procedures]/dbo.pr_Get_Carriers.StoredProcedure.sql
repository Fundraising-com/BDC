USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_Carriers]    Script Date: 06/07/2017 09:19:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_Carriers]

AS

SELECT
	*
FROM
	QSPCanadaCommon..CodeDetail
WHERE
	CodeHeaderInstance = 53000
AND
	Instance IN (53007, 53009, 53010, 53018)
ORDER BY CASE Instance WHEN 53010 THEN 1 WHEN 53009 THEN 2 ELSE 3 END
GO
