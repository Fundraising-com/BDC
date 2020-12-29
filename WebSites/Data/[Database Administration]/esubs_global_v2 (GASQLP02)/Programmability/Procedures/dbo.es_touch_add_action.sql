CREATE PROCEDURE [dbo].[es_touch_add_action]
	(@touch_id int, @action_id int)
AS
BEGIN
	SET NOCOUNT ON;
	
    INSERT INTO dbo.touch_action (touch_id, action_date, action_id, create_date)
	VALUES (@touch_id, GETDATE(), @action_id, GETDATE())
END