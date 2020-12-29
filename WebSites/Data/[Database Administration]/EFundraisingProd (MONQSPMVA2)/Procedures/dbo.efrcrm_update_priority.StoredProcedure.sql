USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_priority]    Script Date: 02/14/2014 13:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Priority
CREATE PROCEDURE [dbo].[efrcrm_update_priority] @Priority_ID int, @Description varchar(50), @Color_Code int AS
begin

update Priority set Description=@Description, Color_Code=@Color_Code where Priority_ID=@Priority_ID

end
GO
