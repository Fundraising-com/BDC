USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[InventoryLevel_Activate]    Script Date: 06/07/2017 09:17:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryLevel_Activate]

	@InventoryLevelID		INT

AS

UPDATE	InventoryLevel
SET		[StatusID] = 2 --2: Active
WHERE	InventoryLevelID = @InventoryLevelID
GO
