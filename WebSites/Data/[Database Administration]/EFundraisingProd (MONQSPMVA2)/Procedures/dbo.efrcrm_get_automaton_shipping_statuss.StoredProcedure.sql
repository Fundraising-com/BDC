USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automaton_shipping_statuss]    Script Date: 02/14/2014 13:03:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Automaton_Shipping_Status
CREATE PROCEDURE [dbo].[efrcrm_get_automaton_shipping_statuss] AS
begin

select From_Status_ID, To_Status_ID from Automaton_Shipping_Status

end
GO
