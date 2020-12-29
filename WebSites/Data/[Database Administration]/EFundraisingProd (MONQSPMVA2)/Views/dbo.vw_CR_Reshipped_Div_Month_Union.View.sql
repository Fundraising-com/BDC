USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_CR_Reshipped_Div_Month_Union]    Script Date: 02/14/2014 13:02:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_CR_Reshipped_Div_Month_Union]
AS
SELECT     Currency_Code, Sales_Year, Sales_Month, Division_ID, Total_Product, Total_Shipping, Total_GST, Total_QST
FROM         vw_CR_Reshipped_Div_Month_Sales
UNION
SELECT     Currency_Code, Sales_Year, Sales_Month, Division_ID, Total_Product, Total_Shipping, Total_GST, Total_QST
FROM         vw_CR_Reshipped_Div_Month_Sales_None
GO
