USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_sales_return_with_payment_summary]    Script Date: 02/14/2014 13:02:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_sales_return_with_payment_summary]
AS
SELECT     Sales_ID, Leadconsultant, SaleConsultant, SUM(Payment_Amount) AS SumOfAmount, Change_Date_Time, User_Name, From_desc, To_desc
FROM         dbo.v_sales_return_with_payment
GROUP BY Sales_ID, Leadconsultant, SaleConsultant, Change_Date_Time, User_Name, From_desc, To_desc
GO
