USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_lead_sale_michauds]    Script Date: 02/14/2014 13:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead_sale_michaud
CREATE PROCEDURE [dbo].[efrcrm_get_lead_sale_michauds] AS
begin

select Lead_id from Lead_sale_michaud

end
GO
