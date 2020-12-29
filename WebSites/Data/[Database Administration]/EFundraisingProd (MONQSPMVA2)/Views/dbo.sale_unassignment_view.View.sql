USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[sale_unassignment_view]    Script Date: 02/14/2014 13:02:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[sale_unassignment_view]

AS

SELECT TOP 100 PERCENT dbo.AR_Status.Description AS AR_Status, dbo.Sale.Total_Amount, dbo.Payment_Method.Description AS Payment_Method, 

dbo.Division.Division_Name, dbo.Consultant.Name AS Consultant, dbo.Sale.Sales_ID, dbo.Confirmation_Method.Description AS Confirmation_Method, 

dbo.Division.short_name, dbo.Sale.Payment_Due_Date, dbo.Payment_Term.Description AS Payment_Term, dbo.Client.Lead_ID, dbo.Consultant.Is_Fm, 

dbo.Sale.Box_Return_Date, dbo.Sale.Reship_Date, dbo.Client.Client_Sequence_Code, dbo.Client.Client_ID, 

dbo.v_Total_Receivable_By_Sales_View.Total_Receivable, MAX(dbo.Payment.Payment_Entry_Date) AS last_payment_date, 

SUM(dbo.Payment.Payment_Amount) AS total_payment, dbo.Collection_Status.Description AS Collection_Status, 

dbo.Credit_Approval_Method.Description AS Credit_Approval_Method, dbo.Sale.Actual_Ship_Date, dbo.Consultant.Is_Agent, 

dbo.Sale.Actual_Delivery_Date, dbo.Sale.Collection_Status_ID,

datediff(day, dbo.sale.Actual_Ship_Date,getdate())AS ageing, p.description

FROM dbo.Client INNER JOIN

dbo.Sale ON dbo.Client.Client_ID = dbo.Sale.Client_ID AND dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code INNER JOIN

dbo.AR_Status ON dbo.Sale.AR_Status_ID = dbo.AR_Status.AR_Status_ID INNER JOIN

dbo.Division ON dbo.Client.Division_ID = dbo.Division.Division_ID INNER JOIN

dbo.Consultant ON dbo.Sale.Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN

dbo.Payment_Method ON dbo.Sale.Payment_Method_ID = dbo.Payment_Method.Payment_Method_ID LEFT OUTER JOIN

dbo.Confirmation_Method ON dbo.Sale.Confirmation_Method_ID = dbo.Confirmation_Method.Confirmation_Method_ID INNER JOIN

dbo.Payment_Term ON dbo.Sale.Payment_Term_ID = dbo.Payment_Term.Payment_Term_ID INNER JOIN

dbo.v_Total_Receivable_By_Sales_View ON dbo.Sale.Sales_ID = dbo.v_Total_Receivable_By_Sales_View.Sales_ID left JOIN

dbo.Credit_Approval_Method ON dbo.Sale.Credit_Approval_Method_ID = dbo.Credit_Approval_Method.Credit_Approval_Method_ID INNER JOIN

dbo.Collection_Status ON dbo.Sale.Collection_Status_ID = dbo.Collection_Status.Collection_Status_ID LEFT OUTER JOIN

dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID inner join

dbo.promotion p on dbo.client.promotion_id = p.promotion_id

WHERE (dbo.Sale.AR_Consultant_ID IS NULL) AND (dbo.Sale.Sales_Status_ID <> 4) 

-- AND p.description not like 'LS-%'

GROUP BY dbo.AR_Status.Description, dbo.Sale.Total_Amount, dbo.Payment_Method.Description, dbo.Division.Division_Name, dbo.Consultant.Name, 

dbo.Sale.Sales_ID, dbo.Confirmation_Method.Description, dbo.Division.short_name, dbo.Sale.Payment_Due_Date, dbo.Payment_Term.Description, 

dbo.Client.Lead_ID, dbo.Sale.Box_Return_Date, dbo.Sale.Reship_Date, dbo.Client.Client_Sequence_Code, dbo.Client.Client_ID, 

dbo.v_Total_Receivable_By_Sales_View.Total_Receivable, dbo.Consultant.Is_Fm, dbo.Collection_Status.Description, 

dbo.Credit_Approval_Method.Description, dbo.Sale.Actual_Ship_Date, dbo.Consultant.Is_Agent, dbo.Sale.Actual_Delivery_Date, 

dbo.Sale.Collection_Status_ID, p.description

HAVING (dbo.Sale.Collection_Status_ID not in (6,11))

ORDER BY dbo.Sale.Actual_Ship_Date
GO
