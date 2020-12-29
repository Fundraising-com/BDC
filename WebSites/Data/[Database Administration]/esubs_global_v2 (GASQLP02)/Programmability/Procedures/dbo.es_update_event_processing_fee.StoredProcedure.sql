USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_event_processing_fee]    Script Date: 02/14/2014 13:07:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		Jiro Hidaka
-- Create date: April 8,2010
-- Description:	Proc to update the event table's
--              'processing_fee' column.
--
-- NOTE:        I decided not to modify the existing
--              procs used for events because this
--              fee structure might be turned off
--              completely depeding on how it affects
--              the overall sales. Business is 
--              currently testing it out.
-- =================================================
CREATE PROCEDURE [dbo].[es_update_event_processing_fee]
	@event_id int,
    @processing_fee bit
AS
BEGIN
	DECLARE @errorCode int	

	BEGIN TRANSACTION
	
	UPDATE event 
	SET processing_fee = @processing_fee
	WHERE event_id = @event_id
	
	SET @errorCode = @@error
	
	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -1
	END
	
	COMMIT TRANSACTION
	RETURN 0
END
GO
