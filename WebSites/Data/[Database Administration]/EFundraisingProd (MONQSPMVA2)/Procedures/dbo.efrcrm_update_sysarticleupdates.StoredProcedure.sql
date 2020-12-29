USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_sysarticleupdates]    Script Date: 02/14/2014 13:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Sysarticleupdates
CREATE PROCEDURE [dbo].[efrcrm_update_sysarticleupdates] @Artid int, @Pubid int, @Sync_ins_proc int, @Sync_upd_proc int, @Sync_del_proc int, @Autogen bit, @Sync_upd_trig int, @Conflict_tableid int, @Ins_conflict_proc int, @Identity_support bit AS
begin

update Sysarticleupdates set Pubid=@Pubid, Sync_ins_proc=@Sync_ins_proc, Sync_upd_proc=@Sync_upd_proc, Sync_del_proc=@Sync_del_proc, Autogen=@Autogen, Sync_upd_trig=@Sync_upd_trig, Conflict_tableid=@Conflict_tableid, Ins_conflict_proc=@Ins_conflict_proc, Identity_support=@Identity_support where Artid=@Artid

end
GO
