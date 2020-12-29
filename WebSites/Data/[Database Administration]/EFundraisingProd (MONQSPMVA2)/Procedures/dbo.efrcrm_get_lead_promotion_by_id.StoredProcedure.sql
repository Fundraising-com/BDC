USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_promotion_by_id]    Script Date: 02/14/2014 13:05:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Lead_Promotion
CREATE PROCEDURE [dbo].[efrcrm_get_lead_promotion_by_id] @Lead_Promotion_Id int AS
begin

select Lead_Promotion_Id, Lead_Id, Promotion_Id, Entry_Date from Lead_Promotion where Lead_Promotion_Id=@Lead_Promotion_Id

end
GO
