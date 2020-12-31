USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_COD_By_COHId_TransId]    Script Date: 06/07/2017 09:19:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_COD_By_COHId_TransId]

@COHId int,
@TransId int

AS

SELECT
	A.*
	, B.Description As 'StatusDesc'
FROM
	CustomerOrderDetail A
	LEFT OUTER JOIN CodeDetail B ON B.Instance = A.StatusInstance
WHERE
	A.CustomerOrderHeaderInstance = @COHId
	AND A.TransId = @TransId
GO
