USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_product_offer_by_id]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 09/12/2011
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_product_offer_by_id]
	@product_offer_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [product_offer_id]
          ,[description]
	FROM   [esubs_global_v2].[dbo].[product_offer]
	WHERE  product_offer_id = @product_offer_id
END
GO
