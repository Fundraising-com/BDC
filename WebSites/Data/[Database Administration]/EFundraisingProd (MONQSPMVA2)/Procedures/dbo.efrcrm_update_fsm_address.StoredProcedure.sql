USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_fsm_address]    Script Date: 02/14/2014 13:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for FSM_Address
CREATE PROCEDURE [dbo].[efrcrm_update_fsm_address] @FSM_Address_ID int, @FSM_ID int, @Country_Code varchar(10), @State_Code varchar(10), @FSM_Address_Type varchar(5), @City varchar(30), @Zip varchar(10), @Street_Address varchar(35) AS
begin

update FSM_Address set FSM_ID=@FSM_ID, Country_Code=@Country_Code, State_Code=@State_Code, FSM_Address_Type=@FSM_Address_Type, City=@City, Zip=@Zip, Street_Address=@Street_Address where FSM_Address_ID=@FSM_Address_ID

end
GO
