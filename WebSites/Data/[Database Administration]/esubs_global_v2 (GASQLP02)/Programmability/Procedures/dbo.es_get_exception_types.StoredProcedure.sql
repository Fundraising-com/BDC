USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_exception_types]    Script Date: 02/14/2014 13:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Exception_type
CREATE PROCEDURE [dbo].[es_get_exception_types] AS
begin

select Exception_type_id, Description from Exception_type

end
GO
