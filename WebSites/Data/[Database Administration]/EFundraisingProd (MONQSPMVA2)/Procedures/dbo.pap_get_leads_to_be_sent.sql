SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: June 2, 2015
-- Description:	Returns Leads created by a Kit Request (PROMOTION ID 42,43 and 44)
-- =============================================
ALTER PROCEDURE pap_get_leads_to_be_sent	
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
	LEAD.lead_id,
	LEAD.promotion_id
	FROM
	lead LEAD (NOLOCK)
	WHERE
	LEAD.fk_kit_type_id IN (42,43,44)
	AND (LEAD.sent_to_pap IS NULL OR LEAD.sent_to_pap = 0)
	AND LEAD.promotion_id > 0
	AND LEAD.lead_entry_date > '01-JUN-2015' -- DATE WHEN WE STARTED THE CONSOLE APP
END
GO
grant exec on pap_get_leads_to_be_sent to db_stored_proc_exec 



-- =============================================
-- Author:		Javier Arellano
-- Create date: June 2, 2015
-- Description:	Updates the lead sent to PAP
-- =============================================
CREATE PROCEDURE pap_update_lead_sent_to_pap @lead_id INT
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE lead SET sent_to_pap = 1 WHERE lead_id = @lead_id
END
GO
grant exec on pap_update_lead_sent_to_pap to db_stored_proc_exec 

select * from lead where sent_to_pap = 1