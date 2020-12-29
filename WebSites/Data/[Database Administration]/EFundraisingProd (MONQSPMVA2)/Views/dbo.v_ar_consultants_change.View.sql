USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[v_ar_consultants_change]    Script Date: 02/14/2014 13:02:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_ar_consultants_change]
AS
SELECT dbo.Unassigned_Consultant_Sale.Sale_ID, dbo.Consultant.Name AS old_consultant, Consultant_1.Name AS new_consultant, 
               dbo.Unassigned_Consultant_Sale.Unassigned_Date
FROM  dbo.Unassigned_Consultant_Sale INNER JOIN
               dbo.Consultant ON dbo.Unassigned_Consultant_Sale.Old_Consultant_ID = dbo.Consultant.Consultant_ID INNER JOIN
               dbo.Consultant Consultant_1 ON dbo.Unassigned_Consultant_Sale.New_Consultant_ID = Consultant_1.Consultant_ID
GO
