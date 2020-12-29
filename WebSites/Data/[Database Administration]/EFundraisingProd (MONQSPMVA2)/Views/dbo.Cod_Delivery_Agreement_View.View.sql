USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Cod_Delivery_Agreement_View]    Script Date: 02/14/2014 13:01:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Cod_Delivery_Agreement_View]
as

SELECT Sale_To_Add.Lead_ID AS lead_id, 
Lead.Organization as organization, 
[Salutation] + ' ' + [First_Name] + ' ' + [Last_Name] AS [name], 
Title.Title_Desc AS title, 
Lead.Day_Phone, 
Lead.Evening_Phone, 
Lead.Fax, 
Lead.Email, 
Lead.Street_Address AS Street, 
Lead.City, Lead.State_Code AS State, Country.Country_Name AS Country, 
Lead.Zip_Code, Sale_To_Add.Sales_Date, Payment_Term.Description AS PaymentTerm, 
Payment_Method.Description AS PaymentMethod, [Shipping_Fees]-[Shipping_Fees_Discount] AS Shipping, 
Scratch_Book.Description AS ScratchBook, Sales_Item_To_Add.Quantity_Sold, 
Sales_Item_To_Add.Quantity_Free, Sales_Item_To_Add.Unit_Price_Sold, 
[Raising_Potential]*([quantity_Sold]+[Quantity_Free]) AS Potentiel, 
Sales_Item_To_Add.Discount_Amount, Sales_Item_To_Add.Sales_Amount AS Amount, 
Consultant.Name AS TM, Sale_To_Add.Total_Amount AS Total, 
(CASE WHEN [Sales_Item_To_Add].[Group_Name] like 'Thank you for supporting' then ' ' else 'Text on Cards : ' + CONVERT(VARCHAR(10), [Sales_Item_To_Add].[Group_Name]) end) AS [Text], 
Sale_To_Add.Sale_To_Add_ID
FROM (Consultant INNER JOIN (Scratch_Book 
RIGHT JOIN (Payment_Term INNER JOIN (Payment_Method 
INNER JOIN (((Sale_To_Add INNER JOIN Sales_Item_To_Add ON Sale_To_Add.Sale_To_Add_ID = Sales_Item_To_Add.Sale_To_Add_ID) 
INNER JOIN Lead ON Sale_To_Add.Lead_ID = Lead.Lead_ID) 
INNER JOIN Country ON Lead.Country_Code = Country.Country_Code) ON Payment_Method.Payment_Method_ID = Sale_To_Add.Payment_Method_ID) ON Payment_Term.Payment_Term_ID = Sale_To_Add.Payment_Term_ID) ON Scratch_Book.Scratch_Book_ID = Sales_Item_To_Add.Scratch_Book_ID) ON Consultant.Consultant_ID = Lead.Consultant_ID) 
INNER JOIN Title ON Lead.Title_Id = Title.Title_Id
GO
