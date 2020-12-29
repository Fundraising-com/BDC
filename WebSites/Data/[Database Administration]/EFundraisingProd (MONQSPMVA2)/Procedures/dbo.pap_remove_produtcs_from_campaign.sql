SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: November 19, 2014
-- Description:	Removes the products related to the Campaign Id received
-- =============================================
CREATE PROCEDURE pap_remove_produtcs_from_campaign
	@campaignId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    DELETE
    FROM pap_scratchbook_campaign
    WHERE pap_product_category_id = @campaignId
END
GO
--grant exec on pap_remove_produtcs_from_campaign to db_stored_proc_exec 