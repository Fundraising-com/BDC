USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_hockey_mailing_information]    Script Date: 02/14/2014 13:02:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_hockey_mailing_information]
AS
SELECT     dbo.Lead.Lead_ID, YEAR(dbo.Lead.Lead_Entry_Date) AS Year, MONTH(dbo.Lead.Lead_Entry_Date) AS Month, dbo.Lead.Salutation, 
                      dbo.Lead.First_Name, dbo.Lead.Last_Name, dbo.Lead.Organization, dbo.Lead.Street_Address, dbo.Lead.City, dbo.Lead.State_Code, 
                      dbo.Lead.Country_Code, dbo.Lead.Zip_Code, dbo.Lead.Day_Phone, dbo.Lead.Day_Phone_Ext, dbo.Lead.Day_Time_Call, dbo.Lead.Evening_Phone, 
                      dbo.Lead.Evening_Phone_Ext, dbo.Lead.Evening_Time_Call, dbo.Lead.Other_Phone, dbo.Lead.Email, dbo.Group_Type.Description, 
                      dbo.Lead.Participant_Count
FROM         dbo.Lead INNER JOIN
                      dbo.Group_Type ON dbo.Lead.Group_Type_ID = dbo.Group_Type.Group_Type_ID
WHERE     (dbo.Group_Type.Description LIKE 'Hockey%') OR
                      (dbo.Lead.Organization LIKE 'Hockey%')
GO
