USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_statement_account]    Script Date: 02/14/2014 13:08:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rs_statement_account]  AS

SELECT top 10
Sale.sales_id, Client_Address.country_code, Client_Address.zip_code, Client_Address.city, 
Client_Address.state_code, Client_Address.street_address, AR_Amount.AR_Amount, Sale.payment_due_date, 
Total_Payment.Payment_Amount AS PaymentAmount, 
DateDiff(day,[Payment_Due_Date], getDate()) AS Late, 
Sale.client_id, Sale.client_sequence_code, Client.organization, Client.first_name, Client.last_name, 
Sale.invoice_date, Sale.total_amount, Sale.billing_company_id
FROM 
(Client_Address INNER JOIN 
((Sale INNER JOIN AR_Amount ON Sale.sales_id = AR_Amount.Sales_ID) 
INNER JOIN 
Client ON (Sale.client_id = Client.client_id) AND (Sale.client_sequence_code = Client.client_sequence_code)) ON (Client.client_id = Client_Address.client_id) AND (Client_Address.client_sequence_code = Client.client_sequence_code)) LEFT JOIN Total_Payment ON Sale.sales_id = Total_Payment.Sales_ID
WHERE (((Client_Address.address_type)='bt'))
GO
