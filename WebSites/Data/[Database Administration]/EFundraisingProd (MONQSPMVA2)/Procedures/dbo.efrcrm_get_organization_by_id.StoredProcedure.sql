USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_organization_by_id]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Organization
CREATE PROCEDURE [dbo].[efrcrm_get_organization_by_id] @Organization_ID int AS
begin

select Organization_ID, FSM_ID, Flag_Pole_ID, Organization_Name, Organization_Status_ID, Address, City, Organization_Type_ID, Zip, Number_of_Members, Number_of_Class_Rooms, State_Code, Country_Code, Agent_ID from Organization where Organization_ID=@Organization_ID

end
GO
