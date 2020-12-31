USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentReceiptBatch_Create]    Script Date: 06/07/2017 09:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentReceiptBatch_Create]

	@Filename				NVARCHAR(137),
	@ShipmentReceiptBatchID	INT OUTPUT

AS

INSERT	ShipmentReceiptBatch
(
	[Filename]
)
VALUES	(@Filename)

SET @ShipmentReceiptBatchID = SCOPE_IDENTITY()
GO
