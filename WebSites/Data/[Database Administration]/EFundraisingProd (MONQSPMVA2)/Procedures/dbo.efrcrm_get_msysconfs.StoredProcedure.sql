USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_msysconfs]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for MSysConf
CREATE PROCEDURE [dbo].[efrcrm_get_msysconfs] AS
begin

select Config, CHValue, NValue, Comments from MSysConf

end
GO
