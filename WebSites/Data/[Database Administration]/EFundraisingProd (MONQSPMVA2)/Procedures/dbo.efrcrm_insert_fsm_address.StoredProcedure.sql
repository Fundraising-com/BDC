USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_fsm_address]    Script Date: 02/14/2014 13:06:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for FSM_Address
CREATE PROCEDURE [dbo].[efrcrm_insert_fsm_address] @FSM_Address_ID int OUTPUT, @FSM_ID int, @Country_Code varchar(10), @State_Code varchar(10), @FSM_Address_Type varchar(5), @City varchar(30), @Zip varchar(10), @Street_Address varchar(35) AS
begin

insert into FSM_Address(FSM_ID, Country_Code, State_Code, FSM_Address_Type, City, Zip, Street_Address) values(@FSM_ID, @Country_Code, @State_Code, @FSM_Address_Type, @City, @Zip, @Street_Address)

select @FSM_Address_ID = SCOPE_IDENTITY()

end
GO
