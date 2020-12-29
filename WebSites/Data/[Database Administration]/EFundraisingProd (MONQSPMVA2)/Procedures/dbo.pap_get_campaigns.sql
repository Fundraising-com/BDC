USE [eFundraisingProd]
GO

/****** Object:  StoredProcedure [dbo].[pap_get_campaigns]    Script Date: 02/20/2015 09:46:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pap_get_campaigns]
AS
BEGIN
SELECT 
ISNULL(T2.pap_product_category_id, 0) AS Id,
ISNULL(T2.product_category_code, 'Default Campaign') AS Name,
COUNT(T0.scratch_book_id) AS Products,
ISNULL(T2.is_visible, 1) AS IsVisible
FROM
pap_product_category (NOLOCK) T2
LEFT JOIN pap_scratchbook_campaign(NOLOCK) T1 ON T1.pap_product_category_id = T2.pap_product_category_id
LEFT JOIN scratch_book (NOLOCK) T0 ON T0.scratch_book_id = T1.scratch_book_id
GROUP BY 
T2.pap_product_category_id,
T2.product_category_code,
T2.is_visible
ORDER BY T2.product_category_code
END
GO


