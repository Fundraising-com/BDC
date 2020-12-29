SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: 2017-03-29
-- Description:	Returns the number of Emails waiting flagged as spam
-- =============================================
CREATE PROCEDURE report_touch_email_process_queue	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @rows INT;
	SELECT @rows = COUNT(*) FROM touch_email_process_queue WHERE status = 2
	IF (@rows < 1)
		BEGIN
			RAISERROR ('No Records Found',16,1)
		END
	ELSE
		BEGIN
			SELECT
				event_id,
				partner_id,
				COUNT(id) as Total
			FROM
				touch_email_process_queue (NOLOCK)
				WHERE status = 2
			GROUP BY
				event_id,
				partner_id
			ORDER BY 
				event_id
	END
END
GO
