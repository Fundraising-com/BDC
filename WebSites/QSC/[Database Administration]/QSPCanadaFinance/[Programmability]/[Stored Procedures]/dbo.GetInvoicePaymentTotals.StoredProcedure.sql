USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoicePaymentTotals]    Script Date: 06/07/2017 09:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInvoicePaymentTotals] 

	@InvoiceID 	INT

AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/7/2004 
--   Get Invoice Payment Totals For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON


DECLARE
	@ChequeCash		varchar(15),
	@ChequeComptant varchar(15),
	@masterCard		varchar(15),
	@Visa			varchar(15),
	@PaymentType	varchar(20), 
	@PaymentAmount	numeric(18,2), 
	@Lang			char(2),
	@ItemExists		varchar(20)
	

SET	@ChequeCash		= 'Cheque/Cash'
SET	@ChequeComptant = 'Chèque/Comptant'
SET	@masterCard		= 'Master Card'
SET	@Visa			= 'Visa'
	
SET @PaymentType	= ''
SET @PaymentAmount	= 0	
SET	@Lang			= 'AN'	
SET	@ItemExists		= ''	
	
-- Get the rows and save them into a temp table for further use	
SELECT	PaymentType,
		PaymentAmount
		--Lang
INTO	#temp		
FROM	UDIF_GetInvoicePaymentTotals_AgregatedByInvoiceIdAndType() --UDIF_GetInvoicePaymentTotals_AgregatedByInvoiceIdAndTypeAllPayments()
WHERE	InvoiceId = @InvoiceID


-- Get the current lang
--SELECT TOP 1 @Lang=Lang FROM #temp

----------------------------
-- Verify for 'Cheque/Cash'
----------------------------
--SET	@ItemExists		= ''	
--SELECT @ItemExists = PaymentType FROM #temp WHERE PaymentType = 'Cheque/Cash' OR PaymentType = 'Chèque/Comptant'
--IF (@ItemExists = '' OR @ItemExists IS NULL)
--BEGIN
--	IF @Lang = 'AN'
--		INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Cheque/Cash', 0, @Lang)
--	ELSE		
--		INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Chèque/Comptant', CAST('0.00' AS numeric(18,2)), @Lang)
--END

----------------------------
-- Verify for 'Master Card'
----------------------------
--SET	@ItemExists		= ''	
--SELECT @ItemExists = PaymentType FROM #temp WHERE PaymentType = 'Master Card' 
--IF (@ItemExists = '' OR @ItemExists IS NULL)
--	INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Master Card', CAST('0.00' AS numeric(18,2)), @Lang)

----------------------------
-- Verify for 'Visa'
----------------------------
--SET	@ItemExists		= ''	
--SELECT @ItemExists = PaymentType FROM #temp WHERE PaymentType = 'Visa' 
--IF (@ItemExists = '' OR @ItemExists IS NULL)
--	INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Visa', CAST('0.00' AS numeric(18,2)), @Lang)





-- Return the result
SELECT	PaymentType,
		PaymentAmount
FROM	#temp	
ORDER BY PaymentType	

-- Free the ressources
DROP TABLE #Temp

SET NOCOUNT OFF
GO
