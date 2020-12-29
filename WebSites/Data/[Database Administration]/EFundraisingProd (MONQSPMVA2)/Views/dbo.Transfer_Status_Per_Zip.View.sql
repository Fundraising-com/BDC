USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Transfer_Status_Per_Zip]    Script Date: 02/14/2014 13:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Transfer_Status_Per_Zip]
AS
SELECT dbo.consultant.consultant_transfer_status_id, dbo.Territory_Def.Zip
FROM  dbo.Territory_Def INNER JOIN
               dbo.territory ON dbo.Territory_Def.Territory_ID = dbo.territory.territory_id INNER JOIN
               dbo.consultant ON dbo.territory.territory_id = dbo.consultant.territory_id
GO
