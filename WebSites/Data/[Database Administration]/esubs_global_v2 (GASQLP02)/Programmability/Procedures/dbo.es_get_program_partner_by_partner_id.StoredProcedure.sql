USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_program_partner_by_partner_id]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Program_partner
CREATE PROCEDURE [dbo].[es_get_program_partner_by_partner_id] @Partner_id int AS
begin

select Program_id, Partner_id, Program_url, Create_date from Program_partner where Partner_id=@Partner_id

end
GO
