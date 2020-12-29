USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_update_lead_activity]    Script Date: 02/14/2014 13:03:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efr_update_lead_activity]
	@lead_activity_id int
	, @lead_id int
	, @lead_activity_type_id int
	, @lead_activity_date datetime
	, @completed_date datetime
	, @comments text AS
begin

    update lead_activity set
	lead_id=@lead_id
	, lead_activity_type_id=@lead_activity_type_id
	, lead_activity_date=@lead_activity_date
	, completed_date=@completed_date
	, comments=@comments

    where lead_activity_id=@lead_activity_id

end
GO
