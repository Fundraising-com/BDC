USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Daily_To_Do_Urgence]    Script Date: 02/14/2014 13:01:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Daily_To_Do_Urgence]
AS
SELECT     dbo.Consultant.Name, dbo.Lead.Lead_ID, dbo.Lead.Salutation, dbo.Lead.First_Name, dbo.Lead.Last_Name, dbo.Lead.Organization, 
                      dbo.Lead.Day_Phone, dbo.Lead.Evening_Phone, dbo.Lead.Member_Count, dbo.Lead_Activity.Comments, dbo.Lead_Activity.Completed_Date, 
                      dbo.Lead_Activity.Lead_Activity_Date, dbo.Consultant.Consultant_ID, dbo.Consultant.Is_Active, dbo.Consultant.Is_Agent, 
                      dbo.Lead.Consultant_ID AS Expr1
FROM         dbo.Lead INNER JOIN
                      dbo.Lead_Activity ON dbo.Lead.Lead_ID = dbo.Lead_Activity.Lead_Id INNER JOIN
                      dbo.Consultant ON dbo.Lead.Consultant_ID = dbo.Consultant.Consultant_ID
WHERE     (dbo.Lead_Activity.Completed_Date IS NULL) AND (dbo.Lead_Activity.Lead_Activity_Date < CONVERT(DATETIME, '2003-05-23 00:00:00', 102)) AND 
                      (dbo.Consultant.Consultant_ID <> 127) AND (dbo.Consultant.Consultant_ID <> 1474) AND (dbo.Consultant.Consultant_ID <> 936) AND 
                      (dbo.Consultant.Is_Active = 1) AND (dbo.Consultant.Is_Agent = 0) AND (dbo.Lead.Consultant_ID IN (544, 541, 924, 630, 1776, 1632))
GO
