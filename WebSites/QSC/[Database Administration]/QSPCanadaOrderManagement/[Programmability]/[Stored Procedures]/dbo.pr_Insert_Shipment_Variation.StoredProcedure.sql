USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Insert_Shipment_Variation]    Script Date: 06/07/2017 09:20:10 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Insert_Shipment_Variation]

@SessionId varchar(100)
, @CustomerOrderHeaderInstance int
, @TransId int
, @QuantityShipped int
, @QuantityReplaced int
, @ReplacementItemId int
, @ShipTF bit
, @Comment varchar(255)
, @CustomerComment varchar(255)
, @ModifiedBy varchar(50)


AS

DELETE	ShipmentVariation
WHERE	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND		TransID = @TransId

INSERT INTO
	ShipmentVariation
	(
		SessionId
		, CustomerOrderHeaderInstance
		, TransId
		, QuantityShipped
		, QuantityReplaced
		, ReplacementItemId
		, ShipTF
		, Comment
		, CustomerComment
		, ModifiedBy
		, CreateDate
		, ModifyDate
	) VALUES (
		@SessionId
		, @CustomerOrderHeaderInstance
		, @TransId
		, @QuantityShipped
		, @QuantityReplaced
		, @ReplacementItemId
		, @ShipTF
		, @Comment
		, @CustomerComment
		, @ModifiedBy
		, getdate()
		, getdate()
	)
GO
