USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[es_get_consultant]    Script Date: 02/14/2014 13:08:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_consultant]
	@consultant_id int
AS
SELECT
	[consultant_id],
	[division_id],
	[client_id],
	[client_sequence_code],
	[department_id],
	[partner_id],
	[consultant_transfer_status_id],
	[territory_id],
	[ext_consultant_id],
	[name],
	[is_agent],
	[is_active],
	[nt_login],
	[phone_extension],
	[email_address],
	[home_phone],
	[work_phone],
	[fax_number],
	[toll_free_phone],
	[mobile_phone],
	[pager_phone],
	[default_proposal_text],
	[csr_consultant],
	[objectives],
	[is_available],
	[password],
	[kit_paid],
	[is_fm]

FROM
	efundraisingprod.dbo.consultant 
WHERE 
	consultant_id = @consultant_id
GO
