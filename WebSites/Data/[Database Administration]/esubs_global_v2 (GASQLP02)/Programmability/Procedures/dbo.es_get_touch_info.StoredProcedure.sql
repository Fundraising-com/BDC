USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_touch_info]    Script Date: 02/14/2014 13:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[es_get_touch_info]
    @touch_info_id int
AS
BEGIN
    SELECT ti.touch_info_id
          , ti.business_rule_id
          , ti.visitor_log_id
          , ti.launch_date
          , ti.create_date
          , subject
          , body_txt
          , body_html
        FROM touch_info as ti
            INNER JOIN custom_email_template as cet
                ON cet.touch_info_id = ti.touch_info_id
    WHERE ti.touch_info_id = @touch_info_id
END
GO
