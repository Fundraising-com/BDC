USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_kickoff_by_partner_id]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pavel Tarassov	
-- Create date: 11-06-2013
-- Description:	get kickoff by partner and filter by create date
-- exec [dbo].[es_get_kickoff_by_partner_id] 58, '08/06/2013'
-- =============================================
CREATE PROCEDURE [dbo].[es_get_kickoff_by_partner_id]

	@partner_id int, 
	@start_date datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
            e.event_id 
            ,e.event_name
            ,MIN (ep.event_participation_id) as event_participation_id
            ,MIN (ep.create_date) as create_date
			 FROM [event] as e
				INNER JOIN event_participation as ep
					ON ep.event_id = e.event_id
				INNER JOIN member_hierarchy as mh
					ON mh.member_hierarchy_id = ep.member_hierarchy_id
				INNER JOIN member as m ON m.member_id = mh.member_id
				inner join event_group eg on eg.event_id = ep.event_id
				inner join [group] g on g.group_id = eg.group_id
				inner join partner p on p.partner_id = g.partner_id
				inner join touch t on t.event_participation_id = ep.event_participation_id
				inner join touch_info ti on ti.touch_info_id = t.touch_info_id
			 WHERE ( m.email_address is not null and m.email_address not like '%@efundraising.com')
			   AND mh.creation_channel_id IN (7,20,23) 
			   and t.processed = 2 and ti.business_rule_id in 
			   (96,97,98,99,100,101,102,103,104,105,106,107,108,134,135,136,137,138,139,140,141,142,143,144,145,146,153,156,164,165,166,167,168,169,170,171,172,173,174, 176,177,178,179,180,181,182,183,218)
			    and g.partner_id = @partner_id
			    
			 GROUP BY e.event_id, e.event_name
			 HAVING MIN(e.create_date) > DATEADD(d,DATEDIFF(d,0,@start_date),0)
END
GO
