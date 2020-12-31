USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoicePaymentTotalsAllPayments]    Script Date: 06/07/2017 09:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInvoicePaymentTotalsAllPayments] -- 269385

	@InvoiceID 	INT

AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/7/2004 
--   Get Invoice Payment Totals For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON


DECLARE
	@CustomerServiceProfit		varchar(50),
	@ProfitServiceClientele		varchar(50),

	@OnlineProfit				varchar(15),
	@ProfitEnLigne 				varchar(15),


	@ChequeCash					varchar(15),
	@ChequeComptant 			varchar(15),
	@masterCard					varchar(15),
	@Visa						varchar(15),
	@PaymentType				varchar(20), 
	@PaymentAmount				numeric(18,2), 
	@Lang						char(2),
	@ItemExists					varchar(20)
	

SET @CustomerServiceProfit		= 'Customer Service profit'
SET @ProfitServiceClientele		= 'Profit de service à la clientèle'

SET @OnlineProfit				= 'Online profit'
SET @ProfitEnLigne 				= 'Profit en ligne'

SET	@ChequeCash					= 'Cheque/Cash'
SET	@ChequeComptant 			= 'Chèque/Comptant'
SET	@masterCard					= 'Master Card'
SET	@Visa						= 'Visa'
	
SET @PaymentType				= ''
SET @PaymentAmount				= CAST('0.00' AS numeric(18,2))	
SET	@Lang						= 'EN'	
SET	@ItemExists					= ''	
	
-- Get the rows and save them into a temp table for further use	
SELECT	PaymentType,
		PaymentAmount,
		Lang
INTO	#temp		
FROM	UDIF_GetInvoicePaymentTotals_AgregatedByInvoiceIdAndTypeAllPayments()
WHERE	InvoiceId = @InvoiceID


-- Get the current lang
SELECT TOP 1 @Lang=Lang FROM #temp

------------------------------------------------------------------------------------
-- Verify for 'Online profit/Profit en ligne'
------------------------------------------------------------------------------------
SET	@ItemExists		= ''	
SELECT @ItemExists = PaymentType FROM #temp WHERE PaymentType = 'Online profit' OR PaymentType = 'Profit en ligne'
IF (@ItemExists = '' OR @ItemExists IS NULL)
BEGIN
	IF @Lang = 'EN'	
		INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Online profit', @PaymentAmount, @Lang)
	ELSE		
		INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Profit en ligne', @PaymentAmount, @Lang)
END


------------------------------------------------------------------------------------
-- Verify for 'Customer Service profit/Profit de service à la clientèle'
------------------------------------------------------------------------------------
SET	@ItemExists		= ''	
SELECT @ItemExists = PaymentType FROM #temp WHERE PaymentType = 'Customer Service profit' OR PaymentType = 'Profit de service à la clientèle'
IF (@ItemExists = '' OR @ItemExists IS NULL)
BEGIN
	IF @Lang = 'EN'	
		INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Customer Service profit', @PaymentAmount, @Lang)
	ELSE		
		INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Profit de service à la clientèle', @PaymentAmount, @Lang)
END


------------------------------------------------------------------------------------
-- Verify for 'Cheque/Cash'
------------------------------------------------------------------------------------
SET	@ItemExists		= ''	
SELECT @ItemExists = PaymentType FROM #temp WHERE PaymentType = 'Cheque/Cash' OR PaymentType = 'Chèque/Comptant'
IF (@ItemExists = '' OR @ItemExists IS NULL)
BEGIN
	IF @Lang = 'EN'	
		INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Cheque/Cash', @PaymentAmount, @Lang)
	ELSE		
		INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Chèque/Comptant', @PaymentAmount, @Lang)
END

------------------------------------------------------------------------------------
-- Verify for 'Master Card'
------------------------------------------------------------------------------------
SET	@ItemExists		= ''	
SELECT @ItemExists = PaymentType FROM #temp WHERE PaymentType = 'Master Card' 
IF (@ItemExists = '' OR @ItemExists IS NULL)
	INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Master Card', @PaymentAmount, @Lang)

------------------------------------------------------------------------------------
-- Verify for 'Visa'
------------------------------------------------------------------------------------
SET	@ItemExists		= ''	
SELECT @ItemExists = PaymentType FROM #temp WHERE PaymentType = 'Visa' 
IF (@ItemExists = '' OR @ItemExists IS NULL)
	INSERT #temp(PaymentType,PaymentAmount,Lang) VALUES('Visa', @PaymentAmount, @Lang)





-- Return the result
SELECT	PaymentType,
		PaymentAmount
FROM	#temp	
ORDER BY PaymentType	

-- Free the ressources
DROP TABLE #Temp

SET NOCOUNT OFF
GO
