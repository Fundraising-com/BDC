USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_site]    Script Date: 02/14/2014 13:07:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Site
CREATE PROCEDURE [dbo].[efrcrm_insert_site] @Site_Id int OUTPUT, @Site_Title varchar(150), @Site_Content varchar(150) AS
begin

insert into Site(Site_Title, Site_Content) values(@Site_Title, @Site_Content)

select @Site_Id = SCOPE_IDENTITY()

end
GO
