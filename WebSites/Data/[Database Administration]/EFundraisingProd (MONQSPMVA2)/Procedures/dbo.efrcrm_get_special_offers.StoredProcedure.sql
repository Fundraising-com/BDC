USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_special_offers]    Script Date: 02/14/2014 13:06:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Special_offer
CREATE PROCEDURE [dbo].[efrcrm_get_special_offers] AS
begin

select Special_offer_id, Brand_id, Product_class_id, Special_offer_text from Special_offer

end
GO
