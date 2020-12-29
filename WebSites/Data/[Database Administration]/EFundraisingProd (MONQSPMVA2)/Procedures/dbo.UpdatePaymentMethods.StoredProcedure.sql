USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePaymentMethods]    Script Date: 02/14/2014 13:09:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Update les payment methods

CREATE   PROCEDURE [dbo].[UpdatePaymentMethods]
AS
	SET NOCOUNT ON
	
	DECLARE @SaleID int;
	DECLARE @PaymentMethodID tinyint;
	DECLARE @DepositID int;
	DECLARE  CreditCardDeclinedSales CURSOR FOR
		SELECT Sales_ID FROM dbo.Sale WHERE Payment_Method_ID =  5;
		
	
	PRINT 'Updating Payment Methods';
	-- Update les payment methods simples
	UPDATE    
		dbo.Deposit
	SET              
		Payment_Method_ID = 1
	WHERE     
		(Payment_Method_ID IN (4,6,7,10));
		
	-- Update les Credit Card Declined
	OPEN CreditCardDeclinedSales;
	FETCH NEXT FROM CreditCardDeclinedSales INTO @SaleID;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @PaymentMethodID = (SELECT Payment_Method_ID FROM dbo.Payment WHERE Sales_ID = @SaleID);
		
		SET @DepositID = (SELECT deposit.Deposit_ID FROM dbo.deposit INNER JOIN dbo.Deposit_Item ON deposit.Deposit_ID = deposit_item.Deposit_ID WHERE deposit_item.Sales_ID = @SaleID);

		UPDATE dbo.Deposit
		SET Payment_Method_ID = @PaymentMethodID
		WHERE Deposit_ID = @DepositID;
		

		FETCH NEXT FROM CreditCardDeclinedSales INTO @SaleID;
	END
	
	CLOSE CreditCardDeclinedSales;
	DEALLOCATE CreditCardDeclinedSales;
	
RETURN
GO
