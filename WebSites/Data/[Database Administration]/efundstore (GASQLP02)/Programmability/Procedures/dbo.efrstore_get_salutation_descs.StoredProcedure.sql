USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_salutation_descs]    Script Date: 02/14/2014 13:05:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Salutation_desc
CREATE PROCEDURE [dbo].[efrstore_get_salutation_descs] AS
begin

select Salutation_id, Culture_code, Description from Salutation_desc

end
GO
