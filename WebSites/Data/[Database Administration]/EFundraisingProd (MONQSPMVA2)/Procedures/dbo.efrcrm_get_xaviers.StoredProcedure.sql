USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_xaviers]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Xavier
CREATE PROCEDURE [dbo].[efrcrm_get_xaviers] AS
begin

select Lead_id, Type from Xavier

end
GO
