USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_template_by_id]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Template
CREATE PROCEDURE [dbo].[efrcrm_get_template_by_id] @Partner_ID int AS
begin

select Partner_ID, Template_Path, ReportCenterPasswd from Template where Partner_ID=@Partner_ID

end
GO
