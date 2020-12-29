USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_activity]    Script Date: 02/14/2014 13:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_activity
CREATE PROCEDURE [dbo].[efrcrm_update_lead_activity] @Lead_activity_id int, @Lead_id int, @Lead_activity_type_id int, @Lead_activity_date datetime, @Completed_date datetime, @Comments text AS
begin

update Lead_activity set Lead_id=@Lead_id, Lead_activity_type_id=@Lead_activity_type_id, Lead_activity_date=@Lead_activity_date, Completed_date=@Completed_date, Comments=@Comments where Lead_activity_id=@Lead_activity_id

end
GO
