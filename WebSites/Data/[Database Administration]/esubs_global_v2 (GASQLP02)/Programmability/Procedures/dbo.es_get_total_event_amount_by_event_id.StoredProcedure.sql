USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_total_event_amount_by_event_id]    Script Date: 02/14/2014 13:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: August 29, 2011
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_total_event_amount_by_event_id] 
	@event_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT  id, event_id, items, total_amount, total_supporters, total_donation_amount, total_donars, total_profit, create_date
	FROM	event_total_amount WITH (NOLOCK)
	WHERE 
		(event_id = @event_id);

END
GO
