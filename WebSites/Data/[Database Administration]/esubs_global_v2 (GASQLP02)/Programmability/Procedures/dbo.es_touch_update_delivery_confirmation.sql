SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: April, 2014
-- Description:	Updates the touch delivery confirmation
-- =============================================
CREATE PROCEDURE es_touch_update_delivery_confirmation
	(@touch_id int, @delivery_confirmation bit)
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO dbo.touch_action (touch_id, action_date, action_id, action_desc, create_date)
	VALUES (@touch_id, GETDATE(), 108, 'Email Opened', GETDATE())
END
GO