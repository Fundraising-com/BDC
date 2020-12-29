USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_xavier]    Script Date: 02/14/2014 13:08:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Xavier
CREATE PROCEDURE [dbo].[efrcrm_update_xavier] @Lead_id int, @Type nvarchar(100) AS
begin

update Xavier set Type=@Type where Lead_id=@Lead_id

end
GO
