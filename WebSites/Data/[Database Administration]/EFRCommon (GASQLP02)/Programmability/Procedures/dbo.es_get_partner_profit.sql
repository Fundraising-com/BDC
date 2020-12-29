-- =============================================
-- Author:		Javier Arellano
-- Create date: October 29, 2014
-- Description:	Returns the profit for the partner received
-- =============================================
CREATE PROCEDURE es_get_partner_profit
	@partnerId INT
AS
BEGIN
	SET NOCOUNT OFF;

    SELECT
		TOP 1
		P.partner_name AS Name
		,PF.profit_percentage AS Percentage
		,PG.profit_group_id AS ProfitGroupId
		,PF.description AS Description
		,PF.disclaimer AS Disclaimer
		,P.partner_type_id AS PartnerType
	FROM
		[dbo].[partner] P (NOLOCK)
		JOIN [dbo].[partner_profit] PP (NOLOCK) ON P.partner_id = PP.partner_id
		JOIN [dbo].[profit_group] PG (NOLOCK) ON PP.profit_group_id = PG.profit_group_id
		JOIN [dbo].[profit] PF (NOLOCK) ON PP.profit_group_id = PF.profit_group_id
	WHERE
		P.partner_id = @partnerId
		AND PP.end_date IS NULL
		AND PF.qsp_catalog_type_id IS NULL
END
GO
--grant exec on es_get_partner_profit to db_stored_proc_exec 
