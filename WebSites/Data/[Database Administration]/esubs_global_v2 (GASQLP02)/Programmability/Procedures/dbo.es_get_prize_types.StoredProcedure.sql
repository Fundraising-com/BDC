USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_prize_types]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Prize_type
CREATE PROCEDURE [dbo].[es_get_prize_types] AS
begin

select Prize_type_id, Prize_type_name, Create_date from Prize_type

end
GO
