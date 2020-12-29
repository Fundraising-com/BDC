USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[VIEW3]    Script Date: 02/14/2014 13:02:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VIEW3]
AS
SELECT     Lead_ID, Account_Number, First_Name, Last_Name, Organization
FROM         dbo.Lead
WHERE     (Account_Number IS NOT NULL) AND (Organization = '%%')
GO
