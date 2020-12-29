USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_getCollectionPerAgeing]    Script Date: 02/14/2014 13:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_getCollectionPerAgeing]
AS
SELECT     TOP 100 PERCENT dbo.Country.Currency_Code, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, '2003-11-30') AS Ageing, 
                      SUM(dbo.Payment.Payment_Amount) AS Collection
FROM         dbo.Sale INNER JOIN
                      dbo.Client_Address ON dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code AND 
                      dbo.Sale.Client_ID = dbo.Client_Address.Client_ID INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code INNER JOIN
                      dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID INNER JOIN
                      dbo.Consultant ON dbo.Sale.Consultant_ID = dbo.Consultant.Consultant_ID
WHERE     (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= '2003-11-30') AND (dbo.Payment.Payment_Entry_Date BETWEEN 
                      CONVERT(DATETIME, '2003-11-1', 102) AND CONVERT(DATETIME, '2003-11-30', 102)) AND (dbo.Consultant.Is_Fm = 0) OR
                      (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= '2003-11-30') AND (dbo.Payment.Payment_Entry_Date BETWEEN 
                      CONVERT(DATETIME, '2003-11-1', 102) AND CONVERT(DATETIME, '2003-11-30', 102)) AND (dbo.Consultant.Is_Fm = 0)
GROUP BY dbo.Country.Currency_Code, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, '2003-11-30')
ORDER BY dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, '2003-11-30')
GO
