USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_action]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Action
CREATE PROCEDURE [dbo].[es_insert_action] @Action_id int OUTPUT, @Action_desc varchar(255), @Create_date datetime AS
begin

insert into Action(Action_desc, Create_date) values(@Action_desc, @Create_date)

select @Action_id = SCOPE_IDENTITY()

end
GO
