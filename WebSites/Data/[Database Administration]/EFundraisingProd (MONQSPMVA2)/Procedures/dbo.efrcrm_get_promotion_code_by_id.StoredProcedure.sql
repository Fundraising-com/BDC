USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotion_code_by_id]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Promotion_Code
CREATE PROCEDURE [dbo].[efrcrm_get_promotion_code_by_id] @Promotion_Code_ID int AS
begin

select Promotion_Code_ID, Promotion_Code_Desc from Promotion_Code where Promotion_Code_ID=@Promotion_Code_ID

end
GO
