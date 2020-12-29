USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_template]    Script Date: 02/14/2014 13:07:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Template
CREATE PROCEDURE [dbo].[efrcrm_insert_template] @Partner_ID int OUTPUT, @Template_Path varchar(256), @ReportCenterPasswd varchar(50) AS
begin

insert into Template(Template_Path, ReportCenterPasswd) values(@Template_Path, @ReportCenterPasswd)

select @Partner_ID = SCOPE_IDENTITY()

end
GO
