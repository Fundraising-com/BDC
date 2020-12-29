USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_processing_fee]    Script Date: 02/14/2014 13:05:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: April 08,2010
-- Description:	Proc to retrieve the event table's
--              'processing_fee' column.
--
-- NOTE:        I decided not to modify the existing
--              procs used for events because this
--              fee structure might be turned off
--              completely depeding on how it affects
--              the overall sales. Business is 
--              currently testing it out.
-- =============================================
CREATE PROCEDURE [dbo].[es_get_event_processing_fee]
	@eventID int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT processing_fee  	
	FROM   event 
	WHERE (event_id = @eventID)
END
GO
