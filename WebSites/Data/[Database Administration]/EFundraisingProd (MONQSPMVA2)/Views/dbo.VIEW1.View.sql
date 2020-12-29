USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[VIEW1]    Script Date: 02/14/2014 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VIEW1]
AS
SELECT     TOP 100 PERCENT dbo.Sale.Sales_ID, dbo.Product_Class.Product_Class_ID, dbo.Product_Class.Description, dbo.Sale.Sales_Date, 
                      SUM(dbo.Sales_Item.Sales_Amount) AS total_amount, dbo.Client.Organization, dbo.Client.Salutation, dbo.Client.First_Name, dbo.Client.Last_name, 
                      dbo.Client.Title, dbo.Client.Day_Phone, dbo.Client.Fax, dbo.Client.Email, dbo.Lead.Lead_ID, dbo.Client_Address.Street_Address, 
                      dbo.Client_Address.State_Code, dbo.Client_Address.City, dbo.Client_Address.Zip_Code, dbo.Client_Address.Country_Code
FROM         dbo.Sale INNER JOIN
                      dbo.Client ON dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code AND dbo.Sale.Client_ID = dbo.Client.Client_ID INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID INNER JOIN
                      dbo.Product_Class ON dbo.Scratch_Book.Product_Class_ID = dbo.Product_Class.Product_Class_ID INNER JOIN
                      dbo.Promotion INNER JOIN
                      dbo.Lead ON dbo.Promotion.Promotion_ID = dbo.Lead.Promotion_ID ON dbo.Client.Lead_ID = dbo.Lead.Lead_ID INNER JOIN
                      dbo.Client_Address ON dbo.Client.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code AND 
                      dbo.Client.Client_ID = dbo.Client_Address.Client_ID
GROUP BY dbo.Sale.Sales_ID, dbo.Product_Class.Product_Class_ID, dbo.Product_Class.Description, dbo.Sale.Sales_Date, dbo.Client.Organization, 
                      dbo.Client.Salutation, dbo.Client.First_Name, dbo.Client.Last_name, dbo.Client.Title, dbo.Client.Day_Phone, dbo.Client.Fax, dbo.Client.Email, 
                      dbo.Lead.Lead_ID, dbo.Client_Address.Street_Address, dbo.Client_Address.State_Code, dbo.Client_Address.City, dbo.Client_Address.Zip_Code, 
                      dbo.Client_Address.Country_Code, dbo.Sale.Actual_Ship_Date, dbo.Client_Address.Address_Type, dbo.Promotion.Partner_ID
HAVING      (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Client_Address.Address_Type = 'BT') AND (dbo.Sale.Sales_Date BETWEEN CONVERT(DATETIME, 
                      '2003-01-01 00:00:00', 102) AND CONVERT(DATETIME, '2003-01-31 00:00:00', 102)) AND (dbo.Promotion.Partner_ID = 0)
ORDER BY dbo.Product_Class.Description, dbo.Sale.Sales_Date DESC
GO
