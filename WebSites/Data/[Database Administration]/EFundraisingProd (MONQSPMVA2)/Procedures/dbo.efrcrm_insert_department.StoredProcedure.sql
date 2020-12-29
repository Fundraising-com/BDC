USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_department]    Script Date: 02/14/2014 13:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Department
CREATE PROCEDURE [dbo].[efrcrm_insert_department] @Department_Id int OUTPUT, @Department_name varchar(50) AS
begin

insert into Department(Department_name) values(@Department_name)

select @Department_Id = SCOPE_IDENTITY()

end
GO
