USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_req_employees]    Script Date: 02/14/2014 13:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Req_Employees
CREATE PROCEDURE [dbo].[efrcrm_insert_req_employees] @Employee_Id int OUTPUT, @Employee_Name varchar(100), @Is_MIS bit, @Password varchar(20), @Email varchar(200), @Is_Manager bit, @Is_Active bit AS
begin

insert into Req_Employees(Employee_Name, Is_MIS, Password, Email, Is_Manager, Is_Active) values(@Employee_Name, @Is_MIS, @Password, @Email, @Is_Manager, @Is_Active)

select @Employee_Id = SCOPE_IDENTITY()

end
GO
