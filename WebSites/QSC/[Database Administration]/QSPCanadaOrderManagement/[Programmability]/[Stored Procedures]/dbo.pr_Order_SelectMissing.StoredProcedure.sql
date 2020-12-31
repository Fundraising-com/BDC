USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Order_SelectMissing]    Script Date: 06/07/2017 09:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Order_SelectMissing]

AS

DECLARE	@CurrentFYStartDate	DATETIME

SELECT	@CurrentFYStartDate = seas.StartDate
FROM	QSPCanadaCommon..Season seas
WHERE	GETDATE() BETWEEN seas.StartDate AND seas.EndDate
AND		seas.Season IN ('Y')

PRINT @CurrentFYStartDate

DECLARE	@ToDate	DATETIME
SET @ToDate = DATEADD(DAY, -5, GETDATE())
PRINT @ToDate

SELECT		ca.SAPAcctNo AccountID,
			c.SAPContractNo CampaignID,
			t.ToteID,
			co.CustomerOrderID AS CustomerOrderID,
			f.FormDesc AS OrderType,
			co.Created AS OrderDateCreated,
			co.Modified AS OrderDateModified,
			custos.CustomerOrderStateDesc OrderState,
			b.OrderID AS LandedOrderID,
			b.DateCreated AS LandedOrderDate,
			(SELECT		TOP 1 t2.ToteID
			FROM		GA.Core.Tote t2
			WHERE		ISNULL(t2.LastWorkflowStepTypeID, 0) NOT IN (6, 29)
			AND			t2.ToteID <> t.ToteID
			AND			t2.SwapContractID = c.ContractID
			ORDER BY	t2.LastWorkflowStepTypeID) AS OtherToteInContractPreventingExtraction
FROM		GA.Core.Tote t
LEFT JOIN	GA.Core.CustomerOrder co
				ON	co.ToteIDContract = t.ToteID
JOIN		GA.Core.Contract c
				ON	c.ContractID = t.ContractID
				AND	c.DivisionCode IN (40, 41)
LEFT JOIN	GA.Core.ContractAddress ca
				ON	ca.ContractID = c.ContractID
				AND	ca.IsOrganization = 1
LEFT JOIN	GA.Focus.Form f
				ON	f.FormCode = co.FormCode
LEFT JOIN	GA.Core.CustomerOrderState custos
				ON	custos.CustomerOrderStateID = co.CustomerOrderStateID
LEFT JOIN	InternetOrderID ioi
				ON	ioi.InternetOrderID = co.CustomerOrderID
LEFT JOIN	LandedOrder lo
				ON	lo.LandedOrderID = co.CustomerOrderID
LEFT JOIN	Batch b
				ON	b.CampaignID = CONVERT(INT, c.SAPContractNo)
				AND	b.OrderQualifierID = 39001
				AND	b.StatusInstance NOT IN (40005)
WHERE		(t.LastWorkflowStepTypeID NOT IN (6)
AND			t.ArrivalType IN (40, 41, 93, 94)
AND			t.Created BETWEEN @CurrentFYStartDate AND @ToDate)
OR			(co.Created BETWEEN @CurrentFYStartDate AND @ToDate
AND			co.CustomerOrderStateID NOT IN (0, 22, 23)
AND			co.FormCode IN ('000A', '000B', '0009', '0737', '0745', '000I', '000J', '000K')
AND			(ioi.InternetOrderID IS NULL AND lo.LandedOrderID IS NULL))
ORDER BY	t.Created, co.Created
GO
