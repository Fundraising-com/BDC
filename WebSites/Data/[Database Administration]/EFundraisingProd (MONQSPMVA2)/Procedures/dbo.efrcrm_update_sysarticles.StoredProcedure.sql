USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sysarticles]    Script Date: 02/14/2014 13:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sysarticles
CREATE PROCEDURE [dbo].[efrcrm_update_sysarticles] @Artid int, @Columns varbinary, @Creation_script nvarchar(510), @Del_cmd nvarchar(510), @Description nvarchar(510), @Dest_table sysname, @Filter int, @Filter_clause ntext, @Ins_cmd nvarchar(510), @Name sysname, @Objid int, @Pubid int, @Pre_creation_cmd tinyint, @Status tinyint, @Sync_objid int, @Type tinyint, @Upd_cmd nvarchar(510), @Schema_option binary, @Dest_owner sysname AS
begin

update Sysarticles set Columns=@Columns, Creation_script=@Creation_script, Del_cmd=@Del_cmd, Description=@Description, Dest_table=@Dest_table, Filter=@Filter, Filter_clause=@Filter_clause, Ins_cmd=@Ins_cmd, Name=@Name, Objid=@Objid, Pubid=@Pubid, Pre_creation_cmd=@Pre_creation_cmd, Status=@Status, Sync_objid=@Sync_objid, Type=@Type, Upd_cmd=@Upd_cmd, Schema_option=@Schema_option, Dest_owner=@Dest_owner where Artid=@Artid

end
GO
