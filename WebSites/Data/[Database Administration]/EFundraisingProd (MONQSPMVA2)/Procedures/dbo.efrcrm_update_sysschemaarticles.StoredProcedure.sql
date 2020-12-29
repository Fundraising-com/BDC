USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sysschemaarticles]    Script Date: 02/14/2014 13:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sysschemaarticles
CREATE PROCEDURE [dbo].[efrcrm_update_sysschemaarticles] @Artid int, @Creation_script nvarchar(510), @Description nvarchar(510), @Dest_object sysname, @Name sysname, @Objid int, @Pubid int, @Pre_creation_cmd tinyint, @Status int, @Type tinyint, @Schema_option binary, @Dest_owner sysname AS
begin

update Sysschemaarticles set Creation_script=@Creation_script, Description=@Description, Dest_object=@Dest_object, Name=@Name, Objid=@Objid, Pubid=@Pubid, Pre_creation_cmd=@Pre_creation_cmd, Status=@Status, Type=@Type, Schema_option=@Schema_option, Dest_owner=@Dest_owner where Artid=@Artid

end
GO
