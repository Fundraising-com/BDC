USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_tags]    Script Date: 02/14/2014 13:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Tags
CREATE PROCEDURE [dbo].[efrcrm_insert_tags] @Tags_ID int OUTPUT, @Label varchar(100), @Control_Name varchar(100) AS
begin

insert into Tags(Label, Control_Name) values(@Label, @Control_Name)

select @Tags_ID = SCOPE_IDENTITY()

end
GO
