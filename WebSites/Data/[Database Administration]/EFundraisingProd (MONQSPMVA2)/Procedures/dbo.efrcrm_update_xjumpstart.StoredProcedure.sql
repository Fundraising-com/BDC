USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_xjumpstart]    Script Date: 02/14/2014 13:08:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for XJumpstart
CREATE PROCEDURE [dbo].[efrcrm_update_xjumpstart] @Lead_id int, @Year int AS
begin

update XJumpstart set Year=@Year where Lead_id=@Lead_id

end
GO
