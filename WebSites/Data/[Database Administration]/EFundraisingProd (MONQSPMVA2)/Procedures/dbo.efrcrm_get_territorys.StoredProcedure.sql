USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_territorys]    Script Date: 02/14/2014 13:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Territory
CREATE PROCEDURE [dbo].[efrcrm_get_territorys] AS
begin

select Territory_id, Territory_name from Territory

end
GO
