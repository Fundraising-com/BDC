USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_organization]    Script Date: 02/14/2014 13:08:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Organization
CREATE PROCEDURE [dbo].[efrcrm_update_organization] @Organization_ID int, @FSM_ID int, @Flag_Pole_ID int, @Organization_Name varchar(50), @Organization_Status_ID int, @Address varchar(50), @City varchar(30), @Organization_Type_ID int, @Zip varchar(10), @Number_of_Members int, @Number_of_Class_Rooms int, @State_Code varchar(10), @Country_Code varchar(10), @Agent_ID int AS
begin

update Organization set FSM_ID=@FSM_ID, Flag_Pole_ID=@Flag_Pole_ID, Organization_Name=@Organization_Name, Organization_Status_ID=@Organization_Status_ID, Address=@Address, City=@City, Organization_Type_ID=@Organization_Type_ID, Zip=@Zip, Number_of_Members=@Number_of_Members, Number_of_Class_Rooms=@Number_of_Class_Rooms, State_Code=@State_Code, Country_Code=@Country_Code, Agent_ID=@Agent_ID where Organization_ID=@Organization_ID

end
GO
