USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_departments]    Script Date: 02/14/2014 13:04:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Department
CREATE PROCEDURE [dbo].[efrcrm_get_departments] AS
begin

select Department_Id, Department_name from Department

end
GO
