USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_personalizeds]    Script Date: 02/14/2014 13:04:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_Personalized
CREATE PROCEDURE [dbo].[efrcrm_get_lead_personalizeds] AS
begin

select GoodEmail from Lead_Personalized

end
GO
