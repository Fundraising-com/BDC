USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_culture_subdivision_names]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Culture_subdivision_name
CREATE PROCEDURE [dbo].[efrstore_get_culture_subdivision_names] AS
begin

select Culture_code, Subdivision_code, Subdivision_name from Culture_subdivision_name

end
GO
