USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_rs_lastcommit_by_id]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Rs_lastcommit
CREATE PROCEDURE [dbo].[efrcrm_get_rs_lastcommit_by_id] @Origin int AS
begin

select Origin, Origin_qid, Secondary_qid, Origin_time, Commit_time from Rs_lastcommit where Origin=@Origin

end
GO
