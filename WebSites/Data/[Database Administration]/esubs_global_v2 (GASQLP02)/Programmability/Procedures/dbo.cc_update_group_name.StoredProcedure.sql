USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_group_name]    Script Date: 02/14/2014 13:05:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from event where event_id =
--dbo.cc_get_general_info 
/*dbo.cc_get_account_info 692221

Author	:	Jf Lavigne 2005-02-08
Description:	This stored procedure updates the information for a campaign.
*/
create   PROCEDURE [dbo].[cc_update_group_name]
	 @event_id INT,
         @group_name varchar(100)
as

update [group]
set group_name = @group_Name 
from [group] gr inner join
       event_group eg on gr.group_id = eg.group_id 

where event_id = @event_id
GO
