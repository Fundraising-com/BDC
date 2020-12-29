USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_action]    Script Date: 02/14/2014 13:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Action
CREATE PROCEDURE [dbo].[es_update_action] @Action_id int, @Action_desc varchar(255), @Create_date datetime AS
begin

update Action set Action_desc=@Action_desc, Create_date=@Create_date where Action_id=@Action_id

end
GO
