USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_employees_by_id]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Req_Employees
CREATE PROCEDURE [dbo].[efrcrm_get_req_employees_by_id] @Employee_Id int AS
begin

select Employee_Id, Employee_Name, Is_MIS, Password, Email, Is_Manager, Is_Active from Req_Employees where Employee_Id=@Employee_Id

end
GO
