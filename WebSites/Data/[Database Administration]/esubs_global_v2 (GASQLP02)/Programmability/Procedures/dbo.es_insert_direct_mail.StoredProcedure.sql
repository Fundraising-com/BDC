USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_direct_mail]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_insert_direct_mail] 
	@direct_mail_id int out
	, @direct_mail_info_id int 
	, @direct_mail_status smallint
	, @event_participation_id int
	, @member_hierarchy_id int
	, @postal_address_id int
	, @create_date datetime
AS
BEGIN

insert into direct_mail (
	direct_mail_info_id
	, direct_mail_status
	, create_date
	, event_participation_id
	, member_hierarchy_id
	, postal_address_id)
values(
	@direct_mail_info_id
	,@direct_mail_status
	,@create_date
	,@event_participation_id
	,@member_hierarchy_id
	,@postal_address_id
)

select @direct_mail_id = SCOPE_IDENTITY();
 
END
GO
