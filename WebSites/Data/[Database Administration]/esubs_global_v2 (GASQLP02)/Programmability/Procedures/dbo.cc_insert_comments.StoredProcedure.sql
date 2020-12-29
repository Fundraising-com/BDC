USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_insert_comments]    Script Date: 02/14/2014 13:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   PROCEDURE [dbo].[cc_insert_comments]
	@event_id INT
	, @comments VARCHAR(2000)
AS

IF EXISTS (
	SELECT 
		* 
	FROM 	
		custcare_comments
	WHERE
		event_id = @event_id

)
BEGIN
	
	UPDATE  
                custcare_comments
	SET 	comments = @comments
	WHERE
		event_id = @event_id

END
ELSE
BEGIN
       INSERT INTO custcare_comments values (@event_id, @comments, GETDATE())
END
GO
