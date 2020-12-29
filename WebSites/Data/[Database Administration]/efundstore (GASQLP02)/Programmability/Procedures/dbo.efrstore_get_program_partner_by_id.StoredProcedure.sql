USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_program_partner_by_id]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Program_partner
CREATE PROCEDURE [dbo].[efrstore_get_program_partner_by_id] @Program_id int AS
begin

select Program_id, Partner_id, Program_url, Create_date from Program_partner where Program_id=@Program_id

end
GO
