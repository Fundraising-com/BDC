USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_deposit_items]    Script Date: 02/14/2014 13:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Deposit_Item
CREATE PROCEDURE [dbo].[efrcrm_get_deposit_items] AS
begin

select Deposit_ID, Sales_ID, Paiement_No from Deposit_Item

end
GO
