USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_req_employees]    Script Date: 02/14/2014 13:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Req_Employees
CREATE PROCEDURE [dbo].[efrcrm_update_req_employees] @Employee_Id int, @Employee_Name varchar(100), @Is_MIS bit, @Password varchar(20), @Email varchar(200), @Is_Manager bit, @Is_Active bit AS
begin

update Req_Employees set Employee_Name=@Employee_Name, Is_MIS=@Is_MIS, Password=@Password, Email=@Email, Is_Manager=@Is_Manager, Is_Active=@Is_Active where Employee_Id=@Employee_Id

end
GO
