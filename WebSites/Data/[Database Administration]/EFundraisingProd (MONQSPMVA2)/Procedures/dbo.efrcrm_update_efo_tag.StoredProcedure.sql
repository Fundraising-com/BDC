USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_tag]    Script Date: 02/14/2014 13:07:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Tag
CREATE PROCEDURE [dbo].[efrcrm_update_efo_tag] @Email_Type_ID int, @Tag_Name varchar(50), @Tag_ID int AS
begin

update EFO_Tag set Tag_Name=@Tag_Name, Tag_ID=@Tag_ID where Email_Type_ID=@Email_Type_ID

end
GO
