SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: June 2, 2015
-- Description:	Returns a Partner A_Aid by its promotion
-- =============================================
CREATE PROCEDURE es_get_partner_by_promotionId
	@promotionId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
		PARTNER_VALUES.value
	FROM
		promotion PROMOTION (NOLOCK)
		JOIN partner_promotion PARTNER_PROMOTION (NOLOCK) ON PROMOTION.promotion_id = PARTNER_PROMOTION.promotion_id
		JOIN partner PARTNER (NOLOCK) ON PARTNER_PROMOTION.partner_id = PARTNER.partner_id
		JOIN partner_attribute_value PARTNER_VALUES (NOLOCK) ON PARTNER.partner_id = PARTNER_VALUES.partner_id
		WHERE
			PROMOTION.promotion_id = @promotionId
			AND PARTNER_VALUES.partner_attribute_id = 12
END
GO

grant exec on es_get_partner_by_promotionId to db_stored_proc_exec


select * from promotion