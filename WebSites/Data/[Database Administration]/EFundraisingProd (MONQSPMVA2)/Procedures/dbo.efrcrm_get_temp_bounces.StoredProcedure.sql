USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_temp_bounces]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Temp_bounce
CREATE PROCEDURE [dbo].[efrcrm_get_temp_bounces] AS
begin

select Email from Temp_bounce

end
GO
