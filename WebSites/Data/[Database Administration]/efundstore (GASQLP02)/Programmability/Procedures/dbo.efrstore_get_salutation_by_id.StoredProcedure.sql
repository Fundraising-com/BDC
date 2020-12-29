USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_salutation_by_id]    Script Date: 02/14/2014 13:05:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Salutation
CREATE PROCEDURE [dbo].[efrstore_get_salutation_by_id] @Salutation_id int AS
begin

select Salutation_id, Description from Salutation where Salutation_id=@Salutation_id

end
GO
