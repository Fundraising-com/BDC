USE [eFundraisingProd]
GO

/****** Object:  StoredProcedure [dbo].[pap_set_product_to_campaign]    Script Date: 02/20/2015 10:10:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pap_set_product_to_campaign]
@productId int,
@campaignId int
AS
BEGIN
DECLARE @id int
	IF @campaignId = 0
		BEGIN
			DELETE FROM pap_scratchbook_campaign WHERE scratch_book_id = @productId;
		END
	ELSE
		BEGIN
			SELECT @id = id FROM pap_scratchbook_campaign WHERE scratch_book_id = @productId;
			IF @id IS NULL
				BEGIN
					INSERT INTO pap_scratchbook_campaign (scratch_book_id, pap_product_category_id) VALUES (@productId, @campaignId);
				END
			ELSE
				BEGIN
					UPDATE pap_scratchbook_campaign SET pap_product_category_id = @campaignId WHERE scratch_book_id = @productId;
				END
		END	
END

GO


