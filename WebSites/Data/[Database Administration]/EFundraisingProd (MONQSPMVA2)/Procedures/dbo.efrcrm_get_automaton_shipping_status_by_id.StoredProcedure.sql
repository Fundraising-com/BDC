USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automaton_shipping_status_by_id]    Script Date: 02/14/2014 13:03:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Automaton_Shipping_Status
CREATE PROCEDURE [dbo].[efrcrm_get_automaton_shipping_status_by_id] @From_Status_ID int AS
begin

select From_Status_ID, To_Status_ID from Automaton_Shipping_Status where From_Status_ID=@From_Status_ID

end
GO
