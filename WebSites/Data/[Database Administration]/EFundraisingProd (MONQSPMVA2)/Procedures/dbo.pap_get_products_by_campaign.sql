USE [eFundraisingProd]
GO

/****** Object:  StoredProcedure [dbo].[pap_get_products_by_campaign]    Script Date: 02/20/2015 09:47:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pap_get_products_by_campaign]
@campaignId int
AS
BEGIN
SELECT 
T0.scratch_book_id AS Id,
T0.description AS Name,
T0.product_code AS Code,
T0.current_description AS Description
FROM
scratch_book (NOLOCK) T0
LEFT JOIN pap_scratchbook_campaign(NOLOCK) T1 ON T0.scratch_book_id = T1.scratch_book_id
WHERE T0.is_active = 1
AND (((@campaignId = 0 OR @campaignId = 23 ) AND T1.id IS NULL) OR (T1.pap_product_category_id = @campaignId))
ORDER BY T0.description
END
GO


