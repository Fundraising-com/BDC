USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_templates]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Template
CREATE PROCEDURE [dbo].[efrcrm_get_templates] AS
begin

select Partner_ID, Template_Path, ReportCenterPasswd from Template

end
GO
