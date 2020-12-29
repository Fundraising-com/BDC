USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_active_leads_informations]    Script Date: 02/14/2014 13:08:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_active_leads_informations](@date_from DATETIME  = NULL, @date_to DATETIME = NULL) AS
	SELECT  dbo.Lead.Lead_ID, dbo.Lead.Lead_Entry_Date, dbo.Lead.First_Name, dbo.Lead.Last_Name, dbo.Lead.Organization, 
                      dbo.Lead.Street_Address, dbo.Lead.City, dbo.Lead.State_Code, dbo.Lead.Country_Code, dbo.Lead.Zip_Code, dbo.Lead.Day_Phone, dbo.Lead.Fax, 
                      dbo.Lead.Email, dbo.Lead.Member_Count, dbo.Lead.Fund_Raising_Goal, dbo.Lead.Participant_Count, dbo.Lead.Fund_Raiser_Start_Date, 
                      dbo.Lead.kit_sent, dbo.Organization_Type.Organization_Type_Desc, dbo.Group_Type.Description
	FROM         dbo.Lead INNER JOIN
                      dbo.Promotion ON dbo.Lead.Promotion_ID = dbo.Promotion.Promotion_ID INNER JOIN
                      dbo.Partner ON dbo.Promotion.Partner_ID = dbo.Partner.Partner_ID INNER JOIN
                      dbo.Organization_Type ON dbo.Lead.Organization_Type_Id = dbo.Organization_Type.Organization_Type_Id INNER JOIN
                      dbo.Group_Type ON dbo.Lead.Group_Type_ID = dbo.Group_Type.Group_Type_ID
	WHERE     (dbo.Partner.Partner_ID = 8)  AND dbo.Lead.Lead_Entry_Date BETWEEN isnull(@date_from, Lead_Entry_Date) AND isnull(@date_to, Lead_Entry_Date)
	ORDER BY dbo.Lead.Lead_Entry_Date DESC;
GO
