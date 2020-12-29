USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_activity]    Script Date: 02/14/2014 13:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_activity
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_activity] @Lead_activity_id int OUTPUT, @Lead_id int, @Lead_activity_type_id int, @Lead_activity_date datetime, @Completed_date datetime, @Comments text AS

declare @id int
exec @id = sp_NewID  'Lead_Activity_Id', 'All'

begin

insert into Lead_activity(Lead_activity_id, Lead_id, Lead_activity_type_id, Lead_activity_date, Completed_date, Comments) values(@id, @Lead_id, @Lead_activity_type_id, @Lead_activity_date, @Completed_date, @Comments)

end
GO
