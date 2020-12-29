USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_department_by_id]    Script Date: 02/14/2014 13:04:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Department
CREATE PROCEDURE [dbo].[efrcrm_get_department_by_id] @Department_Id int AS
begin

select Department_Id, Department_name from Department where Department_Id=@Department_Id

end
GO
