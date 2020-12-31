USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[InventoryLevelBatch_Create]    Script Date: 06/07/2017 09:17:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InventoryLevelBatch_Create]

	@Filename				NVARCHAR(137),
	@InventoryLevelBatchID	INT OUTPUT

AS

INSERT	InventoryLevelBatch
(
	[Filename]
)
VALUES	(@Filename)

SET @InventoryLevelBatchID = SCOPE_IDENTITY()
GO
