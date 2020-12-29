USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_xavier_by_id]    Script Date: 02/14/2014 13:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Xavier
CREATE PROCEDURE [dbo].[efrcrm_get_xavier_by_id] @Lead_id int AS
begin

select Lead_id, Type from Xavier where Lead_id=@Lead_id

end
GO
