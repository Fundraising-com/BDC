USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_automaton_shipping_status]    Script Date: 02/14/2014 13:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Automaton_Shipping_Status
CREATE PROCEDURE [dbo].[efrcrm_insert_automaton_shipping_status] @From_Status_ID int OUTPUT, @To_Status_ID int AS
begin

insert into Automaton_Shipping_Status(To_Status_ID) values(@To_Status_ID)

select @From_Status_ID = SCOPE_IDENTITY()

end
GO
