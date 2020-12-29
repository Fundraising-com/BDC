SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: October 16, 2014
-- Description:	Finds the informationg regarding the partner, to be rendered
-- =============================================
CREATE PROCEDURE es_get_partner_branding
	@partnerId INT
AS
BEGIN
	SET NOCOUNT ON;
    SELECT
		P.partner_id
		,ISNULL(PPO.product_offer_id, 1) product_offer_id
		,ISNULL(PP.program_id, 1) program_id
		,ISNULL(PPC.payment_to, 0) payment_to
		,ISNULL(PCL.culture_code, 'en-US') culture_code
	FROM
		[dbo].[partner] P (NOLOCK)
		LEFT JOIN [dbo].[partner_product_offer] PPO (NOLOCK) ON P.partner_id = PPO.partner_id
		LEFT JOIN [dbo].[program_partner] PP (NOLOCK) ON P.partner_id = PP.partner_id
		LEFT JOIN [dbo].[partner_payment_config] PPC (NOLOCK) ON P.partner_id = PPC.partner_id
		LEFT JOIN [dbo].[partner_culture_link] PCL (NOLOCK) ON P.partner_id = PCL.partner_id AND P.partner_id = PCL.linked_partner_id
		WHERE P.partner_id = @partnerId
END
GO
--grant exec on es_get_partner_branding to db_stored_proc_exec 