USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_sp_GetPartnerInfoFromLeadID]    Script Date: 02/14/2014 13:04:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[efr_call_sp_GetPartnerInfoFromLeadID](@intLeadID AS INT)
AS

DECLARE @intPartnerID AS INT

	SELECT @intPartnerID = p.partner_id
	FROM Lead l 
	INNER JOIN promotion p
		ON p.Promotion_id = l.promotion_id
	WHERE lead_id = @intLeadID		

	SELECT partner_id, 
		partner_group_type_id,
		partner_name, 
		partner_path, 
		eSubs_url, eStore_url,
		free_kit_url, 
		phone_number, 
		url, 
		guid, 
		prize_eligible, 
		has_collection_site
	FROM Partner
	WHERE partner_id = @intPartnerID
GO
