USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_COH_By_COHId]    Script Date: 06/07/2017 09:19:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_COH_By_COHId]

@CustomerOrderHeaderId int

AS

SELECT
	A.*
	, B.[Name] As 'AccountName'
	, C.LastName + ', ' + C.FirstName As 'StudentName'
	, D.Description As 'StatusDesc'
	, E.Description As 'FirstStatusDesc'
	, F.Description As 'TypeDesc'
	, G.Description As 'PaymentMethodInstanceDesc'
FROM
	CustomerOrderHeader A
	LEFT OUTER JOIN Account B ON B.Id = A.AccountId
	LEFT OUTER JOIN Student C ON C.Instance = A.StudentInstance
	LEFT OUTER JOIN CodeDetail D ON D.Instance = A.StatusInstance
	LEFT OUTER JOIN CodeDetail E ON E.Instance = A.FirstStatusInstance
	LEFT OUTER JOIN CodeDetail F ON F.Instance = A.Type
	LEFT OUTER JOIN CodeDetail G ON G.Instance = A.PaymentMethodInstance
WHERE
	A.Instance = @CustomerOrderHeaderId
GO
