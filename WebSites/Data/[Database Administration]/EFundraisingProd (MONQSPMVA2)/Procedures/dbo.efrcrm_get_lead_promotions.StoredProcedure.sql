USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_promotions]    Script Date: 02/14/2014 13:05:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Promotion
CREATE PROCEDURE [dbo].[efrcrm_get_lead_promotions] AS
begin

select Lead_Promotion_Id, Lead_Id, Promotion_Id, Entry_Date from Lead_Promotion

end
GO
