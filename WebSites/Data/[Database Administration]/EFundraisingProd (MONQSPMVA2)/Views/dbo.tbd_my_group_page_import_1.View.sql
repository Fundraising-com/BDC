USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[tbd_my_group_page_import_1]    Script Date: 02/14/2014 13:02:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[tbd_my_group_page_import_1]
AS
SELECT TOP 400 Lead_Entry_Date, Organization, Consultant_ID, Lead_ID, First_Name, Last_Name, Email, Account_Number, Street_Address, City, 
               State_Code, Country_Code, Zip_Code
FROM  dbo.Lead
WHERE (Consultant_ID = 631) AND (Account_Number IS NULL) AND (Email IS NOT NULL)
ORDER BY Lead_Entry_Date DESC
GO
