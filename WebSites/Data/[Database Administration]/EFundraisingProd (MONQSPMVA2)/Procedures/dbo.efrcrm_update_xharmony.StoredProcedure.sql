USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_xharmony]    Script Date: 02/14/2014 13:08:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for XHarmony
CREATE PROCEDURE [dbo].[efrcrm_update_xharmony] @Lead_id int, @Year int AS
begin

update XHarmony set Year=@Year where Lead_id=@Lead_id

end
GO
