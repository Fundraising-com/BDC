USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_department]    Script Date: 02/14/2014 13:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Department
CREATE PROCEDURE [dbo].[efrcrm_update_department] @Department_Id int, @Department_name varchar(50) AS
begin

update Department set Department_name=@Department_name where Department_Id=@Department_Id

end
GO
