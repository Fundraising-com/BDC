USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_dtproperties_by_id]    Script Date: 02/14/2014 13:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Dtproperties
CREATE PROCEDURE [dbo].[efrstore_get_dtproperties_by_id] @Id int AS
begin

select Id, Objectid, Property, Value, Uvalue, Lvalue, Version from Dtproperties where Id=@Id

end
GO
