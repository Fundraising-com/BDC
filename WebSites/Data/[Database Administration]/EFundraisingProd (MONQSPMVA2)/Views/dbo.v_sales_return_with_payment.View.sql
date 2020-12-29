USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_sales_return_with_payment]    Script Date: 02/14/2014 13:02:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_sales_return_with_payment]
AS
SELECT     dbo.Payment.Sales_ID, dbo.Consultant.Name AS Leadconsultant, dbo.Payment.Payment_Entry_Date, dbo.Payment.Payment_Amount, 
                      dbo.Payment.Commission_Paid, Consultant_1.Name AS SaleConsultant, dbo.Sales_Change_Log.Change_Date_Time, 
                      dbo.Sales_Change_Log.User_Name, dbo.Sales_Change_Log.From_Value, dbo.Production_Status.Description AS From_desc, 
                      dbo.Sales_Change_Log.To_Value, Production_Status_1.Description AS To_desc
FROM         dbo.Lead INNER JOIN
                      dbo.Client ON dbo.Lead.Lead_ID = dbo.Client.Lead_ID INNER JOIN
                      dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID INNER JOIN
                      dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN
                      dbo.Consultant Consultant_1 ON dbo.Client.CSR_Consultant_Id = Consultant_1.Consultant_ID INNER JOIN
                      dbo.Sales_Change_Log ON dbo.Sale.Sales_ID = dbo.Sales_Change_Log.Sales_ID INNER JOIN
                      dbo.Production_Status ON dbo.Sales_Change_Log.From_Value = dbo.Production_Status.Production_Status_ID INNER JOIN
                      dbo.Production_Status Production_Status_1 ON dbo.Sales_Change_Log.To_Value = Production_Status_1.Production_Status_ID
WHERE     (dbo.Sale.Production_Status_ID = 7) AND (dbo.Sale.Box_Return_Date IS NOT NULL) AND (dbo.Sale.Reship_Date IS NULL) AND 
                      (dbo.Sales_Change_Log.Column_Name = 'Production_Status_ID') AND (dbo.Sales_Change_Log.To_Value = '7')
GO
