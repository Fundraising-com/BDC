USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_tag]    Script Date: 02/14/2014 13:06:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Tag
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_tag] @Email_Type_ID int OUTPUT, @Tag_Name varchar(50), @Tag_ID int AS
begin

insert into EFO_Tag(Tag_Name, Tag_ID) values(@Tag_Name, @Tag_ID)

select @Email_Type_ID = SCOPE_IDENTITY()

end
GO
