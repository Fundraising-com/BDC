USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_fsm_address_by_id]    Script Date: 02/14/2014 13:04:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for FSM_Address
CREATE PROCEDURE [dbo].[efrcrm_get_fsm_address_by_id] @FSM_Address_ID int AS
begin

select FSM_Address_ID, FSM_ID, Country_Code, State_Code, FSM_Address_Type, City, Zip, Street_Address from FSM_Address where FSM_Address_ID=@FSM_Address_ID

end
GO
