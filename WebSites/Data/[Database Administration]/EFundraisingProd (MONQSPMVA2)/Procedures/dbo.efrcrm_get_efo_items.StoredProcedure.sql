USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_items]    Script Date: 02/14/2014 13:04:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Item
CREATE PROCEDURE [dbo].[efrcrm_get_efo_items] AS
begin

select Item_ID, Title, Price, Amount2Supplier, Amount2Group, Description from EFO_Item

end
GO
