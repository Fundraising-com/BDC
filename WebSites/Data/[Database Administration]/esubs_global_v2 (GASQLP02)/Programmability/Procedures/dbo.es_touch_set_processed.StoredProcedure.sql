USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_set_processed]    Script Date: 02/14/2014 13:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[es_touch_set_processed]
	@touch_id int
	,@error_code tinyint
as

update touch
set processed = @error_code
where touch_id = @touch_id

if @@error = 0
	return 0
else
	return -1
GO
