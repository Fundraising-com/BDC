USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_AdjustmentType_SelectAll]    Script Date: 06/07/2017 09:17:26 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_AdjustmentType_SelectAll] AS

SELECT	adjt.Adjustment_Type_ID,
		adjt.Name,
		adjt.French_Name,
		adjt.Debit_Credit,
		adjt.Is_Cheque_Required,
		adjt.Not_Print_On_Statement,
		adjt.Is_Magnet_Adjustment
FROM		Adjustment_Type adjt
ORDER BY	adjt.Name
GO
