USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_organization]    Script Date: 02/14/2014 13:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Organization
CREATE PROCEDURE [dbo].[efrcrm_insert_organization] @Organization_ID int OUTPUT, @FSM_ID int, @Flag_Pole_ID int, @Organization_Name varchar(50), @Organization_Status_ID int, @Address varchar(50), @City varchar(30), @Organization_Type_ID int, @Zip varchar(10), @Number_of_Members int, @Number_of_Class_Rooms int, @State_Code varchar(10), @Country_Code varchar(10), @Agent_ID int AS
begin

insert into Organization(FSM_ID, Flag_Pole_ID, Organization_Name, Organization_Status_ID, Address, City, Organization_Type_ID, Zip, Number_of_Members, Number_of_Class_Rooms, State_Code, Country_Code, Agent_ID) values(@FSM_ID, @Flag_Pole_ID, @Organization_Name, @Organization_Status_ID, @Address, @City, @Organization_Type_ID, @Zip, @Number_of_Members, @Number_of_Class_Rooms, @State_Code, @Country_Code, @Agent_ID)

select @Organization_ID = SCOPE_IDENTITY()

end
GO
