USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_payment_for_nancy_k]    Script Date: 02/14/2014 13:02:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_payment_for_nancy_k]
AS
SELECT     TOP 100 PERCENT YEAR(dbo.Payment.Payment_Entry_Date) AS Year, MONTH(dbo.Payment.Payment_Entry_Date) AS Month, 
                      dbo.Payment_Method.Description, dbo.Consultant.Name, SUM(dbo.Payment.Payment_Amount) AS payment_amount, 
                      dbo.Payment.Sales_ID AS count_payment
FROM         dbo.Sale INNER JOIN
                      dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID INNER JOIN
                      dbo.Payment_Method ON dbo.Payment.Payment_Method_ID = dbo.Payment_Method.Payment_Method_ID LEFT OUTER JOIN
                      dbo.Consultant ON dbo.Sale.AR_Consultant_ID = dbo.Consultant.Consultant_ID
WHERE     (dbo.Payment.Payment_Entry_Date >= CONVERT(DATETIME, '2002-01-01 00:00:00', 102))
GROUP BY dbo.Consultant.Name, MONTH(dbo.Payment.Payment_Entry_Date), YEAR(dbo.Payment.Payment_Entry_Date), dbo.Payment_Method.Description, 
                      dbo.Payment.Sales_ID
ORDER BY YEAR(dbo.Payment.Payment_Entry_Date), MONTH(dbo.Payment.Payment_Entry_Date), dbo.Consultant.Name
GO
