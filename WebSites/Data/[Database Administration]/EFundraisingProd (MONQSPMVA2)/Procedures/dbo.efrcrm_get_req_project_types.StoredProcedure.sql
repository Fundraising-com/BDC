USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_project_types]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Req_Project_Type
CREATE PROCEDURE [dbo].[efrcrm_get_req_project_types] AS
begin

select Project_Type_ID, Language_Id, Description from Req_Project_Type

end
GO
