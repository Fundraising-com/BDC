USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[InventoryLevel_SelectMissing]    Script Date: 06/07/2017 09:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryLevel_SelectMissing]

AS

SELECT	'No Inventory Level File has been received from PeopleSoft in at least 3 days'
WHERE NOT EXISTS	(SELECT TOP 1
							InventoryLevelID
					FROM	InventoryLevel
					WHERE	CreateDate > DATEADD(DAY, -3, GETDATE()))
GO
