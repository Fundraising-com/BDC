USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_xjumpstarts]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for XJumpstart
CREATE PROCEDURE [dbo].[efrcrm_get_xjumpstarts] AS
begin

select Lead_id, Year from XJumpstart

end
GO
