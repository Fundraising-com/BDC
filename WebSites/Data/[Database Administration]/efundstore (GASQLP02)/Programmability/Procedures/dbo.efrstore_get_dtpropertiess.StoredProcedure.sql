USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_dtpropertiess]    Script Date: 02/14/2014 13:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Dtproperties
CREATE PROCEDURE [dbo].[efrstore_get_dtpropertiess] AS
begin

select Id, Objectid, Property, Value, Uvalue, Lvalue, Version from Dtproperties

end
GO
