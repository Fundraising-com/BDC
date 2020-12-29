USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_packagess]    Script Date: 02/14/2014 13:05:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Packages
CREATE PROCEDURE [dbo].[efrcrm_get_packagess] AS
begin

select Package_id, Parent_package_id, Package_template_id, Accounting_class_id, Package_name, Profit_percentage, Display_order, Package_enabled, Contains_products, Nb_participants_per_case from Packages

end
GO
