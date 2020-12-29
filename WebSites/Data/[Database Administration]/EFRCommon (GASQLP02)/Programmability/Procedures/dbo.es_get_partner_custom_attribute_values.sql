SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: October 16, 2014
-- Description:	Returns ALL the custom attributes for a given partner
-- =============================================
CREATE PROCEDURE es_get_partner_custom_attribute_values
	@partnerId INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		P.partner_id
		,PA.partner_attribute_name
		,PAV.value
		,PAV.culture_code
	FROM
		[dbo].[partner] P (NOLOCK)
		JOIN [dbo].[partner_attribute_value] PAV (NOLOCK) ON P.partner_id = PAV.partner_id
		JOIN [dbo].[partner_attribute] PA (NOLOCK) ON PA.partner_attribute_id = PAV.partner_attribute_id
	WHERE
		P.partner_id = @partnerId
END
GO
--grant exec on es_get_partner_custom_attribute_values to db_stored_proc_exec 