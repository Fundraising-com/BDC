USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[CustomerRefundReport]    Script Date: 06/07/2017 09:17:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[CustomerRefundReport]

	@RefundAmountFrom	NUMERIC(10,2),
	@RefundAmountTo		NUMERIC(10,2),
	@DateCreatedFrom	DATETIME,
	@DateCreatedTo		DATETIME,
	@Province			VARCHAR(5),
	@SortBy				VARCHAR(10)

AS

IF @RefundAmountFrom = 0.00 SET @RefundAmountFrom = NULL
IF @RefundAmountTo = 0.00 SET @RefundAmountTo = NULL
IF @DateCreatedFrom = '1995-01-01' SET @DateCreatedFrom = NULL
IF @DateCreatedTo = '1995-01-01' SET @DateCreatedTo = NULL
IF @Province = '' SET @Province = NULL

SELECT		apcb.CreationDate AS DateSent,
			apc.ChequeNumber,
			cod.ProductCode,
			cod.ProductName,
			cod.CustomerOrderHeaderInstance,
			cod.TransID,
			ref.Amount,
			ref.FirstName,
			ref.LastName,
			ref.Address1,
			ref.Address2,
			ref.City,
			ref.Province,
			ref.PostalCode,
			ref.CreateDate,
			CASE ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) WHEN 1 THEN 'TIME' ELSE 'GAO' END Company
FROM		Refund ref
LEFT JOIN	(AP_Cheque apc
				JOIN	AP_Cheque_Batch apcb
							ON	apcb.AP_Cheque_Batch_ID = apc.AP_Cheque_Batch_ID)
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = ref.CustomerOrderHeaderInstance
				AND	cod.TransID = ref.TransID
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.Date = coh.OrderBatchDate
WHERE		ref.Refund_Type_ID = 1 --2: Customer Refund
AND			ref.Amount BETWEEN ISNULL(@RefundAmountFrom, ref.Amount) AND ISNULL(@RefundAmountTo, ref.Amount)
AND			ref.CreateDate BETWEEN ISNULL(@DateCreatedFrom, ref.CreateDate) AND ISNULL(@DateCreatedTo, ref.CreateDate)
AND			ref.Province = ISNULL(@Province, ref.Province)
ORDER BY	CASE @SortBy
				WHEN 'AMOUNT' THEN	ref.Amount
			END,
			CASE @SortBy
				WHEN 'DATE' THEN	ref.CreateDate
			END,
			CASE @SortBy
				WHEN 'NAME' THEN	ref.LastName
			END
GO
