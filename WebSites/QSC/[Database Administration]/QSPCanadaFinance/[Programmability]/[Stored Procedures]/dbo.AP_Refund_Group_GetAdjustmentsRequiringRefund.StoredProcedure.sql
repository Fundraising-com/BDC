USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Refund_Group_GetAdjustmentsRequiringRefund]    Script Date: 06/07/2017 09:17:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Refund_Group_GetAdjustmentsRequiringRefund]

AS

SELECT	adj.Adjustment_ID
FROM	Adjustment adj
JOIN	Adjustment_Type adjType
			ON	adjType.Adjustment_Type_ID = adj.Adjustment_Type_ID
WHERE	adjType.Is_Cheque_Required = 'Y'
AND		adj.Refund_ID IS NULL
AND		adj.Date_Created >= '2010-03-05'
GO
