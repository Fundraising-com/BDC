USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_program_partners]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Program_partner
CREATE PROCEDURE [dbo].[efrstore_get_program_partners] AS
begin

select Program_id, Partner_id, Program_url, Create_date from Program_partner

end
GO
