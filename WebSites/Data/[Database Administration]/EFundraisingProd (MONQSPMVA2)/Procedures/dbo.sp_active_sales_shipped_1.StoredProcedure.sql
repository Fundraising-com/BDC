USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_active_sales_shipped_1]    Script Date: 02/14/2014 13:08:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[sp_active_sales_shipped_1] (@date_from VARCHAR(10) = NULL, @date_to VARCHAR(10) = NULL) AS
	DECLARE @date_from_temp DATETIME;
	DECLARE @date_to_temp DATETIME;

	if (ltrim(@date_from) = '' )
		SET @date_from_temp = null;
	else
		SET @date_from_temp = @date_from;--Convert(DATETIME, @date_from, 126);

	if (ltrim(@date_to) = '' )
		SET @date_to_temp = null;
	else
		SET @date_to_temp = @date_to;--Convert(DATETIME, @date_to, 126);

	SELECT     dbo.Sale.Sales_ID, dbo.Product_Class.Product_Class_ID, dbo.Product_Class.Description,  dbo.Sale.Sales_Date, dbo.Sale.Total_Amount, dbo.Client.Organization, 
                      dbo.Client.Salutation, dbo.Client.First_Name, dbo.Client.Last_name, dbo.Client.Title, dbo.Client.Day_Phone, dbo.Client.Fax, dbo.Client.Email, 
                      dbo.Client.Lead_ID, dbo.Client_Address.Street_Address, dbo.Client_Address.State_Code, dbo.Client_Address.City, dbo.Client_Address.Zip_Code, 
                      dbo.Client_Address.Country_Code
	FROM         dbo.Product_Class INNER JOIN
                      dbo.ActiveSaleIDView INNER JOIN
                      dbo.Sale ON dbo.ActiveSaleIDView.Sales_ID = dbo.Sale.Sales_ID INNER JOIN
                      dbo.Sales_Item ON dbo.Sale.Sales_ID = dbo.Sales_Item.Sales_ID INNER JOIN
                      dbo.Scratch_Book ON dbo.Sales_Item.Scratch_Book_ID = dbo.Scratch_Book.Scratch_Book_ID ON 
                      dbo.Product_Class.Product_Class_ID = dbo.Scratch_Book.Product_Class_ID INNER JOIN
                      dbo.Client ON dbo.Sale.Client_ID = dbo.Client.Client_ID AND dbo.Sale.Client_Sequence_Code = dbo.Client.Client_Sequence_Code INNER JOIN
                      dbo.Client_Address ON dbo.Client.Client_ID = dbo.Client_Address.Client_ID AND 
                      dbo.Client.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code
	WHERE dbo.Sale.Sales_Date BETWEEN isnull(@date_from_temp, Sales_Date) AND isnull(@date_to_temp, Sales_Date)
	GROUP BY dbo.Sale.Sales_ID, dbo.Product_Class.Product_Class_ID,dbo.Product_Class.Description, dbo.Sale.Sales_Date, dbo.Sale.Total_Amount, dbo.Client.Organization, 
                      dbo.Client.Salutation, dbo.Client.First_Name, dbo.Client.Last_name, dbo.Client.Title, dbo.Client.Day_Phone, dbo.Client.Fax, dbo.Client.Email, 
                      dbo.Client.Lead_ID, dbo.Client_Address.Street_Address, dbo.Client_Address.State_Code, dbo.Client_Address.City, dbo.Client_Address.Zip_Code, 
                      dbo.Client_Address.Country_Code
	ORDER BY dbo.Sale.Sales_Date DESC;
GO
