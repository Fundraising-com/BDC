UPDATE [GA].[AR].[CustomerOrderAR]
   SET [TransactionDateTime] = DATEADD (dd, -3 , [TransactionDateTime] ) --move 3 days back
WHERE [TransactionDateTime] > DATEADD (dd, -1 , GETDATE() ) --orders from the last 24 hours
AND [PaymentGatewayCode] = 1 --Paypal
AND [ARTransactionTypeID] = 3--Credit Card Payment
AND [CustomerOrderARStateID] = 2--Authorized
AND ISNULL([AmountCaptured],0.00) = 0.00
AND @@servername = 'GASQLT01' --TEST ONLY!!!