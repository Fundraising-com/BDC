USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Total_Deposit_By_Sales]    Script Date: 02/14/2014 13:02:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Total_Deposit_By_Sales]
AS
SELECT     dbo.Payment.Sales_ID, SUM(dbo.Payment.Payment_Amount) AS Total_Deposit
FROM         dbo.Deposit INNER JOIN
                      dbo.Deposit_Item ON dbo.Deposit.Deposit_ID = dbo.Deposit_Item.Deposit_ID INNER JOIN
                      dbo.Payment ON dbo.Deposit_Item.Sales_ID = dbo.Payment.Sales_ID AND dbo.Deposit_Item.Paiement_No = dbo.Payment.Payment_No
GROUP BY dbo.Payment.Sales_ID
GO
