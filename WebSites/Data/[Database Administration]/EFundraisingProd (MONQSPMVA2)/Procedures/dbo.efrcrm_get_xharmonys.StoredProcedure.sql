USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_xharmonys]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for XHarmony
CREATE PROCEDURE [dbo].[efrcrm_get_xharmonys] AS
begin

select Lead_id, Year from XHarmony

end
GO
