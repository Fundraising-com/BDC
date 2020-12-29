USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_product_offer_by_id]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 09/12/2011
-- Description:	<Description,,>
-- exec es_get_partner_product_offer_by_id 833
-- =============================================
CREATE PROCEDURE [dbo].[es_get_partner_product_offer_by_id]
	@partner_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [partner_id]
          ,[product_offer_id]
    FROM   [esubs_global_v2].[dbo].[partner_product_offer]
	WHERE  partner_id = @partner_id
END
GO
