USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_msysconf_by_id]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for MSysConf
CREATE PROCEDURE [dbo].[efrcrm_get_msysconf_by_id] @Config int AS
begin

select Config, CHValue, NValue, Comments from MSysConf where Config=@Config

end
GO
