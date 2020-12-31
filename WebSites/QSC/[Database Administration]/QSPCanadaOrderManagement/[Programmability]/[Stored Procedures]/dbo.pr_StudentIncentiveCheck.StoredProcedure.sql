USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_StudentIncentiveCheck]    Script Date: 06/07/2017 09:20:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_StudentIncentiveCheck]

@StudentInstance int,
@PricingDetailsId int,
@CampaignId int,
@Items int output

 AS

SELECT @Items = 0


SELECT	
	@Items = Count(D.StudentInstance)
FROM
	Batch A
	INNER JOIN Teacher B ON A.AccountID = B.AccountID 
	INNER JOIN Student C ON B.Instance = C.TeacherInstance
	INNER JOIN CustomerOrderHeader D ON C.Instance =  D.StudentInstance
	INNER JOIN CustomerOrderDetail E ON D.Instance = E.CustomerOrderHeaderInstance
	--INNER JOIN #Temp1 F ON F.CampaignId = A.CampaignId
WHERE
	IsNull(A.IncentiveCalculationStatus, 0) = 1 
--							AND A.Date = B.OrderBatchDate
	and OrderBatchDate=date and OrderbatchId = id
	AND E.PricingDetailsId = @PricingDetailsId	
	AND A.Campaignid = @CampaignId
	AND D.StudentInstance = @StudentInstance
GO
