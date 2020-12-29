USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_deposit_item_by_id]    Script Date: 02/14/2014 13:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Deposit_Item
CREATE PROCEDURE [dbo].[efrcrm_get_deposit_item_by_id] @Deposit_ID int AS
begin

select Deposit_ID, Sales_ID, Paiement_No from Deposit_Item where Deposit_ID=@Deposit_ID

end
GO
