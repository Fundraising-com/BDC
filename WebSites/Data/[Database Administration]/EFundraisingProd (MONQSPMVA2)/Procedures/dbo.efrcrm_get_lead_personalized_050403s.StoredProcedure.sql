USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_personalized_050403s]    Script Date: 02/14/2014 13:04:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Personalized_050403
CREATE PROCEDURE [dbo].[efrcrm_get_lead_personalized_050403s] AS
begin

select GoodEmail from Lead_Personalized_050403

end
GO
