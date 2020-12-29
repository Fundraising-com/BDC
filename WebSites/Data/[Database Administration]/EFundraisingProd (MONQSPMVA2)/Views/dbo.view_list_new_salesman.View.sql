USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_new_salesman]    Script Date: 02/14/2014 13:02:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_new_salesman]
AS
SELECT     TOP 100 PERCENT Consultant_ID, Name
FROM         dbo.Consultant
WHERE     (NOT (Consultant_ID IN (8, 54, 76, 151, 238, 541, 630, 1698, 1712))) AND (Department_ID = 7) AND (Is_Active = 1) AND (Is_Fm = 0) AND (Is_Agent = 0)
ORDER BY Name
GO
