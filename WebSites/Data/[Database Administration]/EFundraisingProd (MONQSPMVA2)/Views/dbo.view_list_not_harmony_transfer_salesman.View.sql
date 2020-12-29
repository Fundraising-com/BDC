USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_not_harmony_transfer_salesman]    Script Date: 02/14/2014 13:02:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_list_not_harmony_transfer_salesman]
AS
SELECT     TOP 100 PERCENT Consultant_ID, Name
FROM         dbo.Consultant
WHERE     (Department_ID = 7) AND (NOT (Consultant_ID IN (1709, 1658, 1532, 1679, 1708, 1699, 1711, 1734, 1738, 1780)))
ORDER BY Name
GO
