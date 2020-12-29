USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_member_unknown_supporter_GA]    Script Date: 02/14/2014 13:07:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Drew Pettit
-- Create date: 15 Apr 2013
-- Description:	Updates the last 2 days unknown 
--              supporter based on GA's 
--              customer data
--    EXEC [dbo].[es_update_member_unknown_supporter_GA] 2
-- =============================================
CREATE PROCEDURE [dbo].[es_update_member_unknown_supporter_GA] (@numberOfDays INT = 2)
AS
BEGIN
	SET NOCOUNT ON;
    
    -- Update the member name only
    -- we *DO NOT* reassign the sale under a correct member if it exists
    update member
    set first_name = UPPER(tps.first_name)
       ,last_name = UPPER(tps.last_name)
       ,email_address = tps.EmailAddress
    from member_hierarchy mh with (nolock)
         INNER JOIN member m
			on mh.member_id = m.member_id
        inner join event_participation ep with (nolock)
            on ep.member_hierarchy_id = mh.member_hierarchy_id
        inner join [es_get_valid_orders_items]() tps
            on tps.supp_id = ep.event_participation_id
    where m.member_id = mh.member_id
      and m.email_address like 'es%@efundraising.com'
      and m.first_name = ''
      and m.last_name = ''
      and DATEDIFF(day, tps.create_date, getdate()) <= @numberOfDays

END
GO
