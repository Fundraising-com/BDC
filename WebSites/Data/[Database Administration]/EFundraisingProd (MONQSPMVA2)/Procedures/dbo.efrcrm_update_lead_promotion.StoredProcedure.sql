USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_promotion]    Script Date: 02/14/2014 13:08:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Promotion
CREATE PROCEDURE [dbo].[efrcrm_update_lead_promotion] @Lead_Promotion_Id int, @Lead_Id int, @Promotion_Id int, @Entry_Date smalldatetime AS
begin

update Lead_Promotion set Lead_Id=@Lead_Id, Promotion_Id=@Promotion_Id, Entry_Date=@Entry_Date where Lead_Promotion_Id=@Lead_Promotion_Id

end
GO
