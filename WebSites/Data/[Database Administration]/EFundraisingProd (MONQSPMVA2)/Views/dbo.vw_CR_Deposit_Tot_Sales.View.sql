USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Deposit_Tot_Sales]    Script Date: 02/14/2014 13:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Deposit_Tot_Sales]
AS
SELECT     dbo.Deposit_Item.Sales_ID, SUM(dbo.Payment.Payment_Amount) AS Total_Deposit
FROM         dbo.Payment INNER JOIN
                      dbo.Deposit INNER JOIN
                      dbo.Deposit_Item ON dbo.Deposit.Deposit_ID = dbo.Deposit_Item.Deposit_ID ON dbo.Payment.Payment_No = dbo.Deposit_Item.Paiement_No AND 
                      dbo.Payment.Sales_ID = dbo.Deposit_Item.Sales_ID
GROUP BY dbo.Deposit_Item.Sales_ID
GO
