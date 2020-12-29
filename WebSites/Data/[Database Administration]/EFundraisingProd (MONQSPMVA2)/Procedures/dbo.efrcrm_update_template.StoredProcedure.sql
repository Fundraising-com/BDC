USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_template]    Script Date: 02/14/2014 13:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Template
CREATE PROCEDURE [dbo].[efrcrm_update_template] @Partner_ID int, @Template_Path varchar(256), @ReportCenterPasswd varchar(50) AS
begin

update Template set Template_Path=@Template_Path, ReportCenterPasswd=@ReportCenterPasswd where Partner_ID=@Partner_ID

end
GO
