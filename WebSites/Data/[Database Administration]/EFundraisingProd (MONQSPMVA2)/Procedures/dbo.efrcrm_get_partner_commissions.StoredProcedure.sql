USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_partner_commissions]    Script Date: 02/14/2014 13:05:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Partner_commission
CREATE PROCEDURE [dbo].[efrcrm_get_partner_commissions] AS
begin

select Partner_id, Product_class_id, Commission_rate from Partner_commission

end
GO
