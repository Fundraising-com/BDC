USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentBatch_Exported]    Script Date: 06/07/2017 09:20:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentBatch_Exported]

	@ShipmentBatchID			INT,
	@ShipmentSummaryFilename	VARCHAR(150)

AS

UPDATE	ShipmentBatch
SET		[Filename] = @ShipmentSummaryFilename
WHERE	ID = @ShipmentBatchID
GO
