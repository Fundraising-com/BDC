USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_confirmation_method]    Script Date: 02/14/2014 13:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Confirmation_Method
CREATE PROCEDURE [dbo].[efrcrm_update_confirmation_method] @Confirmation_Method_ID int, @Description varchar(50) AS
begin

update Confirmation_Method set Description=@Description where Confirmation_Method_ID=@Confirmation_Method_ID

end
GO
