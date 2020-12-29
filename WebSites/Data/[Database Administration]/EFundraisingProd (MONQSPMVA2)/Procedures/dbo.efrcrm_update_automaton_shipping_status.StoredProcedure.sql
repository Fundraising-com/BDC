USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_automaton_shipping_status]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Automaton_Shipping_Status
CREATE PROCEDURE [dbo].[efrcrm_update_automaton_shipping_status] @From_Status_ID int, @To_Status_ID int AS
begin

update Automaton_Shipping_Status set To_Status_ID=@To_Status_ID where From_Status_ID=@From_Status_ID

end
GO
