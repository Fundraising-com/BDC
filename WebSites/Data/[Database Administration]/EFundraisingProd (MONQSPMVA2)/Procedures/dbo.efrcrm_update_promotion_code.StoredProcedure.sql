USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_promotion_code]    Script Date: 02/14/2014 13:08:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Promotion_Code
CREATE PROCEDURE [dbo].[efrcrm_update_promotion_code] @Promotion_Code_ID int, @Promotion_Code_Desc varchar(25) AS
begin

update Promotion_Code set Promotion_Code_Desc=@Promotion_Code_Desc where Promotion_Code_ID=@Promotion_Code_ID

end
GO
