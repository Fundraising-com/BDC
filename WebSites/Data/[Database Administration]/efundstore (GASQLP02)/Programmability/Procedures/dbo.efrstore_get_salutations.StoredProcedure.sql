USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_salutations]    Script Date: 02/14/2014 13:05:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Salutation
CREATE PROCEDURE [dbo].[efrstore_get_salutations] AS
begin

select Salutation_id, Description from Salutation

end
GO
