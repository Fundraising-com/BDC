USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_web_action]    Script Date: 02/14/2014 13:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_insert_web_action]
	@web_action_id int out,
	@event_participation_id int,
	@member_hierarchy_id int,
	@type int,
	@value varchar(50),
	@create_date datetime
AS
BEGIN

	begin transaction

	insert into web_action(
		event_participation_id
		, member_hierarchy_id
		, [type]
		, [value]
		, create_date
	) values( 
		@event_participation_id
		, @member_hierarchy_id
		, @type
		, @value
		, @create_date
	);

	select @web_action_id =  SCOPE_IDENTITY();

	commit transaction
	
END
GO
