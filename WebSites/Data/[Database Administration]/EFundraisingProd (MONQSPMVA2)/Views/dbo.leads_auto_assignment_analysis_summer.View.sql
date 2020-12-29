USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[leads_auto_assignment_analysis_summer]    Script Date: 02/14/2014 13:02:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       VIEW [dbo].[leads_auto_assignment_analysis_summer]
AS
SELECT     Count(dbo.Lead.Lead_ID) As Count_Leads, 
		Sum(Case when Sale.Sales_ID is NULL  then  0 else 1 end ) AS Count_Sales, 
		Sum(isnull(Sale.Total_Amount,0)) as Total_Amount,
		dbo.Lead.Channel_Code, dbo.Lead.Promotion_ID, 
		(Case when dbo.Lead.Day_Phone is null then 0 else 1 end ) as Day_phone,
		(Case when dbo.Lead.Evening_Phone is null then 0 else 1 end ) as Evening_phone, 
		dbo.Lead.Group_Type_ID, dbo.Lead.State_Code, 
		dbo.Lead.Participant_Count, dbo.Lead.Decision_Maker, dbo.Lead.Fund_Raiser_Start_Date, dbo.Lead.Organization_Type_Id
	FROM    dbo.Lead LEFT OUTER JOIN
                dbo.Client ON dbo.Lead.Lead_ID = dbo.Client.Lead_ID LEFT OUTER JOIN
                dbo.Sale ON dbo.Client.Client_Sequence_Code = dbo.Sale.Client_Sequence_Code AND dbo.Client.Client_ID = dbo.Sale.Client_ID
	WHERE  (dbo.Lead.Lead_Entry_Date BETWEEN CONVERT(DATETIME, '2002-06-01 00:00:00', 102) AND CONVERT(DATETIME, '2002-08-30 23:59:59', 102)) AND 
               (dbo.Sale.Sales_Date BETWEEN CONVERT(DATETIME, '2002-06-01 00:00:00', 102) AND CONVERT(DATETIME, '2002-08-30 23:59:59', 102)) OR
               (dbo.Lead.Lead_Entry_Date BETWEEN CONVERT(DATETIME, '2002-06-01 00:00:00', 102) AND CONVERT(DATETIME, '2002-08-30 23:59:59', 102)) AND 
               (dbo.Sale.Sales_Date IS NULL)
	GROUP BY dbo.Lead.Channel_Code, dbo.Lead.Promotion_ID, dbo.Lead.Group_Type_ID, dbo.Lead.State_Code, 
		(Case when dbo.Lead.Day_Phone is null then 0 else 1 end ),
		(Case when dbo.Lead.Evening_Phone is null then 0 else 1 end ),
		dbo.Lead.Participant_Count, dbo.Lead.Decision_Maker, dbo.Lead.Fund_Raiser_Start_Date, dbo.Lead.Organization_Type_Id
GO
