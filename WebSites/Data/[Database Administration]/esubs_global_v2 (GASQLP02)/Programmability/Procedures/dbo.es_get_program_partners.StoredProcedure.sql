USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_program_partners]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Program_partner
CREATE PROCEDURE [dbo].[es_get_program_partners] AS
begin

select Program_id, Partner_id, Program_url, Create_date from Program_partner

end
GO
