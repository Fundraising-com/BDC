USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_web_site]    Script Date: 02/14/2014 13:07:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Web_Site
CREATE PROCEDURE [dbo].[efrcrm_insert_web_site] @Web_Site_Id int OUTPUT, @Web_Site_Name varchar(50) AS
begin

insert into Web_Site(Web_Site_Name) values(@Web_Site_Name)

select @Web_Site_Id = SCOPE_IDENTITY()

end
GO
