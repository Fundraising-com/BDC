USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_CODs_By_COHId]    Script Date: 06/07/2017 09:19:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_CODs_By_COHId]

@COHId int	

 AS


SELECT
	A.*, B.Description As 'StatusDesc'
FROM
	CustomerOrderDetail A
	LEFT OUTER JOIN CodeDetail B ON B.Instance = A.StatusInstance
WHERE
	A.CustomerOrderHeaderInstance = @COHId
ORDER BY
	A.TransId
GO
